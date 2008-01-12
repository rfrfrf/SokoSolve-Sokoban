using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation.Strategy;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// Check for 'boxes' squares that make the cratemap dead
    /// </summary>
    class CornerRule : StrategyRule<DeadMapState>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="strategy"></param>
        public CornerRule(StrategyPatternBase<DeadMapState> strategy) :  base(strategy, "Corner Rule: For any position are there wall positions on either diagonal side")
        {
        }

        /// <summary>
        /// Corner Rule: For any position are there wall positions on either diagonal side
        /// </summary>
        /// <param name="StateContext"></param>
        /// <returns></returns>
        public override RuleResult Evaluate(DeadMapState StateContext)
        {
            // Do not eval when dynamic
            if (StateContext.IsDynamic) return RuleResult.Skipped;

            // Generate this now
            StateContext.CornerMap = new SolverBitmap("Corner Map",  StateContext.WallMap.Size);

            // No need to check the first and last lines
            for (int cx = 1; cx < StateContext.Size.Width - 1; cx++)
                for (int cy = 1; cy < StateContext.Size.Height - 1; cy++)
                {
                    // Check TopLeft
                    if (CheckCorner(StateContext, new VectorInt(cx, cy)))
                    {
                        // Corner found
                        StateContext.CornerMap[cx, cy] = true;

                        // Dead if not already dead and not a goal
                        if (!StateContext[cx, cy] && !StateContext.GoalMap[cx,cy])
                        {
                            StateContext[cx, cy] = true; // This is a corner and is deaed
                        }
                    }
                }
            return RuleResult.Success;
        }

        /// <summary>
        /// Check an individual node
        /// </summary>
        /// <param name="context"></param>
        /// <param name="checkCell"></param>
        /// <returns></returns>
        private bool CheckCorner(DeadMapState context, VectorInt checkCell)
        {
            // Check to see if this if a floor. As we do not have an explicit floor map use the wall map implicitly
            if (context.WallMap[checkCell]) return false;

            // Check TopRight
            if (context.WallMap[checkCell.Add(0,-1)] && context.WallMap[checkCell.Add(1,0)]) return true;

            // Check TopLeft
            if (context.WallMap[checkCell.Add(0, -1)] && context.WallMap[checkCell.Add(-1, 0)]) return true;

            // Check BottomRight
            if (context.WallMap[checkCell.Add(0, 1)] && context.WallMap[checkCell.Add(1, 0)]) return true;

            // Check BottomLeft
            if (context.WallMap[checkCell.Add(-1, 0)] && context.WallMap[checkCell.Add(0, 1)]) return true;

            return false;
        }
    }
}
