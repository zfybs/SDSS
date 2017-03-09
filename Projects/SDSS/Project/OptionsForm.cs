using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SDSS.Utility;
using SDSS.Solver;
using SDSS.StationModel;

namespace SDSS.Project
{
    public partial class OptionsForm : Form
    {

        public OptionsForm()
        {
            InitializeComponent();
            comboBox_GUI.DataSource = Enum.GetValues(typeof(SolverGUI));
            comboBox_WT.DataSource = Enum.GetValues(typeof(WordTemplateType));
            this.comboBox_GUI.SelectedIndexChanged += new System.EventHandler(this.comboBox_GUI_SelectedIndexChanged);
            this.comboBox_WT.SelectedIndexChanged += new System.EventHandler(this.comboBox_WT_SelectedIndexChanged); ;
            refeshOF();
        }
        //将存储在硬盘中的数据反序列化到Options类中，刷新到OptionsForm中
        private void refeshOF()
        {
            textBox_File.Text = Options.DefaultAbqWorkingDir;
            textBox_WaitingSec.Text = Options.WaitingSeconds.ToString();
            comboBox_GUI.SelectedItem = Options.SolverGUI;
            comboBox_WT.SelectedItem = Options.WordTemplate;
            checkBox_Report.Checked = Options.DirectlyReport;
        }

        #region---   选择Abaqus工作文件夹

        private void button_File_Click(object sender, EventArgs e)
        {
            string rootDir = textBox_File.Text;
            if (!Directory.Exists(rootDir))
            {
                rootDir = Options.DefaultAbqWorkingDir;
            }
            FolderBrowserDialog open = new FolderBrowserDialog();
            open.ShowNewFolderButton = true;
            open.Description = @"Abaqus 的计算文件夹（路径中不要出现中文）";
            open.SelectedPath = rootDir;
            if (open.ShowDialog() == DialogResult.OK)
            {
                string t = open.SelectedPath;
                checkWorkingDir(t);
            }

        }
        //判断选择的文件夹是否含有中文
        private void checkWorkingDir(string str)
        {
            if (sdUtils.StringHasNonEnglish(str))
            {
                MessageBox.Show("计算文件夹路径中不要出现中文", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox_File.Text = str;
            }

        }
        private void textBox_File_TextChanged(object sender, EventArgs e)
        {
            Options.DefaultAbqWorkingDir = textBox_File.Text;
        }
        #endregion

        #region---   选择是否直接输出报告
        private void checkBox_Report_CheckedChanged(object sender, EventArgs e)
        {
            Options.DirectlyReport = checkBox_Report.Checked;
        }
        #endregion

        #region---   设置计算等待时间
        private void textBox_WaitingSec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox_WaitingSec.Text != null)
                {
                    Options.WaitingSeconds = uint.Parse(textBox_WaitingSec.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入计算等待时间", "提示");
            }
        }
        //保证只能输入数字，并且第一个数字不为零
        private void textBox_WaitingSec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8)
            {
                int len = textBox_WaitingSec.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < 48 || e.KeyChar > 57))
                { e.Handled = true; }
            }
        }
        #endregion

        #region---   设置Abaqus显示方式
        private void comboBox_GUI_SelectedIndexChanged(object sender, EventArgs e)
        {
            Options.SolverGUI = (SolverGUI)comboBox_GUI.SelectedItem;
        }
        #endregion

        #region---   设置报告模板
        private void comboBox_WT_SelectedIndexChanged(object sender, EventArgs e)
        {
            Options.WordTemplate = (WordTemplateType)comboBox_WT.SelectedItem;
        }
        #endregion


        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }


    }
}
