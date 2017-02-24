using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using eZstd.Diagnostics;
using SDSS.Definitions;
using SDSS.Utility;

namespace SDSS.Solver
{
    /// <summary>
    /// Abaqus 计算求解器
    /// </summary>
    internal class AbaqusSolver
    {
        #region ---   Fields

        /// <summary> Abaqus 的工作文件夹 </summary>
        private readonly string _workingDir;

        private readonly ModelType _modelType;

        private readonly SolverGUI _solverGui;

        /// <summary> 求解器当前的求解状态 </summary>
        public SolverState State { get; private set; }

        #endregion

        /// <summary> 构造函数 </summary>
        /// <param name="workingDir">Abaqus 的工作文件夹</param>
        /// <param name="modelType">  </param>
        public AbaqusSolver(string workingDir, ModelType modelType, SolverGUI solverGui)
        {
            //workingDir = @"C:\Users\zengfy\Desktop\AbaqusScriptTest\run.cmd";
            _workingDir = workingDir;
            ProjectPaths.SetAbaqusWorkingDir(workingDir);
            //
            State = SolverState.NotStarted;
            _solverGui = solverGui;
            _modelType = modelType;
            //
            ProjectPaths.DetermineSolverSource(modelType);
        }

        #region ---   创建 .bat 命令文件，以启动 Abaqus 的计算

        public bool CreateBatCommand()
        {
            if (!string.IsNullOrEmpty(ProjectPaths.F_PySolver))
            {
                StreamWriter sw = new StreamWriter(ProjectPaths.F_InitialBat, append: false);

                string fp = CreateBatCommand(workingDir: _workingDir, pyFile: ProjectPaths.F_PySolver);
                sw.WriteLine(fp);
                sw.Close();
                return true;
            }
            return false;
        }

        /// <summary> 通过cmd启动Abaqus的 .bat 命令 </summary>
        /// <param name="workingDir">Abaqus 的工作文件夹</param>
        /// <param name="pyFile"> Abaqus 的脚本文件的绝对路径 </param>
        /// <returns></returns>
        private string CreateBatCommand(string workingDir, string pyFile)
        {
            string solGui = "";
            switch (_solverGui)
            {
                case SolverGUI.CAE: solGui = "script"; break;
                default: solGui = "noGUI"; break;
            }

            string cmd = @"@echo off
rem : The directory containing the files created during the calcution as well as the results.
cd /d " + workingDir + @"

rem : Execute Abaqus without showing the users interface.
abaqus cae " + solGui + "=" + pyFile; // 如果要显示 Abaqus CAE 界面，则将 noGUI 修改为 script
            return cmd;

            /* 写入 .bat 文件中的内容如下：
@echo off
rem : The directory containing the files created during the calcution as well as the results.
cd /d C:\Users\zengfy\Desktop\AbaqusScriptTest

rem : Execute Abaqus without showing the users interface.
abaqus cae noGUI=beamExample.py         
*/
        }

        #endregion

        #region ---   计算环境检查

        /// <summary> 检查计算环境，文件配置 </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckEnvironment(StationModel.StationModel sm, out string errorMessage)
        {
            try
            {
                if (!Directory.Exists(_workingDir))
                {
                    errorMessage = "指定的Abaqus工作文件夹不存在";
                    return false;
                }

                // 1. 指定用来计算的车站模型文件
                if (!File.Exists(ProjectPaths.F_ModelFile))
                {
                    string defaultModelFile = Path.Combine(_workingDir, ProjectPaths.FN_DefaultModel);

                    //ProjectPaths.SerializeNewModelFile(xmlFilePath: defaultModelFile, stationModel: sm,
                    //    errorMessage: out errorMessage);
                    Utils.ExportToXmlFile(xmlFilePath: defaultModelFile, src: sm, errorMessage: out errorMessage);
                }

                // 2. 创建启动 Abaqus 的 .bat 文件
                CreateBatCommand();

                // 3. 将存储有模型参数、计算参数的文件所在的路径写入到一个单独的文本中，以供 Python 程序读取。
                ProjectPaths.WriteCalcFilePaths();

                //
                errorMessage = "计算环境正常，可以开始计算";
                return true;
            }
            catch (Exception)
            {
                errorMessage = "计算环境异常";
                return false;
            }
        }
        
