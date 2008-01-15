using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Provide per second rolling averages based on valueTotal
    /// </summary>
    public class StatisticRollingAverage : Statistic
    {
        public StatisticRollingAverage(string name, string stringFormat) : base(name, stringFormat)
        {
        }

        public StatisticRollingAverage(string name) : base(name)
        {
        }

        public StatisticRollingAverage()
        {
        }

        public override void SecondTick()
        {
            // Add difference
            if (history.Count > 0)
            {
                float newVal = valueTotal - lastSample;
                history.Enqueue(newVal);
                lastSample = valueTotal;
            }
            else
            {
                history.Enqueue(valueTotal);
                lastSample = valueTotal;
            }
            
            if (history.Count > 10)
            {
                history.Dequeue();
            }
        }

        float lastSample = 0;

        public float ValuePerSec
        {
            get
            {

                if (history.Count == 0) return 0;

                float total = 0;
                foreach (float f in history)
                {
                    total += f;
                }
                return total / (float)history.Count;
            }
        }

        public override SolverLabel GetDisplayData()
        {
            if (history.Count == 0) return new SolverLabel(name, "", "");

            float total = 0;
            foreach (float f in history)
            {
                total += f;
            }
            return new SolverLabel(name, string.Format("avg {0:0.0}, last {1} ", total/(float)history.Count, history.Peek(), history.Count), unitOfMeasure);
        }

        private Queue<float> history = new Queue<float>();
    }
}
