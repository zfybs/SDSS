using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SocketedShafts.Entities
{
    /// <summary> 模型系统中的基本材料参数 </summary>
    [Serializable()]
    public class SystemProperty : ICloneable
    {
        #region ---   Properties

        #region ---   XmlAttribute
        [XmlAttribute()]
        [Category(Categories.Material), Description("钢材的弹性模量，单位为KPa。")]
        public float Es { get; set; }

        [XmlAttribute()]
        [Category(Categories.Material), Description("钢筋的屈服强度，单位为KPa。比如HPB300的屈服强度为300e3 KPa ")]
        public float fy { get; set; }

        [XmlAttribute()]
        [Category(Categories.Material), Description("28天圆柱体抗压强度，单位为KPa。 C40立方体抗压强度标准值为fcu=26.8MPa，等效为28天圆柱体抗压强度fcp=0.79*fcu=21.172Mpa")]
        public float fcy { get; set; }
        
        //[XmlAttribute()]
        //[Category(Categories.Elevation), Description("整个模型中的水面绝对标高，如果没有水，则设置其值为任意低于土层顶部标高的值。")]
        //public Single WaterTop { get; set; }


        #endregion

        #endregion

        #region ---   构造函数

        public SystemProperty()
        {
            fy = 300;
            fcy = 21172;
            Es = 210000000;
            //
            // WaterTop = 600;
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}