        #endregion

        #region ---   Execute 执行计算

        /// <summary>
        /// 开始执行 Abaqus 的计算
        /// </summary>
        /// <param name="waitingSeconds"> 每隔一段时间提示用户是否还要继续等待计算完成。</param>
        /// <returns>如果计算完成且成功，则返回true，如果计算中断，或者计算失败，则返回 false</returns>
        public SolverState Execute(int waitingSeconds, out string errorMessage)
        {
            // 1. delete the abaqus lock file,if this file exists, the cmd will be terminated.
            var lockFiles = Directory.EnumerateFiles(path: _workingDir, searchPattern: "*.lck",
                searchOption: SearchOption.TopDirectoryOnly);
            if (lockFiles.Any())
            {
                foreach (string lockfile in lockFiles)
                {
                    File.Delete(lockfile);
                }
            }

            // 2. 开始计算
            var succ = RunAndWaitforExit(batFileName: ProjectPaths.F_InitialBat, waitingSeconds: waitingSeconds);

            errorMessage = "计算完成";
            return succ;
        }

        /// <summary>
        /// 以同步阻塞的方式，通过 cmd 打开 .bat 命令，同时隐藏 cmd 窗口。
        /// </summary>
        /// <param name="batFileName">.bat 命令的完整绝对路径</param>
        /// <param name="waitingSeconds">每隔一段时间提示用户是否还要继续等待计算完成。</param>
        /// <returns></returns>
        private SolverState RunAndWaitforExit(string batFileName, int waitingSeconds)
        {
            State = SolverState.Calculating;
            //batFileName = @"C:\Users\zengfy\Desktop\AbaqusScriptTest\run.cmd";

            // var t = IO.ShellExecute(IntPtr.Zero, "open", batFileName, "", "", ShowCommands.SW_HIDE);
            // 上面的这种方式也可以达到通过 cmd 运行 Abaqus，并隐藏 cmd窗口的效果，但是 ShellExecute() 为异步操作
            Process p = new Process();
            p.StartInfo.FileName = batFileName;

            // :warning: 要想通过cmd运行.bat文件，又不显示cmd黑窗，必须使用下面两个组合属性
            p.StartInfo.UseShellExecute = false; // false
            p.StartInfo.CreateNoWindow = (_solverGui == SolverGUI.NoGUI); // true

            //
            p.Start(); // 异步执行
            State = SolverState.Calculating;
            p.WaitForExit(waitingSeconds * 1000); // 线程等待
            State = SolverState.Succeeded;
            while (!p.HasExited)
            {
                var res = MessageBox.Show($"已经等待{waitingSeconds}秒，计算还未完成，是否继续等待？",
                    @"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    p.WaitForExit(waitingSeconds * 1000);
                    State = SolverState.Succeeded;
                }
                else
                {
                    try
                    {
                        TerminateAbqCmd(p);
                        State = SolverState.UserTerminated;
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                        State = SolverState.UserTerninationFailed;
                        // ignored
                    }
                }
            }
            return State;
        }

        /// <summary> 终止 Abaqus 的计算进程树 </summary>
        private static void TerminateAbqCmd(Process proc)
        {
            ProcessesKiller.KillProcessAndChildren(proc.Id);

            //Process[] ps = Process.GetProcessesByName("cmd"); // abq6121 或 ABQcaeK 或 cmd
            //if (ps.Any())
            //{
            //    Process p = ps[0];
            //    ProcessesKiller.KillProcessAndChildren(p.Id);
            //}
        }

        #endregion
    }
}