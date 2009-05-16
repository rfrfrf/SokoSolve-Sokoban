using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Console
{
    public class Solve : ConsoleCommandBase
    {
        private StreamWriter reportFile;

        public Solve() : base("SOLVE", "Attempt to automatically solve a Sokoban puzzle", "SOLVE -lib:Library.ssx -puz:01 report:solver.log", 2)
        {
        }

        public string ArgLibrary
        {
            get { return controller.FindArgExpected("-lib:"); }
        }

        public string ArgPuzzle
        {
            get { return controller.FindArgExpected("-puz:"); }
        }

        public string ArgReport
        {
            get { return controller.FindArg("-report:"); }
        }

        public double ArgMaxTime
        {
            get { return controller.FindArgDouble("-maxtime:", TimeSpan.FromMinutes(2).TotalSeconds, false); }
        }

        public bool IsPathWildCard(string path)
        {
            return path.Contains("*");
        }

        public override ReturnCodes Execute(ConsoleCommandController controller)
        {
            if (ArgReport != null)
            {
                reportFile = File.CreateText(ArgReport);
            }

            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(ArgLibrary);

            if (ArgPuzzle == "*")
            {
                int cc = 0;
                foreach (Puzzle puzzle in lib.Puzzles)
                {
                    cc++;
                    controller.Display("");
                    controller.DisplayLable("Attempting", string.Format("{0:000} of {1:000}", cc, lib.Puzzles.Count));
                    SolvePuzzle(puzzle, controller);
                }   
            }
            else
            {
                SolvePuzzle(lib.GetPuzzleByID(ArgPuzzle), controller);
            }

            if (reportFile != null)
            {
                reportFile.Close();
                reportFile.Dispose();
            }

            return ReturnCodes.OK;
        }

        private void SolvePuzzle(Puzzle puz, ConsoleCommandController controller)
        {
            SolverController ctrl = new SolverController(puz.MasterMap);
            ctrl.ExitConditions.MaxDepth = 1000;
            ctrl.ExitConditions.MaxItterations = 10000000;
            ctrl.ExitConditions.MaxTimeSecs = (float)ArgMaxTime;

            SolverResult res = ctrl.Solve();
            
            controller.DisplayLable("Name", string.Format("{0}, {1}", puz.PuzzleID, puz.Details.Name));
            controller.DisplayLable("Result", res.StatusString);
            controller.DisplayLable("Summary", res.Summary);
            controller.DisplayLable("Exit Conditions", ctrl.ExitConditions.ToString());
            
            if (res.HasSolution)
            {
                controller.DisplayLable("Solution", res.Solutions[0].Steps);
            }

            if (reportFile != null)
            {
                reportFile.WriteLine("###########################################################################");
                reportFile.WriteLine(string.Format("{0}, {1} ===> {2}", puz.PuzzleID, puz.Details.Name, res.Summary));
                reportFile.WriteLine("###########################################################################");
                reportFile.WriteLine(res.Info.ToString());
                reportFile.WriteLine(res.DebugReport.ToString(new DebugReportFormatter()));
                reportFile.WriteLine("---------------------------------------------------------------------------");
                reportFile.WriteLine("");
            }
        }
    }
}
