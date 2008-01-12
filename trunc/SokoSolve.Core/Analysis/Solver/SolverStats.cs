using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SokoSolve.Core.Analysis.Solver
{

    /// <summary>
    /// Universal statistics mechanism
    /// </summary>
    public class SolverStats
    {
        public SolverStats(SolverController controller)
        {
            this.controller = controller;

            // Register all stats, for easy of use
            stats = new List<Statistic>();
            stats.Add(CurrentEvalSecs);
            stats.Add(EvaluationTime);
            stats.Add(EvaluationItterations);
            stats.Add(Nodes);
            stats.Add(Duplicates);
            stats.Add(DeadNodes);
            stats.Add(AvgEvalList);
            stats.Add(MoveMapTime);
            stats.Add(EvalTime);
            stats.Add(EvalChildTime);
            stats.Add(DuplicateCheckTime);
            stats.Add(WeightingMin);
            stats.Add(WeightingMax);
            stats.Add(WeightingAvg);
            stats.Add(HintsUsed);
            stats.Add(MaxDepth);
            
            
        }

        public void Start()
        {
            threadingTimer = new Timer(TimerCallBack, null, 1000, 1000);
        }

        public void Stop()
        {
            threadingTimer.Dispose();
            threadingTimer = null;
        }

        private void TimerCallBack(object state)
        {
            if (controller.State != SolverController.States.Running)
            {
                CurrentEvalSecs.AddMeasure(1f);
            }
        }

        /// <summary>
        /// List of active statistics
        /// </summary>
        public List<Statistic> Stats
        {
            get { return stats; }
        }

        public SolverLabelList GetDisplayData()
        {
            SolverLabelList txt = new SolverLabelList();
            foreach (Statistic stat in stats)
            {
                txt.Add(stat.GetDisplayData());
            }
            return txt;
        }

        private SolverController controller;
        private List<Statistic> stats;
        private Timer threadingTimer;

        public Statistic EvaluationTime = new Statistic("Evaluation Time", "{1:0.000} sec");
        public Statistic EvaluationItterations = new Statistic("Evaluation Itterations", "{1:0} nodes");
        public Statistic CurrentEvalSecs = new Statistic("Evaluation Total Seconds", "{1:0} sec");
        public Statistic Nodes = new Statistic("Total Nodes", "{1:0} nodes");
        public Statistic AvgEvalList = new Statistic("Eval Worker List", "{1:0}");
        public Statistic MoveMapTime = new Statistic("MoveMapTime", "{1:0.000} sec");
        public Statistic EvalTime = new Statistic("EvalTime", "{1:0.000} sec");
        public Statistic EvalChildTime = new Statistic("EvalChildTime", "{1:0.000} sec");
        public Statistic DuplicateCheckTime = new Statistic("DuplicateCheckTime", "{1:0.000} sec");
        public Statistic Duplicates = new Statistic("Duplicates", "{1:0} nodes");
        public Statistic DeadNodes = new Statistic("DeadNodes", "{1:0} nodes");
        public Statistic WeightingMin = new Statistic("Weighting Min", "{1:0}");
        public Statistic WeightingMax = new Statistic("Weighting Max", "{1:0}");
        public Statistic WeightingAvg = new Statistic("Weighting Avg", "{2:0.00} avg.");
        public Statistic HintsUsed = new Statistic("Hints Uses", "{1:0} hits");
        public Statistic MaxDepth = new Statistic("Max Depth", "{1:0}");
    }
}
