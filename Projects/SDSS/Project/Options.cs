using SDSS.Solver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Utility;

namespace SDSS.Project
{
    [Serializable()]
    internal static class Options
    {
        #region ---   Attributes

     
        [XmlAttribute()]
        public static uint WaitingSeconds = 60;

        /// <summary> 用来进行 xml 文件的序列化，此字段不要手动进行读写 </summary>
        [XmlAttribute("DefaultAbqWorkingDir")]
        public static string _DefaultAbqWorkingDir;
        /// <summary> 设置 Abaqus 计算的默认文件夹（如果用户未显式指定，则使用此文件夹） </summary>
        /// <remarks>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ///  @"D:\Workspace\abaqus\ModelStationTest";
        /// </remarks>
        [XmlAttribute()]
        public static string DefaultAbqWorkingDir
        {
            get
            {
                if (string.IsNullOrEmpty(_DefaultAbqWorkingDir))
                {
                    _DefaultAbqWorkingDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Abaqus");
                }
                return _DefaultAbqWorkingDir;
            }
            set
            {
                _DefaultAbqWorkingDir = value;
            }
        }
        

        /// <summary> 是否直接输出报告 </summary>
        [XmlAttribute()]
        public static bool DirectlyReport;

        /// <summary> Abaqus 求解器的显示方式 </summary>
        [XmlAttribute()]
        public static SolverGUI SolverGUI;

        /// <summary>计算报告使用的模板的绝对路径</summary>
        [XmlAttribute()]
        public static string  WordTemplate;

        #endregion

        public static void Save()
        {
            var xmlPath = ProjectPaths.F_Options;
            bool succ1 = StaticSerializer.Save(typeof(Options), xmlPath);
        }
        public static void Load()
        {
            var xmlPath = ProjectPaths.F_Options;
            bool succ2 = StaticSerializer.Load(typeof(Options), xmlPath);
        }

    }
}
