using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common;

namespace SokoSolve.Common.Structures
{

	/// <summary>
	/// Allow the data payload to have a referrence with its structure
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ITreeNodeBackReference<T>
	{
		/// <summary>
		/// Allow the data payload to have a referrence with its structure
		/// </summary>
		TreeNode<T> TreeNode { get; set; }
	}

	/// <summary>
	/// Member node for a Tree
	/// </summary>
	/// <typeparam name="T">Node's data</typeparam>
	public class TreeNode<T> : ITreeNode<T>, IDirectedNode<T>, INode<T>
	{
		private TreeNode<T> parent;
		private ManagedCollection<TreeNode<T>> children;
		private T data;
		private Tree<T> tree;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="parent"></param>
		public TreeNode(TreeNode<T> parent)
		{
			this.parent = parent;
			if (parent != null) this.tree = parent.tree;
			this.children = new ManagedCollection<TreeNode<T>>(ChildrenNotifications);
		}

		/// <summary>
		/// Strong Constructor
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="data"></param>
		public TreeNode(TreeNode<T> parent, T data)
		{
			this.parent = parent;
			if (parent != null)  this.tree = parent.tree;
			Data = data;
		    this.children = null;
		}

		/// <summary>
		/// Data Payload
		/// </summary>
		public T Data
		{
			get { return data; }
			set
			{
				data = value;
				ITreeNodeBackReference<T> back = data as ITreeNodeBackReference<T>;
				if (back != null)
				{
					back.TreeNode = this;
				}
			}
		}

		/// <summary>
		/// Member structure
		/// </summary>
		public Tree<T> Tree
		{
			get { return tree; }
			set { tree = value; }
		}

		/// <summary>
		/// Parent Node
		/// </summary>
		public TreeNode<T> Parent
		{
			get { return parent; }
		}

		/// <summary>
		/// Children Nodes
		/// </summary>
		public IList<TreeNode<T>> Children
		{
			get { return (IList<TreeNode<T>>)children; }
		}

        /// <summary>
        /// Children Data (immutable)
        /// </summary>
        public T[] ChildrenData
        {
            get
            {
                if (!HasChildren) return null;

                T[] ret = new T[Children.Count];

                int cc = 0;
                foreach (TreeNode<T> node in Children)
                {
                    ret[cc++] = node.Data;
                }

                return ret;
            }
        }

		/// <summary>
		/// Add a new child payload to the tree
		/// </summary>
		/// <param name="child"></param>
		public TreeNode<T> Add(T child)
		{
            // Lazy initlisation of children to save space
            if (children == null) children  = new ManagedCollection<TreeNode<T>>(ChildrenNotifications);

            // Add the node
			TreeNode<T> node = new TreeNode<T>(this, child);
			children.Add(node);
			return node;
		}

        /// <summary>
        /// Add a new child payload to the tree
        /// </summary>
        /// <param name="child"></param>
        public TreeNode<T> Add(TreeNode<T> child)
        {
            // Lazy initlisation of children to save space
            if (children == null) children = new ManagedCollection<TreeNode<T>>(ChildrenNotifications);

            // Add the node
            children.Add(child);
            return child;
        }

		/// <summary>
		/// Helper Method. This this the root node (parent == null)
		/// </summary>
		public bool IsRoot
		{
			get
			{
				return parent == null;
			}
		}

		/// <summary>
		/// Helper Method. Does this node have any children
		/// </summary>
		public bool IsLeaf
		{
			get
			{
				return !HasChildren;
			}
		}

		/// <summary>
		/// Helper Method. Does this node have children
		/// </summary>
		public bool HasChildren
		{
			get
			{
				return children != null && children.Count > 0;
			}
		}

		/// <summary>
		/// Recursive depth. The number if step to the root node
		/// </summary>
		public int Depth
		{
			get
			{
				if (parent == null) return 0;
				return parent.Depth + 1;
			}
		}

		/// <summary>
		/// Simular to depth, get all nodes from this node to the top node
		/// </summary>
		/// <returns></returns>
		public List<TreeNode<T>> GetPathToRoot()
		{
			List<TreeNode<T>> results = new List<TreeNode<T>>();
			results.Add(this);

			if (!IsRoot)
			{
				results.AddRange(parent.GetPathToRoot());
			}

			return results;
		}

