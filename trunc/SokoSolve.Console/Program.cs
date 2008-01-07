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
            //return TestMediumPuzzle();
        }

        static int TestPuzzleByStrings(string[] mapstrings)
        {
            CodeTimer timer = new CodeTimer();
            timer.Start();

            try
            {
                SokobanMap map = new SokobanMap();
                map.setFromStrings(mapstrings);

                PuzzleMap pMap = new PuzzleMap(null);
                pMap.Map = map;

                SolverAPI api = new SolverAPI();
                List<INode<SolverNode>> results = api.Solve(pMap);

                if (results == null || results.Count == 0)
                {
                    System.Console.WriteLine("No Solutions found.");
                    return -1;
                }

                System.Console.WriteLine("Solution found.");
            }
            finally
            {
                timer.Stop();
                System.Console.WriteLine("Total Time: " + timer.Duration(1));
                System.Console.WriteLine("Done");

            }


            return 0;
        }

        private static int TestMediumPuzzle()
        {
            return TestPuzzleByStrings(new string[]
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
        }


        private static int TestSimplePuzzle()
        {
            return TestPuzzleByStrings(new string[]
		                                                      {
         "~##~#####",
          "##.##.O.#",
          "#.##.XO.#",
          "~##.X...#",
          "##.XP.###",
          "#.X..##~~",
          "#OO.##.##",
          "#...#~##~",
          "#####~#~~"});
        }

        

    }
}
