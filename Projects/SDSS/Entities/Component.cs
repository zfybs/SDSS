using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Project;
using SDSS.Definitions;
using SDSS.StationModel;

namespace SDSS.Entities
{

    /// <summary> 结构构件对象 </summary>
    [Serializable()]
    public abstract class Component : IGeomObject
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Tag), ReadOnly(true), Description("构件的几何类型")]
        public ComponentGeomType GeomType { get; set; }

        [XmlAttribute()]
        [Category(Categories.Tag), ReadOnly(true), Description("构件类型")]
        public ComponentType ComponentType { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), ReadOnly(true), Description("单元构件的ID编号")]
        public uint ID { get; set; }

        /// <summary>
        /// 用来确定此构件的定位标识的信息，比如对于框架梁，可以通过二维向量“(1,2)”表示最左跨，倒数第二层顶部梁。
        /// </summary>
        [XmlAttribute()]
        [Category(Categories.Property), ReadOnly(true), Description("用来确定此构件的定位标识的信息，比如对于框架梁，可以通过二维向量“(1,2)”表示最左跨，倒数第二层顶部梁。")]
        public string LocationTag { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("构件材料信息")]
        public string MaterialName
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
        public Material Material { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("构件横截面信息")]
        public string ProfileName
        {
            get
            {
                if (Profile == null)
                {
                    return "";
                }
                return Profile.Name;
            }
            set
            {
                var definitions = DefinitionCollection.GetUniqueInstance();
                Profile = definitions.GetProfile(value);
            }
        }

        [XmlIgnore()]
        [Category(Categories.Property), Description("构件横截面信息")]
        public Profile Profile { get; set; }

        #endregion

        #region ---   构造函数

        public Component()
        {
            ID = NewId();
        }

        public Component(ComponentType compType, ComponentGeomType geomType, Material material, Profile profile) : this()
        {
            ComponentType = compType;
            GeomType = geomType;
            Material = material;
            Profile = profile;
        }

        /// <summary> 整个系统中的构件的最大编号值 </summary>
        private static uint _MaxId = 0;
        private uint NewId()
        {
            _MaxId += 1;
            return _MaxId;
        }
        #endregion

        public override string ToString()
        {
            return LocationTag;
        }
    }

    /// <summary> 梁构件对象 </summary>
    [Serializable()]
    public class Beam : Component
    {

        #region ---   XmlAttribute
        [XmlAttribute()]
        [Category(Categories.Property), Description("梁的左边节点")]
        public uint LeftVerticeId
        {
            get
            {
                if (LeftVertice == null)
                {
                    return -0;
                }
                return LeftVertice.ID;
            }
            set
            {
                var definitions = DefinitionCollection.GetUniqueInstance();
                LeftVertice = definitions.GetFrameVertice(value);
            }
        }
        [XmlIgnore()]
        public Vertice LeftVertice { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("梁的右边节点")]
        public uint RightVerticeId
        {
            get
            {
                if (RightVertice == null)
                {
                    return -0;
                }
                return RightVertice.ID;
            }
            set
            {
                var definitions = DefinitionCollection.GetUniqueInstance();
                RightVertice = definitions.GetFrameVertice(value);
            }
        }
        [XmlIgnore]
        public Vertice RightVertice { get; set; }
        #endregion

        public Beam()
        {
        }

        public Beam(Material material, Profile profile, Vertice v1, Vertice v2) :
            base(ComponentType.Beam, ComponentGeomType.Line, material, profile)
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

        #region ---   XmlAttribute
        [XmlAttribute()]
        [Category(Categories.Property), Description("垂直柱的顶部节点")]
        public uint TopVerticeId
        {
            get
            {
                if (TopVertice == null)
                {
                    return -0;
                }
                return TopVertice.ID;
            }
            set
            {
                var definitions = DefinitionCollection.GetUniqueInstance();
                TopVertice = definitions.GetFrameVertice(value);
            }
        }
        [XmlIgnore()]
        public Vertice TopVertice { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("垂直柱的顶部节点")]
        public uint BottomVerticeId
        {
            get
            {
                if (BottomVertice == null)
                {
                    return -0;
                }
                return BottomVertice.ID;
            }
            set
            {
                var definitions = DefinitionCollection.GetUniqueInstance();
                BottomVertice = definitions.GetFrameVertice(value);
            }
        }
        [XmlIgnore()]
        public Vertice BottomVertice { get; set; }

        #endregion
        public Column() : base()
        {
        }

        public Column(Material material, Profile profile, Vertice v1, Vertice v2) :
            base(ComponentType.Column, ComponentGeomType.Line, material, profile)
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