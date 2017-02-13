using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using SocketedShafts.Definitions;

namespace SocketedShafts.Entities
{
    /// <summary> 一根桩的一小截桩段 </summary>
    [Serializable()]
    public class ShaftSectionEntity:Entity
    {
        #region ---   Properties


        /// <summary> 此一小段桩的截面信息 </summary>
        [XmlElement()]
        public ShaftSection Section { get; set; }

        #endregion


        /// <summary> 构造函数 </summary>
        public ShaftSectionEntity()
        {
        }
    }
}