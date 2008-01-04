using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation.Strategy
{
    /// <summary>
    /// Generic rule results
    /// </summary>
    public enum RuleResult
    {
        Skipped,
        Success,
        Failed,
        ExitSolution,
        ExitFailed,
        ExitNeutral,
        Error
    }

    /// <summary>
    /// Encapsulate a basic rule
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public abstract class StrategyRule<TState>
    {
        /// <summary>
        /// Strong Constuctor
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="name"></param>
        protected StrategyRule(StrategyPatternBase<TState> strategy, string name)
        {
            this.strategy = strategy;
            this.name = name;
        }

        /// <summary>
        /// Evaluate the rule on a statefull item (<see cref="StateContext"/> may be enriched if needed)
        /// </summary>
        /// <param name="StateContext"></param>
        /// <returns>true is an exit condition</returns>
        public abstract RuleResult Evaluate(TState StateContext);

        /// <summary>
        /// Strategy
        /// </summary>
        public StrategyPatternBase<TState> Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        private StrategyPatternBase<TState> strategy;
        private string name;
    }
}
