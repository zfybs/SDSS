using System;
using System.Windows.Forms;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            var sm = StationModel1.GetUniqueInstance() as StationModel1;
            var mf = new MainForm(sm);

            Application.Run(mf);
        }
    }
}