using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis;
using SokoSolve.Core.Analysis.DeadMap;
using SokoSolve.Core.Model.Analysis;

namespace SokoSolve.Core.Analysis.Solver
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
            // Set the simple maps, walls, etc
            SetSimpleMaps();


            // Build boundry map
            BuildBoundryMap();

            // Build the initial player move map
            BuildInitialMoveMap();


            

            // Make the dead map
            BuildDeadMap();
 

            // Build Weighting matrix maps
            BuildForwardWeightingMap();
            BuildReverseWeightingMap();
        }

        private void BuildInitialMoveMap()
        {
            initialMoveMap = new SolverBitmap("Initial Move Map", MapAnalysis.GenerateMoveMap(boundryMap, initialCrateMap, controller.Map.Player));
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
            // All goal positions get a vaue of 10
            staticForwardCrateWeighting = new Matrix(goalMap, 10f);

            // Corner goals get another 10
            Bitmap cornerGoals = goalMap.BitwiseAND(cornerMap);
            staticForwardCrateWeighting = staticForwardCrateWeighting.Add(new Matrix(cornerGoals, 10f));

            // No crates will ever get onto a dead piece, but this will make the area around a dead cell less likely to be filled by crates
            staticForwardCrateWeighting = staticForwardCrateWeighting.Add(new Matrix(deadMap, -2f));

            staticForwardCrateWeighting = staticForwardCrateWeighting.Average();

            // Player map
            staticPlayerRating = new Matrix(controller.Map.Size);

            // Leave the plaer map at zero for now
            
        }

        /// <summary>
        /// Build the 'DeadMap' for all positions on which a crate will make the puzzle unsolvable
        /// </summary>
        private void BuildDeadMap()
        {
            deadMapAnalysis = new DeadMapAnalysis(this);
            DeadMapState deadMapResult = deadMapAnalysis.BuildDeadMap(null, goalMap, wallMap);
            deadMap = deadMapResult;
            cornerMap = deadMapResult.CornerMap;
            recessMap = deadMapResult.RecessMap;

            DeadMapAnalysis.LateRuleInit();
        }

        /// <summary>
        /// Build a map of all unreachable positions (void + wall, etc)
        /// </summary>
        private void BuildBoundryMap()
        {
            Bitmap boundry = controller.Map.ToBitmap(CellStates.Wall);
            boundry = boundry.BitwiseOR(controller.Map.ToBitmap(CellStates.Void));

            // TODO: Convert all external floor, goal, etc to void or wall
            boundryMap = new SolverBitmap("Boundry", boundry);
        }

        /// <summary>
        /// Set the simple maps, walls, etc
        /// </summary>
        private void SetSimpleMaps()
        {
            // Wall Map
            Bitmap twall = controller.Map.ToBitmap(CellStates.Wall);
            twall = twall.BitwiseOR(controller.Map.ToBitmap(CellStates.Void));
            wallMap = new SolverBitmap("Wall Map", twall);

            // Floor Map
            floorMap = new SolverBitmap("Floor Map", MapAnalysis.GenerateFloorMap(controller.Map));

            // Goal Map
            Bitmap tgoal = controller.Map.ToBitmap(CellStates.FloorGoal);
            tgoal = tgoal.BitwiseOR(controller.Map.ToBitmap(CellStates.FloorGoalCrate));
            tgoal = tgoal.BitwiseOR(controller.Map.ToBitmap(CellStates.FloorGoalPlayer));
            goalMap = new SolverBitmap("Goal Map", tgoal);

            // Crate Map
            Bitmap tcrate = controller.Map.ToBitmap(CellStates.FloorCrate);
            tcrate = tcrate.BitwiseOR(controller.Map.ToBitmap(CellStates.FloorGoalCrate));
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
        private SolverBitmap initialCrateMap;
        private SolverBitmap initialMoveMap;
        private SolverBitmap deadMap;
        private SolverBitmap cornerMap;
        private SolverBitmap recessMap;

        private Matrix staticForwardCrateWeighting;
        private Matrix staticReverseCrateWeighting;
        private Matrix staticPlayerRating;
        

        private SolverController controller;
        private DeadMapAnalysis deadMapAnalysis;
    }
}

