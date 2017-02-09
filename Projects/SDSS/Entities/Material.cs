using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Definitions;
using SDSS.Constants;

namespace SDSS.Entities
{

    /// <summary> 材料本构信息 </summary>
    [Serializable()]
    public class Material : AbqEntity
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Tag), Description("材料本构类型")]
        public MaterialType Type { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("密度，单位为 Kg/m3")]
        public double Density { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("弹性模量，单位为 MPa")]
        public double Elasticity { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("泊松比")]
        public double PoissonRatio { get; set; }

        #endregion

        #region ---   构造函数

        public Material(string name, double density, double elastic, double poissonRatio, MaterialType type = MaterialType.Elastic) : base(name)
        {
            Density = density;
            Elasticity = elastic;
            PoissonRatio = poissonRatio;
            Type = type;
        }

        #endregion
    }
}
