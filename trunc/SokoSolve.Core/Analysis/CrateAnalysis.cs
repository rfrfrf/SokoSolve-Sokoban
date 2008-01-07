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

        public class ShortestCratePath
        {
            public Path CratePath;
            public Path PlayerPath;
        }

        /// <summary>
        /// Given a map (and player position), find the move tree to push the target crate to a goal position
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="CratePosition"></param>
        /// <param name="GoalCratePosition"></param>
        /// <returns></returns>
        static public ShortestCratePath FindCratePath(SokobanMap Map, VectorInt CratePosition, VectorInt GoalCratePosition)
        {
            CellStates crateCell = Map[CratePosition];
            if (crateCell != CellStates.FloorCrate && crateCell != CellStates.FloorGoalCrate) throw new ArgumentException("CratePosition is not a crate");

            Evaluator<CrateMapNode> eval = new Evaluator<CrateMapNode>();
            BuildCrateMapImplementation strat = new BuildCrateMapImplementation(Map, CratePosition);
            eval.Evaluate(strat);

            return strat.FindShortestCratePath(GoalCratePosition);
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
                constraintMapStatic = null;
                crateMap = null;
            }

            /// <summary>
            /// Return the CrateMap as a result
            /// </summary>
            /// <returns></returns>
            public Bitmap GenerateResult()
            {
                return crateMap;
            }

            public ShortestCratePath FindShortestCratePath(VectorInt CratePostition)
            {
                if (crateMap[CratePostition] == false) return null;

                TreeNode<CrateMapNode> goal = evaluation.Root.Find(
                    delegate(TreeNode<CrateMapNode> item)
                    {
                        return item.Data.CratePosition == CratePostition;
                    }, int.MaxValue);
            
                if (goal != null)
                {
                    List<TreeNode<CrateMapNode>> pathToRoot = goal.GetPathToRoot();
                    pathToRoot.Reverse();

                    ShortestCratePath result = new ShortestCratePath();
                    
                    // Build crate path, this is easy as each node is a crate push
                    result.CratePath = new Path(crateLocationStart);
                    foreach (TreeNode<CrateMapNode> node in pathToRoot)
                    {
                        result.CratePath.Add(node.Data.CratePosition);
                    }

                    // Build the player path, this is not so each, as we have to expand the move map
                    result.PlayerPath = new Path(pathToRoot[0].Data.PlayerPosition);

                    // Walk through all the crate pushes, filling in the actual player moved in between
                    for (int cc=0; cc<pathToRoot.Count; cc++)
                    {
                        TreeNode<CrateMapNode> cratePushNode = pathToRoot[cc];
                        Bitmap boundry = cratePushNode.Data.PlayerMoveMap.BitwiseNOT();
                        boundry[pathToRoot[cc].Data.CratePosition] = true;

                        // Find all positble moves for the player
                        FloodFillStrategy floodFill = new FloodFillStrategy(boundry, cratePushNode.Data.PlayerPosition);
                        Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
                        eval.Evaluate(floodFill);

                        VectorInt playerPushPosition = VectorInt.Empty;
                        if (cc < pathToRoot.Count-1)
                        {
                            playerPushPosition = pathToRoot[cc + 1].Data.PlayerPosition.Offset(VectorInt.Reverse(pathToRoot[cc + 1].Data.Direction));
                            List<LocationNode> movepath = floodFill.GetShortestPath(playerPushPosition);
                            if (movepath != null)
                            {
                                foreach (LocationNode locationNode in movepath)
                                {
                                    result.PlayerPath.Add(locationNode.Location);
                                }
                            }
                        }
                        else
                        {
                            // Final move
                            result.PlayerPath.Add(VectorInt.GetDirection(pathToRoot[cc].Data.PlayerPosition, pathToRoot[cc].Data.CratePosition));
                        }
                    }
                    return result;
                }

                return null;
            }

            /// <summary>
            /// Initialise the start conditions for the search. Prepare the root search node
            /// </summary>
            /// <returns>Return the root search node for init and eval</returns>
            public override CrateMapNode InitStartConditions()
            {
                // Generate the constraint Map
                constraintMapStatic = initialConditions.ToBitmap(CellStates.Void);
                constraintMapStatic = constraintMapStatic.BitwiseOR(initialConditions.ToBitmap(CellStates.Wall));
                constraintMapStatic = constraintMapStatic.BitwiseOR(initialConditions.ToBitmap(CellStates.FloorCrate));
                constraintMapStatic = constraintMapStatic.BitwiseOR(initialConditions.ToBitmap(CellStates.FloorGoalCrate));

                // Remove the target crate
                constraintMapStatic[crateLocationStart] = false;

                crateMap = new Bitmap(constraintMapStatic.Size);

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
                // Crate the new constraint map
                Bitmap constraintMap = new Bitmap(constraintMapStatic);
                constraintMap[node.Data.CratePosition] = true;

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

            /// <summary>
            ///  <see cref="EvaluateChildren"/> Check if a crate can be pushed (resulting in a new sub-node)
            /// </summary>
            /// <param name="node"></param>
            /// <param name="direction"></param>
            /// <returns></returns>
            private bool CheckPush(INode<CrateMapNode> node, Direction direction)
            {
                VectorInt newCrateLocation = node.Data.CratePosition.Offset(direction);

                // Check if this violates the ConstraintMap
                if (!constraintMapStatic[newCrateLocation])
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
                            child.Direction = direction;
                            child.PlayerPosition = node.Data.CratePosition; // Player takes the place of the crate
                            AddNodeForEval(node, child);
                            return true;    
                        }
                    }
                }
                return false;
            }

            private Bitmap constraintMapStatic;
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
                set { nodeID = value; }
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
            public Direction Direction;

            private string nodeID;
            private bool isStateEvaluated;
            private bool isChildrenEvaluated;
        }

    }
}
