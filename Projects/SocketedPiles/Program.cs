using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using eZstd.Data;
using SocketedShafts.Entities;
using SocketedShafts.Forms;

namespace SocketedShafts
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            MainForm mf = new MainForm();

            SocketedShaftSystem sss = GetSSS("");
            mf.RefreshModel(sss);
            //
            Application.Run(mf);
            //
        }

        private static SocketedShaftSystem GetSSS(string xmlFile)
        {
            SocketedShaftSystem sss = SocketedShaftSystem.GetUniqueInstance();
            return sss;
        }
    }
}
