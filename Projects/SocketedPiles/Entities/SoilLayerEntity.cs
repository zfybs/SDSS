using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SocketedShafts.Definitions;

namespace SocketedShafts.Entities
{
    /// <summary> 某具体的一段土层的信息 </summary>
    [Serializable()]
    public class SoilLayerEntity : Entity
    {
        #region ---   Properties

        // 基本信息

        private SoilLayer _layer;
        /// <summary> 这一段土层的材料信息 </summary>
        [XmlElement()]
        public SoilLayer Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }

        #endregion

        /// <summary> 构造函数 </summary>
        public SoilLayerEntity()
        {
        }
    }
}