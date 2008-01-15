using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.UI.Paths
{
    /// <summary>
    /// The most simple of all paths: no movement
    /// </summary>
    public class StaticPath : IPath
    {
        /// <summary>
        /// Strong Constructor
        /// </summary>
        /// <param name="staticPosition"></param>
        public StaticPath(VectorInt staticPosition)
        {
            this.staticPosition = staticPosition;
            duration = -1;
            current = 0;
        }


        public StaticPath(VectorInt staticPosition, int duration)
        {
            this.staticPosition = staticPosition;
            this.duration = duration;
            this.current = 0;
        }

        public VectorInt getNext()
        {
            if (duration == -1) return staticPosition;

             if (current++ < duration)
             {
                 return staticPosition;
             }
             else
             {
                 return VectorInt.Null;
             }
        }

        private VectorInt staticPosition;
        private int duration;
        private int current;
    }
}
