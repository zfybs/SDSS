using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using eZstd.Enumerable;
using SDSS.Definitions;
using SDSS.Entities;

namespace SDSS.StationModel
{
    /// <summary> 所有车站模型的基类 </summary>
    [Serializable()]
    [XmlRoot()]
    [XmlInclude(typeof(StationModel1))]
    public abstract class StationModel
    {
        #region ---   XmlElement

        /// <summary>
        /// 整个系统中所有材料、截面等定义的集合
        /// </summary>
        [XmlElement]
        public DefinitionCollection Definitions { get; set; }

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
            Definitions = DefinitionCollection.GetUniqueInstance();
            SoilLayers = new XmlList<SoilLayer>();

            SystemProperty = new SystemProperty();
        }

        #endregion

        #region ---   抽象方法

        /// <summary> 提取车站模型的几何信息，用于前处理界面绘图 </summary>
        public abstract StationGeometry GetStationGeometry();


        /// <summary> 对模型进行检查，如果此模型不满足进行计算的必备条件，则返回false </summary>
        public abstract bool Validate(out string errorMessage);


        #endregion
    }
}