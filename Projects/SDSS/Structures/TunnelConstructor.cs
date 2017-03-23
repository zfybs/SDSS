using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDSS.Models;
using eZstd.UserControls;
using eZstd.Miscellaneous;
using SDSS.Entities;
using SDSS.Definitions;
using SDSS.Utility;

namespace SDSS.Structures
{
    public partial class TunnelConstructor : UserControl
    {
        /// <summary> 此控件内部的 Tunnel 对象的引用地址发生改变 </summary>
        public event Action<Tunnel> TunnelPointorChanged;

        private Tunnel _tunnelStructure;
        private DefinitionCollection _definitions;
        public TunnelConstructor()
        {
            InitializeComponent();
            //
            _definitions = new DefinitionCollection();
            _tunnelStructure = new Tunnel();
            //
            ConstructeZDataGridViewTunnel(eZDataGridViewTunnel);
        }

        /// <summary>
        /// 将模型中的圆形隧道或者材料、截面定义信息刷新到控件中
        /// </summary>
        /// <param name="tunnel"></param>
        public void ImportTunnelOrDefinitions(Tunnel tunnel, DefinitionCollection definitions)
        {
            if (definitions != null)
            {
                _definitions = definitions;
            }
            if (tunnel != null)
            {
                _tunnelStructure = tunnel;
                TunnelPointorChanged?.Invoke(_tunnelStructure);

                // 半径与管片块数
                textBoxNum_Radius .Text = tunnel.Radius.ToString();
                textBoxNum_Segments .Text = tunnel.SegmentNum.ToString();
                //
                RefreshUI_TunnelTable(tunnel);
            }
        }


        #region ---   eZDataGridViewTunnel  组成构件

        /// <summary> 在 DataGridView 中添加材料列 </summary>
        private DataGridViewComboBoxColumn _comboColMat;

        /// <summary> 在 DataGridView 中添加截面列 </summary>
        private DataGridViewComboBoxColumn _comboColProf;

        /// <summary>  </summary>
        private bool _eZDataGridViewTunnelConstructed = false;

        private void ConstructeZDataGridViewTunnel(eZDataGridView eZdgv)
        {
            
            if (_eZDataGridViewTunnelConstructed) return;

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
            column.Name = "LocationTag";
            column.DataPropertyName = "LocationTag";
            column.HeaderText = @"定位";
            eZdgv.Columns.Add(column);
            

            // -------------------------
            _comboColMat = new DataGridViewComboBoxColumn();
            _comboColMat.Name = "Material";
            _comboColMat.DataPropertyName = "Material";
            _comboColMat.HeaderText = @"材料";
            _comboColMat.DisplayMember = "Name";
            _comboColMat.ValueMember = "Self";
            eZdgv.Columns.Add(_comboColMat);


            // -------------------------
            _comboColProf = new DataGridViewComboBoxColumn();
            _comboColProf.Name = "Profile";
            _comboColProf.DataPropertyName = "Profile";
            _comboColProf.HeaderText = @"截面";
            _comboColProf.DisplayMember = "Name";
            _comboColProf.ValueMember = "Self";
            eZdgv.Columns.Add(_comboColProf);

            //给 DataGridView 的定位列添加注释
            eZDataGridViewTunnel.ShowCellToolTips = true;
            eZDataGridViewTunnel.Columns[1].ToolTipText = "通过二维向量（θ1,θ2）表示该管片起始节点的角度为θ1°，终止节点的角度为θ2°，其中0°表示圆管水平向的右侧点，逆时针旋转为正";

            // 事件绑定
            eZdgv.DataError += EZdgvOnDataError;

            //
            _eZDataGridViewTunnelConstructed = true;
        }

        /// <summary> 根据最新的框架构件信息刷新表格 </summary>
        private void RefreshTunnelData(Tunnel tunnel)
        {
            //
            _comboColMat.DataSource = _definitions.Materials;
            _comboColProf.DataSource = _definitions.Profiles;
            //

            // 将生成好的框架模型显示在 Datagridview 表格中
            List<Component> components = new List<Component>();
            components.AddRange(tunnel.Segments);
            //
            eZDataGridViewTunnel.DataSource = null;
            eZDataGridViewTunnel.DataSource = components;
        }

        private void EZdgvOnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region---  构造圆形隧道
        private void button_GenerateTunnel_Click(object sender, EventArgs e)
        {
            var radius = textBoxNum_Radius.ValueNumber;
            var segmentNum = (ushort)textBoxNum_Segments.ValueNumber;
            if (radius > 0 && segmentNum > 0)
            {
                Material mat = _definitions.Materials.FirstOrDefault();
                Profile prof = _definitions.Profiles.FirstOrDefault();

                _tunnelStructure = Tunnel.Create(radius, segmentNum, defaultMat: mat, defaultProfile: prof);

                // 
                RefreshUI_TunnelTable(_tunnelStructure);
                eZDataGridViewTunnel.Refresh();

                //
                TunnelPointorChanged?.Invoke(_tunnelStructure);
                //

            }
            else
            {
                MessageBox.Show("请输入正确参数","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void RefreshUI_TunnelTable(Tunnel tunnel)
        {
            // 框架构件信息表格
            ConstructeZDataGridViewTunnel(eZDataGridViewTunnel);

            RefreshTunnelData(tunnel);

            // 框架表格中的 ComboBoxColumn
            sdUtils.RefreshComboBoxColumn(_comboColMat, _definitions.Materials);
            sdUtils.RefreshComboBoxColumn(_comboColProf, _definitions.Profiles);
        }


        #endregion

        #region ---   Definition的添加与界面处理

        /// <summary> 为选择的构件指定材料 </summary>
        private void button_assignCompMat_Click(object sender, EventArgs e)
        {
            Material mat = comboBoxCompMaterials.SelectedItem as Material;
            if (mat != null)
            {
                foreach (DataGridViewRow r in eZDataGridViewTunnel.SelectedRows)
                {
                    Component comp = r.DataBoundItem as Component;
                    comp.Material = mat;
                }
            }
            eZDataGridViewTunnel.Refresh();
        }

        private void button_assignProfile_Click(object sender, EventArgs e)
        {
            Profile prof = comboBoxProfiles.SelectedItem as Profile;
            if (prof != null)
            {
                foreach (DataGridViewRow r in eZDataGridViewTunnel.SelectedRows)
                {
                    Component comp = r.DataBoundItem as Component;
                    comp.Profile = prof;
                }
            }
            eZDataGridViewTunnel.Refresh();
        }

        public void MaterialDefinitionChanged()
        {
            //// 刷新 ComboBox 界面
            sdUtils.RefreshComboBox(comboBoxCompMaterials, _definitions.Materials);

            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            sdUtils.RefreshComboBoxColumn(_comboColMat, _definitions.Materials);
            eZDataGridViewTunnel.Refresh();
        }

        public void ProfileDefinitionChanged()
        {
            // 刷新 ComboBox 界面
            sdUtils.RefreshComboBox(comboBoxProfiles, _definitions.Profiles);

            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            sdUtils.RefreshComboBoxColumn(_comboColProf, _definitions.Profiles);
            eZDataGridViewTunnel.Refresh();
        }

        #endregion

        
    }
}
