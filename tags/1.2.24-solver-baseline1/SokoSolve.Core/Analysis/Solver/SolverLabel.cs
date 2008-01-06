using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// A very simple name/value pair for automated/easy presentation and reporting
    /// </summary>
    public class SolverLabel
    {
        public SolverLabel(string name, string value, string unitOfMeasure)
        {
            this.name = name;
            this.value = value;
            this.unitOfMeasure = unitOfMeasure;
        }

        public SolverLabel(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public void SetValue(DateTime data)
        {
            value = data.ToLongDateString();
        }

        public void SetValue(TimeSpan data)
        {
            value = data.ToString();
        }

        public void SetValue(float data)
        {
            value = data.ToString("0.000");
        }

        public string UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set { unitOfMeasure = value; }
        }

        private string name;
        private string value;
        private string unitOfMeasure;
    }
}
