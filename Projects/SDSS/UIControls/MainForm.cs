using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using eZstd.Miscellaneous;
using SDSS.Definitions;
using SDSS.Solver;
using SDSS.StationModel;
using SDSS.Utility;
using Component = SDSS.Entities.Component;

namespace SDSS.UIControls
{
    internal partial class MainForm : Form
    {
        private StationModel1 _stationModel;

        public MainForm(StationModel1 sm)
        {
            InitializeComponent();
            //
            _stationModel = sm;


            //
            sm.Definitions.MaterialsCollectionChanged += DefinitionsOnMaterialsChanged;
        }

        private void DefinitionsOnMaterialsChanged(object sender, EventArgs eventArgs)
        {
            // 刷新 ComboBox 界面
            var mats = _stationModel.Definitions.Materials;

            RefreshComboBox(comboBoxCompMaterials, mats);
            RefreshComboBox(comboBoxSoilMaterials, mats.Clone());
        }

        #region ---   PictureBox 绘图操作

        private void modelDrawer1_Resize(object sender, EventArgs e)
        {
            SoilStructureGeometry ssg = _stationModel.GetStationGeometry() as SoilStructureGeometry;
            if (ssg != null)
            {
                modelDrawer1.DrawSoilStructureModel(ssg);
            }
        }

        private void modelDrawer1_Paint(object sender, PaintEventArgs e)
        {
            SoilStructureGeometry ssg = _stationModel.GetStationGeometry() as SoilStructureGeometry;
            if (ssg != null)
            {
                modelDrawer1.DrawSoilStructureModel(ssg, e.Graphics);
            }
        }

        #endregion

        #region ---   Definition的添加与界面处理

        private void button_Profiles_Click(object sender, EventArgs e)
        {
            DefinitionManager<Profile> dm = new DefinitionManager<Profile>(_stationModel.Definitions.Profiles);
            dm.ShowDialog();
            // 刷新 ComboBox 界面
            //RefreshComboBox(comboBoxProfiles, _stationModel.Definitions.Profiles);

            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            RefreshComboBoxColumn(_comboColProf, _stationModel.Definitions.Profiles);
            eZDataGridViewFrame.Refresh();
        }

        private void button_Materials_Click(object sender, EventArgs e)
        {
            DefinitionManager<Material> dm = new DefinitionManager<Material>(_stationModel.Definitions.Materials);
            dm.ShowDialog();

            //// 刷新 ComboBox 界面
            //RefreshComboBox(comboBoxCompMaterials, _stationModel.Definitions.Materials);
            //RefreshComboBox(comboBoxSoilMaterials, _stationModel.Definitions.Materials);


            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            RefreshComboBoxColumn(_comboColMat, _stationModel.Definitions.Materials);
            eZDataGridViewFrame.Refresh();
        }

        /// <summary> 将材料或者截面定义刷新到组合列表框中 </summary>
        private void RefreshComboBox(ComboBox comboBox, IEnumerable<Definition> definitions)
        {
            List<int> i;
            // 刷新数据列中每一个单元格的选择
            comboBox.DataSource = null;
            comboBox.DataSource = definitions;
            comboBox.DisplayMember = "Name";
        }

        #endregion

        #region ---   构造矩形框架模型

