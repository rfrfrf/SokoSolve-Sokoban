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

            // Make the dead map
            BuildDeadMap();

            // Build Weighting matrix maps
            BuildWeightingMap();
        }

        /// <summary>
        /// Build the weighting maps
        /// </summary>
        private void BuildWeightingMap()
        {
            // All goal positions get a vaue of 10
            staticCrateWeighting = new Matrix(goalMap, 10f);

            // Corner goals get another 10
            Bitmap cornerGoals = goalMap.BitwiseAND(cornerMap);
            staticCrateWeighting = staticCrateWeighting.Add(new Matrix(cornerGoals, 10f));

            // No crates will ever get onto a dead piece, but this will make the area around a dead cell less likely to be filled by crates
            staticCrateWeighting = staticCrateWeighting.Add(new Matrix(deadMap, -2f));

            staticCrateWeighting = staticCrateWeighting.Average();

            // Player map
            staticPlayerRating = new Matrix(controller.Map.Size);

            // Leave the plaer map at zero for now
            
        }

        /// <summary>
        /// Build the 'DeadMap' for all positions on which a crate will make the puzzle unsolvable
        /// </summary>
        private void BuildDeadMap()
        {
            DeadMapAnalysis anal = new DeadMapAnalysis();
            DeadMapState deadMapResult = anal.BuildDeadMap(null, goalMap, wallMap, this);
            deadMap = deadMapResult;
            cornerMap = deadMapResult.CornerMap;
            recessMap = deadMapResult.RecessMap;
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

        public SolverBitmap WallMap
        {
            get { return wallMap; }
        }

        public SolverBitmap FloorMap
        {
            get { return floorMap; }
        }

        public SolverBitmap GoalMap
        {
            get { return goalMap; }
        }

        public SolverBitmap BoundryMap
        {
            get { return boundryMap; }
        }

        public SolverBitmap InitialCrateMap
        {
            get { return initialCrateMap; }
        }

        public SolverBitmap DeadMap
        {
            get { return deadMap; }
        }


        public SolverBitmap CornerMap
        {
            get { return cornerMap; }
        }

        public SolverBitmap RecessMap
        {
            get { return recessMap; }
        }

        public Matrix StaticCrateWeighting
        {
            get { return staticCrateWeighting; }
        }

        public Matrix StaticPlayerRating
        {
            get { return staticPlayerRating; }
        }

        #endregion

        private SolverBitmap wallMap;
        private SolverBitmap floorMap;
        private SolverBitmap goalMap;
        private SolverBitmap boundryMap;
        private SolverBitmap initialCrateMap;
        private SolverBitmap deadMap;
        private SolverBitmap cornerMap;
        private SolverBitmap recessMap;
        private Matrix staticCrateWeighting;
        private Matrix staticPlayerRating;

        private SolverController controller;
    }
}

