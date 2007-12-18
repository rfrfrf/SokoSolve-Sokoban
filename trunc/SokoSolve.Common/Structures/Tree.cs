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
		private TreeNode<T> top;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Tree()
		{
			this.top = new TreeNode<T>(null);
			this.top.Tree = this;
		}

		
		/// <summary>
		/// The top node (essentially this represents the entire tree)
		/// </summary>
		public TreeNode<T> Top
		{
			get { return top; }
		}

		/// <summary>
		/// A collection of leaf nodes (nodes without children)
		/// </summary>
		public ICollection<TreeNode<T>> Leaves
		{
			get
			{
				return Top.FindAll(delegate(TreeNode<T> item) { return item.IsLeaf; }, int.MaxValue);
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


		#region ITree<T> Members

		ITreeNode<T> ITree<T>.Top
		{
			get { return top; }
		}

		ICollection<ITreeNode<T>> ITree<T>.Leaves
		{
			get { return (ICollection<ITreeNode<T>>)Leaves; }
		}

		#endregion
	}
}
