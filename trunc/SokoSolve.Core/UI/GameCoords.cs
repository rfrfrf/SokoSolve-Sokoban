using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Game;

namespace SokoSolve.Core.UI
{
    /// <summary>
    /// Encapsulate the 2D game coordinate system
    /// </summary>
    public class GameCoords
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="aGameUI"></param>
        public GameCoords(GameUI aGameUI)
        {
            GameUI = aGameUI;
            GlobalOffset = new VectorInt(10, 30);
			GlobalTileSize = new SizeInt(16, 16);
            windowRegion =  new RectangleInt(new VectorInt(0, 0), new SizeInt(400, 300));
        }

        /// <summary>
        /// The gobal offset/distance from 0,0 to draw the topleft of the puzzle
        /// </summary>
		public VectorInt GlobalOffset;

        /// <summary>
        /// Pixel size of each tile/cell
        /// </summary>
		public SizeInt GlobalTileSize;


        /// <summary>
        /// Get the absolute pixel coords from a puzzle position
        /// </summary>
        /// <param name="PuzzlePosition"></param>
        /// <returns></returns>
		public VectorInt PositionAbsoluteFromPuzzle(VectorInt PuzzlePosition)
        {
			return new VectorInt(PuzzlePosition.X * GlobalTileSize.X + GlobalOffset.X, PuzzlePosition.Y * GlobalTileSize.Y + GlobalOffset.Y);
        }

        /// <summary>
        /// Get the puzzle position from the absolute pixel position (taking into accout the GlobalOffset)
        /// </summary>
        /// <param name="AbsolutePosition"></param>
        /// <returns></returns>
		public VectorInt PuzzleFromPositionAbs(VectorInt AbsolutePosition)
        {
			return new VectorInt((AbsolutePosition.X - GlobalOffset.X) / GlobalTileSize.X, (AbsolutePosition.Y - GlobalOffset.Y) / GlobalTileSize.Y);
        }

        /// <summary>
        /// Get the puzzle position from the pixel position (NOT taking into accout the GlobalOffset)
        /// </summary>
        /// <param name="LogicalPosition"></param>
        /// <returns></returns>
        public VectorInt PuzzleFromPositionLogical(VectorInt LogicalPosition)
        {
            return new VectorInt((LogicalPosition.X) / GlobalTileSize.X, (LogicalPosition.Y ) / GlobalTileSize.Y);
        }

        /// <summary>
        /// Define a rectangle for the puzzle region
        /// </summary>
        public RectangleInt PuzzleRegion
        {
            get
            {
				return new RectangleInt(GlobalOffset,
									 new VectorInt(GlobalTileSize.X * GameUI.Current.Size.X, GlobalTileSize.Y* GameUI.Current.Size.Y));
            }
        }

        /// <summary>
        /// Define a rectangle for the puzzle region
        /// </summary>
        public RectangleInt WindowRegion
        {
            get { return windowRegion;  }
            set { windowRegion = value; }
        }

        /// <summary>
        /// Region to display the status information
        /// </summary>
        public VectorInt PositionStatus
        {
            get { return WindowRegion.TopRight.Add(-80, 50); }
        }

        /// <summary>
        /// Region to display the status information
        /// </summary>
        public RectangleInt PositionMovementCommands
        {
            get { return new RectangleInt(WindowRegion.BottomRight.Subtract(90, 90), new SizeInt(90, 90)); }
        }

        private GameUI GameUI;
        private RectangleInt windowRegion;
    }
}
