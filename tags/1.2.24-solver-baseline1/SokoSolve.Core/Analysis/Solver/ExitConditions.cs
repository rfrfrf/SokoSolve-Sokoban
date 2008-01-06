using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// When should the solver exit
    /// </summary>
    public class ExitConditions
    {
        public int MaxTimeSecs
        {
            get { return maxTimeSecs; }
            set { maxTimeSecs = value; }
        }

        public int MaxItterations
        {
            get { return maxItterations; }
            set { maxItterations = value; }
        }

        public int MaxDepth
        {
            get { return maxDepth; }
            set { maxDepth = value; }
        }


        public int MaxNodes
        {
            get { return maxNodes; }
            set { maxNodes = value; }
        }

        public bool StopOnSolution
        {
            get { return stopOnSolution; }
            set { stopOnSolution = value; }
        }


        private int maxTimeSecs = 600;
        private int maxItterations = 300000;
        private int maxDepth = 1000;
        private int maxNodes = 100000;
        private bool stopOnSolution = true;
    }
}
