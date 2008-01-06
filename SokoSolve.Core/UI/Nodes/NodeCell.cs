using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.UI.Nodes
{
    /// <summary>
    /// Node derived from a cell on the puzzle
    /// </summary>
    public class NodeCell : NodeBase
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="myCell"></param>
        /// <param name="myGameUI"></param>
        /// <param name="myPuzzleLocation"></param>
        /// <param name="myDepth"></param>
        public NodeCell(GameUI myGameUI, Cell myCell, VectorInt myPuzzleLocation, int myDepth)
            : base(myGameUI, myDepth)
        {
            Cell = myCell;
            puzzleLocation = myPuzzleLocation;
            Size = GameUI.GameCoords.GlobalTileSize;

            CurrentAbsolute = GameUI.GameCoords.PositionAbsoluteFromPuzzle(puzzleLocation);
            if (CurrentAbsolute == null) throw new InvalidOperationException();

           
            DockPoint = DockPoint.TopLeft;

            switch (myCell)
            {
                case (Cell.Void):
                    TileImage = GameUI.ResourceManager["Void"].LoadBitmap();
                    break;
                case (Cell.Floor):
                    TileImage = GameUI.ResourceManager["Floor"].LoadBitmap();
                    break;
                case (Cell.Crate):
                    TileImage = GameUI.ResourceManager["Crate"].LoadBitmap();
                    break;
                case (Cell.Player):
                    TileImage = GameUI.ResourceManager["Player"].LoadBitmap();
                    break;
                case (Cell.Goal):
                    TileImage = GameUI.ResourceManager["Goal"].LoadBitmap();
                    break;
                case (Cell.Wall):
                    TileImage = GameUI.ResourceManager["Wall"].LoadBitmap();
                    break;
            }
        }

        /// <summary>
        /// Logical postion in the puzzle
        /// </summary>
        public virtual VectorInt PuzzleLocation
        {
            get { return puzzleLocation; }
            set { puzzleLocation = value; }
        }


        /// <summary>
        /// The region pixel location of the cell
        /// </summary>
        public RectangleInt PixelRectangle
        {
            get { return new RectangleInt(CurrentAbsolute, GameUI.GameCoords.GlobalTileSize); }
        }


        public override void Render()
        {
            if (TileImage == null)
            {
                Brush b = new SolidBrush(Color.Yellow);
                switch (Cell)
                {
                    case (Cell.Crate):
                        b = new SolidBrush(Color.Brown);
                        break;
                    case (Cell.Floor):
                        b = new SolidBrush(Color.Gray);
                        break;
                    case (Cell.Wall):
                        b = new SolidBrush(Color.LightGray);
                        break;
                    case (Cell.Goal):
                        b = new SolidBrush(Color.Blue);
                        break;
                    case (Cell.Player):
                        b = new SolidBrush(Color.Yellow);
                        break;
                    case (Cell.Void):
                        b = new SolidBrush(Color.Black);
                        break;
                }
                GameUI.Graphics.FillRectangle(b, PixelRectangle.ToDrawingRect());
            }
            else
            {
                GameUI.Graphics.DrawImage(TileImage, PixelRectangle.ToDrawingRect());
            }
        }


        /// <summary>
        /// The Cell at at PuzzleLocation
        /// </summary>
        public Cell Cell
        {
            get { return cell; }
            set { cell = value; }
        }

        /// <summary>
        /// Image to display
        /// </summary>
        public Image TileImage
        {
            get { return tileImage; }
            set { tileImage = value; }
        }

      
        private Cell cell;
        private VectorInt puzzleLocation;
        protected Image tileImage;
    }
}

