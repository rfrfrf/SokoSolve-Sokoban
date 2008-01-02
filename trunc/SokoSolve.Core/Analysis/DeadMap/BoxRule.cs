using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures.Evaluation.Strategy;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// Check for 'boxes' squares that make the cratemap dead
    /// </summary>
    class BoxRule : StrategyRule<DeadMapState>
    {
        public BoxRule(StrategyPatternBase<DeadMapState> strategy)
            : base(strategy, "Box Rule: If a 2x2 square has 3 crates/walls and no goals, then the last position is dead")
        {
        }

        /// <summary>
        /// If a box has 3 crates/walls and no goals, then the last position is dead
        /// </summary>
        /// <param name="StateContext"></param>
        /// <returns></returns>
        public override RuleResult Evaluate(DeadMapState StateContext)
        {
            // No need to check the last lines
            for (int cx = 0; cx < StateContext.Size.Width - 1; cx++)
                for (int cy = 0; cy < StateContext.Size.Height - 1; cy++)
                {
                    // Check if already dead
                    if (StateContext[cx, cy]) continue;

                    // Check TopLeft
                    CheckBox(StateContext, new VectorInt(cx, cy));
                }
            return RuleResult.Success;
        }

        private void CheckBox(DeadMapState context, VectorInt topLeft)
        {
            VectorInt[] box = new VectorInt[4] {topLeft, topLeft.Add(1, 0), topLeft.Add(0, 1), topLeft.Add(1, 1)};

            // Check for goals: any goal means it is not dead
            if (context.GoalMap[box[0]]) return;
            if (context.GoalMap[box[1]]) return;
            if (context.GoalMap[box[2]]) return;
            if (context.GoalMap[box[3]]) return;

            // Count crates and walls
            int count = 0;
            if (context.WallMap[box[0]]) count++;
            if (context.WallMap[box[1]]) count++;
            if (context.WallMap[box[2]]) count++;
            if (context.WallMap[box[3]]) count++;

            if (count >= 3)
            {
                // We have a winner, there is a box and this position is dead
                context[topLeft] = true;
                return;
            }

            if (context.CrateMap != null)
            {
                if (context.CrateMap[box[0]]) count++;
                if (context.CrateMap[box[1]]) count++;
                if (context.CrateMap[box[2]]) count++;
                if (context.CrateMap[box[3]]) count++;
            }


            if (count == 4)
            {
                // We have a winner, there is a box and this position is dead
                context[topLeft] = true;
                return;
            }

           
        }
    }
}
