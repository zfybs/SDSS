using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SocketedShafts.Entities;
namespace SocketedShafts
{
    public static class Utils
    {
        /// <summary> 通过选择文件对话框选择要进行数据提取的Excel文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据提取的Excel文件的绝对路径 </returns>
        public static string ChooseOpenExcel(string title)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = title,
                CheckFileExists = true,
                AddExtension = true,
                Filter = @"Excel文件(*.xls; *.xlsx; *.xlsb)| *.xls; *.xlsx; *.xlsb",
                FilterIndex = 2,
                Multiselect = false,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        /// <summary> 通过选择文件对话框选择要进行数据写入的Excel文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据写入的Excel文件的绝对路径 </returns>
        public static string ChooseSaveExcel(string title)
        {
            SaveFileDialog ofd = new SaveFileDialog
            {
                Title = title,
                CheckFileExists = false,
                AddExtension = true,
                Filter = @" Excel工作簿(*.xlsx)|*.xlsx| Excel二进制工作簿(*.xlsb) |*.xlsb| Excel 97-2003 工作簿(*.xls)|*.xls",
                FilterIndex = 2,  // 默认选择第2项。
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }


        /// <summary> 通过选择文件对话框选择要进行数据提取的Excel文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据提取的Excel文件的绝对路径 </returns>
        public static string ChooseOpenSSS(string title)
        {
            string sss = SocketedShaftSystem.FileExtension;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = title,
                CheckFileExists = true,
                AddExtension = true,
                Filter = $"嵌岩桩(*{sss})| *{sss}",
                FilterIndex = 2,
                Multiselect = false,
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        /// <summary> 通过选择文件对话框选择要进行数据写入的Excel文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据写入的Excel文件的绝对路径 </returns>
        public static string ChooseSaveSSS(string title)
        {
            string sss = SocketedShaftSystem.FileExtension;
            SaveFileDialog ofd = new SaveFileDialog
            {
                Title = title,
                CheckFileExists = false,
                AddExtension = true,
                Filter = $"嵌岩桩(*{sss})| *{sss}",
                FilterIndex = 2,  // 默认选择第2项。
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        /// <summary> 通过选择文件对话框选择要进行数据写入的Excel文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据写入的Excel文件的绝对路径 </returns>
        public static string ChooseSaveEmf(string title)
        {
            string sss = ".emf";
            SaveFileDialog ofd = new SaveFileDialog
            {
                Title = title,
                CheckFileExists = false,
                AddExtension = true,
                Filter = $"Windows 增强型图元文件 (*{sss})| *{sss}",
                FilterIndex = 2,  // 默认选择第2项。
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }
    }
}
