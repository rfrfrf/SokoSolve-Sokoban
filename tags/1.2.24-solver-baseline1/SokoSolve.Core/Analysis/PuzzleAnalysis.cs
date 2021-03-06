using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;

namespace SokoSolve.Core.Model.Analysis
{
    class PuzzleAnalysis
    {
        /// <summary>
        /// Calculate the percentage complete for a map
        /// </summary>
        /// <param name="Map"></param>
        /// <returns>1 = 1%</returns>
        static public int PercentageComplete(SokobanMap Map)
        {
            // This is not 100% correct, as some goal positiona may not be needed
            return (Map.Count(CellStates.FloorGoalCrate)*100)/(Map.Count(Cell.Goal));
        }

        /// <summary>
        /// Calculate an automated Rating map
        /// </summary>
        /// <param name="Map"></param>
        /// <returns></returns>
        static public double CalcRating(SokobanMap Map)
        {
            return (double)Map.Count(Cell.Goal) / (double)Map.Count(Cell.Floor) * 1000;
        }
    }
}
