using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Common.Structures.Evaluation.Visualisation
{
    /// <summary>
    /// Render a large tree, using depth
    /// </summary>
    public class TreeRenderer<T> where T : IEvaluationNode
    {
        /// <summary>
        /// Strong Contructor
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="graphics"></param>
        public TreeRenderer(Tree<T> tree, Graphics graphics)
        {
            this.tree = tree;
            this.graphics = graphics;
        }

        /// <summary>
        /// Initialise and Draw
        /// </summary>
        public void Draw()
        {
            Init();

            graphics.DrawRectangle(new Pen(Color.Black), windowRect.ToDrawingRect() );

            foreach (List<TreeNode<T>> segment in segments.Segments)
            {
                foreach (TreeNode<T> node in segment)
                {
                    VectorInt pos = GetPixelFromLogical(segment, node);
                    RenderNode(pos.X, pos.Y, node);
                    
                }
            }
        }

        /// <summary>
        /// Get the drawing position for a node
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public VectorInt GetPixelFromLogical(List<TreeNode<T>> segment, TreeNode<T> node)
        {
            return new VectorInt( segment.IndexOf(node)*nodeSize.X + windowRect.TopLeft.X,
                                            segments.Segments.IndexOf(segment) * nodeSize.Y + windowRect.TopLeft.Y);
        }

        /// <summary>
        /// From a pixel position find the node
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public TreeNode<T> GetNodeFromPixel(VectorInt position)
        {
            VectorInt rel = position.Subtract(windowRect.TopLeft);
            if (rel.X < 0 || rel.Y < 0) return null;

            VectorInt cell = rel.Divide(nodeSize);
            
            // Lookup segment
            if (cell.Y >= segments.Segments.Count) return null;
            if (segments.Segments[cell.Y] == null) return null;
            if (cell.X >= segments.Segments[cell.Y].Count) return null;

            return segments.Segments[cell.Y][cell.X];
        }

        /// <summary>
        /// Render a Node
        /// </summary>
        /// <param name="x"></param>
        /// <param name="ccy"></param>
        /// <param name="node"></param>
        virtual protected void RenderNode(int x, int ccy, TreeNode<T> node)
        {
            graphics.FillRectangle(GetBrush(node), x, ccy, nodeSize.Width, nodeSize.Height);
            graphics.DrawRectangle(GetPen(node), x, ccy, nodeSize.Width, nodeSize.Height);
        }

        /// <summary>
        /// Get a brush for a node fill
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        virtual protected Brush GetBrush(TreeNode<T> node)
        {
            if (node.Data.IsChildrenEvaluated) return new SolidBrush(Color.Black);
            if (node.Data.IsStateEvaluated) return new SolidBrush(Color.Blue);

            return new SolidBrush(Color.Green);
        }

        /// <summary>
        /// Get a pen for the node border
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        virtual protected Pen GetPen(TreeNode<T> node)
        {
           if (node.Data.IsChildrenEvaluated) return new Pen(Color.Black);
           if (node.Data.IsStateEvaluated) return new Pen(Color.Blue);

            return new Pen(Color.WhiteSmoke);
        }

        /// <summary>
        /// Initialise
        /// </summary>
        private void Init()
        {
            segments = new SegmentTree<T>(tree, maxDepth, maxWidth);
            segments.PerformSegment();   
        }

        public TreeNode<T> GetNode(int x, int y)
        {
            return GetNodeFromPixel(new VectorInt(x, y));
        }

        public RectangleInt WindowRect
        {
            get { return windowRect; }
            set { windowRect = value; }
        }

        public SizeInt NodeSize
        {
            get { return nodeSize; }
            set { nodeSize = value; }
        }

        public int MaxDepth
        {
            get { return maxDepth; }
            set { maxDepth = value; }
        }

        public int MaxWidth
        {
            get { return maxWidth; }
            set { maxWidth = value; }
        }

        /// <summary>
        /// Segment a tree by depth
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class SegmentTree<T>
        {
            public SegmentTree(Tree<T> tree, int maxDepth, int maxMembers)
            {
                this.tree = tree;
                this.maxDepth = maxDepth;
                this.maxMembers = maxMembers;
            }

            public void PerformSegment()
            {
                nodesProgressed = 0;
                segments = new List<List<TreeNode<T>>>(maxDepth);

                for(int cc=0; cc<maxDepth; cc++)
                {
                    segments.Add(new List<TreeNode<T>>(maxMembers));
                }

                tree.Root.ForEach(AssignNode, maxDepth);
            }

            public List<List<TreeNode<T>>> Segments
            {
                get { return segments; }
            }

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

        private RectangleInt windowRect = new  RectangleInt(new VectorInt(0,0), new SizeInt(640, 480));
        private SizeInt nodeSize = new SizeInt(4, 4);   // with padding

        private Tree<T> tree;
        protected Graphics graphics;
        private SegmentTree<T> segments;
        private int maxDepth = 50;
        private int maxWidth = 300;
    }
}