        private void button_GenerateFrame_Click(object sender, EventArgs e)
        {
            // create a new form
            var layerCount = (ushort)textBoxNum_layers.ValueNumber;
            var spanCount = (ushort)textBoxNum_spans.ValueNumber;
            if (layerCount > 0 && spanCount > 0)
            {
                FrameConstructor fc = FrameConstructor.GetUniqueInstance(layerCount, spanCount);
                //
                var res = fc.ShowDialog();
                if (res == DialogResult.OK)
                {
                    Material mat = _stationModel.Definitions.Materials.FirstOrDefault();
                    Profile prof = _stationModel.Definitions.Profiles.FirstOrDefault();

                    _stationModel.GenerateFrame(fc.LayerHeights, fc.SpanWidths, defaultMat: mat, defaultProfile: prof);

                    // 
                    if (!_eZDataGridViewFrameConstructed)
                    {
                        ConstructeZDataGridViewFrame(eZDataGridViewFrame);
                    }
                    RefresheZDataGridViewFrame(_stationModel);
                    RefreshComboBoxColumn(_comboColMat, _stationModel.Definitions.Materials);
                    RefreshComboBoxColumn(_comboColProf, _stationModel.Definitions.Profiles);

                    // 将新模型进行重新绘制
                    SoilStructureGeometry ssg = _stationModel.GetStationGeometry() as SoilStructureGeometry;
                    modelDrawer1.DrawSoilStructureModel(ssg);
                }
            }
        }

        /// <summary> 为选择的构件指定材料 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_assignCompMat_Click(object sender, EventArgs e)
        {
            Material mat = comboBoxCompMaterials.SelectedItem as Material;
            if (mat != null)
            {
                foreach (DataGridViewRow r in eZDataGridViewFrame.SelectedRows)
                {
                    Component comp = r.DataBoundItem as Component;
                    comp.Material = mat;
                }
            }
            eZDataGridViewFrame.Refresh();
        }


        private void button_assignSoilMat_Click(object sender, EventArgs e)
        {
            Material mat = comboBoxSoilMaterials.SelectedItem as Material;
            if (mat != null)
            {
                foreach (DataGridViewRow r in eZDataGridViewSoilLayers.SelectedRows)
                {
                    SoilLayer soil = r.DataBoundItem as SoilLayer;
                    soil.Material = mat;
                }
            }
            eZDataGridViewSoilLayers.Refresh();
        }

        /// <summary> 为选择的构件指定截面 </summary>
        private void button_assignProfile_Click(object sender, EventArgs e)
        {
            Profile prof = comboBoxProfiles.SelectedItem as Profile;
            if (prof != null)
            {
                foreach (DataGridViewRow r in eZDataGridViewFrame.SelectedRows)
                {
                    Component comp = r.DataBoundItem as Component;
                    comp.Profile = prof;
                }
            }
            eZDataGridViewFrame.Refresh();
        }

        #endregion

        #region --- 边界参数的设置
        private void button_Boundary_Click(object sender, EventArgs e)
        {
            var sp = _stationModel.SystemProperty;
            BoundaryParam f = new BoundaryParam(sp.kx, sp.ky, sp.overLayingSoil, sp.soilWidth);

            f.StartPosition = FormStartPosition.CenterParent;
            // f.textBox_Kx.Text = _stationModel.SystemProperty.kx.ToString();
            // f.kx = _stationModel.SystemProperty.kx;
            f.ShowDialog();

            //
            sp.kx = f.Kx;
            sp.ky = f.Ky;
            sp.overLayingSoil = f.OverLayingSoil;
            sp.soilWidth = f.SoilWidth;

        }
        #endregion

        #region ---  将整个模型导出到 xml 文档

