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
            if (StateContext.CrateMap == null) return RuleResult.Skipped; // This only really applies to dynamic boxes (corner rule is more effective)

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


            int walls = 0;
            if (context.WallMap[box[0]]) walls++;
            if (context.WallMap[box[1]]) walls++;
            if (context.WallMap[box[2]]) walls++;
            if (context.WallMap[box[3]]) walls++;
            if (walls == 4)
            {
                return;
            }

            int goals = 0;
            if (context.GoalMap[box[0]]) goals++;
            if (context.GoalMap[box[1]]) goals++;
            if (context.GoalMap[box[2]]) goals++;
            if (context.GoalMap[box[3]]) goals++;

            int crates = 0;
            if (context.CrateMap[box[0]]) crates++;
            if (context.CrateMap[box[1]]) crates++;
            if (context.CrateMap[box[2]]) crates++;
            if (context.CrateMap[box[3]]) crates++;
            
            if (walls + crates == 4 && crates > goals)
            {
                // Box rule applies: All non goal crates are dead
                if (context.CrateMap[box[0]] && !context.GoalMap[box[0]]) context[box[0]] = true;
                if (context.CrateMap[box[1]] && !context.GoalMap[box[1]]) context[box[1]] = true;
                if (context.CrateMap[box[2]] && !context.GoalMap[box[2]]) context[box[2]] = true;
                if (context.CrateMap[box[3]] && !context.GoalMap[box[3]]) context[box[3]] = true;
            }
        }
    }
}
