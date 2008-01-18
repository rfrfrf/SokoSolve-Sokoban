using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Test.Core;

namespace SokoSolve.Console
{
    static class ProgramCommandLine
    {

        public static int Main(string[] args)
        {
            System.Console.WriteLine("SokoSolve Commandline");
            System.Console.WriteLine("=================");

           TestSolver solver = new TestSolver();
           solver.TestSimplePuzzle();

           return 0;
        }

      
        

    }
}
