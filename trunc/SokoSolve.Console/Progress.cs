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

            int total = comp.Items.Count;
            var countSolutions = comp.Items.Count(x => x.HasSolution);
            controller.DisplayLable("# Solutions", string.Format("{0}/{1} = {2} %", countSolutions, total, countSolutions * 100 / total));

            controller.DisplayHeader("TOP 10", 1);
            var best = comp.Items.Where(x => x.HasSolution).OrderByDescending(x => x.Rating).Take(10);
            foreach (SolverPuzzle puzzle in best)
            {
                controller.DisplayLable("Best", puzzle.Rating.ToString());
                controller.Display(StringHelper.Join(puzzle.NormalisedMap, null, Environment.NewLine));
            }
          

            controller.DisplayHeader("WORST 10", 1);
            var worst = comp.Items.Where(x => !x.HasSolution).OrderBy(x => x.Rating).Take(10);
            foreach (SolverPuzzle puzzle in worst)
            {
                controller.DisplayLable("Worst", string.Format("{0}, longest attempt={1}", puzzle.Rating, puzzle.Attempts.Items.Max(x => x.ElapsedTime)));
                controller.Display(StringHelper.Join(puzzle.NormalisedMap, null, Environment.NewLine));
            }

           
            return ReturnCodes.OK;
        }
    }
}
