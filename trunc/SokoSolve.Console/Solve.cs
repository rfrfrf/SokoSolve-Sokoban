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

        public bool ArgNonSolutionOnly
        {
            get { return controller.FindArg("-sol:", "on", false) == "on"; }
        }

        public bool ArgForceGC
        {
            get { return controller.FindArg("-forcegc:", "on", true) == "on"; }
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
                DateTime start = DateTime.Now;

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

                
                int cc = 0;
                foreach (SolverPuzzle item in comp.Items)
                {
                    if (ArgNonSolutionOnly && item.HasSolution)
                    {
                        controller.DisplayLable("Skipping solution", item.Name);
                        cc++;
                        continue;
                    }

                    TimeSpan left = TimeSpan.FromSeconds((double) (comp.Items.Count - cc)*ArgMaxTime);
                    controller.DisplayLable("Est. Time Left", left.ToString());
                    controller.DisplayLable("Time Elapsed", (DateTime.Now-start).ToString());
                    controller.Display("==========================================================================");
                    controller.Display("");
                    controller.DisplayLable("Attempt", string.Format("{0}/{1} {2}%, maxtime={3}", cc, comp.Items.Count, cc*100/comp.Items.Count, TimeSpan.FromSeconds(ArgMaxTime)));
                    controller.DisplayLable("Name", item.Name);
                    controller.DisplayLable("Rating", string.Format("{0}, size=({1}, {2})", item.Rating, item.Width, item.Height));

                    controller.Display(StringHelper.Join(item.NormalisedMap, null, Environment.NewLine));

                    SokobanMap map = new SokobanMap();
                    map.SetFromStrings(item.NormalisedMap);
                    using (SolverController ctrl = new SolverController(map))
                    {
                        ctrl.ExitConditions.MaxDepth = 1000;
                        ctrl.ExitConditions.MaxItterations = 10000000;
                        ctrl.ExitConditions.MaxTimeSecs = (float) ArgMaxTime;

                        SolverResult res = ctrl.Solve();
                        if (res.Exception != null)
                        {
                            controller.Display(res.Exception);
                        }
                        controller.DisplayLable("Result", res.Summary);

                        comp.Update(res);
                    }

                    CheckForceGC();
                    controller.Display("---------------------------------------------------------------------------");

                    if (System.Console.KeyAvailable && System.Console.ReadKey().KeyChar == 'Q')
                    {
                        controller.Display("BREAK REQUESTED... Exiting");
                        break;
                    }


                    cc++;
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

                        CheckForceGC();

                        if (System.Console.KeyAvailable && System.Console.ReadKey().KeyChar == 'Q')
                        {
                            controller.Display("BREAK REQUESTED... Exiting");
                            break;
                        }

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

        void CheckForceGC()
        {
            if (!ArgForceGC) return;

            controller.DisplayLable("Working Set Before GC", (Environment.WorkingSet / 1024 / 1024).ToString("#,##0K"));
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var status = GC.WaitForFullGCComplete();
            controller.DisplayLable("GC Wait", status.ToString());
            controller.DisplayLable("Working Set After GC", (Environment.WorkingSet / 1024 / 1024).ToString("#,##0K"));
        }

        private void SolvePuzzle(PuzzleMap puz, string Name)
        {
            using (SolverController ctrl = new SolverController(puz))
            {
                ctrl.ExitConditions.MaxDepth = 1000;
                ctrl.ExitConditions.MaxItterations = 10000000;
                ctrl.ExitConditions.MaxTimeSecs = (float) ArgMaxTime;

                SolverResult res = ctrl.Solve();
                if (res.Exception != null)
                {
                    controller.Display(res.Exception);
                }

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
}
