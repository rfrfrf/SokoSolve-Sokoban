using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SokoSolve.Common
{
    public static class FileHelper
    {
        /// <summary>
        /// Copy a source directory to another location.
        /// Will create target not exists.
        /// Will not override, but skip if dest file already exists.
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="targetDirectory"></param>
        static public void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            try
            {
                // Create if missing
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // Copy files
                string[] files = Directory.GetFiles(sourceDirectory);
                foreach (string file in files)
                {
                    string dest = targetDirectory + "\\" + Path.GetFileName(file);
                    if (!File.Exists(dest))
                    {
                        File.Copy(file, dest, false);    
                    }
                    
                }
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("Unable to copy directory {0} to {1}", sourceDirectory, targetDirectory), ex);
            }
        }
    }
}

