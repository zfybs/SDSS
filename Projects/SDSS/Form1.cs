using SDSS.Structures;
using SDSS.UIControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SDSS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void modelDrawer1_Paint(object sender, PaintEventArgs e)
        {
            SoilTunnelGeometry stg = new SoilTunnelGeometry(20, new float[] { 3, 4, 5 }, 2, 3, 6);
            Graphics g = e.Graphics;
            modelDrawer1.DrawSoilTunnelModel(stg, g);
        }

    }
}
