using System;
using System.Text;
using System.Xml.Serialization;
using SDSS.Definitions;
using SDSS.Structures;

namespace SDSS.Models
{
    [Serializable()]
    public class Model2 : ModelBase
    {
        #region ---   XmlElement

        /// <summary> 模型所对应的框架结构 </summary>
        [XmlElement]
        public Frame Frame { get; set; }

        #endregion

        #region ---   构造函数

        /// <summary> 构造函数 </summary>
        public Model2() : base(ModelType.Frame, CalculationMethod.FanYingWeiYi)
        {
            DescriptionName = @"矩形车站反应位移法";
            //
            Frame = new Frame();
        }

        #endregion

        #region ---   模型检验

        /// <summary> 对模型进行检查，如果此模型不满足进行计算的必备条件，则返回false </summary>
        public override bool Validate(ref StringBuilder errorMessage)
        {
            errorMessage.AppendLine("模型检验完成，可以进行计算");
            return true;
        }

        #endregion

        #region ---   几何绘图

        public override StationGeometry GetStationGeometry()
        {
            SoilStructureGeometry ssg = null;
            return ssg;
        }

        #endregion
    }
}