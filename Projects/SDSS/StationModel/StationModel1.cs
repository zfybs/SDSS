using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using eZstd.Data;
using SDSS.Entities;
using SDSS.Definitions;

namespace SDSS.StationModel
{
    internal class StationModel1 : StationModel
    {


        #region ---   XmlElement

        /// <summary> 整个框架中所有的梁构件 </summary>
        [XmlElement]
        public XmlList<Beam> Beams { get; set; }

        /// <summary> 整个框架中所有的柱构件 </summary>
        [XmlElement]
        public XmlList<Column> Columns { get; set; }

        #endregion

        #region ---   构造函数
        public static StationModel GetUniqueInstance()
        {
            return _uiniqueInstance ?? new StationModel1();
        }

        /// <summary> 构造函数 </summary>
        private StationModel1() : base()
        {
            Beams = new XmlList<Beam>();
            Columns = new XmlList<Column>();
        }
        #endregion

        #region ---   几何绘图

        public override StationGeometry GetStationGeometry()
        {
            SoilStructureGeometry ssg = null;
            ssg = new SoilStructureGeometry(60, new float[] { 3, 6, 5, 4, 6, 6 }, 3, new float[] { 3, 3, 3 }, new float[] { 6, 6 });
            return ssg;
        }
        #endregion


    }
}
