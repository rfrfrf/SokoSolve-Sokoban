using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Common.Structures
{
    /// <summary>
    /// Link two nodes together
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public interface INodeLink<T>
	{
		INode<T> A { get; }
		INode<T> B { get; }
	}

    /// <summary>
    /// To be used with the <see cref="IEvaluationStrategy{T}"/>, and allows the evaluation state to be 
    /// encapsulated seperately from node's payload data.
    /// </summary>
	public interface IEvaluationNode
	{
        /// <summary>
        /// Human-readable ID.
        /// </summary>
		string NodeID { get; }

        /// <summary>
        /// Has the initial evaluation been done
        /// </summary>
		bool IsStateEvaluated { get; set; }

        /// <summary>
        /// Have the children been generated
        /// </summary>
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
		ITreeNode<T> Root { get; }

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

    /// <summary>
    /// Enumeration of evaluation status
    /// </summary>
    public enum EvalStatus
    {
        NotStarted,
        InProgress,
        CompleteSolution,
        CompleteNoSolution,
        ExitIncomplete,
        Error
    }

	public interface IEvaluator<T>
	{
        EvalStatus Evaluate(IEvaluationStrategy<T> strategy);
	}

    /// <summary>
    /// Encapsulate with the 'Strategy' design pattern the algorythm need to evaluate a graph of interconnected nodes.
    /// Perfect for complex solution searchs.
    /// </summary>
    /// <typeparam name="T">Domain Node</typeparam>
	public interface IEvaluationStrategy<T>
	{
        /// <summary>
        /// Set the initial conditions. This is not strictly nessesary, but makes implementation easier.
        /// </summary>
	    void Init();

        /// <summary>
        /// Is the node a solution
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
		bool IsSolution(INode<T> node);

        /// <summary>
        /// Evaluate the current node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        EvalStatus EvaluateState(INode<T> node);

        /// <summary>
        /// Evaluate all child nodes. Based on a <paramref name="node"/> create all children. Be s
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
		void EvaluateChildren(INode<T> node);

        /// <summary>
        /// Get the next node for evaluation
        /// </summary>
        /// <returns></returns>
		INode<T> GetNext(out EvalStatus Status);
	}


    public interface IEvaluationStrategyItterator<T>
    {
        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns>null means exit evaluation</returns>
        INode<T> GetNext(out EvalStatus Status);

        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="EvalNode"></param>
        void Add(INode<T> EvalNode);

        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        void Remove(INode<T> EvalNode);
    }

    /// <summary>
    /// A stripped down boolean map interface
    /// </summary>
	public interface IBitmap
	{
		bool this[int X, int Y] { get; set; }
        SizeInt Size { get; set; }
	}

}
