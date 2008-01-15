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

        /// <summary>
        /// Use a string.Format string to present the stat. Order is 
        /// (1) ValueLast, (2) ValueTotal, (3) ValueTotal/Count, (4) Count, (5) UnitOfMeasure
        /// </summary>
        /// <param name="StringFormat">(1) ValueLast, (2) ValueTotal, (3) ValueTotal/Count, (4) Count, (5) UnitOfMeasure</param>
        public Statistic(string name, string StringFormat): this()
        {
            this.name = name;
            this.stringFormat = StringFormat;
        }

        public float ValueLast
        {
            get { return valueLast; }
            set { valueLast = value; }
        }

        public virtual float ValueTotal
        {
            get { return valueTotal; }
            set { valueTotal = value; }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
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

        public void Increment()
        {
            count++;
            valueTotal += 1f;
        }

        public void Decrement()
        {
            count++;
            valueTotal -= 1f;
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

        /// <summary>
        /// Perform any logic needed for a second tick
        /// </summary>
        public virtual void SecondTick()
        {
            
        }


        /// <summary>
        /// Use a string.Format string to present the stat. Order is 
        /// (1) ValueLast, (2) ValueTotal, (3) ValueTotal/Count, (4) Count, (5) UnitOfMeasure
        /// </summary>
        public string StringFormat
        {
            get { return stringFormat; }
            set { stringFormat = value; }
        }

        public virtual SolverLabel GetDisplayData()
        {
            if (stringFormat != null)
            {
                return new SolverLabel(name,
                   string.Format(stringFormat,
                       valueLast,
                       valueTotal,
                       valueTotal / (float)count,
                       count,
                       unitOfMeasure),
                    null);
            }

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

        protected float valueLast;
        protected float valueTotal;
        protected int count;
   
        protected string name;
        protected string description;
        protected string unitOfMeasure;
        protected string stringFormat = "{1} {4}, avg {2} of {3}";
    }
}
