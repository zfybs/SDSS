using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace SDSS.Utility
{
    /// <summary> Word 中的一些操作 </summary>
    public static class WordUtils
    {
        #region ---   插入内容

        /// <summary> 在文档中插入一些内容，但是不自动换行 </summary>
        /// <param name="rg">  </param>
        /// <param name="start"> null 表示新的内容覆盖原Range范围，
        /// true表示新的内容插入在原内容之前，
        /// false表示新的内容插入在原内容之后</param>
        /// <param name="data"> 输入的数据 </param>
        /// <param name="style"> 新添加的内容的样式的名称 </param>
        /// <returns>返回的范围为新添加的内容所占的区域，与原内容无关</returns>
        public static Range InsertParagrph(Range rg, string data, bool? start = false, WordStyle style = WordStyle.Follow)
        {
            data = data + Constants.Word.CrLf;
            return InsertText(rg, data, start, style);
        }

        /// <summary> 在文档中插入一些内容，但是不自动换行 </summary>
        /// <param name="rg">  </param>
        /// <param name="start"> null 表示新的内容覆盖原Range范围，
        /// true表示新的内容插入在原内容之前，
        /// false表示新的内容插入在原内容之后</param>
        /// <param name="data"> 输入的数据 </param>
        /// <param name="style"> 新添加的内容的样式的名称 </param>
        /// <returns>返回的范围为新添加的内容所占的区域，与原内容无关</returns>
        public static Range InsertText(Range rg, string data, bool? start = false, WordStyle style = WordStyle.Follow)
        {
            if (start == true)
            {
                rg = rg.Document.Range(Start: rg.Start, End: rg.Start);
            }
            else if (start == false)
            {
                rg = rg.Document.Range(Start: rg.End, End: rg.End);
            }

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
        public static Table InsertTable(Document doc, int startIndex, string[,] data)
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

        #endregion
    }
}
