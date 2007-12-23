using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Analysis
{
    public class CrateAnalysis
    {
        /// <summary>
        /// Given a map (and player position), find the move tree to push the target crate to a goal position
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="CratePosition"></param>
        /// <param name="GoalCratePosition"></param>
        /// <returns></returns>
        static public object FindCratePath(SokobanMap Map, VectorInt CratePosition, VectorInt GoalCratePosition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// For a given map and a specific crate, find all possition position into which the crate can be pushed without affecting another crate.
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="CratePosition"></param>
        /// <returns></returns>
        static public Bitmap BuildCrateMoveMap(SokobanMap Map, VectorInt CratePosition)
        {
            CellStates crateCell = Map[CratePosition];
            if (crateCell != CellStates.FloorCrate && crateCell != CellStates.FloorGoalCrate) throw new ArgumentException("CratePosition is not a crate");

            Evaluator<CrateMapNode> eval = new Evaluator<CrateMapNode>();
            BuildCrateMapImplementation strat = new BuildCrateMapImplementation(Map, CratePosition);
            eval.Evaluate(strat);

            return strat.GenerateResult();
        }

        
        /// <summary>
        /// Tree evaluation to search for all possible legal crate moves
        /// </summary>
        class BuildCrateMapImplementation : EvaluationStrategyBase<CrateMapNode>
        {
            public BuildCrateMapImplementation(SokobanMap InitialConditions, VectorInt CrateStartLocation) : base(new DepthLastItterator<CrateMapNode>())
            {
                crateLocationStart = CrateStartLocation;
                initialConditions = InitialConditions;
                constraintMap = null;
                crateMap = null;
            }

            public Bitmap GenerateResult()
            {
                return crateMap;
            }

            /// <summary>
            /// Initialise the start conditions for the search. Prepare the root search node
            /// </summary>
            /// <returns>Return the root search node for init and eval</returns>
            public override CrateMapNode InitStartConditions()
            {
                // Generate the constraint Map
                constraintMap = initialConditions.ToBitmap(CellStates.Void);
                constraintMap = constraintMap.BitwiseOR(initialConditions.ToBitmap(CellStates.Wall));
                constraintMap = constraintMap.BitwiseOR(initialConditions.ToBitmap(CellStates.FloorCrate));
                constraintMap = constraintMap.BitwiseOR(initialConditions.ToBitmap(CellStates.FloorGoalCrate));

                // Remove the target crate
                constraintMap[crateLocationStart] = false;

                crateMap = new Bitmap(constraintMap.Size);

                CrateMapNode root = new CrateMapNode("$root$");
                root.PlayerPosition = initialConditions.Player;
                root.CratePosition = crateLocationStart;
                root.PlayerMoveMap = null;

                // TODO: Validation: Check that CrateLocation and PlayerLocation are next to each other
                return root;
            }

            /// <summary>
            /// Is the node a solution
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public override bool IsSolution(INode<CrateMapNode> node)
            {
                return false; // No solutions generated at the node level
            }

            /// <summary>
            /// Evaluate the current node
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public override EvalStatus EvaluateState(INode<CrateMapNode> node)
            {
                // Generate MoveMap
                node.Data.PlayerMoveMap = MapAnalysis.GenerateMoveMap(constraintMap, null, node.Data.PlayerPosition);
                node.Data.IsStateEvaluated = true;

                // Make the crate position as taken
                crateMap[node.Data.CratePosition] = true;
               
                return EvalStatus.InProgress;
            }

            /// <summary>
            /// Evaluate all child nodes.
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public override void EvaluateChildren(INode<CrateMapNode> node)
            {
                // Given the new player position for this node
                // Find which pushes are valid (i.e. are within the constraintMap)

                bool canUp = CheckPush(node, Direction.Up);
                bool canDown = CheckPush(node, Direction.Down);
                bool canLeft = CheckPush(node, Direction.Left);
                bool canRight = CheckPush(node, Direction.Right);

                node.Data.IsChildrenEvaluated = true;

                MarkEvalCompelete(node);
            }

           

            private bool CheckPush(INode<CrateMapNode> node, Direction direction)
            {
                VectorInt newCrateLocation = node.Data.CratePosition.Offset(direction);

                // Check if this violates the ConstraintMap
                if (!constraintMap[newCrateLocation])
                {
                    // Check if we have this position already
                    if (!crateMap[newCrateLocation])
                    {
                        // Can we actually push it there?
                        // Ie can the player move to the old crate position?
                        VectorInt requiredPlayerPushPosition = node.Data.CratePosition.Offset(VectorInt.Reverse(direction));
                        if (node.Data.PlayerMoveMap[requiredPlayerPushPosition])
                        {
                            // Make the crate position as taken
                            crateMap[newCrateLocation] = true;

                            // We have a valid child
                            CrateMapNode child = new CrateMapNode(GetNextNodeID());
                            child.CratePosition = newCrateLocation;
                            child.PlayerPosition = node.Data.CratePosition; // Player takes the place of the crate
                            AddNodeForEval(node, child);
                            return true;    
                        }
                        
                    }
                }

                return false;
            }


            private Bitmap constraintMap;
            private Bitmap crateMap;
            private VectorInt crateLocationStart;
            private SokobanMap initialConditions;
        }

        class CrateMapNode : IEvaluationNode
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="nodeID"></param>
            public CrateMapNode(string nodeID)
            { 
                this.nodeID = nodeID;
            }

            #region IManagedNodeData Members

            public string NodeID
            {
                get { return nodeID; }
            }

            public bool IsStateEvaluated
            {
                get { return isStateEvaluated; }
                set { isStateEvaluated = value; }
            }

            public bool IsChildrenEvaluated
            {
                get { return isChildrenEvaluated; }
                set { isChildrenEvaluated = value; }
            }

            #endregion

            public VectorInt PlayerPosition;
            public VectorInt CratePosition;
            public Bitmap PlayerMoveMap; 


            private string nodeID;
            private bool isStateEvaluated;
            private bool isChildrenEvaluated;
        }

    }
}
