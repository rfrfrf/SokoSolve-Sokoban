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


            foreach (SegmentRegion region in regions)
            {
                foreach (TreeNode<T> node in region.nodes)
                {
                    VectorInt pos = GetPixelFromLogical(node);
                    RenderNode(pos.X, pos.Y, node);
                }
            }
        }

        class SegmentRegion
        {
            public int MaxRegionNodeWidth;
            public List<TreeNode<T>> nodes;
            public SizeInt Region;
            public TreeRenderer<T> parent;

            /// <summary>
            /// Find the position within the region
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public VectorInt GetNodePosition(TreeNode<T> node)
            {
                int idx = nodes.IndexOf(node);
                if (idx < 0) return VectorInt.Empty;
                int layer = idx / MaxRegionNodeWidth;
                int offset = idx % MaxRegionNodeWidth;
                return new VectorInt(offset, layer);
            }

            /// <summary>
            /// Find the position within the region
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public VectorInt GetNodePixelPosition(TreeNode<T> node)
            {
                VectorInt logical = GetNodePosition(node);
                if (logical.Y == 0)
                {
                    return logical.Multiply(parent.nodeSize).Add(parent.globalOffset);
                }
                else
                {
                    return logical.Multiply(parent.nodeSize).Add(parent.globalOffset).Add(parent.indent*parent.nodeSize.Width, 0);
                }
                
            }
        }

        /// <summary>
        /// Get the drawing position for a node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public VectorInt GetPixelFromLogical(TreeNode<T> node)
        {
            if (node.Depth >= regions.Count) return VectorInt.Empty;

            SegmentRegion region = regions[node.Depth];
            if (region == null) return VectorInt.Empty; 

            int height = 0;
            for (int cc = 0; cc < node.Depth; cc++)
            {
                height += regions[cc].Region.Height;
            }

            return region.GetNodePixelPosition(node).Add(0, height);
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

            VectorInt cell = rel.Divide(nodeSize.ToVectorInt);
            
            // Lookup segment
            if (cell.Y >= segments.Segments.Count) return null;
            if (segments.Segments[cell.Y] == null) return null;
            if (cell.X >= segments.Segments[cell.Y].Count) return null;

            return segments.Segments[cell.Y].Nodes[cell.X];
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
            segments = new TreeSegmenter<T>(tree, maxDepth, 2000);
            segments.PerformSegment();   

            regions = new List<SegmentRegion>(segments.Segments.Count);

            foreach (TreeSegmenter<T>.TreeSegment segment in segments.Segments)
            {
                SegmentRegion region = new SegmentRegion();
                region.parent = this;
                region.MaxRegionNodeWidth = maxWidth;
                region.Region = new SizeInt(maxWidth*nodeSize.Width+ 50, (segment.Count / maxWidth)*nodeSize.Height);
                region.nodes = segment.Nodes;
                
                regions.Add(region);
            }
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

        

        private RectangleInt windowRect = new  RectangleInt(new VectorInt(0,0), new SizeInt(640, 480));
        private SizeInt nodeSize = new SizeInt(4, 4);   // with padding

        private Tree<T> tree;
        protected Graphics graphics;
        private TreeSegmenter<T> segments;
        private List<SegmentRegion> regions;
        private int maxDepth = 150;
        private int maxWidth = 150;
        VectorInt globalOffset = new VectorInt(1,20);
        private int indent = 3;
    }
}