		/// <summary>
		/// Find the first node to match the predicate
		/// </summary>
		/// <param name="predicate">predicate match function</param>
		/// <param name="searchDepth">Depth 0 (this), 1 (all children), int.MaxValue (recurse)</param>
		/// <returns>Node</returns>
		public TreeNode<T> Find(Predicate<TreeNode<T>> predicate, int searchDepth)
		{
			return FindInternal(predicate, searchDepth, 0, new List<TreeNode<T>>());
		}

		/// <summary>
		/// Find all nodes to match the predicate
		/// </summary>
		/// <param name="predicate">predicate match function</param>
		/// <param name="searchDepth">Depth 0 (this), 1 (all children), int.MaxValue (recurse), -1 parent, int.MinValue (recurse to root)</param>
		/// <returns>Node list</returns>
		public List<TreeNode<T>> FindAll(Predicate<TreeNode<T>> predicate, int searchDepth)
		{
			return FindAllInternal(predicate, searchDepth, 0, new List<TreeNode<T>>());
		}

		private List<TreeNode<T>> FindAllInternal(Predicate<TreeNode<T>> predicate, int searchDepth, int currentDepth, List<TreeNode<T>> results)
		{
			// Current
			if (predicate(this))  results.Add(this); 

			if (searchDepth != 0)
			{
				if (searchDepth > 0)
				{
					if (searchDepth > currentDepth)
					{
						// Children
						foreach (TreeNode<T> kid in children)
						{
							List<TreeNode<T>> recurse = kid.FindAllInternal(predicate, searchDepth, currentDepth++, results);
							if (recurse != null) results.AddRange(recurse);
						}
					}
					
				}
				else
				{
					if (searchDepth < currentDepth)
					{
						// Parents
						List<TreeNode<T>> recurse = parent.FindAllInternal(predicate, searchDepth, currentDepth--, results);
						if (recurse != null) results.AddRange(recurse);
					}
				}
			}

			return results;
		}

		/// <summary>
		/// Apply an action for each node recursive, includingTHIS
		/// </summary>
		/// <param name="predicate">predicate match function</param>
		/// <param name="searchDepth">Depth 0 (this), 1 (all children), int.MaxValue (recurse), -1 parent, int.MinValue (recurse to root)</param>
		/// <returns>Node list</returns>
		public void ForEach(Action<TreeNode<T>> action, int searchDepth)
		{
			ForEachInternal(action, searchDepth, 0);
		}

		private void ForEachInternal(Action<TreeNode<T>> action, int searchDepth, int currentDepth)
		{
			// Current
			action(this);

			if (searchDepth != 0)
			{
				if(searchDepth > 0)
				{
					if(searchDepth > currentDepth)
					{
                        if (HasChildren)
                        {
                            // Children
                            foreach (TreeNode<T> kid in children)
                            {
                                kid.ForEachInternal(action, searchDepth, currentDepth++);
                            }
                        }
					}
				}
				else
				{
					if(searchDepth < currentDepth)
					{
						// Parents
						parent.ForEachInternal(action, searchDepth, currentDepth--);
					}
				}
			}
		}

		private TreeNode<T> FindInternal(Predicate<TreeNode<T>> predicate, int searchDepth, int currentDepth, List<TreeNode<T>> results)
		{
			// Current
			if (predicate(this)) return this;

			if (searchDepth != 0)
			{
				if (searchDepth > 0)
				{
					if (searchDepth > currentDepth)
					{
                        if (HasChildren)
                        {
                            // Children
                            foreach (TreeNode<T> kid in children)
                            {
                                TreeNode<T> recurse = kid.FindInternal(predicate, searchDepth, currentDepth++, results);
                                if (recurse != null) return recurse;
                            }
                        }
					}

				}
				else
				{
					if (searchDepth < currentDepth)
					{
						// Parents
						TreeNode<T> recurse = parent.FindInternal(predicate, searchDepth, currentDepth--, results);
						if (recurse != null) return recurse;
					}
				}
			}

			return null;
		}

        /// <summary>
        /// Does this node have <paramref name="childData"/> as an IMEDIATE child.
        /// </summary>
        /// <param name="childData">domain class T</param>
        /// <returns>true if exists</returns>
        public bool HasChild(T childData)
        {
            return GetChild(childData) != null;
        }


