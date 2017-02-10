using System;
using System.ComponentModel;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Definitions;
using SDSS.Entities;

namespace SDSS.Definitions
{
    /// <summary> 水中桩段或者嵌岩桩段的截面参数 </summary>
    [Serializable()]
    public class SoilLayer : Definition
    {
        #region ---   XmlAttribute

        [XmlAttribute]
        [Category(Categories.Material), Description("土层本构的名称")]
        public string MaterialName {
            get { return Material.Name; } }

        [XmlIgnore]
        [Category(Categories.Material), Description("土层参数")]
        public Material Material { get; set; }


        /// <summary> 此一小段桩的顶部绝对标高 </summary>
        [XmlAttribute()]
        public float Top { get; set; }

        /// <summary> 此一小段桩的底部绝对标高 </summary>
        [XmlAttribute()]
        public float Bottom { get; set; }

        #endregion

        #region ---   构造函数

        public SoilLayer()
        {
          
        }

        #endregion
    }
}