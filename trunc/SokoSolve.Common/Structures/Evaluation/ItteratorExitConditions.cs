using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    /// <summary>
    /// A simple class that sets bounds on a itterator
    /// </summary>
    public class ItteratorExitConditions
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ItteratorExitConditions()
        {
            maxDepth = 1000;
            maxItterations = MaxDepth*8;
            maxTimeSecs = 10; // sec
        }

        /// <summary>
        ///  Limit the maximum search depth
        /// </summary>
        public int MaxDepth
        {
            get { return maxDepth; }
            set { maxDepth = value; }
        }

        /// <summary>
        /// Limit the number of search itterations.
        /// </summary>
        public int MaxItterations
        {
            get { return maxItterations; }
            set { maxItterations = value; }
        }

        /// <summary>
        /// Limit the evalution duration by time
        /// </summary>
        public float  MaxTimeSecs
        {
            get { return maxTimeSecs; }
            set { maxTimeSecs = value; }
        }

        public override string ToString()
        {
            return string.Format("MaxDepth: {0}, MaxItterations: {1}, MaxTimeSecs: {2}", MaxDepth, maxItterations,
                                 TimeSpan.FromSeconds(maxTimeSecs));
        }

        protected int maxDepth;
        protected int maxItterations;
        protected float maxTimeSecs;
    }
}
