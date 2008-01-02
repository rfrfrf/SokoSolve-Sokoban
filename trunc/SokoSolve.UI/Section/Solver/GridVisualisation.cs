using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.UI.Section.Solver
{
    public abstract class GridVisualisation : Visualisation
    {
        /// <summary>
        /// From a logical position, return the element
        /// </summary>
        /// <param name="LogicalCell"></param>
        /// <returns></returns>
        public abstract GridVisualisationElement this[VectorInt LogicalCell]
        {
            get;
            set;
        }

        /// <summary>
        /// Overloaded. From a logical position, return the element
        /// </summary>
        /// <param name="LogicalCellX"></param>
        /// <param name="LogicalCellY"></param>
        /// <returns></returns>
        public GridVisualisationElement this[int LogicalCellX, int LogicalCellY]
        {
            get { return this[new VectorInt(LogicalCellX, LogicalCellY)]; }
            set { this[new VectorInt(LogicalCellX, LogicalCellY)] = value; }
        }

        /// <summary>
        /// Size of each cell in pixels
        /// </summary>
        public virtual SizeInt CellSize
        {
            get { return cellSize; }
            set { cellSize = value; }
        }

        /// <summary>
        /// Logical Size of the entire grid measured in cells.
        /// </summary>
        public virtual SizeInt GridSize
        {
            get { return gridSize; }
            set { gridSize = value; }
        }

        /// <summary>
        /// The Logical Window Region. 
        /// This is the view port, (0,0) is the first pixel, if the window region starts at (10,10)
        /// then there is an scroll offset of 10, 10.
        /// 
        /// This is seperate to the renderOffset which resolved logic coords to physical ones
        /// (in terms of the Graphics device)
        /// </summary>
        public override RectangleInt WindowRegion
        {
            get
            {
                return new RectangleInt(new VectorInt(0, 0), cellSize.Multiply(gridSize));
            }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// The render offset is the difference between the Graphics root (0,0) and
        /// the logical root. This includes its size
        /// </summary>
        public override RectangleInt RenderCanvas
        {
            get
            {
                return WindowRegion; // Don't use any buffer, nor sizing
            }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Find the absolute pixel postition for an element
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override RectangleInt GetDrawRegion(VisualisationElement Element)
        {
            GridVisualisationElement gridElement = Element as GridVisualisationElement;
            if (gridElement == null) throw new NotSupportedException("Element");

            return new RectangleInt(RenderCanvas.TopLeft.Add(gridElement.LogicalPosition.Multiply(CellSize)), CellSize);
        }

        /// <summary>
        /// Find the Element at a pixel location
        /// </summary>
        /// <param name="PixelPosition"></param>
        /// <returns>Null - not found or out of range</returns>
        public override VisualisationElement GetElement(VectorInt PixelPosition)
        {
            VectorInt logical = PixelPosition.Subtract(RenderCanvas.TopLeft).Divide(cellSize);
            if (logical.X > GridSize.X) return null;
            if (logical.Y > GridSize.Y) return null;
            return this[PixelPosition.Subtract(RenderCanvas.TopLeft).Divide(cellSize)];
        }

        private SizeInt cellSize;
        private SizeInt gridSize;
    }

    public abstract class GridVisualisationElement : VisualisationElement
    {
        public abstract VectorInt LogicalPosition
        {
            get;
            set;
        }
    }
}
