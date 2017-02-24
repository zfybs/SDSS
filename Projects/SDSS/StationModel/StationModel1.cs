using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using eZstd.Enumerable;
using SDSS.Constants;
using SDSS.Definitions;
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

        #region ---   XmlAttribute

        ///<summery>从下往上每一层的高度， 对应的数值格式为： @"1.2, 3.2, 5.2," </summery>
        [XmlAttribute()]
        public string LayerHeightsStr
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var h in LayerHeights)
                {
                    sb.Append(h + ", ");
                }
                return sb.ToString();
            }
            set
            {
                //value = @"1.2, 3.2, 5.2,";
                var ss = value.Split(',');
                double[] arr = new double[ss.Length - 1];  // 最后一个元素为空，不能转换为数值
                for (int i = 0; i < ss.Length - 1; i++)
                {
                    arr[i] = double.Parse(ss[i]);
                }
                LayerHeights = arr;
            }
        }

        ///<summery>从下往上每一层的高度</summery>
        [XmlIgnore()]
        public double[] LayerHeights { get; private set; }

        ///<summery>从左往右每一跨的宽度， 对应的数值格式为： @"1.2, 3.2, 5.2," </summery>
        [XmlAttribute()]
        public string SpanWidthsStr
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var h in SpanWidths)
                {
                    sb.Append(h + ", ");
                }
                return sb.ToString();
            }
            set
            {
                //value = @"1.2, 3.2, 5.2,";
                var ss = value.Split(',');
                double[] arr = new double[ss.Length - 1];  // 最后一个元素为空，不能转换为数值
                for (int i = 0; i < ss.Length - 1; i++)
                {
                    arr[i] = double.Parse(ss[i]);
                }
                SpanWidths = arr;
            }
        }

        ///<summery>从左往右每一跨的宽度</summery>
        [XmlIgnore()]
        public double[] SpanWidths { get; private set; }

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
            //
            LayerHeights = new double[0];
            SpanWidths = new double[0];
        }

        #endregion

        #region ---   构造矩形框架

        /// <summary>
        /// 根据层高与跨宽生成矩形车站框架
        /// </summary>
        /// <param name="layerHeights">从下往上每一层的高度</param>
        /// <param name="spanWidths">从左往右每一跨的宽度</param>
        public void GenerateFrame(double[] layerHeights, double[] spanWidths, Material defaultMat, Profile defaultProfile)
        {
            LayerHeights = layerHeights;
            SpanWidths = spanWidths;

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
                    Beam b = new Beam(material: defaultMat, profile: defaultProfile, v1: verticesColle[leftVerticeIndex],
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
                    Column col = new Column(material: defaultMat, profile: defaultProfile,
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
            if (LayerHeights != null && SpanWidths != null)
            {
                ssg = new SoilStructureGeometry(60, new float[] { 3, 6, 5, 4, 6, 6 }, 3,
                    stationFloors: LayerHeights.Select(r => (float)r).Reverse().ToArray(),
                    stationSegments: SpanWidths.Select(r => (float)r).ToArray());
            }

            // ssg = new SoilStructureGeometry(60, new float[] { 3, 6, 5, 4, 6, 6 }, 3, new float[] { 3, 3, 3 }, new float[] { 6, 6 });
            return ssg;
        }

        #endregion
    }
}