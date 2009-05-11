using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Console
{
    class ConsoleReport : ConsoleCommandBase
    {
        public ConsoleReport() : base("REPORT", "Generate an HTML report for a puzzle library", "SOLVE -lib:Library.ssx -puz:01", 2)
        {
        }

        public override ReturnCodes Execute(ConsoleCommandController controller)
        {
            return ReturnCodes.NotImplemented;
        }
  
       

        ///// <summary>
        ///// Perform configuration
        ///// </summary>
        ///// <param name="args"></param>
        ///// <returns>false if invalid</returns>
        //bool BuildParams(string[] args)
        //{
        //    foreach (string arg in args)
        //    {
        //        string argL = arg.ToLower();

        //        if (argL.StartsWith("-s:"))
        //        {
        //            source = arg.Remove(0, 3);
        //        }

        //        if (argL.StartsWith("-o:"))
        //        {
        //            dirOutput = arg.Remove(0, 3);
        //        }

        //        if (argL == "-index")
        //        {
        //            buildIndex = true;
        //        }

        //        if (argL == "-createcss")
        //        {
        //            createCSS = true;
        //        }
        //    }

        //    if (dirOutput == null || source == null)
        //    {
        //        // Invalid
        //        System.Console.WriteLine("*** Invalid Parameters");

        //        System.Console.WriteLine("Required:");
        //        System.Console.WriteLine(" -s:<Source File, or Source Wildcard>");
        //        System.Console.WriteLine(" -o:<Output directory>");
        //        System.Console.WriteLine("Optional:");
        //        System.Console.WriteLine(" -Index -- Build an index.html file with a table of contents");
        //        System.Console.WriteLine(" -CreateCSS -- Create a style.css file in the output directory");
        //        return false;
        //    }

        //    return true;
        //}

        //private string dirOutput;
        //private string source;
        //private bool buildIndex;
        //private bool createCSS;

        
    }
}
