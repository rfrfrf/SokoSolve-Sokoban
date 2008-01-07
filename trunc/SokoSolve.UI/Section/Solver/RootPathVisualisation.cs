using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.UI.Section.Solver
{
    class RootPathVisualisation : Visualisation
    {
        public RootPathVisualisation(SizeInt cellSize, SolverController controller)
        {
            this.cellSize = cellSize;
            this.controller = controller;
        }

        public RootPathElement this[SolverNode node]
        {
            get { return elements.Find(delegate(RootPathElement item) { return item.Node == node; }); }
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
        /// Draw the entire scene
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(Color.LightGoldenrodYellow), renderCanvas.ToDrawingRect());

            int ccy = 0;
            int vspace = renderCanvas.Height / (bands.Count + 1);
            foreach (List<RootPathElement> band in bands)
            {
                int hspace = renderCanvas.Width/(band.Count + 1);
                int ccx = 0;
                foreach (RootPathElement node in band)
                {
                    node.Region = new RectangleInt(hspace*(ccx+1) -cellSize.Width/2, vspace*(ccy+1)-cellSize.Height/2, cellSize.Width, cellSize.Height);
                    ccx++;
                }
                ccy++;
            }

            foreach (RootPathElement element in elements)
            {
                element.Draw(graphics, element.Region);
            }
        }

        public void Init(SolverNode current)
        {
            elements = new List<RootPathElement>();
            bands = new List<List<RootPathElement>>();
            
            

            int maxDepth = 0;
            int currentDepth = 10;
            List<RootPathElement> currentBand;

            for (int cc = 0; cc < currentDepth + 2; cc++ )
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

        private List<RootPathElement> elements;
        private List<List<RootPathElement>> bands;
        private SizeInt cellSize;
        internal SolverController controller;
        private RectangleInt windowRegion;
        private RectangleInt renderCanvas;
        
    }

    class RootPathElement : VisualisationElement
    {
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

        private RootPathVisualisation owner;
        private RectangleInt region;
        private SolverNode node;
        private bool onPath;

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
            return "ID"+node.NodeID + " ("+node.TreeNode.Depth.ToString()+")";
        }
        public override void Draw(Graphics graphics, RectangleInt region)
        {
            this.region = region;

            graphics.FillRectangle(GetBrush(node), region.ToDrawingRect());
            graphics.DrawRectangle(GetPen(node), region.ToDrawingRect());

            // Draw path to parent
            if (node.TreeNode.Parent != null)
            {
                RootPathElement parent = owner[node.TreeNode.Parent.Data];
                if (parent != null)
                {
                    if (onPath)
                    {
                        graphics.DrawLine(new Pen(Color.Black, 3f), region.TopMiddle.ToPoint(), owner.GetDrawRegion(parent).BottomMiddle.ToPoint());
                    }
                    else
                    {
                        graphics.DrawLine(new Pen(Color.LightGray), region.TopMiddle.ToPoint(), owner.GetDrawRegion(parent).BottomMiddle.ToPoint() );
                    }
                }
            }

            if (owner.Selected == this)
            {
                graphics.DrawRectangle(new Pen(Color.Yellow, 2f), region.ToDrawingRect());
            }
        }

        protected Pen GetPen(SolverNode node)
        {
            if (node != null)
            {
                if (node.IsStateEvaluated) return new Pen(Color.Blue);
                if (node.IsChildrenEvaluated) return new Pen(Color.Orange);
            }
            return new Pen(Color.Gray);
        }

        protected Brush GetBrush(SolverNode node)
        {
            if (node != null)
            {
                switch (node.Status)
                {
                    case (SolverNodeStates.None):


                        float weighting = node.Weighting;
                        if (weighting < 0)
                        {
                            return new SolidBrush(Color.DarkGray);
                        }
                        if (weighting == 0)
                        {
                            new SolidBrush(Color.Cornsilk);
                        }
                        if (owner.controller.Stats.WeightingMax.ValueTotal > 0)
                        {
                            int min = 50;
                            int max = 250;
                            int value = Convert.ToInt32(weighting / owner.controller.Stats.WeightingMax.ValueTotal * (float)max);
                            return new SolidBrush(Color.FromArgb(50, value, 50));
                        }
                        return new SolidBrush(Color.Cornsilk);

                    case (SolverNodeStates.Duplicate): return new SolidBrush(Color.Brown);
                    case (SolverNodeStates.Solution): return new SolidBrush(Color.Red);
                    case (SolverNodeStates.SolutionPath): return new SolidBrush(Color.Red);
                    case (SolverNodeStates.Dead): return new SolidBrush(Color.Pink);
                    case (SolverNodeStates.DeadChildren): return new SolidBrush(Color.Pink);
                }
            }
            return new SolidBrush(Color.Purple);
        }
    }
}
