using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Definitions;

namespace SDSS.Entities
{
    public interface IGeomObject
    {
        /// <summary> 节点或者构件的 ID 编号 </summary>
        uint ID { get; set; }
    }

    /// <summary> 每一个Abaqus仓库中的对象  </summary>
    [Serializable()]
    public class AbqEntity
    {
        public string Name;
        /// <summary> 构造函数  </summary>
        public AbqEntity(string name)
        {
            Name = "AbaqusEntity";
        }
    }

}
