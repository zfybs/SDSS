using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using eZstd.Miscellaneous;
using SDSS.Definitions;
using SDSS.Solver;
using SDSS.Project;

namespace SDSS.Project
{
    /// <summary> 整个项目中与路径相关的信息 </summary>
    public static class ProjectPaths
    {
        /// <summary> 前处理程序的文件绝对路径 </summary>
        private static readonly FileInfo _f_PreProc = new FileInfo(Assembly.GetExecutingAssembly().FullName);
        /// <summary> 前处理程序的文件绝对路径 </summary>
        public static readonly string F_PreProc = _f_PreProc.FullName;

        #region ---   文件夹路径

        /// <summary> 前处理程序的文件所在文件夹的绝对路径 </summary>
        public static readonly string D_PreProc = _f_PreProc.Directory.FullName;

        /// <summary> 整个解决方案所在文件夹 </summary>
        private static readonly DirectoryInfo _d_Solution = _f_PreProc.Directory.Parent;
        /// <summary> 整个解决方案所在文件夹 </summary>
        public static readonly string D_Solution = _d_Solution.FullName;


        /// <summary> 前处理程序的文件所在文件夹的绝对路径 </summary>
        public static readonly string D_MiddleFiles = _d_Solution.GetDirectories("MidFiles").First().FullName;

        /// <summary> 利用 Abaqus 进行计算的 Python 源代码所在文件夹 </summary>
        public static readonly string D_PythonSource = _d_Solution.GetDirectories("AbaqusSolver").First().FullName;

        /// <summary> 输出的报告所对应的 word 模块文件所在的文件夹 </summary>
        public static readonly string D_WordTemplate = D_MiddleFiles;

        #region ---   AbaqusWorkingDir

        /// <summary> 设置 Abaqus 计算的默认文件夹（如果用户未显式指定，则使用此文件夹） </summary>
        /// <remarks>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ///  @"D:\Workspace\abaqus\ModelStationTest";
        /// </remarks>
        public static string D_AbaqusDefaultWorkingDir
        {
            get
            {
                return Options.DefaultAbqWorkingDir;
            }
        }

        private static string _D_AbaqusWorkingDir;
        /// <summary> Abaqus 的工作文件夹 </summary>
        public static string D_AbaqusWorkingDir
        {
            get
            {
                if (string.IsNullOrEmpty(_D_AbaqusWorkingDir))
                {
                    _D_AbaqusWorkingDir = Options.DefaultAbqWorkingDir;
                }
                return _D_AbaqusWorkingDir;
            }
            private set
            {
                _D_AbaqusWorkingDir = value;
            }
        }

        #endregion

        #endregion

        #region ---   文件路径

        #region -- 记录车站模型数据的 xml 文件

        /// <summary> 默认的车站模型的计算文件的名称 </summary>
        public const string FN_DefaultModel = "StationDesginModel" + Constants.FileExtensions.StationModel;

        /// <summary> 默认的车站模型的计算文件的名称 </summary>
        public static string F_CalculationModel
        {
            get { return Path.Combine(D_MiddleFiles, FN_DefaultModel); }
        }

        #endregion

        /// <summary> .txt 文件，此文本文件中记录有所有存储有模型参数、计算参数的文件所在的路径 </summary>
        private const string FN_FilePaths = "CalculationFiles" + Constants.FileExtensions.Paths;

        /// <summary> .txt 文件，此文本文件中记录有所有存储有模型参数、计算参数的文件所在的路径 </summary>
        public static string F_FilePaths
        {
            get { return Path.Combine(D_MiddleFiles, FN_FilePaths); }
        }


        #region -- Abaqus 计算过程中生成的文件

        private const string FN_PyMessage = "CalculationMessage" + Constants.FileExtensions.PyMessageExt;
        /// <summary> Abaqus 计算过程的信息输出文件，对应 python 中的 sys.stdout 与 print() 函数所对应的输出流文件 ，
        /// 这些输出信息主要是程序运行中的提示，与具体的模型信息无关，此文件中的信息没有任何代码上的特殊意义，只供用户自行查看。 </summary>
        public static string F_PyMessage
        {
            get { return Path.Combine(D_AbaqusWorkingDir, FN_PyMessage); }
        }

