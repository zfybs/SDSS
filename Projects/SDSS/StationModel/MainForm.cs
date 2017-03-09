using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using eZstd.Enumerable;
using SDSS.Definitions;
using SDSS.PostProcess;
using SDSS.Project;
using SDSS.Solver;
using SDSS.UIControls;
using SDSS.Utility;
using Component = SDSS.Entities.Component;
using Timer = System.Timers.Timer;

namespace SDSS.StationModel
{
    internal partial class MainForm : Form
    {
        private const string FormTitle = "地铁车站抗震设计";

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

        #region ---   界面的刷新 RefreshUI

        private void RefreshUI(StationModel1 stationModel)
        {
            Text = FormTitle + @" - " + stationModel.ModelName;
            label_elapsedTime.Text = "";

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

            //
            textBoxNum_OverLaying.Text = _stationModel.SoilProperty.OverLayingSoilHeight.ToString();
            textBoxNum_topEle.Text = _stationModel.SoilProperty.TopElevation.ToString();

            // 图片框
            RefreshUI_PictureBox(stationModel);
            //
            setToolStripTxtboxText(tst_abqWorkingDir, ProjectPaths.D_AbaqusWorkingDir);
        }

        private static void setToolStripTxtboxText(ToolStripTextBox tstb, string text)
        {
            tstb.Text = text;
            var width = tstb.TextBox.CreateGraphics().MeasureString(text, tstb.Font).Width;
            var size = new Size((int) width, tstb.Height);
            tstb.Size = size;
        }

        private void RefreshUI_FrameTable(StationModel1 stationModel)
        {
            // 框架构件信息表格
            ConstructeZDataGridViewFrame(eZDataGridViewFrame);
            RefreshFrameData(stationModel);

            // 框架表格中的 ComboBoxColumn
            RefreshComboBoxColumn(_comboColMat, stationModel.Definitions.Materials);
            RefreshComboBoxColumn(_comboColProf, stationModel.Definitions.Profiles);
            eZDataGridViewFrame.Refresh();
        }

