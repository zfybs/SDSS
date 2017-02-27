﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Definitions;
using SDSS.Constants;

namespace SDSS.Definitions
{
    /// <summary> 构件截面信息 </summary>
    [Serializable()]
    [XmlInclude(typeof(Rectangular))]
    [XmlInclude(typeof(T))]
    public class Profile : Definition
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Tag), Description("构件横截面类型")]
        public ProfileType Type { get; set; }

        #endregion

        #region ---   构造函数

        public Profile() : base()
        { }

        public Profile(string name, ProfileType tp) : base()
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

        [XmlAttribute()]
        [Category(Categories.Property), Description("截面的宽度，单位为 mm")]
        public double Width { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("截面的高度，单位为 mm")]
        public double Height { get; set; }
        #endregion

        #region ---   构造函数
        public Rectangular() : base("矩形", ProfileType.Rectangular)
        {

        }
        public Rectangular(string name, double width, double height) : base(name, ProfileType.Rectangular)
        {
            Width = width;
            Height = height;
        }

        #endregion

    }

    /// <summary> T形截面信息 </summary>
    [Serializable()]
    public class T : Profile
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Property), Description("截面的宽度，单位为 mm")]
        public double Width { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("截面的高度，单位为 mm")]
        public double Height { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("腹板厚度，单位为 m")]
        public double WebThickness { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("翼缘厚度，单位为 m")]
        public double FlangeThickness { get; set; }

        #endregion

        #region ---   构造函数

        public T() : base("T形", ProfileType.T)
        {
        }

        /// <summary> 构造函数 </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="webThickness">腹板厚度</param>
        /// <param name="flangeThickness">翼缘厚度</param>
        public T(string name, double width, double height, double webThickness, double flangeThickness) : base(name, ProfileType.T)
        {
            Width = width;
            Height = height;
            WebThickness = webThickness;
            FlangeThickness = flangeThickness;
        }

        #endregion

    }
}
