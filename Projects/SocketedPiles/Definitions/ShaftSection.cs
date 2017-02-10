using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SocketedShafts.Definitions
{
    /// <summary> 水中桩段或者嵌岩桩段的截面参数 </summary>
    [Serializable()]
    public class ShaftSection : Definition
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Property), Description("桩的外径，单位为m。如果有钢套管，则为连同钢套管的总外径。 ")]
        public float Diameter { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("钢套管的壁厚，单位为m。如果没有钢套管，则为0")]
        public float ThicknessOfSteeltube { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("纵向钢筋直径，单位为m")]
        public float DBar { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("钢筋笼的直径（外径），单位为m。")]
        public float DSteelCage { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("纵向钢筋的根数")]
        public int BarsCount { get; set; }

        #endregion

        #region ---   构造函数

        public ShaftSection()
        {
            Diameter = 2.4f;
            ThicknessOfSteeltube = 0.01f;
            DBar = 0.06f;
            DSteelCage = 2.3f;
            BarsCount = 28;
        }

        #endregion
    }
}