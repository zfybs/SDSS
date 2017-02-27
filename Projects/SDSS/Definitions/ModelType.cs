using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSS.Definitions
{
    /// <summary> 不同的计算对象，比如 矩形车站、隧道、矿山等 </summary>
    [Serializable]
    public enum ModelType
    {
        /// <summary> 矩形框架 </summary>
        Frame = 0,

        /// <summary> </summary>
        Model2 = 1,

        /// <summary> </summary>
        Model3 = 2,
    }

    /// <summary> 不同的计算方法，比如反应位移法，等效加速度法、惯性力法等 </summary>
    [Serializable]
    public enum CalculationMethod
    {
        /// <summary> 惯性力法 </summary>
        InertialForce = 0,

        /// <summary> </summary>
        FanYingWeiYi = 1,

        /// <summary> </summary>
        Method3 = 2,
        /// <summary> </summary>
        Method4 = 3,
    }
}
