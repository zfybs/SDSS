using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using eZstd.Miscellaneous;
using eZstd.UserControls;
using SocketedShafts.Definitions;
using SocketedShafts.Entities;
using SocketedShafts.Forms;

namespace SocketedShafts.Forms
{
    internal partial class MainForm : Form
    {
        #region ---   Fields

        private SocketedShaftSystem _sss;
        /// <summary> 模型画板 </summary>
        private SssDrawing _drawing;


        /// <summary> 整个模型中每一个具体土层的信息 </summary>
        private BindingList<SoilLayerEntity> _soilLayers;
        /// <summary> 整个模型中每一个具体桩截面的信息 </summary>
        private BindingList<ShaftSectionEntity> _shaftSections;

        #endregion

        #region ---   构造函数

        public MainForm()
        {
            InitializeComponent();

            //
            _drawing = new SssDrawing(_pictureBoxSystem);

            // 表格
            ConstructdataGridViewShaft();
            ConstructdataGridViewSoilLayers();

            //
            // 事件绑定
            dataGridViewSoilLayers.DataError += DataGridViewSoilLayersOnDataError;
            dataGridViewShaft.DataError += DataGridViewShaftOnDataError;

            // 绘图事件
            dataGridViewSoilLayers.CellValueChanged += DataGridViewSoilLayersOnCellValueChanged;
            dataGridViewShaft.CellValueChanged += DataGridViewShaftOnCellValueChanged;
        }

        public void RefreshModel(SocketedShaftSystem sss)
        {
            _sss = sss;
            //
            //
            RefreshdataGridViewSoilLayers(sss);
            RefreshdataGridViewShaft(sss);
            //
            _soilLayers.AddingNew += SoilLayersOnAddingNew;
            _shaftSections.AddingNew += ShaftSectionsOnAddingNew;

        }

        #endregion

        #region ---   界面操作

        private void buttonSoilManager_Click(object sender, EventArgs e)
        {
            DefinitionManager<SoilLayer> dm = new DefinitionManager<SoilLayer>(_sss.SoilDefinitions);
            dm.ShowDialog();
            // 刷新表格界面
            RefreshComboBox(ColumnSoil, _sss.SoilDefinitions);
        }

        private void buttonSectionManager_Click(object sender, EventArgs e)
        {
            DefinitionManager<ShaftSection> dm = new DefinitionManager<ShaftSection>(_sss.SectionDefinitions);
            dm.ShowDialog();
            // 刷新表格界面
            RefreshComboBox(ColumnSegment, _sss.SectionDefinitions);
        }

        private void buttonSystemProperty_Click(object sender, EventArgs e)
        {
            var s = _sss.SystemProperty;
            AddDefinition<SystemProperty> ads = new AddDefinition<SystemProperty>(s);
            ads.ShowDialog();
        }



        #endregion

        #region ---  PictureBox 绘图界面的刷新

        // 引发绘图操作的各种事件
        private void DataGridViewSoilLayersOnCellValueChanged(object sender, DataGridViewCellEventArgs dataGridViewCellEventArgs)
        {
            RefreshPaintingWithSss(_sss);
        }

        private void DataGridViewShaftOnCellValueChanged(object sender, DataGridViewCellEventArgs dataGridViewCellEventArgs)
        {
            RefreshPaintingWithSss(_sss);
        }

        // 绘图
        private void RefreshPaintingWithSss(SocketedShaftSystem sss)
        {
            _drawing.Draw(_sss);
        }

        // 保存
        private void buttonSavePicture_Click(object sender, EventArgs e)
        {
            string filePath = Utils.ChooseSaveEmf("将模型导出为矢量图");
            if (filePath.Length > 0)
            {
                _drawing.Save(filePath, _sss);
            }
        }

        #endregion

