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
        private SizeInt cellSize;
        protected Graphics graphics;
        private int maxDepth = 150;
        private int maxMembers = 5000;
        private int maxWidth = 150;
        private List<SegmentRegion> regions;
        private RectangleInt renderCanvas;
        private TreeSegmenter<SolverNode> segments;
        private Brush summaryBush = new SolidBrush(Color.Black);
        private Font summaryFont = new Font("Arial Narrow", 7f);
        private int summaryIndent = 50;
        private Pen summaryPen = new Pen(new SolidBrush(Color.DimGray), 2f);
        private Tree<SolverNode> tree;
        private RectangleInt windowRegion;
        private SolverController controller;

        /// <summary>
        /// Strong COnstructoin
        /// </summary>
        /// <param name="sourceTree"></param>
        public TreeVisualisation(Tree<SolverNode> sourceTree, RectangleInt renderSize, SizeInt cellSize)
        {
            // this.controller = controller;
            this.tree = sourceTree;

            // Calculate the render size
            RenderCanvas = renderSize;
            CellSize = cellSize;
        }

        /// <summary>
        /// May be null
        /// </summary>
        public SolverController Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        /// <summary>
        /// Element cell size 4,4 by default
        /// </summary>
        public SizeInt CellSize
        {
            get { return cellSize; }
            set 
            { 
                cellSize = value; 
                maxWidth = (renderCanvas.Width - summaryIndent) / cellSize.Width;
            }
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
                    height += regions[cc].RenderRegion.Height;
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
            for (int cc = 0; cc < regions.Count; cc++)
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

            graphics.FillRectangle(SystemBrushes.ControlLight, RenderCanvas.ToDrawingRect());
            graphics.DrawRectangle(new Pen(SystemBrushes.ControlDark, 2f), RenderCanvas.ToDrawingRect());

            // Draw all nodes.
            // This is a very slow (but nicely genric) method; it may be faster to not make the wrapping GridVisualisationElement 
            // and just render directly. Then only use the wrapping GridVisualisationElement for mouse (selected) logic.
            int cc = 0;
            foreach (SegmentRegion region in regions)
            {
                foreach (TreeNode<SolverNode> node in region.treeSegment.Nodes)
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

                // Draw the region summaries
                graphics.DrawLine(summaryPen,
                                  region.RenderRegion.TopLeft.Add(summaryIndent - 5, 1).ToPoint(),
                                  region.RenderRegion.BottomLeft.Add(summaryIndent - 5, -1).ToPoint());

                string txt = region.treeSegment.Count.ToString();
                if (region.treeSegment.Discarded > 0)
                {
                    txt += "+" + region.treeSegment.Discarded.ToString();
                }

                // Alternate text
                if (cc%2 == 0)
                {
                    graphics.DrawString(txt, summaryFont, summaryBush, region.RenderRegion.TopLeft.ToPoint());
                }
                else
                {
                    SizeF txtSize = graphics.MeasureString(txt, summaryFont);
                    VectorInt pos = region.RenderRegion.TopLeft.Add(summaryIndent - 7 - (int) txtSize.Width, 0);
                    graphics.DrawString(txt, summaryFont, summaryBush, pos.ToPoint());
                }


                cc++;
            }

            // Re-draw selected, so that it is on top
            if (Selected != null)
            {
                Selected.Draw(graphics, GetDrawRegion(Selected));
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

            int height = 0;
            foreach (TreeSegmenter<SolverNode>.TreeSegment segment in segments.Segments)
            {
                SegmentRegion region = new SegmentRegion();
                region.owner = this;
                region.MaxRegionNodeWidth = maxWidth;
                SizeInt regionSize =
                    new SizeInt(maxWidth*CellSize.Width + 50, (segment.Count/maxWidth + 1)*CellSize.Height);
                region.RenderRegion = new RectangleInt(renderCanvas.TopLeft.Add(0, height), regionSize);
                region.treeSegment = segment;
                region.treeSegment.Nodes.Sort(
                    delegate(TreeNode<SolverNode> lhs, TreeNode<SolverNode> rhs) { return rhs.Data.Weighting.CompareTo(lhs.Data.Weighting); });

                regions.Add(region);

                height += regionSize.Height;
            }
        }

        #region Nested type: SegmentRegion

        private class SegmentRegion
        {
            private List<TreeVisualisationElement> elements;
            public int MaxRegionNodeWidth;
            public TreeVisualisation owner;
            public RectangleInt RenderRegion;
            public TreeSegmenter<SolverNode>.TreeSegment treeSegment;

            public SegmentRegion()
            {
                elements = new List<TreeVisualisationElement>();
            }

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
                        if (element.Node == node) return element;
                    }
                    TreeVisualisationElement newElement = new TreeVisualisationElement(owner, node);
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
                int idx = treeSegment.Nodes.IndexOf(node);
                if (idx < 0) return VectorInt.Null;
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
                return logical.Multiply(owner.CellSize).Add(owner.RenderCanvas.TopLeft).Add(owner.summaryIndent, 0);
            }

            /// <summary>
            /// From a pixel position find the relevant node element
            /// </summary>
            /// <param name="pixelPosition"></param>
            /// <returns></returns>
            public VisualisationElement GetNodeFromPixelPosition(VectorInt pixelPosition)
            {
                VectorInt logical = pixelPosition.Subtract(RenderRegion.TopLeft).Subtract(owner.summaryIndent, 0);
                int idx = (logical.X/owner.cellSize.Width) + (logical.Y/owner.cellSize.Height)*MaxRegionNodeWidth;
                if (idx < treeSegment.Count) return this[treeSegment.Nodes[idx]];
                return null; // Not found
            }
        }

        #endregion
    }

    internal class TreeVisualisationElement : VisualisationElement
    {
        private TreeNode<SolverNode> node;
        private TreeVisualisation owner;
        private RectangleInt renderRegion;

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


        public TreeNode<SolverNode> Node
        {
            get { return node; }
        }

        public SolverNode Data
        {
            get { return node.Data; }
        }

        public override string GetID()
        {
            return node.Data.NodeID;
        }

        public override string GetName()
        {
            return "ID " + node.Data.NodeID;
        }

        public override string GetDisplayData()
        {
            return "NodeID: " + node.Data.NodeID;
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

        protected Pen GetPen(TreeNode<SolverNode> node)
        {
            if (node.Data != null)
            {
                if (node.Data.IsStateEvaluated) return new Pen(Color.Blue);
                if (node.Data.IsChildrenEvaluated) return new Pen(Color.Orange);
            }
            return new Pen(Color.Gray);
        }

        public float MaxWeighting
        {
            get
            {
                if (owner.Controller == null)
                {
                    return 100;
                }
                else
                {
                    return owner.Controller.Stats.WeightingMax.ValueTotal;
                }
            }
        }

        public float MinWeighting
        {
            get
            {
                if (owner.Controller == null)
                {
                    return 0;
                }
                else
                {
                    return owner.Controller.Stats.WeightingMin.ValueTotal;
                }
            }
        }

        protected Brush GetBrush(TreeNode<SolverNode> node)
        {
            if (node.Data != null)
            {
                switch (node.Data.Status)
                {
                    case (SolverNodeStates.None):

                        // Color Scale 0-255 Blue then 0-255 Green (0-512 color range)
                        // This is match on scale to MinWeighting=0, MaxWeighting=512
                        float colourIndexMin = 0;
                        float colourIndexMax = 255*2;

                        // Range 0-max in weightings
                        float absrange = MaxWeighting - MinWeighting;
                        
                        // scale to colour index range
                        float weightrange = (node.Data.Weighting - MinWeighting) /absrange;
                        int colourIndex =  Convert.ToInt32(weightrange * (colourIndexMax - colourIndexMin) + colourIndexMin);
                        
                        // Convert to RGB
                        int r=0, g=0, b=0;

                        if (colourIndex < 255) g = (colourIndex % 255);
                            else g = 255;
                        if (colourIndex > 255) b = (colourIndex-255) % 255;

                        return new SolidBrush(Color.FromArgb(r, g, b));

                    case (SolverNodeStates.Duplicate):
                        return new SolidBrush(Color.Brown);
                    case (SolverNodeStates.Solution):
                        return new SolidBrush(Color.Red);
                    case (SolverNodeStates.SolutionPath):
                        return new SolidBrush(Color.Red);
                    case (SolverNodeStates.SolutionChain):
                        return
                            new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DottedGrid,
                                                                    Color.Yellow, Color.Blue);
                    case (SolverNodeStates.Dead):
                        return new SolidBrush(Color.Pink);
                    case (SolverNodeStates.DeadChildren):
                        return new SolidBrush(Color.LightPink);
                }
            }
            return new SolidBrush(Color.Purple);
        }
    }
}