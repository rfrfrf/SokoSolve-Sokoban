using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;

namespace SokoSolve.Core.Model
{
    class PuzzleAnalysis
    {
        /// <summary>
        /// Find the shortest path from the player position to a destination position
        /// </summary>
        /// <param name="Map">Map</param>
        /// <param name="Destination">Destination goal position</param>
        /// <returns>NULL if no path is found</returns>
        public static List<VectorInt> FindPath(SokobanMap Map, VectorInt Destination)
        {
            Bitmap boundry = GenerateBoundryMap(Map);
            boundry = boundry.BitwiseOR(GenerateCrateMap(Map));

            // Find all positble moves for the player
            FloodFillStrategy floodFill = new FloodFillStrategy(boundry, Map.Player);
            Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
            eval.Evaluate(floodFill);

            List<LocationNode> result = floodFill.GetShortestPath(Destination);
            if (result == null) return null;

            // Path found, convert to VectoInt
            return result.ConvertAll<VectorInt>(delegate(LocationNode item) { return item.Location; });
        }

        /// <summary>
        /// Generate a static move map, ie. all positions for which a player could move to in the course of a puzzle
        /// </summary>
        /// <param name="Map"></param>
        /// <returns></returns>
        public static Bitmap GenerateStaticMoveMap(SokobanMap Map)
        {
            Bitmap boundry = GenerateBoundryMap(Map);

            // Flood fill from player position to remove any unreachable positions
            FloodFillStrategy floodFill = new FloodFillStrategy(boundry, Map.Player);
            Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
            eval.Evaluate(floodFill);
          
            return floodFill.Result;
        }


        /// <summary>
        /// Get a simple boundry map
        /// </summary>
        /// <param name="Map"></param>
        /// <returns></returns>
        public static Bitmap GenerateBoundryMap(SokobanMap Map)
        {
            Bitmap boundry = Map.ToBitmap(CellStates.Wall);
            boundry = boundry.BitwiseOR(Map.ToBitmap(CellStates.Void));
            return boundry;
        }


        /// <summary>
        /// Get map for the crates
        /// </summary>
        /// <param name="Map"></param>
        /// <returns></returns>
        public static Bitmap GenerateCrateMap(SokobanMap Map)
        {
            Bitmap result = Map.ToBitmap(CellStates.FloorCrate);
            result = result.BitwiseOR(Map.ToBitmap(CellStates.FloorGoalCrate));
            return result;
        }

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
