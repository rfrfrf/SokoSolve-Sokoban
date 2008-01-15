using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using SokoSolve.Common;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.Analysis;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Return result from a solver attempt
    /// </summary>
    public class SolverResult
    {
        #region CalculationResult enum

        public enum CalculationResult
        {
            NoSolution,
            SolutionFound,
            GaveUp,
            Cancelled,
            Error
        }

        #endregion

        private CalculationResult status;
        private SolverController.States controllerResult;
        private DebugReport debugReport;
        private Exception exception;
        private SolverResultInfo info;
        private List<Solution> solutions;
        private string summary = "Not set";
        PuzzleMap map;


        public PuzzleMap Map
        {
            get { return map; }
            set { map = value; }
        }

        /// <summary>
        /// Was a solution found
        /// </summary>
        public bool HasSolution
        {
            get { return solutions != null && solutions.Count > 0; }
        }

        /// <summary>
        /// The solutions found
        /// </summary>
        public List<Solution> Solutions
        {
            get { return solutions; }
            set { solutions = value; }
        }

        /// <summary>
        /// The calculation outcome
        /// </summary>
        public CalculationResult Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Status as a human readable string
        /// </summary>
        public string StatusString
        {
            get
            {
                switch(status)
                {
                    case(CalculationResult.Cancelled) : return "Cancelled";
                    case (CalculationResult.Error): return "Error";
                    case (CalculationResult.GaveUp): return "Gave Up";
                    case (CalculationResult.SolutionFound): return "Solution";
                    case (CalculationResult.NoSolution): return "Unsolvable";
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 1-liner summary
        /// </summary>
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        /// <summary>
        /// Exception of that occured
        /// </summary>
        public Exception Exception
        {
            get { return exception; }
            set { exception = value; }
        }

        /// <summary>
        /// Solver Result
        /// </summary>
        public SolverController.States ControllerResult
        {
            get { return controllerResult; }
            set { controllerResult = value; }
        }

        /// <summary>
        /// More detailed information
        /// </summary>
        public SolverResultInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        /// <summary>
        /// Debug report
        /// </summary>
        public DebugReport DebugReport
        {
            get { return debugReport; }
            set { debugReport = value; }
        }

        /// <summary>
        /// Build the result from the completed solver
        /// </summary>
        /// <param name="controller"></param>
        public void Build(SolverController controller)
        {
            map = controller.PuzzleMap;

            // Build type strong info class
            info = new SolverResultInfo();
            info.TotalNodes = (int) controller.Stats.Nodes.ValueTotal;
            info.TotalSecond = controller.Stats.EvaluationTime.ValueTotal;
            info.Machine = DebugHelper.GetCPUDescription();
            info.RatingScore = PuzzleAnalysis.CalcRating(map.Map);
            foreach (Statistic stat in controller.Stats.Stats)
            {
                SolverLabel lb = stat.GetDisplayData();

                info.InfoValues.Add(lb.Name, lb.Value);
            }


            // Build the exit status and summary
            if (HasSolution)
            {
                // General Sucess
                status = CalculationResult.SolutionFound;
                StringBuilder sb = new StringBuilder();
                if (solutions.Count == 1)
                {
                    sb.Append("Solution");
                }
                else
                {
                    sb.AppendFormat("{0} solutions", solutions.Count);
                }

                sb.AppendFormat(" in {0} and {1} nodes at {2:0} nodes/s (puzzle score {3})",
                                StringHelper.ToString(TimeSpan.FromSeconds(info.TotalSecond)),
                                info.TotalNodes,
                                info.TotalNodes/info.TotalSecond,
                                info.RatingScore);
                summary = sb.ToString();
            }
            else
            {
                if (exception != null)
                {
                    status = CalculationResult.Error;
                    summary = string.Format("Error ({0})", exception.Message);
                }
                else
                {
                    if (controllerResult == SolverController.States.Cancelled)
                    {
                        status = CalculationResult.Cancelled;
                        summary =
                            string.Format(" Cancelled after {0} sec and {1} nodes at {2:0} nodes/s (puzzle score {3})",
                                          StringHelper.ToString(TimeSpan.FromSeconds(info.TotalSecond)),
                                          info.TotalNodes,
                                          info.TotalNodes/info.TotalSecond,
                                          info.RatingScore);
                    }
                    else if (controllerResult == SolverController.States.CompleteNoSolution)
                    {
                        status = CalculationResult.NoSolution;
                        summary = "The puzzle does not have a solution";
                    }
                    else
                    {
                        status = CalculationResult.GaveUp;
                        summary = "";
                        summary =
                            string.Format(
                                "The solver could not find a within the given constraints. Tried for {0} sec and {1} nodes at {2:0} nodes/s (puzzle score {3})",
                                StringHelper.ToString(TimeSpan.FromSeconds(info.TotalSecond)),
                                info.TotalNodes,
                                info.TotalNodes/info.TotalSecond,
                                info.RatingScore);
                    }
                }
            }
        }
    }

    public class SolverResultInfo
    {
        private NameValueCollection infoValues;
        private string machine;
        private double ratingScore;
        private int totalNodes;
        private float totalSecond;

        public SolverResultInfo()
        {
            infoValues = new NameValueCollection();
        }

        public int TotalNodes
        {
            get { return totalNodes; }
            set { totalNodes = value; }
        }

        public float TotalSecond
        {
            get { return totalSecond; }
            set { totalSecond = value; }
        }

        public string Machine
        {
            get { return machine; }
            set { machine = value; }
        }

        public double RatingScore
        {
            get { return ratingScore; }
            set { ratingScore = value; }
        }

        public NameValueCollection InfoValues
        {
            get { return infoValues; }
        }
    }
}