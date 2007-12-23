using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Analysis
{
    class MapAnalysis
    {

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
        /// Generate the move map for a current position
        /// </summary>
        /// <param name="boundryMap"></param>
        /// <param name="crateMap">May be null, assumes boundryMap is constraintmap</param>
        /// <param name="playerPosition"></param>
        /// <returns></returns>
        public static Bitmap GenerateMoveMap(Bitmap boundryMap, Bitmap crateMap, VectorInt playerPosition)
        {
            Bitmap constraints = boundryMap;
            if (crateMap != null) constraints = boundryMap.BitwiseOR(crateMap);

            // Flood fill from player position to remove any unreachable positions
            FloodFillStrategy floodFill = new FloodFillStrategy(constraints, playerPosition);
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
    }
}
