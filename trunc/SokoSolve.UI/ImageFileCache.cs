using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using SokoSolve.Core.Model;

namespace SokoSolve.UI
{
    /// <summary>
    /// The HTML controls use temp local files for display of inline dynamic images. This class controls this
    /// functionality
    /// </summary>
    class ImageFileCache : IDisposable 
    {
        private ImageFileCache()
        {
            images = new Dictionary<string, string>();
        }

        public static ImageFileCache Singleton
        {
            get
            {
                if (singletonInstance == null) singletonInstance = new ImageFileCache();
                return singletonInstance;
            }
        }

        /// <summary>
        /// Add or Replace an image, this will save/create a new file in the temp files folder.
        /// Disposal of this class will remove the files.
        /// </summary>
        /// <param name="PuzzleMap"></param>
        /// <param name="NewImage"></param>
        /// <returns></returns>
        public string AddSaveImage(PuzzleMap PuzzleMap, Image NewImage)
        {
            string id = MakeCompoundImageID(PuzzleMap);

            if (images.ContainsKey(id))
            {
                // Already exists, cleanup
                File.Delete(images[id]);
                images.Remove(id);
            }

            string tmpFile = string.Format("{0}\\{1}.png", Path.GetTempPath(), id);
            NewImage.Save(tmpFile, ImageFormat.Png);
            images[id] = tmpFile;
            return tmpFile;
        }

        public string GetImageURL(PuzzleMap PuzzleMap)
        {
            string id = MakeCompoundImageID(PuzzleMap);
            if (!images.ContainsKey(id)) return null;
            return images[id];
        }

        static private string MakeCompoundImageID(PuzzleMap PuzzleMap)
        {
            if (PuzzleMap.Puzzle.Library.LibraryID == null) throw new ArgumentNullException("LibraryID");
            if (PuzzleMap.MapID == null) throw new ArgumentNullException("MapID");

            return string.Format("SS-L{0}{1}", PuzzleMap.Puzzle.Library.LibraryID, PuzzleMap.MapID);
        }

        /// <summary>
        /// CleanUp
        /// Clean up unneeded HTML temp images
        /// </summary>
        public void Dispose()
        {
            if (images != null)
            {
                foreach (string delFile in images.Values)
                {
                    File.Delete(delFile);
                }
                images = null;
            }
        }

        private static ImageFileCache singletonInstance;
        private Dictionary<string, string> images;
    }
}
