using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSS.Solver
{
    /// <summary>
    /// 求解器的状态
    /// </summary>
    internal enum SolverState
    {
        /// <summary> 求解器还未开始计算 </summary>
        NotStarted = 0,
        /// <summary> Abaqus 正在计算过程中 </summary>
        Calculating = 1,
        /// <summary> Abaqus 计算完成 </summary>
        Succeeded = 2,
        /// <summary> 用户强制终止计算 </summary>
        UserTerminated = 3,
        /// <summary> 用户强制终止计算时出现错误 </summary>
        UserTerninationFailed = 4,
        /// <summary> Abaqus 计算过程中出现错误 </summary>
        FailedWithError = 5,
    }
}
