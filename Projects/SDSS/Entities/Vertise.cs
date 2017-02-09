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

    /// <summary> 几何节点 </summary>
    [Serializable()]
    public class Vertice : IGeomObject
    {
        #region ---   XmlAttribute

        private static uint _id = 0;
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

        public Vertice(double x, double y)
        {
            X = x;
            Y = y;
            ID = NewId();
        }

        /// <summary> 整个系统中的节点的最大编号值 </summary>
        private static uint _MaxId = 0;
        private uint NewId()
        {
            _MaxId += 1;
            return _MaxId;
        }
        #endregion


    }
}

