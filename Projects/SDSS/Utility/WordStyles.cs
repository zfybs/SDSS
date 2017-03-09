using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace SDSS.Utility
{
    /// <summary> Word 中的样式枚举 </summary>
    public enum WordStyle
    {
        /// <summary> 跟随其前面的样式 </summary>
        Follow = 0,
        /// <summary> 正文 </summary>
        Content = 1,
        Title = 2,
        Title1 = 3,
        Title2 = 4,
        Title3 = 5,
        Title4 = 6,
        Picture = 7,
        Caption_Pic = 8,
        Table = 9,
    }

    /// <summary> Word 中的样式名称 </summary>
    public static class WordStyles
    {
        private const string Content = "正文";
        private const string Title = "标题";
        private const string Title1 = "标题 1";
        private const string Title2 = "标题 2";
        private const string Title3 = "标题 3";
        private const string Title4 = "标题 4";
        private const string Picture = "图片";
        private const string Caption_Pic = "图片题注";
        private const string Table = "网格型";

        private static string GetStyleName(WordStyle style)
        {
            switch (style)
            {
                case WordStyle.Content: return Content;
                case WordStyle.Title: return Title;
                case WordStyle.Title1: return Title1;
                case WordStyle.Title2: return Title2;
                case WordStyle.Title3: return Title3;
                case WordStyle.Title4: return Title4;
                case WordStyle.Picture: return Picture;
                case WordStyle.Caption_Pic: return Caption_Pic;
                case WordStyle.Table: return Table;
                default: return Content;
            }
        }
        
        /// <summary> 为指定的范围设置样式 </summary>
        public static void SetStyle(Range rg, WordStyle style)
        {
            rg.set_Style(GetStyleName(style));
        }

        /// <summary> 为指定的范围设置样式 </summary>
        public static void SetStyle(Table table, WordStyle style)
        {
            table.set_Style(GetStyleName(style));
        }
    }
}
