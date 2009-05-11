using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Test.Core;

namespace SokoSolve.Console
{
    static class ProgramCommandLine
    {

        class Program
        {
            static int Main(string[] args)
            {
                ConsoleCommandController controller = new ConsoleCommandController();
                controller.Enroll("SokoSolve.Console");

                return (int)controller.Execute(args);
                
            }

        }
    }
}
