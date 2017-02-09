using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using eZstd.Enumerable;
using eZstd;
using eZstd.API;
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
            var sm = StationModel1.GetUniqueInstance();
            var mf = new MainForm(sm);

            Application.Run(mf);
            
        }
    }
}
