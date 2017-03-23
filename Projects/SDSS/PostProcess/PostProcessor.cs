using System.IO;
using System.Text;
using SDSS.Constants;
using SDSS.Models;
using SDSS.Project;
using SDSS.Solver;

namespace SDSS.PostProcess
{
    internal class PostProcessor
    {
        public readonly ModelBase Model;
        public readonly AnsysSolver Solver;
        private readonly AnsysWorkingDir _workingDir;

        /// <summary> 整个 Ansys 计算得到的所有结果，而不是用来导出的筛选过的结果 </summary>
        public Result Results { get; private set; }

        #region ---   构造函数

        public PostProcessor()
        {
        }

        public PostProcessor(ModelBase model, AnsysSolver solver) : this()
        {
            Model = model;
            Solver = solver;
            _workingDir = solver.WorkingDir;
        }

        /// <summary> 析构函数 </summary>
        ~PostProcessor()
        {
        }

        #endregion

        #region ---   检查生成报告的条件，将“中间计算过程”所得到的计算信息与计算结果与“后处理”过程进行连接

        /// <summary> 检查求解过程是否是由Ansys自行结束 </summary>
        /// <returns> 如果是，则不论求解过程是否成功，都则返回 true。返回 false 则表示是由用户强制终止计算过程 </returns>
        public bool CheckFinishState(ref StringBuilder errorMessage)
        {
            if ((Solver.State & SolverState.SelfFinished) > 0)
            {
                return true;
            }
            else if ((Solver.State & SolverState.UserTerminated) > 0)
            {
                errorMessage.AppendLine(@"Ansys 求解过程被用户强制终止。");
                return false;
            }
            else if ((Solver.State & SolverState.UserTerninationFailed) > 0)
            {
                errorMessage.AppendLine(@"用户强制终止 Ansys 计算时出现错误。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 对 Ansys 计算完成后的 output 文件、 result 文件等进行检查，以判断 Ansys 的求解过程是否成功
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public SolverState CheckOutputFiles(ref StringBuilder errorMessage)
        {
            // 检查 output 文件中的数据
            string outputFile = _workingDir.F_Output;
            SolverState state = SolverState.SelfFinished;
            if (File.Exists(outputFile))
            {
                using (var sr = new StreamReader(outputFile))
                {
                    string line;
                    state = SolverState.FailedWithError;
                    do
                    {
                        line = sr.ReadLine();
                        // 通过 Ansys 的output 文件中的内容，判断 Ansys 计算过程中是否出现问题，并适当地对出错信息进行提取
                        if (line.Contains("ANSYS RUN COMPLETED"))
                        {
                            state = SolverState.Succeeded;
                            break;
                        }
                        //if (line.Equals(ProjectConsts.CalculationSucceededTag))
                        //{
                        //    state = SolverState.Succeeded;
                        //    break;
                        //}
                        //else if (line.Equals(ProjectConsts.CalculationFailedTag))
                        //{
                        //    //
                        //    errorMessage.AppendLine("Ansys 计算过程出错而导致计算结束。");
                        //    // 将具体的出错信息提取出来
                        //    GetErrorMessage(sr, ref errorMessage);
                        //    //
                        //    state = SolverState.FailedWithError;
                        //    break;
                        //}
                    } while (!sr.EndOfStream);
                }
            }
            else
            {
                errorMessage.AppendLine("指定的Ansys output 文件未找到");
                return SolverState.FailedWithError;
            }

            // 检查 result 文件中的数据
            if (state == SolverState.Succeeded)
            {
                //
                string resultFile = _workingDir.F_AnsysResult;

                if (!File.Exists(resultFile))
                {
                    errorMessage.AppendLine("指定的Ansys计算结果文件未找到");
                    return SolverState.FailedWithError;
                }
            }

            return state;
        }

        private void GetErrorMessage(StreamReader sr, ref StringBuilder errorMsg)
        {
            string line = sr.ReadLine();
            errorMsg.AppendLine(line);
            while (line != null && !line.Equals(ProjectConsts.CalculationFailedEndTag))
            {
                errorMsg.AppendLine(line);
                line = sr.ReadLine();
            }
        }

        #endregion

        #region ---   后处理操作： 计算结果数据的提取 与 撰写报告

        /// <summary> 从 Result.sdr 文件中读取计算结果 </summary>
        public void ReadResultFile(string resultFilePath)
        {
            var res = ResultConstructor.ReadResult(resultFilePath);
            Results = res;
        }

        /// <summary> 列出所有的计算结果，并显示在窗口中，以供用户选择性导出 </summary>
        public void ShowResultsList()
        {
            if (Results != null)
            {
                var rl = new ResultLister(Model, Results, _workingDir);
                rl.ShowDialog(null);
            }
        }

        #endregion
    }
}