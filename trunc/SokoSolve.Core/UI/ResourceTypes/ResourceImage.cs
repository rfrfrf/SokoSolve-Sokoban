using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

namespace SokoSolve.Core.UI.ResourceTypes
{
    public class ResourceImage : Resource
    {
        public ResourceImage(ResourceFactory factory, ResourceType type, XmlElement xmlConfigData): base(factory, type, xmlConfigData)
        {
            XmlElement xmlImage = xmlConfigData["Image"];
            if (xmlImage == null) throw new Exception("Expected XML Image tag");
            if (!xmlImage.HasAttribute("FileName")) throw new Exception("Image Xml Element must have FileName attribute");

            dataUnTyped = Image.FromFile(factory.BuildFullPath(xmlImage.GetAttribute("FileName")));
        }

        public Image Image
        {
            get { return dataUnTyped as Image; }
        }
    }
}
