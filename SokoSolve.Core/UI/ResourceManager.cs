using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Core.DataModel;

namespace SokoSolve.Core.UI
{
    public class ResourceManager
    {
        public ResourceManager()
        {
            pResourcesID = new Hashtable();
            pResourcesName = new Hashtable();
            
        }

        public string getResourceFileName(Resource aResouce)
        {
            return FileManager.getContent(string.Format("{0}\\{1}\\{2}", DirectoryBase, DirectoryRelative, aResouce.Filename));
        }
        

        public void Load(string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            DirectoryBase = xmlDoc.DocumentElement.GetAttribute("Directory");
            Description = xmlDoc.DocumentElement.GetAttribute("Description");

            foreach (XmlNode xn in xmlDoc.DocumentElement.ChildNodes)
            {
                if (xn is XmlElement && xn.Name == "Resource")
                {
                    Resource res = new Resource(this, (XmlElement)xn);
                    Add(res.ID, res);
                }
            }

        }

        public void Add(Identity ID, Resource res)
        {
            pResourcesID.Add(ID, res);
            pResourcesName.Add(res.Name, res);
            
        }

        public Resource this[int id]
        {
            get
            {
                return this[new Identity(id)];
            }
        }

        public Resource this[Identity id]
        {
            get
            {
                return getResource(id, true);
            }
        }

        public Resource this[string aName]
        {
            get
            {
                return getResource(aName, true);
            }
        }

        public Resource getResource(Identity anID, bool isRequired)
        {
            Resource r = (Resource)pResourcesID[anID];
            if (isRequired && r == null) throw new ArgumentNullException(string.Format("Cannot find ID:{0}", anID.ID));
            return r;
        }

        public Resource getResource(string aName, bool isRequired)
        {
            Resource r = (Resource)pResourcesName[aName];
            if (isRequired && r == null) throw new ArgumentNullException(string.Format("Cannot find Name:{0}", aName));
            return r;
        }
        
        Hashtable pResourcesID;
        Hashtable pResourcesName;
        public string Description;
        public string DirectoryBase;
        public string DirectoryRelative;

    }
}
