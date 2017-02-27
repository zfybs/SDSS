using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using SDSS.Constants;

namespace SDSS
{
    public static class Utils
    {
        #region ---   文件的打开或保存
        // --------------------------------------------------------------------
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

        // --------------------------------------------------------------------
        /// <summary> 通过选择文件对话框选择要进行数据提取的 xml 文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据提取的 xml 文件的绝对路径 </returns>
        public static string ChooseOpenStationModel(string title)
        {
            string sss = FileExtensions.StationModel;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = title,
                CheckFileExists = true,
                AddExtension = true,
                Filter = $"车站模型(*{sss})| *{sss}",
                FilterIndex = 2,
                Multiselect = false,
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        /// <summary> 通过选择文件对话框选择要进行数据写入的 xml 文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据写入的 xml 文件的绝对路径 </returns>
        public static string ChooseSaveStationModel(string title)
        {
            string sss = FileExtensions.StationModel;
            SaveFileDialog ofd = new SaveFileDialog
            {
                Title = title,
                CheckFileExists = false,
                AddExtension = true,
                Filter = $"车站模型(*{sss})| *{sss}",
                FilterIndex = 2,  // 默认选择第2项。
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        // --------------------------------------------------------------------
        /// <summary> 通过选择文件对话框选择要进行数据提取的 xml 文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据提取的 xml 文件的绝对路径 </returns>
        public static string ChooseOpenMaterials(string title)
        {
            string sss = FileExtensions.Materials;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = title,
                CheckFileExists = true,
                AddExtension = true,
                Filter = $"材料库(*{sss})| *{sss}",
                FilterIndex = 2,
                Multiselect = false,
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        /// <summary> 通过选择文件对话框选择要进行数据写入的 xml 文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据写入的 xml 文件的绝对路径 </returns>
        public static string ChooseSaveMaterials(string title)
        {
            string sss = FileExtensions.Materials;
            SaveFileDialog ofd = new SaveFileDialog
            {
                Title = title,
                CheckFileExists = false,
                AddExtension = true,
                Filter = $"材料库(*{sss})| *{sss}",
                FilterIndex = 2,  // 默认选择第2项。
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        // --------------------------------------------------------------------
        /// <summary> 通过选择文件对话框选择要进行数据提取的 xml 文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据提取的 xml 文件的绝对路径 </returns>
        public static string ChooseOpenProfiles(string title)
        {
            string sss = FileExtensions.Profiles;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = title,
                CheckFileExists = true,
                AddExtension = true,
                Filter = $"截面库(*{sss})| *{sss}",
                FilterIndex = 2,
                Multiselect = false,
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        /// <summary> 通过选择文件对话框选择要进行数据写入的 xml 文件 </summary>
        /// <param name="title">对话框的标题</param>
        /// <returns> 要进行数据写入的 xml 文件的绝对路径 </returns>
        public static string ChooseSaveProfiles(string title)
        {
            string sss = FileExtensions.Profiles;
            SaveFileDialog ofd = new SaveFileDialog
            {
                Title = title,
                CheckFileExists = false,
                AddExtension = true,
                Filter = $"截面库(*{sss})| *{sss}",
                FilterIndex = 2,  // 默认选择第2项。
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName.Length > 0 ? ofd.FileName : "";
            }
            return "";
        }

        // --------------------------------------------------------------------
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

        #endregion

        #region ---   xml 文件的导入导出

        ///<summary>从 xml 文件中导入对象 </summary>
        /// <param name="filePath">此路径必须为一个有效的路径</param>
        /// <param name="importedType">要导入的类型</param>
        /// <param name="succeeded">  </param>
        /// <param name="errorMessage">要导入的类型</param>
        public static object ImportFromXml(string filePath, Type importedType, out bool succeeded, ref StringBuilder errorMessage)
        {
            FileStream reader = null;
            object obj = null;
            try
            {
                // 
                reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                XmlSerializer sReader = new XmlSerializer(importedType);
                obj = sReader.Deserialize(reader);
                //
                errorMessage.AppendLine("成功将 xml 文件中的对象进行导入");
                succeeded = true;
            }
            catch (Exception ex)
            {
                errorMessage.AppendLine("将 xml 文件中的对象进行导入时出错" + "\r\n" + ex.Message);
                succeeded = false;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return obj;
        }

        /// <summary>
        /// 将C#中的可序列化对象写入到 xml 文件中
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="src">要导出的数据源</param>
        /// <param name="errorMessage"></param>
        /// <returns>如果成功写入，则返回 true，如果失败则返回 false。</returns>
        public static bool ExportToXmlFile(string xmlFilePath, object src, ref StringBuilder errorMessage)
        {
            StreamWriter fs = null;
            try
            {
                Type tp = src.GetType();

                fs = new StreamWriter(xmlFilePath, append: false);
                XmlSerializer s = new XmlSerializer(tp);
                s.Serialize(fs, src);
                //
                errorMessage.AppendLine("成功将数据导出为 xml 文件");
                return true;
            }
            catch (Exception ex)
            {
                errorMessage.AppendLine("数据写入 xml 文件失败" + "\r\n" + ex.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        #endregion

        /// <summary> 指定的字符串中是否包含有非英文字符 </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StringHasNonEnglish(string str)
        {
            // 1、用ASCII码判断：在 ASCII码表中，英文的范围是0 - 127，而汉字则是大于127。
            return str.Any(t => (int)t > 127);
        }
    }
}
