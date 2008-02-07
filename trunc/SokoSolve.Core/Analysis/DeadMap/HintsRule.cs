using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures.Evaluation.Strategy;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;

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
                    last = GeneralHelper.Rotate<HintCell>(last);

                    Hint newHint = new Hint(name + "-rot" + cc.ToString(), last);
                    if (newHint.PreProcess(staticAnalysis))
                    {
                        staticAnalysis.Controller.DebugReport.Append("Using PreProcessed Hint {0} at {1}", newHint.Name, StringHelper.Join<VectorInt>(newHint.CheckLocations, null, ", "));
                        hints.Add(newHint);
                    }
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
                        case ('F'):
                            result[cx, cy] = HintCell.Floor;
                            break;
                        case ('C'):
                            result[cx, cy] = HintCell.CrateNotGoal;
                            break;
                        case ('O'):
                            result[cx, cy] = HintCell.CrateGoalOrCrateNonGoal;
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

        
    }
}