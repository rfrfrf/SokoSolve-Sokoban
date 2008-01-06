using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;

namespace SokoSolve.UI
{
    static class ProgramCommandLine
    {
        public static int Run(string[] args)
        {
            Console.WriteLine("SokoSolve Commandline");
            Console.WriteLine("=================");

            return TestSimplePuzzle();
        }

        private static int TestSimplePuzzle()
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

            if (results == null || results.Count== 0)
            {
                Console.WriteLine("No Solutions found.");
                return -1;
            }


            return 0;
        }
    }
}
