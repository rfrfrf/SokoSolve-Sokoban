using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Model
{
    class Profile
    {
        public Profile()
        {
            simpleParams = new Dictionary<string, string>();
        }

        public string LibraryCurrentOpenDir
        {
            get { return simpleParams["LibraryCurrentOpenDir"]; }
            set { simpleParams["LibraryCurrentOpenDir"] = value; }
        }

        public string LibraryCurrentImportDir
        {
            get { return simpleParams["LibraryCurrentImportDir"]; }
            set { simpleParams["LibraryCurrentImportDir"] = value; }
        }

        public string LibraryLastFile
        {
            get { return simpleParams["LibraryLastFile"]; }
            set { simpleParams["LibraryLastFile"] = value; }
        }

        public string LibraryLastPuzzle
        {
            get { return simpleParams["LibraryLastPuzzle"]; }
            set { simpleParams["LibraryLastPuzzle"] = value; }
        }

        private Dictionary<string, string> simpleParams;
    }
}
