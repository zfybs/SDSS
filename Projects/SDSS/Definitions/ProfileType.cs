using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SDSS.Definitions
{
    /// <summary> 材料类型  </summary>
    [Serializable()]
    public enum MaterialType
    {
        /// <summary> 弹性  </summary>
        Elastic = 0,
        /// <summary>  摩尔库仑 </summary>
        MohrCoulomb = 1,
    }

    /// <summary> 构件截面类型  </summary>
    [Serializable()]
    public enum ProfileType
    {
        /// <summary> 矩形  </summary>
        Rectangular = 0,
        /// <summary> T 形  </summary>
        T = 1,
    }

    /// <summary> 构件类型  </summary>
    [Serializable()]
    public enum ComponentType
    {
        /// <summary> 梁  </summary>
        Beam = 0,
        /// <summary> 柱  </summary>
        Column = 1,
    }

    /// <summary> 构件几何类型  </summary>
    [Serializable()]
    public enum ComponentGeomType
    {
        /// <summary> 线型构件  </summary>
        Line = 0,
        /// <summary> 圆弧形  </summary>
        Arc = 1,
    }
}