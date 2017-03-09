using SDSS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDSS.Project;

namespace SDSS.StationModel
{

    public partial class ModelOptions : Form
    {

        private StationModel _sm;
        public string ModelWorkingDir { get; private set; }

        public ModelOptions(StationModel sm)
        {
            InitializeComponent();
            //
            _sm = sm;
            RefreshMO(sm);
        }

        private void RefreshMO(StationModel sm)
        {
            textBox_WkDir.Text = ProjectPaths.D_AbaqusWorkingDir;
            textBox_ModelName.Text = sm.ModelName;
        }

        private void button_WkDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog()
            {
                Description = @"选择Abaqus工作文件夹（路径中不能出现中文）",
                ShowNewFolderButton = true,
                SelectedPath = textBox_WkDir.Text,
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                string str = open.SelectedPath;
                if (sdUtils.StringHasNonEnglish(str))
                {
                    MessageBox.Show("计算文件夹路径中不要出现中文", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    textBox_WkDir.Text = str;
                    ModelWorkingDir = str;
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // validate the ui values
            string text = textBox_ModelName.Text;
            if (string.IsNullOrEmpty(text) || Utility.sdUtils.StringHasNonEnglish(text))
            {
                MessageBox.Show(@"模型名称未指定，或者名称中包含非英文的字符！", @"提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _sm.ModelName = text;

            //
            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void ModelOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            this.Hide();
        }

        private void textBox_WkDir_TextChanged(object sender, EventArgs e)
        {
            ProjectPaths.SetAbaqusWorkingDir(textBox_WkDir.Text);
        }

        private void ModelOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
