using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Analysis.Solver
{
    public class SolverNodeEventArgs : EventArgs
    {
        public SolverNodeEventArgs(TreeNode<SolverNode> node)
        {
            this.node = node;
        }

        public TreeNode<SolverNode> Node
        {
            get { return node; }
            set { node = value; }
        }

        private TreeNode<SolverNode> node;
    }


}
