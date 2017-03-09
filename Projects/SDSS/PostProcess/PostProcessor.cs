using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using eZstd.Miscellaneous;
using SDSS.Project;
using SDSS.Solver;
using SDSS.Utility;

namespace SDSS.PostProcess
{
    internal class PostProcessor
    {
        public AbaqusSolver Solver { get; private set; }
        private StreamReader _sr;

        /// <summary> 整个 Abaqus 计算得到的所有结果，而不是用来导出的筛选过的结果 </summary>
        public Result Results { get; private set; }

        #region ---   构造函数

        public PostProcessor(AbaqusSolver solver)
        {
            Solver = solver;
        }

        /// <summary> 析构函数 </summary>
        ~PostProcessor()
        {
            if (_sr != null)
            {
                _sr.Close();
            }
        }

        #endregion

        #region ---   检查生成报告的条件

        /// <summary> 检查求解过程是否是由Abaqus自行结束 </summary>
        /// <returns> 如果是，则不论求解过程是否成功，都则返回 true。返回 false 则表示是由用户强制终止计算过程 </returns>
        public bool CheckFinishState(ref StringBuilder errorMessage)
        {
            if ((Solver.State & SolverState.SelfFinished) > 0)
            {

                return true;

            }
            else
            {
                errorMessage.AppendLine(@"Abaqus 求解过程被用户强制终止。");
                return false;
            }
        }

        /// <summary>
        /// 对 Abaqus 计算完成后的 output 文件、 result 文件等进行检查，以判断 Abaqus 的求解过程是否成功
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public SolverState CheckResultFiles(ref StringBuilder errorMessage)
        {
            // 检查 output 文件中的数据
            string outputFile = ProjectPaths.F_PyOutput;
            SolverState state = SolverState.SelfFinished;
            if (File.Exists(outputFile))
            {
                using (var sr = new StreamReader(outputFile))
                {
                    string line;
                    do
                    {
                        line = sr.ReadLine();
                        if (line.Equals(Constants.FileExtensions.CalculationSucceededTag))
                        {
                            state = SolverState.Succeeded;
                            break;
                        }
                        else if (line.Equals(Constants.FileExtensions.CalculationFailedTag))
                        {
                            errorMessage.AppendLine("Abaqus 计算过程出错而导致计算结束。");
                            state = SolverState.FailedWithError;
                            break;
                        }
                    } while (sr.EndOfStream);
                }
            }
            else
            {
                errorMessage.AppendLine("指定的Abaqus output 文件未找到");
                return SolverState.FailedWithError;
            }

            // 检查 result 文件中的数据
            if (state == SolverState.Succeeded)
            {
                //
                string resultFile = ProjectPaths.F_AbqResult;

                if (!File.Exists(resultFile))
                {
                    errorMessage.AppendLine("指定的Abaqus计算结果文件未找到");
                    return SolverState.FailedWithError;
                }
            }

            return state;
        }

        #endregion

        #region ---   计算结果数据的提取

        /// <summary> 从 Result.sdr 文件中读取计算结果 </summary>
        public void ReadResultFile(string resultFilePath)
        {
            var res = ResultConstructor.ReadResult(resultFilePath);
            this.Results = res;
        }

        #endregion

        #region ---   撰写报告

        /// <summary> 列出所有的计算结果，并显示在窗口中，以供用户选择性导出 </summary>
        public void ShowResultsList()
        {
           if (this.Results != null)
            {
                var rl = new ResultLister(Results);
                rl.ShowDialog(null);
            }
        }


        #endregion
    }
}