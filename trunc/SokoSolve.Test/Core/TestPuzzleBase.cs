using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;

namespace SokoSolve.Test.Core
{
    public abstract class TestPuzzleBase
    {
        protected string MakeProjectRootPath(string path)
        {
            return @"..\..\..\..\..\" + path;
        }

        protected string MakePathUIContent(string path)
        {
            return MakeProjectRootPath(@"\..\SokoSolve.UI\Content\" + path);
        }


        protected SolverResult SolveFromString(string[] puzzle)
        {
            SokobanMap map = new SokobanMap();
            map.SetFromStrings(puzzle);

            PuzzleMap pMap = new PuzzleMap((Puzzle)null);
            pMap.Map = map;

            return Solve(pMap);
        }

        protected SolverResult Solve(PuzzleMap puzzle)
        {
            using (CodeTimer timer = new CodeTimer("TestSolver.Solve(...)"))
            {
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
        }

        protected int SolveForSolutions(PuzzleMap puzzle)
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
    }
}
