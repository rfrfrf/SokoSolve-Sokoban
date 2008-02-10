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

            foreach (VectorInt corner in StateContext.StaticAnalysis.CornerMap.TruePositions)
            {
                if (!StateContext.StaticAnalysis.GoalMap[corner])
                {
                    // Not a goal so it is dead
                    StateContext[corner] = true;
                }
            }

            return RuleResult.Success;
        }

       
    }
}
