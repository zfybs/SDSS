using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDSS.Constants;
using SDSS.Project;

namespace SDSS.Definitions
{
    /// <summary> 水中桩段或者嵌岩桩段的截面参数 </summary>
    [Serializable()]
    [XmlInclude(typeof(Material))]
    [XmlInclude(typeof(Profile))]
    public class Definition : ICloneable
    {
        #region ---   XmlAttribute
        [XmlAttribute()]
        [Category(Categories.Tag), Description("定义对象的名称")]
        public string Name { get; set; }

        [XmlIgnore]
        private Guid _id;
        [XmlElement]
        [Browsable(true), ReadOnly(true), Category(Categories.Tag), Description("定义对象的唯一标识符")]
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        [XmlIgnore()]
        [Category(Categories.Tag)]
        public Definition Self
        {
            get { return this; }
        }

        /// <summary> 构造函数 </summary>
        public Definition()
        {
            _id = Guid.NewGuid();
            Name = _id.ToString();
        }

        public override bool Equals(object obj)
        {
            Definition def = obj as Definition;
            if (def == null)
            {
                return false;
            }
            return this.Name.Equals(def.Name) && (this.ID == def.ID);
        }

        /// <summary> 返回一个<seealso cref="Definition"/>对象 </summary>
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