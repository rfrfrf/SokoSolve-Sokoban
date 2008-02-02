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

        public static int Main(string[] args)
        {
            System.Console.WriteLine(" SokoSolve Console - " + ProgramVersion.VersionString);
            System.Console.WriteLine("============================================");
            System.Console.WriteLine("");

           if (args == null || args.Length == 0)
           {
               DisplayHelp();
               return -1;
           }

           System.Console.WriteLine("Console is not implemented.");
           return 0;
        }

        private static void DisplayHelp()
        {
            System.Console.WriteLine(@"Available commands are:
LIST - Usage: List <Library.ssx> - Provides a simple list of puzzle IDs and Names
REPORT - Usage: REPORT <*.ssx> - Generates an HTML report of puzzles with inline images
IMPORT - Usage: IMPORT <Source.xsb|*.xsb> <Target.ssx> - Import one or many sokoban puzzles
SOLVE - Usage: SOLVE <Library.ssx> <PuzzleID|StartPuzzleID-EndPuzzleID> maxTime=X maxNode=X maxItt=X maxDepth=X Save=<Yes|No> ReportXML=<Report.XML> ReportHTML=<Report.HTML>");

        }
    }
}
