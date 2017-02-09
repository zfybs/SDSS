using System.Drawing;
using System.Windows.Forms;
using SDSS.StationModel;

namespace SDSS.UIControls
{
    public partial class MainForm : Form
    {
        private StationModel.StationModel _stationModel;

        public MainForm(StationModel.StationModel sm)
        {
            InitializeComponent();
            //
            _stationModel = sm;
            // 对不同的车站模型，在界面中进行绘图显示
            RefreshModel(_stationModel);
        }

        /// <summary> 对不同的车站模型，在界面中进行绘图显示 </summary>
        /// <param name="sm"></param>
        private void RefreshDataGridView(StationModel.StationModel sm)
        {

        }

        #region ---   前处理界面效果显示

        /// <summary> 对不同的车站模型，在界面中进行绘图显示 </summary>
        /// <param name="sm"></param>
        private void RefreshModel(StationModel.StationModel sm)
        {

        }

        private void modelDrawer1_Paint(object sender, PaintEventArgs e)
        {
            SoilStructureGeometry ssg = ConstructSSG();
            Graphics g = e.Graphics;
            modelDrawer1.DrawSoilStructureModel(g, ssg);
        }

        //获取车站模型参数类
        private SoilStructureGeometry ConstructSSG()
        {
            var geo = _stationModel.GetStationGeometry();

            return geo as SoilStructureGeometry;
        }



        #endregion

    }
}