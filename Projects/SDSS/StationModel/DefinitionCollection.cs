using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using eZstd.Enumerable;
using SDSS.Definitions;
using SDSS.Entities;

namespace SDSS.StationModel
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

        /// <summary> 整个框架中所有的矩形框架节点定义 </summary>
        [XmlElement]
        public XmlList<FrameVertice> FrameVertices { get; set; }

        #region ---   构造函数

        private static DefinitionCollection _uiniqueInstance;
        public static DefinitionCollection GetUniqueInstance()
        {
            _uiniqueInstance = _uiniqueInstance ?? new DefinitionCollection();
            return _uiniqueInstance;
        }

        /// <summary>
        /// 此构造函数可以直接声明为 private，在反序列化时 Visual Studio 仍然会将其作为 public 并执行其中的代码。
        /// </summary>
        private DefinitionCollection()
        {
            //
            Materials = new XmlList<Material>();
            Profiles = new XmlList<Profile>();
            FrameVertices = new XmlList<FrameVertice>();

            // 这一句必须保留，因为在序列化时会直接进行此处的 public 构造函数，而不会从 public static DefinitionCollection GetUniqueInstance() 进入。
            // 此时必须通过这一句保证 _uiniqueInstance 与本全局对象的同步。
            _uiniqueInstance = this;
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
        public Vertice GetFrameVertice(uint verticeId)
        {
            return FrameVertices.FirstOrDefault(r => r.ID == verticeId);
        }
        #endregion

    }
}
