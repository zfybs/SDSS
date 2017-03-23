using System;
using System.IO;
using System.Windows.Forms;
using SDSS.Constants;

namespace SDSS.Project
{
    /// <summary> 用来进行 Ansys 的计算的文件夹 </summary>
    public class AnsysWorkingDir
    {
        /// <summary> 用来进行 Ansys 的计算的文件夹 </summary>
        public readonly string WorkingDirectory;

        #region -- 用来启动 Ansys 计算所需要的文件

        /// <summary> .txt 文件，此文本文件中记录有所有存储有模型参数、计算参数的文件所在的路径 </summary>
        public string F_FilePaths { get; private set; }

        /// <summary> 用来提供给 Ansys 的 APDL 代码进行计算的参数文件 </summary>
        public string F_ModelParameter { get; private set; }

        #endregion

        #region -- Ansys 计算过程中生成的文件

        /// <summary> Ansys 计算结果的信息输出文件，Python 脚本运行过程中，用户指定输出的与模型相关的数据，以及计算过程是否正常成功 </summary>
        public string F_Output { get; private set; }

        /// <summary> Ansys计算完成后，将最终的计算结果以及报告所须的关键信息都保存在此结果文件中 </summary>
        public string F_AnsysResult { get; private set; }

        /// <summary> Ansys计算完成后，生成的弯矩图所对应的文件 </summary>
        public string F_BendingMoment { get; private set; }

        #endregion

        /// <summary> 构造函数 </summary>
        public AnsysWorkingDir(string ansysWkDir)
        {
            if (!Directory.Exists(ansysWkDir))
            {
                var res = MessageBox.Show(@"指定的计算文件夹不存在，是否创建此文件夹？", @"提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (res != DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(ansysWkDir);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    //throw new DirectoryNotFoundException("Ansys 工作文件夹不存在");
                }
            }
            WorkingDirectory = ansysWkDir;
            ConstructFilePaths(ansysWkDir);
        }

        private void ConstructFilePaths(string workingDir)
        {
            // 用来启动 Ansys 计算所需要的文件
            F_FilePaths = Path.Combine(workingDir, "CalculationFiles" + FileExtensions.Paths);
            F_ModelParameter = Path.Combine(workingDir, "SdssModel" + FileExtensions.AnsysCalParameters);

            // Ansys 计算过程中生成的文件

            F_Output = Path.Combine(workingDir, "Output" + FileExtensions.Output);
            F_AnsysResult = Path.Combine(workingDir, "Result" + FileExtensions.Results);
            F_BendingMoment = Path.Combine(workingDir, "Bending Moment.png");
        }
    }
}