using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using SDSS.Entities;

namespace SDSS.Methods
{
    /// <summary> 与惯性力法相关的参数或方法 </summary>
    [Serializable()]
    public class MethodInertial : ICloneable
    {
        #region ---  BoundaryParam中的边界参数

        /// <summary> 土层的水平基床系数 </summary>
        [XmlAttribute()]
        public float Kx { get; set; }

        /// <summary> 土层的竖向基床系数 </summary>
        [XmlAttribute()]
        public float Ky { get; set; }

        /// <summary> 矩形地铁车站结构等代水平地震惯性力系数，用来计算作用在框架节点上的惯性力 </summary>
        [XmlAttribute()]
        public float Kc { get; set; }

        /// <summary> 地表的标高 </summary>
        [XmlAttribute()]
        public float TopElevation { get; set; }

        /// <summary> 上覆土厚度 </summary>
        [XmlAttribute()]
        public float OverLayingSoilHeight { get; set; }

        #endregion

        #region ---   构造函数

        public MethodInertial()
        {
            // 给出一些默认值
            Kx = 1.0e6f;
            Ky = 1.0e6f;
            Kc = 0.5f;
        }

        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        /// <summary>
        /// 根据每一层土的Kci0值计算出整个模型所对应的惯性力系数值
        /// </summary>
        /// <param name="importantSoilLayers"> 自地表起地铁车站周围对结构地震反应有较大影响的土层，第一个元素表示最上面的土层 </param>
        public void GetKc(IEnumerable<SoilLayer_Inertial> importantSoilLayers)
        {
            var bh = 1.00 - 0.0093 * OverLayingSoilHeight;
            float H = importantSoilLayers.Select(r => r.Top - r.Bottom).Sum();
            //
            float kc = 0;
            float hi;
            foreach (var s in importantSoilLayers)
            {
                hi = s.Top - s.Bottom;
                var kci = (float)(s.Kci0 * bh);
                kc += hi / H * kci;
            }

            Kc = kc;

            //kc = 0;
        }
    }
}