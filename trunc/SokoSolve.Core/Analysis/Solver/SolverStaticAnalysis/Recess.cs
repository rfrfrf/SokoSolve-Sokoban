using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis
{
    /// <summary>
    /// A recess is a 1-cell width room wall with containing corner pieces.
    /// This is a simple and effective dead rule, with Dead when Count(Crate) > Count(Goal) as the excess crates 
    /// can never be pushed out of the recess
    /// </summary>
    public class Recess : SolverBitmap
    {
        private int goalCount;

        public Recess(string name, SizeInt aSize) : base(name, aSize)
        {
        }

        /// <summary>
        /// The number of goals in this recess
        /// </summary>
        public int GoalCount
        {
            get { return goalCount; }
            set { goalCount = value; }
        }
    }
}
