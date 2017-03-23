using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Project;
using SDSS.Definitions;
using SDSS.Models;
using SDSS.Structures;

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
        [Category(Categories.Property), ReadOnly(true), Description("用来确定此构件的定位标识的信息，比如对于框架梁，可以通过二维向量“(1,2)”表示最左跨，倒数第二层顶部梁。对于圆形隧道管片，通过二维向量“（0,90）”表示该管片起始节点的角度为0°，终止节点的角度为90°")]
        public string LocationTag { get; set; }

        [XmlAttribute(attributeName: "Material")]
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
                var definitions = DefinitionCollection.ActiveDefiCollForDeserialize;
                Material = definitions.GetMaterial(value);
            }
        }

        [XmlIgnore()]
        [Category(Categories.Property), Description("构件材料信息")]
        public Material Material { get; set; }

        [XmlAttribute(attributeName: "Profile")]
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
                var definitions = DefinitionCollection.ActiveDefiCollForDeserialize;
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
                var vertices = Frame.ActiveFrameForDeserialize;
                LeftVertice = vertices.GetFrameVertice( value);
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
                var vertices = Frame.ActiveFrameForDeserialize; 
                RightVertice = vertices.GetFrameVertice(value);
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
                var vertices = Frame.ActiveFrameForDeserialize;
                TopVertice = vertices.GetFrameVertice(value);
            }
        }
        [XmlIgnore()]
        public Vertice TopVertice { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("垂直柱的底部节点")]
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
                var vertices = Frame.ActiveFrameForDeserialize;
                BottomVertice = vertices.GetFrameVertice(value);
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

    /// <summary> 管片构件对象 </summary>
    [Serializable()]
    public class Segment : Component
    {
        #region ---   XmlAttribute
        [XmlAttribute()]
        [Category(Categories.Property), Description("管片沿着逆时针方向上的上游顶点")]
        public uint Vertice1Id
        {
            get
            {
                if (Vertice1 == null)
                {
                    return -0;
                }
                return Vertice1.ID;
            }
            set
            {
                var vertices = Tunnel.ActiveTunnelForDeserialize;
                Vertice1 = vertices.GetTunnelVertice(value);
            }
        }
        [XmlIgnore()]
        public TunnelVertice Vertice1 { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("管片沿着逆时针方向上的下游顶点")]
        public uint Vertice2Id
        {
            get
            {
                if (Vertice2 == null)
                {
                    return -0;
                }
                return Vertice2.ID;
            }
            set
            {
                var vertices = Tunnel.ActiveTunnelForDeserialize;
                Vertice2 = vertices.GetTunnelVertice(value);
            }
        }
        [XmlIgnore()]
        public TunnelVertice Vertice2 { get; set; }

        #endregion
        public Segment() : base()
        {
        }

        public Segment(Material material, Profile profile, TunnelVertice v1, TunnelVertice v2) :
            base(ComponentType.Segment, ComponentGeomType.Arc, material, profile)
        {
            if (v1.Angle <= v2.Angle)
            {
                Vertice1 = v1;
                Vertice2 = v2;
            }
            else
            {
                Vertice1 = v2;
                Vertice2 = v1;
            }
        }
    }
}