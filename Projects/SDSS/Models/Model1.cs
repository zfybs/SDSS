using System;
using System.IO;
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
        
        /// <summary> 将模型信息写入一个文本文件中，用来作为 Ansys 计算的初始参数提供给 APDL 命令流 </summary>
        /// <param name="filePath">要写入的文件路径，此文件当前可以不存在</param>
        /// <param name="errMsg">出错信息</param>
        /// <returns>是否写入成功</returns>
        public override bool WriteCalculateFileForAnsys(string filePath, ref StringBuilder errMsg)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(ModelName);
                    sw.WriteLine(ID);
                    /* sw.WriteLine(@"
finish
/clear

Item='Weight'
data=234.56
Unit='Kg'
*Dim,AA,Array,4,1,1
AA(1)=10.2,324.5,123.7,908
*Dim,BB,Char,3,1,1
BB(1)='I am','a good','man'

*CFopen,result,txt

*vwrite,
(5X,'*Vwrite Demo')

*vwrite,
('******************************************************')

*vwrite,Item,data,Unit
(A8,F10.2,A8)

*vwrite
(/'*********** Array Parameter Output Demo**************')

*vwrite,
('Float Format /SEQU Keyword:')

*vwrite,SEQU,AA(1)
(F3.0,4F10.4)

*vwrite,AA(1),AA(2),AA(3),AA(4)
(//'Float /x Format:'/F4.1,2X,F10.4,2X,F10.4,2X,F10.4)

*vwrite,AA(1),AA(2),AA(3),AA(4)
(//'Float Format:'/4F10.4)

*vwrite,AA(1),AA(2),AA(3),AA(4)
(//'Double Format:'/D13.5,/D15.6,/D18.10,/D10.3)

*vwrite,
(/'****************char parametric output demo *******')

*vwrite,BB(1),BB(2),BB(3)
(3A6)

*CFclos
");
*/
                }
            }
            return true;
        }


        #region ---   几何绘图

        public override StationGeometry GetStationGeometry()
        {
            SoilFrameGeometry sfg = null;
            if (Frame != null)
            {
                sfg = new SoilFrameGeometry(
                    soilWidth: Frame.SpanWidths.Sum() * 3.0f,
                    soilHeight: SoilLayers.Select(r => r.Top - r.Bottom).ToArray(),
                    overlyingSoilHeight: MethodProperty.OverLayingSoilHeight,
                    stationFloors: Frame.LayerHeights.Select(r => (float)r).Reverse().ToArray(),
                    stationSegments: Frame.SpanWidths.Select(r => (float)r).ToArray());
            }

            // ssg = new SoilFrameGeometry(60, new float[] { 3, 6, 5, 4, 6, 6 }, 3, new float[] { 3, 3, 3 }, new float[] { 6, 6 });
            return sfg;
        }

        #endregion
    }
}