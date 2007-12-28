using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Common.Structures
{
    /// <summary>
    /// Encapsulate a puzzle path of contigious steps of (Up, Down, Left, or Right)
    /// </summary>
    public class Path
    {
        /// <summary>
        /// Basic Contructor
        /// </summary>
        /// <param name="startLocation"></param>
        public Path(VectorInt startLocation)
        {
            this.startLocation = startLocation;
            moves = new List<Direction>();
        }

        /// <summary>
        /// Strong Constructor
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="moves"></param>
        public Path(VectorInt startLocation, List<Direction> moves)
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
                    switch (move)
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
                List<VectorInt> result = new List<VectorInt>(moves.Count + 1);
                result.Add(StartLocation);

                VectorInt next = null;
                VectorInt current = StartLocation;
                foreach (Direction move in moves)
                {
                    next = current.Offset(move);
                    result.Add(next);
                    current = next;
                }

                return result.ToArray();
            }
        }

        /// <summary>
        /// Length of the path
        /// </summary>
        public int Count
        {
            get { return moves.Count;  }
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Len:{0}, Starting:{1} ", moves.Count, startLocation);
            foreach (Direction dir in moves)
            {
                switch(dir)
                {
                    case (Direction.Up): sb.Append("U"); break;
                    case (Direction.Down): sb.Append("D"); break;
                    case (Direction.Left): sb.Append("L"); break;
                    case (Direction.Right): sb.Append("R"); break;
                }
            }
            return sb.ToString();
        }

        private VectorInt startLocation;
        private List<Direction> moves;
    }
}
