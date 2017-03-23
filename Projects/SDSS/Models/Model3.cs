using SDSS.Structures;
using SDSS.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Methods;

namespace SDSS.Models
{
    [Serializable()]
    public class Model3: ModelBase
    {
        #region ---   XmlElement

        /// <summary> 模型所对应的框架结构 </summary>
        [XmlElement]
        public Tunnel Tunnel { get; set; }

        /// <summary> 与材料、水位标高等相关的系统参数 </summary>
        [XmlElement]
        public MethodInertial MethodProperty { get; set; }

        #endregion

        #region ---   构造函数

        /// <summary> 构造函数 </summary>
        public Model3() : base(ModelType.Tunnel, CalculationMethod.InertialForce)
        {
            DescriptionName = @"圆形隧道惯性力法";
            //
            Tunnel = new Tunnel();
            MethodProperty = new MethodInertial();
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

        /// <summary> 将模型信息写入一个文本文件中，用来作为 Ansys 计算的初始参数提供给 APDL 命令流 </summary>
        /// <param name="filePath">要写入的文件路径，此文件当前可以不存在</param>
        /// <param name="errMsg">出错信息</param>
        /// <returns>是否写入成功</returns>
        public override bool WriteCalculateFileForAnsys(string filePath, ref StringBuilder errMsg)
        {
            throw new NotImplementedException();
        }

        #region ---   几何绘图

        public override StationGeometry GetStationGeometry()
        {
            SoilTunnelGeometry stg = null;
            if (Tunnel != null)
            {
                stg = new SoilTunnelGeometry(
                    soilWidth: (float)Tunnel.Radius * 6.0f,
                    soilHeight: SoilLayers.Select(r => r.Top - r.Bottom).ToArray(),
                    overlyingSoilHeight: MethodProperty.OverLayingSoilHeight,
                    tunnelRadius: (float)Tunnel.Radius,tunnelSegmentNum:Tunnel.SegmentNum);
            }
            return stg;
        }

        #endregion
    }
}
