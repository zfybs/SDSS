using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using eZstd.Enumerable;
using SDSS.Constants;
using SDSS.Definitions;
using SDSS.Entities;
using SDSS.Structures;

namespace SDSS.Models
{
    /// <summary> 所有车站模型的基类 </summary>
    [Serializable()]
    [XmlRoot()]
    [XmlInclude(typeof(Model1))]
    public abstract class ModelBase
    {
        #region ---   XmlAttribute

        /// <summary> 模型的名称发生改变 </summary>
        public event Action<ModelBase, string> ModelNameChanged;

        private string _modelName;
        [XmlAttribute()]
        [Category(Categories.Property), Description("项目的名称")]
        public string ModelName
        {
            get { return _modelName; }
            set
            {
                _modelName = value;
                // 触发事件
                ModelNameChanged?.Invoke(this, value);
            }
        }

        /// <summary> 模型的描述类型，比如“矩形框架惯性力法”，可以看成一个常数 </summary>
        [XmlIgnore()]
        public virtual string DescriptionName { get; protected set; }

        /// <summary> 模型的标识ID值 </summary>
        [XmlAttribute()]
        public Guid ID { get; set; }

        /// <summary> 模型的类型，比如矩形框架，圆形隧道等 </summary>
        [XmlAttribute()]
        [Category(Categories.Property), Description("模型的类型，比如矩形框架，圆形隧道等")]
        public ModelType ModelType { get; set; }

        /// <summary> 模型计算的方法，比如惯性力法，反应位移法等 </summary>
        [XmlAttribute()]
        [Category(Categories.Property), Description("模型计算的方法，比如惯性力法，反应位移法等")]
        public CalculationMethod CalculationMethod { get; set; }

        #endregion

        #region ---   XmlElement

        /// <summary>
        /// 整个系统中所有材料、截面等定义的集合
        /// </summary>
        [XmlElement]
        public DefinitionCollection Definitions { get; set; }

        /// <summary> 土层集合 </summary>
        public XmlList<SoilLayer_Inertial> SoilLayers { get; set; }

        #endregion

        #region ---   构造函数

        protected ModelBase(ModelType modelType, CalculationMethod calMethod) : this()
        {
            ModelType = modelType;
            CalculationMethod = calMethod;
        }

        protected ModelBase()
        {
            ModelName = Constants.ProjectConsts.DefaultModelName;
            ID = Guid.NewGuid();
            //
            Definitions = new DefinitionCollection();
            SoilLayers = new XmlList<SoilLayer_Inertial>();
            //

            ModelType = ModelType.Frame;
            CalculationMethod = CalculationMethod.InertialForce;
        }

        #endregion

        #region ---   抽象方法

        /// <summary> 提取车站模型的几何信息，用于前处理界面绘图 </summary>
        public abstract StationGeometry GetStationGeometry();

        /// <summary> 对模型进行检查，如果此模型不满足进行计算的必备条件，则返回false </summary>
        public abstract bool Validate(ref StringBuilder errorMessage);

        /// <summary> 将模型信息写入一个文本文件中，用来作为 Ansys 计算的初始参数提供给 APDL 命令流 </summary>
        /// <param name="filePath">要写入的文件路径，此文件当前可以不存在</param>
        /// <param name="errMsg">出错信息</param>
        /// <returns>是否写入成功</returns>
        public abstract bool WriteCalculateFileForAnsys(string filePath, ref StringBuilder errMsg);

        #endregion

        public override string ToString()
        {
            return ModelName;
        }

        public override bool Equals(object obj)
        {
            var model = obj as ModelBase;
            if (model != null && model.ID == this.ID)
            {
                return true;
            }
            return false;
        }
    }
}