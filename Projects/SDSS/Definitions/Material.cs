using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Definitions;
using SDSS.Constants;

namespace SDSS.Definitions
{

    /// <summary> 材料本构信息，也表示基本的弹性材料 </summary>
    [Serializable()]
    public class Material : Definition
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

        public Material() : base()
        {
        }

        public Material(string name, double density, double elastic, double poissonRatio, MaterialType type = MaterialType.Elastic) : base()
        {
            Name = name;
            Density = density;
            Elasticity = elastic;
            PoissonRatio = poissonRatio;
            Type = type;
        }

        #endregion
    }

    /// <summary> 摩尔库仑 理想弹塑性材料 </summary>
    [Serializable()]
    public class MohrCoulomb : Material
    {
        #region ---   XmlAttribute
        
        [XmlAttribute()]
        [Category(Categories.Property), Description("黏聚力，单位为 KPa")]
        public double Cohesion { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("摩擦角，单位为 度")]
        public double FrictionAngle { get; set; }
        
        #endregion

        #region ---   构造函数


        public MohrCoulomb(string name, double density, double elastic, double poissonRatio,
            double cohesion, double frictionAngle) : base()
        {
            Name = name;
            Density = density;
            Elasticity = elastic;
            PoissonRatio = poissonRatio;
            Type = MaterialType.MohrCoulomb;
            //
            Cohesion = cohesion;
            FrictionAngle = frictionAngle;
        }

        #endregion
    }
}
