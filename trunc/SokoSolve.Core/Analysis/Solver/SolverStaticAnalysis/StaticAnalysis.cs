using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis;
using SokoSolve.Core.Analysis.DeadMap;
using SokoSolve.Core.Model.Analysis;

namespace SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis
{

    /// <summary>
    /// Provide analysis of the static (unchanging elements of the map) e.g walls, etc
    /// </summary>
    public class StaticAnalysis
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="controller"></param>
        public StaticAnalysis(SolverController controller)
        {
            this.controller = controller;
           
        }

        /// <summary>
        /// Perform the static analysis
        /// </summary>
        public void Analyse()
        {
            // Build boundry map
            BuildBoundryMap();

            // Set the simple maps, walls, etc
            BuildAdjustedCellMaps();

            // Build the initial player move map
            BuildInitialMoveMap();

            // Make the dead map
            BuildDeadMap();

            // Room Analysis
            BuildRoomAnalysis();

            // Build Weighting matrix maps
            BuildForwardWeightingMap();
            BuildReverseWeightingMap();
        }

        /// <summary>
        /// Find all room and door with RoomAnalysis
        /// </summary>
        private void BuildRoomAnalysis()
        {
            roomAnalysis = new RoomAnalysis(controller);
            roomAnalysis.Analyse();
        }

        private void BuildInitialMoveMap()
        {
            initialMoveMap = new SolverBitmap("Initial Move Map", MapAnalysis.GenerateMoveMap(boundryMap, initialCrateMap, controller.Map.Player));
            initialMoveMap[controller.Map.Player] = true;
        }

        /// <summary>
        /// Build the reverse weighting map
        /// </summary>
        private void BuildReverseWeightingMap()
        {
            // Set goals
            staticReverseCrateWeighting = new Matrix(initialCrateMap, 10f);

            // Average
            staticReverseCrateWeighting = staticReverseCrateWeighting.Average();
        }

        /// <summary>
        /// Build the weighting maps
        /// </summary>
        private void BuildForwardWeightingMap()
        {
            // All goal weightings
            staticForwardCrateWeighting = new Matrix(goalMap,55f);

            // Corner goals get another 10
            Bitmap cornerGoals = goalMap.BitwiseAND(cornerMap);
            staticForwardCrateWeighting = staticForwardCrateWeighting.Add(new Matrix(cornerGoals, 33f));

            staticForwardCrateWeighting = staticForwardCrateWeighting.Average();

            // No crates will ever get onto a dead piece, but this will make the area around a dead cell less likely to be filled by crates
            staticForwardCrateWeighting = staticForwardCrateWeighting.Add(new Matrix(deadMap, -5f));

            // Player map
            staticPlayerRating = new Matrix(controller.Map.Size);

            // Leave the plaer map at zero for now
            
        }

        /// <summary>
        /// Build the 'DeadMap' for all positions on which a crate will make the puzzle unsolvable
        /// </summary>
        private void BuildDeadMap()
        {
            // Perform Recess Analysis
            RecessAnalysis recessAnalysis = new RecessAnalysis();
            Bitmap TMPrecessMap;

            // Corners
            cornerMap = recessAnalysis.FindCorners(this);

            // Recesses
            recessAnalysis.FindRecesses(this, out recesses, out TMPrecessMap);
            recessMap = new SolverBitmap("RecessMap", TMPrecessMap);

            // Build Dead Map
            deadMapAnalysis = new DeadMapAnalysis(this);
            DeadMapState deadMapResult = deadMapAnalysis.BuildDeadMap(null, goalMap, wallMap);
            deadMap = deadMapResult;
            
            // TODO: This is a little rough, it needs to be reworked into 'DynamicPreProcesses' or similar
            DeadMapAnalysis.LateRuleInit();
        }

        /// <summary>
        /// Build a map of all unreachable positions (void + wall, etc)
        /// </summary>
        private void BuildBoundryMap()
        {
            // Make a simple boundry map
            Bitmap simpleBoundry = controller.Map.ToBitmap(CellStates.Wall);
            simpleBoundry = simpleBoundry.BitwiseOR(controller.Map.ToBitmap(CellStates.Void));

            // Create the AccessMap, all areas the player can go.
            FloodFillStrategy result = FloodFillStrategy.Evaluate(simpleBoundry, controller.Map.Player);
            accessMap = new SolverBitmap("AccessMap", result.Result);
            accessMap[controller.Map.Player] = true;
            controller.DebugReport.Append("AccessMap:");
            controller.DebugReport.Append(accessMap.ToString());

            floorMap = accessMap;

            // Correct/Full boundry is the reverse of the access map
            boundryMap = new SolverBitmap("Boundry", accessMap.BitwiseNOT());
            controller.DebugReport.Append("BoundryMap:");
            controller.DebugReport.Append(boundryMap.ToString());

        }

