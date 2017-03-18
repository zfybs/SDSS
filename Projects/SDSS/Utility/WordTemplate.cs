using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SDSS.Project;
using System.ComponentModel;

namespace SDSS.Utility
{
    /// <summary> Word 中的样式名称 </summary>
    public static class WordTemplates
    {
        private static Dictionary<string, string> _dic;

        public static Dictionary<string, string> GetWordTemplateFiles(string path)
        {
            string[] fileNames = Directory.GetFiles(path, "*.dotm");
            _dic = new Dictionary<string, string>();
            foreach (string file in fileNames)
            {
                if (Path.GetFileNameWithoutExtension(file).Substring(0, 1) != "~")
                {
                    _dic.Add(Path.GetFileNameWithoutExtension(file), file);
                }
            }
            return _dic;
        }


        public static string GetTemplatePath(string fileName)
        {
            if (_dic.Keys.Contains(fileName))
            {
                return _dic[fileName];
            }
            else
            {
                return _dic.Values.ToArray()[0];
            }
        }
    }
}