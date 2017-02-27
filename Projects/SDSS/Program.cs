using System;
using System.Windows.Forms;
using SDSS.Definitions;
using SDSS.StationModel;
using SDSS.UIControls;

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
            Test(args);
            // return;
            StartProgram(args);
        }

        private static void StartProgram(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            var sm = StationModel1.GetUniqueInstance() as StationModel1;
            //ConstructStationModel(sm);

            //
            var mf = new MainForm(sm);
            Application.Run(new EnterSplash());
        }

        #region ---   测试

        private static void Test(string[] args)
        {
        }

        /// <summary> 构造一个车站模型实例 </summary>
        /// <param name="sm"></param>
        public static void ConstructStationModel(StationModel1 sm)
        {
            //
            Material mat1 = new Material("elas1", 1900, 200e9, 0.3, MaterialType.Elastic);
            sm.Definitions.Materials.Add(mat1);
            Material mat2 = new MohrCoulomb("MC", 1900, 200e9, 0.3, 60e6, 30);
            sm.Definitions.Materials.Add(mat2);
            //
            Profile rec = new Rectangular("rec1", 0.5, 1.5);
            sm.Definitions.Profiles.Add(rec);
            Profile profT = new T("t1", 2, 1, 0.1, 0.1);
            sm.Definitions.Profiles.Add(profT);

            //
            sm.GenerateFrame(new float[] { 4, 5, 6, }, new float[] { 3, 6 }, defaultMat: mat1, defaultProfile: rec);

            //
            sm.Beams[0].Profile = profT;
            sm.Columns[0].Material = mat2;
        }

        #endregion
    }
}