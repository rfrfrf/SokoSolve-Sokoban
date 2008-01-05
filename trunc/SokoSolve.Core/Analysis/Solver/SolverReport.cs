using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common;

namespace SokoSolve.Core.Analysis.Solver
{
    public class SolverReport : DebugReport
    {
        public SolverReport() : base(new DebugReportHtmlFormatter())
        {
        }

        public void Build(SolverController controller)
        {
            AppendHeadingOne("SokoSolve Sokoban Solver Report");
            AppendLabel("Generated", DateTime.Now.ToLongDateString());

            Append(controller.Stats.GetDisplayData().ToHTML());
        }
    }
}
