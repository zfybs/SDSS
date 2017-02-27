using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using eZstd.Miscellaneous;
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
            _resultFilePath = ProjectPaths.F_AbqResult;
        }

        /// <summary> 析构函数 </summary>
        ~PostProcessor()
        {
            if (_sr != null)
            {
                _sr.Close();
            }
        }

        #region ---   检查生成报告的条件

        /// <summary> 检查求解结果的状态 </summary>
        /// <returns> 如果可以继续生成报告，则返回 true </returns>
        public bool CheckSolveState(ref StringBuilder errorMessage)
        {
            // 后处理
            switch (_solver.State)
            {
                case SolverState.Succeeded:
                    errorMessage.AppendLine(@"Abaqus 求解过程正常");
                    return true;
                case SolverState.UserTerminated:
                    errorMessage.AppendLine(@"用户强行终止计算");
                    return false;
            }
            errorMessage.AppendLine(@"求解状态 SolverState 的值异常。");
            return false;
        }

        /// <summary>
        /// 对 Abaqus 计算完成后的 output 文件、 result 文件等进行检查，以判断 Abaqus 的求解过程是否成功
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckResultFiles(ref StringBuilder errorMessage)
        {
            // 检查 output 文件中的数据
            string outputFile = ProjectPaths.F_PyOutput;
            bool outputSucceeded = false;
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
                            outputSucceeded = true;
                            break;
                        }
                        else if (line.Equals(Constants.FileExtensions.CalculationFailedTag))
                        {
                            errorMessage.AppendLine("Abaqus 计算过程出错而导致计算结束。");
                            outputSucceeded = false;
                            break;
                        }
                    } while (sr.EndOfStream);
                }
            }
            else
            {
                errorMessage.AppendLine("指定的Abaqus计算结果文件未找到");
                return false;
            }

            // 检查 result 文件中的数据
            if (outputSucceeded)
            {
                //
                string resultFile = ProjectPaths.F_AbqResult;

                if (!File.Exists(resultFile))
                {
                    errorMessage.AppendLine("指定的Abaqus计算结果文件未找到");
                    return false;
                }
            }

            return outputSucceeded;
        }

        #endregion

        #region ---   计算结果数据的提取

        private Result _result;
        /// <summary> 从 Result.sdr 文件中读取计算结果 </summary>
        public void ReadFile()
        {
            _sr = new StreamReader(_resultFilePath);

            //var s = _sr.ReadLine();
            //MessageBox.Show(s);
            _sr.Close();
            _result = new Result();
        }

        #endregion

        #region ---   撰写报告

        /// <summary> 撰写报告 </summary>
        public void WriteReport()
        {
            if (_result != null)
            {
                try
                {
                    Reporter rp = new Reporter();
                    bool succ = rp.NewDocument();
                    if (succ)
                    {
                        //
                        _sr = new StreamReader(_resultFilePath);
                        while (!_sr.EndOfStream)
                        {
                            var s = _sr.ReadLine();
                            if (s.StartsWith("T"))
                            {
                                rp.InsertParagrph(rp.CollapsToEnd(), s, style: WordStyle.Title2);
                            }
                            else
                            {
                                rp.InsertParagrph(rp.CollapsToEnd(), s, style: WordStyle.Content);
                            }
                        }

                        _sr.Close();
                    }
                    rp.SetVisibility(true);
                }
                catch (Exception ex)
                {
                    DebugUtils.ShowDebugCatch(ex, "撰写报告时出现错误");
                }

            }
        }
        #endregion
    }
}