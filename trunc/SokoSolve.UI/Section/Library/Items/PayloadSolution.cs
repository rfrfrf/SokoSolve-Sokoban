using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Math;
using SokoSolve.Core.Game;
using SokoSolve.Core.Model;

namespace SokoSolve.UI.Section.Library.Items
{
    public partial class PayloadSolution : UserControl
    {
        public PayloadSolution()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Current solution.
        /// Sets the internal UI state
        /// </summary>
        [Browsable(false)]
        public Solution Solution
        {
            get { return solution; }
            set 
            { 
                solution = value;
                if (solution == null || string.IsNullOrEmpty(solution.Steps))
                {
                    // No solution
                    upSteps.Enabled = false;
                    tbStep.Enabled = false;
                }
                else
                {
                    upSteps.Enabled = true;
                    tbStep.Enabled = true;
                    steps = GenerateGameMapsFromSolution(solution);
                    upSteps.Maximum = steps.Count - 1;
                    tbStep.Maximum = steps.Count - 1;
                    CurrentStep = 0;
                }
            }
        }

        /// <summary>
        /// Generate a list of all map states for each of the solution moves
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        private List<SokobanMap> GenerateGameMapsFromSolution(Solution solution)
        {
            Game game = new Game(solution.Map.Puzzle, solution.Map.Map);
            List<SokobanMap> result = new List<SokobanMap>();
            foreach (Direction step in solution.ToPath().Moves)
            {
                result.Add(new SokobanMap(game.Current));
                Game.MoveResult moveResult = game.Move(step);
                if (moveResult == Game.MoveResult.Invalid) break;
            }
            return result;
        }

        /// <summary>
        /// Current Solution step, also updates UI
        /// </summary>
        [Browsable(false)]
        public int CurrentStep
        {
            get { return currentStep; }
            set
            {
                currentStep = value;
                upSteps.Value = value;
                tbStep.Value = value;
                label1.Text = string.Format("Step {0} of {1}", currentStep, steps.Count - 1);

                ReDraw();
            }
        }

        /// <summary>
        /// Draw the current state
        /// </summary>
        private void ReDraw()
        {
            if (steps != null && steps.Count > currentStep)
            {
                pbSolution.Image = DrawingHelper.DrawPuzzle(steps[CurrentStep]);
            }
        }

        int currentStep;
        Solution solution;
        List<SokobanMap> steps;

        private void tbStep_ValueChanged(object sender, EventArgs e)
        {
            CurrentStep = tbStep.Value;
        }
        
        private void upSteps_ValueChanged(object sender, EventArgs e)
        {
            CurrentStep = (int)upSteps.Value;
        }
    }
}
