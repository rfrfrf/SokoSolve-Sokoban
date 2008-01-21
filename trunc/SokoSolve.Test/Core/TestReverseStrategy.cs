using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Analysis.Solver.Reverse;
using SokoSolve.Core.Model;

namespace SokoSolve.Test.Core
{
    [TestClass]
    public class TestReverseStrategy
    {
        [TestMethod]
        public void TestReverseStrategyCoreSimple()
        {
            CodeTimer timer = new CodeTimer();
            timer.Start();

            try
            {
                SokobanMap map = new SokobanMap();
                map.SetFromStrings(new string[]
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

                PuzzleMap pMap = new PuzzleMap((Puzzle)null);
                pMap.Map = map;

                SolverController controller = new SolverController(pMap);
                ReverseStrategy rev = new ReverseStrategy(controller);

                Evaluator<SolverNode> eval = new Evaluator<SolverNode>();
                EvalStatus result =  eval.Evaluate(rev);


                Assert.AreEqual(EvalStatus.CompleteSolution, result, "Should find a solution");
            }
            finally
            {
                timer.Stop();
                System.Console.WriteLine("Total Time: " + timer.Duration(1));
                System.Console.WriteLine("No Solutions");

            }

        }
    }
}
