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
    /// <summary> 构件截面信息 </summary>
    [Serializable()]
    public abstract class Profile : AbqEntity
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Tag), Description("构件横截面类型")]
        public ProfileType Type { get; set; }

        #endregion

        #region ---   构造函数

        public Profile(string name, ProfileType tp):base(name)
        {
            Name = name;
            Type = tp;
        }

        #endregion

    }

    /// <summary> 矩形截面信息 </summary>
    [Serializable()]
    public class Rectangular : Profile
    {
        #region ---   XmlAttribute

        #region ---  一般性 Property
        [XmlAttribute()]
        [Category(Categories.Property), Description("截面的宽度，单位为 mm")]
        public double Width { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("截面的高度，单位为 mm")]
        public double Height { get; set; }
        #endregion
        #endregion

        #region ---   构造函数

        public Rectangular(string name, double width, double height) : base(name, ProfileType.Rectangular)
        {
            Width = width;
            Height = height;
        }

        #endregion
        
    }
}
