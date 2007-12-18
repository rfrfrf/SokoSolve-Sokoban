using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Game
{
    /// <summary>
    /// A chain of player moves
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Construct from a previous move
        /// </summary>
        /// <param name="aBefore"></param>
        /// <param name="aMoveDirection"></param>
        public Move(SokobanMap aBefore, Direction aMoveDirection, bool aIsPush)
        {
            Before = aBefore;
            MoveDirection = aMoveDirection;
            isPush = aIsPush;
        }

        public SokobanMap Before;
        public Direction MoveDirection;
        public bool isPush;
    }
}
