using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using eZstd.Data;
using SocketedShafts.Definitions;

namespace SocketedShafts.Entities
{
    /// <summary> 整个嵌岩桩模型 </summary>
    [Serializable()]
    public class SocketedShaftSystem
    {
        #region ---   Constants

        /// <summary> 整个模型系统参数文件的后缀名 </summary>
        public const string FileExtension = ".sss";

        #endregion

        #region ---   XmlIgnore Properties

        /// <summary> 整个系统中所有的土层定义 </summary>
        [XmlElement]
        public List<SoilLayer> SoilDefinitions { get; set; }

        /// <summary> 整个系统中所有的桩截面 </summary>
        [XmlElement]
        public List<ShaftSection> SectionDefinitions { get; set; }

        #endregion

        #region ---   XmlAttribute

        #endregion

        #region ---   XmlElement

        /// <summary> 与材料、水位标高等相关的系统参数 </summary>
        public SystemProperty SystemProperty { get; set; }


        /// <summary> 一根嵌岩桩 </summary>
        public SocketedShaft SocketedShaft { get; set; }

        /// <summary> 土层集合 </summary>
        public XmlList<SoilLayerEntity> SoilLayers { get; set; }

        #endregion

        #region ---   构造函数

        [XmlIgnore]
        private static SocketedShaftSystem _uiniqueInstance;
        public static SocketedShaftSystem GetUniqueInstance()
        {
            return _uiniqueInstance ?? (_uiniqueInstance = new SocketedShaftSystem());
        }

        public static void SetUniqueInstance(SocketedShaftSystem sss)
        {
            _uiniqueInstance = sss;
        }
        
        /// <summary> 构造函数 </summary>
        public SocketedShaftSystem()
        {

            //// 土层集合
            SoilLayers = new XmlList<SoilLayerEntity>();
            //SoilLayer sl = new SoilLayer()
            //{
            //    Name = "第一层土",
            //    CompressiveStrength = (float)2e7,
            //};
            //SoilLayer s2 = new SoilLayer()
            //{
            //    Name = "第二层土",
            //    CompressiveStrength = (float)123000,
            //};
            SoilDefinitions = new List<SoilLayer>();
            //SoilDefinitions.Add(sl);
            //SoilDefinitions.Add(s2);
            //SoilLayerEntity sle1 = new SoilLayerEntity()
            //{
            //    Bottom = 250,
            //    Top = 290,
            //    Layer = sl
            //};

            //SoilLayerEntity sle2 = new SoilLayerEntity()
            //{
            //    Bottom = 200,
            //    Top = 300,
            //    Layer = s2
            //};
            //SoilLayers.Add(sle1);
            //SoilLayers.Add(sle2);

            //// 嵌岩桩
            SocketedShaft = new SocketedShaft();

            //// 桩截面集合
            //ShaftSection ss1 = new ShaftSection() { Name = "截面1", BarsCount = 20, Diameter = 2 };
            //ShaftSection ss2 = new ShaftSection() { Name = "截面2", BarsCount = 35, Diameter = 1.5f };
            //SectionDefinitions = new List<ShaftSection> { ss1, ss2 };
            SectionDefinitions = new List<ShaftSection> ();
            ////
            //ShaftSectionEntity sse1 = new ShaftSectionEntity() { Top = 20, Bottom = 10, Section = ss1 };
            //ShaftSectionEntity sse2 = new ShaftSectionEntity() { Top = 10, Bottom = 5, Section = ss2 };
            //XmlList<ShaftSectionEntity> xl = new XmlList<ShaftSectionEntity>() { sse1, sse2 };
            //SocketedShaft.Sections = xl;

            ////
            SystemProperty = new SystemProperty();
            ////
            _uiniqueInstance = this;
        }

        #endregion
    }
}