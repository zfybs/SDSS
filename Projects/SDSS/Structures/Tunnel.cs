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
    public class Tunnel
    {
        #region ---   XmlElement

        /// <summary> 用来在每次反序列化<see cref="ModelBase"/>对象时，用来根据<see cref="Component"/>的节点编号字符来返回对应的材料等。 </summary>
        [XmlIgnore]
        public static Tunnel ActiveTunnelForDeserialize { get; private set; }

        /// <summary> 整个隧道中所有的管片节点定义 </summary>
        [XmlElement]
        public XmlList<TunnelVertice> TunnelVertices { get; set; }

        /// <summary> 整个隧道中所有的管片构件 </summary>
        [XmlElement]
        public XmlList<Segment> Segments { get; set; }

        #endregion

        #region ---   XmlAttribute
        [XmlAttribute]
        public int SegmentNum { get; set; }

        [XmlAttribute]
        public double Radius { get; set; }


        #endregion

        public Tunnel()
        {

            TunnelVertices = new XmlList<TunnelVertice>();
            Segments = new XmlList<Segment>();
            //
            SegmentNum = new int();
            Radius = new double();
            //
            ActiveTunnelForDeserialize = this;
        }

        #region ---   构造圆形隧道
        private double _startAngle;
        /// <summary>
        /// 根据隧道半径、管片数、材料、截面生成一个圆形隧道
        /// </summary>
        /// <param name="radius"> 圆形隧道的半径 </param>
        /// <param name="segmentNum"> 管片的分块数 </param>
        /// <param name="defaultMat"> 管片的材料 </param>
        /// <param name="defaultProfile"> 管片的截面 </param>
        /// <returns></returns>
        public static Tunnel Create(double radius, int segmentNum, Material defaultMat, Profile defaultProfile)
        {
            //
            var tunnelVertices = new XmlList<TunnelVertice>();
            var verticesColle = tunnelVertices;
            //构造节点系统
            double[] Angles = new double[segmentNum+1];
            double _angle = 360 / segmentNum;
            for (int i = 0; i < segmentNum+1; i++)
            {
                double _angleI = -90 + _angle * (i - 0.5);
                Angles[i]= _angleI;
                verticesColle.Add(new TunnelVertice(radius, _angleI));
            }

            //根据节点对象生成管片构件
            var segments = new XmlList<Segment>();
            for (int i = 0; i < segmentNum; i++)
            {
                Segment _seg = new Segment(material: defaultMat, profile: defaultProfile, v1: verticesColle[i],v2:verticesColle[i+1]);
                _seg.LocationTag = $"({_seg.Vertice1.Angle},{_seg.Vertice2.Angle})";
                segments.Add(_seg);
            }

            return new Tunnel()
            {
                TunnelVertices = tunnelVertices,
                Segments = segments,
                //
                SegmentNum = segmentNum,
                Radius = radius,
                _startAngle = -90 - 0.5 * 360 / segmentNum

            };
        }

        #endregion

        public TunnelVertice GetTunnelVertice(uint verticeId)
        {
            return TunnelVertices.FirstOrDefault(r => r.ID == verticeId);
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
