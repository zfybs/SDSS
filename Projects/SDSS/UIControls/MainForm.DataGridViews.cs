using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using eZstd.UserControls;
using SDSS.Definitions;
using SDSS.Entities;

namespace SDSS.UIControls
{
    internal partial class MainForm
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
            column.AutoSizeMode= DataGridViewAutoSizeColumnMode.DisplayedCells;
            eZdgv.Columns.Add(column);

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.Name = "ComponentType";
            column.DataPropertyName = "ComponentType";
            column.HeaderText = @"类型";
            column.AutoSizeMode= DataGridViewAutoSizeColumnMode.DisplayedCells;
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
        private void RefreshFrameData(StationModel.StationModel1 sm)
        {
            //
            _comboColMat.DataSource = sm.Definitions.Materials;
            _comboColProf.DataSource = sm.Definitions.Profiles;
            //

            // 将生成好的框架模型显示在 Datagridview 表格中
            List<Entities.Component> components = new List<Entities.Component>();
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
        /// <summary>  </summary>
        private bool _eZDataGridViewSoilConstructed = false;

        private void ConstructeZDataGridViewSoil(eZDataGridView eZdgv)
        {
            if (_eZDataGridViewSoilConstructed) { return; }

            //
            eZdgv.AutoGenerateColumns = false;
            eZdgv.ManipulateRows = true;
            eZdgv.ShowRowNumber = true;
            eZdgv.SupportPaste = false;
            //
            eZdgv.DataError += EZdgvOnDataError;
            //


            // 创建数据列并绑定到数据源 ----------------------------------------------

            // -------------------------
            var column = new DataGridViewTextBoxColumn();
            column.Name = "Top";
            column.DataPropertyName = "Top";
            column.HeaderText = @"顶部";
            column.ToolTipText = @"土层顶部标高，单位为（mm）";
            eZdgv.Columns.Add(column);

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.Name = "Bottom";
            column.DataPropertyName = "Bottom";
            column.HeaderText = @"底部";
            column.ToolTipText = @"土层底部标高，单位为（mm）";
            eZdgv.Columns.Add(column);

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.Name = "Kci0";
            column.DataPropertyName = "Kci0";
            column.HeaderText = @"K_ci0";
            column.ToolTipText = @"矩形地铁车站结构顶板上表面与地表齐平时的Kci值";
            eZdgv.Columns.Add(column);
            //
            _eZDataGridViewSoilConstructed = true;
        }

        /// <summary> 根据最新的土层信息刷新表格 </summary>
        private void RefreshSoilData(StationModel.StationModel1 sm)
        {
            _soilLayers = new BindingList<SoilLayer_Inertial>(sm.SoilLayers)
            {
                AllowNew = true,
                AllowEdit = true,
            };
            _soilLayers.ListChanged += SoilLayersOnListChanged;
            _soilLayers.AddingNew += SoilLayersOnAddingNew;
            eZDataGridViewSoilLayers.DataSource = null;
            eZDataGridViewSoilLayers.DataSource = _soilLayers;

        }

        private void EZdgvOnDataError(object sender, DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs)
        {

        }

        #region ---   表格数据行新增

        private void SoilLayersOnListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                var oldSoil = GetLastRowBoundingSoil(e.NewIndex);
                var newSoil = _soilLayers[e.NewIndex];
                if (oldSoil != null && newSoil != null)
                {
                    newSoil.Top = oldSoil.Bottom;
                    newSoil.Bottom = newSoil.Top;
                    newSoil.Kci0 = oldSoil.Kci0;
                }
            }
        }

        private void SoilLayersOnAddingNew(object sender, AddingNewEventArgs e)
        {
            var newL = new SoilLayer_Inertial();
            e.NewObject = newL;
        }

        private SoilLayer_Inertial GetLastRowBoundingSoil(int newRowid)
        {
            if (newRowid > 0)
            {
                var r = eZDataGridViewSoilLayers.Rows[newRowid - 1];

                var s = r.DataBoundItem as SoilLayer_Inertial;
                return s;
            }
            return null;
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
                Definition defaltDef = null;
                if (definitions != null && definitions.Any())
                {
                    defaltDef = definitions.First();
                }
                //
                // 刷新数据列中每一个单元格的选择
                var dgv = column.DataGridView;
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    DataGridViewComboBoxCell cell = r.Cells[column.Index] as DataGridViewComboBoxCell;
                    Definition df = cell.Value as Definition;
                    //
                    if (defaltDef != null)
                    {
                        if (df == null)  // 说明此单元格还没有赋值 
                        {
                            cell.Value = definitions.First();
                        }
                        else
                        {
                            if (!definitions.Contains(df))
                            {
                                cell.Value = definitions.First();
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
