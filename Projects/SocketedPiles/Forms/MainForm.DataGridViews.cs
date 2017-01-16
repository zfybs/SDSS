using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using eZstd.UserControls;
using SocketedShafts.Definitions;
using SocketedShafts.Entities;

namespace SocketedShafts.Forms
{
    internal partial class MainForm
    {
        #region ---   dataGridViewSoilLayers  土层

        private void ConstructdataGridViewSoilLayers()
        {
            dataGridViewSoilLayers.AutoGenerateColumns = false;
            dataGridViewSoilLayers.ShowRowNumber = true;
            dataGridViewSoilLayers.SupportPaste = true;
        }

        private void RefreshdataGridViewSoilLayers(SocketedShaftSystem sss)
        {
            _soilLayers = new BindingList<SoilLayerEntity>(sss.SoilLayers);
            //
            dataGridViewSoilLayers.DataSource = _soilLayers;
            // 数据列的绑定
            ColumnSoilTop.DataPropertyName = "Top";
            ColumnSoilBottom.DataPropertyName = "Bottom";

            // DataGridViewComboBoxColumn
            ColumnSoil.DataPropertyName = "Layer";
            ColumnSoil.DisplayMember = "Name";  // 土层定义的标识名称
            ColumnSoil.ValueMember = "Self";    // 土层定义的标识名称
            ColumnSoil.DataSource = _sss.SoilDefinitions;
        }

        private void DataGridViewSoilLayersOnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var a = dataGridViewSoilLayers.Rows[0].Cells[2];
            var b = a.Value;
            var c = a.ValueType;
            Debug.Print(e.Exception.Message + "土层");
        }
        private void SoilLayersOnAddingNew(object sender, AddingNewEventArgs e)
        {
            var se = new SoilLayerEntity();
            se.Layer = _sss.SoilDefinitions.Count > 0 ? _sss.SoilDefinitions.First() : null;
            e.NewObject = se;
        }
        #endregion

        #region ---   dataGridViewShaft  桩截面

        private void ConstructdataGridViewShaft()
        {
            dataGridViewShaft.AutoGenerateColumns = false;
            dataGridViewShaft.ShowRowNumber = true;
            dataGridViewShaft.SupportPaste = true;
        }

        private void RefreshdataGridViewShaft(SocketedShaftSystem sss)
        {
            _shaftSections = new BindingList<ShaftSectionEntity>(sss.SocketedShaft.Sections);
            dataGridViewShaft.DataSource = _shaftSections;
            //
            // 数据列的绑定
            ColumnSegTop.DataPropertyName = "Top";
            ColumnSegBottom.DataPropertyName = "Bottom";
            ColumnSegment.DataPropertyName = "Section";

            // DataGridViewComboBoxColumn
            ColumnSegment.DataPropertyName = "Section";
            ColumnSegment.DisplayMember = "Name";  // 桩截面定义的标识名称
            ColumnSegment.ValueMember = "Self";    // 土层定义的标识名称
            ColumnSegment.DataSource = _sss.SectionDefinitions;
        }

        private void DataGridViewShaftOnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var a = dataGridViewShaft.Rows[0].Cells[2];
            var b = a.Value;
            var c = a.ValueType;
            Debug.Print(e.Exception.Message + "桩");
        }

        private void ShaftSectionsOnAddingNew(object sender, AddingNewEventArgs e)
        {
            var se = new ShaftSectionEntity();
            se.Section = _sss.SectionDefinitions.Count > 0 ? _sss.SectionDefinitions.First() : null;
            e.NewObject = se;
        }

        #endregion

        #region ---   DataGridViewComboBoxColumn 的刷新

        /// <summary> 将代表 土层定义 或者 截面定义 的集合转换为可以放置到表格中的 ComboBox 中的集合 </summary>
        /// <param name="column"></param>
        /// <param name="definitions"></param>
        /// <returns></returns>
        private void RefreshComboBox(DataGridViewComboBoxColumn column, IEnumerable<Definition> definitions)
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
                if (r.Index < dgv.RowCount - 1)
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

        /// <summary> 将代表 土层定义 或者 截面定义 的集合转换为可以放置到表格中的 ComboBox 中的集合 </summary>
        /// <param name="definitions"></param>
        /// <returns></returns>
        private BindingList<ListControlValue<Definition>> TransferDefinitionToComboBox(IEnumerable<Definition> definitions)
        {
            BindingList<ListControlValue<Definition>> llName = new BindingList<ListControlValue<Definition>>();
            foreach (var def in definitions)
            {
                llName.Add(new ListControlValue<Definition>(def.Name, def));
            }
            return llName;
        }



        #endregion
    }
}
