using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Encapsule all aspects of the Solver process.
    /// </summary>
    public class SolverController
    {
        /// <summary>
        /// Strong Contruction
        /// </summary>
        /// <param name="puzzleMap"></param>
        public SolverController(PuzzleMap puzzleMap)
        {
            this.puzzleMap = puzzleMap;
            attempted = false;
            stats = new SolverStats(this);

            // TODO: This should be configured, perhaps via a factory pattern
            strategy = new SolverStrategy(this);
            evaluator = new Evaluator<SolverNode>();
        }
        
        /// <summary>
        /// Map to solve with added information (Model)
        /// </summary>
        public PuzzleMap PuzzleMap
        {
            get { return puzzleMap; }
        }

        /// <summary>
        /// Map to solve
        /// </summary>
        public SokobanMap Map
        {
            get { return puzzleMap.Map; }
        }

        /// <summary>
        /// The strategy to use for this puzzle
        /// </summary>
        public SolverStrategy Strategy
        {
            get { return strategy; }
        }

        /// <summary>
        /// Itterator used to search the move tree
        /// </summary>
        public Evaluator<SolverNode> Evaluator
        {
            get { return evaluator; }
        }

        /// <summary>
        /// Stats subsystem
        /// </summary>
        public SolverStats Stats
        {
            get { return stats; }
        }

        /// <summary>
        /// Attempt to find a solution
        /// </summary>
        public EvalStatus Solve()
        {
            CodeTimer solveTime = new CodeTimer();
            solveTime.Start();
            try
            {
                if (attempted) throw new Exception("Solve cannot be re-run on a single instance, this may cause state corruption.");
                attempted = true;
                return evaluator.Evaluate(strategy);
            }
            catch (Exception ex)
            {
                throw new Exception("Solver failed.", ex);
            }
            finally
            {
                stats.EvaluationTime.AddMeasure(solveTime);
            }
        }


        private bool attempted;
        private PuzzleMap puzzleMap;
        private SolverStrategy strategy;
        private SolverStats stats;
        private Evaluator<SolverNode> evaluator;

        
    }
}
