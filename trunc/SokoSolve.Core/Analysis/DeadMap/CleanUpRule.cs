using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures.Evaluation.Strategy;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// Does not check any dead states, rather cleans up  the map
    /// </summary>
    class CleanUpRule: StrategyRule<DeadMapState>
    {
        public CleanUpRule(StrategyPatternBase<DeadMapState> strategy)
            : base(strategy, "Does not check any dead states, rather cleans up  the map")
        {
        }


        /// <summary>
        /// Evaluate the rule on a statefull item (<see cref="StateContext"/> may be enriched if needed)
        /// </summary>
        /// <param name="StateContext"></param>
        /// <returns>true is an exit condition</returns>
        public override RuleResult Evaluate(DeadMapState StateContext)
        {
            for (int cx = 0; cx < StateContext.Size.Width ; cx++)
                for (int cy = 0; cy < StateContext.Size.Height; cy++)
                {
                    // If it is a bounrdy then don't set it dead (this is unnesseary and will aid readability)
                    if (StateContext.Analysis.BoundryMap[cx, cy])
                        StateContext[cx, cy] = false;
                }

            return RuleResult.Success;
        }
    }
}