        /// <summary>
        /// Set the simple maps, walls, etc
        /// </summary>
        private void BuildAdjustedCellMaps()
        {
            // Wall Map <-> boundry map
            wallMap = new SolverBitmap("BoundryWall Map", boundryMap);

            // Goal Map
            Bitmap tgoal = controller.Map.ToBitmap(CellStates.FloorGoal);
            tgoal = tgoal.BitwiseOR(controller.Map.ToBitmap(CellStates.FloorGoalCrate));
            tgoal = tgoal.BitwiseOR(controller.Map.ToBitmap(CellStates.FloorGoalPlayer));
            tgoal = tgoal.BitwiseAND(accessMap); // Must be accessable
            goalMap = new SolverBitmap("Goal Map", tgoal);

            // Crate Map
            Bitmap tcrate = controller.Map.ToBitmap(CellStates.FloorCrate);
            tcrate = tcrate.BitwiseOR(controller.Map.ToBitmap(CellStates.FloorGoalCrate));
            tcrate = tcrate.BitwiseAND(accessMap); // Must be accessable
            initialCrateMap = new SolverBitmap("Initial Crate Map", tcrate);
        }

        #region Maps

        /// <summary>
        /// All wall positions
        /// </summary>
        public SolverBitmap WallMap
        {
            get { return wallMap; }
        }

        /// <summary>
        /// All floor positions (including goals)
        /// </summary>
        public SolverBitmap FloorMap
        {
            get { return floorMap; }
        }

        /// <summary>
        /// All goal positions
        /// </summary>
        public SolverBitmap GoalMap
        {
            get { return goalMap; }
        }

        /// <summary>
        /// Boundry map (all wall and void) positions
        /// </summary>
        public SolverBitmap BoundryMap
        {
            get { return boundryMap; }
        }

        /// <summary>
        /// Initial (start) crate positions
        /// </summary>
        public SolverBitmap InitialCrateMap
        {
            get { return initialCrateMap; }
        }

        /// <summary>
        /// Player's Initial move map
        /// </summary>
        public SolverBitmap InitialMoveMap
        {
            get { return initialMoveMap; }
        
        }

        /// <summary>
        /// Deadmap, all positions on which a crate will make the puzzle unsolvable
        /// </summary>
        public SolverBitmap DeadMap
        {
            get { return deadMap; }
        }

        /// <summary>
        /// All corner positions
        /// </summary>
        public SolverBitmap CornerMap
        {
            get { return cornerMap; }
        }

        /// <summary>
        /// All recesses
        /// </summary>
        public SolverBitmap RecessMap
        {
            get { return recessMap; }
        }

        /// <summary>
        /// List of all recesses on map (goal or other)
        /// </summary>
        public List<Recess> Recesses
        {
            get { return recesses; }
        }

        /// <summary>
        /// Static forward weightings
        /// </summary>
        public Matrix StaticForwardCrateWeighting
        {
            get { return staticForwardCrateWeighting; }
        }

        /// <summary>
        /// Weighting for the player positions (not currently used)
        /// </summary>
        public Matrix StaticPlayerRating
        {
            get { return staticPlayerRating; }
        }

        /// <summary>
        /// Rule Strategy to analyse static and dynamic dead maps
        /// </summary>
        public DeadMapAnalysis DeadMapAnalysis
        {
            get { return deadMapAnalysis; }
        }

        /// <summary>
        /// Access Map, all possible positions a player can move to (with crates removed)
        /// </summary>
        public SolverBitmap AccessMap
        {
            get { return accessMap; }
        }

        /// <summary>
        /// Allow analysis of rooms and doors
        /// </summary>
        public RoomAnalysis RoomAnalysis
        {
            get { return roomAnalysis; }
        }

        /// <summary>
        /// Controller class
        /// </summary>
        public SolverController Controller
        {
            get { return controller; }
        }

        #endregion

        private SolverBitmap wallMap;
        private SolverBitmap floorMap;
        private SolverBitmap goalMap;
        private SolverBitmap boundryMap;
        private SolverBitmap accessMap;
        private SolverBitmap initialCrateMap;
        private SolverBitmap initialMoveMap;
        private SolverBitmap deadMap;
        private SolverBitmap cornerMap;
        private SolverBitmap recessMap;

        private Matrix staticForwardCrateWeighting;
        private Matrix staticReverseCrateWeighting;
        private Matrix staticPlayerRating;

        private List<Recess> recesses;
        private SolverController controller;
        private DeadMapAnalysis deadMapAnalysis;
        private RoomAnalysis roomAnalysis;
    }
}

