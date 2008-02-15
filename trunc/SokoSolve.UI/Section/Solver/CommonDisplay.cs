using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.UI.Section.Solver
{
    public class CommonDisplay
    {
        SolverController controller;

        public CommonDisplay(SolverController controller)
        {
            this.controller = controller;
        }

        public float MaxWeighting
        {
            get
            {
                if (controller == null)
                {
                    return 100;
                }
                else
                {
                    return controller.Stats.WeightingMax.ValueTotal;
                }
            }
        }

        public float MinWeighting
        {
            get
            {
                if (controller == null)
                {
                    return 0;
                }
                else
                {
                    return controller.Stats.WeightingMin.ValueTotal;
                }
            }
        }

        public Pen GetPen(TreeNode<SolverNode> node)
        {
            if (node != null && node.Data != null)
            {
                // In reverse order...

                if (node.Data.Status == SolverNodeStates.Dead) return new Pen(Color.Purple, 2f);
                if (node.Data.Status == SolverNodeStates.DeadChildren) return new Pen(Color.Purple, 2f);
                if (node.Data.Status == SolverNodeStates.DeadExhausted) return new Pen(Color.Yellow, 2f);
                
                if (node.Data.IsChildrenEvaluated) return new Pen(Color.Blue, 1f);
                if (node.Data.IsStateEvaluated) return new Pen(Color.LightBlue, 1f);
                
                return new Pen(Color.White, 1f);
            }
            return new Pen(Color.LightGray, 1f);
        }

        public Brush GetBrush(TreeNode<SolverNode> node)
        {
            if (node != null && node.Data != null)
            {
                switch (node.Data.Status)
                {
                    case (SolverNodeStates.None):

                        // Color Scale 0-255 Blue then 0-255 Green (0-512 color range)
                        // This is match on scale to MinWeighting=0, MaxWeighting=512
                        float colourIndexMin = 0;
                        float colourIndexMax = 255;

                        // Range 0-max in weightings
                        float absrange = MaxWeighting - MinWeighting;

                        // scale to colour index range
                        float weightrange = (node.Data.Weighting - MinWeighting) / absrange;
                        int colourIndex = absrange==0 ? 0 : Convert.ToInt32(weightrange * (colourIndexMax - colourIndexMin) + colourIndexMin);

                        // Convert to RGB
                        int r = 0, g = 0, b = 0;

                        g = colourIndex;

                        return new SolidBrush(Color.FromArgb(r, g, b));

                    case (SolverNodeStates.Duplicate):
                        return new SolidBrush(Color.Brown);
                    case (SolverNodeStates.Solution):
                        return new SolidBrush(Color.Red);
                    case (SolverNodeStates.SolutionPath):
                        return new SolidBrush(Color.Red);
                    case (SolverNodeStates.SolutionChain):
                        return
                            new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DottedDiamond,
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
