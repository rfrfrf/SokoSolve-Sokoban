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
    class SolverItterator : IEvaluationStrategyItterator<SolverNode>
    {
        public SolverItterator(SolverController controller) 
        {
            evalList = new List<INode<SolverNode>>(10000);
          
            this.controller = controller;
        }

        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns></returns>
        public INode<SolverNode> GetNext(out EvalStatus Status)
        {
            // Check Exit conditions

            // Stopped?
            if (!controller.IsEnabled)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                return null;
            }

            // Max Time
            if ((int)controller.Stats.CurrentEvalSecs.ValueTotal >= controller.ExitConditions.MaxTimeSecs)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                return null;
            }

            // Max Depth
            if ((int)controller.Stats.MaxDepth.ValueTotal >= controller.ExitConditions.MaxDepth)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                return null;
            }

            // Max itterations
            if (controller.Stats.EvaluationItterations.ValueTotal >= controller.ExitConditions.MaxItterations)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                return null;
            }

            if (evalList.Count == 0)
            {
                exitStatus = EvalStatus.CompleteNoSolution;
                Status = exitStatus;
                return null;
            }

            controller.Stats.EvaluationItterations.Increment();
            INode<SolverNode> next = evalList[0];

            // Check max depth
            int currDepth = next.Data.TreeNode.Depth;
            if (currDepth > (int)controller.Stats.MaxDepth.ValueTotal) controller.Stats.MaxDepth.ValueTotal = currDepth;

            // Exit
            Status = exitStatus;
            return next;
        }



        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="NewEvalNode"></param>
        public void Add(INode<SolverNode> NewEvalNode)
        {
            controller.Stats.AvgEvalList.Increment();

           evalList.Add(NewEvalNode);

           evalList.Sort(CompareNodes);
        }

        static int CompareNodes(INode<SolverNode> lhs, INode<SolverNode> rhs)
        {
            return rhs.Data.Weighting.CompareTo(lhs.Data.Weighting);
        }


        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        public void Remove(INode<SolverNode> EvalNode)
        {
            controller.Stats.AvgEvalList.Decrement();

            evalList.Remove(EvalNode);
        }


        /// <summary>
        /// Return a copy of the evaluation list
        /// </summary>
        /// <returns>A copy of the nodes</returns>
        public List<INode<SolverNode>> GetEvalList()
        {
            return new List<INode<SolverNode>>(evalList);
        }


        private List<INode<SolverNode>> evalList;
        private SolverController controller;
        private EvalStatus exitStatus;
    }
}
