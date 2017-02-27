using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using eZstd.Miscellaneous;
using SDSS.Definitions;
using SDSS.StationModel;
using SDSS.UIControls;
using SDSS.Utility;

namespace SDSS
{
    partial class EnterSplash : Form
    {
        private Color _originalBackColor;
        private ModelType _modelType;
        private CalculationMethod _calculationMethod;

        private string _abqWorkingDir;
        private string AbqWorkingDir
        {
            get { return _abqWorkingDir; }
            set
            {
                if (Directory.Exists(value))
                {
                    ProjectPaths.SetAbaqusWorkingDir(value);
                }
                _abqWorkingDir = value;
            }
        }

        #region ---   窗口的打开与关闭

        public EnterSplash()
        {
            InitializeComponent();

            // 模型类型的绑定
            pictureBox_Frame.Tag = ModelType.Frame;
            pictureBox2.Tag = ModelType.Model2;
            pictureBox3.Tag = ModelType.Model3;

            // 计算方法的绑定
            radioButton1.Tag = CalculationMethod.InertialForce;
            radioButton2.Tag = CalculationMethod.FanYingWeiYi;
            radioButton3.Tag = CalculationMethod.Method3;
            radioButton4.Tag = CalculationMethod.Method4;
            //
            _originalBackColor = pictureBox_Frame.BackColor;

            //
            radioButton1.Checked = true;
            pictureBoxes_Click(pictureBox_Frame, null);

            // 将 settings 中的数据刷新到 界面中
            var s = new Properties.Settings();
            textBox1.Text = s.AbaWorkingDir;
            textBox2.Text = s.ModelName;
        }

        private void AboutBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void EnterSplash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { Close(); }
        }

        private void EnterSplash_FormClosing(object sender, FormClosingEventArgs e)
        {
            var s = new Properties.Settings();
            s.AbaWorkingDir = textBox1.Text;
            s.ModelName = textBox2.Text;
            s.Save();
        }

        #endregion

        #region ---   pictureBox 的选择与取消选择

        private void pictureBoxes_Click(object sender, EventArgs e)
        {
            PictureBox p0 = sender as PictureBox;

            var pictures = tableLayoutPanel_ModelType.Controls;
            foreach (PictureBox p in pictures)
            {
                if (p.Equals(p0))
                {
                    PictureboxCheck(p);
                }
                else
                {
                    PictureboxUncheck(p);
                }
            }
        }
        private void PictureboxCheck(PictureBox p)
        {
            p.BackColor = Color.Red;
            _modelType = (ModelType)p.Tag;
        }
        private void PictureboxUncheck(PictureBox p)
        {
            p.BackColor = _originalBackColor;
        }
        #endregion

        #region ---   radioButton 的选择与取消选择

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RadioButton r in tableLayoutPanel_Method.Controls)
            {
                if (r.Checked)
                {
                    _calculationMethod = (CalculationMethod)r.Tag;
                    break;
                }
            }
        }

        #endregion

        private void button_chooseDir_Click(object sender, EventArgs e)
        {

            string rootDir = textBox1.Text;
            if (!Directory.Exists(rootDir))
            {
                rootDir = "";
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog()
            {
                SelectedPath = rootDir,
                ShowNewFolderButton = true,
                Description = @"Abaqus 的计算文件夹（路径中不要出现中文）",
            };
            string strPath;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                strPath = fbd.SelectedPath;
                if (strPath.Length > 0)
                {
                    textBox1.Text = strPath;
                }
            }
        }

        #region ---   开始计算

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            if (string.IsNullOrEmpty(text) || Utils.StringHasNonEnglish(text))
            {
                MessageBox.Show(@"模型名称未指定，或者名称中包含非英文的字符！", @"提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string modelName = text;
            AbqWorkingDir = textBox1.Text;
            this.Hide();
            //
            MainForm mf = GetMainForm(modelName);
            mf.ShowDialog();

            // 关闭整个程序
            this.Close();
        }

        private MainForm GetMainForm(string modelName)
        {
            switch (_modelType)
            {
                case ModelType.Frame: break;
                case ModelType.Model2: break;
            }
            switch (_calculationMethod)
            {
                case CalculationMethod.InertialForce: break;
            }

            var stationModel = ConstructStationModel();
            stationModel.ModelName = modelName;
            var mf = new MainForm(stationModel as StationModel1);

            return mf;
        }

        private StationModel.StationModel ConstructStationModel()
        {
            var sm = StationModel1.GetUniqueInstance() as StationModel1;
            Program.ConstructStationModel(sm);
            //ImExportModel(sm);

            return sm;
        }

        #endregion


    }
}