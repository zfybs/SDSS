using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using eZstd.Miscellaneous;
using Microsoft.Office.Interop.Word;
using SDSS.Project;
using SDSS.Utility;
using CheckBox = System.Windows.Forms.CheckBox;
using Options = SDSS.Project.Options;

namespace SDSS.PostProcess
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class ResultLister : Form
    {
        private readonly Result _result;
        private Reporter _reporter;

        /// <summary> 构造函数 </summary>
        /// <param name="result"></param>
        public ResultLister(Result result)
        {
            InitializeComponent();
            //
            _result = result;
            //
            ShowResultItems(result);
        }

        #region --- 将计算结果显示到窗口界面上

        private void ShowResultItems(Result res)
        {
            var values = res.Items.Values.ToList();
            float maxWidth = 0;
            //
            //flowLayoutPanel_Items.Controls.Clear();
            foreach (ResultItem item in values)
            {
                CheckBox cb = new CheckBox()
                {
                    Text = item.Name,
                    Tag = item,
                    Checked = false,
                    // BackColor = Color.CadetBlue,
                };

                // 为控件添加提示文字
                var tip = item.Description + "\r\n" + item.GetValueString();
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
                _bgw_Report.RunWorkerAsync(argument: null);
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
                    WriteCheckedResultItems();

                    return;
                    _reporter.WriteContents(_result, errorMessage: ref sb);
                    _reporter.SetVisibility(true);
                }
                else
                {
                    throw new InvalidOperationException();
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
        private static bool OpenWordReporter(ref StringBuilder errorMessage, out Reporter reporter)
        {
            reporter = null;
            bool succ = false;
            try
            {
                reporter = new Reporter(visible: true, openWordSucceeded: ref succ);
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

        /// <summary> 只作测试，最后删除 </summary> 
        private void WriteReport()
        {
            if (_result != null)
            {
                var sb = new StringBuilder();
                bool succ = OpenWordReporter(ref sb, out _reporter);

                if (succ)
                {
                    //

                    var sr = new StreamReader(ProjectPaths.F_AbqResult);
                    while (!sr.EndOfStream)
                    {
                        var s = sr.ReadLine();
                        if (s.StartsWith("T"))
                        {
                            _reporter.InsertParagrph(s, _reporter.ContentEnd, style: WordStyle.Title2);
                        }
                        else
                        {
                            _reporter.InsertParagrph(s, _reporter.ContentEnd, style: WordStyle.Content);
                        }
                    }

                    sr.Close();
                }
                _reporter.SetVisibility(true);
            }
        }

        /// <summary> 只作测试，最后删除 </summary> 
        private void WriteCheckedResultItems()
        {
            var endPosi = _reporter.ContentEnd;
            Range rg;
            foreach (Control cont in flowLayoutPanel_Items.Controls)
            {
                var chkBox = cont as CheckBox;
                if (chkBox.Checked)
                {
                    var item = chkBox.Tag as ResultItem;
                    if (item != null)
                    {
                        rg = _reporter.InsertParagrph(item.Name, endPosi, style: WordStyle.Title2);
                        endPosi = rg.End;
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            rg = _reporter.InsertParagrph(item.Description, endPosi, style: WordStyle.Content);
                            endPosi = rg.End;
                        }
                        var v = item.GetValueString();

                        if (item.ValueType == ResultValueType.Array2D)
                        {
                            var arr = item.Value as double[,];
                            Table tb = _reporter.InsertTable(_reporter.Document, rg.End, data: v,
                                rows: arr.GetLength(0), columns: arr.GetLength(1));
                            endPosi = tb.Range.End;
                        }
                        else
                        {
                            rg = _reporter.InsertParagrph(v, endPosi, style: WordStyle.Content);
                            endPosi = rg.End;
                        }
                    }
                }
            }
        }

        private void _bgw_Report_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void _bgw_Report_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        #endregion
    }
}