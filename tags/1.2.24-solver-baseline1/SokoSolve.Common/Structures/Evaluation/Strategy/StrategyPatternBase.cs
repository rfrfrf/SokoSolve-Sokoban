using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation.Strategy
{
    /// <summary>
    /// Provide the must basic well-defined strategy pattern. It allow encapsulation of a simple set of 
    /// rules. The rules are pre-registered and evaluated in sequence
    /// </summary>
    /// <typeparam name="TState">State-pattern, which may be enriched during evaluation</typeparam>
    public abstract class StrategyPatternBase<TState>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Strategy Name</param>
        protected StrategyPatternBase(string name)
        {
            rules = new List<StrategyRule<TState>>();
            this.name = name;
        }

        /// <summary>
        /// Evaluate all rules in sequence
        /// </summary>
        /// <param name="StateContext"></param>
        /// <returns>true is the returned exit state</returns>
        public virtual RuleResult Evaluate(TState StateContext)
        {
            try
            {
                ruleResults = new List<RuleResult>(rules.Count);

                foreach (StrategyRule<TState> rule in rules)
                {
                    RuleResult result = rule.Evaluate(StateContext);
                    ruleResults.Add(result);
                    if (result == RuleResult.ExitFailed) return result;
                    if (result == RuleResult.ExitSolution) return result;
                    if (result == RuleResult.ExitNeutral) return result;
                }
                return RuleResult.ExitNeutral;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed during evaluation of strategy "+name, ex);
            }
        }

        /// <summary>
        /// Last Rule results in order
        /// </summary>
        public List<RuleResult> RuleResults
        {
            get { return ruleResults; }
        }

        /// <summary>
        /// Register a new rule in the strategy
        /// </summary>
        /// <param name="newRule"></param>
        protected void Register(StrategyRule<TState> newRule)
        {
            rules.Add(newRule);
        }

        private List<StrategyRule<TState>> rules;
        private List<RuleResult> ruleResults;

        private string name;
    }
}
