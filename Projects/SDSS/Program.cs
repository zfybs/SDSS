using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDSS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void test()
        {
            string[] ss = new string[5] { "1", "1.5", "", "", "" };
            var tt = ss.Where(r => !string.IsNullOrEmpty(r)).Select(Convert.ToSingle).ToArray();
            var c = Color.FromArgb(1, Color.Pink);

            var t = ss.Select(Convert.ToSingle).ToArray();
        }
    }
}
