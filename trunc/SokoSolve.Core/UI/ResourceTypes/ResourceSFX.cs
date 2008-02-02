using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SokoSolve.Core.UI.ResourceTypes
{
    public class ResourceSFX : Resource
    {

        public ResourceSFX(ResourceFactory factory, ResourceType type, XmlElement xmlConfigData)
            : base(factory, type, xmlConfigData)
        {
            XmlElement xmlSound = xmlConfigData["Sound"];
            if (xmlSound == null) throw new Exception("Expected XML Sound tag");
            if (!xmlSound.HasAttribute("FileName")) throw new Exception("Sound Xml Element must have FileName attribute");

            dataUnTyped = factory.SoundSubSystem.GetHandle(factory.BuildFullPath(xmlSound.GetAttribute("FileName")));
        }

        public ISoundHandle Sound
        {
            get { return dataUnTyped as ISoundHandle; }
        }
    }
}
