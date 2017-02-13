using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using eZstd.Miscellaneous;
using SDSS.Definitions;
using SDSS.Entities;
using SDSS.StationModel;

namespace SDSS.UIControls
{
    internal partial class MainForm : Form
    {
        private StationModel1 _stationModel;

        public MainForm(StationModel1 sm)
        {
            InitializeComponent();
            //
            _stationModel = sm;
        }

        #region ---   PictureBox 绘图操作

        private void modelDrawer1_Resize(object sender, System.EventArgs e)
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

        private void button_Profiles_Click(object sender, System.EventArgs e)
        {
            DefinitionManager<Profile> dm = new DefinitionManager<Profile>(_stationModel.Definitions.Profiles);
            dm.ShowDialog();
            // 刷新 ComboBox 界面
            RefreshComboBox(comboBoxProfiles, _stationModel.Definitions.Profiles);
            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            RefreshComboBoxColumn(_comboColProf, _stationModel.Definitions.Profiles);
            eZDataGridViewFrame.Refresh();
        }

        private void button_Materials_Click(object sender, System.EventArgs e)
        {
            DefinitionManager<Material> dm = new DefinitionManager<Material>(_stationModel.Definitions.Materials);
            dm.ShowDialog();
            // 刷新 ComboBox 界面
            RefreshComboBox(comboBoxMaterials, _stationModel.Definitions.Materials);
            // 刷新 datagridview 中的对应 DataGridViewComboBoxColumn 列的数据
            RefreshComboBoxColumn(_comboColMat, _stationModel.Definitions.Materials);
            eZDataGridViewFrame.Refresh();
        }

        /// <summary>  </summary>
        private void RefreshComboBox(ComboBox comboBox, IEnumerable<Definition> definitions)
        {
            // 刷新数据列中每一个单元格的选择
            comboBox.DataSource = null;
            comboBox.DataSource = definitions;
            comboBox.DisplayMember = "Name";
        }

        #endregion

        #region ---   构造矩形框架模型

        private void button_GenerateFrame_Click(object sender, System.EventArgs e)
        {
            // create a new form
            var layerCount = (ushort)textBoxNum_layers.ValueNumber;
            var spanCount = (ushort)textBoxNum_spans.ValueNumber;
            if (layerCount > 0 && spanCount > 0)
            {
                FrameConstructor fc = FrameConstructor.GetUniqueInstance(layerCount, spanCount);
                //
                var res = fc.ShowDialog();
                if (res == DialogResult.OK)
                {
                    _stationModel.GenerateFrame(fc.LayerHeights, fc.SpanWidths);

                    // 
                    if (!_eZDataGridViewFrameConstructed)
                    {
                        ConstructeZDataGridViewFrame(eZDataGridViewFrame);
                    }
                    RefresheZDataGridViewFrame(_stationModel);
                    RefreshComboBoxColumn(_comboColMat, _stationModel.Definitions.Materials);
                    RefreshComboBoxColumn(_comboColProf, _stationModel.Definitions.Profiles);

                    // 将新模型进行重新绘制
                    SoilStructureGeometry ssg = _stationModel.GetStationGeometry() as SoilStructureGeometry;
                    modelDrawer1.DrawSoilStructureModel(ssg);
                }
            }
        }

        /// <summary> 为选择的构件指定材料 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_assignMat_Click(object sender, System.EventArgs e)
        {
            Material mat = comboBoxMaterials.SelectedItem as Material;
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
        private void button_assignProfile_Click(object sender, System.EventArgs e)
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

        #region ---  将整个模型导出到 xml 文档

        /// <summary> 将整个模型导出到 xml 文档 </summary>
        private void TSM_Export_Click(object sender, System.EventArgs e)
        {
            string errorMessage;
            if (ValidateModel(_stationModel, out errorMessage))
            {
                string filePath = Utils.ChooseSaveStationModel("导出车站模型");
                if (filePath.Length > 0)
                {
                    ExportToXml(_stationModel, filePath);
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
        private void ExportToXml(StationModel.StationModel stationModel, string filePath)
        {
            FileStream fs = null;
            try
            {
                Type tp = stationModel.GetType();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                fs = new FileStream(filePath, FileMode.OpenOrCreate);
                XmlSerializer s = new XmlSerializer(tp);
                s.Serialize(fs, stationModel);
                fs.Close();

                //
                FileStream reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                XmlSerializer sReader = new XmlSerializer(tp);
                object obj = sReader.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, "");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        #endregion

        #region ---  从 xml 文档中导入整个模型

        private void TSM_Import_Click(object sender, System.EventArgs e)
        {
            string filePath = Utils.ChooseOpenStationModel("导入车站模型");
            if (filePath.Length > 0)
            {
                StationModel.StationModel1 sm = ImportFromXml(filePath);
                if (sm != null)
                {
                    _stationModel = sm;
                }
                else
                {
                    MessageBox.Show("指定的文件不能正常解析为车站模型。\r\n请检查文件中的内容，或者重新指定模型文件。", "提示", MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Error);
                }
            }
        }

        /// <param name="filePath">此路径必须为一个有效的路径</param>
        private static StationModel.StationModel1 ImportFromXml(string filePath)
        {
            FileStream reader = null;
            object obj = null;
            try
            {
                Type tp = typeof(StationModel.StationModel1);

                //
                reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                XmlSerializer sReader = new XmlSerializer(tp);
                obj = sReader.Deserialize(reader);
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, "");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return obj as StationModel.StationModel1;
        }

        #endregion

        #region ---   求解计算

        private void buttonSolve_Click(object sender, System.EventArgs e)
        {
            Solver solver = new Solver(@"C:\Users\zengfy\Desktop\AbaqusScriptTest\run.cmd");
            solver.Execute();
        }

        #endregion
    }
}