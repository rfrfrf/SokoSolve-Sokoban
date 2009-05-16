using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
        private ReaderWriterLock readWriteLock;
        private IDDFSItterator<SolverNode> innerItterator;
        private ItteratorExit status;
        private EvalStatus exitStatus;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="controller"></param>
        public SolverItterator(SolverController controller)
        {
            this.controller = controller;
            innerItterator = new IDDFSItterator<SolverNode>(GetSolverNodeDepth, CompareNodes);
            innerItterator.ExitConditions = controller.ExitConditions;
            innerItterator.TimerEnabled = true;
        }

        public ItteratorExitConditions ExitConditions
        {
            get { return innerItterator.ExitConditions;  }
            set { innerItterator.ExitConditions = value; }
        }

        /// <summary>
        /// Return a SolverNodes depth
        /// </summary>
        /// <param name="EvalNode"></param>
        /// <returns></returns>
        private int GetSolverNodeDepth(INode<SolverNode> EvalNode)
        {
            return EvalNode.Data.TreeNode.Depth;
        }

        /// <summary>
        /// Itterator Exit Status (additional to EvalStatus)
        /// </summary>
        public ItteratorExit Status
        {
            get { return status; }
            set { status = value; }
        }

        #region IEvaluationStrategyItterator<SolverNode> Members

        /// <summary>
        /// Start the itterator (this will also start the inner clock)
        /// </summary>
        public void Init()
        {
            innerItterator.Init();
        }

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

            // INode<SolverNode> next = evalList[0];
            INode<SolverNode> next = innerItterator.GetNext(out exitStatus);
            if (next == null)
            {
                EvalStatus = exitStatus;
                return null;
            }

            // Updates stats
            controller.Stats.EvaluationItterations.Increment();

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
            controller.Stats.AvgEvalList.Increment();

            //readWriteLock.AcquireWriterLock(1000);
            innerItterator.Add(NewEvalNode);
            //readWriteLock.ReleaseWriterLock();    
        }


        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        public void Remove(INode<SolverNode> EvalNode)
        {
            controller.Stats.AvgEvalList.Decrement();

            //readWriteLock.AcquireWriterLock(1000);
            innerItterator.Remove(EvalNode);
            //readWriteLock.ReleaseWriterLock();    
        }


        /// <summary>
        /// Return a copy of the evaluation list
        /// </summary>
        /// <returns>A copy of the nodes</returns>
        public List<INode<SolverNode>> GetEvalList()
        {
            return null;
        }

      

        #endregion

        private static int CompareNodes(INode<SolverNode> lhs, INode<SolverNode> rhs)
        {
            return rhs.Data.Weighting.CompareTo(lhs.Data.Weighting);
        }
    }
}