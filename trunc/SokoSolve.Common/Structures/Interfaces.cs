using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Common.Structures
{
	public interface INodeLink<T>
	{
		T A { get; }
		T B { get; }
	}

	public interface IManagedNodeData
	{
		string NodeID { get; }
		bool IsStateEvaluated { get; set; }
		bool IsChildrenEvaluated { get; set; }
	}

	/// <summary>
	/// The most simple network node
	/// </summary>
	/// <typeparam name="T">data payload</typeparam>
	public interface INode<T> 
	{
		/// <summary>
		/// Data Payload
		/// </summary>
		T Data { get; set; }

		/// <summary>
		/// Links (undirected from/to this node)
		/// </summary>
		ICollection<INode<T>> LinkNodes { get; }

		/// <summary>
		/// The network/graph to which this node belongs
		/// </summary>
		IGraph<T> Graph { get; }
	}

	/// <summary>
	/// A basic directed graph
	/// </summary>
	/// <typeparam name="T">Data payload</typeparam>
	public interface IDirectedNode<T> 
	{
		/// <summary>
		/// Data Payload
		/// </summary>
		T Data { get; set; }

		/// <summary>
		/// Outward (from this to another) nodes
		/// </summary>
		ICollection<IDirectedNode<T>> OutwardNodes	{ get; }

		/// <summary>
		/// Inward nodes (from another to this) 
		/// </summary>
		ICollection<IDirectedNode<T>> InwardNodes	{ get; }

		/// <summary>
		/// The directed graph to which this belongs
		/// </summary>
		IDirectedGraph<T> Graph { get; }
	}

	

	public interface IGraph<T>
	{
		ICollection<INode<T>> Nodes { get; }
	}

	public interface IDirectedGraph<T> 
	{
		ICollection<INode<T>> StartNodes { get; }
		ICollection<INode<T>> EndNodes { get; }
		IDirectedGraph<T> DirectedGraph { get; }
	}

	/// <summary>
	/// Encapsulate a Tree structure for parent / children
	/// </summary>
	/// <typeparam name="T">Node's data</typeparam>
	public interface ITree<T> 
	{
		/// <summary>
		/// The top node (essentially this represents the entire tree)
		/// </summary>
		ITreeNode<T> Top { get; }

		/// <summary>
		/// A collection of leaf nodes (nodes without children)
		/// </summary>
		ICollection<ITreeNode<T>> Leaves { get; }
	}

	/// <summary>
	/// Member node for a Tree
	/// </summary>
	/// <typeparam name="T">Node's data</typeparam>
	public interface ITreeNode<T>
	{
		/// <summary>
		/// Data payload
		/// </summary>
		T Data { get; set; }

		/// <summary>
		/// Parent Node
		/// </summary>
		/// <remarks>Have added 'Node' suffix to this name so that the implmenation can implement a type-strong self-referrence</remarks>
		ITreeNode<T> ParentNode { get; }

		/// <summary>
		/// List of children nodes
		/// </summary>
		ICollection<ITreeNode<T>> ChildrenNodes { get; }

		/// <summary>
		/// Tree to which this node belongs
		/// </summary>
		ITree<T> Tree { get; }
	}

	public interface IPath<T>
	{
		ICollection<INodeLink<T>> Links { get; }
		ICollection<INode<T>> Nodes { get; }
	}

	public interface IEvaluator<T>
	{
		bool Evaluate(IEvaluationStrategy<T> strategy );
	}

	public interface IEvaluationStrategy<T>
	{
		bool IsSolution(INode<T> node);
		bool EvaluateState(INode<T> node);
		bool EvaluateChildren(INode<T> node);
		INode<T> GetNext();
	}

	public interface IBitmap
	{
		bool this[int X, int Y] { get; set; }
        SizeInt Size { get; set; }
	}

}
