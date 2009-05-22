using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SokoSolve.Common;

namespace SokoSolve.Core.Analysis.Solver
{

    /// <summary>
    /// Universal statistics mechanism
    /// </summary>
    public class SolverStats : IDisposable
    {
        /// <summary>
        /// Strong constuctor, also 'registers' all available stats
        /// </summary>
        /// <param name="controller"></param>
        public SolverStats(SolverController controller)
        {
            this.controller = controller;

            // Register all stats, for easy of use
            stats = new List<Statistic>();
            stats.Add(CurrentEvalSecs);
            stats.Add(EvaluationTime);
            stats.Add(EvaluationItterations);
            stats.Add(Nodes);
            stats.Add(NodesPerSecond);
            stats.Add(NodesFwd);
            stats.Add(NodesRev);
            
            stats.Add(Duplicates);
            stats.Add(DeadNodes);
            stats.Add(AvgEvalList);
            stats.Add(WeightingMin);
            stats.Add(WeightingMax);
            stats.Add(WeightingAvg);
            stats.Add(HintsUsed);
            stats.Add(MaxDepth);

            BestNodesFwd = new LinkedList<SolverNode>();
            BestNodesRev = new LinkedList<SolverNode>();
        }

        /// <summary>
        /// Start the stats per/sec counter
        /// </summary>
        public void Start()
        {
            threadingTimer = new Timer(TimerCallBack, null, 1000, 1000);
        }

        /// <summary>
        /// Stop the stats timer
        /// </summary>
        public void Stop()
        {
            threadingTimer.Dispose();
            threadingTimer = null;
        }

        private void TimerCallBack(object state)
        {
            try
            {
                CurrentEvalSecs.AddMeasure(1f);
                foreach (Statistic stat in stats)
                {
                    stat.SecondTick();
                }

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Searching {0:#,##0} nodes as {1:#,##0.00}/sec. EvalList length={2:#,##0.00}\t\t\t", Nodes.ValueTotal, NodesPerSecond.ValuePerSec, AvgEvalList.ValueTotal);
            }
            catch(Exception ex)
            {
                //TODO: Thread-safe bubbling
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// List of active statistics
        /// </summary>
        public List<Statistic> Stats
        {
            get { return stats; }
        }

        /// <summary>
        /// Get all stats as simple name, value labels
        /// </summary>
        /// <returns></returns>
        public SolverLabelList GetDisplayData()
        {
            SolverLabelList txt = new SolverLabelList();
            foreach (Statistic stat in stats)
            {
                txt.Add(stat.GetDisplayData());
            }

            // Manual information
            // Perfect (solution score)
            SolverLabel man = new SolverLabel("Solution Fwd Score", controller.StaticAnalysis.SolutionScoreForward.ToString("0.00") , "");
            txt.Add(man);


            // Manually add BestNodes
            string bestForward = StringHelper.Join<SolverNode>(BestNodesFwd, BestNodeToString, ", ");
            SolverLabel lb = new SolverLabel("Best Fwd Nodes", bestForward, "");
            txt.Add(lb);

            string bestRev = StringHelper.Join<SolverNode>(BestNodesRev, BestNodeToString, ", ");
            SolverLabel lbRev = new SolverLabel("Best Rev Nodes", bestRev, "");
            txt.Add(lbRev);

            return txt;
        }

        string BestNodeToString(SolverNode node)
        {
            if (node.IsChildrenEvaluated)
            {
                return string.Format("<a href=\"app://node/{0}\"><b>{1:0.00}</b></a>", node.NodeID, node.Weighting); 
            }
            return string.Format("<a href=\"app://node/{0}\">{1:0.00}</a>", node.NodeID, node.Weighting); 
        }

        /// <summary>
        /// Register a new node for stats
        /// </summary>
        /// <param name="node"></param>
        public void NewNode(SolverNode node)
        {
            if (node == null) return;

       
            if (node.IsForward)
            {
                if (BestNodesFwd.Count == 0 || node.Weighting > BestNodesFwd.Last.Value.Weighting)
                {
                    InsertSorted(BestNodesFwd, node);
                }
            }
            else
            {
                if (BestNodesRev.Count == 0 || node.Weighting > BestNodesRev.Last.Value.Weighting)
                {
                    InsertSorted(BestNodesRev, node);
                }
            }
        }

        private void InsertSorted(LinkedList<SolverNode>  list, SolverNode node)
        {
            lock(list)
            {
                if (list.Count == 0)
                {
                    list.AddFirst(node);
                    return;
                }

                LinkedListNode<SolverNode> current = list.First;
                while (current != null && current.Value.Weighting >= node.Weighting)
                {
                    current = current.Next;
                }

                if (current == null)
                {
                    if (list.Count < MaxBestNodes)
                    {
                        list.AddLast(node);
                        return;
                    }
                    return;
                }

                

                // Add
                list.AddBefore(current, node);

                // Check max length
                if (list.Count > MaxBestNodes)
                {
                    list.RemoveLast();
                }
            }
        }

        public void Clear()
        {
            stats.Clear();
            BestNodesRev.Clear();
            BestNodesFwd.Clear();
        }


        public void Dispose()
        {
            if (threadingTimer != null)
            {
                threadingTimer.Dispose();    
            }
        }


        private SolverController controller;
        private List<Statistic> stats;
        private Timer threadingTimer;

        public Statistic EvaluationTime = new Statistic("Evaluation Time", "{1:0.000} sec");
        public Statistic EvaluationItterations = new Statistic("Evaluation Itterations", "{1:0} nodes");
        public Statistic CurrentEvalSecs = new Statistic("Evaluation Total Seconds", "{1:0} sec");
        public Statistic Nodes = new Statistic("Total Nodes", "{1:0} nodes");
        public Statistic NodesFwd = new Statistic("Total Forward Nodes", "{1:0} nodes");
        public Statistic NodesRev = new Statistic("Total Reverse Nodes", "{1:0} nodes");
        public Statistic AvgEvalList = new Statistic("Eval Worker List", "{1:0}");
        public Statistic Duplicates = new Statistic("Duplicates", "{1:0} nodes");
        public Statistic DeadNodes = new Statistic("DeadNodes", "{1:0} nodes");
        public Statistic WeightingMin = new Statistic("Weighting Min", "{1:0}");
        public Statistic WeightingMax = new Statistic("Weighting Max", "{1:0}");
        public Statistic WeightingAvg = new Statistic("Weighting Avg", "{2:0.00} avg.");
        public Statistic HintsUsed = new Statistic("Hints Uses", "{1:0} hits");
        public Statistic MaxDepth = new Statistic("Max Depth", "{1:0}");
        public StatisticRollingAverage NodesPerSecond = new StatisticRollingAverage("Node per second");
        public LinkedList<SolverNode> BestNodesFwd;
        public LinkedList<SolverNode> BestNodesRev;
        public readonly int MaxBestNodes = 20;

      
    }
}
