using System;
using System.Windows.Forms;
using SDSS.Project;
using SDSS.Models;
using SDSS.Utility;

namespace SDSS.ModelForms
{
    /// <summary> 与具体模型相关的选项 </summary>
    internal partial class ModelOptions : Form
    {
        private readonly MainForm _mainForm;
        private readonly ModelBase _model;
        public string ModelWorkingDir { get; private set; }

        public ModelOptions(MainForm mainForm)
        {
            InitializeComponent();
            //
            _mainForm = mainForm;
            _model = mainForm.Model;
            RefreshModelOption(mainForm, mainForm.Model);
        }
        
        private void RefreshModelOption(MainForm mf,ModelBase mb)
        {
            textBox_WkDir.Text = mf.WorkingDir.WorkingDirectory;
            textBox_ModelName.Text = mb.ModelName;
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
                    MessageBox.Show(@"计算文件夹路径中不要出现中文", @"错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (string.IsNullOrEmpty(text) || sdUtils.StringHasNonEnglish(text))
            {
                MessageBox.Show(@"模型名称未指定，或者名称中包含非英文的字符！", @"提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _model.ModelName = text;

            //
            Close();
            DialogResult = DialogResult.OK;
        }
        
        private void textBox_WkDir_TextChanged(object sender, EventArgs e)
        {
            _mainForm.SetAbqWorkingDir(textBox_WkDir.Text);
        }

        private void ModelOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}