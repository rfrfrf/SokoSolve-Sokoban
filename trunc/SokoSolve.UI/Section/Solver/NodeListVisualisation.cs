using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.UI.Section.Solver
{
    class NodeListVisualisation : Visualisation
    {
        private RectangleInt windowRegion;
        private RectangleInt renderCanvas;
        private SizeInt cellSize;
        private int maxCellWidth = 50;
        private List<SolverNode> nodes;
        private List<NodeListVisualisationElement> elements;


        /// <summary>
        /// Strong constructor
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="cellSize"></param>
        public NodeListVisualisation(SolverController controller, List<SolverNode> nodes, SizeInt cellSize)
        {
            Display = new CommonDisplay(controller);
            this.nodes = nodes;
            this.cellSize = cellSize;

            int maxH = ((nodes.Count + 1)/maxCellWidth+1)*cellSize.Height;
            renderCanvas = new RectangleInt(0,0, cellSize.Width*maxCellWidth, maxH);
            windowRegion = renderCanvas;
        }

        /// <summary>
        /// Get the logical postiion for a node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public VectorInt GetLogicalPosition(SolverNode node)
        {
            int idx = IndexOf(nodes, node);
            if (idx < 0) return VectorInt.Null;

            return new VectorInt(idx % maxCellWidth, idx / maxCellWidth);
        }

        int IndexOf(List<SolverNode> list, SolverNode node)
        {
            
            for (int cc=0; cc<list.Count; cc++)
            {
                if (object.ReferenceEquals(list[cc], node)) return cc;
            }
            return -1;
        }

        /// <summary>
        /// Find the absolute pixel postition for an element
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override RectangleInt GetDrawRegion(VisualisationElement Element)
        {
            NodeListVisualisationElement nodeElement = Element as NodeListVisualisationElement;
            if (nodeElement == null) return null;

            VectorInt logical = GetLogicalPosition(nodeElement.Node);
            if (logical == null) return null;

            VectorInt topLeft = logical.Multiply(cellSize).Add(RenderCanvas.TopLeft).Add(windowRegion.TopLeft);

            return new RectangleInt(topLeft, cellSize);
        }

        /// <summary>
        /// Find the Element at a pixel location
        /// </summary>
        /// <param name="PixelPosition"></param>
        /// <returns>Null - not found or out of range</returns>
        public override VisualisationElement GetElement(VectorInt PixelPosition)
        {
            VectorInt logical = PixelPosition.Subtract(windowRegion.TopLeft).Subtract(renderCanvas.TopLeft).Divide(cellSize);
            int idx = logical.X + logical.Y*maxCellWidth;
            if (idx < elements.Count) return elements[idx];
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
            elements = new List<NodeListVisualisationElement>();
            foreach (SolverNode node in nodes)
            {
                NodeListVisualisationElement element = new NodeListVisualisationElement(this, node);
                elements.Add(element);
                element.Draw(graphics, GetDrawRegion(element));
            }
        }

    }

    class NodeListVisualisationElement : VisualisationElement
    {
        public NodeListVisualisationElement(NodeListVisualisation parent, SolverNode node)
        {
            this.node = node;
            this.owner = parent;
        }

        private NodeListVisualisation owner;


        public SolverNode Node
        {
            get { return node; }
            set { node = value; }
        }

        private SolverNode node;

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
            return node.NodeID;
        }

        public override void Draw(Graphics graphics, RectangleInt region)
        {
            graphics.FillRectangle(owner.Display.GetBrush(node.TreeNode), region.ToDrawingRect());
            graphics.DrawRectangle(owner.Display.GetPen(node.TreeNode), region.ToDrawingRect());

            if (owner.Selected == this)
            {
                graphics.DrawRectangle(new Pen(Color.Yellow, 2f), region.ToDrawingRect());
            }
        }

       
    }
}
