using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;

namespace SokoSolve.Console
{
    static class ProgramCommandLine
    {

        public static int Main(string[] args)
        {
            System.Console.WriteLine("SokoSolve Commandline");
            System.Console.WriteLine("=================");

            return TestSimplePuzzle();
        }

        private static int TestSimplePuzzle()
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
                return -1;
            }

            System.Console.WriteLine("No Solutions");

            return 0;
        }

        

        private static int TestMediumPuzzle()
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

            if (results == null || results.Count == 0)
            {
                System.Console.WriteLine("No Solutions found.");
                return -1;
            }

            System.Console.WriteLine("No Solutions");

            return 0;
        }
    }
}
