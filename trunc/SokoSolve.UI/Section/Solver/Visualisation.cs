using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.UI.Section.Solver
{
    public abstract class Visualisation
    {
        /// <summary>
        /// Find the absolute pixel postition for an element
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public abstract RectangleInt GetDrawRegion(VisualisationElement Element);

        /// <summary>
        /// Find the Element at a pixel location
        /// </summary>
        /// <param name="PixelPosition"></param>
        /// <returns>Null - not found or out of range</returns>
        public abstract VisualisationElement GetElement(VectorInt PixelPosition);

        /// <summary>
        /// The Logical Window Region. 
        /// This is the view port, (0,0) is the first pixel, if the window region starts at (10,10)
        /// then there is an scroll offset of 10, 10.
        /// 
        /// This is seperate to the renderOffset which resolved logic coords to physical ones
        /// (in terms of the Graphics device)
        /// </summary>
        public abstract RectangleInt WindowRegion { get; set; }

        /// <summary>
        /// The render offset is the difference between the Graphics root (0,0) and
        /// the logical root. This includes its size
        /// </summary>
        public abstract RectangleInt RenderCanvas { get; set; }

        /// <summary>
        /// Draw the entire scene
        /// </summary>
        /// <param name="graphics"></param>
        public abstract void Draw(Graphics graphics);

        /// <summary>
        /// Currently selected element (null implies none)
        /// </summary>
        public virtual VisualisationElement Selected
        {
            get { return selected; }
            set { selected = value; }
        }


        public CommonDisplay Display
        {
            get { return display; }
            set { display = value; }
        }

        CommonDisplay display;

        private VisualisationElement selected;
    }

    public abstract class VisualisationElement
    {
        public abstract string GetID();
        public abstract string GetName();
        public abstract string GetDisplayData();
        public abstract void Draw(Graphics graphics, RectangleInt region);
    }

}
