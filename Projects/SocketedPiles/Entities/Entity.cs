using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SocketedShafts.Definitions;

namespace SocketedShafts.Entities
{
    /// <summary> 桩段或者土层段 </summary>
    [Serializable()]
    public class Entity
    {
        #region ---   Properties

        // 基本信息

        /// <summary> 此一小段桩的顶部绝对标高 </summary>
        [XmlAttribute()]
        public float Top { get; set; }

        /// <summary> 此一小段桩的底部绝对标高 </summary>
        [XmlAttribute()]
        public float Bottom { get; set; }

        #endregion

        /// <summary> 构造函数 </summary>
        public Entity()
        {
        }
        
        /// <summary> 这一小段的实体的中间位置标高 </summary>
        public float GetMiddle()
        {
            return (Top + Bottom) / 2;
        }
    }
}