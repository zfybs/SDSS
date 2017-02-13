using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SocketedShafts.Definitions
{
    /// <summary> 水中桩段或者嵌岩桩段的截面参数 </summary>
    [Serializable()]
    public class Definition : ICloneable
    {
        [XmlAttribute()]
        [Category(Categories.Tag), Description("桩截面的名称")]
        public string Name { get; set; }

        [XmlIgnore]
        private Guid _id;
        [XmlElement]
        [ReadOnly(true)]
        [Category(Categories.Tag), Description("定义对象的唯一标识符")]
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [Category(Categories.Tag)]
        public Definition Self
        {
            get { return this; }
        }

        /// <summary> 构造函数 </summary>
        public Definition()
        {
            _id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            return _id.Equals((obj as Definition).ID);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
