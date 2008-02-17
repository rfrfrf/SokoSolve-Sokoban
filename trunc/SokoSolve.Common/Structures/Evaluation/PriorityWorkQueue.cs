using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    public class PriorityWorkQueue<T> 
    {
        public PriorityWorkQueue(Comparison<T> comparison)
        {
            this.comparison = comparison;
            evaluationList = new LinkedList<T>();
        }

        public T GetBest()
        {
            if (evaluationList.Count == 0) return default(T);
            return evaluationList.First.Value;
        }

        public T GetWorst()
        {
            if (evaluationList.Count == 0) return default(T);
            return evaluationList.Last.Value;
        }

        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="NewEvalNode"></param>
        public virtual void Add(T NewEvalNode)
        {
            if (evaluationList.Count == 0)
            {
                evaluationList.AddFirst(NewEvalNode);
                return;
            }

            if (evaluationList.Contains(NewEvalNode)) return;

            LinkedListNode<T> current = evaluationList.First;
            while (current != null)
            {
                if (comparison(NewEvalNode, current.Value) < 0)
                {
                    evaluationList.AddBefore(current, NewEvalNode);
                    return;
                }
                current = current.Next;
            }

            evaluationList.AddLast(NewEvalNode);
        }

        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        public void Remove(T EvalNode)
        {
            evaluationList.Remove(EvalNode);
        }

        /// <summary>
        /// The number of items in the sortedlist/queue
        /// </summary>
        public int Count
        {
            get
            {
                return evaluationList.Count;
            }
        }

        protected LinkedList<T> evaluationList;
        private Comparison<T> comparison;
    }
}
