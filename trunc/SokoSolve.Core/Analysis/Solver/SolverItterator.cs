using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;

namespace SokoSolve.Core.Analysis.Solver
{

    /// <summary>
    /// Custom evaluator (buildin og <see cref="DepthLastItterator{T}"/>) make a custom itterator 
    /// which can intergrate into the Solver implmentation with stats, weighting and multi-threading
    /// </summary>
    class SolverItterator :  DepthLastItterator<SolverNode>
    {
        public SolverItterator(SolverController controller)
        {
            base.GetDepth = GetSolverNodeDepth;
            this.controller = controller;
        }

        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns></returns>
        public override INode<SolverNode> GetNext(out EvalStatus Status)
        {
            // Get the basic depth-last node
            INode<SolverNode> depthNext =  base.GetNext(out Status);

            return depthNext;
        }


        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="NewEvalNode"></param>
        public override void Add(INode<SolverNode> NewEvalNode)
        {
            if (evaluationList.Count == 0)
            {
                evaluationList.AddFirst(NewEvalNode);
                return;
            }

            if (evaluationList.Contains(NewEvalNode)) return;

            LinkedListNode<INode<SolverNode>> current = evaluationList.First;

            int depthNewEvalNode = GetDepth(NewEvalNode);
            while (current != null)
            {
                int depthCurrentNode = GetDepth(current.Value);

                if (depthNewEvalNode == depthCurrentNode)
                {
                    if (NewEvalNode.Data.Weighting > current.Value.Data.Weighting)
                    {
                        evaluationList.AddBefore(current, NewEvalNode);
                        return;
                    }
                    return;
                }

                if (depthNewEvalNode < depthCurrentNode)
                {
                    evaluationList.AddBefore(current, NewEvalNode);
                    return;
                }

                current = current.Next;
            }

            evaluationList.AddLast(NewEvalNode);
        }

        private int GetSolverNodeDepth(INode<SolverNode> node)
        {
            return node.Data.TreeNode.Depth;
        }

        private SolverController controller;
    }
}
