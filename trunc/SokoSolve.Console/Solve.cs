using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Console
{
    public class Solve : ConsoleCommandBase
    {
        public Solve() : base("SOLVE", "Attempt to automatically solve a Sokoban puzzle", "SOLVE -lib:Library.ssx -puz:01", 2)
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

        public override ReturnCodes Execute(ConsoleCommandController controller)
        {
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(ArgLibrary);
            Puzzle puz = lib.GetPuzzleByID(ArgPuzzle);
            SolverController ctrl = new SolverController(puz.MasterMap);
            SolverResult res = ctrl.Solve();
            controller.Display(res.DebugReport.ToString());

            return ReturnCodes.OK;
        }
    }
}
