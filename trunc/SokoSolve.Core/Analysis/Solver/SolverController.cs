using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;
using SokoSolve.Core.Model;
using SokoSolve.Core.Analysis.Solver.Reverse;
using SokoSolve.Core.Model.DataModel;
using Path=SokoSolve.Common.Structures.Path;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Encapsule all aspects of the Solver process. It is the root object and is responsible
    /// for threading and coordinating the solver process.
    /// </summary>
    public class SolverController : IDisposable
    {
        /// <summary>
        /// Strong Contruction
        /// </summary>
        /// <param name="puzzleMap">Map to solve</param>
        public SolverController(PuzzleMap puzzleMap)
        {
            this.state = States.NotStarted;
            this.puzzleMap = puzzleMap;
            debugReport = new SolverReport();
            stats = new SolverStats(this);
            exitConditions = new ExitConditions();


            // TODO: This should be configured, perhaps via a factory pattern
            strategy = new SolverStrategy(this);
            evaluator = new Evaluator<SolverNode>(true);

            reverseStrategy = new ReverseStrategy(this);
            reverseEvaluator = new Evaluator<SolverNode>(true);
        }

        /// <summary>
        /// Controller internal states (used to controll threading, validation, etc)
        /// This is similar to EvalStatus , but includes paused
        /// </summary>
        public enum States
        {
            NotStarted,
            Paused,
            Running,
            Cancelled,
            CompleteSolution,
            CompleteNoSolution,
            CompleteNoSolutionInConstaints,
            Error
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
        /// Provide static analysis. Static Analysis is stateless in terms of the solver progress (SolverNode)
        /// </summary>
        public StaticAnalysis StaticAnalysis
        {
            get { return staticAnalysis; }
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
        /// The internal calculation state <see cref="States"/>
        /// </summary>
        public States State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Debug report for the solver. 
        /// </summary>
        public SolverReport DebugReport
        {
            get { return debugReport; }
        }

        /// <summary>
        /// Cancel the current solver
        /// </summary>
        public void Cancel()
        {
            state = States.Cancelled;
        }

        /// <summary>
        /// Initialise and perform the static puzzle analysis
        /// </summary>
        public void Init()
        {
            staticAnalysis = new StaticAnalysis(this);
            staticAnalysis.Analyse();
        }

        /// <summary>
        /// Attempt to find a solution
        /// </summary>
        /// <returns>Solution class, or null which means no solution found</returns>
        public SolverResult Solve()
        {
            if (state != States.NotStarted) throw new Exception("Solve cannot be re-run on a single instance, this may cause state corruption.");

            if (staticAnalysis == null)
            {
                Init();
            }

            SolverResult solverResult = new SolverResult();
            solverResult.DebugReport = debugReport;

            CodeTimer solveTime = new CodeTimer("Solver Timer");
            solveTime.Start();

            try
            {
                debugReport.Append("Solver starting");
                
                // Init
                state = States.Running;
                // Start the stats timer (per sec)
                stats.Start(); 
                
                // Prepare forward
                if (Thread.CurrentThread.Name == null)
                {
                    Thread.CurrentThread.Name = "FWD";    
                }

                // Prepare and start reverse
                reverseWorker = new Thread(new ThreadStart(StartReverseWorker));
                reverseWorker.Name = "REV";
                reverseWorker.Priority = Thread.CurrentThread.Priority;
                reverseWorker.Start();

                // Start forward
                debugReport.AppendTimeStamp("Forward Started.");
                EvalStatus result = evaluator.Evaluate(strategy);
                if (state == States.Running)
                {
                    if (result == EvalStatus.CompleteSolution)
                    {
                        state = States.CompleteSolution;
                    }
                    else if (result == EvalStatus.CompleteNoSolution)
                    {
                        state = States.CompleteNoSolution;
                    }
                    else
                    {
                        state = States.CompleteNoSolutionInConstaints;
                    }
                }
                

                debugReport.AppendTimeStamp("Forward Complete. Status:{0}", result);

                // Wait for the reverse strategy to complete
                debugReport.AppendTimeStamp("Waiting for reverse to JOIN...");
                reverseWorker.Join();
                debugReport.AppendTimeStamp("Done.");
                
                // Rethrow on calling thread.
                if (reverseWorkerException != null) throw new Exception("Exception Throw by ReverseStrategy", reverseWorkerException);

                if (state == States.CompleteSolution)
                {
                    solverResult.Solutions = BuildSolutionPath();
                }

                if (state == States.Running)
                {
                    state = States.CompleteNoSolutionInConstaints;
                }

                // Exit
               
            }
            catch (Exception ex)
            {
                state = States.Error;
                debugReport.AppendException(ex);
                solverResult.Exception = ex;
            }
            finally
            {
               
                // Stop the timer
                stats.Stop();
                stats.EvaluationTime.AddMeasure(solveTime);
            }

            solverResult.ControllerResult = state;
            solverResult.Build(this);

            return solverResult;
        }

        /// <summary>
        /// Generate the solution from the solver tree
        /// </summary>
        /// <returns></returns>
        private List<Solution> BuildSolutionPath()
        {
            List<INode<SolverNode>> solutions = evaluator.Solutions;
            if (solutions == null || solutions.Count == 0)
            {
                solutions = reverseEvaluator.Solutions;
            }
            if (solutions == null || solutions.Count == 0)
            {
                // No solution forward or reverse
                throw new Exception("A solution was expected, but not found");
            }

            List<Solution> results = new List<Solution>();
            foreach (INode<SolverNode> node in solutions)
            {
                if (node.Data.Status == SolverNodeStates.Solution)
                {
                    if (node.Data.IsForward)
                    {
                        Solution simpleForward = new Solution(puzzleMap, Map.Player);
                        simpleForward.Set(strategy.BuildPath(node.Data));

                        simpleForward.Details = new GenericDescription();
                        simpleForward.Details.Name = "SokoSolve Solution";
                        simpleForward.Details.Author = new GenericDescriptionAuthor();
                        simpleForward.Details.Date = DateTime.Now;
                        simpleForward.Details.DateSpecified = true;

                        // Build a description
                        SolverLabelList labels = Stats.GetDisplayData();
                        labels.Add("Machine", string.Format("{0} Running {1}.", DebugHelper.GetCPUDescription(), Environment.OSVersion));

                        simpleForward.Details.Description = labels.ToString();

                        results.Add(simpleForward);    
                    }
                    else
                    {

                        Solution simpleForward = new Solution(puzzleMap, Map.Player);
                        simpleForward.Set(reverseStrategy.BuildPath(node.Data));

                        simpleForward.Details = new GenericDescription();
                        simpleForward.Details.Name = "SokoSolve Solution";
                        simpleForward.Details.Author = new GenericDescriptionAuthor();
                        simpleForward.Details.Date = DateTime.Now;
                        simpleForward.Details.DateSpecified = true;

                        // Build a description
                        SolverLabelList labels = Stats.GetDisplayData();
                        labels.Add("Machine", string.Format("{0} Running {1}.", DebugHelper.GetCPUDescription(), Environment.OSVersion));

                        simpleForward.Details.Description = labels.ToString();

                        results.Add(simpleForward);    
                    }
                    
                }

                if (node.Data.Status == SolverNodeStates.SolutionChain)
                {
                    if (node.Data.ChainSolutionLink == null) throw new InvalidDataException("ChainSolutionLink must be set");

                    SolverNode forward = null;
                    SolverNode reverse = null;
                    if (node.Data.IsForward)
                    {
                        forward = node.Data;
                        reverse = node.Data.ChainSolutionLink;
                    }
                    else
                    {
                        forward = node.Data.ChainSolutionLink;
                        reverse = node.Data;
                    }
        
                    // Build the paths
                    Path forwardPortion = strategy.BuildPath(forward);
                    Path reversePortion = this.reverseStrategy.BuildPath(reverse);

                    debugReport.AppendLabel("Forward Chain", "[{0}->{1}] links to {2}", forward.PlayerPositionBeforeMove,forward.PlayerPosition, forward.ChainSolutionLink.PlayerPosition);
                    debugReport.AppendLabel("Reverse Chain", "[{0}->{1}] links to {2}", reverse.PlayerPositionBeforeMove, reverse.PlayerPosition, reverse.ChainSolutionLink.PlayerPosition);
                  

                    debugReport.AppendLabel("Forward", string.Format("[{0}] {1}", forwardPortion.Count, StringHelper.Join(forwardPortion.MovesAsPosition, null, ", ")));
                    debugReport.AppendLabel("Reverse", string.Format("[{0}] {1}", reversePortion.Count, StringHelper.Join(reversePortion.MovesAsPosition, null, ", ")));
                    

                    Path fullPath = new Path(Map.Player);
                    fullPath.Add(forwardPortion);
                    fullPath.Add(reversePortion);

                    // Join them
                    Solution solution = new Solution(puzzleMap, Map.Player);
                    solution.Set(fullPath);

                    solution.Details = new GenericDescription();
                    solution.Details.Name = "SokoSolve Solution";
                    solution.Details.Author = new GenericDescriptionAuthor();
                    solution.Details.Date = DateTime.Now;
                    solution.Details.DateSpecified = true;

                    // Build a description
                    SolverLabelList labels = Stats.GetDisplayData();
                    labels.Add("Machine", string.Format("{0} Running {1}.", DebugHelper.GetCPUDescription(), Environment.OSVersion));

                    solution.Details.Description = labels.ToString();

                    results.Add(solution);
                }
            }

            // Sanity check
            string firsterror;
            foreach (Solution solution in results)
            {
                if (!solution.Test(out firsterror)) throw new Exception("Sanity Check Failed. Solution is not valid: "+firsterror);
            }

            return results;
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
                if (state == States.Running)
                {
                    if (revStatus == EvalStatus.CompleteSolution)
                    {
                        state = States.CompleteSolution;
                    }
                    else if (revStatus == EvalStatus.CompleteNoSolution)
                    {
                        state = States.CompleteNoSolution;
                    }
                    else
                    {
                        state = States.CompleteNoSolutionInConstaints;
                    }
                }
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

            SolverNode match = strategy.CheckDuplicate(reverseNode);
            if (match != null)
            {
                debugReport.Append("Found a forward chain {0}<->{1}", match.NodeID, reverseNode.NodeID);
                match.Status = SolverNodeStates.SolutionChain;
                reverseNode.Status = SolverNodeStates.SolutionChain;
                state = States.CompleteSolution;

                // Set the link property
                reverseNode.ChainSolutionLink = match;
                match.ChainSolutionLink = reverseNode;
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

            SolverNode match = reverseStrategy.CheckDuplicate(forwardNode);
            if (match != null)
            {
                debugReport.Append("Found a reverse chain {0}<->{1}", match.NodeID, forwardNode.NodeID);
                match.Status = SolverNodeStates.SolutionChain;
                forwardNode.Status = SolverNodeStates.SolutionChain;
                state = States.CompleteSolution;

                // Set the link property
                forwardNode.ChainSolutionLink = match;
                match.ChainSolutionLink = forwardNode;
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

        private SolverReport debugReport;
        private Evaluator<SolverNode> evaluator;
        private ExitConditions exitConditions;
        private PuzzleMap puzzleMap;
        private SolverStats stats;
        private SolverStrategy strategy;
        private ReverseStrategy reverseStrategy;
        private StaticAnalysis staticAnalysis;
        private Evaluator<SolverNode> reverseEvaluator;
        private Thread reverseWorker;
        private Exception reverseWorkerException;
        private States state;

        #region IDisposable Members

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            
        }

        #endregion
    }
}