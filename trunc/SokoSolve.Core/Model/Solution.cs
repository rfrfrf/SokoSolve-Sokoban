using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Game;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    public class Solution
    {
        private GenericDescription details;
        private string steps;


        public GenericDescription Details
        {
            get { return details; }
            set { details = value; }
        }

        public string Steps
        {
            get { return steps; }
            set { steps = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Steps, details);
        }

        public void FromGame(Stack<Move> moves)
        {
            StringBuilder sb = new StringBuilder();

            Move[] moveArray = moves.ToArray();
            Array.Reverse(moveArray);
            foreach (Move move in moveArray)
            {
                char moveChar = '?';
                switch(move.MoveDirection)
                {
                    case (Direction.Up): moveChar = 'u'; break;
                    case (Direction.Down): moveChar = 'd'; break;
                    case (Direction.Left): moveChar = 'l'; break;
                    case (Direction.Right): moveChar = 'r'; break;
                }

                if (move.isPush) moveChar = char.ToUpper(moveChar);

                sb.Append(moveChar);
            }

            Steps = sb.ToString();
        }
    }
}
