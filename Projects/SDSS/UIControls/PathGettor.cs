using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SDSS.Utility;

namespace SDSS.UIControls
{
    /// <summary> 专门用来选择文件或者文件夹的窗体 </summary>
    public partial class PathGettor : Form
    {
        #region ---   Fields

        private readonly bool _fileOrDir;
        private readonly bool _nonEnglistAllowed;
        private readonly string _selectedPath;
        private readonly string _title;

        /// <summary> 文件过滤规则，比如 
        /// 材料库(*.txt)| *.txt
        /// Excel文件(*.xls; *.xlsx; *.xlsb)| *.xls; *.xlsx; *.xlsb
        /// </summary>
        public string Filter { get; set; }

        /// <summary> 是否支持多选 </summary>
        public bool Multiselect;

        /// <summary> 构造好的文件夹路径 </summary>
        public string ChoosedPath { get; private set; }
        
        #endregion

        #region --- 构造函数
        /// <summary> 构造函数 </summary>
        /// <param name="fileOrDir">true 表示选择 文件路径， false 表示选择 文件夹路径。 </param>
        /// <param name="title">窗体的标题</param>
        /// <param name="nonEnglistAllowed">路径是否可以包含非英文字符</param>
        /// <param name="selectedPath"> 默认打开文件夹选择器时，要定位到的文件夹路径 </param>
        /// <param name="tip"> 具体的提示性描述 </param>
        /// <param name="filter"> 文件过滤规则，比如 
        /// 材料库(*.txt)| *.txt
        /// Excel文件(*.xls; *.xlsx; *.xlsb)| *.xls; *.xlsx; *.xlsb </param>
        public PathGettor(bool fileOrDir, string title, bool nonEnglistAllowed, string selectedPath = null,
            string tip = null, string filter = null)
        {
            InitializeComponent();
            //
            _fileOrDir = fileOrDir;
            _nonEnglistAllowed = nonEnglistAllowed;
            Filter = filter;
            _selectedPath = selectedPath;
            textBox_Dir.Text = selectedPath;
            _title = title;
            Text = title;
            label_tip.Text = @"提示: " + tip;
        }

        /// <param name="fileOrDir">true 表示选择 文件路径， false 表示选择 文件夹路径。 </param>
        /// <param name="title">窗体的标题</param>
        /// <param name="nonEnglistAllowed">路径是否可以包含非英文字符</param>
        /// <param name="selectedPath"> 默认打开文件夹选择器时，要定位到的文件夹路径 </param>
        /// <param name="tip"> 具体的提示性描述 </param>
        /// <param name="filter"> 文件过滤规则，比如 
        /// 材料库(*.txt)| *.txt
        /// Excel文件(*.xls; *.xlsx; *.xlsb)| *.xls; *.xlsx; *.xlsb </param>
        private static DialogResult ShowDialog(bool fileOrDir, string title, bool nonEnglistAllowed, string selectedPath = null,
            string tip = null, string filter = null)
        {
            var pg = new PathGettor(fileOrDir, title, nonEnglistAllowed, selectedPath, tip, filter);
            return pg.ShowDialog();
        }

        #endregion

        private void button_Dir_Click(object sender, EventArgs e)
        {
            if (_fileOrDir)
            {
                ChooseFile();
            }
            else
            {
                ChooseDir();
            }
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            if (_fileOrDir)
            {
                OkFile();
            }
            else
            {
                OkDir();
            }
        }

        #region ---   文件 路径操作

        private void ChooseFile()
        {
            var p = sdUtils.ChooseOpenFile(title: _title, filter: Filter, multiselect: Multiselect);
            if (p != null)
            {
                var sb = new StringBuilder();
                if (ValidateFile(p, ref sb))
                {
                    textBox_Dir.Text = p;
                }
                else
                {
                    var res = MessageBox.Show("指定的文件路径无效！\r\n" + sb.ToString(),
                        @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OkFile()
        {
            var sb = new StringBuilder();
            string p = textBox_Dir.Text;
            if (ValidateFile(p, ref sb))
            {
                ChoosedPath = p;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                var res = MessageBox.Show("指定的文件路径无效！\r\n" + sb.ToString(),
                    @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateFile(string path, ref StringBuilder errMsg)
        {
            if (!_nonEnglistAllowed && sdUtils.StringHasNonEnglish(path))
            {
                errMsg.AppendLine("路径中不要出现中文");
                return false;
            }
            return true;
        }

        #endregion

        #region ---   文件夹路径操作

        private void ChooseDir()
        {
            FolderBrowserDialog open = new FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
                Description = _title,
                SelectedPath = _selectedPath,
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                var sb = new StringBuilder();
                string p = open.SelectedPath;
                if (ValidateDir(p, ref sb))
                {
                    textBox_Dir.Text = p;
                }
                else
                {
                    var res = MessageBox.Show("指定的文件夹路径无效！\r\n" + sb.ToString(),
                        @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OkDir()
        {
            var sb = new StringBuilder();
            string p = textBox_Dir.Text;
            if (ValidateDir(p, ref sb))
            {
                ChoosedPath = p;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                var res = MessageBox.Show("指定的文件夹路径无效！\r\n" + sb.ToString(),
                    @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateDir(string path, ref StringBuilder errMsg)
        {
            if (!_nonEnglistAllowed && sdUtils.StringHasNonEnglish(path))
            {
                errMsg.AppendLine("路径中不要出现中文");
                return false;
            }
            if (!Directory.Exists(path))
            {
                var res = MessageBox.Show(@"指定的文件夹不存在，是否创建新的文件夹？", @"提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        return true;
                    }
                    catch (Exception)
                    {
                        errMsg.AppendLine(@"创建文件夹失败");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}