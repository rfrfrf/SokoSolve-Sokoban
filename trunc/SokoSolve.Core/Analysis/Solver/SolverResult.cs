using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Return result from a solver attempt
    /// </summary>
    public class SolverResult
    {
        public enum ExitTypes
        {
            SolutionFound,
            Cancelled,
            Error
        }

        public bool HasSolution
        {
            get { return solutions != null && solutions.Count > 0;  }
        }


        public List<Solution> Solutions
        {
            get { return solutions; }
            set { solutions = value; }
        }

        public ExitTypes Exit
        {
            get { return exit; }
            set { exit = value; }
        }

        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        public Exception Exception
        {
            get { return exception; }
            set { exception = value; }
        }

        public SolverController.States ControllerResult
        {
            get { return controllerResult; }
            set { controllerResult = value; }
        }

        public void BuildSummary()
        {
            StringBuilder sb = new StringBuilder();
            if (exception != null)
            {
                sb.Append("Error: ");
                sb.Append(exception.Message);
            }

            if (HasSolution)
            {
                if (solutions.Count == 1)
                {
                    sb.AppendFormat("Solution found in {0} moves", solutions[0].Steps.Length);    
                }
                else
                {
                    sb.AppendFormat("{0} solutions found", solutions.Count);    
                }
            }
            else
            {
                sb.Append("No solutions");
            }

            summary = sb.ToString();
        }

        private List<Solution> solutions;
        private ExitTypes exit;
        private SolverController.States controllerResult;
        private string summary = "Not set";
        private Exception exception;
    }
}
