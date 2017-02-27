using System;
using System.Collections.Generic;
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
        public Document Doc { get; private set; }
        public Application App { get; private set; }

        #region ---   构造函数

        public WordWriter()
        {
            App = new Application
            {
                Visible = false
            };
        }

        ~WordWriter()
        {
            Dispose();
        }

        public void Dispose()
        {
            try
            {
                if (Doc != null)
                {
                    Doc.Close();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templatePath"> word 模块的名称，空则表示默认的 Normal 模板。</param>
        public virtual bool NewDocument(string templatePath = null)
        {
            if (!File.Exists(templatePath))
            {
                templatePath = "";
            }
            try
            {
                if (Doc != null)
                {
                    Doc.Close(SaveChanges: false);
                }
                Doc = App.Documents.Add(Template: templatePath);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Word 文档创建出错：" + ex.Message);
                return false;
            }
        }

        public void SaveDocument(string filePath)
        {
            if (Doc != null)
            {
                Doc.SaveAs(FileName: filePath);
            }
        }

        #endregion

        #region ---   Range 操作

        public Range CollapsToEnd(Range rg = null)
        {
            if (rg == null)
            {
                rg = Doc.Content;
            }
            rg.Collapse(WdCollapseDirection.wdCollapseEnd);
            return rg;
        }

        #endregion

        #region ---   插入内容

        /// <summary> 在文档中插入一些内容，但是不自动换行 </summary>
        /// <param name="rg">  </param>
        /// <param name="start"> null 表示新的内容覆盖原Range范围，
        /// true表示新的内容插入在原内容之前，
        /// false表示新的内容插入在原内容之后</param>
        /// <param name="data"> 输入的数据 </param>
        /// <param name="style"> 新添加的内容的样式的名称 </param>
        /// <returns>返回的范围为新添加的内容所占的区域，与原内容无关</returns>
        public Range InsertParagrph(Range rg, string data, bool? start = false, WordStyle style = WordStyle.Follow)
        {
            return WordUtils.InsertParagrph(rg, data, start, style);
        }

        /// <summary> 在文档中插入一些内容，但是不自动换行 </summary>
        /// <param name="rg">  </param>
        /// <param name="start"> null 表示新的内容覆盖原Range范围，
        /// true表示新的内容插入在原内容之前，
        /// false表示新的内容插入在原内容之后</param>
        /// <param name="data"> 输入的数据 </param>
        /// <param name="style"> 新添加的内容的样式的名称 </param>
        /// <returns>返回的范围为新添加的内容所占的区域，与原内容无关</returns>
        public Range InsertText(Range rg, string data, bool? start = false, WordStyle style = WordStyle.Follow)
        {
            return WordUtils.InsertText(rg, data, start, style);
        }

        /// <summary> 在文档中插入一个表格 </summary>
        /// <param name="startIndex">表格起始的位置</param>
        /// <param name="data"> 表格所对应的数据，包含表头 </param>
        public Table InsertTable(Document doc, int startIndex, string[,] data)
        {
            return WordUtils.InsertTable(doc, startIndex, data);
        }

        #endregion
    }

}
