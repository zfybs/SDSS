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

        /// <summary> 整个框架中所有的节点定义 </summary>
        [XmlElement]
        public XmlList<FrameVertice> Vertices { get; set; }


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
            Vertices = new XmlList<FrameVertice>();
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

        #region ---   几何绘图

        /// <summary>
        /// 根据层高与跨宽生成矩形车站框架
        /// </summary>
        /// <param name="layerHeights">从下往上每一层的高度</param>
        /// <param name="spanWidths">从左往右每一跨的宽度</param>
        public void GenerateFrame(double[] layerHeights, double[] spanWidths)
        {
            Vertices = new XmlList<FrameVertice>();
            int spanCount = spanWidths.Length;
            int layerCount = layerHeights.Length;

            // 构造节点系统
            double[] Xs = new double[spanCount + 1];
            for (int i = 0; i < spanCount; i++)
            {
                Xs[i + 1] = Xs[i] + spanWidths[i];
            }
            double[] Ys = new double[layerCount + 1];
            for (int i = 0; i < layerCount; i++)
            {
                Ys[i + 1] = Ys[i] + layerHeights[i];
            }
            // 生成结点对象
            for (int c = 0; c < layerCount + 1; c++)
            {
                for (int r = 0; r < spanCount + 1; r++)
                {
                    Vertices.Add(new FrameVertice(x: Xs[r], y: Ys[c], index_x: r, index_y: c));
                }
            }

            // 根据节点对象生成梁柱单元
            Beams = new XmlList<Beam>();
            for (int c = 0; c < layerCount + 1; c++)
            {
                for (int r = 0; r < spanCount; r++)
                {
                    int leftVerticeIndex = c * (spanCount + 1) + r;
                    Beam b = new Beam(material: null, profile: null, v1: Vertices[leftVerticeIndex],
                        v2: Vertices[leftVerticeIndex + 1]);
                    b.LocationTag = $"({Vertices[leftVerticeIndex].Index_X + 1},{Vertices[leftVerticeIndex].Index_Y})";
                    Beams.Add(b);
                }
            }
            Columns = new XmlList<Column>();
            for (int c = 0; c < layerCount; c++)
            {
                for (int r = 0; r < spanCount + 1; r++)
                {
                    int bottomVerticeIndex = c * (spanCount + 1) + r;
                    Column col = new Column(material: null, profile: null,
                        v1: Vertices[bottomVerticeIndex], v2: Vertices[bottomVerticeIndex + spanCount + 1]);
                    col.LocationTag = $"({Vertices[bottomVerticeIndex].Index_X },{Vertices[bottomVerticeIndex].Index_Y + 1})";
                    Columns.Add(col);
                }
            }
        }
        #endregion
    }
}