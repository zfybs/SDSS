using System;
using System.ComponentModel;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Definitions;
namespace SDSS.Entities
{
    /// <summary> 土体环境相关的信息 </summary>
    [Serializable()]
    public class SoilPropertys : ICloneable
    {

        #region ---  BoundaryParam中的边界参数

        /// <summary> 土层的水平基床系数 </summary>
        [XmlAttribute()]
        public float kx { get; set; }  

        /// <summary> 土层的竖向基床系数 </summary>
        [XmlAttribute()]
        public float ky { get; set; }  

        /// <summary> 地表的标高 </summary>
        [XmlAttribute()]
        public float TopElevation { get; set; }  

        /// <summary> 上覆土厚度 </summary>
        [XmlAttribute()]
        public float OverLayingSoilHeight { get; set; } 
        
        #endregion
        
        #region ---   构造函数

        public SoilPropertys()
        {

        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}