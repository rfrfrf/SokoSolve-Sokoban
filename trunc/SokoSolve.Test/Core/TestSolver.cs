using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation.Visualisation;
using SokoSolve.Core.Analysis;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using Bitmap=System.Drawing.Bitmap;

namespace SokoSolve.Test.Core
{
    [TestClass]
    public class TestSolver
    {

        private int SolveFromString(string[] puzzle)
        {
            CodeTimer timer = new CodeTimer();
            timer.Start();

            SokobanMap map = new SokobanMap();
            map.setFromStrings(puzzle);

            PuzzleMap pMap = new PuzzleMap(null);
            pMap.Map = map;

            SolverController controller = new SolverController(pMap);

            try
            {
               
                
                SolverResult results = controller.Solve();
                

            
                

                if (!results.HasSolution)
                {
                    System.Console.WriteLine("No Solutions found.");
                    return 0;
                }

                System.Console.WriteLine("Solutions " + results.Solutions.Count);
                return results.Solutions.Count;
            }
            finally
            {
                timer.Stop();
                Console.WriteLine(controller.DebugReport.ToString(new DebugReportFormatter()));
                System.Console.WriteLine("Total Time: " + timer.Duration(1));
                System.Console.WriteLine("---");

            }
        }

        [TestMethod]
        public  void TestSimplePuzzle()
        {
            string[] puzzle = new string[]
                {
                    "~##~#####",
                    "##.##.O.#",
                    "#.##.XO.#",
                    "~##.X...#",
                    "##.XP.###",
                    "#.X..##~~",
                    "#OO.##.##",
                    "#...#~##~",
                    "#####~#~~"
                };

            int solutions = SolveFromString(puzzle);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestMediumPuzzle()
        {
            string[] puzzle = new string[]
                {
                    "~~~###~~~~~",
                    "~~##.#~####",
                    "~##..###..#",
                    "##.X......#",
                    "#...PX.#..#",
                    "###.X###..#",
                    "~~#..#OO..#",
                    "~##.##O#.##",
                    "~#......##~",
                    "~#.....##~~",
                    "~#######~"
                };

            int solutions = SolveFromString(puzzle);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestVis()
        {
            SokobanMap map = new SokobanMap();
            map.setFromStrings(new string[]
                                   {
                                       "~~~###~~~~~",
                                       "~~##.#~####",
                                       "~##..###..#",
                                       "##.X......#",
                                       "#...PX.#..#",
                                       "###.X###..#",
                                       "~~#..#OO..#",
                                       "~##.##O#.##",
                                       "~#......##~",
                                       "~#.....##~~",
                                       "~#######~~~"
                                   });

            PuzzleMap pMap = new PuzzleMap(null);
            pMap.Map = map;

            SolverController ctrl = new SolverController(pMap);
            Console.WriteLine(ctrl.Solve().ToString());

            Bitmap bm = new Bitmap(800, 600);

            TreeRenderer<SolverNode> draw = new TreeRenderer<SolverNode>(ctrl.Strategy.EvaluationTree, Graphics.FromImage(bm)); 
            draw.Draw();

            bm.Save(@"C:\junk\tree.png");
        }
    }
}
