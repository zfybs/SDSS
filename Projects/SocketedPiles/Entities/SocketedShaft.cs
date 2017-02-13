using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using eZstd.Data;

namespace SocketedShafts.Entities
{
    /// <summary> 一根嵌岩桩 </summary>
    [Serializable()]
    public class SocketedShaft : ICloneable
    {
        #region ---   Properties

        // 基本信息

        ///// <summary> 桩顶（一般位于水面）所受的水平荷载，单位为 KN </summary>
        //[XmlAttribute()]
        //[Category(Categories.Property), Description("桩顶所受的水平荷载，单位为 KN")]
        //public Single HorizontalLoads { get; set; }

        /// <summary> 嵌岩桩的名称 </summary>
        [XmlAttribute()]
        [Category(Categories.Tag), Description("嵌岩桩的名称")]
        public string Name { get; set; }

        /// <summary> 嵌岩桩的基本信息描述 </summary>
        [XmlAttribute()]
        [Category(Categories.Property), Description("嵌岩桩的基本信息描述")]
        public string Description { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description(@"整根桩在土层中部分的等效半径，用于基本有限差分法中的计算，单位为m。
由于桩是变截面的，所以这里的R是一个等效值，而且在一次有限差分的计算过程中是不变的；在桩的开裂以及刚度折减的过程中，这个值暂且也认为它是一个不变量。")]
        public float R { get; set; }

        /// <summary> 整根桩的所有截面集合（不区分水中与土层中，而将其看成是未安装前的一根桩） </summary>
        [Category(Categories.Property), ReadOnly(true), Browsable(false), Description("桩中的所有截面集合")]
        public XmlList<ShaftSectionEntity> Sections { get; set; }

        #endregion

        #region ---   构造函数

        /// <summary> 构造函数 </summary>
        public SocketedShaft()
        {
            Sections = new XmlList<ShaftSectionEntity>();
        }

        #endregion

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}