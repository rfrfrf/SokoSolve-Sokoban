using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures.Evaluation.Strategy;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// Sead the deadmap with common senarios, outside of  the generic rules. See remarks for encoding rules
    /// </summary>
    /// <remarks>
    ///  Hint map of 
    ///    <list>
    ///        <item>W - Wall</item>
    ///        <item>C - Crate (but not goal!)</item>
    ///        <item>O - Crate OR Goal</item>
    ///        <item>D - Dead position</item>
    ///        <item>? - Ignore</item>
    ///    </list>
    /// </remarks>
    internal class HintsRule : StrategyRule<DeadMapState>
    {
        #region HintCell enum

        public enum HintCell
        {
            /// <summary>
            /// Encode as W
            /// </summary>
            Wall,

            /// <summary>
            /// Encode as C
            /// </summary>
            CrateNotGoal,

            /// <summary>
            /// Encode as O
            /// </summary>
            CrateGoal,

            /// <summary>
            /// Encode as ?
            /// </summary>
            Ignore,

            /// <summary>
            /// Encode as D
            /// </summary>
            DeadStatic,

            /// <summary>
            /// Encode as d
            /// </summary>
            DeadStaticOrDynamic
        }

        #endregion

        private List<Hint> hints;
        private StaticAnalysis staticAnalysis;


        /// <summary>
        /// Standard Constructor
        /// </summary>
        /// <param name="strategy"></param>
        public HintsRule(StrategyPatternBase<DeadMapState> strategy, StaticAnalysis staticAnalysis)
            : base(strategy, "Sead the deadmap with common senarios, outside of  the generic rules")
        {
            this.staticAnalysis = staticAnalysis;
        }

        public void ProcessHints()
        {

            hints = new List<Hint>();

            // Apply the hint any any of the four directions
            CreateHintRotations("Recess Hovering #1", new string[]
                                                          {
                                                              "?WW?",
                                                              "WDDW",
                                                              "?CO?"
                                                          }, 3);

            CreateHintRotations("Recess Hovering #2", new string[]
                                                          {
                                                              "?WW?",
                                                              "WDDW",
                                                              "?OC?"
                                                          }, 3);

            CreateHintRotations("Dead Corner #1", new string[]
                                                      {
                                                          "WWW",
                                                          "WDC",
                                                          "WO?"
                                                      }, 3);

            CreateHintRotations("Dead Corner #2", new string[]
                                                      {
                                                          "WWW",
                                                          "WDO",
                                                          "WC?"
                                                      }, 3);
        }

        /// <summary>
        /// Create rotations for the hints
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hint"></param>
        /// <param name="rotations"></param>
        /// <returns></returns>
        public void CreateHintRotations(string name, string[] hint, int rotations)
        {
            HintCell[,] hintMap = Convert(hint);
            Hint aHint = new Hint(name, hintMap);
            if (aHint.PreProcess(staticAnalysis)) hints.Add(aHint);
            if (rotations > 0)
            {
                HintCell[,] last = hintMap;
                for (int cc = 0; cc < rotations; cc++)
                {
                    last = Rotate<HintCell>(last);

                    Hint newHint = new Hint(name + "-rot" + cc.ToString(), last);
                    if (newHint.PreProcess(staticAnalysis)) hints.Add(newHint);
                    
                }
            }
        }

        /// <summary>
        /// Convert a list of strings into a grid array
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static HintCell[,] Convert(string[] source)
        {
            int max = 0;
            foreach (string s in source)
            {
                max = Math.Max(max, s.Length);
            }

            HintCell[,] result = new HintCell[max,source.Length];
            for (int cy = 0; cy < source.Length; cy++)
            {
                for (int cx = 0; cx < source[cy].Length; cx++)
                {
                    switch (source[cy][cx])
                    {
                        case ('W'):
                            result[cx, cy] = HintCell.Wall;
                            break;
                        case ('C'):
                            result[cx, cy] = HintCell.CrateNotGoal;
                            break;
                        case ('O'):
                            result[cx, cy] = HintCell.CrateGoal;
                            break;
                        case ('D'):
                            result[cx, cy] = HintCell.DeadStatic;
                            break;
                        case ('d'):
                            result[cx, cy] = HintCell.DeadStaticOrDynamic;
                            break;
                        case ('?'):
                            result[cx, cy] = HintCell.Ignore;
                            break;

                        default:
                            throw new InvalidDataException("Unknown char " + source[cy][cx]);
                    }
                }
            }
            return result;
        }

      


        /// <summary>
        /// Rotate an array left-to-right by 90deg
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T[,] Rotate<T>(T[,] source)
        {
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);
            T[,] result = new T[sizeY,sizeX]; // swap dimensions

            for (int cx = 0; cx < sizeX; cx++)
            {
                for (int cy = 0; cy < sizeY; cy++)
                {
                    result[sizeY - cy - 1, cx] = source[cx, cy];
                }
            }

            return result;
        }

        /// <summary>
        /// Evaluate the rule on a statefull item (<see cref="StateContext"/> may be enriched if needed)
        /// </summary>
        /// <param name="StateContext"></param>
        /// <returns>true is an exit condition</returns>
        public override RuleResult Evaluate(DeadMapState StateContext)
        {
            if (!StateContext.IsDynamic) return RuleResult.Skipped;

           

           
                    foreach (Hint hint in hints)
                    {
                        hint.Check(StateContext);
                    }
                
            return RuleResult.Success;
        }

        #region Nested type: Hint

        /// <summary>
        /// Basic Hint
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

            public void PreProcessHintLocation(int cx, int cy, StaticAnalysis staticAnalysis)
            {
                for (int hintY = 0; hintY < hint.GetLength(1); hintY++)
                    for (int hintX = 0; hintX < hint.GetLength(0); hintX++)
                    {
                        if (staticAnalysis.WallMap.Size.Width <= hintX + cx) return;
                        if (staticAnalysis.WallMap.Size.Height <= hintY + cy) return;

                        switch (hint[hintX, hintY])
                        {
                            case (HintCell.Wall):
                                if (!staticAnalysis.WallMap[cx + hintX, cy + hintY]) return;
                                break;
                            case (HintCell.CrateNotGoal):
                                if (!staticAnalysis.FloorMap[cx + hintX, cy + hintY] &&
                                    !staticAnalysis.GoalMap[cx + hintX, cy + hintY]) return;
                                break;
                            case (HintCell.CrateGoal):
                                if (!staticAnalysis.FloorMap[cx + hintX, cy + hintY] &&
                                    staticAnalysis.GoalMap[cx + hintX, cy + hintY]) return;
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

                    for (int hintY = 0; hintY < hint.GetLength(1); hintY++)
                        for (int hintX = 0; hintX < hint.GetLength(0); hintX++)
                        {
                            switch (hint[hintX, hintY])
                            {
                                case (HintCell.Wall):
                                    if (!context.WallMap[cx + hintX, cy + hintY]) return;
                                    break;
                                case (HintCell.CrateNotGoal):
                                    if (
                                        !(context.CrateMap[cx + hintX, cy + hintY] &&
                                          !context.GoalMap[cx + hintX, cy + hintY])) return;
                                    break;
                                case (HintCell.CrateGoal):
                                    if (
                                        !(context.CrateMap[cx + hintX, cy + hintY])) return;
                                    break;
                                case (HintCell.DeadStatic):
                                    // Static deadmap or dynamic dead map
                                    if (!context.StaticAnalysis.DeadMap[cx + hintX, cy + hintY] &&
                                        !context[cx + hintX, cy + hintY]) return;
                                    break;
                                case (HintCell.Ignore):
                                    break;
                                default:
                                    throw new InvalidDataException("Hint char is invalid:" + hint[hintX, hintY]);
                            }
                        }

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

        #endregion
    }
}