using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.UI.Section.Solver
{
    /// <summary>
    /// Draw the path to the root, including all imediate children for each node on the path
    /// </summary>
    internal class RootPathVisualisation : Visualisation
    {
        private List<List<RootPathElement>> bands;
        private SizeInt cellSize;
        internal SolverController controller;
        private List<RootPathElement> elements;
        private RectangleInt renderCanvas;
        private RectangleInt windowRegion;

        /// <summary>
        /// Strong constuctor
        /// </summary>
        /// <param name="cellSize"></param>
        /// <param name="controller"></param>
        public RootPathVisualisation(SizeInt cellSize, SolverController controller)
        {
            this.cellSize = cellSize;
            this.controller = controller;
            this.Display = new CommonDisplay(controller);
        }

        /// <summary>
        /// Retrive a display element for the corrosponding domain node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public RootPathElement this[SolverNode node]
        {
            get { return elements.Find(delegate(RootPathElement item) { return item.Node == node; }); }
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
            set { windowRegion = value; }
        }

        /// <summary>
        /// The render offset is the difference between the Graphics root (0,0) and
        /// the logical root. This includes its size
        /// </summary>
        public override RectangleInt RenderCanvas
        {
            get { return renderCanvas; }
            set { renderCanvas = value; }
        }

        /// <summary>
        /// Find the absolute pixel postition for an element
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override RectangleInt GetDrawRegion(VisualisationElement Element)
        {
            return (Element as RootPathElement).Region;
        }

        /// <summary>
        /// Find the Element at a pixel location
        /// </summary>
        /// <param name="PixelPosition"></param>
        /// <returns>Null - not found or out of range</returns>
        public override VisualisationElement GetElement(VectorInt PixelPosition)
        {
            foreach (RootPathElement element in elements)
            {
                if (element.Region.Contains(PixelPosition)) return element;
            }
            return null;
        }

        /// <summary>
        /// Draw the entire scene
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle(SystemBrushes.ControlLightLight, renderCanvas.ToDrawingRect());

            int ccy = 0;
            int vspace = renderCanvas.Height/(bands.Count + 1);
            foreach (List<RootPathElement> band in bands)
            {
                int hspace = renderCanvas.Width/(band.Count + 1);
                int ccx = 0;
                foreach (RootPathElement node in band)
                {
                    node.Region =
                        new RectangleInt(hspace*(ccx + 1) - cellSize.Width/2, vspace*(ccy + 1) - cellSize.Height/2,
                                         cellSize.Width, cellSize.Height);
                    ccx++;
                }
                ccy++;
            }

            foreach (RootPathElement element in elements)
            {
                element.Draw(graphics, element.Region);
            }
        }

        /// <summary>
        /// Initialise the elements
        /// </summary>
        /// <param name="current"></param>
        public void Init(SolverNode current)
        {
            elements = new List<RootPathElement>();
            bands = new List<List<RootPathElement>>();

            int maxDepth = 0;
            int currentDepth = 10;
            List<RootPathElement> currentBand;

            for (int cc = 0; cc < currentDepth + 2; cc++)
            {
                bands.Add(new List<RootPathElement>());
            }

            while (currentDepth >= maxDepth && current != null)
            {
                // Add
                RootPathElement newElement = new RootPathElement(this, current);
                newElement.OnPath = true;
                elements.Add(newElement);
                bands[currentDepth].Add(newElement);

                // Add all children
                if (current.TreeNode.HasChildren)
                {
                    foreach (SolverNode child in current.TreeNode.ChildrenData)
                    {
                        if (!Exists(child))
                        {
                            RootPathElement childElement = new RootPathElement(this, child);
                            elements.Add(childElement);
                            bands[currentDepth + 1].Add(childElement);
                        }
                    }
                }
                if (current.TreeNode.Parent != null)
                {
                    current = current.TreeNode.Parent.Data;
                }
                else
                {
                    current = null;
                }
                currentDepth--;
            }

            foreach (List<RootPathElement> band in bands)
            {
                // Insert path back in the middle
                RootPathElement path = band.Find(delegate(RootPathElement element) { return element.OnPath; });
                if (path != null)
                {
                    band.Remove(path);
                    band.Insert(band.Count/2, path);
                }
            }
        }

        private bool Exists(SolverNode current)
        {
            return this[current] != null;
        }
    }

    internal class RootPathElement : VisualisationElement
    {
        private SolverNode node;
        private bool onPath;
        private RootPathVisualisation owner;
        private RectangleInt region;

        public RootPathElement(RootPathVisualisation owner, SolverNode node)
        {
            this.owner = owner;
            this.node = node;
        }


        public RootPathVisualisation Owner
        {
            get { return owner; }
        }


        public SolverNode Node
        {
            get { return node; }
        }


        public RectangleInt Region
        {
            get { return region; }
            set { region = value; }
        }

        public bool OnPath
        {
            get { return onPath; }
            set { onPath = value; }
        }

        public override string GetID()
        {
            return node.NodeID;
        }

        public override string GetName()
        {
            return node.NodeID;
        }

        public override string GetDisplayData()
        {
            return "ID" + node.NodeID + " (" + node.TreeNode.Depth.ToString() + ")";
        }

        public override void Draw(Graphics graphics, RectangleInt region)
        {
            this.region = region;

            graphics.FillEllipse(owner.Display.GetBrush(node.TreeNode), region.ToDrawingRect());
            graphics.DrawEllipse(owner.Display.GetPen(node.TreeNode), region.ToDrawingRect());

            // Draw path to parent
            if (node.TreeNode.Parent != null)
            {
                RootPathElement parent = owner[node.TreeNode.Parent.Data];
                if (parent != null)
                {
                    if (onPath)
                    {
                        graphics.DrawLine(new Pen(Color.Black, 2f), region.TopMiddle.ToPoint(),
                                          owner.GetDrawRegion(parent).BottomMiddle.ToPoint());
                    }
                    else
                    {
                        graphics.DrawLine(new Pen(Color.Black), region.TopMiddle.ToPoint(),
                                          owner.GetDrawRegion(parent).BottomMiddle.ToPoint());
                    }
                }
            }

            if (owner.Selected == this)
            {
                graphics.DrawRectangle(new Pen(Color.Yellow, 2f), region.ToDrawingRect());
            }

            // ID and Weighting text
            graphics.DrawString(node.Weighting.ToString("0"), font, brushShaddow, region.TopLeft.Add(1,1).ToPoint());
            graphics.DrawString(node.Weighting.ToString("0"), font, brush, region.TopLeft.ToPoint());

            if (false)
            {
                // Show ID below
                graphics.DrawString(node.NodeID, font, brushShaddow, region.TopLeft.X + 1, region.TopLeft.Y + 9);
                graphics.DrawString(node.NodeID, font, brush, region.TopLeft.X, region.TopLeft.Y + 8);
            }
            
        }

        private static Font font = new Font("Arial Narrow", 8f, FontStyle.Bold);
        private static Brush brush = new SolidBrush(Color.Yellow);
        private static Brush brushShaddow = new SolidBrush(Color.Black);
    
    }
}