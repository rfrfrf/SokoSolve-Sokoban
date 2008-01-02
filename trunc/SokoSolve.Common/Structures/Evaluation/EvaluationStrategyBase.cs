using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    /// <summary>
    /// Implement a basic (tree-based) evaluation strategy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EvaluationStrategyBase<T> : IEvaluationStrategy<T> where T : IEvaluationNode
    {
        /// <summary>
        /// Strong Constructor
        /// </summary>
        /// <param name="workList"></param>
        protected EvaluationStrategyBase(IEvaluationStrategyItterator<T> workList)
        {
            this.workList = workList;
            this.evaluation = new Tree<T>();
            nextID = 0;
        }

        /// <summary>
        /// Use a depth-last itterator by default.
        /// </summary>
        protected EvaluationStrategyBase() : this (new DepthLastItterator<T>())
        {
            
        }

        /// <summary>
        /// Initialise the start conditions for the search. Prepare the root search node
        /// </summary>
        /// <returns>Return the root search node for init and eval</returns>
        public abstract T InitStartConditions();

        #region IEvaluationStrategy<T> Members

        /// <summary>
        /// Set the initial conditions
        /// </summary>
        public void Init()
        {
            evaluation.Root.Data = InitStartConditions();

            workList.Add(evaluation.Root);
        }

        /// <summary>
        /// Is the node a solution
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public abstract bool IsSolution(INode<T> node);

        /// <summary>
        /// Evaluate the current node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public abstract EvalStatus EvaluateState(INode<T> node);

        /// <summary>
        /// Evaluate all child nodes
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public abstract void EvaluateChildren(INode<T> node);

        /// <summary>
        /// Get the next node for evaluation. <see cref="IEvaluationStrategyItterator<T>"/>
        /// </summary>
        /// <returns></returns>
        public INode<T> GetNext(out EvalStatus Status)
        {
            return workList.GetNext(out Status);
        }

        #endregion

        /// <summary>
        /// Add a new search node to the evaluation, also add it to the working evaluation list
        /// </summary>
        /// <param name="CurrentNode"></param>
        /// <param name="NewChild"></param>
        protected void AddNodeForEval(INode<T> CurrentNode, T NewChild)
        {
            if (NewChild.NodeID == null) NewChild.NodeID = GetNextNodeID();

            // Add it to the tree
            TreeNode<T> currentTreeNode = CurrentNode as TreeNode<T>;
            if (currentTreeNode == null) throw new InvalidCastException("Must be TreeNode<T>");
            TreeNode<T> newChildNode = currentTreeNode.Add(NewChild);


            // Add it to the eval list
            workList.Add(newChildNode);
        }

        protected void MarkEvalCompelete(INode<T> CurrentNode)
        {
            workList.Remove(CurrentNode);
        }

        /// <summary>
        /// Get the next ID in sequence
        /// </summary>
        /// <returns></returns>
        protected string GetNextNodeID()
        {
            nextID++;
            return nextID.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.GetType().Name, evaluation, workList);
        }

        private int nextID;
        protected Tree<T> evaluation;
        protected IEvaluationStrategyItterator<T> workList;
    }
}
