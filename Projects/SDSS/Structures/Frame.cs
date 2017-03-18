using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using eZstd.Enumerable;
using SDSS.Definitions;
using SDSS.Entities;
using SDSS.Models;

namespace SDSS.Structures
{
    [Serializable()]
    public class Frame
    {
        #region ---   XmlElement


        /// <summary> 用来在每次反序列化<see cref="ModelBase"/>对象时，用来根据<see cref="Component"/>的节点编号字符来返回对应的材料等。 </summary>
        [XmlIgnore]
        public static Frame ActiveFrameForDeserialize { get; private set; }

        /// <summary> 整个框架中所有的矩形框架节点定义 </summary>
        [XmlElement]
        public XmlList<FrameVertice> FrameVertices { get; set; }

        /// <summary> 整个框架中所有的梁构件 </summary>
        [XmlElement]
        public XmlList<Beam> Beams { get; set; }

        /// <summary> 整个框架中所有的柱构件 </summary>
        [XmlElement]
        public XmlList<Column> Columns { get; set; }

        #endregion

        #region ---   XmlAttribute


        ///<summery>从下往上每一层的高度， 对应的数值格式为： @"1.2, 3.2, 5.2," </summery>
        [XmlAttribute(attributeName: "LayerHeights")]
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
                var arr = new float[ss.Length - 1];  // 最后一个元素为空，不能转换为数值
                for (int i = 0; i < ss.Length - 1; i++)
                {
                    arr[i] = float.Parse(ss[i]);
                }
                LayerHeights = arr;
            }
        }

        ///<summery>从下往上每一层的高度</summery>
        [XmlIgnore()]
        public float[] LayerHeights { get; private set; }

        ///<summery>从左往右每一跨的宽度， 对应的数值格式为： @"1.2, 3.2, 5.2," </summery>
        [XmlAttribute(attributeName: "SpanWidths")]
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
                var arr = new float[ss.Length - 1];  // 最后一个元素为空，不能转换为数值
                for (int i = 0; i < ss.Length - 1; i++)
                {
                    arr[i] = float.Parse(ss[i]);
                }
                SpanWidths = arr;
            }
        }

        ///<summery>从左往右每一跨的宽度</summery>
        [XmlIgnore()]
        public float[] SpanWidths { get; private set; }

        #endregion

        public Frame()
        {
            Beams = new XmlList<Beam>();
            Columns = new XmlList<Column>();
            FrameVertices = new XmlList<FrameVertice>();
            //
            LayerHeights = new float[0];
            SpanWidths = new float[0];
            //
            ActiveFrameForDeserialize = this;
        }

        #region ---   构造矩形框架

        /// <summary>
        /// 根据层高与跨宽生成矩形车站框架
        /// </summary>
        /// <param name="layerHeights">从下往上每一层的高度</param>
        /// <param name="spanWidths">从左往右每一跨的宽度</param>
        public static Frame Create(float[] layerHeights, float[] spanWidths, Material defaultMat, Profile defaultProfile)
        {

            //
            var frameVertices = new XmlList<FrameVertice>();
            var verticesColle = frameVertices;
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
            var beams = new XmlList<Beam>();
            for (int c = 0; c < layerCount + 1; c++)
            {
                for (int r = 0; r < spanCount; r++)
                {
                    int leftVerticeIndex = c * (spanCount + 1) + r;
                    Beam b = new Beam(material: defaultMat, profile: defaultProfile, v1: verticesColle[leftVerticeIndex],
                        v2: verticesColle[leftVerticeIndex + 1]);
                    b.LocationTag = $"({verticesColle[leftVerticeIndex].Index_X + 1},{verticesColle[leftVerticeIndex].Index_Y})";
                    beams.Add(b);
                }
            }
            var columns = new XmlList<Column>();
            for (int c = 0; c < layerCount; c++)
            {
                for (int r = 0; r < spanCount + 1; r++)
                {
                    int bottomVerticeIndex = c * (spanCount + 1) + r;
                    Column col = new Column(material: defaultMat, profile: defaultProfile,
                        v1: verticesColle[bottomVerticeIndex], v2: verticesColle[bottomVerticeIndex + spanCount + 1]);
                    col.LocationTag =
                        $"({verticesColle[bottomVerticeIndex].Index_X},{verticesColle[bottomVerticeIndex].Index_Y + 1})";
                    columns.Add(col);
                }
            }

            return new Frame()
            {
                Beams = beams,
                Columns = columns,
                FrameVertices = frameVertices,
                //
                LayerHeights = layerHeights,
                SpanWidths = spanWidths,
            };
        }

        #endregion

        public Vertice GetFrameVertice(uint verticeId)
        {
            return FrameVertices.FirstOrDefault(r => r.ID == verticeId);
        }

        #region ---   模型检验

        /// <summary> 对模型进行检查，如果此模型不满足进行计算的必备条件，则返回false </summary>
        public bool Validate(ref StringBuilder errorMessage)
        {
            errorMessage.AppendLine("模型检验完成，可以进行计算");
            return true;
        }

        #endregion
    }
}
