using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SokoSolve.Core;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.UI
{
    /// <summary>
    /// Controller for the UI
    /// </summary>
    class ProfileController
    {
        /// <summary>
        /// Current profile
        /// </summary>
        static public Profile Current
        {
            get
            {
                return current;
            }
        }

        /// <summary>
        /// Load the profile from the user directory
        /// </summary>
        /// <param name="UserAppDir"></param>
        static public void Init(string UserAppDir)
        {
            string profilePath = UserAppDir + "\\SokoSolveProfile.xml";

            if (File.Exists(profilePath))
            {
                current = new Profile();
                current.Load(profilePath);    
            }
            else
            {
                current = new Profile();
                current.UserName = "Anonymous";
                current.UserEmail = "anonymous@sokosolve.sourceforge.net";
                current.UserHomepage = "http://sokosolve.sourceforge.net";
                current.UserLicense = "Creative Commons";

                string initLib = FileManager.getContent("$Libraries", "Sasquatch.ssx");
                if (File.Exists(initLib))
                {
                    // Set defaults
                    current.LibraryCurrentOpenDir = FileManager.getContent("$Libraries");
                    current.LibraryLastFile = initLib;
                    current.LibraryLastPuzzle = "";
                    current.LibraryCurrentImportDir = FileManager.getContent("$Libraries");

                    SokoSolveProfileLibrary lib = new SokoSolveProfileLibrary();
                    lib.LibraryID = "L1";
                    lib.FileName = current.LibraryLastFile;
                    lib.Name = "Sasquatch";
                    lib.Order = 1;
                    current.Register(lib);
                }
            }
        }

        /// <summary>
        /// Save on application exit.
        /// </summary>
        /// <param name="UserAppDir"></param>
        static public void SaveOnClose(string UserAppDir)
        {
            if (current != null)
            {
                string profilePath = UserAppDir + "\\SokoSolveProfile.xml";
                current.Save(profilePath);
            }
        }

        static Profile current;
    }
}