        /// <summary> 将整个模型导出到 xml 文档 </summary>
        private void TSM_Export_Click(object sender, EventArgs e)
        {
            string errorMessage;
            if (ValidateModel(_stationModel, out errorMessage))
            {
                string filePath = Utils.ChooseSaveStationModel("导出车站模型");
                if (filePath.Length > 0)
                {
                    bool succ = ExportToXml(_stationModel, filePath, out errorMessage);
                    if (succ)
                    {
                        ProjectPaths.F_ModelFile = filePath;
                    }
                }
            }
            else
            {
                MessageBox.Show("当前模型不符合导出规范 \n\r" + errorMessage, "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateModel(StationModel1 sm, out string errorMessage)
        {
            errorMessage = "可以成功导出。";
            return true;
            //if (string.IsNullOrEmpty(sss.SocketedShaft.Name))
            //{
            //    errorMessage = "桩未命名";
            //    return false;
            //}
            //if (!ValidateConnectivity(sss.SoilLayers, out errorMessage))
            //{
            //    errorMessage = "土层连续性不满足： " + errorMessage;
            //    return false;
            //}
            //if (!ValidateConnectivity(sss.SocketedShaft.Sections, out errorMessage))
            //{
            //    errorMessage = "桩段连续性不满足： " + errorMessage;
            //    return false;
            //}

            errorMessage = "可以成功导出。";
            return true;
        }

        /// <param name="stationModel">车站模型</param>
        /// <param name="filePath">此路径必须为一个有效的路径</param>
        private bool ExportToXml(StationModel.StationModel stationModel, string filePath, out string errorMessage)
        {
            return ProjectPaths.SerializeNewModelFile(filePath, stationModel, out errorMessage);
        }

        #endregion

        #region ---  从 xml 文档中导入整个模型

        private void TSM_Import_Click(object sender, EventArgs e)
        {
            string filePath = Utils.ChooseOpenStationModel("导入车站模型");
            if (filePath.Length > 0)
            {
                StationModel1 sm = ImportFromXml(filePath);
                if (sm != null)
                {
                    _stationModel = sm;
                }
                else
                {
                    MessageBox.Show("指定的文件不能正常解析为车站模型。\r\n请检查文件中的内容，或者重新指定模型文件。", "提示", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <param name="filePath">此路径必须为一个有效的路径</param>
        private static StationModel1 ImportFromXml(string filePath)
        {
            FileStream reader = null;
            object obj = null;
            try
            {
                Type tp = typeof(StationModel1);

                //
                reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                XmlSerializer sReader = new XmlSerializer(tp);
                obj = sReader.Deserialize(reader);
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, "");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return obj as StationModel1;
        }

        #endregion

        #region ---   求解计算

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            // 对模型进行检查，看是否可以进行计算
            if (_stationModel.Validate(out errorMessage))
            {
                AbaqusSolver solver = new AbaqusSolver(
                    workingDir: ProjectPaths.D_AbaqusWorkingDir ?? ProjectPaths.D_AbaqusDefaultWorkingDir,
                    modelType: ModelType.Model1,
                    solverGui: SolverGUI.CAE
                   );

                // 检查计算环境，文件配置
                if (solver.CheckEnvironment(_stationModel, out errorMessage))
                {
                    // 求解计算
                    _bcParameters = new BackgroundCalculationParameters()
                    {
                        Solver = solver,
                        ErrorMessage = errorMessage,

                    };
                    //
                    progressBar1.Visible = true;
                    progressBar1.Style = ProgressBarStyle.Marquee;

                    if (_backgroundWorker.IsBusy != true)
                    {
                        // Start the asynchronous operation.
                        _backgroundWorker.RunWorkerAsync(argument: _bcParameters);
                    }

                }
            }
        }

        private BackgroundCalculationParameters _bcParameters;

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundCalculationParameters para = e.Argument as BackgroundCalculationParameters;

                SolverState state = para.Solver.Execute(waitingSeconds: 60 * 10,
                    errorMessage: out para.ErrorMessage);

                e.Result = state;
            }
            catch (Exception)
            {
                e.Cancel = true;
                // ignored
            }

        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            if (e.Cancelled)  // 判断是否是手动退出线程
            {

            }
            SolverState state = (SolverState)e.Result;
            AbaqusSolver solver = _bcParameters.Solver;

            // 后处理
            switch (state)
            {
                case SolverState.Succeeded:
                    MessageBox.Show(@"计算完成", @"Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case SolverState.UserTerminated:
                    MessageBox.Show(@"用户强行终止计算", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
            }
        }

        private class BackgroundCalculationParameters
        {
            public AbaqusSolver Solver;
            public string ErrorMessage;
        }
        #endregion

        #region ---   后处理操作


        #endregion

    }
}