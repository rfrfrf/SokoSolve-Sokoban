using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using SokoSolve.Common;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;

namespace SokoSolve.Test.Core
{
    public abstract class TestPuzzleBase
    {
        protected string MakeProjectRootPath(string path)
        {
            return @"..\..\..\" + path;
        }

        protected string MakePathUIContent(string path)
        {
            return MakeProjectRootPath(@"\SokoSolve.UI\Content\" + path);
        }


        protected SolverResult SolveFromString(string[] puzzle)
        {
            SokobanMap map = new SokobanMap();
            map.SetFromStrings(puzzle);

            return Solve(map);
        }

        protected SolverResult Solve(SokobanMap puzzle)
        {
            using (CodeTimer timer = new CodeTimer("TestSolver.Solve(...)"))
            {
                SolverController controller = new SolverController(puzzle);
                try
                {
                    System.Console.WriteLine(puzzle.ToString());

                    SolverResult results = controller.Solve();
                    if (results.Exception != null)
                    {
                        // Bubble up
                        throw new Exception("Solver Failed Iternally", results.Exception);
                    }

                    if (results.Status == SolverResult.CalculationResult.SolutionFound)
                    {
                        // Check that 
                        Assert.IsTrue(results.HasSolution, "State says a solution was found, but none are listed in solutions list");
                    }

                    if (results.HasSolution)
                    {
                        int cc = 0;
                        foreach (Solution solution in results.Solutions)
                        {
                            string testRes = "Error";
                            Assert.IsTrue(solution.Test(puzzle, out testRes), testRes);
                            Console.WriteLine("Testing solution: {0} - {1}", cc, testRes);
                            cc++;
                        }
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
            SolverResult results = Solve(puzzle.Map);
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
