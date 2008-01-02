using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation.Visualisation;
using SokoSolve.Core.Analysis.Solver;
using Bitmap=System.Drawing.Bitmap;

namespace SokoSolve.UI.Section.Solver
{
   

    public partial class TreeViewer : UserControl
    {
        private TreeRenderer<SolverNode> renderer;
        private Image result;
        
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TreeViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prepare for rendering
        /// </summary>
        /// <param name="renderTree"></param>
        public void Init(Tree<SolverNode> renderTree)
        {
            result = new Bitmap(Size.Width, Size.Height);
            renderer = new SolverTreeRenderer(renderTree, Graphics.FromImage(result), new RectangleInt(new VectorInt(1, 20), new SizeInt(Size)));
        }

        /// <summary>
        /// Render the tree result. As this is a slow-running process, it must be called manually, and not via refresh, etc.
        /// </summary>
        public void Render()
        {
            if (renderer != null)
            {
                renderer.Draw();
                Refresh();
            }
        }

        /// <summary>
        /// When a node is clicked on the visualisation
        /// </summary>
        public event EventHandler<SolverNodeEventArgs> OnClickNode;


        private void TreeViewer_Paint(object sender, PaintEventArgs e)
        {
            if (result != null)
            {
                e.Graphics.DrawImageUnscaled(result, 0, 0);
            }
        }

        private void TreeViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (renderer != null)
            {
                INode<SolverNode> node = renderer.GetNode(e.X, e.Y);
                if (node != null)
                {
                    string txt = string.Format("({0},{1}) -> Node {2}", e.X, e.Y, node.Data.ToString());
                    labelStatus.Text = txt;
                }
            }
        }

        private void TreeViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (renderer != null)
            {
                TreeNode<SolverNode> node = renderer.GetNode(e.X, e.Y);
                if (node != null)
                {
                    if (OnClickNode != null)
                    {
                        OnClickNode(this, new SolverNodeEventArgs(node));
                    }
                }
            }
        }

        #region Nested type: SolverTreeRenderer

        private class SolverTreeRenderer : TreeRenderer<SolverNode>
        {
            public SolverTreeRenderer(Tree<SolverNode> tree, Graphics graphics, RectangleInt size) : base(tree, graphics)
            {
                WindowRect = size;
                base.NodeSize = new SizeInt(8,8);
            }

            protected override Pen GetPen(TreeNode<SolverNode> node)
            {
                if (node.Data != null)
                {
                    if (node.Data.IsStateEvaluated) return new Pen(Color.Blue);
                    if (node.Data.IsChildrenEvaluated) return new Pen(Color.Orange);
                }
                return new Pen(Color.Gray);
            }

            protected override Brush GetBrush(TreeNode<SolverNode> node)
            {
                if (node.Data != null)
                {
                    switch (node.Data.Status)
                    {
                        case (SolverNodeStates.None):
                            // Try something else
                            if (node.Data.Weighting != 0)
                            {
                                return new SolidBrush(Color.FromArgb(0, (int) (node.Data.Weighting*50), 0));
                            }
                            return new SolidBrush(Color.Cornsilk);

                        case (SolverNodeStates.Duplicate):
                            return new SolidBrush(Color.DarkGray);
                        case (SolverNodeStates.Solution):
                            return new SolidBrush(Color.LightYellow);
                        case (SolverNodeStates.SolutionPath):
                            return new SolidBrush(Color.Yellow);
                        case (SolverNodeStates.Dead):
                            return new SolidBrush(Color.Black);
                        case (SolverNodeStates.DeadChildren):
                            return new SolidBrush(Color.DarkRed);
                    }
                }
                return new SolidBrush(Color.Purple);
            }
        }

        #endregion

       

        
    }
}