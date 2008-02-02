using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

namespace SokoSolve.Core.UI.ResourceTypes
{
    public class ResourceFont : Resource
    {
        public ResourceFont(ResourceFactory factory, ResourceType type, XmlElement xmlConfigData): base(factory, type, xmlConfigData)
        {
            XmlElement xmlFont = xmlConfigData["Font"];
            if (xmlFont == null) throw new Exception("Expected XML Font tag");

            dataUnTyped = new Font(xmlFont.GetAttribute("Name"),
                float.Parse(xmlFont.GetAttribute("Size")),
                (FontStyle)Enum.Parse(typeof(FontStyle), xmlFont.GetAttribute("Style")));
        }

        public Font Font
        {
            get { return dataUnTyped as Font; }
        }
    }
}
