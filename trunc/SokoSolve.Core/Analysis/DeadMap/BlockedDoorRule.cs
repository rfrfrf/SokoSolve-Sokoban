using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation.Strategy;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// Block Door Rule: Check the all room entrances are blocked (and non goal) and the player is outside the room
    /// </summary>
    class BlockedDoorRule : StrategyRule<DeadMapState>
    {
        public BlockedDoorRule(StrategyPatternBase<DeadMapState> strategy) : base(strategy, "Block Door Rule: Check the all room entrances are blocked (and non goal) and the player is outside the room")
        {

        }

        /// <summary>
        /// Evaluare the rule returning the result (maby also enrich state)
        /// </summary>
        /// <param name="StateContext">state pattern</param>
        /// <returns>Eval result</returns>
        public override RuleResult Evaluate(DeadMapState StateContext)
        {
            if (!StateContext.IsDynamic) return RuleResult.Skipped;

            // TODO: This need only be checked if the crates have changed since parent node

            // Check each room doors
            foreach (Room room in StateContext.StaticAnalysis.RoomAnalysis.Rooms)
            {
                EvaluateRoom(room, StateContext);
            }
            return RuleResult.Success;
        }

        /// <summary>
        /// Check an individual room
        /// </summary>
        /// <param name="room"></param>
        /// <param name="context"></param>
        private void EvaluateRoom(Room room, DeadMapState context)
        {
            // Is the player already in the room
            foreach (VectorInt roomCell in room.TruePositions)
            {
                if (context.DynamicNode.PlayerPosition == roomCell) return;
            }
            foreach (Door door in room.Doors)
            {
                if (context.DynamicNode.PlayerPosition == door.Position) return;
            }


            foreach (Door door in room.Doors)
            {
                if (door.Type != Door.Types.Basic) return; // TODO: Support other door types

                Direction roomDir = room.GetRoomDirection(door);
                VectorInt off0 = door.Position;
                VectorInt off1 = off0.Offset(roomDir);
                VectorInt off2 = off1.Offset(roomDir);
                
                if (context.CrateMap[off0] && context.CrateMap[off1]) continue; // Door is blocked..
                if (context.CrateMap[off1] && context.CrateMap[off2]) continue; // Door is blocked..
                if (context.CrateMap[off0] && context.CrateMap[off2]) continue; // Door is blocked..

                // Not blocked
                return;
                
            }

            // All doors are blocked
            foreach (VectorInt roomPos in room.TruePositions)
            {
                context[roomPos] = true;
            }
        }
    }
}
