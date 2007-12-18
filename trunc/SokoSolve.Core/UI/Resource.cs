using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Drawing;
using SokoSolve.Core.DataModel;

namespace SokoSolve.Core.UI
{
    public enum ResourceType
    {
        Image,
        Animation,
        SoundEffect
    }

    public class Resource
    {
        public Resource()
        {
        }

        public Resource(ResourceManager aManager, XmlElement myElement)
        {
            pManager = aManager;
            string id = myElement.GetAttribute("ID");
            if (id != null && id.Length > 0) ID = new Identity(XmlConvert.ToInt32(id));
            Name = myElement.GetAttribute("Name");
            Filename = myElement.GetAttribute("Filename");
            string t = myElement.GetAttribute("ResourceType");
            if (t != null && t.Length > 0) Type = (ResourceType)Enum.Parse(typeof(ResourceType), t);
        }

        public string FullPath
        {
            get
            {
                return pManager.getResourceFileName(this);
                
            }
        }

        public Image LoadBitmap()
        {
            if (i == null) i = Bitmap.FromFile(FullPath);
            return i;
        }

        public Identity ID;
        public string Name;
        public string Filename;
        public ResourceType Type;
        public ResourceManager pManager;
        

        Image i;
    }
}
