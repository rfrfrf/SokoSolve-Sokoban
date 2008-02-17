using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Console
{
    class ConsoleReport
    {
        /// <summary>
        /// Build a XHTML report(s) (and optionally a index table)
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// SokoSolve.Console.exe REPORT -s:C:\TMP\*.ssx -o:C:\HTML -index -CreateCSS
        /// </remarks>
        public void Execute(string[] args)
        {
            if (!BuildParams(args)) return;

            
        }

        /// <summary>
        /// Perform configuration
        /// </summary>
        /// <param name="args"></param>
        /// <returns>false if invalid</returns>
        bool BuildParams(string[] args)
        {
            foreach (string arg in args)
            {
                string argL = arg.ToLower();

                if (argL.StartsWith("-s:"))
                {
                    source = arg.Remove(0, 3);
                }

                if (argL.StartsWith("-o:"))
                {
                    dirOutput = arg.Remove(0, 3);
                }

                if (argL == "-index")
                {
                    buildIndex = true;
                }

                if (argL == "-createcss")
                {
                    createCSS = true;
                }
            }

            if (dirOutput == null || source == null)
            {
                // Invalid
                System.Console.WriteLine("*** Invalid Parameters");

                System.Console.WriteLine("Required:");
                System.Console.WriteLine(" -s:<Source File, or Source Wildcard>");
                System.Console.WriteLine(" -o:<Output directory>");
                System.Console.WriteLine("Optional:");
                System.Console.WriteLine(" -Index -- Build an index.html file with a table of contents");
                System.Console.WriteLine(" -CreateCSS -- Create a style.css file in the output directory");
                return false;
            }

            return true;
        }

        private string dirOutput;
        private string source;
        private bool buildIndex;
        private bool createCSS;

    }
}
