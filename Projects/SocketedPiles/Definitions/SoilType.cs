using System;

namespace SocketedShafts.Definitions
{
    /// <summary> 水下土层或者岩石层的类型 </summary>
    [Serializable()]
    public enum SoilType : byte
    {
        Clay = 0,
        Sand = 1,
        RockSmooth = 2,
        RockRough = 3,
    }
}