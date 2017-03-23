using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using eZstd.Diagnostics;
using SDSS.Project;
using SDSS.Models;
using SDSS.Utility;
using Timer = System.Timers.Timer;

namespace SDSS.Solver
{
    /// <summary>
    /// Ansys 计算求解器
    /// </summary>
    internal class AnsysSolver : IDisposable
    {
        #region ---   Fields

        /// <summary> Ansys 的工作文件夹 </summary>
        public readonly AnsysWorkingDir WorkingDir;


        private readonly SolverGUI _solverGui;

        /// <summary> 求解器当前的求解状态 </summary>
        public SolverState State { get; private set; }

        public Process AbqProcess { get; private set; }

        /// <summary> 是否要强制退出 Ansys 计算过程 </summary>
        public bool ForceKillProcess { get; private set; }

        #endregion

        /// <summary> 构造函数 </summary>
        /// <param name="workingDir">Ansys 的工作文件夹</param>
        /// <param name="modelName"> 模型名称 </param>
        /// <param name="solverGui">  </param>
        public AnsysSolver(AnsysWorkingDir workingDir, SolverGUI solverGui)
        {
            //workingDir = @"C:\Users\zengfy\Desktop\AnsysScriptTest\run.cmd";
            WorkingDir = workingDir;

            // ProjectPaths.SetAnsysWorkingDir(workingDir);
            //
            State = SolverState.NotStarted;
            _solverGui = SolverGUI.NoGUI;
            //
        }

        public void Dispose()
        {
            // TerminateAbqCmd();
        }

        #region ---   ！ 配置计算环境，将“前处理”的数据与“中间计算过程”进行连接

        /// <summary> 配置计算环境，文件配置 </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool SetEnvironment(ModelBase sm, ref StringBuilder errorMessage)
        {
            try
            {
                if (!Directory.Exists(WorkingDir.WorkingDirectory))
                {
                    errorMessage.AppendLine("指定的Ansys工作文件夹不存在");
                    return false;
                }

                // 1. 指定用来计算的车站模型文件
                sm.WriteCalculateFileForAnsys(WorkingDir.F_ModelParameter, ref errorMessage);

                // 2. 创建启动 Ansys 的 .bat 文件
                CreateBatCommand(sm);

                //// 这一条在最后程序完成后要删除：将此文件写入 MiddleFiles 文件夹只是为了在调试时方便。
                //ProjectPaths.WriteAnsysCalcFilePaths(WorkingDir,
                //    Path.Combine(ProjectPaths.D_MiddleFiles, Path.GetFileName(WorkingDir.F_FilePaths)));

                //// 3. 将存储有模型参数、计算参数的文件所在的路径写入到一个单独的文本中，以供 Python 程序读取。
                //ProjectPaths.WriteAnsysCalcFilePaths(WorkingDir, WorkingDir.F_FilePaths);


                //
                errorMessage.AppendLine("计算环境正常，可以开始计算");
                return true;
            }
            catch (Exception ex)
            {
                errorMessage.AppendLine("计算环境异常" + "\r\n" + ex.Message);
                return false;
            }
        }

