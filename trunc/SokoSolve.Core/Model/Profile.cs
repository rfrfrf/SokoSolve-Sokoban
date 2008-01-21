using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SokoSolve.Common;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    /// <summary>
    /// Domain logic for the users profile
    /// </summary>
    public class Profile
    {
        public Profile()
        {
            simpleParams = new Dictionary<string, string>();
            currentXML = new SokoSolveProfile();
        }

        public string LibraryCurrentOpenDir
        {
            get { return GetGeneralParam("LibraryCurrentOpenDir"); }
            set { simpleParams["LibraryCurrentOpenDir"] = value; }
        }

        public string LibraryCurrentImportDir
        {
            get { return GetGeneralParam("LibraryCurrentImportDir"); }
            set { simpleParams["LibraryCurrentImportDir"] = value; }
        }

        public string LibraryLastFile
        {
            get { return GetGeneralParam("LibraryLastFile"); }
            set { simpleParams["LibraryLastFile"] = value; }
        }

        public string LibraryLastPuzzle
        {
            get { return GetGeneralParam("LibraryLastPuzzle"); }
            set { simpleParams["LibraryLastPuzzle"] = value; }
        }

        public string UserName
        {
            get { return GetGeneralParam("UserName"); }
            set { simpleParams["UserName"] = value; }
        }

        public string UserEmail
        {
            get { return GetGeneralParam("UserEmail"); }
            set { simpleParams["UserEmail"] = value; }
        }

        public string UserHomepage
        {
            get { return GetGeneralParam("UserHomepage"); }
            set { simpleParams["UserHomepage"] = value; }
        }

        public string UserLicense
        {
            get { return GetGeneralParam("UserLicense"); }
            set { simpleParams["UserLicense"] = value; }
        }

        /// <summary>
        /// Get the default author
        /// </summary>
        public GenericDescriptionAuthor UserAsGenericAuthor
        {
            get
            {
                GenericDescriptionAuthor author = new GenericDescriptionAuthor();
                author.Name = UserName;
                author.Email = UserEmail;
                author.Homepage = UserHomepage;
                return author;
            }
        }


        /// <summary>
        /// Get the default author
        /// </summary>
        public GenericDescription GenericDescription
        {
            get
            {
                GenericDescription desc = new GenericDescription();
                desc.DateSpecified = true;
                desc.Date = DateTime.Now;
                desc.License = UserLicense;
                desc.Author = UserAsGenericAuthor;
                return desc;
            }
        }

        public void Register(SokoSolveProfileLibrary newLib)
        {
            currentXML.LibraryRepository = ListHelper.AddToArray<SokoSolveProfileLibrary>(currentXML.LibraryRepository, newLib);
        }

        public void Load(string FileName)
        {
            using (FileStream stream = File.OpenRead(FileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(SokoSolveProfile));
                currentXML = (SokoSolveProfile) ser.Deserialize(stream);

                // Init this clas with the xml proxy

                // General Settings
                simpleParams.Clear();
                foreach (SokoSolveProfileSetting setting in currentXML.GeneralSettings)
                {
                    simpleParams.Add(setting.Name, setting.Value);
                }

                // Libraries
                // TODO

                // History
                // TODO
                
            }   
        }

        public void Save(string FileName)
        {
            // Sync this class to currentXML

            // General Settings
            currentXML.GeneralSettings = new SokoSolveProfileSetting[simpleParams.Count];
            string[] keys = new string[simpleParams.Keys.Count];
            simpleParams.Keys.CopyTo(keys, 0);
            for (int cc = 0; cc < simpleParams.Count; cc++)
            {
                currentXML.GeneralSettings[cc] = new SokoSolveProfileSetting();
                currentXML.GeneralSettings[cc].Name = keys[cc];
                currentXML.GeneralSettings[cc].Value = simpleParams[keys[cc]];

            }

            // Libraries
            // TODO: Update libraries

            // History
            // TODO: Convert History 

            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            using (FileStream stream = File.OpenWrite(FileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(SokoSolveProfile));
                ser.Serialize(stream, currentXML);
            }   
        }

        string GetGeneralParam(string parmName)
        {
            if (!simpleParams.ContainsKey(parmName)) return null;
            return simpleParams[parmName];
        }

        private Dictionary<string, string> simpleParams;
        private SokoSolveProfile currentXML;

    }
}

