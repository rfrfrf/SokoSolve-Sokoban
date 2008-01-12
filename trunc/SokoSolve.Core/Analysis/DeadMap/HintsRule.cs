using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures.Evaluation.Strategy;

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
        /// <summary>
        /// Standard Constructor
        /// </summary>
        /// <param name="strategy"></param>
        public HintsRule(StrategyPatternBase<DeadMapState> strategy) : base(strategy, "Sead the deadmap with common senarios, outside of  the generic rules")
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
            Hint aHint = new Hint(name, hint);
            hints.Add(aHint);
            if (rotations > 0)
            {
                string[] last = hint;
                for (int cc = 0; cc < rotations; cc++)
                {
                    last = Convert(Rotate(Convert(last)));

                    Hint newHint = new Hint(name + "-rot" + cc.ToString(), last);
                    hints.Add(newHint);
                }
            }
        }

        /// <summary>
        /// Convert a list of strings into a grid array
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static char[,] Convert(string[] source)
        {
            int max = 0;
            foreach (string s in source)
            {
                max = Math.Max(max, s.Length);
            }

            char[,] result = new char[max,source.Length];
            for (int cy = 0; cy < source.Length; cy++)
            {
                for (int cx = 0; cx < source[cy].Length; cx++)
                {
                    result[cx, cy] = source[cy][cx];
                }
            }
            return result;
        }

        /// <summary>
        /// Convert a a grid array into a list of strings 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] Convert(char[,] source)
        {
            string[] result = new string[source.GetLength(1)];

            for (int cy = 0; cy < source.GetLength(1); cy++)
            {
                StringBuilder sb = new StringBuilder();
                for (int cx = 0; cx < source.GetLength(0); cx++)
                {
                    sb.Append(source[cx, cy]);
                }

                result[cy] = sb.ToString();
            }

            return result;
        }


        /// <summary>
        /// Rotate an array left-to-right by 90deg
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static char[,] Rotate(char[,] source)
        {
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);
            char[,] result = new char[sizeY,sizeX]; // swap dimensions

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

            for (int cc = 0; cc < StateContext.MapSize.Width; cc++)
                for (int cy = 0; cy < StateContext.MapSize.Height; cy++)
                {
                    foreach (Hint hint in hints)
                    {
                        hint.Check(cc, cy, StateContext);
                    }
                }
            return RuleResult.Success;
        }

        #region Nested type: Hint

        /// <summary>
        /// Basic Hint
        /// </summary>
        public class Hint
        {
            public string[] hint;
            public string name;
            public bool IsForward;


            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="name"></param>
            /// <param name="hint"></param>
            public Hint(string name, string[] hint)
            {
                this.name = name;
                this.hint = hint;
                this.IsForward = true;
            }

            /// <summary>
            /// Check to see if the dead map hint applies
            /// </summary>
            /// <param name="cx"></param>
            /// <param name="cy"></param>
            /// <param name="context"></param>
            public void Check(int cx, int cy, DeadMapState context)
            {
                for (int hintY = 0; hintY < hint.Length; hintY++)
                    for (int hintX = 0; hintX < hint[hintY].Length; hintX++)
                    {
                        // Check size overruns
                        if (cx + hintX >= context.MapSize.Width) return;
                        if (cy + hintY >= context.MapSize.Height) return;

                        switch (hint[hintY][hintX])
                        {
                            case ('W'):
                                if (!context.WallMap[cx + hintX, cy + hintY]) return;
                                break;
                            case ('C'):
                                if (
                                    !(context.CrateMap[cx + hintX, cy + hintY] &&
                                      !context.GoalMap[cx + hintX, cy + hintY])) return;
                                break;
                            case ('O'):
                                if (
                                    !(context.CrateMap[cx + hintX, cy + hintY])) return;
                                break;
                            case ('D'):
                                // Static deadmap or dynamic dead map
                                if (!context.StaticAnalysis.DeadMap[cx + hintX, cy + hintY] &&
                                    !context[cx + hintX, cy + hintY]) return;
                                break;
                            case ('?'):
                                break;
                            default:
                                throw new InvalidDataException("Hint char is invalid:" + hint[hintY][hintX]);
                        }
                    }

                // All criteria passed
                // Mark all crates as dead
                for (int hintY = 0; hintY < hint.Length; hintY++)
                    for (int hintX = 0; hintX < hint[hintY].Length; hintX++)
                    {
                        if (hint[hintY][hintX] == 'C') context[cx + hintX, cy + hintY] = true;
                    }

               
                // Increase the hints
               context.DeadMapAnalysis.StaticAnalysis.Controller.Stats.HintsUsed.Increment();
            }
        }

        #endregion

        private List<Hint> hints;
    }
}