        /// <summary> 创建 .bat 命令文件，以启动 Ansys 的计算 </summary>
        public bool CreateBatCommand(ModelBase sm)
        {
            if (File.Exists(WorkingDir.F_ModelParameter))
            {
                using (StreamWriter sw = new StreamWriter(ProjectPaths.F_InitialBat, append: false))
                {
                    string ansysExe = Options.AnsysExePath; //  Ansys 计算程序所对应的文件路径，比如 C:\Softwares\Ansys\v150\ANSYS\bin\winx64\ansys150.exe
                    string wkDir = WorkingDir.WorkingDirectory;
                    string inputFile = ProjectPaths.GetAnsysModelSolverFile(sm);
                    string outputFile = WorkingDir.F_Output;
                    string jobName = sm.ModelName;

                    string cmd = @"@echo off
cd /d " + WorkingDir.WorkingDirectory + "\r\n\r\n";
                    cmd += $"\"{ansysExe}\"  -p struct -dir \"{wkDir}\" -j \"{jobName}\" - s read -l en-us -b " +
                           $"-i \"{inputFile}\" -o \"{outputFile}\"";
                    sw.WriteLine(cmd);
                }
                return true;
            }
            else
            {
                MessageBox.Show(@"用来进行Ansys计算的输入文件未找到！", @"出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region ---   Execute 执行计算

        /// <summary>
        /// 开始执行 Ansys 的计算
        /// </summary>
        /// <param name="waitingSeconds"> 每隔一段时间提示用户是否还要继续等待计算完成。</param>
        /// <returns>如果计算完成且成功，则返回true，如果计算中断，或者计算失败，则返回 false</returns>
        public SolverState Execute(uint waitingSeconds, out string errorMessage)
        {
            // 1. delete the Ansys lock file,if this file exists, the cmd will be terminated.
            //var lockFiles = Directory.EnumerateFiles(path: WorkingDir.WorkingDirectory, searchPattern: "*.lck",
            //    searchOption: SearchOption.TopDirectoryOnly);
            //if (lockFiles.Any())
            //{
            //    foreach (string lockfile in lockFiles)
            //    {
            //        File.Delete(lockfile);
            //    }
            //}

            // 2. 开始计算
            var succ = RunAndWaitforExit(batFileName: ProjectPaths.F_InitialBat, waitingSeconds: waitingSeconds);

            // 整个计算完成 --------------
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
            //
            ForceKillProcess = false;
            AbqProcess = null;
            //
            errorMessage = "计算完成";
            return succ;
        }

        /// <summary>
        /// 以同步阻塞的方式，通过 cmd 打开 .bat 命令，同时隐藏 cmd 窗口。
        /// </summary>
        /// <param name="batFileName">.bat 命令的完整绝对路径</param>
        /// <param name="waitingSeconds">每隔一段时间提示用户是否还要继续等待计算完成。</param>
        /// <returns></returns>
        private SolverState RunAndWaitforExit(string batFileName, uint waitingSeconds)
        {
            State = SolverState.Calculating;
            //batFileName = @"C:\Users\zengfy\Desktop\AnsysScriptTest\run.cmd";

            // var t = IO.ShellExecute(IntPtr.Zero, "open", batFileName, "", "", ShowCommands.SW_HIDE);
            // 上面的这种方式也可以达到通过 cmd 运行 Ansys，并隐藏 cmd窗口的效果，但是 ShellExecute() 为异步操作
            AbqProcess = new Process();
            AbqProcess.StartInfo.FileName = batFileName;

            // :warning: 要想通过cmd运行.bat文件，又不显示cmd黑窗，必须使用下面两个组合属性
            AbqProcess.StartInfo.UseShellExecute = false; // false
            AbqProcess.StartInfo.CreateNoWindow = (_solverGui == SolverGUI.NoGUI); // true

            //
            AbqProcess.Start(); // 异步执行，不阻塞
            State = SolverState.Calculating;
            // 计时操作
            _lastWaitingTime = DateTime.Now;
            _calcStartTime = _lastWaitingTime;
            _timer = new Timer(interval: 1000);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
            // 
            WaitForTerminate(waitingSeconds); // AbqProcess.WaitForExit((int)waitingSeconds * 1000); // 线程等待
            return State;
        }

        #endregion

        #region ---   计算过程的计时

        /// <summary> 当计算过程执行了一段时间 </summary>
        public event Action<Timer, TimeSpan> CalculationTimerElapsed;

        /// <summary> 用户上次刷新等待的时间点 </summary>
        private DateTime _lastWaitingTime;

        /// <summary> Ansys 开始计算的时间点 </summary>
        private DateTime _calcStartTime;

        private Timer _timer;

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            var tsp = e.SignalTime.Subtract(_calcStartTime);
            // 触发事件
            CalculationTimerElapsed?.Invoke(_timer, tsp);
        }

        #endregion

        #region ---   计算 的终止

        /// <summary> 从外部强制终止 Ansys 的计算过程（任意线程） </summary>
        public void TerminateAbqCalculation()
        {
            ForceKillProcess = true;
        }

        /// <summary> 从外部强制终止 Ansys 的计算过程（任意线程） </summary>
        private void WaitForTerminate(uint waitingSeconds)
        {
            if (AbqProcess != null)
            {
                while (!AbqProcess.HasExited)
                {
                    if (ForceKillProcess)
                    {
                        TerminateAbqCmd();
                        return;
                    }
                    var now = DateTime.Now;
                    var seconds = (now - _lastWaitingTime).Seconds;
                    if (seconds >= waitingSeconds)
                    {
                        var res = MessageBox.Show($"已经等待{waitingSeconds}秒，计算还未完成，是否继续等待？",
                            @"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            _lastWaitingTime = now;
                            State = SolverState.Calculating;
                        }
                        else
                        {
                            TerminateAbqCmd();
                            return;
                        }
                    }
                }
                State = SolverState.SelfFinished;
            }
        }

        /// <summary> 终止 Ansys 的计算进程树 </summary>
        /// <remarks> 注意此进程从哪个线程中启动，也只能从哪个线程中终止 </remarks>
        private void TerminateAbqCmd()
        {
            if (AbqProcess != null && !AbqProcess.HasExited)
            {
                try
                {
                    ProcessesKiller.KillProcessAndChildren(AbqProcess.Id);

                    //Process[] ps = Process.GetProcessesByName("cmd"); // abq6121 或 ABQcaeK 或 cmd
                    //if (ps.Any())
                    //{
                    //    Process p = ps[0];
                    //    ProcessesKiller.KillProcessAndChildren(p.Id);
                    //}

                    State = SolverState.UserTerminated;
                }
                catch (Exception)
                {
                    State = SolverState.UserTerninationFailed;
                }
                finally
                {
                    AbqProcess = null;
                }
            }
        }

        #endregion
    }
}