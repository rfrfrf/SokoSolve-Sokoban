using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SokoSolve.UI.Section.Solver
{
    public partial class ExitConditions : UserControl
    {
        public ExitConditions()
        {
            InitializeComponent();
        }

        private void cbProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the values
            switch(cbProfiles.SelectedIndex)
            {
                case (2):
                    upMaxDepth.Value = 2000;
                    upMaxNodes.Value = 400000;
                    upMaxItter.Value = 800000;
                    upMaxTime.Value = 10m;
                    cbStopOnSolution.Checked = true;
                    break;
                case(1):
                    upMaxDepth.Value = 200;
                    upMaxNodes.Value = 40000;
                    upMaxItter.Value = 80000;
                    upMaxTime.Value = 5m;
                    cbStopOnSolution.Checked = true;
                    break;
                case(0):
                default :
                    upMaxDepth.Value = 100;
                    upMaxNodes.Value = 20000;
                    upMaxItter.Value = 40000;
                    upMaxTime.Value = 2.5m;
                    cbStopOnSolution.Checked = true;
                    break;
            }
        }
    }
}
