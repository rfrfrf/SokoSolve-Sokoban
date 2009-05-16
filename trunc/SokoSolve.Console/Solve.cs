using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Core.Analysis.Progress;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.Model.Services;

namespace SokoSolve.Console
{
    public class Solve : ConsoleCommandBase
    {
        private StreamWriter reportFile;

        public Solve() : base("SOLVE", "Attempt to automatically solve a Sokoban puzzle", "SOLVE -lib:Library.ssx -puz:01 report:solver.log", 2)
        {
        }

        public string ArgSolverLibrary
        {
            get { return controller.FindArg("-slib:"); }
        }

        public string ArgLibrary
        {
            get { return controller.FindArg("-lib:"); }
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
            
            if (!string.IsNullOrEmpty(ArgSolverLibrary))
            {
                // MODE: Progress + Library
                ProgressComponent comp = new ProgressComponent();
                Library lib = null;

                // It is exists load...
                if (File.Exists(ArgSolverLibrary))
                {
                    comp.Load(ArgSolverLibrary);
                }
                
                // If a library is specified, merge/add it...
                if (!string.IsNullOrEmpty(ArgLibrary))
                {
                    XmlProvider xml = new XmlProvider();
                    lib = xml.Load(ArgLibrary);
                    comp.Add(lib);
                }

                foreach (SolverPuzzle item in comp.Items)
                {
                    controller.Display("");
                    controller.DisplayLable("Name", item.Name);
                    controller.Display(StringHelper.Join(item.NormalisedMap, null, Environment.NewLine));

                    SokobanMap map = new SokobanMap();
                    map.SetFromStrings(item.NormalisedMap);
                    SolverController ctrl = new SolverController(map);
                    ctrl.ExitConditions.MaxDepth = 1000;
                    ctrl.ExitConditions.MaxItterations = 10000000;
                    ctrl.ExitConditions.MaxTimeSecs = (float)ArgMaxTime;

                    SolverResult res = ctrl.Solve();
                    comp.Update(res);
                }

                comp.Save(ArgSolverLibrary);

            }
            else if (!string.IsNullOrEmpty(ArgLibrary))
            {
                // MODE: Only Library
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
                        SolvePuzzle(puzzle.MasterMap, puzzle.GetDetails().Name);
                    }
                }
                else
                {
                    Puzzle pux = lib.GetPuzzleByID(ArgPuzzle);
                    SolvePuzzle(pux.MasterMap, pux.GetDetails().Name);
                }    
            }
            

            if (reportFile != null)
            {
                reportFile.Close();
                reportFile.Dispose();
            }

            return ReturnCodes.OK;
        }

        private void SolvePuzzle(PuzzleMap puz, string Name)
        {
            SolverController ctrl = new SolverController(puz);
            ctrl.ExitConditions.MaxDepth = 1000;
            ctrl.ExitConditions.MaxItterations = 10000000;
            ctrl.ExitConditions.MaxTimeSecs = (float)ArgMaxTime;

            SolverResult res = ctrl.Solve();
            
            controller.DisplayLable("Name", Name);
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
                reportFile.WriteLine(string.Format("{0} ===> {1}", Name, res.Summary));
                reportFile.WriteLine("###########################################################################");
                reportFile.WriteLine(res.Info.ToString());
                reportFile.WriteLine(res.DebugReport.ToString(new DebugReportFormatter()));
                reportFile.WriteLine("---------------------------------------------------------------------------");
                reportFile.WriteLine("");
            }
        }
    }
}
