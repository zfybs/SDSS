using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Project;

namespace SDSS.Utility
{
    public static class sdUtils
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

        #region ---   色彩的处理

        /// <summary> 根据经典的有限元插值色系作为基准来进行颜色的插值扩展 </summary>
        /// <param name="colorsCount">目标颜色集的个数</param>
        /// <returns>数组中元素的个数为colorsCount </returns>
        public static Color[] ClassicalColorsExpand(int colorsCount)
        {
            if (colorsCount <= 0) return new Color[0];

            // 定义6种经典的过渡颜色用于插值
            Color[] baseColors = new Color[6]
            {
                Color.FromArgb(255, 0,  0   ),
                Color.FromArgb(255 ,192,0   ),
                Color.FromArgb(255 ,255,0   ),
                Color.FromArgb(0,   176,80  ),
                Color.FromArgb(0,   112,192 ),
                Color.FromArgb(20,   50, 150  ),
            };

            return ColorExpand(baseColors, colorsCount);
        }

        /// <summary> 根据多个基准色平均插值出指定数量的渐变颜色集 </summary>
        /// <param name="baseColors">用来进行插值的基准色</param>
        /// <param name="colorsCount">目标颜色集的数量</param>
        /// <returns>数组中元素的个数为colorsCount</returns>
        public static Color[] ColorExpand(Color[] baseColors, int colorsCount)
        {
            if (colorsCount <= 0) throw new ArgumentException("进行颜色插值的目标个数至少为1");

            if (baseColors == null || baseColors.Length == 0) throw new ArgumentException("进行颜色插值的基准色至少要有一个");

            //
            int baseCount = baseColors.Length;  // 基准色的数量
            Color[] colors = new Color[colorsCount];  // 最后插值完成后的颜色集
            if (baseCount == 1)
            {
                for (int i = 0; i < colorsCount; i++)
                {
                    colors[i] = baseColors[0];
                }
            }
            else  // 基准色不只一个
            {
                // 开始插值
                if (colorsCount == 1)
                {
                    colors[0] = baseColors[0];
                    return colors;
                }
                // 当要插值的颜色多于1个时，即 colorsCount 至少为 2
                double interval = 1D / (colorsCount - 1);  // 全局中每个目标色之间的间隔
                for (int i = 0; i < colorsCount; i++)
                {
                    double interpRatio = interval * i; // interpRatio 的值处于[0,1]之间，0代表第一个基准色，1代表最后一个基准色

                    // 由 interpRatio 的值确定要在哪两个基准色之间进行插值
                    if (interpRatio == 0)
                    {
                        colors[i] = baseColors[0];
                        continue;
                    }
                    else if (interpRatio == 1)
                    {
                        colors[i] = baseColors[baseCount - 1];
                        continue;
                    }
                    else if (interpRatio > 0 && interpRatio < 1)
                    {
                        double baseInterval = 1D / (baseCount - 1);

                        int baseColor1 = (int)Math.Ceiling(interpRatio / baseInterval); // 第一个基准色
                        int baseColor2 = baseColor1 + 1;  // 第二个基准色
                                                          // 换算新的插值比例
                        var x0 = baseInterval * (baseColor1 - 1);
                        var x1 = baseInterval * (baseColor1);
                        var localInterpRatio = (interpRatio - x0) / (x1 - x0);

                        // 在两个基准颜色之间进行插值
                        colors[i] = ColorInterp(baseColors[baseColor1 - 1], baseColors[baseColor2 - 1], localInterpRatio);
                    }
                }
            }
            return colors;
        }

        /// <summary>
        /// RGB 颜色插值
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="x">x的值位于[0,1]的闭区间内，当其值为0时，它代表颜色c1，当其值为1时，它代表颜色c2</param>
        /// <returns>插值后的RGB颜色</returns>
        private static Color ColorInterp(Color c1, Color c2, double x)
        {
            // 插值
            var r = (byte)(c1.R + (c2.R - c1.R) * x);
            var g = (byte)(c1.G + (c2.G - c1.G) * x);
            var b = (byte)(c1.B + (c2.B - c1.B) * x);
            //
            return Color.FromArgb(r, g, b);
        }

        #endregion
    }
}
