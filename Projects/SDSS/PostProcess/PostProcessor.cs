using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SDSS.Solver;
using SDSS.Utility;

namespace SDSS.PostProcess
{
    internal class PostProcessor
    {
        private AbaqusSolver _solver;
        private string _resultFilePath;
        private StreamReader _sr;
        public PostProcessor(AbaqusSolver solver)
        {
            _solver = solver;
            _resultFilePath = Path.Combine(ProjectPaths.D_AbaqusWorkingDir, ProjectPaths.FN_AbqResult);
        }

        /// <summary> 析构函数 </summary>
        ~PostProcessor()
        {
            if (_sr != null)
            {
                _sr.Close();
            }
        }

        /// <summary> 检查求解结果的状态 </summary>
        public void CheckSolveState()
        {

            // 后处理
            switch (_solver.State)
            {
                case SolverState.Succeeded:
                    MessageBox.Show(@"计算完成", @"Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case SolverState.UserTerminated:
                    MessageBox.Show(@"用户强行终止计算", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
            }
        }

        private Result _result;

        /// <summary> 从 Result.sdr 文件中读取计算结果 </summary>
        public void ReadFile()
        {
            _sr = new StreamReader(_resultFilePath);

            var s = _sr.ReadLine();
            MessageBox.Show(s);
            //_sr.Close();
            _result = new Result();

        }

        /// <summary> 撰写报告 </summary>
        public void WriteReport()
        {
            if (_result != null)
            {
                Reporter rp = new Reporter();
                rp.NewDocument();
                rp.SetVisibility(true);
                //_result.Name
            }
        }
    }
}