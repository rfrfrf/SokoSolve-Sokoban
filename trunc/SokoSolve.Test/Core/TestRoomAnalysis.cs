using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;
using SokoSolve.Core.Model;

namespace SokoSolve.Test.Core
{
    [TestClass]
    public class TestRoomAnalysis : TestPuzzleBase
    {
        [TestMethod]
        public void TestRoom()
        {
            string[] puzzle = new string[]
                {
                    "~~###########",
                    "~##.....#..P#",
                    "###.X.XX#...#",
                    "#.##X....XX.#",
                    "#..#..X.#...#",
                    "######.######",
                    "#OO.OOX.#$##~",
                    "#.OO....###~~",
                    "#..OO#####~~~",
                    "#########~~~~"
                };

            SokobanMap map = new SokobanMap();
            map.SetFromStrings(puzzle);

            PuzzleMap pMap = new PuzzleMap((Puzzle)null);
            pMap.Map = map;

            SolverController solver = new SolverController(pMap);
            solver.Strategy.Init();

          
        }
    }
}
