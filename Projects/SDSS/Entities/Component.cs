using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Definitions;

namespace SDSS.Entities
{

    /// <summary> 结构构件对象 </summary>
    [Serializable()]
    public abstract class Component : IGeomObject
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Tag), Description("构件的几何类型")]
        public ComponentGeomType GeomType { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("构件材料信息")]
        public Material Material { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("构件横截面信息")]
        public Profile Profile { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("单元构件的ID编号")]
        public uint ID { get; set; }
        #endregion

        #region ---   构造函数

        public Component(ComponentGeomType geomType, Material material, Profile profile)
        {
            GeomType = geomType;
            Material = material;
            Profile = profile;

            ID = NewId();
        }

        /// <summary> 整个系统中的构件的最大编号值 </summary>
        private static uint _MaxId = 0;
        private uint NewId()
        {
            _MaxId += 1;
            return _MaxId;
        }
        #endregion
    }

    /// <summary> 梁构件对象 </summary>
    [Serializable()]
    public class Beam : Component
    {
        public Vertice LeftVertice { get; set; }
        public Vertice RightVertice { get; set; }

        public Beam(Material material, Profile profile, Vertice v1, Vertice v2) :
            base(ComponentGeomType.Line, material, profile)
        {
            if (v1.X <= v2.X)
            {
                LeftVertice = v1;
                RightVertice = v2;
            }
            else
            {
                LeftVertice = v2;
                RightVertice = v1;
            }
        }
    }


    /// <summary> 柱构件对象 </summary>
    [Serializable()]
    public class Column : Component
    {

        public Vertice TopVertice { get; set; }
        public Vertice BottomVertice { get; set; }

        public Column(Material material, Profile profile, Vertice v1, Vertice v2) :
            base(ComponentGeomType.Line, material, profile)
        {
            if (v1.Y <= v2.Y)
            {
                BottomVertice = v1;
                TopVertice = v2;
            }
            else
            {
                BottomVertice = v2;
                TopVertice = v1;
            }
        }

    }
}