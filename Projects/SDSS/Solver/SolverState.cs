using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSS.Solver
{
    /// <summary>
    /// 求解器的状态
    /// </summary>
    [Flags]
    internal enum SolverState
    {
        /// <summary> 求解器还未开始计算 </summary>
        NotStarted = 0,

        /// <summary> Abaqus 正在计算过程中 </summary>
        Calculating = 1,

        // ------------------------------------------------

        /// <summary> Abaqus 计算过程自行结束（不是由用户强制终止），但是不一定成功，也有可能是因为计算出错而结束 </summary>
        SelfFinished = 2,

        /// <summary> 用户强制终止计算 </summary>
        UserTerminated = 4,

        /// <summary> 用户强制终止计算时出现错误 </summary>
        UserTerninationFailed = 8,

        // ------------------------------------------------

        /// <summary> Abaqus 计算自行完成而且计算结果正常成功 </summary>
        Succeeded = 18, // Finished + Succeeded

        /// <summary> Abaqus 计算自行完成，但是计算过程中出现错误 </summary>
        FailedWithError = 34,  // Finished + FailedWithError
    }
}
