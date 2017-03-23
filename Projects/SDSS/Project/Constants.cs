using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSS.Constants
{

    /// <summary> 与整个 SDSS 项目相关的常数 </summary>
    public static class ProjectConsts
    {
        /// <summary> 整个SDSS项目的标题 </summary>
        public const string ProjectTitle = @"地下车站抗震设计";

        /// <summary> 所有模型的默认名称 </summary>
        public const string DefaultModelName = @"SubStructures";



        /// <summary> output 文件中，标识Abaqus计算过程成功的字符串 </summary>
        public const string CalculationSucceededTag = "*** Calculation finished successfully! ***";

        /// <summary> output 文件中，标识Abaqus计算过程出错的字符串 </summary>
        public const string CalculationFailedTag = "*** Calculation terminated with error! ***";
        /// <summary> output 文件中，标识Abaqus计算过程出错的字符串 </summary>
        public const string CalculationFailedEndTag = "*** Error message ends here ***";


    }


    public static class Word
    {
        /// <summary> 集合的第一个无素的下标 </summary>
        public const int CollectionStart = 1;
        /// <summary> Word界面中的换行符（ASCII:13） </summary>
        /// <remarks></remarks>
        public const char CrLf = '\r';
    }


    /// <summary>
    /// UI界面属性列表中，将某些属性归并到某个类别中
    /// </summary>
    public static class Categories
    {
        /// <summary> 在参数设置界面中，为土层或者截面参数所在的分类的名称 </summary>
        public const string Property = "参数";

        /// <summary> 在参数设置界面中，为土层或者截面参数所在的分类的名称 </summary>
        public const string Tag = "标识";

        public const string Material = "材料";
        public const string Profile = "截面参数";
        public const string Rock = "岩石";
        public const string Sand = "砂性土";
        public const string Clay = "黏性土";
    }

    /// <summary>
    /// 各种文件后缀名
    /// </summary>
    public static class FileExtensions
    {
        /// <summary> 车站模型的信息文件 </summary>
        public const string ModelInfo = ".sdss";

        /// <summary> 用来提供给 Ansys 的 APDL 代码进行计算的参数文件 </summary>
        public const string AnsysCalParameters = ".sdmp";

        /// <summary> 用来进行Ansys计算的 APDL 代码文件 </summary>
        public const string AnsysModelSolver = ".sdinp";

        /// <summary> 此文本文件中记录有所有存储有模型参数、计算参数等文件所在的路径 </summary>
        public const string Paths = ".sdp";

        /// <summary> 用来保存材料信息的 xml 文件 </summary>
        public const string Materials = ".sdm";

        /// <summary> 用来保存截面信息的 xml 文件 </summary>
        public const string Profiles = ".sdpf";


        #region ---   Abaqus 计算过程中生成的文件

        /// <summary> Python 或 APDL 脚本运行过程中，用户指定输出的与模型相关的数据，以及计算过程是否正常成功 </summary>
        public const string Output = ".sdo";

        /// <summary> 计算完成后，将最终的计算结果以及报告所须的关键信息都保存在此结果文件中 </summary>
        public const string Results = ".sdr";

        /// <summary> Abaqus 计算过程中， sys.stdout 与 Python中的 print() 函数所对应的输出流文件，此文件中的信息没有任何代码上的特殊意义，只供用户自行查看。 </summary>
        public const string PyMessageExt = ".sdmsg";

        #endregion
    }
}