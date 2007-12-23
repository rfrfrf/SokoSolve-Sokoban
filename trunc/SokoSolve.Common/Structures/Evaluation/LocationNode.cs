using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Common.Structures.Evaluation
{
	public class LocationNode : IEvaluationNode
	{
		private VectorInt location;
		private string nodeID;
		private bool isStateEvaluated;
		private bool isChildrenEvaluated;

		public LocationNode(VectorInt location)
		{
			this.location = location;
		}

		public VectorInt Location
		{
			get { return location; }
			set { location = value; }
		}

		#region IManagedNodeData Members

		public string NodeID
		{
			get { return nodeID; }
		}

		public bool IsStateEvaluated
		{
			get { return isStateEvaluated; }
			set { isStateEvaluated = value; }
		}

		public bool IsChildrenEvaluated
		{
			get { return isChildrenEvaluated; }
			set { isChildrenEvaluated = value; }
		}

		#endregion
	}

}
