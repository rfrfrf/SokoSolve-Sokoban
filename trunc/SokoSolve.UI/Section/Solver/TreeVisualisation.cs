using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation.Visualisation;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.UI.Section.Solver
{
    internal class TreeVisualisation : Visualisation
    {
        private SizeInt cellSize = new SizeInt(4, 4);
        protected Graphics graphics;
        private int maxDepth = 50;
        private int maxMembers = 5000;
        private int maxWidth = 150;
        private List<SegmentRegion> regions;
        private RectangleInt renderCanvas;
        private TreeSegmenter<SolverNode> segments;
        private Tree<SolverNode> tree;
        private RectangleInt windowRegion;
        private int wrapIndent = 3;

        /// <summary>
        /// Element cell size 4,4 by default
        /// </summary>
        public SizeInt CellSize
        {
            get { return cellSize; }
            set { cellSize = value; }
        }

        /// <summary>
        /// The Logical Window Region. 
        /// This is the view port, (0,0) is the first pixel, if the window region starts at (10,10)
        /// then there is an scroll offset of 10, 10.
        /// 
        /// This is seperate to the renderOffset which resolved logic coords to physical ones
        /// (in terms of the Graphics device)
        /// </summary>
        public override RectangleInt WindowRegion
        {
            get { return windowRegion; }
            set { throw new NotSupportedException("Set the RenderCanvas"); }
        }

        /// <summary>
        /// The render offset is the difference between the Graphics root (0,0) and
        /// the logical root. This includes its size
        /// </summary>
        public override RectangleInt RenderCanvas
        {
            get { return renderCanvas; }
            set
            {
                renderCanvas = value;
                // Calculate the new window region
                windowRegion = new RectangleInt(renderCanvas.TopLeft.Add(3, 3), renderCanvas.Size.Subtract(6, 6));
            }
        }

        /// <summary>
        /// Find the absolute pixel postition for an element
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override RectangleInt GetDrawRegion(VisualisationElement Element)
        {
            TreeVisualisationElement treeElement = Element as TreeVisualisationElement;
            if (treeElement != null)
            {
                int elementDepth = treeElement.Node.Depth;
                if (elementDepth >= regions.Count) return null;

                SegmentRegion region = regions[elementDepth];
                if (region == null) return null;

                int height = 0;
                for (int cc = 0; cc < elementDepth; cc++)
                {
                    height += regions[cc].Region.Height;
                }

                return new RectangleInt(region.GetNodePixelPosition(treeElement.Node).Add(0, height), cellSize);
            }
            return null;
        }

        /// <summary>
        /// Find the Element at a pixel location
        /// </summary>
        /// <param name="PixelPosition"></param>
        /// <returns>Null - not found or out of range</returns>
        public override VisualisationElement GetElement(VectorInt PixelPosition)
        {
            // Find element, then find node within element
            for (int cc=0; cc<regions.Count; cc++)
            {
                if (regions[cc].RenderRegion != null && regions[cc].RenderRegion.Contains(PixelPosition))
                {
                    return regions[cc].GetNodeFromPixelPosition(PixelPosition);
                }
            }
            return null;
        }

        /// <summary>
        /// Draw the entire scene
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            Init();

            graphics.DrawRectangle(new Pen(Color.Black), RenderCanvas.ToDrawingRect());

            // Draw all nodes.
            // This is a very slow (but nicely genric) method; it may be faster to not make the wrapping GridVisualisationElement 
            // and just render directly. Then only use the wrapping GridVisualisationElement for mouse (selected) logic.
            foreach (SegmentRegion region in regions)
            {
                foreach (TreeNode<SolverNode> node in region.nodes)
                {
                    TreeVisualisationElement element = region[node];
                    if (element != null)
                    {
                        if (element.RenderRegion != null)
                        {
                            element.Draw(graphics, element.RenderRegion);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Initialise
        /// </summary>
        private void Init()
        {
            segments = new TreeSegmenter<SolverNode>(tree, maxDepth, maxMembers);
            segments.PerformSegment();

            regions = new List<SegmentRegion>(segments.Segments.Count);

            foreach (List<TreeNode<SolverNode>> segment in segments.Segments)
            {
                SegmentRegion region = new SegmentRegion();
                region.owner = this;
                region.MaxRegionNodeWidth = maxWidth;
                region.Region = new SizeInt(maxWidth*CellSize.Width + 50, (segment.Count/maxWidth)*CellSize.Height);
                region.nodes = segment;

                regions.Add(region);
            }
        }

        #region Nested type: SegmentRegion

        private class SegmentRegion
        {
            public int MaxRegionNodeWidth;
            public List<TreeNode<SolverNode>> nodes;
            private List<TreeVisualisationElement> elements;
            public TreeVisualisation owner;
            public SizeInt Region;
            public RectangleInt RenderRegion;

            /// <summary>
            /// Cached Factory Method. Produce an element for a node
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public TreeVisualisationElement this[TreeNode<SolverNode> node]
            {
                get
                {
                    foreach (TreeVisualisationElement element in elements)
                    {
                        if (element.Node ==  node) return element;
                    }
                    TreeVisualisationElement newElement  = new TreeVisualisationElement(owner, node);
                    newElement.RenderRegion = owner.GetDrawRegion(newElement);
                    elements.Add(newElement);
                    return newElement;
                }
            }

            /// <summary>
            /// Find the position within the region
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public VectorInt GetNodePosition(TreeNode<SolverNode> node)
            {
                int idx = nodes.IndexOf(node);
                if (idx < 0) return null;
                int layer = idx/MaxRegionNodeWidth;
                int offset = idx%MaxRegionNodeWidth;
                return new VectorInt(offset, layer);
            }

            /// <summary>
            /// Find the position within the region
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public VectorInt GetNodePixelPosition(TreeNode<SolverNode> node)
            {
                VectorInt logical = GetNodePosition(node);
                if (logical.Y == 0)
                {
                    return logical.Multiply(owner.CellSize).Add(owner.RenderCanvas.TopLeft);
                }
                else
                {
                    return
                        logical.Multiply(owner.CellSize).Add(owner.RenderCanvas.TopLeft).Add(
                            owner.wrapIndent*owner.CellSize.Width, 0);
                }
            }

            /// <summary>
            /// From a pixel position find the relevant node element
            /// </summary>
            /// <param name="pixelPosition"></param>
            /// <returns></returns>
            public VisualisationElement GetNodeFromPixelPosition(VectorInt pixelPosition)
            {
                VectorInt logical = pixelPosition.Subtract(RenderRegion.TopLeft);
                int idx = (pixelPosition.X / owner.cellSize.Width) + (pixelPosition.Y / owner.cellSize.Height) * MaxRegionNodeWidth;
                if (idx < nodes.Count) return this[nodes[idx]];
                return null; // Not found
            }
        }

        #endregion
    }

    internal class TreeVisualisationElement : VisualisationElement
    {
        public TreeVisualisationElement(TreeVisualisation owner, TreeNode<SolverNode> node)
        {
            this.owner = owner;
            this.node = node;
        }

        public RectangleInt RenderRegion
        {
            get { return renderRegion; }
            set { renderRegion = value; }
        }

        private TreeNode<SolverNode> node;
        private RectangleInt renderRegion;
        private TreeVisualisation owner;


        public TreeNode<SolverNode> Node
        {
            get { return node; }
        }

        public SolverNode Data
        {
            get { return node.Data;  }
        }

        public override string GetID()
        {
            return node.Data.NodeID;
        }

        public override string GetName()
        {
            return node.Data.NodeID;
        }

        public override string GetDisplayData()
        {
            return node.Data.NodeID;
        }

        public override void Draw(Graphics graphics, RectangleInt region)
        {
            graphics.FillRectangle(GetBrush(node), region.ToDrawingRect());
            graphics.DrawRectangle(GetPen(node), region.ToDrawingRect());

            if (owner.Selected == this)
            {
                graphics.DrawRectangle(new Pen(Color.Yellow, 2f), region.ToDrawingRect());
            }
        }

        protected  Pen GetPen(TreeNode<SolverNode> node)
        {
            if (node.Data != null)
            {
                if (node.Data.IsStateEvaluated) return new Pen(Color.Blue);
                if (node.Data.IsChildrenEvaluated) return new Pen(Color.Orange);
            }
            return new Pen(Color.Gray);
        }

        protected  Brush GetBrush(TreeNode<SolverNode> node)
        {
            if (node.Data != null)
            {
                switch (node.Data.Status)
                {
                    case (SolverNodeStates.None):
                        // Try something else
                        if (node.Data.Weighting != 0)
                        {
                            int green = 200 - (int)node.Data.Weighting * 50;
                            if (green > 250) green = 250;
                            return new SolidBrush(Color.FromArgb(0, green, 0));
                        }
                        return new SolidBrush(Color.Cornsilk);

                    case (SolverNodeStates.Duplicate): return new SolidBrush(Color.DarkGray);
                    case (SolverNodeStates.Solution): return new SolidBrush(Color.Cyan);
                    case (SolverNodeStates.SolutionPath): return new SolidBrush(Color.LightCyan);
                    case (SolverNodeStates.Dead): return new SolidBrush(Color.Black);
                    case (SolverNodeStates.DeadChildren): return new SolidBrush(Color.DarkRed);
                }
            }
            return new SolidBrush(Color.Purple);
        }
    }
}