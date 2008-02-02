using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SokoSolve.Core.UI.ResourceTypes
{
    public class ResourceString : Resource
    {
        public ResourceString(ResourceFactory factory, ResourceType type, XmlElement xmlConfigData)
            : base(factory, type, xmlConfigData)
        {
            XmlElement xmlString = xmlConfigData["String"];
            if (xmlString == null) throw new Exception("Expected XML String tag");

            dataUnTyped = xmlString.InnerText;
        }

        public string String
        {
            get { return dataUnTyped as string; }
        }
    }
}
