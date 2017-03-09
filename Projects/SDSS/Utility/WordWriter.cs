using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using SDSS.StationModel;
using Application = Microsoft.Office.Interop.Word.Application;
using SDSS.Utility;

namespace SDSS.Utility
{
    internal class WordWriter : IDisposable
    {
        ///// <summary> 当前整个系统中关联在一起的 word 进程 </summary>
        //public static Application[] OpenedApplications { get; private set; }

        public Document Document { get; private set; }
        public Application App { get; private set; }
        /// <summary> 文本的正文部分，其 End 位置包含正文中最后一个换行符，所以必须将其值 -1 才是正文部分有效的 End。 </summary>
        public Range Content { get { return Document.Content; } }


        #region ---   构造函数

        /// <summary> 构造函数 </summary>
        /// <param name="visible"> Word 进程是否可见 </param>
        public WordWriter(bool visible, ref bool openWordSucceeded)
        {
            try
            {
                App = new Application
                {
                    Visible = visible
                };
                //
                openWordSucceeded = true;

            }
            catch (Exception)
            {
                openWordSucceeded = false;
                Dispose();
            }
        }

        ~WordWriter()
        {
            Dispose();
        }

        public void Dispose()
        {
            try
            {
                if (Document != null)
                {
                    Document.Close(SaveChanges: false);
                }
                if (App != null)
                {
                    App.Quit();
                }
            }
            catch (Exception)
            {

                // ignored;
            }

        }

        #endregion

        #region ---   Application 操作

        public void SetVisibility(bool visible)
        {
            App.Visible = visible;
        }

        #endregion

        #region ---   Document 操作


        /// <summary> 创建一个新的文档 </summary>
        /// <param name="wordTemplate"> word 模块的名称，空则表示默认的 Normal 模板。</param>
        public virtual bool NewDocument(WordTemplateType wordTemplate)
        {
            string templatePath = WordTemplates.GetTemplatePath(wordTemplate);
            if (!File.Exists(templatePath))
            {
                MessageBox.Show(@"用来撰写报告的 Word 模板文件不存在，请自行尝试找回，或者重新安装本软件。");
                return false;
            }
            try
            {
                if (Document != null)
                {
                    Document.Close(SaveChanges: false);
                }
                Document = App.Documents.Add(Template: templatePath);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Word 文档创建出错：" + ex.Message);
                return false;
            }
        }

        public void SaveDocument(string filePath)
        {
            if (Document != null)
            {
                Document.SaveAs(FileName: filePath);
            }
        }

        #endregion

        #region ---   Range 操作

        #endregion

        #region ---   插入内容的静态方法

        /// <summary> 在文档中插入一些内容，并且自动换行 </summary>
        /// <param name="position"> 文字从哪个位置插入</param>
        /// <param name="data"> 输入的数据 </param>
        /// <param name="style"> 新添加的内容的样式的名称 </param>
        /// <returns>返回的范围为新添加的内容所占的区域，与原内容无关</returns>
        public Range InsertParagrph(string data, int position, WordStyle style = WordStyle.Follow)
        {
            data = data + Constants.Word.CrLf;
            return InsertText(data, position, style);
        }

        /// <summary> 在文档中插入一些内容，但是不自动换行 </summary>
        /// <param name="position"> 文字从哪个位置插入</param>
        /// <param name="data"> 输入的数据 </param>
        /// <param name="style"> 新添加的内容的样式的名称 </param>
        /// <returns>返回的范围为新添加的内容所占的区域，与原内容无关</returns>
        public Range InsertText(string data, int position, WordStyle style = WordStyle.Follow)
        {
            var rg = Document.Range(position, position);

            rg.InsertAfter(data);

            if (style != WordStyle.Follow)
            {
                WordStyles.SetStyle(rg, style);
            }
            return rg;
        }


        /// <summary> 在文档中插入一个表格 </summary>
        /// <param name="startIndex">表格起始的位置</param>
        /// <param name="data"> 表格所对应的数据，包含表头 </param>
        public Table InsertTable(Document doc, int startIndex, string[,] data)
        {
            Range rg = doc.Range(Start: startIndex, End: startIndex);
            StringBuilder sb = new StringBuilder();
            int rows = data.GetLength(0);
            int columns = data.GetLength(1);
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    sb.Append(data[r, c] + Constants.Word.CrLf);
                }
            }
            //
            rg.Text = sb.ToString();
            Table tb = rg.ConvertToTable(Separator: WdTableFieldSeparator.wdSeparateByParagraphs,
                NumRows: rows, NumColumns: columns);
            return tb;
        }

        /// <summary> 在文档中插入一个表格 </summary>
        /// <param name="startIndex">表格起始的位置</param>
        /// <param name="data"> 表格所对应的数据，包含表头。同一行的元素之间用制表符分隔，多行之间用换行符分隔 </param>
        /// <param name="rows"> 表格的行数 </param>
        /// <param name="columns"> 表格的列数 </param>
        public Table InsertTable(Document doc, int startIndex, string data, int rows, int columns, WordStyle style = WordStyle.Table)
        {
            Range rg = doc.Range(Start: startIndex, End: startIndex);
            //
            rg.Text = data;
            Table tb = rg.ConvertToTable(Separator: WdTableFieldSeparator.wdSeparateByTabs,
                NumRows: rows, NumColumns: columns);
            WordStyles.SetStyle(tb, style);
            return tb;
        }

        /// <summary> 在文档中插入一张图片</summary>
        /// <param name="PP">图片的文件路径</param>
        /// <param name="PN">图片的名字</param>>
        /// <returns></returns>
        public Range InsertPicture(Range rg, string PP, string PN, float width, float height, bool? start = false, WordStyle style = WordStyle.Picture)
        {
            if (start == true)
            {
                rg = rg.Document.Range(Start: rg.Start, End: rg.Start);
            }
            else if (start == false)
            {
                rg = rg.Document.Range(Start: rg.End, End: rg.End);
            }

            InlineShape shape = rg.InlineShapes.AddPicture(PP);
            shape.Width = width;
            shape.Height = height;
            InsertText(Constants.Word.CrLf + PN, rg.End, WordStyle.Follow);
            return rg;
        }

        #endregion
    }
}
