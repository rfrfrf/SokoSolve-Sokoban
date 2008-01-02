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
        /// Default Constructor
        /// </summary>
        public Evaluator()
        {
            stopOnSolution = false;
            solutions = new List<INode<T>>();
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
                    if (strategy.IsSolution(current))
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
			return EvalStatus.CompleteNoSolution;
		}

	    public bool HasSolution
	    {
            get { return solutions != null && solutions.Count > 0;  }
	    }

	    public List<INode<T>> Solutions
	    {
	        get { return solutions; }
	    }

	    public bool StopOnSolution
	    {
	        get { return stopOnSolution; }
	        set { stopOnSolution = value; }
	    }

        public IEvaluationStrategy<T> Strategy
        {
            get { return strategy; }
        }

        public override string ToString()
        {
            return string.Format("Evaluting. Solutions:{0} {1}", solutions.Count, strategy);
        }

        private bool stopOnSolution;
        private List<INode<T>> solutions;
        private IEvaluationStrategy<T> strategy;
	}

}
