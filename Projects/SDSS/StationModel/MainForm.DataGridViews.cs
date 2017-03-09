using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using eZstd.Enumerable;
using eZstd.UserControls;
using SDSS.Definitions;
using SDSS.Entities;
using SDSS.StationModel;
using Component = SDSS.Entities.Component;

namespace SDSS.StationModel
{

    internal partial class MainForm : Form
    {
        /// <summary> 整个模型中每一个具体土层的信息 </summary>
        private BindingList<SoilLayer_Inertial> _soilLayers;

        #region ---   eZDataGridViewFrame  框架构件

        /// <summary>  </summary>
        private DataGridViewComboBoxColumn _comboColMat;

        /// <summary>  </summary>
        private DataGridViewComboBoxColumn _comboColProf;

        /// <summary>  </summary>
        private bool _eZDataGridViewFrameConstructed = false;

        private void ConstructeZDataGridViewFrame(eZDataGridView eZdgv)
        {
            if (_eZDataGridViewFrameConstructed) return;

            //eZdgv.DataSource = _persons;

            eZdgv.AllowUserToAddRows = false;
            eZdgv.AutoGenerateColumns = false;
            //
            eZdgv.ShowRowNumber = true;
            eZdgv.ManipulateRows = false;
            eZdgv.SupportPaste = false;

            // 创建数据列并绑定到数据源 ----------------------------------------------

            // -------------------------
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ID";
            column.Name = "ID";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            eZdgv.Columns.Add(column);

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.Name = "ComponentType";
            column.DataPropertyName = "ComponentType";
            column.HeaderText = @"类型";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            eZdgv.Columns.Add(column);

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.Name = "LocationTag";
            column.DataPropertyName = "LocationTag";
            column.HeaderText = @"定位";
            eZdgv.Columns.Add(column);

            // -------------------------
            _comboColMat = new DataGridViewComboBoxColumn();
            //combo.DataSource = Enum.GetValues(typeof(Gender));
            _comboColMat.Name = "Material";
            _comboColMat.DataPropertyName = "Material";
            _comboColMat.HeaderText = @"材料";

            _comboColMat.DisplayMember = "Name";
            _comboColMat.ValueMember = "Self";

            eZdgv.Columns.Add(_comboColMat);
            // 如果要设置对应单元格的值为某枚举项：combo.Item(combo.Index,行号).Value = Gender.Male;

            // -------------------------
            _comboColProf = new DataGridViewComboBoxColumn();
            //combo.DataSource = Enum.GetValues(typeof(Gender));
            _comboColProf.Name = "Profile";
            _comboColProf.DataPropertyName = "Profile";
            _comboColProf.HeaderText = @"截面";

            _comboColProf.DisplayMember = "Name";
            _comboColProf.ValueMember = "Self";

            eZdgv.Columns.Add(_comboColProf);
            // 如果要设置对应单元格的值为某枚举项：combo.Item(combo.Index,行号).Value = Gender.Male;

            _eZDataGridViewFrameConstructed = true;
        }

        /// <summary> 根据最新的框架构件信息刷新表格 </summary>
        private void RefreshFrameData(StationModel1 sm)
        {
            //
            _comboColMat.DataSource = sm.Definitions.Materials;
            _comboColProf.DataSource = sm.Definitions.Profiles;
            //

            // 将生成好的框架模型显示在 Datagridview 表格中
            List<Component> components = new List<Component>();
            components.AddRange(sm.Beams);
            components.AddRange(sm.Columns);
            //
            eZDataGridViewFrame.DataSource = null;
            eZDataGridViewFrame.DataSource = components;
        }

        private void eZDataGridViewFrame_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
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
        private void RefreshSoilData(StationModel1 sm)
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

        private void EZdgvOnDataError(object sender, DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs)
        {
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
            var top = _stationModel.SoilProperty.TopElevation;
            var overlay = _stationModel.SoilProperty.OverLayingSoilHeight;
            top = top - overlay;
            //
            var soils = new XmlList<SoilLayer_Inertial>();
            foreach (var l in layers)
            {

                soils.Add(new SoilLayer_Inertial(top: top, bottom: top - l.SoilHeight, kci0: l.Kci0));
                top = top - l.SoilHeight;
            }
            //
            _stationModel.SoilLayers = soils;
            _stationModel.SoilProperty.GetKc(importantSoilLayers: soils);
            //
            RefreshUI_PictureBox(_stationModel);
        }

        private void SoilLayersOnAddingNew(object sender, AddingNewEventArgs e)
        {
            var newL = new SoilLayerEntity(soilHeight: 1.0f, kci0: 0.38f);
            e.NewObject = newL;
        }

        #endregion

        #endregion

        #region ---   eZDataGridViewComboBoxColumn 的刷新

        /// <summary> 将代表 土层定义 或者 截面定义 的集合转换为可以放置到表格中的 ComboBoxColumn 中的集合 </summary>
        /// <param name="column"></param>
        /// <param name="definitions"></param>
        /// <returns></returns>
        private void RefreshComboBoxColumn(DataGridViewComboBoxColumn column, IEnumerable<Definition> definitions)
        {
            if (column != null)
            {
                // 设置一个默认的定义
                Definition defaultDef = null;
                if (definitions != null && definitions.Any())
                {
                    defaultDef = definitions.First();
                }
                //
                // 刷新数据列中每一个单元格的选择
                var dgv = column.DataGridView;
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    DataGridViewComboBoxCell cell = r.Cells[column.Index] as DataGridViewComboBoxCell;
                    Definition df = cell.Value as Definition;
                    //
                    if (defaultDef != null)
                    {
                        if (df == null) // 说明此单元格还没有赋值 
                        {
                            cell.Value = defaultDef;
                        }
                        else
                        {
                            if (!definitions.Contains(df))
                            {
                                cell.Value = defaultDef;
                            }
                        }
                    }
                    else // 说明没有任何有效的定义
                    {
                        cell.Value = null;
                    }
                }
            }
        }

        #endregion
    }
}