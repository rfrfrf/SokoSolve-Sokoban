using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.Analysis;

namespace SokoSolve.Core.Analysis
{
    public class MoveAnalysis
    {

        /// <summary>
        /// Find the shortest path from the player position to a destination position
        /// </summary>
        /// <param name="Map">Map</param>
        /// <param name="Destination">Destination goal position</param>
        /// <returns>NULL if no path is found</returns>
        public static List<VectorInt> FindPlayerPath(SokobanMap Map, VectorInt Destination)
        {
            Bitmap boundry = MapAnalysis.GenerateBoundryMap(Map);
            boundry = boundry.BitwiseOR(MapAnalysis.GenerateCrateMap(Map));

            // Find all positble moves for the player
            FloodFillStrategy floodFill = new FloodFillStrategy(boundry, Map.Player);
            Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
            eval.Evaluate(floodFill);

            List<LocationNode> result = floodFill.GetShortestPath(Destination);
            if (result == null) return null;

            // Path found, convert to VectoInt
            return result.ConvertAll<VectorInt>(delegate(LocationNode item) { return item.Location; });
        }
    }
}
