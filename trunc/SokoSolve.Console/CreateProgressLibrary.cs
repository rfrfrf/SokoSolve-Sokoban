using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Core.Analysis.Progress;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.Model.Services;

namespace SokoSolve.Console
{
    public class CreateProgressLibrary : ConsoleCommandBase
    {
        public CreateProgressLibrary()
            : base("PROGLIB", "Create a Progress Library", "--todo--", 2)
        {
        }

        public string ArgSolverLibrary
        {
            get { return controller.FindArgExpected("-slib:"); }
        }

        public string ArgLibrary
        {
            get { return controller.FindArgExpected("-lib:"); }
        }

        public string ArgPuzzle
        {  
            get { return controller.FindArg("-puz:"); }
        }


        public override ReturnCodes Execute(ConsoleCommandController controller)
        {
            // MODE: Progress + Library
            ProgressComponent comp = new ProgressComponent();
            comp.Load(ArgSolverLibrary);

            // MODE: Only Library
            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(ArgLibrary);

            controller.DisplayLable("Reading File", ArgLibrary);
            controller.DisplayLable("Reading Library", lib.GetDetails().Name);

            if (ArgPuzzle == null || ArgPuzzle == "*")
            {
                int cc = 0;
                comp.Add(lib);
            }
            else
            {
                Puzzle pux = lib.GetPuzzleByID(ArgPuzzle);
                comp.Add(pux.MasterMap);
            }

            comp.Sort();
            comp.Save(ArgSolverLibrary);
            controller.Display(comp.Summary);
            
            return ReturnCodes.OK;
        }
    }
}
