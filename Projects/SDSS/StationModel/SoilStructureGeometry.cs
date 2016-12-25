using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSS.StationModel
{
    /// <summary>
    /// 地层-结构时程分析法 计算简图的相关参数
    /// </summary>
    public class SoilStructureGeometry
    {
        #region ---   Properties

        /// <summary> 整个模型土体的宽度 </summary>
        public double SoilWidth { get; private set; }
        /// <summary> 整个模型每一层土体的厚度，，数组中的第一个元素表示最上层 </summary>
        public double[] SoilHeight { get; private set; }

        /// <summary> 车站上覆土的厚度 </summary>
        public double OverlyingSoilHeight { get; private set; }


        /// <summary> 车站中每一层楼板的层高，数组中的第一个元素表示最上层 </summary>
        public double[] StationFloors { get; private set; }
        /// <summary> 车站中每一跨的宽度，数组中的第一个元素表示最左边一跨 </summary>
        public double[] StationSegments { get; private set; }

        #endregion

        #region ---   Fields

        /// <summary> 车站总宽度 </summary>
        private double _stationWidth;
        /// <summary> 车站总高度 </summary>
        private double _stationHeight;

        #endregion

        /// <summary>
        /// 检查车站模型几何参数是否能够形成一个有效的车站，而不会出现车站宽度大于土体宽度等问题
        /// </summary>
        /// <returns></returns>
        public bool CheckGeometry()
        {

            return true;
        }
    }
}
