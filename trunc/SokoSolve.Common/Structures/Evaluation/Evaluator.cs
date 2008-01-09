using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    /// <summary>
    /// Simple Evaluation implementation.
    /// Itterates through all nodes, storing all solutions
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class Evaluator<T> : IEvaluator<T> where T : IEvaluationNode
	{
        /// <summary>
        /// Constructor
        /// </summary>
        public Evaluator(bool StopOnSolution)
        {
            stopOnSolution = StopOnSolution;
            solutions = new List<INode<T>>();
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Evaluator() : this(false)
        {
            
        }

        /// <summary>
        /// Evaluate using a strategy
        /// </summary>
        /// <param name="evalStrategy"></param>
        /// <returns></returns>
		public virtual EvalStatus Evaluate(IEvaluationStrategy<T> evalStrategy)
		{
            // Initialise
            strategy = evalStrategy;
            strategy.Init();

			bool complete = false;
			while (!complete)
			{
			    EvalStatus nextStatus;
				INode<T> current = strategy.GetNext(out nextStatus);
                if (current == null) return nextStatus;

                // Try to Evaluated first
				if (!current.Data.IsStateEvaluated)
				{
                    // Evaluate
					complete = (strategy.EvaluateState(current) == EvalStatus.CompleteSolution);

                    // Check Solution
                    if (complete || strategy.IsSolution(current))
                    {                        
                        solutions.Add(current);
                        if (stopOnSolution) return EvalStatus.CompleteSolution;
                    }
				}
				else if (!current.Data.IsChildrenEvaluated)
				{
                    // Eval all children
					strategy.EvaluateChildren(current);
				}
                else
				{
				    // Warning, why is this in the eval list if it is fully evaluated?
                    throw new InvalidOperationException("This node has already been processed");
				}
			}
            if (solutions.Count > 0) return EvalStatus.CompleteSolution;
			return EvalStatus.CompleteNoSolution;
		}

        /// <summary>
        /// Has a solution been found
        /// </summary>
	    public bool HasSolution
	    {
            get { return solutions != null && solutions.Count > 0;  }
	    }

        /// <summary>
        /// List of all solutions found
        /// </summary>
	    public List<INode<T>> Solutions
	    {
	        get { return solutions; }
	    }

        /// <summary>
        /// Should the evaluator break/exit when a solution is found
        /// </summary>
	    public bool StopOnSolution
	    {
	        get { return stopOnSolution; }
	        set { stopOnSolution = value; }
	    }

        /// <summary>
        /// Strategy used
        /// </summary>
        public IEvaluationStrategy<T> Strategy
        {
            get { return strategy; }
        }

        /// <summary>
        /// Simple debug-helper string override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Evaluting. Solutions:{0} {1}", solutions.Count, strategy);
        }

        private bool stopOnSolution;
        private List<INode<T>> solutions;
        private IEvaluationStrategy<T> strategy;
	}

}
