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

        /// <summary> Ansys 正在计算过程中 </summary>
        Calculating = 1,

        // ------------------------------------------------

        /// <summary> Ansys 计算过程自行结束（不是由用户强制终止），但是不一定成功，也有可能是因为计算出错而结束 </summary>
        SelfFinished = 2,

        /// <summary> 用户强制终止计算 </summary>
        UserTerminated = 4,

        /// <summary> 用户强制终止计算时出现错误 </summary>
        UserTerninationFailed = 8,

        // ------------------------------------------------

        /// <summary> Ansys 计算自行完成而且计算结果正常成功 </summary>
        Succeeded = SelfFinished + 16 , // SelfFinished + Succeeded

        /// <summary> Ansys 计算自行完成，但是计算过程中出现错误 </summary>
        FailedWithError = SelfFinished + 32,  // SelfFinished + FailedWithError

        /// <summary> 在 C# 的代码中出现异常(Exception)而结束，而不是在 Ansys 的计算过程中出错而结束 </summary>
        FailedInCs = SelfFinished + 64,  // SelfFinished + FailedInCs

    }
}
