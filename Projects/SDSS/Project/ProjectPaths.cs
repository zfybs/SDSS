using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using eZstd.Miscellaneous;
using SDSS.Constants;
using SDSS.Definitions;
using SDSS.Models;
using SDSS.Solver;
using SDSS.Project;

namespace SDSS.Project
{
    /// <summary> 整个项目中与路径相关的信息 </summary>
    internal static class ProjectPaths
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

        /// <summary> 利用 Ansys 的 APDL 代码 进行计算的源代码所在文件夹 </summary>
        public static readonly string D_AnsysSolver = _d_Solution.GetDirectories("AnsysSolver").First().FullName;

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

        #endregion

        #region ---   文件路径

        /// <summary> 用于启动 Abaqus 的.bat文件 </summary>
        public static string F_InitialBat
        {
            get { return Path.Combine(D_MiddleFiles, "InitialSolver.bat"); }
        }

        /// <summary> 执行 Abaqus 计算的初始 .py 源代码文件，即在Python脚本中，启动整个程序的入口模块 </summary>
        public static readonly string F_PySolver = Path.Combine(D_PythonSource, "EnvironmentBuild.py");

        /// <summary> 用户选项数据所在的文件 </summary>
        public static readonly string F_Options = Path.Combine(D_MiddleFiles, "Options.xml");

        public static string GetAnsysModelSolverFile(ModelBase mb)
        {
            return Path.Combine(D_AnsysSolver, mb.GetType().Name + FileExtensions.AnsysModelSolver);
        }

        #endregion

        /// <summary>
        /// 将存储有模型参数、计算参数的文件所在的路径写入到一个单独的文本中，以供 Python 程序读取。
        /// 此文件的路径是固定的。
        /// </summary>
        /// <param name="abqWkDir"> Abaqus 计算文件夹路径 </param>
        /// <param name="filePath"> 要将这些信息写入哪一个文件中 </param>
        public static void WriteAbqCalcFilePaths(AbqWorkingDir abqWkDir, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    // 在此文件中写入各种计算文件的路径，路径含义与路径字符之间通过“ * ”进行分隔
                    string sep = @" * ";

                    // 1. Python 脚本源代码所在文件夹
                    sw.WriteLine("PythonSourceDir" + sep + D_PythonSource);

                    // 2. Abaqus 的工作文件夹
                    sw.WriteLine("AbaqusWorkingDir" + sep + abqWkDir.WorkingDirectory);

                    // 3. 记录模型信息的 xml 文件
                    sw.WriteLine("ModelFile" + sep + abqWkDir.F_CalculationModel);

                    // 4. SDSS 解决方案的中间文件夹
                    sw.WriteLine("MiddleFileDir" + sep + D_MiddleFiles);

                    // 5. Abaqus 计算过程的信息输出文件，对应 python 中的 sys.stdout，这些输出信息主要是程序运行中的提示，与具体的模型信息无关
                    sw.WriteLine("PyMessageFile" + sep + abqWkDir.F_PyMessage);

                    // 6. Python脚本运行过程中，用户指定输出的与模型相关的数据
                    sw.WriteLine("PyOutputFile" + sep + abqWkDir.F_PyOutput);

                    // 7. Abaqus计算完成后，将最终的计算结果以及报告所须的关键信息都保存在此结果文件中
                    sw.WriteLine("CalculationResultFile" + sep + abqWkDir.F_AbqResult);

                    //
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// 将存储有模型参数、计算参数的文件所在的路径写入到一个单独的文本中，以供 Python 程序读取。
        /// 此文件的路径是固定的。
        /// </summary>
        /// <param name="abqWkDir"> Abaqus 计算文件夹路径 </param>
        /// <param name="filePath"> 要将这些信息写入哪一个文件中 </param>
        public static void WriteAnsysCalcFilePaths(AnsysWorkingDir abqWkDir, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    // 在此文件中写入各种计算文件的路径，路径含义与路径字符之间通过“ * ”进行分隔
                    string sep = @" * ";

                    // 1. Python 脚本源代码所在文件夹
                    sw.WriteLine("PythonSourceDir" + sep + D_PythonSource);

                    // 2. Abaqus 的工作文件夹
                    sw.WriteLine("AbaqusWorkingDir" + sep + abqWkDir.WorkingDirectory);

                    // 3. 记录模型信息的 xml 文件
                    sw.WriteLine("ModelFile" + sep + abqWkDir.F_ModelParameter);

                    // 4. SDSS 解决方案的中间文件夹
                    sw.WriteLine("MiddleFileDir" + sep + D_MiddleFiles);

                    // 6. Python脚本运行过程中，用户指定输出的与模型相关的数据
                    sw.WriteLine("PyOutputFile" + sep + abqWkDir.F_Output);

                    // 7. Abaqus计算完成后，将最终的计算结果以及报告所须的关键信息都保存在此结果文件中
                    sw.WriteLine("CalculationResultFile" + sep + abqWkDir.F_AnsysResult);

                    //
                    sw.Close();
                }
            }
        }

    }
}
