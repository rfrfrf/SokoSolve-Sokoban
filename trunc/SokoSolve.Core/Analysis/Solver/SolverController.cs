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
        public SolverController(PuzzleMap map) : this(map.Map)
        {
            PuzzleMap = map;
        }

        /// <summary>
        /// Strong Contruction
        /// </summary>
        /// <param name="map">Map to solve</param>
        public SolverController(SokobanMap map)
        {
            if (map == null) throw new ArgumentNullException("map");
            this.settings = new Settings();

            this.state = States.NotStarted;
            this.map = map;
            debugReport = new SolverReport();

            debugReport.AppendHeading(1, "SokoSolve | Solver Debug Report v{0}", ProgramVersion.VersionString);

            debugReport.Append("Creating Statistics");
            stats = new SolverStats(this);
            debugReport.Append("Creating Exit Conditions");
            exitConditions = new ItteratorExitConditions();


            // TODO: This should be configured, perhaps via a factory pattern
            debugReport.Append("Creating Forward Strategy");
            strategy = new SolverStrategy(this);
            debugReport.Append("Creating Forward Evaluator");
            evaluator = new Evaluator<SolverNode>(true);

            debugReport.Append("Creating Reverse Strategy");
            reverseStrategy = new ReverseStrategy(this);
            debugReport.Append("Creating Reverse Evaluator");
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
        /// Map to solve
        /// </summary>
        public SokobanMap Map
        {
            get { return map; }
        }

        /// <summary>
        /// This is for information purpuse (use Map instead), only Solutions use this value
        /// </summary>
        public PuzzleMap PuzzleMap { get; set; }
        

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
        /// Solver Settings (configuration)
        /// </summary>
        public Settings Settings
        {
            get { return settings; }
        }

        /// <summary>
        /// The conditions under which the solver should exit without solution
        /// </summary>
        public ItteratorExitConditions ExitConditions
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
            debugReport.AppendHeading(2, "Static Puzzle Analysis");
            staticAnalysis = new StaticAnalysis(this);
            staticAnalysis.Analyse();
            debugReport.CompleteSection();
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

            debugReport.AppendHeading(2, "Controller | Solving");

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
                if (settings.UseReverseSolver)
                {
                    reverseWorker = new Thread(new ThreadStart(StartReverseWorker));
                    reverseWorker.Name = "REV";
                    reverseWorker.Priority = Thread.CurrentThread.Priority;
                    reverseWorker.Start();
                }

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
                if (reverseWorker != null)
                {
                    reverseWorker.Join();    
                }
                
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

            debugReport.CompleteSection();

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
                        Solution simpleForward = new Solution(PuzzleMap, Map.Player);
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

                        Solution simpleForward = new Solution(PuzzleMap, Map.Player);
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
                    Solution solution = new Solution(PuzzleMap, Map.Player);
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
                if (!solution.Test(map, out firsterror)) throw new Exception("Sanity Check Failed. Solution is not valid: "+firsterror);
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
            if (stats != null)
            {
                stats.NewNode(reverseNode);
            }

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
            if (stats != null)
            {
                stats.NewNode(forwardNode);
            }

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


        #region IDisposable Members

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (reverseWorker != null)
            {
                if (reverseWorker.IsAlive) reverseWorker.Join(100);
                reverseWorker = null;
            }

            if (strategy != null)
            {
                strategy.Dispose();
                strategy = null;
            }
            if (evaluator != null)
            {
                evaluator.Dispose();
                evaluator = null;
            }
            if (reverseEvaluator != null)
            {
                reverseEvaluator.Dispose();
                reverseEvaluator = null;
            }
            if (reverseStrategy != null)
            {
                reverseStrategy.Dispose();
                reverseStrategy = null;
            }
            if (stats != null)
            {
                stats.Clear();
                stats.Dispose();
                stats = null;
            }
        }

        #endregion


        /// <summary>
        /// Find an active node by ID
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public SolverNode FindNode(string nodeID)
        {
            TreeNode<SolverNode> node = strategy.EvaluationTree.Root.Find(delegate(TreeNode<SolverNode> item) { return item.Data.NodeID == nodeID; }, int.MaxValue);
            if (node != null) return node.Data;

            node = ReverseStrategy.EvaluationTree.Root.Find(delegate(TreeNode<SolverNode> item) { return item.Data.NodeID == nodeID; }, int.MaxValue);
            if (node != null) return node.Data;

            return null;
        }

        private SolverReport debugReport;
        private Evaluator<SolverNode> evaluator;
        private ItteratorExitConditions exitConditions;
        private SokobanMap map;
        private SolverStats stats;
        private SolverStrategy strategy;
        private ReverseStrategy reverseStrategy;
        private StaticAnalysis staticAnalysis;
        private Evaluator<SolverNode> reverseEvaluator;
        private Thread reverseWorker;
        private Exception reverseWorkerException;
        private States state;
        private Settings settings;

    }
}