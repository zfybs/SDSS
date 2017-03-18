using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SDSS.Definitions;
using SDSS.Project;
using SDSS.Properties;
using SDSS.Models;
using SDSS.ModelForms;
using SDSS.Utility;

namespace SDSS
{
    partial class EnterSplash : Form
    {
        private Color _originalBackColor;
        private ModelType _modelType;
        private CalculationMethod _calculationMethod;

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
            var s = new Settings();
            textBox1.Text = s.AbaWorkingDir;
            textBox2.Text = s.ModelName;
        }

        private void AboutBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void EnterSplash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void EnterSplash_FormClosing(object sender, FormClosingEventArgs e)
        {
            var s = new Settings();
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
            // 模型名称
            string text = textBox2.Text;
            if (string.IsNullOrEmpty(text) || sdUtils.StringHasNonEnglish(text))
            {
                MessageBox.Show(@"模型名称未指定，或者名称中包含非英文的字符！", @"提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string modelName = text;



            // Abaqus 计算文件夹
            string abqWkDir = Options.DefaultAbqWorkingDir;
            text = textBox1.Text;
            if (!string.IsNullOrEmpty(text))
            {
                if (sdUtils.StringHasNonEnglish(text))
                {
                    MessageBox.Show(@"模型名称未指定，或者名称中包含非英文的字符！", @"提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (!Directory.Exists(text))
                {
                    var res = MessageBox.Show(@"指定的 Abaqus 计算文件夹不存在，是否创建此文件夹？", @"提示",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        try
                        {
                            Directory.CreateDirectory(text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(@"创建 Abaqus 计算文件夹失败\r\n" + ex.Message, @"出错",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                {
                    abqWkDir = text;
                }
            }
            Hide();
            //
            var mf = GetMainFormMdi(modelName, abqWkDir);
            mf.ModelForm.SetAbqWorkingDir(abqWkDir);
            mf.ShowDialog();

            // 关闭整个程序
            Close();
        }
        
        private MainFormMdi GetMainFormMdi(string modelName, string abqWkDir)
        {
            var mfm = new MainFormMdi(_modelType, _calculationMethod, modelName);
            // mfm.ActiveMdiChild.Text = modelName;
            return mfm;
        }

        #endregion
    }
}