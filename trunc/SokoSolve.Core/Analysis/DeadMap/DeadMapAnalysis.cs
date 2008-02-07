using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation.Strategy;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;

namespace SokoSolve.Core.Analysis.DeadMap
{
    /// <summary>
    /// Analyse a map to see if it is solveable or unsolvable/dead
    /// </summary>
    public class DeadMapAnalysis : StrategyPatternBase<DeadMapState>
    {
        /// <summary>
        /// Default Constructor. Register all strategy rules
        /// </summary>
        public DeadMapAnalysis(StaticAnalysis staticAnalysis) : base("DeadMap analysis")
        {
            this.staticAnalysis = staticAnalysis;

            Register(new CornerRule(this));
            Register(new BoxRule(this));
            Register(new RecessRule(this));
            hintsRule = new HintsRule(this, staticAnalysis);
            Register(hintsRule);
            Register(new CleanUpRule(this));
        }

        /// <summary>
        /// Build a deadmap
        /// </summary>
        /// <param name="crateMap"></param>
        /// <param name="goalMap"></param>
        /// <param name="wallMap"></param>
        /// <returns></returns>
        public DeadMapState BuildDeadMap(Bitmap moveMap, Bitmap crateMap, Bitmap goalMap, Bitmap wallMap)
        {
            DeadMapState result = new DeadMapState(moveMap, crateMap, goalMap, wallMap, this);
            Evaluate(result);
            return result;
        }

        public void LateRuleInit()
        {
            hintsRule.ProcessHints();
        }

        /// <summary>
        /// Static Analysis class
        /// </summary>
        public StaticAnalysis StaticAnalysis
        {
            get
            {
                return staticAnalysis;
            }
        }

        HintsRule hintsRule;
        private SolverStrategy strategy;
        private StaticAnalysis staticAnalysis;
    }

    /// <summary>
    /// State Pattern for deadmap analysis
    /// </summary>
    public class DeadMapState : SolverBitmap
    {
        /// <summary>
        /// Strong Constructor. Start with the dead map including all walls.
        /// </summary>
        /// <param name="crateMap">map be null</param>
        /// <param name="currentMoveMap">map be null</param>
        /// <param name="goalMap"></param>
        /// <param name="wallMap"></param>
        public DeadMapState(Bitmap currentMoveMap, Bitmap crateMap, Bitmap goalMap, Bitmap wallMap, DeadMapAnalysis deadMapAnalysis) : base("Dead Map", wallMap.Size)
        {
            this.moveMap = currentMoveMap;
            this.crateMap = crateMap;
            this.goalMap = goalMap;
            this.wallMap = wallMap;
            this.deadMapAnalysis = deadMapAnalysis;

            this.mapSize = wallMap.Size;

            if (goalMap == null) throw new ArgumentNullException("goalMap");
            if (wallMap == null) throw new ArgumentNullException("goalMap");
            if (deadMapAnalysis == null) throw new ArgumentNullException("deadMapAnalysis");
        }

        /// <summary>
        /// May be null for static
        /// </summary>
        public Bitmap CrateMap
        {
            get { return crateMap; }
        }

        /// <summary>
        /// May be null for static
        /// </summary>
        public Bitmap MoveMap
        {
            get { return moveMap; }
        }

        /// <summary>
        /// Static
        /// </summary>
        public Bitmap GoalMap
        {
            get { return goalMap; }
        }

        /// <summary>
        /// Static
        /// </summary>
        public Bitmap WallMap
        {
            get { return wallMap; }
        }

        /// <summary>
        /// Generated by the rule <see cref="CornerRule"/>, all corners in the puzzle
        /// </summary>
        public SolverBitmap CornerMap
        {
            get { return cornerMap; }
            set { cornerMap = value; }
        }

        /// <summary>
        /// Generated by the rule <see cref="RecessRule"/>
        /// </summary>
        public SolverBitmap RecessMap
        {
            get { return recessMap; }
            set { recessMap = value; }
        }

        /// <summary>
        /// Is the analysis dynamic
        /// </summary>
        public bool IsDynamic
        {
            get { return crateMap != null; }
        }

       

        /// <summary>
        /// Static Analysis
        /// </summary>
        public DeadMapAnalysis DeadMapAnalysis
        {
            get { return deadMapAnalysis; }
        }

        public StaticAnalysis StaticAnalysis
        {
            get { return deadMapAnalysis.StaticAnalysis; }
        }

        /// <summary>
        /// Cache. Helper to avoid costly size lookip
        /// </summary>
        public SizeInt MapSize
        {
            get { return mapSize; }
        }

        private Bitmap moveMap;
        private Bitmap crateMap;
        private Bitmap goalMap;
        private Bitmap wallMap;
        private SolverBitmap cornerMap;
        private SolverBitmap recessMap;
        private DeadMapAnalysis deadMapAnalysis;
        private SizeInt mapSize;
    }
  
    
}
