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
            System.Console.WriteLine(" SokoSolve Console | " + ProgramVersion.VersionString);
            System.Console.WriteLine("============================================");
            System.Console.WriteLine("");

           if (args == null || args.Length == 0)
           {
               DisplayHelp();
               return -1;
           }

           try
           {
               switch (args[0].ToUpper())
               {
                   case("REPORT") :
                       ConsoleReport report = new ConsoleReport();
                       report.Execute(args);
                       break;

                   default:
                       DisplayHelp();
                       break;
               }
               
           }
           catch (Exception ex)
           {
               System.Console.WriteLine("FAILED" + ex.Message);
               return -1;
           }
           
           return 0;
        }

        private static void DisplayHelp()
        {
            System.Console.WriteLine(@"Available commands are:
LIST - List <Library.ssx> - Provides a simple list of puzzle IDs and Names
REPORT - REPORT <*.ssx> <destination_directory> - Generates an HTML report of puzzles with inline images
IMPORT - IMPORT <Source.xsb|*.xsb> <Target.ssx> - Import one or many sokoban puzzles
SOLVE - SOLVE <Library.ssx> <PuzzleID|StartPuzzleID-EndPuzzleID> -maxTime=X -maxNode=X -maxItt=X -maxDepth=X -Save=<Yes|No> -ReportXML=<Report.XML> -ReportHTML=<Report.HTML>");

        }
    }
}
