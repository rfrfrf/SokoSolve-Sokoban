using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    public class Solution
    {
        private GenericDescription details;
        private string steps;


        public GenericDescription Details
        {
            get { return details; }
            set { details = value; }
        }

        public string Steps
        {
            get { return steps; }
            set { steps = value; }
        }
    }
}
