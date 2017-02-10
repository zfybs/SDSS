using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDSS.Definitions
{
    /// <summary>
    /// 几何单元
    /// </summary>
    public interface IGeomObject
    {
        /// <summary> 节点或者构件的 ID 编号 </summary>
        uint ID { get; set; }
    }
  
}
