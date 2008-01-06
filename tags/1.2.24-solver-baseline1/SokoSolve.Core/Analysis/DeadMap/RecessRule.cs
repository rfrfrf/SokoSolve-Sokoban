using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures.Evaluation.Strategy;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// Check which nodes form a recess without any goals. This rule assumes that the Corner rule evaluates first
    /// </summary>
    public class RecessRule : StrategyRule<DeadMapState>
    {
        public RecessRule(StrategyPatternBase<DeadMapState> strategy)
            : base(strategy, "Check which nodes form a recess without any goals")
        {
        }

        public override RuleResult Evaluate(DeadMapState StateContext)
        {
            // Do not eval when dynamic
            if (StateContext.IsDynamic) return RuleResult.Skipped;

            StateContext.RecessMap = new SolverBitmap("Recess Map", StateContext.WallMap.Size);

            // No need to check the first and last lines
            for (int cx = 1; cx < StateContext.Size.Width - 1; cx++)
                for (int cy = 1; cy < StateContext.Size.Height - 1; cy++)
                {
                    // Moving in each direction try to find another non-goal corner
                    CheckRecessFromCorner(StateContext, new VectorInt(cx, cy), Direction.Up);
                    CheckRecessFromCorner(StateContext, new VectorInt(cx, cy), Direction.Down);
                    CheckRecessFromCorner(StateContext, new VectorInt(cx, cy), Direction.Left);
                    CheckRecessFromCorner(StateContext, new VectorInt(cx, cy), Direction.Right);
                }

            return RuleResult.Success;
        }

        /// <summary>
        /// Moving in each direction try to find another non-goal corner
        /// </summary>
        /// <param name="StateContext"></param>
        /// <param name="CheckPos"></param>
        /// <param name="CheckDirection"></param>
        private void CheckRecessFromCorner(DeadMapState StateContext, VectorInt CheckPos, Direction CheckDirection)
        {
            // Must start on a corner position
            if (!StateContext.CornerMap[CheckPos]) return;

            RectangleInt region = new RectangleInt(new VectorInt(0,0), StateContext.Size.Subtract(1,1));
            VectorInt pos = CheckPos;

            Direction sideA;
            Direction sideB;
            // Check Recess wall side
            if (CheckDirection == Direction.Up || CheckDirection == Direction.Down)
            {
                sideA = Direction.Left;
                sideB = Direction.Right;
            }
            else
            {
                sideA = Direction.Up ;
                sideB = Direction.Down;
            }

            bool hasSideA = true;
            bool hasSideB = true;

            // Try to find another corner with SideA or SideB
            while(region.Contains(pos))
            {
                // Check Fail
                if (StateContext.GoalMap[pos]) return; // Goal- Fail
                if (StateContext.WallMap[pos]) return; // Wall in way- Fail

                if (hasSideA)
                {
                    if (!StateContext.WallMap[pos.Offset(sideA)]) hasSideA = false;
                }

                if (hasSideB)
                {
                    if (!StateContext.WallMap[pos.Offset(sideB)]) hasSideB = false;
                }

                if (!hasSideA && !hasSideB) return;

                // Check success
                // Don't check the first time
                if (pos != CheckPos)
                {
                    if (StateContext.CornerMap[pos])
                    {
                        // Winner
                        // Mark all in path as corner
                        VectorInt setTrue = CheckPos;
                        while (setTrue != pos)
                        {
                            // Set as corner
                            StateContext.RecessMap[setTrue] = true;

                            // Set as dead
                            StateContext[setTrue] = true;

                            // Next
                            setTrue = setTrue.Offset(CheckDirection);
                        }
                        return;
                    }
                }

                // Next
                pos = pos.Offset(CheckDirection);
            }
        }
    }
}
