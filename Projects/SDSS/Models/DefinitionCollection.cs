using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using eZstd.Enumerable;
using SDSS.Definitions;
using SDSS.Entities;

namespace SDSS.Models
{
    /// <summary> 整个系统中所有材料、截面等定义的集合 </summary>
    [Serializable()]
    public class DefinitionCollection
    {
        /// <summary> 整个系统中所有的材料定义 </summary>
        [XmlElement]
        public XmlList<Material> Materials { get; set; }

        /// <summary> 整个系统中所有的横截面定义 </summary>
        [XmlElement]
        public XmlList<Profile> Profiles { get; set; }
        
        #region ---   构造函数


        /// <summary> 用来在每次反序列化<see cref="ModelBase"/>对象时，用来根据<see cref="Component"/>的材料名称字符来返回对应的材料等。 </summary>
        [XmlIgnore]
        public static DefinitionCollection ActiveDefiCollForDeserialize { get; private set; }

        /// <summary>
        /// 此构造函数可以直接声明为 private，在反序列化时 Visual Studio 仍然会将其作为 public 并执行其中的代码。
        /// </summary>
        public DefinitionCollection()
        {
            //
            Materials = new XmlList<Material>();
            Profiles = new XmlList<Profile>();

            // 这一句必须保留，因为在序列化时会直接进行此处的 public 构造函数，而不会从 public static DefinitionCollection GetUniqueInstance() 进入。
            // 此时必须通过这一句保证 _uiniqueInstance 与本全局对象的同步。
            //_uiniqueInstance = this;
            //
            ActiveDefiCollForDeserialize = this;
        }

        #endregion


        #region ---   元素索引

        public Material GetMaterial(string matName)
        {
            return Materials.FirstOrDefault(r => r.Name == matName);
        }
        public Profile GetProfile(string profileName)
        {
            return Profiles.FirstOrDefault(r => r.Name == profileName);
        }
    
        #endregion

    }
}
