using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using eZstd.Data;
using SDSS.Definitions;
using SDSS.Entities;

namespace SDSS.StationModel
{
    /// <summary> 所有车站模型的基类 </summary>
    [Serializable()]
    public abstract class StationModel
    {

        #region ---   XmlElement

        /// <summary> 整个系统中所有的材料定义 </summary>
        [XmlElement]
        public XmlList<Material> MaterialDefinitions { get; set; }

        /// <summary> 整个系统中所有的横截面定义 </summary>
        [XmlElement]
        public XmlList<Profile> ProfileDefinitions { get; set; }

        /// <summary> 与材料、水位标高等相关的系统参数 </summary>
        public SystemProperty SystemProperty { get; set; }

        /// <summary> 土层集合 </summary>
        public XmlList<SoilLayer> SoilLayers { get; set; }

        #endregion

        #region ---   构造函数

        [XmlIgnore]
        protected static StationModel _uiniqueInstance;


        protected StationModel()
        {
            MaterialDefinitions = new XmlList<Material>();
            ProfileDefinitions = new XmlList<Profile>();
            SoilLayers = new XmlList<SoilLayer>();
        }

        #endregion

        #region ---   几何绘图

        /// <summary> 提取车站模型的几何信息，用于前处理界面绘图 </summary>
        public abstract  StationGeometry GetStationGeometry();

        #endregion

    }
}