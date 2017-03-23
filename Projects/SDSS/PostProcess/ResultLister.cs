using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using eZstd.Miscellaneous;
using Microsoft.Office.Interop.Word;
using SDSS.Project;
using SDSS.Models;
using SDSS.Utility;
using CheckBox = System.Windows.Forms.CheckBox;
using Options = SDSS.Project.Options;
using System.Collections.Generic;

namespace SDSS.PostProcess
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class ResultLister : Form
    {
        public readonly ModelBase Model;
        private readonly Result _result;
        private readonly AnsysWorkingDir _workingDir;
        private Reporter _reporter;

        /// <summary> 构造函数 </summary>
        /// <param name="result"></param>
        public ResultLister(ModelBase model, Result result, AnsysWorkingDir workingDir)
        {
            InitializeComponent();
            //
            Model = model;
            _result = result;
            _workingDir = workingDir;
            //
            ShowResultItems(result);
        }

        #region --- 将计算结果显示到窗口界面上

        private void ShowResultItems(Result res)
        {
            var values = res.Items.Values.ToList();
            float maxWidth = 0;  // 所有的 CheckBox 的最大宽度，用来将这些 CheckBox 进行对齐
            //
            //flowLayoutPanel_Items.Controls.Clear();
            foreach (ResultFileItem item in values)
            {
                CheckBox cb = new CheckBox()
                {
                    Text = item.Name,
                    Tag = item,
                    Checked = false,
                    // BackColor = Color.CadetBlue,
                };

                // 为控件添加提示文字
                string tip = null;
                if (!string.IsNullOrEmpty(item.Description))
                {
                    tip = item.Description + "\r\n";
                }
                tip += item.GetValueString();
                toolTip1.SetToolTip(cb, tip);
                //
                maxWidth = Math.Max(maxWidth, cb.CreateGraphics().MeasureString(item.Name, cb.Font).Width);
                //
                flowLayoutPanel_Items.Controls.Add(cb);
            }
            // 设置控件的大小
            if (flowLayoutPanel_Items.Controls.Count > 0)
            {
                maxWidth = maxWidth + 20; // 扩展的 20 像素表示 CheckBox 控件的勾选区的宽度
                int h = flowLayoutPanel_Items.Controls[0].Size.Height;
                foreach (Control c in flowLayoutPanel_Items.Controls)
                {
                    c.Size = new Size((int)maxWidth, h);
                }
            }
        }

        #endregion

        #region ---   界面操作

        private void checkBox_ChooseAll_CheckedChanged(object sender, EventArgs e)
        {
            bool @checked = checkBox_ChooseAll.Checked;
            foreach (Control cont in flowLayoutPanel_Items.Controls)
            {
                var chkBox = cont as CheckBox;
                chkBox.Checked = @checked;
            }
        }

        #endregion

        #region --- 撰写报告

        /// <summary> 撰写报告 </summary>
        private void button_WriteReport_Click(object sender, EventArgs e)
        {
            if (_result != null && !_bgw_Report.IsBusy)
            {
                // Start the asynchronous operation.
                _bgw_Report.RunWorkerAsync(argument: _result);
            }
        }

        private void _bgw_Report_DoWork(object sender, DoWorkEventArgs e)
        {
            var sb = new StringBuilder();
            try
            {
                bool succ = OpenWordReporter(ref sb, out _reporter);
                if (succ)
                {
                    // 具体进行报告的撰写
                    var items = new List<ResultFileItem>();
                    foreach (Control cont in flowLayoutPanel_Items.Controls)
                    {
                        var chkBox = cont as System.Windows.Forms.CheckBox;
                        if (chkBox.Checked)
                        {
                            items.Add(chkBox.Tag as ResultFileItem);
                        }
                    }
                    _reporter.WriteContents(Model, _result, _workingDir, items, errorMessage: ref sb);
                    _reporter.SetVisibility(true);
                }
                else
                {
                    throw new InvalidOperationException(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, sb.ToString(), "撰写报告时出错");
            }
        }

        /// <summary> 打开 Word 进程并创建一个 Document </summary>
        /// <param name="errorMessage"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        private bool OpenWordReporter(ref StringBuilder errorMessage, out Reporter reporter)
        {
            reporter = null;
            bool succ = false;
            try
            {
                reporter = new Reporter(model: Model, visible: false, openWordSucceeded: ref succ);
            }
            catch (Exception)
            {
                succ = false;
                errorMessage.AppendLine("Word 进程打开异常");
            }
            if (succ)
            {
                succ = reporter.OpenDocument(wordTemplate: Options.WordTemplate);
            }
            return succ;
        }

        private void _bgw_Report_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        /// <summary> 撰写报告结束，进行最后的处理与用户提示 </summary>
        private void _bgw_Report_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(@"撰写报告完成", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}