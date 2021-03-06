using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;

namespace SokoSolve.Core.Model.Analysis
{
    public class PuzzleAnalysis
    {
        /// <summary>
        /// Calculate the percentage complete for a map
        /// </summary>
        /// <param name="Map"></param>
        /// <returns>1 = 1%</returns>
        static public int PercentageComplete(SokobanMap Map)
        {
            int crategoals = Map.Count(CellStates.FloorGoalCrate);
            int goals = Map.Count(Cell.Goal);

            if (goals == 0) return 0;
            
            // This is not 100% correct, as some goal positiona may not be needed
            return (crategoals*100)/goals;
        }

        /// <summary>
        /// Calculate an automated Rating map
        /// </summary>
        /// <param name="Map">Sokoban Map (Initial condition)</param>
        /// <returns></returns>
        static public double CalcRating(SokobanMap Map)
        {
            double result = 0;
            result += Map.Count(Cell.Crate) * 1.0;
            result += Map.Count(CellStates.Floor) * 0.11;

            if (PercentageComplete(Map) > 50)
            {
                // Counter-intuitive map
                result *= 1.15;
            }
            return result;
        }
    }
}
