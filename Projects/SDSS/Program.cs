using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using SDSS.Definitions;
using SDSS.Models;
using SDSS.UIControls;
using SDSS.Project;
using SDSS.Utility;
using System.Xml;
using System.Xml.Schema;
using SDSS.Structures;

namespace SDSS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            BeforeProgramStarted(args);
            //
            if (Test(args))
            {
                StartProgram(args);
            }
            //
            AfterProgramFinished(args);
        }

        private static void StartProgram(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EnterSplash());
        }

        #region ---   测试

        private static bool Test(string[] args)
        {
            return true;
        }

        /// <summary> 构造一个车站模型实例 </summary>
        /// <param name="sm"></param>
        public static void ConstructStationModel(Model1 sm)
        {
            //
            Material mat1 = new Material("elas1", 1900, 200e9, 0.3, MaterialType.Elastic);
            sm.Definitions.Materials.Add(mat1);
            Material mat2 = new MohrCoulomb("MC", 1900, 200e9, 0.3, 60e6, 30);
            sm.Definitions.Materials.Add(mat2);
            //
            Profile rec = new Rectangular("rec1", 0.5, 1.5);
            sm.Definitions.Profiles.Add(rec);
            Profile profT = new TT("t1", 2, 1, 0.1, 0.1);
            sm.Definitions.Profiles.Add(profT);

            //
            sm.Frame = Frame.Create(new float[] { 4, 5, 6, }, new float[] { 3, 6 }, defaultMat: mat1, defaultProfile: rec);

            //
            sm.Frame.Beams[0].Profile = profT;
            sm.Frame.Columns[0].Material = mat2;
        }

        #endregion

        #region ---   程序运行前后的操作

        /// <summary> 在程序开始前的操作 </summary>
        /// <param name="args"></param>
        private static void BeforeProgramStarted(string[] args)
        {
            Options.Load();
        }

        /// <summary> 程序结束后的操作 </summary>
        private static void AfterProgramFinished(string[] args)
        {
            Options.Save();
        }
        #endregion
    }
}