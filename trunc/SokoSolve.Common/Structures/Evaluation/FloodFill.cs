using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures.Evaluation;

namespace SokoSolve.Common.Structures
{
	
	
	public class FloodFillStrategy : IEvaluationStrategy<LocationNode>
	{
		private IBitmap initialState;
		private Bitmap currentState;
		private VectorInt startLocation;
		private List<TreeNode<LocationNode>> evaluationList;
		private Tree<LocationNode> searchTree;


		public FloodFillStrategy(IBitmap initialState, VectorInt startLocation)
		{
			this.initialState = initialState;
			this.currentState = new Bitmap(initialState.Size);
			this.startLocation = startLocation;
			evaluationList = new List<TreeNode<LocationNode>>();

			// Add the start location
			searchTree = new Tree<LocationNode>();
			TreeNode<LocationNode> startNode = searchTree.Root;
			startNode.Data = new LocationNode(startLocation);

			evaluationList.Add(startNode);
		}

		/// <summary>
		/// The result of the solution
		/// </summary>
		public Bitmap Result
		{
			get { return currentState; }
		}

		/// <summary>
		/// Get the shortest path from the start location to a destination location
		/// </summary>
		/// <param name="destination"></param>
		/// <returns>null - no path found</returns>
		public List<LocationNode> GetShortestPath(VectorInt destination)
		{
			TreeNode<LocationNode> destinationNode = searchTree.Root.Find(delegate(TreeNode<LocationNode> node) { return node.Data.Location == destination; }, int.MaxValue);
			if (destinationNode == null) return null;

			// Here root node is the start location
			List<TreeNode<LocationNode>> route = destinationNode.GetPathToRoot();

			route.Reverse();
			return Tree<LocationNode>.Convert(route);
		}

		#region IEvaluationStrategy<BitmapLocation> Members

		public bool IsSolution(INode<LocationNode> node)
		{
			// There is no solution for flood fill.
			return false;
		}

		public bool EvaluateState(INode<LocationNode> node)
		{
			node.Data.IsStateEvaluated = true;
			// Nothing to do, we only evaluate children
			return false;
		}

		public bool EvaluateChildren(INode<LocationNode> node)
		{
			TreeNode<LocationNode> treeNode = node as TreeNode<LocationNode>;
			if (treeNode == null) throw new NotSupportedException();

			// Up
			if (treeNode.Data.Location.Y > 0)
			{
				VectorInt pos = treeNode.Data.Location.Subtract(0, 1);
				AddChild(pos, treeNode);
			}

			// Down
			if (treeNode.Data.Location.Y != initialState.Size.Y-1)
			{
				VectorInt pos = treeNode.Data.Location.Add(0, 1);
				AddChild(pos, treeNode);
			}

			// Left
			if (treeNode.Data.Location.X > 0)
			{
				VectorInt pos = treeNode.Data.Location.Subtract(1, 0);
				AddChild(pos, treeNode);
			}

			// Right
			if (treeNode.Data.Location.X != initialState.Size.X - 1)
			{
				VectorInt pos = treeNode.Data.Location.Add(1, 0); // Up
				AddChild(pos, treeNode);
			}

			node.Data.IsChildrenEvaluated = true;

			return false;
		}


		public INode<LocationNode> GetNext()
		{
			while(true)
			{
				if (evaluationList.Count == 0) return null;

				// Depth first
				// Note (this is a very slow way of doing this)
				evaluationList.Sort(delegate(TreeNode<LocationNode> lhs, TreeNode<LocationNode> rhs) { return lhs.Depth.CompareTo(rhs.Depth); });

				TreeNode<LocationNode> first = evaluationList[0];
				if (!first.Data.IsStateEvaluated) return first;
				if (!first.Data.IsChildrenEvaluated) return first;

				evaluationList.RemoveAt(0);	
			}			
		}

		#endregion

		private void AddChild(VectorInt pos, TreeNode<LocationNode> treeNode)
		{
			if (!currentState[pos] && !initialState[pos.X, pos.Y])
			{
				TreeNode<LocationNode> kid = new TreeNode<LocationNode>(treeNode, new LocationNode(pos));
				treeNode.Children.Add(kid);
				evaluationList.Add(kid);
				currentState[pos] = true;
				kid.Data.IsStateEvaluated = true;
			}
		}

	}
}