        #region ---  将整个模型导出到 xml 文档
        /// <summary> 将整个模型导出到 xml 文档 </summary>
        private void buttonExportToXML_Click(object sender, EventArgs e)
        {
            string errorMessage;
            if (ValidateModel(_sss, out errorMessage))
            {
                string filePath = Utils.ChooseSaveSSS("导出水平受荷嵌岩桩文件");
                if (filePath.Length > 0)
                {
                    ExportToXml(_sss, filePath);
                }
            }
            else
            {
                MessageBox.Show("当前模型不符合导出规范 \n\r" + errorMessage, "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateModel(SocketedShaftSystem sss, out string errorMessage)
        {
            if (string.IsNullOrEmpty(sss.SocketedShaft.Name))
            {
                errorMessage = "桩未命名";
                return false;
            }
            if (!ValidateConnectivity(sss.SoilLayers, out errorMessage))
            {
                errorMessage = "土层连续性不满足： " + errorMessage;
                return false;
            }
            if (!ValidateConnectivity(sss.SocketedShaft.Sections, out errorMessage))
            {
                errorMessage = "桩段连续性不满足： " + errorMessage;
                return false;
            }
            // 检查标高位置是否合适
            if (sss.SocketedShaft.Sections.First().Top < sss.SoilLayers.First().Top)
            {
                errorMessage = "桩顶标高不能低于土层顶部标高 ";
                return false;
            }

            if (sss.SocketedShaft.Sections.First().Bottom > sss.SoilLayers.First().Top)
            {
                errorMessage = "土层顶部以上（水中）最多只能有一种截面（暂时对于水中桩段的计算只设计均匀截面的情况。）";
                return false;
            }

            if ((sss.SocketedShaft.Sections.Last().Bottom > sss.SoilLayers.First().Top)
                || (sss.SocketedShaft.Sections.Last().Bottom < sss.SoilLayers.Last().Bottom))
            {
                errorMessage = "桩底标高必须在土层范围之内 ";
                return false;
            }

            //
            //var soilTop = sss.SoilLayers.First().Top;
            //if (soilTop < sss.SystemProperty.WaterTop)
            //{
            //    //errorMessage = "水面标高低于土层顶部标高";
            //    //return false;
            //    //sss.SystemProperty.WaterTop = soilTop;
            //}

            errorMessage = "可以成功导出。";
            return true;
        }

        /// <summary>
        /// 保证土层或者桩截面的连续性
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        private bool ValidateConnectivity(IEnumerable<Entity> entities, out string errorMessage)
        {
            double precision = 0.001;
            // 保证桩截面的连续性
            if (!entities.Any())
            {
                errorMessage = "至少要有一层土或一个桩段";
                return false;
            }

            int count = entities.Count();
            int index = 0;
            float top = entities.First().Top;
            var it = entities.GetEnumerator();
            while (it.MoveNext())
            {
                var ent = it.Current;
                if (Math.Abs(top - ent.Top) > precision)
                {
                    errorMessage = $"第 {index + 1} 层的顶部标高与其上一层的底部标高不连续";
                    return false;
                }
                else
                {
                    top = ent.Bottom;
                }
                //
                if (ent.Top <= ent.Bottom)
                {
                    errorMessage = $"第 {index + 1} 层的顶部标高必须高于底部标高";
                    return false;
                }

                //
                index += 1;
            }
            errorMessage = "满足连续性";
            return true;
        }

        /// <param name="filePath">此路径必须为一个有效的路径</param>
        private void ExportToXml(SocketedShaftSystem sss, string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);

                XmlSerializer s = new XmlSerializer(sss.GetType());
                s.Serialize(fs, sss);
                fs.Close();
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, "");
            }

        }

        #endregion

        #region ---  从 xml 文档导入整个模型信息

        /// <summary> 从 xml 文档导入整个模型信息 </summary>
        private void buttonImportFromXML_Click(object sender, EventArgs e)
        {
            string filePath = Utils.ChooseOpenSSS("导入水平受荷嵌岩桩文件");
            if (filePath.Length > 0)
            {
                // "../2.sss"
                ImportFromXml(filePath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">此路径必须为一个有效的路径</param>
        private void ImportFromXml(string filePath)
        {
            try
            {
                //
                XmlReader xr = XmlReader.Create(filePath);
                //
                XmlSerializer ss = new XmlSerializer(typeof(SocketedShaftSystem));
                SocketedShaftSystem sss = (SocketedShaftSystem)ss.Deserialize(xr);
                xr.Close();

                // 同步到全局
                SocketedShaftSystem.SetUniqueInstance(sss);
                //
                this.RefreshModel(sss);
                // 绘图
                RefreshPaintingWithSss(_sss);
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, "指定的文件不能正常导入为嵌岩桩模型。");
            }
        }

        #endregion


        private void buttonShaft_Click(object sender, EventArgs e)
        {
            AddDefinition<SocketedShaft> dds = new AddDefinition<SocketedShaft>(_sss.SocketedShaft);
            dds.ShowDialog();
        }
        
    }
}