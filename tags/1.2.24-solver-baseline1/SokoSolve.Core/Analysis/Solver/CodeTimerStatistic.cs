using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// A need way of updating a timer without makeing the code to complex, 
    /// it implements <see cref="IDisposable"/> to leverage the using syntax
    /// </summary>
    public class CodeTimerStatistic  : IDisposable
    {
        public CodeTimerStatistic(Statistic stat)
        {
            this.stat = stat;
            timer = new CodeTimer();
            timer.Start();
        }

        private Statistic stat;
        private CodeTimer timer;

        public void Dispose()
        {
            timer.Stop();
            stat.AddMeasure(timer);
        }
    }
}
