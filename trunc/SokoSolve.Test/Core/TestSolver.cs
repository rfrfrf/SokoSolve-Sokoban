using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
    public class TestSolver : TestPuzzleBase
    {

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

        public string MakeProjectRootPath(string path)
        {
            return @"..\..\..\..\..\" + path;
        }

        public string MakePathUIContent(string path)
        {
            return MakeProjectRootPath(@"\..\SokoSolve.UI\Content\" + path);
        }

        [TestMethod]
        public void TestRecessHoverHint()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P2");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestCornerHoverHint()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P0");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions == 0, "Must not find a solution");
        }


        [TestMethod]
        public void TestAtomicSolution()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P20");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestMicrobanFailureOne()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P31");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [TestMethod]
        public void TestMicrobanFailureTwo()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P33");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }


        [TestMethod]
        public void TestMicrobanFailureReverseSolution()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
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
