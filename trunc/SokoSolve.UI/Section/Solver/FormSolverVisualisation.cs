using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.UI.Section.Solver
{
    public partial class FormSolverVisualisation : Form
    {
        public FormSolverVisualisation()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        public SolverController Controller
        {
            get { return solverSectionVisualisation1.Controller; }
            set { solverSectionVisualisation1.Controller = value; }
        }

        private void solverSectionVisualisation1_Load(object sender, EventArgs e)
        {

        }

    }
}