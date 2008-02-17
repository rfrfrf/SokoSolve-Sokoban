using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    /// <summary>
    /// Provide an Informed Itterative Deepening Depth-First Search (IDDFS) Itterator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IDDFSItterator<T> : BaseItterator<T>
    {
        public IDDFSItterator(GetDepthDelegate getDepth, Comparison<INode<T>> priorityComparison): base(getDepth)
        {
            this.priorityComparison = priorityComparison;
            segmentedWorkList = new List<PriorityWorkQueue<INode<T>>>(exitConditions.MaxDepth);

            // Add a queue for each depth
            for(int cc=0; cc<exitConditions.MaxDepth+1; cc++)
            {
                segmentedWorkList.Add(new PriorityWorkQueue<INode<T>>(priorityComparison)); 
            }

            // Init
            currentStartDepth = 0;
            currentDepth = 0;
            maxDepth = 250;
        }

        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns>null means exit evaluation</returns>
        public override INode<T> GetNext(out EvalStatus Status)
        {
            INode<T> next = segmentedWorkList[currentDepth].GetBest();
            if (next == null)
            {
                // Move to next
                if (currentDepth == currentStartDepth)
                {
                    currentStartDepth++;
                    if (currentStartDepth >= exitConditions.MaxDepth)
                    {
                        Status = EvalStatus.ExitIncomplete;
                        exitStatus = EvalStatus.ExitIncomplete;
                        return null;
                    }
                    currentDepth = currentStartDepth;
                    return GetNext(out Status);
                }
                else
                {
                    if (currentDepth < currentStartDepth+maxDepth)
                    {
                        currentDepth++;
                        return GetNext(out Status);
                    }
                    else
                    {
                        // Restart
                        currentDepth = currentStartDepth;
                        return GetNext(out Status);
                    }
                }
            }
            
            // Check Exit Conditions
            if (CheckExitConditions(next, float.MinValue))
            {
                Status = EvalStatus.ExitIncomplete;
                exitStatus = EvalStatus.ExitIncomplete;
                return null;
            }

            // Next node
            Status = EvalStatus.InProgress;
            exitStatus = EvalStatus.InProgress;
            return next;
        }

        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="NewEvalNode"></param>
        public override void Add(INode<T> NewEvalNode)
        {
            int depthNewEvalNode = GetDepth(NewEvalNode);
            if (depthNewEvalNode > exitConditions.MaxDepth) return;

            segmentedWorkList[depthNewEvalNode].Add(NewEvalNode);
        }

        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        public override void Remove(INode<T> EvalNode)
        {
            int depthNewEvalNode = GetDepth(EvalNode);
            if (depthNewEvalNode > exitConditions.MaxDepth) return;

            segmentedWorkList[depthNewEvalNode].Remove(EvalNode);

            currentDepth++;
            if (currentDepth >= currentStartDepth + maxDepth) currentDepth = currentStartDepth;
        }

        /// <summary>
        /// Return a copy of the evaluation list
        /// </summary>
        /// <returns>A copy of the nodes</returns>
        public override List<INode<T>> GetEvalList()
        {
            throw new NotImplementedException();
        }

        private int currentStartDepth;
        private int currentDepth;
        private int maxDepth;
        private List<PriorityWorkQueue<INode<T>>> segmentedWorkList;
        private Comparison<INode<T>> priorityComparison;
        
    }
}