        private void RefreshUI_PictureBox(StationModel1 stationModel)
        {
            // 将新模型进行重新绘制
            SoilStructureGeometry ssg = stationModel.GetStationGeometry() as SoilStructureGeometry;
            modelDrawer1.DrawSoilStructureModel(ssg);
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
            var layerCount = (ushort) textBoxNum_layers.ValueNumber;
            var spanCount = (ushort) textBoxNum_spans.ValueNumber;
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
                //
                RefreshUI_PictureBox(_stationModel);
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

        #endregion

        #region ---   环境与边界参数的设置

        private void textBoxNum_topEle_ValueNumberChanged(object arg1, double arg2)
        {
            _stationModel.SoilProperty.TopElevation = (float) arg2;
            //
            RefreshUI_PictureBox(_stationModel);
        }

        private void textBoxNum_OverLaying_ValueNumberChanged(object arg1, double arg2)
        {
            _stationModel.SoilProperty.OverLayingSoilHeight = (float) arg2;
            //
            RefreshUI_PictureBox(_stationModel);
        }

        private void button_Boundary_Click(object sender, EventArgs e)
        {
            var sp = _stationModel.SoilProperty;
            BoundaryParamForm f = new BoundaryParamForm(sp.Kx, sp.Ky);
            f.ShowDialog();

            //
            sp.Kx = f.Kx;
            sp.Ky = f.Ky;
        }

        #endregion

        #region ---  将整个模型导出到 xml 文档 与  从 xml 文档中导入整个模型

        /// <summary> 将整个模型导出到 xml 文档 </summary>
        private void TSM_Export_Click(object sender, EventArgs e)
        {
            StringBuilder errorMessage = new StringBuilder();
            if (ValidateModel(_stationModel, ref errorMessage))
            {
                string filePath = sdUtils.ChooseSaveStationModel("导出车站模型");
                if (filePath.Length > 0)
                {
                    bool succ = sdUtils.ExportToXmlFile(xmlFilePath: filePath, src: _stationModel,
                        errorMessage: ref errorMessage);
                    if (succ)
                    {
                        MessageBox.Show(@"模型导出成功", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(@"模型导出失败", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("当前模型不符合导出规范 \n\r" + errorMessage, "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateModel(StationModel1 sm, ref StringBuilder errorMessage)
        {
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
        }

        private void TSM_Import_Click(object sender, EventArgs e)
        {
            string filePath = sdUtils.ChooseOpenStationModel("导入车站模型");
            if (filePath.Length > 0)
            {
                StringBuilder errorMessage = new StringBuilder();
                bool succeeded;
                var sm = sdUtils.ImportFromXml(filePath, typeof (StationModel1),
                    out succeeded, ref errorMessage) as StationModel1;

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

        #region ---   Abaqus 求解 以及 前后处理

        #region ---   求解计算

        private AbaqusSolver _solver;
        private BackgroundCalculationParameters _bcParameters;

        private class BackgroundCalculationParameters
        {
            public AbaqusSolver Solver;
            public string ErrorMessage;
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            StringBuilder errorMessage = new StringBuilder();

            // 对模型进行检查，看是否可以进行计算
            if (_stationModel.Validate(ref errorMessage))
            {
                _solver = new AbaqusSolver(
                    workingDir: ProjectPaths.D_AbaqusWorkingDir,
                    modelType: ModelType.Frame,
                    solverGui: SolverGUI.NoGUI
                    );

                // 检查计算环境，文件配置
                if (_solver.CheckEnvironment(_stationModel, ref errorMessage))
                {
                    // 求解计算
                    _bcParameters = new BackgroundCalculationParameters()
                    {
                        Solver = _solver,
                        ErrorMessage = errorMessage.ToString(),
                    };
                    //
                    progressBar1.Visible = true;
                    progressBar1.Style = ProgressBarStyle.Marquee;

                    if (_bgw_Solve.IsBusy != true)
                    {
                        // 
                        SetUIForCalculationStart();
                        // Start the asynchronous operation.
                        _bgw_Solve.RunWorkerAsync(argument: _bcParameters);
                    }
                }
            }
        }

        /// <summary> 开始计算 </summary>
        private void _bgw_Solver_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundCalculationParameters para = e.Argument as BackgroundCalculationParameters;
                //
                _solver.CalculationTimerElapsed += SolverOnCalculationTimerElapsed;
                SolverState state = para.Solver.Execute(
                    waitingSeconds: Options.WaitingSeconds,
                    errorMessage: out para.ErrorMessage
                    );
            }
            catch (Exception)
            {
                e.Cancel = true;
                // ignored
            }
        }

        /// <summary> 强行终止 Abaqus 的计算 </summary>
        private void button_Terminate_Click(object sender, EventArgs e)
        {
            if (_solver != null && _solver.State == SolverState.Calculating)
            {
                _solver.TerminateAbqCalculation();
            }
        }

        #endregion

        #region ---   计算过程中的操作

        private void SolverOnCalculationTimerElapsed(Timer timer, TimeSpan timeSpan)
        {
            if (_bgw_Solve != null)
            {
                _bgw_Solve.ReportProgress((int) timeSpan.TotalSeconds, timeSpan);
            }
        }

        private void _bgw_Solve_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var ts = (TimeSpan) e.UserState;
            label_elapsedTime.Text = @"Elapsed time : " + ts.ToString(@"hh\:mm\:ss");
        }

        #endregion

        #region ---   后处理操作

        private PostProcessor _postProcessor;

        /// <summary> 计算完成，开始后处理 </summary>
        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetUIForCalculationFinished();
            //
            if (e.Cancelled) // 判断是否是手动退出线程
            {
            }

            AbaqusSolver solver = _bcParameters.Solver;

            // 后处理
            StringBuilder sb = new StringBuilder();
            _postProcessor = new PostProcessor(solver);
            if (_postProcessor.CheckFinishState(errorMessage: ref sb))
            {
                SolverState ss = _postProcessor.CheckResultFiles(errorMessage: ref sb);
                if (ss == SolverState.Succeeded)
                {
                    if (Options.DirectlyReport)
                    {
                        ReadAndShowResults(_postProcessor);
                    }
                    else
                    {
                        var res = MessageBox.Show(@"计算结束且成功，是否直接生成报告？", @"Congratulations", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Asterisk);
                        if (res == DialogResult.OK)
                        {
                            ReadAndShowResults(_postProcessor);
                        }
                    }
                }
                else if (ss == SolverState.FailedWithError)
                {
                    var res = MessageBox.Show("Abaqus 计算过程中出错而导致计算终止！\r\n" + sb.ToString(), @"提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                var res = MessageBox.Show("Abaqus 计算过程未正常结束！\r\n" + sb.ToString(), @"提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void tsm_WriteReport_Click(object sender, EventArgs e)
        {
            ReadAndShowResults(_postProcessor);
        }


        /// <summary>
        /// 将 Abaqus 的计算结果进行读取，并显示出来
        /// </summary>
        /// <param name="pp"></param>
        private void ReadAndShowResults(PostProcessor pp)
        {
            if (pp != null)
            {
                try
                {
                    if (pp.Results == null)
                    {
                        pp.ReadResultFile(resultFilePath: ProjectPaths.F_AbqResult);
                    }
                    pp.ShowResultsList();
                }
                catch (Exception ex)
                {
                    // ignored
                    var res = MessageBox.Show("后处理过程出现异常！\r\n", @"提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                var res = MessageBox.Show(@"未找到有效的计算结果！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsm_ReportTest_Click(object sender, EventArgs e)
        {
            var pp = new PostProcessor(null);
            try
            {
                if (pp.Results == null)
                {
                    pp.ReadResultFile(resultFilePath: ProjectPaths.F_AbqResult);
                }
                pp.ShowResultsList();
            }
            catch (Exception ex)
            {
                // ignored
                var res = MessageBox.Show("后处理过程出现异常！\r\n", @"提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        /// <summary> 为 Abaqus 的开始计算设置对应的 UI 界面 </summary>
        private void SetUIForCalculationStart()
        {
            button_Terminate.Visible = true;
            buttonSolve.Enabled = false;
            label_elapsedTime.Text = "";
            label_elapsedTime.Visible = true;
        }

        /// <summary> 为 Abaqus 的结束计算设置对应的 UI 界面 </summary>
        private void SetUIForCalculationFinished()
        {
            progressBar1.Visible = false;
            button_Terminate.Visible = false;
            buttonSolve.Enabled = true;
            label_elapsedTime.Visible = false;
        }

        #endregion

        #region ---   设置软件环境

        private OptionsForm _opForm;

        private void TSM_Option_Click(object sender, EventArgs e)
        {
            if (_opForm == null)
            {
                _opForm = new OptionsForm();
            }
            _opForm.ShowDialog();
        }

        //设置当前项目的计算工作路径（项目>工作文件夹）
        private ModelOptions _mo;

        private void tsm_abqWkDir_Click(object sender, EventArgs e)
        {
            if (_mo == null)
            {
                _mo = new ModelOptions(_stationModel);
            }
            var res = _mo.ShowDialog();
            if (res == DialogResult.OK)
            {
                string str = ProjectPaths.D_AbaqusWorkingDir;
                setToolStripTxtboxText(tst_abqWorkingDir, str);

                //
                Text = FormTitle + " - " + _stationModel.ModelName;
            }
        }

        #endregion
    }
}