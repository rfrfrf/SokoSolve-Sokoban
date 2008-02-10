using System;
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
    /// Check which nodes form a recess without any goals. This rule assumes that the Corner rule evaluates first
    /// </summary>
    public class RecessRule : StrategyRule<DeadMapState>
    {
        public RecessRule(StrategyPatternBase<DeadMapState> strategy): base(strategy, "Dead Recess: Count(Crate) > Count(Goals)")
        {
        }


        /// <summary>
        /// Evaluate the recess rule
        /// </summary>
        /// <param name="StateContext"></param>
        /// <returns></returns>
        public override RuleResult Evaluate(DeadMapState StateContext)
        {
            if (!StateContext.IsDynamic)
            {
                foreach (Recess recess in StateContext.StaticAnalysis.Recesses)
                {
                    // Is Statically dead?
                    if (recess.GoalCount == 0)
                    {
                        foreach (VectorInt recessCell in recess.TruePositions)
                        {
                            StateContext[recessCell] = true;
                        }
                    }
                }
            }
            else
            {
                // Using StaticAnalysis recess list find dead positions
                Bitmap hits = StateContext.StaticAnalysis.RecessMap.BitwiseAND(StateContext.CrateMap);
                if (hits.Count > 0)
                {
                    // Crates on recesses, check
                    foreach (VectorInt recessCrate in hits.TruePositions)
                    {
                        Recess recess = FindRecess(StateContext.StaticAnalysis, recessCrate);
                        int crateCount = StateContext.CrateMap.BitwiseAND(recess).Count;
                        if (crateCount > recess.GoalCount)
                        {
                            // Dead recess
                            foreach (VectorInt recessCell in recess.TruePositions)
                            {
                                StateContext[recessCell] = true;
                            }
                        }
                    }
                }    
            }
            

            return RuleResult.Success;
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="staticAnalysis"></param>
        /// <param name="cellPos"></param>
        /// <returns></returns>
        private Recess FindRecess(StaticAnalysis staticAnalysis, VectorInt cellPos)
        {
            foreach (Recess recess in staticAnalysis.Recesses)
            {
                if (recess[cellPos] == true) return recess;
            }
            return null;
        }
    }
}
