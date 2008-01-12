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

        private List<Solution> solutions;
        private ExitTypes exit;
        private string summary;
    }
}
