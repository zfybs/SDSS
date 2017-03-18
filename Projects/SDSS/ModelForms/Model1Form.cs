using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using eZstd.Enumerable;
using eZstd.UserControls;
using SDSS.Entities;
using SDSS.PostProcess;
using SDSS.Project;
using SDSS.Solver;
using SDSS.Models;
using SDSS.Structures;
using SDSS.UIControls;

namespace SDSS.ModelForms
{
    internal partial class Model1Form : MainForm
    {
        private Model1 _stationModel1;

        #region ---   构造函数

        public Model1Form(Model1 stationModel) : base(stationModel)
        {
            InitializeComponent();
            //
            _stationModel1 = stationModel;
        }

        private void frameConstructor1_FramePointorChanged(Frame newFrame)
        {
            _stationModel1.Frame = newFrame;
        }

        #endregion

        #region ---   界面的刷新 Refresh_NewModel

        /// <summary> 当窗口所对应的整个 StationModel 发生改变时，刷新整个界面 </summary>
        /// <param name="stationModel"></param>
        public override void Refresh_NewModel(ModelBase stationModel)
        {
            base.Refresh_NewModel(stationModel);
            //  

            frameConstructor1.ImportFrameOrDefinitions(_stationModel1.Frame, _stationModel1.Definitions);

            OnSdMaterialDefinitionChanged();
            OnSdProfileDefinitionChanged();

            // 土层信息表格
            ConstructeZDataGridViewSoil(eZDataGridViewSoilLayers);
            RefreshSoilData(_stationModel1);

            // 土层信息
            textBoxNum_OverLaying.Text = _stationModel1.MethodProperty.OverLayingSoilHeight.ToString();
            textBoxNum_topEle.Text = _stationModel1.MethodProperty.TopElevation.ToString();
        }

        #endregion

        #region ---   Definition的添加与界面处理

        public override void OnSdMaterialDefinitionChanged()
        {
            base.OnSdMaterialDefinitionChanged();
            frameConstructor1.MaterialDefinitionChanged();
        }

        public override void OnSdProfileDefinitionChanged()
        {
            base.OnSdProfileDefinitionChanged();
            frameConstructor1.ProfileDefinitionChanged();
        }

        #endregion

        #region ---   eZeZDataGridViewSoilLayers  土层参数

        /// <summary> 仅用于 Datagridview 控件的数据绑定的土层类 </summary>
        private class SoilLayerEntity
        {
            public float SoilHeight { get; set; }
            public float Kci0 { get; set; }

            public SoilLayerEntity(float soilHeight, float kci0)
            {
                SoilHeight = soilHeight;
                Kci0 = kci0;
            }
        }

        /// <summary>  </summary>
        private bool _eZDataGridViewSoilConstructed = false;

        private void ConstructeZDataGridViewSoil(eZDataGridView eZdgv)
        {
            if (_eZDataGridViewSoilConstructed)
            {
                return;
            }

            //
            eZdgv.AutoGenerateColumns = false;
            eZdgv.ManipulateRows = true;
            eZdgv.ShowRowNumber = true;
            eZdgv.SupportPaste = false;
            //

            // 创建数据列并绑定到数据源 ----------------------------------------------

            // -------------------------
            var column = new DataGridViewTextBoxColumn();
            column.Name = "SoilHeight";
            column.DataPropertyName = "SoilHeight";
            column.HeaderText = @"土层厚度 (m)";
            eZdgv.Columns.Add(column);

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.Name = "Kci0";
            column.DataPropertyName = "Kci0";
            column.HeaderText = @"K_ci0";
            column.ToolTipText = @"矩形地铁车站结构顶板上表面与地表齐平时的Kci值";
            eZdgv.Columns.Add(column);

            // 事件绑定
            eZdgv.DataError += EZdgvOnDataError;
            eZdgv.CellValueChanged += EZdgvOnCellValueChanged;
            //
            _eZDataGridViewSoilConstructed = true;
        }

