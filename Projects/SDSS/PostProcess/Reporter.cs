using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using eZstd.Miscellaneous;
using Microsoft.Office.Interop.Word;
using SDSS.Utility;

namespace SDSS.PostProcess
{
    internal class Reporter : WordWriter
    {
        public int ContentEnd { get { return Content.End - 1; } }

        /// <param name="visible"> Word 进程是否可见 </param>
        /// <param name="openWordSucceeded"> Word 进程的打开是否成功 </param>
        public Reporter(bool visible, ref bool openWordSucceeded) : base(visible, ref openWordSucceeded)
        {
        }

        /// <summary>
        /// 获取一个文档，如果当前还没有打开其他文档，则创建一个新的；而如果当前打开的文档的模板与指定模板不同，也打开一个新的。
        /// </summary>
        /// <param name="wordTemplate"></param>
        /// <returns>如果执行成功，则返回 true </returns>
        public bool OpenDocument(WordTemplateType wordTemplate)
        {
            if (Document != null)
            {
                string oldT = Document.get_AttachedTemplate().FullName;
                var newT = WordTemplates.GetTemplatePath(wordTemplate);
                if (string.Compare(oldT, newT, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    return base.NewDocument(wordTemplate);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return base.NewDocument(wordTemplate);
            }
        }

        #region ---   撰写报告


        /// <summary> 在报告中写入内容、图片、公式等 </summary>
        /// <param name="result">要输出到 word 报告中的结果数据</param>
        /// <param name="errorMessage"> 如果在撰写报告的过程中出错，则对应了出错的信息 </param>
        public void WriteContents(Result result, ref StringBuilder errorMessage)
        {
            try
            {
                InsertParagrph(result.Name, ContentEnd, style: WordStyle.Content);
            }
            catch (Exception ex)
            {
                errorMessage.AppendLine(ex.Message);
            }

        }

        #endregion

    }
}
