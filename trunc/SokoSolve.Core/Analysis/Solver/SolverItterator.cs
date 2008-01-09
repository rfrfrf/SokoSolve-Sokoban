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
            evalLinkedList = new LinkedList<INode<SolverNode>>();

            useLinked = true;
          
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
                controller.DebugReport.AppendTimeStamp("Exiting - Controller not Enabled");
                return null;
            }

            // Max Time
            if ((int)controller.Stats.CurrentEvalSecs.ValueTotal >= controller.ExitConditions.MaxTimeSecs)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                controller.DebugReport.AppendTimeStamp("Exiting - Max Time exceeded");
                return null;
            }

            // Max Depth
            if ((int)controller.Stats.MaxDepth.ValueTotal >= controller.ExitConditions.MaxDepth)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
                controller.DebugReport.AppendTimeStamp("Exiting - Max Depth exceeded");
                return null;
            }

            // Max itterations
            if (controller.Stats.EvaluationItterations.ValueTotal >= controller.ExitConditions.MaxItterations)
            {
                exitStatus = EvalStatus.ExitIncomplete;
                Status = exitStatus;
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
                    Status = exitStatus;
                    controller.DebugReport.AppendTimeStamp("Exiting - Node worker list is empty. All possible moved exhausted?");
                    return null;
                }
                next = evalLinkedList.First.Value;
            }
            else
            {
                if (evalList.Count == 0)
                {
                    exitStatus = EvalStatus.CompleteNoSolution;
                    Status = exitStatus;
                    controller.DebugReport.AppendTimeStamp("Exiting - Node worker list is empty. All possible moved exhausted?");
                    return null;
                }
                next = evalList[0];
            }

            controller.Stats.EvaluationItterations.Increment();


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
                if (controller.Stats.AvgEvalList.ValueTotal % 100 == 0)
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

        private bool useLinked = true;

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
            return new List<INode<SolverNode>>(evalList);
        }


        private List<INode<SolverNode>> evalList;
        private LinkedList<INode<SolverNode>> evalLinkedList;
        private SolverController controller;
        private EvalStatus exitStatus;
    }
}
