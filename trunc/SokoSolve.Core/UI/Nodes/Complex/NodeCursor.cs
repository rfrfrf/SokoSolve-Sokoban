using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis;
using SokoSolve.Core.Model;
using Bitmap=SokoSolve.Common.Structures.Bitmap;

namespace SokoSolve.Core.UI.Nodes.Complex
{
    /// <summary>
    /// Handle cursor user interaction and presentation.
    /// </summary>
    public class NodeCursor : NodeBase
    {
        public NodeCursor(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            penCursorMini = new Pen(new SolidBrush(Color.FromArgb(180, Color.Wheat)), 2);
            brushCellMarker = new SolidBrush(Color.FromArgb(40, Color.Yellow));
            brushCursorMini = new SolidBrush(Color.FromArgb(200, Color.White));
        }

        /// <summary>
        /// Allow other elements to 'listen' to mouse changes
        /// </summary>
        public event EventHandler<NodeCursorEventArgs> OnClick;

        /// <summary>
        /// Update the cursor position
        /// </summary>
        /// <param name="X">Mouse X</param>
        /// <param name="Y">Mouse Y</param>
        /// <param name="ClickCount">Number of clicks</param>
        /// <param name="Button">Button enum 0=None, 1=Left, 2=Right</param>
        /// <returns>true is cursor is drawn by game</returns>
        public bool SetCursor(int X, int Y, int ClickCount, GameUI.MouseButtons Button, GameUI.MouseClicks ClickType)
        {
            bool manualDraw = false;

            // Relative position
            int rX = X - GameUI.GameCoords.GlobalOffset.X;
            int rY = Y - GameUI.GameCoords.GlobalOffset.Y;

            // Set the mouse cursor, if it is on the page
            //if (rX >= 0 && rY >= 0)
            {
                this.CurrentLogical = new VectorInt(rX, rY);
            }

            // Find the puzzle cell position
            VectorInt puzPos = GameUI.GameCoords.PuzzleFromPositionAbs(this.CurrentAbsolute);
            CellStates puzCell = CellStates.Void;
            if (GameUI.Current.Rectangle.Contains(puzPos))
            {
                // Is this cursor within the puzzle
                manualDraw = true;
                puzCell = GameUI.Current[puzPos];
            }

            // Click started
            if (ClickCount > 0 && ClickType == GameUI.MouseClicks.Down)
            {
                dragStarted = true;
                dragStartPixelLocation = this.CurrentAbsolute;
                dragStartCellLocation = puzPos;
                dragStartCell = puzCell;
            }

            // Click ended
            if (ClickCount > 0 && ClickType == GameUI.MouseClicks.Up)
            {
                dragStarted = false;
                if (dragStartCell == CellStates.Floor || dragStartCell == CellStates.FloorGoal)
                {
                    PerformPlayerMovement(puzPos, puzCell);       
                }
                else if (dragStartCell == CellStates.FloorCrate || dragStartCell == CellStates.FloorGoalCrate)
                {
                    PerformCrateMovement(dragStartCellLocation, puzPos);
                }
            }

            // Let the rest of the world know...
            if (OnClick != null)
            {
                OnClick(this, new NodeCursorEventArgs(this, "Click", this, X, Y, ClickCount, Button, ClickType));
            }

            return manualDraw; 
        }

        /// <summary>
        /// Move the crate
        /// </summary>
        /// <param name="startCrateLocation"></param>
        /// <param name="targetCrateLocation"></param>
        private void PerformCrateMovement(VectorInt startCrateLocation, VectorInt targetCrateLocation)
        {
            CrateAnalysis.ShortestCratePath path = CrateAnalysis.FindCratePath(GameUI.Current, startCrateLocation, targetCrateLocation);
            if (path != null)
            {
                foreach (Direction step in path.PlayerPath.Moves)
                {
                    GameUI.Player.doMove(step);
                }   
            }
        }

        /// <summary>
        /// Move the player
        /// </summary>
        /// <param name="puzPos"></param>
        /// <param name="puzCell"></param>
        private void PerformPlayerMovement(VectorInt puzPos, CellStates puzCell)
        {
            // Do not allow the move, it other previous moves are still pending
            if (puzCell != CellStates.Void && !GameUI.Player.HasFutureMoves)
            {
                // Draw the path from the player to the this as a path
                VectorInt playerPos = GameUI.Current.Player;

                // Try Path movement
                List<Direction> path = Convert(MoveAnalysis.FindPlayerPath(GameUI.Current, puzPos));
                if (path != null)
                {
                    foreach (Direction aMove in path)
                    {
                        GameUI.Player.doMove(aMove);
                    }
                }
            }
        }