        /// <summary>
        /// Find a child by data value as an IMEDIATE child.
        /// </summary>
        /// <param name="childData">domain class T</param>
        /// <returns>null if not found</returns>
        public TreeNode<T> GetChild(T childData)
        {
            if (children == null) return null;
            foreach (TreeNode<T> child in children)
            {
                if (object.Equals(child.Data, childData)) return child;
            }
            return null;
        }

        /// <summary>
        /// Return a flattened tree collection, of all subitems including this item
        /// </summary>
        /// <returns></returns>
        public List<T> ToList()
        {
            List<T> res = new List<T>();

            // Add this;
            res.Add(data);

            if (HasChildren)
            {
                foreach (TreeNode<T> child in children)
                {
                    res.AddRange(child.ToList());
                }
            }

            return res;
        }

        /// <summary>
        /// Return the number of children nodes
        /// </summary>
	    public int Count
	    {
            get
            {
                if (children == null) return 0;
                return children.Count;
            }
	    }

        /// <summary>
        /// Get the total (recursive) number of children
        /// </summary>
	    public int TotalChildCount
	    {
	        get
	        {
                if (children == null) return 0;
	            int count = children.Count;
	            foreach (TreeNode<T> node in children)
	            {
	                count += node.TotalChildCount;
	            }
	            return count;
	        }
	    }

        /// <summary>
        /// Get the total (recursive) number of children
        /// </summary>
        public int TotalDepth
        {
            get
            {
                if (!HasChildren) return Depth;
                int maxDepth = Depth;

                foreach (TreeNode<T> node in children)
                {
                    int childDepth = node.TotalDepth;
                    if (childDepth > maxDepth) maxDepth = childDepth;
                }
                return maxDepth;
            }
        }

        /// <summary>
        /// Find a child by data value as an IMEDIATE child.
        /// </summary>
        /// <param name="childData">domain class T</param>
        /// <returns>null if not found</returns>
        public void RemoveChild(T childData)
        {
            RemoveChild(GetChild(childData));
        }

        /// <summary>
        /// Find a child by data value as an IMEDIATE child.
        /// </summary>
        /// <param name="childNode">node</param>
        /// <returns>null if not found</returns>
        public void RemoveChild(TreeNode<T> childNode)
        {
            if (childNode == null) throw new ArgumentNullException("childNode");
            Children.Remove(childNode);
        }

        /// <summary>
        /// Removed all children
        /// </summary>
        public void Clear()
        {
            if (children != null) children.Clear();
        }

		#region ITreeNode<T> Members

		/// <summary>
		/// Data payload
		/// </summary>
		T ITreeNode<T>.Data
		{
			get { return data; }
			set { data = value; }
		}

		/// <summary>
		/// Parent Node
		/// </summary>
		/// <remarks>Have added 'Node' suffix to this name so that the implmenation can implement a type-strong self-referrence</remarks>
		ITreeNode<T> ITreeNode<T>.ParentNode
		{
			get { return parent; }
		}

		/// <summary>
		/// List of children nodes
		/// </summary>
		ICollection<ITreeNode<T>> ITreeNode<T>.ChildrenNodes
		{
			get { return (ICollection<ITreeNode<T>>)children; }
		}

		/// <summary>
		/// Tree to which this node belongs
		/// </summary>
		ITree<T> ITreeNode<T>.Tree
		{
			get { return tree; }
		}

		/// <summary>
		/// Outward (from this to another) nodes
		/// </summary>
		ICollection<IDirectedNode<T>> IDirectedNode<T>.OutwardNodes
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		/// Inward nodes (from another to this) 
		/// </summary>
		ICollection<IDirectedNode<T>> IDirectedNode<T>.InwardNodes
		{
			get { throw new NotImplementedException(); }
		}

		#region IDirectedNode<T> Members

		/// <summary>
		/// The directed graph to which this belongs
		/// </summary>
		IDirectedGraph<T> IDirectedNode<T>.Graph
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		/// <summary>
		/// Links (undirected from/to this node)
		/// </summary>
		ICollection<INode<T>> INode<T>.LinkNodes
		{
			get { throw new NotImplementedException(); }
		}

		#region INode<T> Members

		/// <summary>
		/// The network/graph to which this node belongs
		/// </summary>
		IGraph<T> INode<T>.Graph
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#endregion
		
		private bool ChildrenNotifications(TreeNode<T> node, int index, Notification notification)
		{
			return true;
		}


	    
	}
}
