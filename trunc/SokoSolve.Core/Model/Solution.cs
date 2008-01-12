using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Game;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    /// <summary>
    /// Encapsulate a Sokoban puzzle solution
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// Strong contructor
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="startPosition"></param>
        public Solution(PuzzleMap parent, VectorInt startPosition)
        {
            this.map = parent;
            this.startPosition = startPosition;
        }

        /// <summary>
        /// Map for which there is a solution
        /// </summary>
        public PuzzleMap Map
        {
            get { return map; }
            set { map = value; }
        }

        /// <summary>
        /// Descriptive Details
        /// </summary>
        public GenericDescription Details
        {
            get { return details; }
            set { details = value; }
        }

        /// <summary>
        /// Steps/Moves in the solution
        /// </summary>
        public string Steps
        {
            get { return steps; }
            set { steps = value; }
        }

        /// <summary>
        /// Puzzle start position
        /// </summary>
        public VectorInt StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }

        /// <summary>
        /// Test the solution to see if indeed it results in a solution. <see cref="Game.Test"/>
        /// </summary>
        /// <remarks>True means a solution is valid</remarks>
        public bool Test(out string firstError)
        {
            SokoSolve.Core.Game.Game coreGame = new SokoSolve.Core.Game.Game(map.Puzzle, map.Map);
            return coreGame.Test(this, out firstError);
        }

        /// <summary>
        /// Set the solution from a path
        /// </summary>
        /// <param name="solutionPath"></param>
        public void Set(Path solutionPath)
        {
            startPosition = solutionPath.StartLocation;
            StringBuilder sb = new StringBuilder();
            foreach (Direction move in solutionPath.Moves)
            {
                char moveChar = '?';
                switch (move)
                {
                    case (Direction.Up):
                        moveChar = 'u';
                        break;
                    case (Direction.Down):
                        moveChar = 'd';
                        break;
                    case (Direction.Left):
                        moveChar = 'l';
                        break;
                    case (Direction.Right):
                        moveChar = 'r';
                        break;
                }
                sb.Append(moveChar);
            }
            steps = sb.ToString();
        }

        /// <summary>
        /// Debug information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Steps, details);
        }

        /// <summary>
        /// Convert to a path
        /// </summary>
        /// <returns></returns>
        public Path ToPath()
        {
            Path res = new Path(startPosition);
            foreach (char c in steps)
            {
                switch (Char.ToLower(c))
                {
                    case ('u'):
                        res.Add(Direction.Up);
                        break;
                    case ('d'):
                        res.Add(Direction.Down);
                        break;
                    case ('l'):
                        res.Add(Direction.Left);
                        break;
                    case ('r'):
                        res.Add(Direction.Right);
                        break;
                }
            }
            return res;
        }

        /// <summary>
        /// Set the solution directly from a game conclusion
        /// </summary>
        /// <param name="moves"></param>
        public void FromGame(Stack<Move> moves)
        {
            StringBuilder sb = new StringBuilder();

            Move[] moveArray = moves.ToArray();
            Array.Reverse(moveArray);
            foreach (Move move in moveArray)
            {
                char moveChar = '?';
                switch (move.MoveDirection)
                {
                    case (Direction.Up):
                        moveChar = 'u';
                        break;
                    case (Direction.Down):
                        moveChar = 'd';
                        break;
                    case (Direction.Left):
                        moveChar = 'l';
                        break;
                    case (Direction.Right):
                        moveChar = 'r';
                        break;
                }

                if (move.isPush) moveChar = char.ToUpper(moveChar);

                sb.Append(moveChar);
            }

            Steps = sb.ToString();
        }

        private GenericDescription details;
        private PuzzleMap map;
        private VectorInt startPosition;
        private string steps;
    }
}