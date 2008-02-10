using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation.Strategy;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// Analyse a map to see if it is solveable or unsolvable/dead
    /// </summary>
    public class DeadMapAnalysis : StrategyPatternBase<DeadMapState>
    {
        /// <summary>
        /// Default Constructor. Register all strategy rules
        /// </summary>
        public DeadMapAnalysis(StaticAnalysis staticAnalysis) : base("DeadMap analysis")
        {
            this.staticAnalysis = staticAnalysis;

            Register(new CornerRule(this));
            Register(new BoxRule(this));
            Register(new RecessRule(this));
            hintsRule = new HintsRule(this, staticAnalysis);
            Register(hintsRule);
            Register(new CleanUpRule(this));
            Register(new BlockedDoorRule(this));
        }

        /// <summary>
        /// Build a deadmap
        /// </summary>
        /// <param name="crateMap"></param>
        /// <param name="goalMap"></param>
        /// <param name="wallMap"></param>
        /// <returns></returns>
        public DeadMapState BuildDeadMap(SolverNode dynamicNode, Bitmap goalMap, Bitmap wallMap)
        {
            DeadMapState result = null;
            if (dynamicNode == null)
            {
                result = new DeadMapState(goalMap, wallMap, this);
            }
            else
            {
                result = new DeadMapState(dynamicNode, goalMap, wallMap, this);
            }
            Evaluate(result);
            return result;
        }

        public void LateRuleInit()
        {
            hintsRule.ProcessHints();
        }

        /// <summary>
        /// Static Analysis class
        /// </summary>
        public StaticAnalysis StaticAnalysis
        {
            get
            {
                return staticAnalysis;
            }
        }

        HintsRule hintsRule;
        private SolverStrategy strategy;
        private StaticAnalysis staticAnalysis;
    }

    
  
    
}
