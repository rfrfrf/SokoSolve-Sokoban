using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// In this context a hint is a region of the puzzle that conforms to a spacial pattern.
    /// The hint may be dynamic or static, and can is pre-processed to add speed.
    /// Static element are pre-process to limit the total number of positions to check.
    /// </summary>
    public class Hint
    {
        private List<VectorInt> checkLocations;
        private HintCell[,] hint;
        private bool IsForward;
        private string name;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hint"></param>
        public Hint(string name, HintCell[,] hint)
        {
            this.name = name;
            this.hint = hint;
            this.IsForward = true;
        }

        /// <summary>
        /// The hint cell array
        /// </summary>
        public HintCell[,] HintArray
        {
            get { return hint; }
        }

        /// <summary>
        /// Hint Name
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// List of positions this hint may apply to
        /// </summary>
        public List<VectorInt> CheckLocations
        {
            get { return checkLocations; }
        }

        /// <summary>
        /// Has preprocessing found any matches
        /// </summary>
        public bool HasPreProcessedMatches
        {
            get { return checkLocations != null; }
        }

        /// <summary>
        /// Filter out irrelivant checks (use wall/floor to match). From all posible check locations (Width, Height)
        /// generate the list of possible hits
        /// </summary>
        /// <param name="staticAnalysis"></param>
        /// <returns>true of match found</returns>
        public bool PreProcess(StaticAnalysis staticAnalysis)
        {
            checkLocations = new List<VectorInt>();

            for (int cx = 0; cx < staticAnalysis.WallMap.Size.Width; cx++)
                for (int cy = 0; cy < staticAnalysis.WallMap.Size.Height; cy++)
                {
                    PreProcessHintLocation(cx, cy, staticAnalysis);
                }

            if (checkLocations.Count == 0)
            {
                checkLocations = null;
                return false;
            }
            return true;
        }


        /// <summary>
        /// Perform the PreProcess check at a specific location
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="staticAnalysis"></param>
        public void PreProcessHintLocation(int cx, int cy, StaticAnalysis staticAnalysis)
        {
            for (int hintY = 0; hintY < hint.GetLength(1); hintY++)
                for (int hintX = 0; hintX < hint.GetLength(0); hintX++)
                {
                    if (staticAnalysis.WallMap.Size.Width <= hintX + cx) return;
                    if (staticAnalysis.WallMap.Size.Height <= hintY + cy) return;

                    switch (hint[hintX, hintY])
                    {
                        case (HintCell.Floor):
                            if (!staticAnalysis.FloorMap[cx + hintX, cy + hintY]) return;
                            break;

                        case (HintCell.Wall):
                            if (!staticAnalysis.WallMap[cx + hintX, cy + hintY]) return;
                            break;
                        case (HintCell.CrateNotGoal):
                            if (!staticAnalysis.FloorMap[cx + hintX, cy + hintY] &&
                                !staticAnalysis.GoalMap[cx + hintX, cy + hintY]) return;
                            break;
                        case (HintCell.CrateGoalOrCrateNonGoal):
                            if (!staticAnalysis.FloorMap[cx + hintX, cy + hintY]) return;
                            break;
                        case (HintCell.DeadStatic):
                            if (!staticAnalysis.DeadMap[cx + hintX, cy + hintY]) return;
                            break;
                        case (HintCell.DeadStaticOrDynamic):
                            // Cannot precalculate this
                            break;
                        case (HintCell.Ignore):
                            break;
                        default:
                            throw new InvalidDataException("Unknown hint cell state:" + hint[hintX, hintY]);
                    }
                }
            // All criteria passed
            this.checkLocations.Add(new VectorInt(cx, cy));
        }

        /// <summary>
        /// Check to see if the dead map hint applies
        /// </summary>
        /// <param name="context"></param>
        public void Check(DeadMapState context)
        {
            foreach (VectorInt checkLocation in checkLocations)
            {
                int cx = checkLocation.X;
                int cy = checkLocation.Y;

                if (CheckMatchAtLocation(context, cx, cy))
                {
                    // All criteria passed
                    // Mark all crates as dead
                    for (int hintY = 0; hintY < hint.GetLength(1); hintY++)
                        for (int hintX = 0; hintX < hint.GetLength(0); hintX++)
                        {
                            if (hint[hintX, hintY] == HintCell.CrateNotGoal) context[cx + hintX, cy + hintY] = true;
                        }

                    // Increase the hints
                    context.DeadMapAnalysis.StaticAnalysis.Controller.Stats.HintsUsed.Increment();
                }
            }
        }

        /// <summary>
        /// Perform the full check at a location
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        bool CheckMatchAtLocation(DeadMapState context, int cx, int cy)
        {
            for (int hintY = 0; hintY < hint.GetLength(1); hintY++)
                for (int hintX = 0; hintX < hint.GetLength(0); hintX++)
                {
                    switch (hint[hintX, hintY])
                    {
                        case (HintCell.Floor):
                            if (!context.StaticAnalysis.FloorMap[cx + hintX, cy + hintY]) return false;
                            break;
                        case (HintCell.Wall):
                            if (!context.WallMap[cx + hintX, cy + hintY]) return false;
                            break;
                        case (HintCell.CrateNotGoal):
                            if (!(context.CrateMap[cx + hintX, cy + hintY] && !context.GoalMap[cx + hintX, cy + hintY])) return false;
                            break;
                        case (HintCell.CrateGoalOrCrateNonGoal):
                            if (!(context.CrateMap[cx + hintX, cy + hintY])) return false;
                            break;
                        case (HintCell.DeadStatic):
                            // Dead cannot be in the move map
                            if (context.MoveMap[cx + hintX, cy + hintY]) return false;
                            // Static deadmap or dynamic dead map
                            if (!context.StaticAnalysis.DeadMap[cx + hintX, cy + hintY] &&
                                !context[cx + hintX, cy + hintY]) return false;
                            break;
                        case (HintCell.Ignore):
                            break;
                        default:
                            throw new InvalidDataException("Hint char is invalid:" + hint[hintX, hintY]);
                    }
                }
            return true;
        }
    }    
}
