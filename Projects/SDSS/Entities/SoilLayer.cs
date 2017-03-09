using System;
using System.ComponentModel;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Project;
using SDSS.Definitions;
using SDSS.Entities;
using SDSS.StationModel;

namespace SDSS.Entities
{
    /// <summary> 土层信息 </summary>
    [Serializable()]
    [XmlInclude(typeof(SoilLayer_Inertial))]
    public class SoilLayer : Definition
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Property), Description("土层本构的名称")]
        public virtual string MaterialName
        {
            get
            {
                if (Material == null)
                {
                    return "";
                }
                return Material.Name;
            }
            set
            {
                var definitions = DefinitionCollection.GetUniqueInstance();
                Material = definitions.GetMaterial(value);
            }
        }

        [XmlIgnore()]
        [Category(Categories.Property), Description("构件材料信息")]
        public virtual Material Material { get; set; }

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

    /// <summary> 惯性力算法中的土层信息 </summary>
    [Serializable()]
    public class SoilLayer_Inertial : SoilLayer
    {
        #region ---   XmlAttribute

        /// <summary> 土层材料信息，但是对于 惯性力法求解过程中，并不需要土层的材料信息 </summary>
        [XmlAttribute(), Browsable(false)]
        public override string MaterialName { get { return null; } }

        /// <summary> 土层材料信息，但是对于 惯性力法求解过程中，并不需要土层的材料信息 </summary>
        [XmlIgnore(), Browsable(false)]
        public override Material Material { get { return null; } }

        /// <summary> 矩形地铁车站结构顶板上表面与地表齐平时的Kci值 </summary>
        [XmlAttribute()]
        public float Kci0 { get; set; }


        #endregion

        #region ---   构造函数

        public SoilLayer_Inertial()
        {
            MaterialName = null;
            Material = null;
        }
        public SoilLayer_Inertial(float top, float bottom, float kci0)
        {
            Top = top;
            Bottom = bottom;
            Kci0 = kci0;
        }
        #endregion
    }
}