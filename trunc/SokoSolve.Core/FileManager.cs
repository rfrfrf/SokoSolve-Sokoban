using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SokoSolve.Core.UI;

namespace SokoSolve.Core
{
    public class FileManager
    {
		static FileManager()
		{
			baseloc = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Content";
			if (!Directory.Exists(baseloc))
			{
				// Assume we are in debug mode
				baseloc = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Replace("\\bin\\Debug", "") + "Content";
			}

            if (!Directory.Exists(baseloc))
            {
                // Assume we are in unit tests
                baseloc = @"C:\Projects\Personal\SokoSolve\svn\trunc\SokoSolve.UI\Content";
            }

			if (!Directory.Exists(baseloc))
			{
				throw new Exception("Cannot find the application content directory");
			}
		}

        public static string getContent(string relPath)
        {
            if (relPath.StartsWith("$")) return string.Format("{0}\\{1}", baseloc, relPath.Remove(0,1));
            return relPath;
        }

        public static  string getContent(string relPath, string afile)
        {
            return getContent(string.Format("{0}\\{1}", relPath, afile));
        }

       


        // This has got to go
    	public static string baseloc;
    }
}
