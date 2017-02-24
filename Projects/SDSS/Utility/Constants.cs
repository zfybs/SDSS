using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSS.Constants
{
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
        public const string StationModel = ".sdss";

        /// <summary> Abaqus 计算过程中， sys.stdout 所对应的输出流文件 </summary>
        public const string PyMessageExt = ".sdmsg";

        /// <summary> 此文本文件中记录有所有存储有模型参数、计算参数等文件所在的路径 </summary>
        public const string Paths = ".sdp";
        
        /// <summary> Python脚本运行过程中，用户指定输出的与模型相关的数据 </summary>
        public const string Output = ".sdo";

        /// <summary> Abaqus计算完成后，将最终的计算结果以及报告所须的关键信息都保存在此结果文件中 </summary>
        public const string AbqResult = ".sdr";

        /// <summary> 用来保存材料信息的 xml 文件 </summary>
        public const string Materials = ".sdm";

        /// <summary> 用来保存截面信息的 xml 文件 </summary>
        public const string Profiles = ".sdpf";
    }
}