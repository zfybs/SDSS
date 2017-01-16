using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDSS.StationModel;

namespace SDSS.UIControls
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        //获取车站模型参数类
        private SoilStructureGeometry ConstructSSG()
        {
            SoilStructureGeometry ssg = null;
            ssg = new SoilStructureGeometry(60, new float[] { 3, 6, 5, 4, 6, 6 }, 3, new float[] { 3, 3, 3 }, new float[] { 6, 6 });
            return ssg;
        }

        private void modelDrawer1_Paint(object sender, PaintEventArgs e)
        {
            SoilStructureGeometry ssg = ConstructSSG();
            Graphics g = e.Graphics;
            modelDrawer1.DrawSoilStructureModel(g, ssg);
        }
    }
}
