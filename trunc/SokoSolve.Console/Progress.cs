using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Core.Analysis.Progress;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.Services;

namespace SokoSolve.Console
{
    public class Progress: ConsoleCommandBase
    {
        public Progress() : base("PROGRESS", "Attempt to automatically solve a Sokoban puzzle", "PROGRESS --todo--", 1)
        {
        }

        public string ArgSolverLibrary
        {
            get { return controller.FindArgExpected("-slib:"); }
        }


        public override ReturnCodes Execute(ConsoleCommandController controller)
        {
            // MODE: Progress + Library
            ProgressComponent comp = new ProgressComponent();
            comp.Load(ArgSolverLibrary);

            var best = comp.Items.Where(x => x.HasSolution).OrderByDescending(x => x.Rating).First();
            controller.DisplayLable("Best", best.Rating.ToString());
            controller.Display(StringHelper.Join(best.NormalisedMap, null, Environment.NewLine));

            var worst = comp.Items.Where(x => !x.HasSolution).OrderBy(x => x.Rating).First();
            controller.DisplayLable("Worst", worst.Rating.ToString());
            controller.Display(StringHelper.Join(worst.NormalisedMap, null, Environment.NewLine));

            int total = comp.Items.Count;
            var countSolutions = comp.Items.Count(x => x.HasSolution);
            controller.DisplayLable("# Solutions", string.Format("{0}/{1} = {2} %", countSolutions, total, countSolutions*100/total));
            

            return ReturnCodes.OK;
        }
    }
}
