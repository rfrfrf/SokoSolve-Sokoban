using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI;

namespace SokoSolve.UI.Section
{
    class DrawingHelper
    {
        static public Image DrawPuzzle(PuzzleMap map)
        {
            return drawing.Draw(map.Map);
        }

        static public StaticImage Images
        {
            get { return drawing;  }
        }

        static StaticImage drawing = new StaticImage(ResourceFactory.Singleton.GetInstance("Default.Tiles"), new VectorInt(16, 16));
    }
}
