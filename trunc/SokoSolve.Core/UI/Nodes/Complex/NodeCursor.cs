using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.UI.Nodes.Complex
{

    class NodeCursorEventArgs : NotificationEvent
    {
        public NodeCursorEventArgs(NodeBase source, string command, object tag, int x, int y, int clicks, int button) : base(source, command, tag)
        {
            this.x = x;
            this.y = y;
            this.clicks = clicks;
            this.button = button;
        }


        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Clicks
        {
            get { return clicks; }
        }

        public int Button
        {
            get { return button; }
        }

        private int x;
        private int y;
        private int clicks;
        private int button;
    }

    class NodeCursor : NodeBase
    {
        public NodeCursor(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            penCursorMini = new Pen(new SolidBrush(Color.FromArgb(180, Color.Wheat)), 2);

            brushCellMarker = new SolidBrush(Color.FromArgb(40, Color.Yellow));
            brushCursorMini = new SolidBrush(Color.FromArgb(200, Color.White));
        }

        public override VectorInt CurrentAbsolute
        {
            get
            {
                return base.CurrentAbsolute;
            }
            set
            {
                base.CurrentAbsolute = value;
                // Notify stuff that the position has changed
            }
        }

        public event EventHandler<NodeCursorEventArgs> OnClick;

          /// <summary>
        /// Update the cursor position
        /// </summary>
        /// <param name="X">Mouse X</param>
        /// <param name="Y">Mouse Y</param>
        /// <param name="Clicks">Number of clicks</param>
        /// <param name="Button">Button enum 0=None, 1=Left, 2=Right</param>
        /// <returns>true is cursor is drawn by game</returns>
        public bool SetCursor(int X, int Y, int Clicks, int Button)
        {
            bool manualDraw = false;

            int rX = X - GameUI.GameCoords.GlobalOffset.X;
            int rY = Y - GameUI.GameCoords.GlobalOffset.Y;

            if (rX >= 0 && rY >= 0)
            {
                this.CurrentLogical = new VectorInt(rX, rY);
            }

            VectorInt puzPos = GameUI.GameCoords.PuzzleFromPositionAbs(this.CurrentAbsolute);
            CellStates puzCell = CellStates.Void;
            if (GameUI.Current.Rectangle.Contains(puzPos))
            {
                manualDraw = true;
                puzCell = GameUI.Current[puzPos];
            }

            // Process a click
            if (Clicks > 0)
            {
                // Do not allow the move, it other previous moves are still pending
                if (puzCell != CellStates.Void && !GameUI.Player.HasFutureMoves)
                {
                    // Draw the path from the player to the this as a path
                    VectorInt playerPos = GameUI.Current.Player;

                    // Try crate movement
                    bool cellIsCrate = (puzCell == CellStates.FloorCrate || puzCell == CellStates.FloorGoalCrate);
                    Direction playerIsAdjacent = IsAdjacentPlayer(GameUI.Current, puzPos);
                    if (cellIsCrate && playerIsAdjacent != Direction.None)
                    {
                        GameUI.Player.doMove(playerIsAdjacent);
                    }
                    else
                    {
                        // Try Path movement
                        List<Direction> path = Convert(PuzzleAnalysis.FindPath(GameUI.Current, puzPos));
                        if (path != null)
                        {
                            foreach (Direction aMove in path)
                            {
                                GameUI.Player.doMove(aMove);
                            }
                        }
                    }
                }

                if (OnClick != null)
                {
                    OnClick(this, new NodeCursorEventArgs(this, "Click", Clicks, X, Y, Clicks, Button));
                }
            }
            return manualDraw; 
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

        public override void Render()
        {
            // Mark the current puzzle cell
            VectorInt cellPos = GameUI.GameCoords.PuzzleFromPositionAbs(CurrentAbsolute);
            if (cellPos != null && GameUI.Current.Rectangle.Contains(cellPos))
            {
                VectorInt cellStartAbs = GameUI.GameCoords.PositionAbsoluteFromPuzzle(cellPos);
                GameUI.Graphics.FillRectangle(brushCellMarker, cellStartAbs.X, cellStartAbs.Y, GameUI.GameCoords.GlobalTileSize.X, GameUI.GameCoords.GlobalTileSize.Y);    
            }

            // Draw the path from the player to the cursor as a path
            VectorInt playerPos = GameUI.Current.Player;

            // Show path to cursor, if the player is not busy moving
            if (!GameUI.Player.HasFutureMoves)
            {
                List<VectorInt> path = PuzzleAnalysis.FindPath(GameUI.Current, cellPos);
                if (path != null)
                {
                    foreach (VectorInt pathNode in path)
                    {
                        VectorInt pathDrawPos = GameUI.GameCoords.PositionAbsoluteFromPuzzle(pathNode);
                        pathDrawPos = pathDrawPos.Add(GameUI.GameCoords.GlobalTileSize.X/2, GameUI.GameCoords.GlobalTileSize.Y/2);
                        GameUI.Graphics.FillEllipse(brushCursorMini, pathDrawPos.X, pathDrawPos.Y, 3, 3);
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
    }
}
