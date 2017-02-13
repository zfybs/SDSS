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

        /// <summary> 整个模型中每一个具体梁柱等构件的信息 </summary>
        private BindingList<Entities.Component> _components;
        /// <summary> 整个模型中每一个具体土层的信息 </summary>
        private BindingList<SoilLayer> _soilLayers;


        //        #region ---   eZeZDataGridViewSoilLayers  土层参数

        //        private void ConstructeZDataGridViewSoilLayers()
        //        {
        //            eZDataGridViewSoilLayers.AutoGenerateColumns = false;
        //            eZDataGridViewSoilLayers.ShowRowNumber = true;
        //            eZDataGridViewSoilLayers.SupportPaste = true;
        //            //

        //        }

        //        private void RefresheZDataGridViewSoilLayers(StationModel.StationModel sm)
        //        {
        //            _soilLayers = new BindingList<SoilLayer>(sm.SoilLayers);
        //            //
        //            eZDataGridViewSoilLayers.DataSource = _soilLayers;
        //            // 数据列的绑定
        //            ColumnSoilTop.DataPropertyName = "Top";
        //            ColumnSoilBottom.DataPropertyName = "Bottom";

        //            // eZDataGridViewComboBoxColumn
        //            ColumnSoil.DataPropertyName = "Layer";
        //            ColumnSoil.DisplayMember = "Name";  // 土层定义的标识名称
        //            ColumnSoil.ValueMember = "Self";    // 土层定义的标识名称
        //            ColumnSoil.DataSource = sm.SoilDefinitions;
        //        }

        //        private void eZDataGridViewSoilLayersOnDataError(object sender, DataGridViewDataErrorEventArgs e)
        //        {
        //            var a = eZDataGridViewSoilLayers.Rows[0].Cells[2];
        //            var b = a.Value;
        //            var c = a.ValueType;
        //            Debug.Print(e.Exception.Message + "土层");
        //        }
        //        private void SoilLayersOnAddingNew(object sender, AddingNewEventArgs e)
        //        {
        //            var se = new SoilLayer();
        //            se.Layer = _sss.SoilDefinitions.Count > 0 ? _sss.SoilDefinitions.First() : null;
        //            e.NewObject = se;
        //        }
        //        #endregion

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

            eZdgv.AutoGenerateColumns = false;
            eZdgv.ShowRowNumber = true;
            eZdgv.SupportPaste = false;

            // 创建数据列并绑定到数据源 ----------------------------------------------

            // -------------------------
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ID";
            column.Name = "ID";
            eZdgv.Columns.Add(column);

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.Name = "ComponentType";
            column.DataPropertyName = "ComponentType";
            column.HeaderText = @"类型";
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



        private void RefresheZDataGridViewFrame(StationModel.StationModel1 sm)
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
            eZDataGridViewFrame.DataSource = components;
        }

        private void eZDataGridViewFrame_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

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
