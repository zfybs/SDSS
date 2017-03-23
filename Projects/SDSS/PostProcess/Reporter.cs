using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using eZstd.Miscellaneous;
using Microsoft.Office.Interop.Word;
using SDSS.Models;
using SDSS.Project;
using SDSS.Utility;
using CheckBox = Microsoft.Office.Interop.Word.CheckBox;

namespace SDSS.PostProcess
{
    internal class Reporter : WordWriter
    {
        public readonly ModelBase Model;
        public int ContentEnd { get { return Content.End - 1; } }

        /// <summary>构造函数</summary>
        /// <param name="visible"> Word 进程是否可见 </param>
        /// <param name="model">  </param>
        /// <param name="openWordSucceeded"> Word 进程的打开是否成功 </param>
        public Reporter(ModelBase model, bool visible, ref bool openWordSucceeded) : base(visible, ref openWordSucceeded)
        {
            Model = model;
        }

        /// <summary>
        /// 获取一个文档，如果当前还没有打开其他文档，则创建一个新的；而如果当前打开的文档的模板与指定模板不同，也打开一个新的。
        /// </summary>
        /// <param name="wordTemplate"> word 模块的绝对路径，空则表示默认的 Normal 模板。</param>
        /// <returns>如果执行成功，则返回 true </returns>
        public bool OpenDocument(string wordTemplate)
        {
            if (Document != null)
            {
                string oldT = Document.get_AttachedTemplate().FullName;
                var newT = wordTemplate;
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
        /// <param name="mb">  </param>
        /// <param name="result">要输出到 word 报告中的结果数据</param>
        /// <param name="exportedItems"> 要导出的那些结果项 </param>
        /// <param name="errorMessage"> 如果在撰写报告的过程中出错，则对应了出错的信息 </param>
        public void WriteContents(ModelBase mb, Result result, AnsysWorkingDir workDir,
            List<ResultFileItem> exportedItems, ref StringBuilder errorMessage)
        {
            // 具体进行报告的撰写

            WriteCheckedResultItems(exportedItems);
            SetVisibility(true);

            InsertPictures(workDir);
            SetVisibility(true);
        }

        /// <summary> 只作测试，最后删除 </summary> 
        private void WriteCheckedResultItems(List<ResultFileItem> exportedItems)
        {
            var endPosi = ContentEnd;
            Range rg;
            foreach (ResultFileItem item in exportedItems)
            {
                if (item != null)
                {
                    rg = InsertParagrph(endPosi, item.Name, style: WordStyle.Title2);
                    endPosi = rg.End;
                    if (!string.IsNullOrEmpty(item.Description))
                    {
                        rg = InsertParagrph(endPosi, item.Description, style: WordStyle.Content);
                        endPosi = rg.End;
                    }
                    var v = item.GetValueString();

                    if (item.ValueType == ResultValueType.Array2D)
                    {
                        var arr = item.Value as double[,];
                        Table tb = InsertTable(Document, rg.End, data: v,
                            rows: arr.GetLength(0), columns: arr.GetLength(1));
                        endPosi = tb.Range.End;
                    }
                    else
                    {
                        rg = InsertParagrph(endPosi, v, style: WordStyle.Content);
                        endPosi = rg.End;
                    }
                }
            }
        }

        /// <summary> 只作测试，最后删除 </summary> 
        private void InsertPictures(AnsysWorkingDir wkDir)
        {
            var d = new DirectoryInfo(wkDir.WorkingDirectory);
            string modelName = Model.ModelName;
            string pattern = modelName + "-(.+).png";
            var rg = Content;
            rg.Collapse(WdCollapseDirection.wdCollapseEnd);
            //
            rg = InsertParagrph(rg.End, "计算结果图", style: WordStyle.Title2);
            foreach (var f in d.GetFiles("*.png"))
            {
                var reg = new Regex(pattern);
                var m = reg.Match(f.Name);
                string description;
                if (m.Success)
                {
                    var tp = OutputField.GetOutputField(m.Groups[1].Value);
                    if (tp != null)
                    {
                        description = OutputField.GetOutputFieldDescription(tp.Value);
                        switch (tp.Value)
                        {
                            default:
                                var shp = InsertPicture(rg.End, f.FullName, width: 400, height: 200);
                                rg = shp.Range;
                                rg = InsertParagrph(rg.End);
                                rg = InsertParagrph(rg.End, description, WordStyle.Caption_Pic);
                                break;
                        }
                    }
                }
            }
        }

        #endregion

    }
}
