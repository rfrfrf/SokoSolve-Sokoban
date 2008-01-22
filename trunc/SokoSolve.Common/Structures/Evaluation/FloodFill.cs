using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures.Evaluation;

namespace SokoSolve.Common.Structures
{
    /// <summary>
    /// Do a location based (Up, Down, Left, Right) classic flood fill within a constraint map.
    /// </summary>
	public class FloodFillStrategy : IEvaluationStrategy<LocationNode>
	{
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="startLocation"></param>
		public FloodFillStrategy(IBitmap initialState, VectorInt startLocation)
		{
            if (startLocation.IsNull) throw new ArgumentException("Cannot be null", "startLocation");
			this.initialState = initialState;
			this.currentState = new Bitmap(initialState.Size);
			this.startLocation = startLocation;
            workList = new DepthLastItterator<LocationNode>(GetLocationNodeDepth);
            workList.MaxDepth = 1000;
            workList.MaxItterations = 100000;

            // Mark the start location as true
            //currentState[startLocation] = true;

			// Add the start location
			searchTree = new Tree<LocationNode>();
			TreeNode<LocationNode> startNode = searchTree.Root;
			startNode.Data = new LocationNode(startLocation);

			workList.Add(startNode);
		}

        private static int GetLocationNodeDepth(INode<LocationNode> node)
        {
            TreeNode<LocationNode> upcast = node as TreeNode<LocationNode>;
            if (upcast == null) return 0;
            return upcast.Depth;
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

        #region IEvaluationStrategy<LocationNode> Members

        /// <summary>
        /// Set the initial conditions. This is not strictly nessesary, but makes implementation easier.
        /// </summary>
        public void Init()
        {
            // Nothing to do
        }

        #endregion

        public bool IsSolution(INode<LocationNode> node)
		{
			// There is no solution for flood fill.
			return false;
		}

		public EvalStatus EvaluateState(INode<LocationNode> node)
		{
            
			node.Data.IsStateEvaluated = true;
			// Nothing to do, we only evaluate children
            return EvalStatus.InProgress;
		}

		public void EvaluateChildren(INode<LocationNode> node)
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

            // Remove this node from the worklist
            workList.Remove(node);
		}


		public INode<LocationNode> GetNext(out EvalStatus Status)
		{
            // Delegate this work to the DepthFirstIterator
            return workList.GetNext(out Status);	
		}

		#endregion

		private void AddChild(VectorInt pos, TreeNode<LocationNode> treeNode)
		{
			if (!currentState[pos] && !initialState[pos.X, pos.Y])
			{
                TreeNode<LocationNode> kid = treeNode.Add(new LocationNode(pos));
				workList.Add(kid);
				currentState[pos] = true;
				kid.Data.IsStateEvaluated = true;
			}
		}

        private IBitmap initialState;
        private Bitmap currentState;
        private VectorInt startLocation;
        private DepthLastItterator<LocationNode> workList;
        private Tree<LocationNode> searchTree;
	}
}
