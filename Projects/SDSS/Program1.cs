using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Xml.Serialization;
using SDSS.Solver;

namespace SerializeCollection
{
    class Program1
    {

        private static readonly string[] sep = new string[] { "\r\n" };

        public static void Main(string[] args)
        {
            var dir = @"C:\Users\zengfy\Desktop\新建文件夹";
            dir = @"E:\GitHubProjects\SDSS\AbaqusSolver";

            var files = Directory.GetFiles(dir, "*.py", SearchOption.AllDirectories);
            foreach (string file in files)
            {

                FileInfo f = new FileInfo(file);
                var ac = f.GetAccessControl();
                f.SetAccessControl(ac);

                var contents = ReadOneFile(file);
                WriteOneFile(file, contents);
            }
        }

        static string[] ReadOneFile(string filePath)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            
            StreamReader reader = new StreamReader(filePath);
            string line = reader.ReadToEnd();
            var lines = line.Split(sep, StringSplitOptions.None);

            reader.Close();
            return lines;
        }

        static void WriteOneFile(string filePath, string[] contents)
        {
            StreamWriter sw = new StreamWriter(filePath, append: false);
            int l = 1;
            string newLine;
            foreach (var line in contents)
            {
                newLine = line;
                if (l < 30)
                {
                    if (line.StartsWith("from abaqus", StringComparison.Ordinal))
                    {
                        // 添加注释
                        newLine = "# " + line;
                    }
                    else if (line.StartsWith("# from abaqus", StringComparison.Ordinal))
                    {
                        // 解除注释
                        newLine = line.Substring(2);
                    }
                }
                sw.WriteLine(newLine);
            }

            sw.Close();
        }
    }
}