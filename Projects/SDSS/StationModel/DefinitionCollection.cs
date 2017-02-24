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

        public DefinitionCollection()
        {
            Materials = new XmlList<Material>();
            //
            Profiles = new XmlList<Profile>();
            FrameVertices = new XmlList<FrameVertice>();
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
