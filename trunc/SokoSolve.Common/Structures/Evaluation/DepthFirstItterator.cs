using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    /// <summary>
    /// High-speed, optimised, depth-first (deepest node first) Eval Strategy iterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DepthFirstItterator<T> : BaseItterator<T>
    {
        public DepthFirstItterator(GetDepthDelegate getDepth) : base(getDepth)
        {
            evaluationList = new PriorityWorkQueue<INode<T>>(CompareDepth);
        }

        private int CompareDepth(INode<T> lhs, INode<T> rhs)
        {
            return GetDepth(lhs).CompareTo(GetDepth(rhs));
        }

        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns></returns>
        public override  INode<T> GetNext(out EvalStatus Status)
        {
            if (evaluationList.Count == 0)
            {
                exitStatus = EvalStatus.CompleteNoSolution;
                Status = exitStatus;
                return null;
            }

            INode<T> next = evaluationList.GetWorst();
            if (CheckExitConditions(next))
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                return null;
            }

            exitStatus = EvalStatus.InProgress;
            Status = exitStatus;
            return next;
        }

        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="NewEvalNode"></param>
        public override void Add(INode<T> NewEvalNode)
        {
            evaluationList.Add(NewEvalNode);
        }

        /// <summary>
        /// Return a copy of the evaluation list
        /// </summary>
        /// <returns></returns>
        public override  List<INode<T>> GetEvalList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        public override void Remove(INode<T> EvalNode)
        {
            evaluationList.Remove(EvalNode);
        }

        public override string ToString()
        {
            return string.Format("{0} EvalList:{1} LastStatus:{2} Itt:{3} Depth:{4}", 
                this.GetType().Name, evaluationList.Count, exitStatus, currentItteration, currentMaxDepth);
        }

        protected PriorityWorkQueue<INode<T>> evaluationList;
       
       
    }
}


