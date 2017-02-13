using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using eZstd.Data;
using eZstd.Miscellaneous;
using SDSS.Definitions;
using SDSS.StationModel;
using SDSS.UIControls;

namespace SDSS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            var sm = StationModel1.GetUniqueInstance() as StationModel1;
            constructStationModel(sm);
            //ImExportModel(sm);

            //
            var mf = new MainForm(sm);
            Application.Run(mf);
        }

        private static void constructStationModel(StationModel1 sm)
        {
            sm.GenerateFrame(new double[] { 4, 5, 6, }, new double[] { 3, 6 });
            //
            Material mat = new Material("弹性", 1900, 200e9, 0.3, MaterialType.Elastic);
            sm.Definitions.Materials.Add(mat);
            mat = new MohrCoulomb("MC", 1900, 200e9, 0.3, 60e6, 30);
            sm.Definitions.Materials.Add(mat);
            //
            Profile rec = new Rectangular("深梁", 0.5, 1.5);
            sm.Definitions.Profiles.Add(rec);
            rec = new T("T形梁", 2, 1, 0.3);
            sm.Definitions.Profiles.Add(rec);
            //
            sm.Beams[0].Profile = rec;
            sm.Columns[0].Material = mat;

        }

        #region ---   模型文件的导入导出

        private static void ImExportModel(StationModel.StationModel sm)
        {
            XmlDictionary<string, int> d = new XmlDictionary<string, int>();
            d.Add("a", 10);
            d.Add("b", 20);
            d.Add("c", 30);
            
            string filePath = @"E:\GitHubProjects\SDSS\bin\m1.sdss";
            // ExportToXml(sm, filePath);
            ExportToXml(d, filePath);
            var sm1 = ImportFromXml(filePath, d.GetType());

            //var sm1 = ImportFromXml(filePath, typeof(StationModel1));
            // Debug.Print(sm1.Definitions.Materials.Count.ToString());
            //
        }

        /// <param name="stationModel">车站模型</param>
        /// <param name="filePath">此路径必须为一个有效的路径</param>
        private static void ExportToXml(object obj, string filePath)
        {
            FileStream fs = null;
            try
            {
                Type tp = obj.GetType();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                fs = new FileStream(filePath, FileMode.OpenOrCreate);
                XmlSerializer s = new XmlSerializer(tp);
                s.Serialize(fs, obj);
                fs.Close();
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, "");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <param name="stationModel">车站模型</param>
        /// <param name="filePath">此路径必须为一个有效的路径</param>
        private static object ImportFromXml(string filePath, Type rootType)
        {
            FileStream reader = null;
            object obj = null;
            try
            {
                //
                reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                XmlSerializer sReader = new XmlSerializer(rootType);
                obj = sReader.Deserialize(reader);
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, "");
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
        #endregion


    }
}