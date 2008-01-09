using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SokoSolve.Common;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Model;
using SokoSolve.Core.Analysis.Solver.Reverse;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Encapsule all aspects of the Solver process. It is the root object and is responsible
    /// for threading and coordinating the solver process.
    /// </summary>
    public class SolverController
    {
        /// <summary>
        /// Strong Contruction
        /// </summary>
        /// <param name="puzzleMap">Map to solve</param>
        public SolverController(PuzzleMap puzzleMap)
        {
            this.puzzleMap = puzzleMap;
            debugReport = new SolverReport();
            attempted = false;
            stats = new SolverStats(this);
            exitConditions = new ExitConditions();
            isEnabled = true;

            // TODO: This should be configured, perhaps via a factory pattern
            strategy = new SolverStrategy(this);
            evaluator = new Evaluator<SolverNode>(true);

            reverseStrategy = new ReverseStrategy(this);
            reverseEvaluator = new Evaluator<SolverNode>(true);
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
        /// The strategy to use for this puzzle
        /// </summary>
        public ReverseStrategy ReverseStrategy
        {
            get { return reverseStrategy; }
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
        /// Debug report for the solver. 
        /// </summary>
        public SolverReport DebugReport
        {
            get { return debugReport; }
        }

        /// <summary>
        /// Attempt to find a solution
        /// </summary>
        public EvalStatus Solve()
        {
            if (attempted) throw new Exception("Solve cannot be re-run on a single instance, this may cause state corruption.");

            CodeTimer solveTime = new CodeTimer();
            solveTime.Start();

            try
            {
                debugReport.Append("Solver starting");
                
                // Init
                IsEnabled = true;
                stats.Start();
                attempted = true;

                // Prepare forward
                Thread.CurrentThread.Name = "FWD";

                // Prepare and start reverse
                reverseWorker = new Thread(new ThreadStart(StartReverseWorker));
                reverseWorker.Name = "REV";
                reverseWorker.Start();

                // Start forward
                debugReport.AppendTimeStamp("Forward Started.");
                EvalStatus result = evaluator.Evaluate(strategy);

                debugReport.AppendTimeStamp("Forward Complete. Status:{0}", result);

                // Wait for the reverse strategy to complete
                debugReport.AppendTimeStamp("Waiting for reverse to JOIN...");
                reverseWorker.Join();
                debugReport.AppendTimeStamp("Done.");
                
                // Rethrow on calling thread.
                if (reverseWorkerException != null) throw new Exception("Exception Throw by ReverseStrategy", reverseWorkerException);

                // Exit
                return result;
            }
            catch (Exception ex)
            {
                debugReport.AppendException(ex);
                throw new Exception("Solver failed.", ex);
            }
            finally
            {
                IsEnabled = false;
                stats.Stop();
                stats.EvaluationTime.AddMeasure(solveTime);
            }
        }

        /// <summary>
        /// Thread delegare to start the reverse thread
        /// </summary>
        private void StartReverseWorker()
        {
            try
            {
                debugReport.AppendTimeStamp("Reverse Starting. ");  
                EvalStatus revStatus = reverseEvaluator.Evaluate(reverseStrategy);
                debugReport.AppendTimeStamp("Reverse Complete. Status: {0} ", revStatus);  
            }
            catch(Exception ex)
            {
                reverseWorkerException = ex;
                debugReport.AppendException(ex);
            }
        }

        /// <summary>
        /// Check in the forward solver if a reverse node is found. Ie look for a cross-over, hence solution.
        /// </summary>
        /// <param name="reverseNode"></param>
        /// <returns></returns>
        public bool CheckChainForward(SolverNode reverseNode)
        {
            if (strategy == null) return false;
            
            TreeNode<SolverNode> match = strategy.EvaluationTree.Root.Find(delegate(TreeNode<SolverNode> item) { return ChainMatch(reverseNode, item.Data); }, int.MaxValue);
            if (match != null)
            {
                debugReport.Append("Found a forward chain {0}<->{1}", match.Data.NodeID, reverseNode.NodeID);
                match.Data.Status = SolverNodeStates.SolutionChain;
                reverseNode.Status = SolverNodeStates.SolutionChain;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check in the reverse solver if a normal forward node is found. Ie look for a cross-over, hence solution.
        /// </summary>
        /// <param name="forwardNode"></param>
        /// <returns></returns>
        public bool CheckChainBack(SolverNode forwardNode)
        {
            if (reverseStrategy == null) return false;

            TreeNode<SolverNode> match = reverseStrategy.EvaluationTree.Root.Find(delegate(TreeNode<SolverNode> item) { return ChainMatch(forwardNode, item.Data); }, int.MaxValue);
            if (match != null)
            {
                debugReport.Append("Found a reverse chain {0}<->{1}", match.Data.NodeID, forwardNode.NodeID);
                match.Data.Status = SolverNodeStates.SolutionChain;
                forwardNode.Status = SolverNodeStates.SolutionChain;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if two node are a match
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        private bool ChainMatch(SolverNode lhs, SolverNode rhs)
        {
            if (lhs == null) return false;
            if (rhs == null) return false;

            // Question: Are the cratemaps equal enough? Or do we need the move map too?
            return (lhs.CrateMap == rhs.CrateMap && lhs.MoveMap == rhs.MoveMap && lhs.MoveMap != null);
        }

        private bool attempted;
        private SolverReport debugReport;
        private Evaluator<SolverNode> evaluator;
        private ExitConditions exitConditions;
        private bool isEnabled;
        private PuzzleMap puzzleMap;
        private SolverStats stats;
        private SolverStrategy strategy;
        private ReverseStrategy reverseStrategy;
        private Evaluator<SolverNode> reverseEvaluator;
        private Thread reverseWorker;
        private Exception reverseWorkerException;
    }
}