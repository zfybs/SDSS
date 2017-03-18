using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using eZstd.UserControls;
using SDSS.Definitions;
using SDSS.Entities;
using SDSS.Models;
using SDSS.ModelForms;
using SDSS.Utility;

namespace SDSS.Structures
{
    public partial class FrameConstructor : UserControl
    {
        /// <summary> 此控件内部的 Frame 对象的引用地址发生改变 </summary>
        public event Action<Frame> FramePointorChanged;

        private Frame _frameStructure;
        private DefinitionCollection _definitions;

        public FrameConstructor()
        {
            InitializeComponent();
            //
            _definitions = new DefinitionCollection();
            _frameStructure = new Frame();
            //
            ConstructeZDataGridViewFrame(eZDataGridViewFrame);
        }

        /// <summary>
        /// 将模型中的框架结构或者材料、截面定义信息刷新到控件中
        /// </summary>
        /// <param name="frame"></param>
        public void ImportFrameOrDefinitions(Frame frame, DefinitionCollection definitions)
        {
            if (definitions != null)
            {
                _definitions = definitions;
            }
            if (frame != null)
            {
                _frameStructure = frame;
                FramePointorChanged?.Invoke(_frameStructure);

                // 层高与跨度
                textBoxNum_layers.Text = frame.LayerHeights.Length.ToString();
                textBoxNum_spans.Text = frame.SpanWidths.Length.ToString();
                //
                RefreshUI_FrameTable(frame);
            }
        }

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


            // 事件绑定
            eZdgv.DataError += EZdgvOnDataError;

            //
            _eZDataGridViewFrameConstructed = true;
        }

        /// <summary> 根据最新的框架构件信息刷新表格 </summary>
        private void RefreshFrameData(Frame frame)
        {
            //
            _comboColMat.DataSource = _definitions.Materials;
            _comboColProf.DataSource = _definitions.Profiles;
            //

            // 将生成好的框架模型显示在 Datagridview 表格中
            List<Component> components = new List<Component>();
            components.AddRange(frame.Beams);
            components.AddRange(frame.Columns);
            //
            eZDataGridViewFrame.DataSource = null;
            eZDataGridViewFrame.DataSource = components;
        }

        private void EZdgvOnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region ---   构造矩形框架模型

        private void button_GenerateFrame_Click(object sender, EventArgs e)
        {
            // create a new form
            var layerCount = (ushort) textBoxNum_layers.ValueNumber;
            var spanCount = (ushort) textBoxNum_spans.ValueNumber;


            // 构造窗口
            FrameConstructorForm fc = null;

            if ((layerCount > 0) && (spanCount > 0))
            {
                if (layerCount == _frameStructure.LayerHeights.Length
                    && spanCount == _frameStructure.SpanWidths.Length)
                {
                    fc = FrameConstructorForm.GetUniqueInstance(_frameStructure.LayerHeights, _frameStructure.SpanWidths);
                }
                else
                {
                    fc = FrameConstructorForm.GetUniqueInstance(layerCount, spanCount);
                }
            }
            else
            {
                MessageBox.Show(@"框架的层数与跨数都必须大于0", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (fc != null)
            {
                var res = fc.ShowDialog();
                if (res == DialogResult.OK)
                {
                    Material mat = _definitions.Materials.FirstOrDefault();
                    Profile prof = _definitions.Profiles.FirstOrDefault();

                    _frameStructure = Frame.Create(fc.LayerHeights, fc.SpanWidths, defaultMat: mat, defaultProfile: prof);

                    // 
                    RefreshUI_FrameTable(_frameStructure);
                    eZDataGridViewFrame.Refresh();

                    //
                    FramePointorChanged?.Invoke(_frameStructure);
                }
            }
        }

        private void RefreshUI_FrameTable(Frame frame)
        {
            // 框架构件信息表格
            ConstructeZDataGridViewFrame(eZDataGridViewFrame);

            RefreshFrameData(frame);

            // 框架表格中的 ComboBoxColumn
            sdUtils.RefreshComboBoxColumn(_comboColMat, _definitions.Materials);
            sdUtils.RefreshComboBoxColumn(_comboColProf, _definitions.Profiles);
        }

        #endregion

        #region ---   Definition的添加与界面处理

        /// <summary> 为选择的构件指定材料 </summary>
        private void button_assignCompMat_Click_1(object sender, EventArgs e)
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

        private void button_assignProfile_Click_1(object sender, EventArgs e)
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

        public void MaterialDefinitionChanged()
        {
            //// 刷新 ComboBox 界面
            sdUtils.RefreshComboBox(comboBoxCompMaterials, _definitions.Materials);

            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            sdUtils.RefreshComboBoxColumn(_comboColMat, _definitions.Materials);
            eZDataGridViewFrame.Refresh();
        }

        public void ProfileDefinitionChanged()
        {
            // 刷新 ComboBox 界面
            sdUtils.RefreshComboBox(comboBoxProfiles, _definitions.Profiles);

            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            sdUtils.RefreshComboBoxColumn(_comboColProf, _definitions.Profiles);
            eZDataGridViewFrame.Refresh();
        }

        #endregion
    }
}