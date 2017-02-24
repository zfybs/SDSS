using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using eZstd.Enumerable;
using eZstd.Miscellaneous;
using SDSS.Definitions;
using SDSS.Entities;
using SDSS.PostProcess;
using SDSS.Solver;
using SDSS.StationModel;
using SDSS.Utility;
using Component = SDSS.Entities.Component;

namespace SDSS.UIControls
{
    internal partial class MainForm : Form
    {
        private StationModel1 _stationModel;

        #region ---   窗口的打开与关闭

        public MainForm(StationModel1 sm)
        {
            InitializeComponent();
            //
            _stationModel = sm;
            //
            RefreshUI(sm);
        }
        private void TSM_Exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region ---   界面的刷新

        private void RefreshUI(StationModel1 stationModel)
        {
            // 层高与跨度
            textBoxNum_layers.Text = stationModel.LayerHeights.Length.ToString();
            textBoxNum_spans.Text = stationModel.SpanWidths.Length.ToString();

            //
            RefreshUI_FrameTable(stationModel);

            // 土层信息表格
            ConstructeZDataGridViewSoil(eZDataGridViewSoilLayers);
            RefreshSoilData(stationModel);

            // 
            DefinitionsOnMaterialsChanged(stationModel.Definitions.Materials);
            DefinitionsOnProfilessChanged(stationModel.Definitions.Profiles);

            // 将新模型进行重新绘制
            SoilStructureGeometry ssg = _stationModel.GetStationGeometry() as SoilStructureGeometry;
            modelDrawer1.DrawSoilStructureModel(ssg);
        }

        private void RefreshUI_FrameTable(StationModel1 stationModel)
        {
            // 框架构件信息表格
            ConstructeZDataGridViewFrame(eZDataGridViewFrame);
            RefreshFrameData(stationModel);

            // 框架表格中的 ComboBoxColumn
            RefreshComboBoxColumn(_comboColMat, _stationModel.Definitions.Materials);
            RefreshComboBoxColumn(_comboColProf, _stationModel.Definitions.Profiles);

        }

        #endregion

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
            DefinitionsOnProfilessChanged(_stationModel.Definitions.Profiles);

            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            RefreshComboBoxColumn(_comboColProf, _stationModel.Definitions.Profiles);
            eZDataGridViewFrame.Refresh();
        }

        private void button_Materials_Click(object sender, EventArgs e)
        {
            DefinitionManager<Material> dm = new DefinitionManager<Material>(_stationModel.Definitions.Materials);
            dm.ShowDialog();

            //// 刷新 ComboBox 界面
            DefinitionsOnMaterialsChanged(_stationModel.Definitions.Materials);


            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            RefreshComboBoxColumn(_comboColMat, _stationModel.Definitions.Materials);
            eZDataGridViewFrame.Refresh();
        }

        /// <summary> 刷新 ComboBox 界面 </summary>
        private void DefinitionsOnMaterialsChanged(XmlList<Material> mats)
        {
            RefreshComboBox(comboBoxCompMaterials, mats);
            RefreshComboBox(comboBoxSoilMaterials, mats.Clone() as XmlList<Material>);
        }

        /// <summary> 刷新 ComboBox 界面 </summary>
        private void DefinitionsOnProfilessChanged(XmlList<Profile> profs)
        {
            RefreshComboBox(comboBoxProfiles, profs);
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
            // 构造窗口
            FrameConstructor fc;
            if (layerCount == _stationModel.LayerHeights.Length
                && spanCount == _stationModel.SpanWidths.Length)
            {
                fc = FrameConstructor.GetUniqueInstance(_stationModel.LayerHeights, _stationModel.SpanWidths);
            }
            else if (layerCount > 0 && spanCount > 0)
            {
                fc = FrameConstructor.GetUniqueInstance(layerCount, spanCount);
            }
            else
            {
                return;
            }

            var res = fc.ShowDialog();
            if (res == DialogResult.OK)
            {
                Material mat = _stationModel.Definitions.Materials.FirstOrDefault();
                Profile prof = _stationModel.Definitions.Profiles.FirstOrDefault();

                _stationModel.GenerateFrame(fc.LayerHeights, fc.SpanWidths, defaultMat: mat, defaultProfile: prof);

                // 
                RefreshUI_FrameTable(_stationModel);
            }
        }


        /// <summary> 为选择的构件指定材料 </summary>
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

        /// <summary> 为选择的构件指定截面 </summary>
        private void button_assignCompProfile_Click(object sender, EventArgs e)
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

        private void button_assignSoilMat_Click(object sender, EventArgs e)
        {
            //Material mat = comboBoxSoilMaterials.SelectedItem as Material;
            //if (mat != null)
            //{
            //    foreach (DataGridViewRow r in eZDataGridViewSoilLayers.SelectedRows)
            //    {
            //        SoilLayer soil = r.DataBoundItem as SoilLayer;
            //        soil.Material = mat;
            //    }
            //}
            //eZDataGridViewSoilLayers.Refresh();
        }

        #endregion

        #region ---   边界参数的设置
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
            return Utils.ExportToXmlFile(xmlFilePath: filePath, src: stationModel, errorMessage: out errorMessage);
            //return ProjectPaths.SerializeNewModelFile(filePath, stationModel, out errorMessage);
        }

        #endregion

        #region ---  从 xml 文档中导入整个模型

        private void TSM_Import_Click(object sender, EventArgs e)
        {
            string filePath = Utils.ChooseOpenStationModel("导入车站模型");
            if (filePath.Length > 0)
            {
                string errorMessage;
                bool succeeded;
                var sm = Utils.ImportFromXml(filePath, typeof(StationModel1),
                    out succeeded, out errorMessage) as StationModel1;

                if (succeeded && sm != null)
                {
                    _stationModel = sm;
                    RefreshUI(sm);
                }
                else
                {
                    MessageBox.Show("指定的文件不能正常解析为车站模型。\r\n请检查文件中的内容，或者重新指定模型文件。", "提示", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Error);
                }
            }
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
                    modelType: ModelType.Frame,
                    solverGui: SolverGUI.NoGUI
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

        /// <summary> 开始计算 </summary>
        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundCalculationParameters para = e.Argument as BackgroundCalculationParameters;

                SolverState state = para.Solver.Execute(waitingSeconds: (int)(60 * 0.1),
                    errorMessage: out para.ErrorMessage);

                e.Result = state;
            }
            catch (Exception)
            {
                e.Cancel = true;
                // ignored
            }

        }

        /// <summary> 计算完成，开始后处理 </summary>
        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            if (e.Cancelled)  // 判断是否是手动退出线程
            {

            }
            SolverState state = (SolverState)e.Result;
            AbaqusSolver solver = _bcParameters.Solver;

            // 后处理
            PostProcessor pp = new PostProcessor(solver);
            pp.CheckSolveState();
            pp.ReadFile();
            pp.WriteReport();
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