        /// <summary> 根据最新的土层信息刷新表格 </summary>
        private void RefreshSoilData(Model1 sm)
        {
            var layers = new BindingList<SoilLayerEntity>()
            {
                AllowNew = true,
                AllowEdit = true,
            };

            foreach (var s in sm.SoilLayers)
            {
                layers.Add(new SoilLayerEntity(s.Top - s.Bottom, s.Kci0));
            }
            //
            layers.ListChanged += SoilLayersOnListChanged;
            layers.AddingNew += SoilLayersOnAddingNew;
            //
            eZDataGridViewSoilLayers.DataSource = layers;
        }

        private void EZdgvOnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #region ---   表格数据行的 修改 与 新增

        private void EZdgvOnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = eZDataGridViewSoilLayers.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var v = (float)cell.Value;
                if (v <= 0)
                {
                    MessageBox.Show(@"输入的数值必须大于0", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cell.Value = 1.0f;
                }
                else
                {
                    OnSoilLayerChanged(eZDataGridViewSoilLayers.DataSource as BindingList<SoilLayerEntity>);
                }
            }
        }

        private void SoilLayersOnListChanged(object sender, ListChangedEventArgs e)
        {
            var layers = sender as BindingList<SoilLayerEntity>;

            OnSoilLayerChanged(layers);
        }

        /// <summary> 土层的数量或者参数发生变化 </summary>
        /// <param name="layers"></param>
        private void OnSoilLayerChanged(IEnumerable<SoilLayerEntity> layers)
        {
            //_stationModel;
            var top = _stationModel1.MethodProperty.TopElevation;
            var overlay = _stationModel1.MethodProperty.OverLayingSoilHeight;
            top = top - overlay;
            //
            var soils = new XmlList<SoilLayer_Inertial>();
            foreach (var l in layers)
            {
                soils.Add(new SoilLayer_Inertial(top: top, bottom: top - l.SoilHeight, kci0: l.Kci0));
                top = top - l.SoilHeight;
            }
            //
            Model.SoilLayers = soils;
            _stationModel1.MethodProperty.GetKc(importantSoilLayers: soils);
            //
            RefreshUI_PictureBox(Model, null);
        }

        private void SoilLayersOnAddingNew(object sender, AddingNewEventArgs e)
        {
            var newL = new SoilLayerEntity(soilHeight: 1.0f, kci0: 0.38f);
            e.NewObject = newL;
        }

        #endregion

        #endregion

        #region ---   土层参数修改

        private void textBoxNum_OverLaying_ValueNumberChanged(object arg1, double arg2)
        {
            _stationModel1.MethodProperty.OverLayingSoilHeight = (float)arg2;
            //
            RefreshUI_PictureBox(Model, null);
        }

        private void textBoxNum_topEle_ValueNumberChanged(object arg1, double arg2)
        {
            _stationModel1.MethodProperty.TopElevation = (float)arg2;
            //
            RefreshUI_PictureBox(Model, null);
        }

        #endregion

        private void tsm_TestShowResult_Click(object sender, EventArgs e)
        {
            var solver = new AbaqusSolver(WorkingDir, Options.SolverGUI);
            var pp = new PostProcessor(Model, solver);
            try
            {
                if (pp.Results == null)
                {
                    pp.ReadResultFile(resultFilePath: WorkingDir.F_AbqResult);
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

        #region ---   环境与边界参数的设置

        private void button_Boundary_Click(object sender, EventArgs e)
        {
            var sp = _stationModel1.MethodProperty;
            BoundaryParamForm f = new BoundaryParamForm(sp.Kx, sp.Ky);
            f.ShowDialog();

            //
            sp.Kx = f.Kx;
            sp.Ky = f.Ky;
        }

        #endregion

        #region ---   Abaqus 求解开始 与 求解结束 的操作

        /// <summary> 为 Abaqus 的开始计算设置对应的 UI 界面 </summary>
        protected override void OnSdCalculationStart()
        {
            base.OnSdCalculationStart();
            //
        }

        /// <summary> 为 Abaqus 的结束计算设置对应的 UI 界面 </summary>
        protected override void OnSdCalculationFinished()
        {
            base.OnSdCalculationFinished();
            //
        }

        #endregion
    }
}