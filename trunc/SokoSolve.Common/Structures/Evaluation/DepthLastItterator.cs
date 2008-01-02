using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    /// <summary>
    /// High-speed, optimised, depth-last (shallowest node first) Eval Strategy iterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DepthLastItterator<T> : DepthFirstItterator<T>
    {
        public DepthLastItterator(GetDepthDelegate getDepth) : base(getDepth)
        {
        }
        
        public DepthLastItterator() : base()
        {
        }

        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns></returns>
        public override INode<T> GetNext(out EvalStatus Status)
        {
            if (evaluationList.Count == 0)
            {
                exitStatus = EvalStatus.CompleteNoSolution;
                Status = exitStatus;
                return null;
            }
            if (currentMaxDepth > maxDepth)
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

            INode<T> next = evaluationList.First.Value;

            int nextDepth = GetDepth(next);
            if (nextDepth > currentMaxDepth) currentMaxDepth = nextDepth;

            Status = exitStatus;
            return next;
        }       
    }
}



