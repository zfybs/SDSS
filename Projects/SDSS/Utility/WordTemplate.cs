using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SDSS.Project;

namespace SDSS.Utility
{
    /// <summary> word 报告模板类型</summary>
    public enum WordTemplateType
    {
        Reporter_Casual = 0,
        Reporter_Official = 1
    }

    /// <summary> Word 中的样式名称 </summary>
    public static class WordTemplates
    {
        private const string Reporter_Casual = "Reporter_Casual.dotm";
        private const string Reporter_Official = "Reporter_Official.dotm";
        /// <summary> 根据 word 模板类型返回对应的模板文件路径 </summary>
        public static string GetTemplatePath(WordTemplateType template)
        {
            switch (template)
            {
                case WordTemplateType.Reporter_Casual: return Path.Combine(ProjectPaths.D_WordTemplate, Reporter_Casual);
                case WordTemplateType.Reporter_Official: return Path.Combine(ProjectPaths.D_WordTemplate, Reporter_Official);
                default: return Path.Combine(ProjectPaths.D_WordTemplate, Reporter_Official);
            }
        }
    }
}