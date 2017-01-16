using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDSS
{
    internal class Solver
    {

        private string _workingDir;

        public Solver(string workingDir)
        {
            workingDir = @"C:\Users\zengfy\Desktop\AbaqusScriptTest\run.cmd";

            _workingDir = workingDir;
        }

        public bool Execute()
        {

            var bo = RunAndWaitforExit();

            return bo;
        }

        /// <summary>
        /// 以同步阻塞的方式，通过 cmd 打开 .bat 命令，同时隐藏 cmd 窗口。
        /// </summary>
        /// <param name="batFileName">.bat 命令的完整绝对路径</param>
        /// <returns></returns>
        private static bool RunAndWaitforExit(string batFileName = null)
        {

            //batFileName = @"C:\Users\zengfy\Desktop\AbaqusScriptTest\run.cmd";

            // var t = IO.ShellExecute(IntPtr.Zero, "open", batFileName, "", "", ShowCommands.SW_HIDE);
            // 上面的这种方式也可以达到通过 cmd 运行 Abaqus，并隐藏 cmd窗口的效果，但是 ShellExecute() 为异步操作
            
            int waitingSeconds = 100;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = batFileName;

            // :warning: 要想通过cmd运行.bat文件，又不显示cmd黑窗，必须使用下面两个组合属性
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;

            //
            p.Start(); // 异步执行
            p.WaitForExit(waitingSeconds * 1000); // 线程等待
            while (!p.HasExited)
            {
                var res = MessageBox.Show($"已经等待{waitingSeconds}秒，计算还未完成，是否继续等待？",
                    @"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    p.WaitForExit(waitingSeconds * 1000);
                }
                else
                {
                    // p.Kill();
                    return false;
                }
            }
            return true;
        }

    }
}
