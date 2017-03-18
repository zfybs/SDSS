using System;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Definitions;
using SDSS.Methods;
using SDSS.Structures;

namespace SDSS.Models
{
    [Serializable()]
    public class Model1 : ModelBase
    {
        #region ---   XmlElement

        /// <summary> 模型所对应的框架结构 </summary>
        [XmlElement]
        public Frame Frame { get; set; }
        
        /// <summary> 与材料、水位标高等相关的系统参数 </summary>
        [XmlElement]
        public MethodInertial MethodProperty { get; set; }

        #endregion

        #region ---   构造函数

        /// <summary> 构造函数 </summary>
        public Model1() : base(ModelType.Frame, CalculationMethod.InertialForce)
        {
            DescriptionName = @"矩形车站惯性力法";
            //
            MethodProperty = new MethodInertial();
            Frame = new Frame();
        }

        #endregion

        #region ---   模型检验

        /// <summary> 对模型进行检查，如果此模型不满足进行计算的必备条件，则返回false </summary>
        public override bool Validate(ref StringBuilder errorMessage)
        {
            errorMessage.AppendLine("模型检验完成，可以进行计算");
            return true;

            //if (string.IsNullOrEmpty(sss.SocketedShaft.Name))
            //{
            //    errorMessage = "桩未命名";
            //    return false;
            //}
            //if (!ValidateConnectivity(sss.SoilLayers, out errorMessage))
            //{
            //    errorMessage = "土层连续性不满足： " + errorMessage;
            //    return false;
            //}
            //if (!ValidateConnectivity(sss.SocketedShaft.Sections, out errorMessage))
            //{
            //    errorMessage = "桩段连续性不满足： " + errorMessage;
            //    return false;
            //}
        }

        #endregion

        #region ---   几何绘图

        public override StationGeometry GetStationGeometry()
        {
            SoilStructureGeometry ssg = null;
            if (Frame != null)
            {
                ssg = new SoilStructureGeometry(
                    soilWidth: Frame.SpanWidths.Sum()*3.0f,
                    soilHeight: SoilLayers.Select(r => r.Top - r.Bottom).ToArray(),
                    overlyingSoilHeight: MethodProperty.OverLayingSoilHeight,
                    stationFloors: Frame.LayerHeights.Select(r => (float) r).Reverse().ToArray(),
                    stationSegments: Frame.SpanWidths.Select(r => (float) r).ToArray());
            }

            // ssg = new SoilStructureGeometry(60, new float[] { 3, 6, 5, 4, 6, 6 }, 3, new float[] { 3, 3, 3 }, new float[] { 6, 6 });
            return ssg;
        }

        #endregion
    }
}