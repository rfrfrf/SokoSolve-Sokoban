using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
	public class Evaluator<T> : IEvaluator<T> where T : IManagedNodeData
	{
		public bool Evaluate(IEvaluationStrategy<T> strategy)
		{
			bool complete = false;
			while (!complete)
			{
				INode<T> current = strategy.GetNext();
				if (current == null) return false;

				if (!current.Data.IsStateEvaluated)
				{
					complete = strategy.EvaluateState(current);
				}
				else if (!current.Data.IsChildrenEvaluated)
				{
					complete = strategy.EvaluateChildren(current);
				}
			}
			return false;
		}
	}

}
