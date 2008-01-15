using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.UI.Section.Solver
{
    public partial class TreeViewer : UserControl
    {
        public TreeViewer()
        {
            InitializeComponent();

            
        }

        [Browsable(true)]
        public EventHandler<VisEventArgs> OnVisualisationClick
        {
            get
            {
                return treeVis.OnVisualisationClick;
            }
            set
            {
                treeVis.OnVisualisationClick = value;
            }
        }

        [Browsable(false)]
        public SolverController Controller
        {
            get { return treeRenderer.Controller; }
            set { treeRenderer.Controller = value; }
        }

        public void Init(SolverController controller)
        {
            treeRenderer = new TreeVisualisation(controller.Strategy.EvaluationTree, new RectangleInt(0, 0, 1800, 1800), new SizeInt(8, 8));

            treeVis.Visualisation = treeRenderer;
        }

        public void Render()
        {
            treeVis.Render();
        }

        private TreeVisualisation treeRenderer;
    }
}
