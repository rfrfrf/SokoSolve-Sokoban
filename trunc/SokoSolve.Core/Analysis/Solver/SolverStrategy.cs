using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Analysis.DeadMap;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Encapsulate the system of rules to be used to solve the puzzle. However, no state should be stored here.
    /// </summary>
    public class SolverStrategy : EvaluationStrategyBase<SolverNode>
    {
        /// <summary>
        /// Strong Contruction
        /// </summary>
        /// <param name="controller"></param>
        public SolverStrategy(SolverController controller) : base(new SolverItterator(controller))
        {
            this.controller = controller;
            cachedNodes = new SolverNodeCollection();
        }

        /// <summary>
        /// Provide analysis of the static (unchanging elements of the map) e.g walls, etc
        /// </summary>
        public StaticAnalysis StaticAnalysis
        {
            get { return staticAnalysis; }
        }

        /// <summary>
        /// The master/root class for the solver
        /// </summary>
        public SolverController Controller
        {
            get { return controller; }
        }

        /// <summary>
        /// Tree of all evaluation nodes
        /// </summary>
        public Tree<SolverNode> EvaluationTree
        {
            get { return evaluation;  }
        }

        /// <summary>
        /// Helper. Current node
        /// </summary>
        public SokobanMap SokobanMap
        {
            get { return controller.Map;  }
        }

        #region EvaluationStrategy

        /// <summary>
        /// Initialise the start conditions for the search. Prepare the root search node
        /// </summary>
        /// <returns>Return the root search node for init and eval</returns>
        public override SolverNode InitStartConditions()
        {
            // Recalculate the static position
            staticAnalysis = new StaticAnalysis(controller);
            staticAnalysis.Analyse();

            // Make the root node
            SolverNode root = new SolverNode();
            root.IsChildrenEvaluated = false;
            root.IsStateEvaluated = false;
            root.Weighting = rootWeighting;
            root.PlayerPosition = SokobanMap.Player;
            root.MoveDirection = Direction.None;
            root.CrateMap = staticAnalysis.InitialCrateMap;

            return root;
        }

        /// <summary>
        /// Is the node a solution
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override bool IsSolution(INode<SolverNode> node)
        {
            return node.Data.Status == SolverNodeStates.Solution || node.Data.Status == SolverNodeStates.SolutionChain;
        }

        /// <summary>
        /// Evaluate the current node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override EvalStatus EvaluateState(INode<SolverNode> node)
        {
            controller.Stats.Nodes.AddMeasure(1f);
            controller.Stats.NodesPerSecond.AddMeasure(1f);

            // Check if this is a solution
            if (node.Data.CrateMap.Equals(staticAnalysis.GoalMap))
            {
                node.Data.Status = SolverNodeStates.Solution;
                node.Data.IsStateEvaluated = true;
                node.Data.IsChildrenEvaluated = true;

                MarkEvalCompelete(node);

                // Itterate back to root, marking the path
                List<TreeNode<SolverNode>> solutionPath = node.Data.TreeNode.GetPathToRoot();
                solutionPath.ForEach(delegate(TreeNode<SolverNode> item) { item.Data.Status = SolverNodeStates.SolutionPath; });
                node.Data.Status = SolverNodeStates.Solution;

                return EvalStatus.CompleteSolution;
            }

            // Set weighting
            node.Data.Weighting = CalcWeighting(node.Data);

            // Check for deadness
            node.Data.DeadMap = staticAnalysis.DeadMapAnalysis.BuildDeadMap(node.Data.CrateMap, staticAnalysis.GoalMap, staticAnalysis.WallMap);
            node.Data.DeadMap.Name = "Dynamic Deadmap";
            if (node.Data.CrateMap.BitwiseAND(node.Data.DeadMap).Count > 0)
            {
                // Dead
                node.Data.Status = SolverNodeStates.Dead;
                node.Data.IsStateEvaluated = true;
                node.Data.IsChildrenEvaluated = true;

                controller.Stats.DeadNodes.Increment();

            
                MarkEvalCompelete(node);

                // Exit
                return EvalStatus.InProgress;
            }

            if (node.Data.MoveMap == null)
            {
                // Generate MoveMap
                node.Data.MoveMap = MapAnalysis.GenerateMoveMap(staticAnalysis.BoundryMap, node.Data.CrateMap, node.Data.PlayerPosition);
                if (node.Data.TreeNode.IsRoot)
                {
                    node.Data.MoveMap[node.Data.PlayerPosition] = true;
                }
                
                // Check Duplicate
                if (CheckDuplicate(node.Data) != null)
                {
                    controller.Stats.Duplicates.Increment();

                    node.Data.Status = SolverNodeStates.Duplicate;
                    node.Data.IsStateEvaluated = true;
                    node.Data.IsChildrenEvaluated = true;

                    MarkEvalCompelete(node);

                    // Remove duplicate from tree
                    node.Data.TreeNode.Parent.RemoveChild(node.Data);

                    // Exit
                    return EvalStatus.InProgress;
                }
                else
                {
                    cachedNodes.Add(node.Data);
                }
                
            }

            node.Data.IsStateEvaluated = true;
           

            // Check to chain match (forward and reverse)
            if (controller.CheckChainBack(node.Data))
            {
                return EvalStatus.CompleteSolution;
            }

            return EvalStatus.InProgress;
        }

        /// <summary>
        /// Build a player moves (steps) path, based on this node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Path BuildPath(SolverNode node)
        {
            List<TreeNode<SolverNode>> nodePath = node.TreeNode.GetPathToRoot();

            nodePath.Reverse();

            Path result = new Path(controller.Map.Player);
            VectorInt curentPos = VectorInt.Null; 
            for (int cc=0; cc<nodePath.Count-1; cc++)
            {
                Bitmap boundry = nodePath[cc].Data.CrateMap.BitwiseOR(staticAnalysis.BoundryMap);

                // Find all positble moves for the player
                FloodFillStrategy floodFill = new FloodFillStrategy(boundry, nodePath[cc].Data.PlayerPosition);
                Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
                eval.Evaluate(floodFill);

                VectorInt dest;
                if (cc == nodePath.Count-2)
                {
                    dest = nodePath[cc + 1].Data.PlayerPositionBeforeMove;
                }
                else
                {
                    dest = nodePath[cc + 1].Data.PlayerPositionBeforeMove;
                }
                List<LocationNode> shortestPath = floodFill.GetShortestPath(dest);
                if (shortestPath == null) throw new InvalidOperationException("New player position must be on the path. This should never happen");

                foreach (LocationNode locationNode in shortestPath)
                {
                    result.Add(locationNode.Location);
                }
            }

            // Add the last step
            result.Add(nodePath[nodePath.Count - 1].Data.PlayerPosition);

            return result;
        }

        /// <summary>
        /// Create a weighting for this node (many different strategies may be used here)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private float CalcWeighting(SolverNode node)
        {
            if (false)
            {
                //################################
                // Simple depth-last weighting
                //if (node.TreeNode != null)
                //{
                //    return 100 - node.TreeNode.Depth;
                //}
                //else
                //{
                //    return 0;
                //}    
            }
            else
            {
                //################################
                // Building weighting

                // Start with crate map weighting
                float weighting = new Matrix(node.CrateMap, 1f).Multiply(staticAnalysis.StaticForwardCrateWeighting).Total();

                // Add a movemap weighting
                if (node.MoveMap != null) weighting += node.CrateMap.BitwiseAND(staticAnalysis.GoalMap).Count * 0.3f;

                // Tree related datd
                if (node.TreeNode != null)
                {
                    // Add a depth weight
                    weighting += node.TreeNode.Depth * -0.3f;

                    // Add a parent weighting
                    weighting += node.TreeNode.Parent == null ? 0 : node.TreeNode.Parent.Data.Weighting * 0.5f;

                    // Add a children (proliferation) weighting
                    weighting += node.TreeNode.Count * 1.6f;

                    // Make sure we eval all beginning nodes
                    if (node.TreeNode.Depth < 2) weighting = rootWeighting;
                }


                //  Record in stats
                controller.Stats.WeightingAvg.AddMeasure(weighting);
                if (controller.Stats.WeightingMin.ValueTotal > weighting) controller.Stats.WeightingMin.ValueTotal = weighting;
                if (controller.Stats.WeightingMax.ValueTotal < weighting) controller.Stats.WeightingMax.ValueTotal = weighting;

                return weighting;
            }
        }

        /// <summary>
        /// Check to see if this node already exists in the search tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns>duplicate</returns>
        public SolverNode CheckDuplicate(SolverNode node)
        {
            List<SolverNode> matches = cachedNodes.GetMatch(node);
            if (matches == null || matches.Count == 0) return null;
            if (matches.Count > 1)
            {
                throw new NotImplementedException();
            }
            else
            {
                return matches[0];   
            }
        }

        /// <summary>
        /// Evaluate all child nodes
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override void EvaluateChildren(INode<SolverNode> node)
        {
            using (CodeTimerStatistic timer = new CodeTimerStatistic(controller.Stats.EvalChildTime))
            {
                int pushes = 0;
                // Find all possible pushes as result of the new move map
                Bitmap moves = node.Data.MoveMap;
                if (moves != null)
                {
                    for (int cx = 0; cx < moves.Size.Width; cx++)
                        for (int cy = 0; cy < moves.Size.Height; cy++)
                        {
                            VectorInt moveMapPos = new VectorInt(cx, cy);
                            if (moves[moveMapPos])
                            {
                                // Valid Move
                                pushes += CheckPlayerPush(node, moveMapPos, Direction.Up);
                                pushes += CheckPlayerPush(node, moveMapPos, Direction.Down);
                                pushes += CheckPlayerPush(node, moveMapPos, Direction.Left);
                                pushes += CheckPlayerPush(node, moveMapPos, Direction.Right);
                            }
                        }

                    if (pushes == 0)
                    {
                        node.Data.Status = SolverNodeStates.DeadChildren;
                    }
                }
                else
                {
                    node.Data.Status = SolverNodeStates.Dead;
                    controller.Stats.DeadNodes.Increment();
                }

                // Recalculate weighting
                CalcWeighting(node.Data);

                // Mark as complete
                node.Data.IsChildrenEvaluated = true;
                MarkEvalCompelete(node);
            }
        }

        /// <summary>
        /// Prefix nodes with F for Forward
        /// </summary>
        /// <returns></returns>
        protected override string GetNextNodeID()
        {
            return "F" + base.GetNextNodeID();
        }
      
        /// <summary>
        /// See if there is a crate in a direction to push, checking validity
        /// </summary>
        /// <param name="node"></param>
        /// <param name="moveMapPos"></param>
        /// <param name="PushDirection"></param>
        private int CheckPlayerPush(INode<SolverNode> node,VectorInt moveMapPos , Direction PushDirection)
        {
            VectorInt cratePos = moveMapPos.Offset(PushDirection);
            if (node.Data.CrateMap[cratePos])
            {
                // Crate found, is it pushable?
                VectorInt crateFreePos = cratePos.Offset(PushDirection);
                if (!node.Data.CrateMap[crateFreePos] && !staticAnalysis.BoundryMap[crateFreePos])
                {
                    VectorInt newCratePos = new VectorInt(crateFreePos);
                    // Would this crate push results in a dead puzzle
                    if (CheckIfDead(node, newCratePos))
                    {
                        // Dead
                        controller.Stats.DeadNodes.Increment();
                        return 0;
                    }
                    else
                    {
                        // Pushable crate found, have we found it already?
                        // Duplicate check involved checking the unque combination of MoveMap and CrateMap (which is done in EvaluateState(...))

                        // Add as child
                        SolverNode newChild = new SolverNode();
                        AddNodeForEval(node, newChild); // We we get the depth from the tree in CalcWeighting()
                        newChild.CrateMap = node.Data.CrateMap.Clone();

                        // Move the crates
                        newChild.CrateMap[cratePos] = false;
                        newChild.CrateMap[newCratePos] = true;
                        newChild.PlayerPosition = cratePos; // Player ends up where the crate was
                        newChild.MoveDirection = PushDirection;
                        
                        // Take the parents current weighting
                        newChild.Weighting = CalcWeighting(newChild);

                        return 1;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Check if a crate at CratePostion would result in a dead puzzle
        /// </summary>
        /// <param name="node"></param>
        /// <param name="CratePosition"></param>
        /// <returns></returns>
        private bool CheckIfDead(INode<SolverNode> node, VectorInt CratePosition)
        {
            // Check Static Dead positions
            if (staticAnalysis.DeadMap[CratePosition]) return true;

            // TODO: Check dynamic dead positions

            return false;
        }

        protected override void AddNodeForEval(INode<SolverNode> CurrentNode, SolverNode NewChild)
        {
            base.AddNodeForEval(CurrentNode, NewChild);
        }

        protected override void MarkEvalCompelete(INode<SolverNode> CurrentNode)
        {
            base.MarkEvalCompelete(CurrentNode);
        }

        protected override void RemoveRedundantNode(INode<SolverNode> CurrentNode)
        {
            // Remove from cache
            cachedNodes.Remove(CurrentNode.Data);
        }

        #endregion EvaluationStrategy

        /// <summary>
        /// Initial root node weighting
        /// </summary>
        private float rootWeighting = 100f;


        private StaticAnalysis staticAnalysis;
        private SolverController controller;
        private SolverNodeCollection cachedNodes;   
    }
}
