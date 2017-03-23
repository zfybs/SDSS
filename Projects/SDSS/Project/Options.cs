using SDSS.Solver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using SDSS.UIControls;
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
                if (!Directory.Exists(_DefaultAbqWorkingDir))
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

        /// <summary>  Ansys 计算程序所对应的文件路径，比如 C:\Softwares\Ansys\v150\ANSYS\bin\winx64\ansys150.exe </summary>
        [XmlAttribute("AnsysExePath")]
        public static string _AnsysExePath;
        /// <summary>  Ansys 计算程序所对应的文件路径 </summary>
        /// <remarks> 比如 C:\Softwares\Ansys\v150\ANSYS\bin\winx64\ansys150.exe </remarks>
        [XmlAttribute()]
        public static string AnsysExePath
        {
            get
            {
                if (!File.Exists(_AnsysExePath))
                {
                    _AnsysExePath = GetAnsysExePath();
                }
                return _AnsysExePath;
            }
            set
            {
                _AnsysExePath = value;
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
        public static string WordTemplate;

        #endregion

        #region ---   选项的导入与导出

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

        #endregion

        #region ---   获得 Ansys 计算程序文件所在的路径，比如  C:\Softwares\Ansys\v150\ANSYS\bin\winx64\ansys150.exe

        /// <summary>
        /// Ansys 计算程序所对应的文件路径，比如 C:\Softwares\Ansys\v150\ANSYS\bin\winx64\ansys150.exe
        /// </summary>
        /// <returns></returns>
        public static string GetAnsysExePath()
        {
            string ansysDir = null;   // 其结果大致为： C:\Softwares\Civil Engineering\Ansys\v150\ANSYS
            string ansysSysDir = null;   // 其结果大致为： winx64
            string versionNum = null;
            string pat = @"^ANSYS(\d+)_DIR$"; // 比如：ANSYS150_DIR
            var reg = new Regex(pat, RegexOptions.IgnoreCase);
            IDictionary machineVars = Environment.GetEnvironmentVariables(target: EnvironmentVariableTarget.Machine);
            foreach (DictionaryEntry v in machineVars)
            {
                string vName = v.Key.ToString();

                // 判断 1：
                if (vName.Equals("ANSYS_SYSDIR", StringComparison.OrdinalIgnoreCase))
                {
                    ansysSysDir = v.Value.ToString();
                    continue;
                }

                // 判断 2：
                var mat = reg.Match(vName);
                if (mat.Success)
                {
                    versionNum = mat.Groups[1].Value;
                    ansysDir = v.Value.ToString();
                }
            }
            if (ansysDir != null && ansysSysDir != null)
            {
                // 其结果大致为： C:\Softwares\Civil Engineering\Ansys\v150\ansys\bin\winx64\ANSYS150.exe
                return Path.Combine(ansysDir, "bin", ansysSysDir, $"ANSYS{versionNum}.exe");
            }
            else
            {
                var dg = new PathGettor(true, "Ansys 计算程序路径", nonEnglistAllowed: false, 
                    tip: @"例如; C:\Ansys\v150\ansys\bin\winx64\ANSYS150.exe");
                var res = dg.ShowDialog();
                if (res == DialogResult.OK)
                {
                    return dg.ChoosedPath;
                }
                else
                {
                    throw new InvalidOperationException("无法找到用来进行计算的 Ansys 程序");
                }
            }
        }

        #endregion

    }
}
