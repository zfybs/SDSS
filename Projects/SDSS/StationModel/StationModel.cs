using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using eZstd.Enumerable;
using SDSS.Constants;
using SDSS.Project;
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
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Property), Description("项目的名称")]
        public string ModelName { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("模型的类型，比如矩形框架，圆形隧道等")]
        public ModelType ModelType { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("计算的方法，比如惯性力法，反应位移法等")]
        public CalculationMethod CalculationMethod { get; set; }

        #endregion

        #region ---   XmlElement

        /// <summary>
        /// 整个系统中所有材料、截面等定义的集合
        /// </summary>
        [XmlElement]
        public DefinitionCollection Definitions { get; set; }

        /// <summary> 与材料、水位标高等相关的系统参数 </summary>
        public SoilPropertys SoilProperty { get; set; }

        /// <summary> 土层集合 </summary>
        public XmlList<SoilLayer_Inertial> SoilLayers { get; set; }

        #endregion

        #region ---   构造函数

        [XmlIgnore]
        protected static StationModel _uiniqueInstance;

        protected StationModel(string modelName,ModelType modelType,CalculationMethod calMethod) : this()
        {
            ModelName = modelName;
            ModelType = modelType;
            CalculationMethod = calMethod;
        }

        protected StationModel()
        {
            ModelName = "StationModel";
            Definitions = DefinitionCollection.GetUniqueInstance();
            SoilLayers = new XmlList<SoilLayer_Inertial>();
            //
            SoilProperty = new SoilPropertys();
            ModelType = ModelType.Frame;
            CalculationMethod = CalculationMethod.InertialForce;
        }

        #endregion

        #region ---   抽象方法

        /// <summary> 提取车站模型的几何信息，用于前处理界面绘图 </summary>
        public abstract StationGeometry GetStationGeometry();


        /// <summary> 对模型进行检查，如果此模型不满足进行计算的必备条件，则返回false </summary>
        public abstract bool Validate(ref StringBuilder errorMessage);

        #endregion
    }
}