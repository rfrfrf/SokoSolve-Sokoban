using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Drawing;
using SokoSolve.Common;
using SokoSolve.Core.DataModel;

namespace SokoSolve.Core.UI
{
    public enum ResourceType
    {
        Image,
        Animation,
        String,
        Bool,
        Int32,
        Float,
        Font,
        Brush,
        Pen,
        Colour,
        VectorInt,
        FloatInt,
        SoundEffect,
        File,

        Other
    }


    /// <summary>
    /// Unlike standard .NET resource which are statically compiled in
    /// this simple resource system is dynamic and my be swapped at
    /// run-time.
    /// </summary>
    public interface IDynamicResource
    {
        Identity Identity { get;}
        string Name { get; }
        object DataUnTyped { get; }
    }
  

    /// <summary>
    /// Simple resource handle.
    /// It is responsible for loading the resource into memory. Basic type (image, file, string, etc) are handled by this base type
    /// to allow strong-typeing and quick access.
    /// </summary>
    public abstract class Resource : IDynamicResource
    {
        private Identity identity;
        private string name;
        private string description;
        private ResourceType type;
        protected object dataUnTyped;
        private XmlElement xmlConfigData;
        private ResourceFactory factory;

        /// <summary>
        /// Build from XML based on type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xmlConfigData"></param>
        protected Resource(ResourceFactory factory, ResourceType type, XmlElement xmlConfigData)
        {
            this.type = type;
            this.xmlConfigData = xmlConfigData;
            this.factory = factory;

            identity = new Identity(int.Parse(xmlConfigData.GetAttribute("ID")));
            name = xmlConfigData.GetAttribute("Name");
            if (xmlConfigData.HasAttribute("Description"))
            {
                description = xmlConfigData.GetAttribute("Description");
            }
        }

        public Identity Identity
        {
            get { return identity; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public ResourceType Type
        {
            get { return type; }
        }

        public object DataUnTyped
        {
            get { return dataUnTyped; }
        }

        public Image DataAsImage
        {
            get
            {
                Image ret = dataUnTyped as Image;
                if (ret == null) throw new InvalidCastException("Excpected Image");
                return ret;
            }
        }

        public Font DataAsFont
        {
            get
            {
                Font ret = dataUnTyped as Font;
                if (ret == null) throw new InvalidCastException("Expected Font");
                return ret;
            }
        }

        public string DataAsString
        {
            get
            {
                String ret = dataUnTyped as String;
                if (ret == null) throw new InvalidCastException("Expected String");
                return ret;
            }
        }

        public ISoundHandle DataAsSound
        {
            get
            {
                ISoundHandle ret = dataUnTyped as ISoundHandle;
                if (ret == null) throw new InvalidCastException("Expected ISoundHandle");
                return ret;
            }
        }


        public string[] DataAsStringArray
        {
            get
            {
                String ret = dataUnTyped as String;
                if (ret == null) throw new InvalidCastException("Expected String");
                string[] lines = StringHelper.Split(ret, Environment.NewLine);
                for (int cc = 0; cc < lines.Length; cc++)
                {
                    lines[cc] = lines[cc].Trim();
                }
                return lines;
            }
        }

        public Brush DataAsBrush
        {
            get
            {
                Brush ret = dataUnTyped as Brush;
                if (ret == null) throw new InvalidCastException("Expected Brush");
                return ret;
            }
        }
    }
}

