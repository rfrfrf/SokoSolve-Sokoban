using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core;

using SokoSolve.Core.UI;
using SokoSolve.Core.Game;



namespace SokoSolve.Core.UI.Nodes
{
    /// <summary>
    /// A puzzle cell that is capable of moving
    /// </summary>
    public class NodeDynamic : NodeCell
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="myGameUI"></param>
        /// <param name="myCell"></param>
        /// <param name="myPuzzleLocation"></param>
        /// <param name="myDepth"></param>
        public NodeDynamic(GameUI myGameUI, Cell myCell, VectorInt myPuzzleLocation, int myDepth)
            : base(myGameUI, myCell, myPuzzleLocation, myDepth)
        {
            
        }

        /// <summary>
        /// Location of the cell oin the logical puzzle
        /// </summary>
        public override VectorInt PuzzleLocation
        {
            get
            {
                return base.PuzzleLocation;
            }
            set
            {
                // Capture the location as a target
                base.PuzzleLocation = value;
                target = GameUI.GameCoords.PositionAbsoluteFromPuzzle(value);
            }
        }

        /// <summary>
        /// Do the animation
        /// </summary>
        public override void doStep()
        {
            if (target == null) return;

            if (!Animate)
            {
                CurrentAbsolute = target;
            }
            else
            {
                if (target != CurrentAbsolute)
                {
                    if (target.X > CurrentAbsolute.X) CurrentAbsolute = CurrentAbsolute.Add(Speed, 0);
                    if (target.X < CurrentAbsolute.X) CurrentAbsolute = CurrentAbsolute.Subtract(Speed, 0);
                    if (target.Y > CurrentAbsolute.Y) CurrentAbsolute = CurrentAbsolute.Add(0, Speed);
                    if (target.Y < CurrentAbsolute.Y) CurrentAbsolute = CurrentAbsolute.Subtract(0, Speed);
                }
            }
        }

        protected bool Animate = false;
        protected int Speed = 16;
        protected VectorInt  target;     
    }
}
