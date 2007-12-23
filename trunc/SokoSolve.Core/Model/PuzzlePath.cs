using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.Model
{
    /// <summary>
    /// Encapsulate a puzzle path of contigious steps of (Up, Down, Left, or Right)
    /// </summary>
    public class PuzzlePath
    {
        /// <summary>
        /// Basic Contructor
        /// </summary>
        /// <param name="startLocation"></param>
        public PuzzlePath(VectorInt startLocation)
        {
            this.startLocation = startLocation;
            moves = new List<Direction>();
        }

        /// <summary>
        /// Strong Constructor
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="moves"></param>
        public PuzzlePath(VectorInt startLocation, List<Direction> moves)
        {
            this.startLocation = startLocation;
            this.moves = moves;
        }

        /// <summary>
        /// Is the path empty
        /// </summary>
        public bool IsEmpty
        {
            get { return moves == null || moves.Count == 0; }
        }

        /// <summary>
        /// Is the path valid (like not null)
        /// </summary>
        public bool IsValid
        {
            get { return startLocation != null && moves != null; }
        }

        /// <summary>
        /// Start Location
        /// </summary>
        public VectorInt StartLocation
        {
            get { return startLocation; }
        }

        /// <summary>
        /// End Location
        /// </summary>
        public VectorInt EndLocation
        {
            get
            {
                int deltaX = 0;
                int deltaY = 0;
                foreach (Direction move in moves)
                {
                    switch(move)
                    {
                        case (Direction.Up): deltaY--; break;
                        case (Direction.Down): deltaY++; break;
                        case (Direction.Left): deltaX--; break;
                        case (Direction.Right): deltaX++; break;
                    }
                }
                return StartLocation.Add(deltaX, deltaY);
            }
        }

        /// <summary>
        /// Moves as a direction
        /// </summary>
        public Direction[] Moves
        {
            get { return moves.ToArray(); }
        }

        /// <summary>
        /// Moves as a direction
        /// </summary>
        public VectorInt[] MovesAsPosition
        {
            get
            {
                List<VectorInt> result = new List<VectorInt>(moves.Count+1);
                result.Add(StartLocation);

                VectorInt next = null;
                VectorInt current = StartLocation;
                foreach (Direction move in moves)
                {
                    next = current.Add(current.Offset(move));
                    result.Add(next);
                    current = next;
                }

                return result.ToArray();
            }
        }

        /// <summary>
        /// Add a move to the path
        /// </summary>
        /// <param name="aDirection"></param>
        public void Add(Direction aDirection)
        {
            if (aDirection == Direction.None) return;
            moves.Add(aDirection);
        }

        /// <summary>
        /// Add a move to the path
        /// </summary>
        /// <param name="nextPosition"></param>
        public void Add(VectorInt nextPosition)
        {
            Add(VectorInt.GetDirection(EndLocation, nextPosition));
        }

        private VectorInt startLocation;
        private List<Direction> moves;
    }
}