        private const string FN_PyOutput = "Output" + Constants.FileExtensions.Output;
        /// <summary> Abaqus 计算结果的信息输出文件，Python 脚本运行过程中，用户指定输出的与模型相关的数据，以及计算过程是否正常成功 </summary>
        public static string F_PyOutput
        {
            get { return Path.Combine(D_AbaqusWorkingDir, FN_PyOutput); }
        }

        private const string FN_AbqResult = "Result" + Constants.FileExtensions.AbqResult;
        /// <summary> Abaqus计算完成后，将最终的计算结果以及报告所须的关键信息都保存在此结果文件中 </summary>
        public static string F_AbqResult
        {
            get { return Path.Combine(D_AbaqusWorkingDir, FN_AbqResult); }
        }

        /// <summary> Abaqus计算完成后，生成的弯矩图所对应的文件 </summary>
        public static string F_BendingMoment
        {
            get { return Path.Combine(D_AbaqusWorkingDir, "Bending Moment.png"); }
        }

        #endregion

        /// <summary> 用于启动 Abaqus 的.bat文件 </summary>
        public static string F_InitialBat
        {
            get { return Path.Combine(D_MiddleFiles, "InitialSolver.bat"); }
        }

        /// <summary> 执行 Abaqus 计算的初始 .py 源代码文件，即在Python脚本中，启动整个程序的入口模块 </summary>
        public readonly static string F_PySolver = Path.Combine(D_PythonSource, "EnvironmentBuild.py");

        /// <summary> 用户选项数据所在的文件 </summary>
        public readonly static string F_Options = Path.Combine(D_MiddleFiles, "Options.xml");

        #endregion

        #region ---   路径设置或获取的方法



        /// <summary>
        /// 根据 Abaqus 工作空间的不同来设置对应的文件路径
        /// </summary>
        /// <param name="workingDir"></param>
        public static void SetAbaqusWorkingDir(string workingDir)
        {
            if (!Directory.Exists(workingDir))
            {
                var res = MessageBox.Show(@"指定的计算文件夹不存在，是否创建此文件夹？", @"提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                {
                    Directory.CreateDirectory(workingDir);
                }
                else
                {
                    return;
                }
            }
            D_AbaqusWorkingDir = workingDir;
            //F_FilePaths = Path.Combine(workingDir, FN_CalcutionFile);
        }

        #endregion

        #region ---   文件数据写入

        /// <summary>
        /// 将存储有模型参数、计算参数的文件所在的路径写入到一个单独的文本中，以供 Python 程序读取。
        /// 此文件的路径是固定的。
        /// </summary>
        public static void WriteCalcFilePaths()
        {
            StreamWriter sw = new StreamWriter(F_FilePaths, append: false);

            // 在此文件中写入各种计算文件的路径，路径含义与路径字符之间通过“ * ”进行分隔
            string sep = @" * ";

            // 1. Python 脚本源代码所在文件夹
            sw.WriteLine("PythonSourceDir" + sep + D_PythonSource);

            // 2. Abaqus 的工作文件夹
            sw.WriteLine("AbaqusWorkingDir" + sep + D_AbaqusWorkingDir);

            // 3. 记录模型信息的 xml 文件
            sw.WriteLine("ModelFile" + sep + F_CalculationModel);

            // 4. SDSS 解决方案的中间文件夹
            sw.WriteLine("MiddleFileDir" + sep + D_MiddleFiles);

            // 5. Abaqus 计算过程的信息输出文件，对应 python 中的 sys.stdout，这些输出信息主要是程序运行中的提示，与具体的模型信息无关
            sw.WriteLine("PyMessageFile" + sep + F_PyMessage);

            // 6. Python脚本运行过程中，用户指定输出的与模型相关的数据
            sw.WriteLine("PyOutputFile" + sep + F_PyOutput);

            // 7. Abaqus计算完成后，将最终的计算结果以及报告所须的关键信息都保存在此结果文件中
            sw.WriteLine("CalculationResultFile" + sep + F_AbqResult);

            //
            sw.Close();
        }

        #endregion
    }
}
