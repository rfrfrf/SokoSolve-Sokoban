using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using NUnit.Framework;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation.Visualisation;
using SokoSolve.Core.Analysis;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using Bitmap=System.Drawing.Bitmap;
using NUnit.Framework;

namespace SokoSolve.Test.Core
{
    /// <summary>
    /// This test set should make sure that we do not go backwards. It is important to keep all the test working.
    /// </summary>
    [TestFixture]
    public class TestSolverRegression : TestPuzzleBase
    {
        

        [Test]
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

        [Test]
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

     

        [Test]
        public void TestRecessHoverHint()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P9");
            Assert.IsNotNull(puz);

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions == 0, "Puzzle is dead");
        }


        [Test]
        public void TestRecessHoverHint2()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P11");
            Assert.IsNotNull(puz);

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions == 0, "Puzzle is dead");
        }


        [Test]
        public void TestCornerHoverHint()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P7");
            Assert.IsNotNull(puz);

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions == 0, "Must not find a solution");
        }


        [Test]
        public void TestAtomicSolution()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P20");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [Test]
        public void TestMicrobanFailureOne()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P31");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

        [Test]
        public void TestMicrobanFailureTwo()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P33");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }


        [Test]
        public void TestMicrobanFailureReverseSolution()
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));
            Puzzle puz = lib.GetPuzzleByID("P35");

            int solutions = SolveForSolutions(puz.MasterMap);
            Assert.IsTrue(solutions > 0, "Must find a solution");
        }

     
    }


}
