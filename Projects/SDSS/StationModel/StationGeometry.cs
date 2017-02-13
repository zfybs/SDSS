using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSS.StationModel
{
    public abstract class StationGeometry
    {
        /// <summary>
        /// 检查车站模型几何参数是否能够形成一个有效的车站，而不会出现车站宽度大于土体宽度等问题
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckGeometry();
    }
}
