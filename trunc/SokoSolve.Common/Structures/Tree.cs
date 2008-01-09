using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures
{

	/// <summary>
	/// Encapsulate a Tree structure for parent / children
	/// </summary>
	/// <typeparam name="T">Node's data</typeparam>
	public class Tree<T> : ITree<T>
	{
		private TreeNode<T> root;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Tree()
		{
			this.root = new TreeNode<T>(null);
			this.root.Tree = this;
		}

		
		/// <summary>
		/// The top node (essentially this represents the entire tree)
		/// </summary>
		public TreeNode<T> Root
		{
			get { return root; }
		}

		/// <summary>
		/// A collection of leaf nodes (nodes without children)
		/// </summary>
		public ICollection<TreeNode<T>> Leaves
		{
			get
			{
				return Root.FindAll(delegate(TreeNode<T> item) { return item.IsLeaf; }, int.MaxValue);
			}
		}

        /// <summary>
        /// A collection of ALL nodes
        /// </summary>
        public List<T> Nodes
        {
            get
            {
                return Root.ToList();
            }
        }

		/// <summary>
		/// Convert a list of nodes to a list of data payload T
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static List<T> Convert(ICollection<TreeNode<T>> source)
		{
			List<T> results = new List<T>();
			foreach(TreeNode<T> node in source)
			{
				results.Add(node.Data);
			}
			return results;
		}

        /// <summary>
        /// Move a node to another parent
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="NewParent">null means make it the new root node</param>
        public void Move(TreeNode<T> Node, TreeNode<T> NewParent)
        {
            // Remove it from tree
            if (!Node.IsRoot)
            {
                Node.Parent.RemoveChild(Node);
            }
            else
            {
                throw new NotSupportedException("Cannot move root node to sub-node");
            }

            // Add it to new parent
            if (NewParent == null)
            {
                TreeNode<T> oldRoot = root;
                root = Node;
                Node.Add(oldRoot);
            }
            else
            {
                NewParent.Add(Node);    
            }
        }


		#region ITree<T> Members

		ITreeNode<T> ITree<T>.Root
		{
			get { return root; }
		}

		ICollection<ITreeNode<T>> ITree<T>.Leaves
		{
			get { return (ICollection<ITreeNode<T>>)Leaves; }
		}

		#endregion

        public override string ToString()
        {
            return string.Format("Tree Size:{0}", Leaves.Count);
        }
	}
}