        /// <summary>
        /// Is the player adjacent to a puzzle position
        /// </summary>
        /// <param name="PuzzlePos"></param>
        /// <returns></returns>
        static private Direction IsAdjacentPlayer(SokobanMap Map, VectorInt PuzzlePos)
        {
            VectorInt playerPos = Map.Player;
            if (playerPos.Offset(Direction.Up) == PuzzlePos) return Direction.Up;
            if (playerPos.Offset(Direction.Down) == PuzzlePos) return Direction.Down;
            if (playerPos.Offset(Direction.Left) == PuzzlePos) return Direction.Left;
            if (playerPos.Offset(Direction.Right) == PuzzlePos) return Direction.Right;

            return Direction.None;
        }

        /// <summary>
        /// Convert a list of points in 2D space into directoins
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static List<Direction> Convert(List<VectorInt> path)
        {
            if (path == null || path.Count <= 1) return null;

            List<Direction> result = new List<Direction>();
            for (int cc = 1; cc < path.Count; cc++)
            {
                result.Add(VectorInt.GetDirection(path[cc - 1], path[cc]));
            }

            return result;
        }

        /// <summary>
        /// Draw the cursor
        /// </summary>
        public override void Render()
        {
            // Mark the current puzzle cell
            VectorInt cellPos = GameUI.GameCoords.PuzzleFromPositionAbs(CurrentAbsolute);
            if (cellPos != null && GameUI.Current.Rectangle.Contains(cellPos))
            {
                VectorInt cellStartAbs = GameUI.GameCoords.PositionAbsoluteFromPuzzle(cellPos);
                GameUI.Graphics.FillRectangle(brushCellMarker, cellStartAbs.X, cellStartAbs.Y, GameUI.GameCoords.GlobalTileSize.X, GameUI.GameCoords.GlobalTileSize.Y);    
            }

            if (dragStarted)
            {
                if ((dragStartCell == CellStates.FloorCrate || dragStartCell == CellStates.FloorGoalCrate) && !GameUI.Player.HasFutureMoves)
                {
                    CrateAnalysis.ShortestCratePath path = CrateAnalysis.FindCratePath(GameUI.Current, dragStartCellLocation, cellPos);
                    if (path != null)
                    {
                        Pen cratePen = new Pen(Color.Yellow, 2f);
                        VectorInt lastPos = VectorInt.Empty;

                        VectorInt startDrag = GameUI.GameCoords.PositionAbsoluteFromPuzzle(dragStartCellLocation);

                        GameUI.Graphics.DrawRectangle(cratePen,
                            startDrag.X,
                            startDrag.Y, 
                            GameUI.GameCoords.GlobalTileSize.X, 
                            GameUI.GameCoords.GlobalTileSize.Y);
                        
                        foreach (VectorInt cratePos in path.CratePath.MovesAsPosition)
                        {
                            VectorInt cratePosPixel = GameUI.GameCoords.PositionAbsoluteFromPuzzle(cratePos);
                            VectorInt cratePosPixelCenter = cratePosPixel.Add(GameUI.GameCoords.GlobalTileSize.Divide(2, 2));

                            //GameUI.Graphics.DrawRectangle(cratePen, cratePosPixelCenter.X -3, cratePosPixelCenter.Y -3, 6, 6);
                            if (lastPos != null)
                            {
                                GameUI.Graphics.DrawLine(cratePen, lastPos.X, lastPos.Y, cratePosPixelCenter.X, cratePosPixelCenter.Y);
                            }

                            lastPos = cratePosPixelCenter;
                        }

                        // Draw Arrow
                        // TODO
                    }
                }
            }
            else
            {
                // Draw the path from the player to the cursor as a path
                VectorInt playerPos = GameUI.Current.Player;

                // Show path to cursor, if the player is not busy moving
                if (!GameUI.Player.HasFutureMoves)
                {
                    List<VectorInt> path = MoveAnalysis.FindPlayerPath(GameUI.Current, cellPos);
                    if (path != null)
                    {
                        foreach (VectorInt pathNode in path)
                        {
                            VectorInt pathDrawPos = GameUI.GameCoords.PositionAbsoluteFromPuzzle(pathNode);
                            pathDrawPos = pathDrawPos.Add(GameUI.GameCoords.GlobalTileSize.X / 2, GameUI.GameCoords.GlobalTileSize.Y / 2);
                            GameUI.Graphics.FillEllipse(brushCursorMini, pathDrawPos.X, pathDrawPos.Y, 3, 3);
                        }
                    }
                }
            }

            // Draw the current mini-cursor
            int miniCursorSize = 5;
            GameUI.Graphics.DrawLine(penCursorMini, CurrentAbsolute.X - miniCursorSize, CurrentAbsolute.Y, 
                CurrentAbsolute.X + miniCursorSize, CurrentAbsolute.Y);
            GameUI.Graphics.DrawLine(penCursorMini, CurrentAbsolute.X, CurrentAbsolute.Y - miniCursorSize,
                CurrentAbsolute.X, CurrentAbsolute.Y + miniCursorSize);
        }

        private Brush brushCursorMini;
        private Brush brushCellMarker;
        private Pen penCursorMini;
        private bool dragStarted = false;
        private VectorInt dragStartPixelLocation;
        private VectorInt dragStartCellLocation;
        private CellStates dragStartCell;
    }
}
