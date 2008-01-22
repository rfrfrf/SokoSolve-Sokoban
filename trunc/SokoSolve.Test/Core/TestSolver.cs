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
using SokoSolve.Core.Model.DataModel;
using Bitmap=System.Drawing.Bitmap;

namespace SokoSolve.Test.Core
{
    [TestClass]
    public class TestSolver
    {

        private SolverResult SolveFromString(string[] puzzle)
        {
            SokobanMap map = new SokobanMap();
            map.SetFromStrings(puzzle);

            PuzzleMap pMap = new PuzzleMap((Puzzle)null);
            pMap.Map = map;

            return Solve(pMap);
        }

        private SolverResult Solve(PuzzleMap puzzle)
        {
            CodeTimer timer = new CodeTimer();
            timer.Start();

            SolverController controller = new SolverController(puzzle);
            try
            {
                System.Console.WriteLine(puzzle.Map.ToString());

                SolverResult results = controller.Solve();

                if (results.Exception != null)
                {
                    // Bubble up
                    throw results.Exception;
                }
                return results;
            }
            finally
            {
                timer.Stop();
                Console.WriteLine(controller.DebugReport.ToString(new DebugReportFormatter()));
                System.Console.WriteLine("Total Time: " + timer.Duration(1));
                System.Console.WriteLine("---");
            }
        }

        private int SolveForSolutions(PuzzleMap puzzle)
        {
            SolverResult results = Solve(puzzle);
            if (!results.HasSolution)
            {
                System.Console.WriteLine("No Solutions found.");
                return 0;
            }

            System.Console.WriteLine("Solutions " + results.Solutions.Count);
            return results.Solutions.Count;
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

            int solutions = SolveFromString(puzzle).Solutions.Count;
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

            int solutions = SolveFromString(puzzle).Solutions.Count;
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }


        [TestMethod]
        public void TestRecessHoverHint()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(@"C:\Projects\Personal\SokoSolve\svn\trunc\SokoSolve.UI\Content\Libraries\Solver.ssx");
            Puzzle puz = lib.GetPuzzleByID("P2");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestCornerHoverHint()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(@"C:\Projects\Personal\SokoSolve\svn\trunc\SokoSolve.UI\Content\Libraries\Solver.ssx");
            Puzzle puz = lib.GetPuzzleByID("P0");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions == 0, "Must not find a solution");
        }


        [TestMethod]
        public void TestAtomicSolution()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(@"C:\Projects\Personal\SokoSolve\svn\trunc\SokoSolve.UI\Content\Libraries\SolverDevelopment.ssx");
            Puzzle puz = lib.GetPuzzleByID("P20");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestMicrobanFailureOne()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(@"C:\Projects\Personal\SokoSolve\svn\trunc\SokoSolve.UI\Content\Libraries\SolverDevelopment.ssx");
            Puzzle puz = lib.GetPuzzleByID("P31");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestMicrobanFailureTwo()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(@"C:\Projects\Personal\SokoSolve\svn\trunc\SokoSolve.UI\Content\Libraries\SolverDevelopment.ssx");
            Puzzle puz = lib.GetPuzzleByID("P33");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }


        [TestMethod]
        public void TestMicrobanFailureReverseSolution()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(@"C:\Projects\Personal\SokoSolve\svn\trunc\SokoSolve.UI\Content\Libraries\SolverDevelopment.ssx");
            Puzzle puz = lib.GetPuzzleByID("P35");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestInvalidRecessHoverHint()
        {
            string[] puzzle = new string[]
                {
"#############",
"#O#.P#..#...#",
"#O#XX...#.X.#",
"#O#..#.X#...#",
"#O#.X#..#.X##",
"#O#..#.X#..#~",
"#O#.X#..#.X#~",
"#OO..#.X...#~",
"#OO..#..#..#~",
"############~"
                };

            int solutions = SolveFromString(puzzle).Solutions.Count;
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

          [TestMethod]
        public void TestDead_InvalidCornerHoverHint()
        {
            string[] puzzle = new string[]
                {
"~####~~~~~~~~~~~",
"##..####~~~~~~~~",
"...OOO#~~~~~~~~",
"#...OOO#~~~~~~~~",
"#...#.##~~~~~~~~",
"#...#P.####~####",
"#####...X.###..#",
"~~~~#..##X.X...#",
"~~~###.....XX..#",
"~~~#.X..##...###",
"~~~#....######~~",
"~~~######~~~~~~~"
                    };

            SolverResult result = SolveFromString(puzzle);
            Assert.IsFalse(result.HasSolution, "Must be Dead, but has solutions");
            Assert.AreEqual(1, result.Info.TotalNodes, "Must be Dead, but has more than one eval node");
        }
    }


}
