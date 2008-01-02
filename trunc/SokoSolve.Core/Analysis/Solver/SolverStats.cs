using System;
using System.Collections.Generic;
using System.Text;

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
            stats.Add(EvaluationTime);
            stats.Add(Nodes);
            stats.Add(AvgEvalList);
            stats.Add(MoveMapTime);
            stats.Add(EvalTime);
            stats.Add(EvalChildTime);
            stats.Add(DuplicateCheckTime);
            stats.Add(Duplicates);
            
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

        public Statistic EvaluationTime = new Statistic("Evaluation Time", "sec");
        public Statistic Nodes = new Statistic("Total Nodes", "nodes");
        public Statistic AvgEvalList = new Statistic("Avg Eval Worker List", "nodes");
        public Statistic MoveMapTime = new Statistic("MoveMapTime", "sec");
        public Statistic EvalTime = new Statistic("EvalTime", "sec");
        public Statistic EvalChildTime = new Statistic("EvalChildTime", "sec");
        public Statistic DuplicateCheckTime = new Statistic("DuplicateCheckTime", "sec");
        public Statistic Duplicates = new Statistic("Duplicates", "count");
    }
}
