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
            debugReport = new SolverReport();
            attempted = false;
            stats = new SolverStats(this);
            exitConditions = new ExitConditions();

            // TODO: This should be configured, perhaps via a factory pattern
            strategy = new SolverStrategy(this);
            evaluator = new Evaluator<SolverNode>(true);

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
        /// The conditions under which the solver should exit without solution
        /// </summary>
        public ExitConditions ExitConditions
        {
            get { return exitConditions; }
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
        /// Helper for threading (allow the solver to be stopped)
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
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
                debugReport.Append("Starting");
                IsEnabled = true;
                stats.Start();
                if (attempted) throw new Exception("Solve cannot be re-run on a single instance, this may cause state corruption.");
                attempted = true;
                EvalStatus result = evaluator.Evaluate(strategy);

                debugReport.AppendLabel("Complete", result.ToString());
                return result;
            }
            catch (Exception ex)
            {
                debugReport.Append(ex.Message);
                debugReport.Append(ex.StackTrace);
                throw new Exception("Solver failed.", ex);
            }
            finally
            {
                
                IsEnabled = false;
                stats.Stop();
                stats.EvaluationTime.AddMeasure(solveTime);
            }
        }

        public void BuildReport()
        {
            
        }

        public SolverReport DebugReport
        {
            get { return debugReport; }
        }

        private bool attempted;
        private PuzzleMap puzzleMap;
        private SolverStrategy strategy;
        private SolverStats stats;
        private Evaluator<SolverNode> evaluator;
        private bool isEnabled;
        private SolverReport debugReport;
        private ExitConditions exitConditions;
        
    }
}

