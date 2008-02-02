using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

namespace SokoSolve.Core.UI.ResourceTypes
{
  
    public class ResourceBrush : Resource
    {
        public ResourceBrush(ResourceFactory factory, ResourceType type, XmlElement xmlConfigData)
            : base(factory, type, xmlConfigData)
        {
            XmlElement xmlSolidBrush = xmlConfigData["SolidBrush"];
            if (xmlSolidBrush != null)
            {
                dataUnTyped = new SolidBrush(ColorFromXML(xmlSolidBrush));
            }

            
        }

        private Color ColorFromXML(XmlElement xmlElement)
        {
            if (xmlElement.HasAttribute("ColorName"))
            {
                return Color.FromName(xmlElement.GetAttribute("ColorName"));
            }
            else if (xmlElement.HasAttribute("Color"))
            {
                string clr = xmlElement.GetAttribute("Color");
                if (clr.Length == 7 && clr[0] =='#')
                {
                    // RGB
                    int r = int.Parse(clr.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                    int g = int.Parse(clr.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                    int b = int.Parse(clr.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                    return Color.FromArgb(r, g, b);
                } 
                else if (clr.Length == 9 && clr[0] == '#')
                {
                    // RGB
                    int a = int.Parse(clr.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                    int r = int.Parse(clr.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                    int g = int.Parse(clr.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                    int b = int.Parse(clr.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);
                    return Color.FromArgb(a, r, g, b);
                }
            }
            
            throw new Exception("Unknown colour XML");
            
        }


        public Brush Brush
        {
            get { return dataUnTyped as Brush; }
        }
    }
}
