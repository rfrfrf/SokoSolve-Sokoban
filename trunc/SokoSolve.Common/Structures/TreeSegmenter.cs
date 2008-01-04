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

        /// <summary>
        /// Segment the tree by depth
        /// </summary>
        public void PerformSegment()
        {
            nodesProgressed = 0;
            segments = new List<List<TreeNode<T>>>(maxDepth);

            for (int cc = 0; cc < maxDepth; cc++)
            {
                segments.Add(new List<TreeNode<T>>(maxMembers));
            }

            tree.Root.ForEach(AssignNode, maxDepth);
        }

        /// <summary>
        /// Segments as generated as <see cref="PerformSegment"/>
        /// </summary>
        public List<List<TreeNode<T>>> Segments
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
        private List<List<TreeNode<T>>> segments;
        private int maxDepth;
        private int maxMembers;
        private int membersDiscarded;
        private int nodesProgressed;
    }
}
