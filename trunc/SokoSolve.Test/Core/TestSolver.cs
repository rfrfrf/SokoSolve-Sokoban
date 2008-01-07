using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public  void TestSimplePuzzle()
        {
            CodeTimer timer = new CodeTimer();
            timer.Start();

            try
            {
                SokobanMap map = new SokobanMap();
                map.setFromStrings(new string[]
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
		                           });

                PuzzleMap pMap = new PuzzleMap(null);
                pMap.Map = map;

                SolverAPI api = new SolverAPI();
                List<INode<SolverNode>> results = api.Solve(pMap);

                if (results == null || results.Count == 0)
                {
                    System.Console.WriteLine("No Solutions found.");
                    return;
                }

                System.Console.WriteLine("No Solutions");
            }
            finally
            {
                timer.Stop();
                System.Console.WriteLine("Total Time: " + timer.Duration(1));
                System.Console.WriteLine("No Solutions");

            }

        }

        [TestMethod]
        public void TestMediumPuzzle()
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

            SolverAPI api = new SolverAPI();
            List<INode<SolverNode>> results = api.Solve(pMap);

            Assert.IsNotNull(results, "No solution found");
            Assert.IsTrue(results.Count > 0, "No Solutions");

            Debug.WriteLine(map.ToString());
            


            Debug.WriteLine("done."); // Bug (last line does not display correctly)
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
