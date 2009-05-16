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
            baseloc = CleanPath(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Content");
			if (!Directory.Exists(baseloc))
			{
				// Assume we are in debug mode
                baseloc = CleanPath(AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Replace("\\bin\\Debug", "") + "Content");
			}

            if (!Directory.Exists(baseloc))
            {
                if (Directory.GetCurrentDirectory().EndsWith(@"\SokoSolve.Test\bin\Debug"))
                {
                    baseloc = Directory.GetCurrentDirectory().Replace(@"\SokoSolve.Test\bin\Debug", @"\SokoSolve.UI\Content");
                }
            }

			if (!Directory.Exists(baseloc))
			{
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Cannot find the application content directory.");
                sb.AppendLine("Current Directory: " + Directory.GetCurrentDirectory());
                sb.AppendLine("App Directory: " + AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                sb.AppendLine("BaseLoc: " + baseloc);
				throw new Exception(sb.ToString());
			}
		}

        private static string CleanPath(string path)
        {
            string ret = path.Replace('/', '\\');
            ret = ret.Replace("\\\\", "\\");
            return ret;
        }

        public static string GetContent(string relPath, bool checkPath)
        {
            if (relPath.StartsWith("$")) return CleanPath(string.Format("{0}\\{1}", baseloc, relPath.Remove(0, 1)));
            string path = CleanPath(relPath);
            if (!File.Exists(path) && !Directory.Exists(path))
            {
                throw new FileNotFoundException("Path does not exist", path);
            }
            return path;
        }

        public static string GetContent(string relPath)
        {
            return GetContent(relPath, true);
        }

        public static  string GetContent(string relPath, string afile)
        {
            return GetContent(string.Format("{0}\\{1}", relPath, afile), true);
        }

      
        // This has got to go
    	public static string baseloc;
    }
}
