using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Custom evaluator (buildin og <see cref="BreadthFirstItterator{T}"/>) make a custom itterator 
    /// which can intergrate into the Solver implmentation with stats, weighting and multi-threading
    /// </summary>
    internal class SolverItterator : IEvaluationStrategyItterator<SolverNode>
    {
        #region ItteratorExit enum

        public enum ItteratorExit
        {
            None,
            Cancelled,
            MaxTimeExceeded,
            MaxDepthExceeded,
            MaxItteratorsExceeded,
            EvalListEmpty
        }

        #endregion

        private SolverController controller;
        private LinkedList<INode<SolverNode>> evalLinkedList;
        private List<INode<SolverNode>> evalList;
        private EvalStatus exitStatus;
        private ItteratorExit status;
        private bool useLinked = true;
        private bool locked = false;

        public SolverItterator(SolverController controller)
        {
            evalList = new List<INode<SolverNode>>(10000);
            evalLinkedList = new LinkedList<INode<SolverNode>>();

            useLinked = true;

            this.controller = controller;
        }

        public ItteratorExit Status
        {
            get { return status; }
            set { status = value; }
        }

        #region IEvaluationStrategyItterator<SolverNode> Members

        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns></returns>
        public INode<SolverNode> GetNext(out EvalStatus EvalStatus)
        {
            // Check Exit conditions

            // Stopped?
            if (controller.State != SolverController.States.Running)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                EvalStatus = exitStatus;
                status = ItteratorExit.Cancelled;
                controller.DebugReport.AppendTimeStamp("Exiting - Controller not running");
                return null;
            }

            // Max Time
            if ((int) controller.Stats.CurrentEvalSecs.ValueTotal >= controller.ExitConditions.MaxTimeSecs)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                EvalStatus = exitStatus;
                status = ItteratorExit.MaxTimeExceeded;
                controller.DebugReport.AppendTimeStamp("Exiting - Max Time exceeded");
                return null;
            }

            // Max Depth
            if ((int) controller.Stats.MaxDepth.ValueTotal >= controller.ExitConditions.MaxDepth)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                EvalStatus = exitStatus;
                controller.DebugReport.AppendTimeStamp("Exiting - Max Depth exceeded");
                status = ItteratorExit.MaxDepthExceeded;
                return null;
            }

            // Max itterations
            if (controller.Stats.EvaluationItterations.ValueTotal >= controller.ExitConditions.MaxItterations)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                EvalStatus = exitStatus;
                status = ItteratorExit.MaxItteratorsExceeded;
                controller.DebugReport.AppendTimeStamp("Exiting - Max Itterations exceeded");
                return null;
            }

            // INode<SolverNode> next = evalList[0];
            INode<SolverNode> next = null;
            if (useLinked)
            {
                if (evalLinkedList.First == null)
                {
                    exitStatus = EvalStatus.CompleteNoSolution;
                    EvalStatus = exitStatus;
                    status = ItteratorExit.EvalListEmpty;
                    controller.DebugReport.AppendTimeStamp(
                        "Exiting - Node worker list is empty. All possible moved exhausted?");
                    return null;
                }
                next = evalLinkedList.First.Value;
            }
            else
            {
                if (evalList.Count == 0)
                {
                    exitStatus = EvalStatus.CompleteNoSolution;
                    EvalStatus = exitStatus;
                    status = ItteratorExit.EvalListEmpty;
                    controller.DebugReport.AppendTimeStamp(
                        "Exiting - Node worker list is empty. All possible moved exhausted?");
                    return null;
                }
                next = evalList[0];
            }

            controller.Stats.EvaluationItterations.Increment();


            // Check max depth
            int currDepth = next.Data.TreeNode.Depth;
            if (currDepth > (int) controller.Stats.MaxDepth.ValueTotal)
                controller.Stats.MaxDepth.ValueTotal = currDepth;

            // Exit
            EvalStatus = exitStatus;
            return next;
        }


        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="NewEvalNode"></param>
        public void Add(INode<SolverNode> NewEvalNode)
        {

            while (locked) ;

                controller.Stats.AvgEvalList.Increment();

                if (useLinked)
                {
                    if (evalLinkedList.First == null)
                    {
                        // First
                        evalLinkedList.AddFirst(NewEvalNode);
                    }
                    else
                    {
                        LinkedListNode<INode<SolverNode>> current = evalLinkedList.First;
                        while (current != null && CompareNodes(NewEvalNode, current.Value) > 0)
                        {
                            current = current.Next;
                        }
                        if (current == null)
                        {
                            // Last
                            evalLinkedList.AddLast(NewEvalNode);
                        }
                        else
                        {
                            evalLinkedList.AddBefore(current, NewEvalNode);
                        }
                    }

                    // This implementation does not garentee sortedness, as the wieghting value may change after added
                    // As a work-around we should sort the list ever 100 (or some other number) of itterations
                    if (controller.Stats.AvgEvalList.ValueTotal%100 == 0)
                    {
                        INode<SolverNode>[] tmp = new INode<SolverNode>[evalLinkedList.Count];
                        evalLinkedList.CopyTo(tmp, 0);
                        Array.Sort(tmp, CompareNodes);
                        evalLinkedList = new LinkedList<INode<SolverNode>>(tmp);
                    }
                }
                else
                {
                    evalList.Add(NewEvalNode);
                    evalList.Sort(CompareNodes);
                }
            
        }


        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        public void Remove(INode<SolverNode> EvalNode)
        {
            controller.Stats.AvgEvalList.Decrement();

            if (useLinked)
            {
                evalLinkedList.Remove(EvalNode);
            }
            else
            {
                evalList.Remove(EvalNode);
            }
        }


        /// <summary>
        /// Return a copy of the evaluation list
        /// </summary>
        /// <returns>A copy of the nodes</returns>
        public List<INode<SolverNode>> GetEvalList()
        {
            try
            {
                locked = true;

                if (useLinked)
                {
                    List<INode<SolverNode>> tmp = new List<INode<SolverNode>>();
                    foreach (INode<SolverNode> node in evalLinkedList)
                    {
                        tmp.Add(node);
                    }
                    return tmp;
                }
                else
                {
                    return new List<INode<SolverNode>>(evalList);
                }
            }
            finally
            {
                locked = false;
            }
            
        }

        #endregion

        private static int CompareNodes(INode<SolverNode> lhs, INode<SolverNode> rhs)
        {
            return rhs.Data.Weighting.CompareTo(lhs.Data.Weighting);
        }
    }
}