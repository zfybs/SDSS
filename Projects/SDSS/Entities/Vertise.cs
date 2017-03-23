using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Definitions;
using SDSS.Project;

namespace SDSS.Entities
{

    /// <summary> 几何节点 </summary>
    [Serializable()]
    [XmlInclude(typeof(FrameVertice))]
    public class Vertice : IGeomObject
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Property), Description("节点的ID编号")]
        public uint ID { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("X坐标")]
        public double X { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("Y坐标")]
        public double Y { get; set; }

        #endregion

        #region ---   构造函数

        public Vertice()
        {
            ID = NewId();
        }

        public Vertice(double x, double y) : this()
        {
            X = x;
            Y = y;
        }

        /// <summary> 整个系统中的节点的最大编号值 </summary>
        private static uint _MaxId = 0;
        private uint NewId()
        {
            _MaxId += 1;
            return _MaxId;
        }
        #endregion

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    /// <summary> 矩形框架中的几何节点 </summary>
    [Serializable()]
    public class FrameVertice : Vertice
    {
        #region ---   XmlAttribute

        /// <summary> 此节点在矩形框架中的位置下标，最左边的下标值为0 </summary>
        [XmlAttribute()]
        [Category(Categories.Property), Description("此节点在矩形框架中的位置下标，最左边的下标值为0")]
        public int Index_X { get; set; }

        /// <summary> 此节点在矩形框架中的位置下标，最底部的下标值为0 </summary>
        [XmlAttribute()]
        [Category(Categories.Property), Description("此节点在矩形框架中的位置下标，最底部的下标值为0")]
        public int Index_Y { get; set; }
        #endregion

        #region ---   构造函数

        public FrameVertice()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="index_x">此节点在矩形框架中的位置下标，最左边的下标值为0</param>
        /// <param name="index_y">此节点在矩形框架中的位置下标，最底部的下标值为0</param>
        public FrameVertice(double x, double y, int index_x, int index_y) : base(x, y)
        {
            Index_X = index_x;
            Index_Y = index_y;
        }

        #endregion
    }


    /// <summary> 圆形隧道的几何节点（极坐标） </summary>
    [Serializable()]
    public class TunnelVertice : IGeomObject
    {
        #region ---   XmlAttribute

        [XmlAttribute()]
        [Category(Categories.Property), Description("节点的ID编号")]
        public uint ID { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("半径")]
        public double R { get; set; }

        [XmlAttribute()]
        [Category(Categories.Property), Description("角度")]
        public double Angle{ get; set; }

        #endregion

        #region ---   构造函数

        public TunnelVertice()
        {
            ID = NewId();
        }

        public TunnelVertice(double r, double angle) : this()
        {
            R = r;
            Angle = angle;
        }

        /// <summary> 整个系统中的节点的最大编号值 </summary>
        private static uint _MaxId = 0;
        private uint NewId()
        {
            _MaxId += 1;
            return _MaxId;
        }
        #endregion

        public override string ToString()
        {
            return $"({R},{Angle})";
        }
    }
}

