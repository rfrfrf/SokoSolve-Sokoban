using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures
{
    /// <summary>
    /// Segment a tree by depth
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeSegmenter<T>
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="maxDepth"></param>
        /// <param name="maxMembers"></param>
        public TreeSegmenter(Tree<T> tree, int maxDepth, int maxMembers)
        {
            this.tree = tree;
            this.maxDepth = maxDepth;
            this.maxMembers = maxMembers;
        }

        public class TreeSegment
        {
            public TreeSegment(List<TreeNode<T>> nodes, int discarded, int total)
            {
                this.nodes = nodes;
                this.discarded = discarded;
                this.total = total;
            }

            public List<TreeNode<T>> Nodes
            {
                get { return nodes; }
                set { nodes = value; }
            }

            public int Discarded
            {
                get { return discarded; }
                set { discarded = value; }
            }

            public int Total
            {
                get { return total; }
                set { total = value; }
            }

            private List<TreeNode<T>> nodes;
            private int discarded;
            private int total;

            public void Add(TreeNode<T> node)
            {
                nodes.Add(node);
            }

            public int Count
            {
                get { return nodes.Count;  }
            }
        }

        /// <summary>
        /// Segment the tree by depth
        /// </summary>
        public void PerformSegment()
        {
            nodesProgressed = 0;
            segments = new List<TreeSegment>(maxDepth);

            for (int cc = 0; cc < maxDepth; cc++)
            {
                segments.Add(new TreeSegment(new List<TreeNode<T>>(), 0, 0));
            }

            tree.Root.ForEach(AssignNode, maxDepth);
        }

        /// <summary>
        /// Segments as generated as <see cref="PerformSegment"/>
        /// </summary>
        public List<TreeSegment> Segments
        {
            get { return segments; }
        }

        /// <summary>
        /// Assign a node to a segment
        /// </summary>
        /// <param name="node"></param>
        private void AssignNode(TreeNode<T> node)
        {
            nodesProgressed++;
            if (node.Depth < segments.Count)
            {
                if (segments[node.Depth].Count >= maxMembers)
                {
                    segments[node.Depth].Discarded++;
                    membersDiscarded++;
                }
                else
                {
                    segments[node.Depth].Add(node);
                }
            }
            else
            {
                membersDiscarded++;
            }
        }

        private Tree<T> tree;
        private List<TreeSegment> segments;
        private int maxDepth;
        private int maxMembers;
        private int membersDiscarded;
        private int nodesProgressed;
    }
}
