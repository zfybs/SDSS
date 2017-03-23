using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSS.Structures
{
    /// <summary>
    /// 矩形结构的计算简图
    /// </summary>
    public class SoilFrameGeometry : StationGeometry
    {
        #region ---   Properties

        /// <summary> 整个模型土体的宽度 </summary>
        public float SoilWidth { get; private set; }
        /// <summary> 整个模型每一层土体的厚度，数组中的第一个元素表示最上层 </summary>
        public float[] SoilHeight { get; private set; }

        /// <summary> 车站上覆土的厚度 </summary>
        public float OverlyingSoilHeight { get; private set; }

        /// <summary> 车站中每一层楼板的层高，数组中的第一个元素表示最上层 </summary>
        public float[] StationFloors { get; private set; }
        /// <summary> 车站中每一跨的宽度，数组中的第一个元素表示最左边一跨 </summary>
        public float[] StationSegments { get; private set; }

        #endregion

        #region ---   Fields

        /// <summary> 车站总宽度 </summary>
        private float _stationWidth;
        /// <summary> 车站总高度 </summary>
        private float _stationHeight;

        #endregion

        #region---   构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="soilWidth"></param>
        /// <param name="soilHeight"></param>
        /// <param name="overlyingSoilHeight">车站上覆土的厚度</param>
        /// <param name="stationFloors">车站中每一层楼板的层高，数组中的第一个元素表示最上层</param>
        /// <param name="stationSegments">车站中每一跨的宽度，数组中的第一个元素表示最左边一跨</param>
        public SoilFrameGeometry(float soilWidth, float[] soilHeight, float overlyingSoilHeight,
            float[] stationFloors, float[] stationSegments)
        {
            SoilWidth = soilWidth;
            SoilHeight = soilHeight;
            OverlyingSoilHeight = overlyingSoilHeight;
            StationFloors = stationFloors;
            StationSegments = stationSegments;
        }
        #endregion

        /// <summary>
        /// 检查车站模型几何参数是否能够形成一个有效的车站，而不会出现车站宽度大于土体宽度等问题
        /// </summary>
        /// <returns></returns>
        public override bool CheckGeometry()
        { return true; }

    }


    /// <summary>
    /// 圆形隧道的计算简图
    /// </summary>
    public class SoilTunnelGeometry : StationGeometry
    {
        #region ---   Properties

        /// <summary> 整个模型土体的宽度 </summary>
        public float SoilWidth { get; private set; }
        /// <summary> 整个模型每一层土体的厚度，，数组中的第一个元素表示最上层 </summary>
        public float[] SoilHeight { get; private set; }

        /// <summary> 隧道上覆土的厚度 </summary>
        public float OverlyingSoilHeight { get; private set; }


        /// <summary> 圆形隧道的半径 </summary>
        public float TunnelRadius { get; private set; }
        /// <summary> 圆形隧道的管片块数 </summary>
        public int TunnelSegmentNum { get; private set; }

        #endregion

        #region ---   Fields

        /// <summary> 隧道总宽度 </summary>
        private float _tunnelWidth;
        /// <summary> 隧道总高度 </summary>
        private float _tunnelHeight;

        #endregion

        #region---   构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="soilWidth"></param>
        /// <param name="soilHeight"></param>
        /// <param name="overlyingSoilHeight"> 车站上覆土的厚度 </param>
        /// <param name="tunnelRadius"> 圆形隧道的半径 </param>
        /// <param name="tunnelSegmentNum"> 圆形隧道的管片块数 </param>
        public SoilTunnelGeometry(float soilWidth, float[] soilHeight, float overlyingSoilHeight,
            float tunnelRadius, int tunnelSegmentNum)
        {
            SoilWidth = soilWidth;
            SoilHeight = soilHeight;
            OverlyingSoilHeight = overlyingSoilHeight;
            TunnelRadius = tunnelRadius;
            TunnelSegmentNum = tunnelSegmentNum;
        }
        #endregion

        /// <summary>
        /// 检查车站模型几何参数是否能够形成一个有效的车站，而不会出现车站宽度大于土体宽度等问题
        /// </summary>
        /// <returns></returns>
        public override bool CheckGeometry()
        { return true; }
    }
}