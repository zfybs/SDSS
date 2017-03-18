using System.IO;
using System.Windows.Forms;
using SDSS.Constants;

namespace SDSS.Project
{
    /// <summary> 用来进行 Abaqus 的计算的文件夹 </summary>
    public class AbqWorkingDir
    {
        /// <summary> 用来进行 Abaqus 的计算的文件夹 </summary>
        public readonly string WorkingDirectory;

        #region -- 用来启动 Abaqus 计算所需要的文件

        private const string FN_FilePaths = "CalculationFiles" + FileExtensions.Paths;
        /// <summary> .txt 文件，此文本文件中记录有所有存储有模型参数、计算参数的文件所在的路径 </summary>
        public string F_FilePaths { get; private set; }


        private const string FN_DefaultModel = "StationDesginModel" + FileExtensions.StationModel;
        public string F_CalculationModel { get; private set; }

        #endregion

        #region -- Abaqus 计算过程中生成的文件

        private const string FN_PyMessage = "CalculationMessage" + FileExtensions.PyMessageExt;
        /// <summary> Abaqus 计算过程的信息输出文件，对应 python 中的 sys.stdout 与 print() 函数所对应的输出流文件 ，
        /// 这些输出信息主要是程序运行中的提示，与具体的模型信息无关，此文件中的信息没有任何代码上的特殊意义，只供用户自行查看。 </summary>
        public string F_PyMessage { get; private set; }

        private const string FN_PyOutput = "Output" + FileExtensions.Output;
        /// <summary> Abaqus 计算结果的信息输出文件，Python 脚本运行过程中，用户指定输出的与模型相关的数据，以及计算过程是否正常成功 </summary>
        public string F_PyOutput { get; private set; }


        private const string FN_AbqResult = "Result" + FileExtensions.AbqResult;
        /// <summary> Abaqus计算完成后，将最终的计算结果以及报告所须的关键信息都保存在此结果文件中 </summary>
        public string F_AbqResult { get; private set; }


        private const string FN_BendingMoment = "Bending Moment.png";
        /// <summary> Abaqus计算完成后，生成的弯矩图所对应的文件 </summary>
        public string F_BendingMoment { get; private set; }

        #endregion

        /// <summary> 构造函数 </summary>
        public AbqWorkingDir(string abqWkDir)
        {
            if (!Directory.Exists(abqWkDir))
            {
                var res = MessageBox.Show(@"指定的计算文件夹不存在，是否创建此文件夹？", @"提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                {
                    Directory.CreateDirectory(abqWkDir);
                }
                else
                {
                    throw new DirectoryNotFoundException("Abaqus 工作文件夹不存在");
                }
            }
            WorkingDirectory = abqWkDir;
            ConstructFilePaths(abqWkDir);
        }

        private void ConstructFilePaths(string abqWkDir)
        {
            // 用来启动 Abaqus 计算所需要的文件
            F_FilePaths = Path.Combine(WorkingDirectory, FN_FilePaths);
            F_CalculationModel = Path.Combine(WorkingDirectory, FN_DefaultModel);

            // Abaqus 计算过程中生成的文件
            F_PyMessage = Path.Combine(WorkingDirectory, FN_PyMessage);
            F_PyOutput = Path.Combine(WorkingDirectory, FN_PyOutput);
            F_AbqResult = Path.Combine(WorkingDirectory, FN_AbqResult);
            F_BendingMoment = Path.Combine(WorkingDirectory, FN_BendingMoment);
        }
    }
}