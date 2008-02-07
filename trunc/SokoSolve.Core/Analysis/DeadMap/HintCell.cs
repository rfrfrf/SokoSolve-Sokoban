using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Analysis.DeadMap
{
    

    public enum HintCell
    {
        /// <summary>
        /// Encode as W
        /// </summary>
        Wall,

        /// <summary>
        /// Encode as C
        /// </summary>
        CrateNotGoal,

        /// <summary>
        /// Encode as O
        /// </summary>
        CrateGoalOrCrateNonGoal,

        /// <summary>
        /// Encode as ?
        /// </summary>
        Ignore,

        /// <summary>
        /// Encode as D
        /// </summary>
        DeadStatic,

        /// <summary>
        /// Encode as d
        /// </summary>
        DeadStaticOrDynamic,

        /// <summary>
        /// Encode as F
        /// </summary>
        Floor
    }

    
}
