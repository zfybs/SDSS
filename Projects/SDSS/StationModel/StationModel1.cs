using System;
using System.Linq;
using System.Xml.Serialization;
using eZstd.Enumerable;
using SDSS.Entities;

namespace SDSS.StationModel
{
    [Serializable()]
    public class StationModel1 : StationModel
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
            _uiniqueInstance = _uiniqueInstance ?? new StationModel1();
            return _uiniqueInstance;
        }

        /// <summary> 构造函数 </summary>
        private StationModel1() : base()
        {
            Beams = new XmlList<Beam>();
            Columns = new XmlList<Column>();
        }

        #endregion

        #region ---   构造矩形框架

        private double[] _layerHeight;
        private double[] _spanWidth;

        /// <summary>
        /// 根据层高与跨宽生成矩形车站框架
        /// </summary>
        /// <param name="layerHeights">从下往上每一层的高度</param>
        /// <param name="spanWidths">从左往右每一跨的宽度</param>
        public void GenerateFrame(double[] layerHeights, double[] spanWidths)
        {
            _layerHeight = layerHeights;
            _spanWidth = spanWidths;

            //
            Definitions.FrameVertices = new XmlList<FrameVertice>();
            var verticesColle = Definitions.FrameVertices;
            //
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
                    verticesColle.Add(new FrameVertice(x: Xs[r], y: Ys[c], index_x: r, index_y: c));
                }
            }

            // 根据节点对象生成梁柱单元
            Beams = new XmlList<Beam>();
            for (int c = 0; c < layerCount + 1; c++)
            {
                for (int r = 0; r < spanCount; r++)
                {
                    int leftVerticeIndex = c * (spanCount + 1) + r;
                    Beam b = new Beam(material: null, profile: null, v1: verticesColle[leftVerticeIndex],
                        v2: verticesColle[leftVerticeIndex + 1]);
                    b.LocationTag = $"({verticesColle[leftVerticeIndex].Index_X + 1},{verticesColle[leftVerticeIndex].Index_Y})";
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
                        v1: verticesColle[bottomVerticeIndex], v2: verticesColle[bottomVerticeIndex + spanCount + 1]);
                    col.LocationTag =
                        $"({verticesColle[bottomVerticeIndex].Index_X},{verticesColle[bottomVerticeIndex].Index_Y + 1})";
                    Columns.Add(col);
                }
            }
        }

        #endregion

        #region ---   模型检验

        /// <summary> 对模型进行检查，如果此模型不满足进行计算的必备条件，则返回false </summary>
        public override bool Validate(out string errorMessage)
        {
            errorMessage = "模型检验完成，可以进行计算";
            return true;
        }

        #endregion

        #region ---   几何绘图

        public override StationGeometry GetStationGeometry()
        {
            SoilStructureGeometry ssg = null;
            if (_layerHeight != null && _spanWidth != null)
            {
                ssg = new SoilStructureGeometry(60, new float[] { 3, 6, 5, 4, 6, 6 }, 3,
                    stationFloors: _layerHeight.Select(r => (float)r).Reverse().ToArray(),
                    stationSegments: _spanWidth.Select(r => (float)r).ToArray());
            }

            // ssg = new SoilStructureGeometry(60, new float[] { 3, 6, 5, 4, 6, 6 }, 3, new float[] { 3, 3, 3 }, new float[] { 6, 6 });
            return ssg;
        }

        #endregion
    }
}