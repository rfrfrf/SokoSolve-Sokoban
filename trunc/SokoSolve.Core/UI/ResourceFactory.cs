using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.FactoryPattern;
using SokoSolve.Core.DataModel;
using SokoSolve.Core.UI.ResourceTypes;

namespace SokoSolve.Core.UI
{
    public class ResourceFactory : FactoryStrict<Resource, Identity>
    {
        /// <summary>
        /// Construct the resources from an XML File
        /// </summary>
        /// <param name="FileName"></param>
        public ResourceFactory(string FileName, ISoundSubSystem soundSubSystem)
        {
            this.soundSubSystem = soundSubSystem;
            if (soundSubSystem == null) throw new ArgumentNullException("SoundSubSystem");
            if (string.IsNullOrEmpty(FileName)) throw new ArgumentNullException("FileName");

            FileName = CleanPath(FileName);


            baseDirectory = Path.GetDirectoryName(FileName);
            try
            {
                LoadFromXML(FileName);
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to load and init the resource file: " + FileName, ex);
            }
        }

        /// <summary>
        /// Factory lookup from a resource enum, return the full resource
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public Resource this[ResourceID resourceID]
        {
            get
            {
                return GetInstance(new Identity((int) resourceID));
            }
        }

        /// <summary>
        /// Return the resource manager base directory
        /// </summary>
        public string BaseDirectory
        {
            get { return baseDirectory; }
        }

        /// <summary>
        /// Sound System
        /// </summary>
        public ISoundSubSystem SoundSubSystem
        {
            get { return soundSubSystem; }
        }

        /// <summary>
        /// Clean a path string:
        /// Convert all seperators to "\"
        /// Remove any trailing "\"
        /// </summary>
        /// <param name="pathToClean"></param>
        /// <returns></returns>
        public static string CleanPath(string pathToClean)
        {
            string res = pathToClean.Replace('/', '\\');
            res = res.Trim();
            while(res.EndsWith("\\"))
            {
                // Remove last char
                res = res.Remove(res.Length - 1, 1);
            }
            return res;
        }

        /// <summary>
        /// A relative filename, there is not path, it will use the base directory
        /// if path starts with "$" then "$" will be replaced by the base directory
        /// </summary>
        /// <param name="relFileName">$ means relative to base directory</param>
        /// <returns></returns>
        public string BuildFullPath(string relFileName)
        {
            if (relFileName.StartsWith("$"))
            {
                return baseDirectory + "\\" + relFileName.Remove(0, 1);
            }
            else
            {
                if (relFileName.IndexOf("\\") >= 0)
                {
                    // Full path
                    return CleanPath(relFileName);
                }
                else
                {
                    // File Only
                    return baseDirectory + "\\" + relFileName;
                }
            }
        }

        /// <summary>
        /// Load resources from XML file
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadFromXML(string fileName)
        {
            XmlDocument xmlRes = new XmlDocument();
            xmlRes.Load(fileName);

            if (xmlRes.DocumentElement.LocalName != "ResourceLibrary") throw new Exception("Expected XML Node ResourceLibrary");

            if (xmlRes.DocumentElement.HasAttribute("BaseDir"))
            {
                string baseDir = xmlRes.DocumentElement.GetAttribute("BaseDir");
                if (!string.IsNullOrEmpty(baseDir))
                {
                    baseDir = CleanPath(baseDir);
                    if (baseDir == ".")
                    {
                        // do nothing leave default base dir
                    }
                    else
                    {
                        if (Directory.Exists(baseDir))
                        {
                            baseDirectory = baseDir;
                        }
                        else
                        {
                            throw new Exception("Cannot find base directory: " + baseDir);
                        }
                    }
                }
            }

            foreach (XmlNode xmlNode in xmlRes.DocumentElement.ChildNodes)
            {
                XmlElement xmlResource = xmlNode as XmlElement;
                if (xmlResource != null && xmlResource.LocalName == "Resource")
                {
                    try
                    {
                        Identity id = new Identity(int.Parse(xmlResource.GetAttribute("ID")));
                        ResourceType type = (ResourceType)Enum.Parse(typeof(ResourceType), xmlResource.GetAttribute("Type"));

                        switch (type)
                        {
                            case (ResourceType.Image):  Init(new ResourceImage(this, type, xmlResource), id); break;
                            case (ResourceType.String):   Init(new ResourceString(this, type, xmlResource), id); break;
                            case (ResourceType.Font):      Init(new ResourceFont(this, type, xmlResource), id); break;
                            case (ResourceType.Brush): Init(new ResourceBrush(this, type, xmlResource), id); break;
                            case (ResourceType.SoundEffect): Init(new ResourceSFX(this, type, xmlResource), id); break;

                            default:
                                throw new Exception("Unknown type" + type.ToString());
                        }    
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Unable to load Resource: " + xmlResource.OuterXml, ex);
                    }
                    
                }
            }

            CreationSealed = true;
        }

        private string baseDirectory;
        private ISoundSubSystem soundSubSystem;
    }
}
