using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI;
using SokoSolve.UI.Section.Library;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Section
{
    public enum IconSizes
    {
        Small,
        Icon, 
        Thumbnail
    }

    public class IconBinder
    {
        private ImageList small;
        private ImageList icon;
        private ImageList thumbnail;
        private StaticImage images;
        private Dictionary<SokobanMap, int> cache = new Dictionary<SokobanMap, int>();


        public IconBinder(StaticImage images)
        {
            this.images = images;
        }

        public ImageList GetImageList(IconSizes size)
        {
            switch(size)
            {
                case (IconSizes.Small):
                    return small;
                case (IconSizes.Icon):
                    return icon;
                case (IconSizes.Thumbnail):
                    return thumbnail;
            }

            return null;
        }

        public void SetImageList(IconSizes size, ImageList list)
        {
            switch (size)
            {
                case (IconSizes.Small):
                    small = list;
                    return;
                case (IconSizes.Icon):
                    icon = list;
                    return;
                case (IconSizes.Thumbnail):
                    thumbnail = list;
                    return;
            }
        }

        public int getIcon(IconSizes size, object item)
        {
            if (item == null) return 0;

            Type type = item.GetType();
            if (type == typeof(Type))
            {
                type = item as Type;
            }

            if (type.IsSubclassOf(typeof(ExplorerItem)))
            {
                if (item is ItemLibrary) return 0;
                if (item is ItemCategory) return 1;
                if (item is ItemPuzzle) return 0;
               
            }

            
            if (item is SokoSolve.Core.Model.Library) return 0;
            if (item is SokoSolve.Core.Model.Category) return 1;
            if (item is SokoSolve.Core.Model.Puzzle)
            {
                if (size == IconSizes.Small) return 2;

                Puzzle puz = item as Puzzle;
                if (puz.MasterMap != null)
                {
                    if (cache.ContainsKey(puz.MasterMap.Map))
                    {
                        return cache[puz.MasterMap.Map];
                    }
                    else
                    {
                        // Create
                        thumbnail.Images.Add(this.images.DrawColours(puz.MasterMap.Map));
                        int idx = thumbnail.Images.Count - 1;
                        cache.Add(puz.MasterMap.Map, idx);
                        return idx;
                    }    
                }
                
               
            }
            
            

            return 0;
        }


    }
}
