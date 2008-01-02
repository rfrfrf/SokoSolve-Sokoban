using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Meta data for a statistic
    /// </summary>
    public class Statistic
    {
        public Statistic()
        {
            count = 0;
            valueTotal = 0;
            valueLast = 0;
        }

        public Statistic(string name) : this()
        {
            this.name = name;
        }


        public Statistic(string name, string unitOfMeasure): this()
        {
            this.name = name;
            this.unitOfMeasure = unitOfMeasure;
        }

        /// <summary>
        /// Short Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Comprehensive description, allows linefeeds
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Basic unit of measure eg "avg nodes", "pixels", "msecs"
        /// </summary>
        public string UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set { unitOfMeasure = value; }
        }

        public void AddMeasure(float avalue)
        {
            count++;
            valueLast = avalue;
            valueTotal += avalue;
        }

        public void AddMeasure(TimeSpan avalue)
        {
            count++;
            valueLast = (float)avalue.TotalSeconds;
            valueTotal += valueLast;
        }

        public void AddMeasure(CodeTimer timer)
        {
            count++;
            valueLast = (float)timer.Duration(count);
            valueTotal += valueLast;
        }

        public SolverLabel GetDisplayData()
        {
            // Hint: Counter
            if (count == (int)valueTotal) return new SolverLabel(name, count.ToString(), null);

            // Hint no ave
            if (count == 1)
            {
                return new SolverLabel(name, ToString(valueTotal), null);

            }

            // Hint Time
            return new SolverLabel(name, 
                    string.Format("{1} {4}, avg {2} of {3}", 
                        ToString(valueLast),
                        ToString(valueTotal),
                        ToString(valueTotal / (float)count),
                        count, 
                        unitOfMeasure),
                     null);
        }

        static string ToString(float value)
        {
            if (value == 0f) return "0";
            if (value < 1) return value.ToString("0.0000");
            return value.ToString("0");
        }

        private float valueLast;
        private float valueTotal;
        private int count;
   
        private string name;
        private string description;
        private string unitOfMeasure;
    }
}
