using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    /// <summary>
    /// High-speed, optimised, depth-first (deepest node first) Eval Strategy iterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DepthFirstItterator<T> : IEvaluationStrategyItterator<T>
    {
        public DepthFirstItterator(GetDepthDelegate getDepth)
        {
            GetDepth = getDepth;
            evaluationList = new LinkedList<INode<T>>();
            maxDepth = 100;
            maxItterations = 1000;
            currentMaxDepth = 0;
            currentItteration = 0;
        }

        public DepthFirstItterator() : this(DefaultGetLocationNodeDepth)
        {
        }

        /// <summary>
        /// Limit the maximum depth. Default is 100
        /// </summary>
        public int MaxDepth
        {
            get { return maxDepth; }
            set { maxDepth = value; }
        }

        /// <summary>
        /// Limit the number of search itterations. Default is 1000
        /// </summary>
        public int MaxItterations
        {
            get { return maxItterations; }
            set { maxItterations = value; }
        }

        /// <summary>
        /// Exit Status. <see cref="GetNext"/>
        /// </summary>
        public EvalStatus ExitStatus
        {
            get { return exitStatus; }
        }

        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns></returns>
        public virtual INode<T> GetNext(out EvalStatus Status)
        {
            if (evaluationList.Count == 0)
            {
                exitStatus = EvalStatus.CompleteNoSolution;
                Status = exitStatus;
                return null;
            }
            if (currentMaxDepth++ > maxDepth)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                return null;
            }
            if (currentItteration++ > maxItterations)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                return null;
            }

            INode<T> next = evaluationList.Last.Value;

            int nextDepth = GetDepth(next);
            if (nextDepth > currentMaxDepth) currentMaxDepth = nextDepth;

            Status = exitStatus;
            return next;
        }

        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="EvalNode"></param>
        public void Add(INode<T> EvalNode)
        {
            if (evaluationList.Count == 0)
            {
                evaluationList.AddFirst(EvalNode);
                return;
            }

            if (evaluationList.Contains(EvalNode)) return;

            LinkedListNode<INode<T>> current = evaluationList.First;
            while (current != null)
            {
                if (GetDepth(EvalNode) < GetDepth(current.Value))
                {
                    evaluationList.AddBefore(current, EvalNode);
                    return;
                }

                current = current.Next;
            }

            evaluationList.AddLast(EvalNode);
        }

        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        public void Remove(INode<T> EvalNode)
        {
            evaluationList.Remove(EvalNode);
        }

        private static int DefaultGetLocationNodeDepth(INode<T> node)
        {
            TreeNode<T> upcast = node as TreeNode<T>;
            if (upcast == null) return 0;
            return upcast.Depth;
        }

        public delegate int GetDepthDelegate(INode<T> EvalNode);

        public GetDepthDelegate GetDepth;

        public override string ToString()
        {
            return string.Format("{0} EvalList:{1} LastStatus:{2} Itt:{3} Depth:{4}", 
                this.GetType().Name, evaluationList.Count, exitStatus, currentItteration, currentMaxDepth);
        }

        protected LinkedList<INode<T>> evaluationList;
        protected int maxDepth;
        protected int maxItterations;
        protected int currentMaxDepth;
        protected int currentItteration;
        protected EvalStatus exitStatus;
       
    }
}


