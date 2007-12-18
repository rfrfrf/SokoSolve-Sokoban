using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.UI.Paths
{
    /// <summary>
    /// Allow automated movement
    /// </summary>
    public interface IPath
    {
        /// <summary>
        /// Get the next position in the path
        /// </summary>
        /// <returns>null implies the end of the animation</returns>
        VectorInt getNext();
    }

}
