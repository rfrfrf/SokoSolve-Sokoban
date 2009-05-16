using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;

namespace SokoSolve.Core.Analysis.Solver.Reverse
{
    /// <summary>
    /// Work backwards from a goal position to the initial crate position
    /// </summary>
    public class ReverseStrategy : EvaluationStrategyBase<SolverNode>
    {
        /// <summary>
        /// Strong Constructor
        /// </summary>
        /// <param name="controller"></param>
        public ReverseStrategy(SolverController controller) : base(new SolverItterator(controller))
        {
            this.controller = controller;
            cachedNodes = new SolverNodeCollection();
        }

        /// <summary>
        /// Evaluation tree of search positions
        /// </summary>
        public Tree<SolverNode> EvaluationTree
        {
            get { return base.evaluation; }
        }

        /// <summary>
        /// Provide access to static analysis
        /// </summary>
        private StaticAnalysis StaticAnalysis
        {
            get { return controller.StaticAnalysis;  }
        }

        /// <summary>
        /// Initialise the start conditions for the search. Prepare the root search node
        /// </summary>
        /// <returns>Return the root search node for init and eval</returns>
        public override SolverNode InitStartConditions()
        {
            if (StaticAnalysis == null) throw new NoNullAllowedException("StaticAnalysis");
            SolverNode endposition = new SolverNode();
            endposition.NodeID = "R0";

            // Crate map is the goal map
            endposition.CrateMap = StaticAnalysis.GoalMap;

            // We do not know that the player position is at the end
            //endposition.PlayerPosition = VectorInt.Empty; 

            // Similarly, we do not know what the final move map is.
            // We need to merge all posible end move maps.
            endposition.MoveMap = new Bitmap(endposition.CrateMap.Size);
            foreach (VectorInt crate in endposition.CrateMap.TruePositions)
            {
                CheckPosiblePlayerEndPosition(endposition, crate, Direction.Up);
                CheckPosiblePlayerEndPosition(endposition, crate, Direction.Down);
                CheckPosiblePlayerEndPosition(endposition, crate, Direction.Left);
                CheckPosiblePlayerEndPosition(endposition, crate, Direction.Right);
            }

            endposition.Weighting = 100;

            return endposition;
        }

        /// <summary>
        /// Generate the end position for the player (by merging all possition end positions)
        /// </summary>
        /// <param name="endposition"></param>
        /// <param name="crate"></param>
        /// <param name="pullDirection"></param>
        private void CheckPosiblePlayerEndPosition(SolverNode endposition, VectorInt crate, Direction pullDirection)
        {
            VectorInt newCratePosition = crate.Offset(pullDirection);
            VectorInt newPlayerPosition = crate.Offset(pullDirection).Offset(pullDirection);
            VectorInt oldPlayerPosition = newCratePosition;


            // Bounds check
            if (newCratePosition.X < 0 || newCratePosition.X >= endposition.CrateMap.Size.X ||
                newCratePosition.Y < 0 || newCratePosition.Y >= endposition.CrateMap.Size.Y)
            {
                return;
            }

            // Bounds check
            if (newPlayerPosition.X < 0 || newPlayerPosition.X >= endposition.CrateMap.Size.X ||
              newPlayerPosition.Y < 0 || newPlayerPosition.Y >= endposition.CrateMap.Size.Y)
            {
                return;
            }

            // Already a move map
            if (endposition.MoveMap[oldPlayerPosition])
            {
                return;
            }

            // Must be a crate to pull
            if (!endposition.CrateMap[crate]) return;

            // Player must move into a space
            if (StaticAnalysis.BoundryMap[newPlayerPosition]) return;
            if (endposition.CrateMap[newPlayerPosition]) return;

            // We have a valid player start position
            Bitmap partialMoveMap = MapAnalysis.GenerateMoveMap(StaticAnalysis.BoundryMap, endposition.CrateMap, oldPlayerPosition);
            endposition.MoveMap = endposition.MoveMap.BitwiseOR(partialMoveMap);
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
            controller.Stats.NodesRev.AddMeasure(1f);
            controller.Stats.NodesPerSecond.AddMeasure(1f);

            // Check if this is a solution
            if (node.Data.CrateMap.Equals(StaticAnalysis.InitialCrateMap))
            {
                // Player position must be inside the initial move map
                if (StaticAnalysis.InitialMoveMap[node.Data.PlayerPosition])
                {
                    controller.DebugReport.Append("Node {0}: Found a solution using only the reverse solver.", node.Data.NodeID);

                    // Solution found
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
                else
                {
                    controller.DebugReport.Append("Node {0}: Found a near solution using only the reverse solver, but the player position was incorrect", node.Data.NodeID);
                }
            }

            // Build the players move map
            if (node.Data.MoveMap == null)
            {
                // Generate MoveMap
                node.Data.MoveMap = MapAnalysis.GenerateMoveMap(StaticAnalysis.BoundryMap, node.Data.CrateMap, node.Data.PlayerPosition);
            }

            node.Data.IsStateEvaluated = true;

            

            // Check to chain match (forward and reverse)
            if (controller.CheckChainForward(node.Data))
            {
                return EvalStatus.CompleteSolution;
            }

            // Check for duplicates
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

                node.Data.IsStateEvaluated = true;
                return EvalStatus.InProgress;
            }
        }

        /// <summary>
        /// Evaluate all child nodes
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override void EvaluateChildren(INode<SolverNode> node)
        {
            SolverNode data = node.Data;
            SizeInt mapSize = data.MoveMap.Size;

            // Find all crate pulls
            int pulls = 0;
            for (int cy=0; cy<mapSize.Y; cy++)
                for (int cx=0; cx<mapSize.X;cx++)
                {
                    VectorInt playerPosition = new VectorInt(cx, cy);
                    if (data.MoveMap[playerPosition])
                    {
                        pulls += CheckPull(data, playerPosition, Direction.Up);
                        pulls += CheckPull(data, playerPosition, Direction.Down);
                        pulls += CheckPull(data, playerPosition, Direction.Left);
                        pulls += CheckPull(data, playerPosition, Direction.Right);
                    }
                }

            if (pulls == 0)
            {
                // Dead, no further moves
                data.Status = SolverNodeStates.DeadChildren;
            }

            node.Data.Weighting = CalcWeighting(node.Data);

            data.IsChildrenEvaluated = true;
            MarkEvalCompelete(node);
        }

        /// <summary>
        /// For a given node (normally a solution) build the player moves
        /// </summary>
        /// <param name="reverse"></param>
        /// <returns></returns>
        public Path BuildPath(SolverNode reverse)
        {
            List<TreeNode<SolverNode>> nodePath = reverse.TreeNode.GetPathToRoot();


            Path result = null;

            int startIndex = 0;
            VectorInt startPos;
            
            if (nodePath[0].Data.ChainSolutionLink == null)
            {
                // Start at the root
                result = new Path(controller.Map.Player);

                // Fill in the gap between the player position and the first move node
                
                VectorInt firstMove =  nodePath[0].Data.PlayerPosition;

                Bitmap boundry = StaticAnalysis.InitialCrateMap.BitwiseOR(StaticAnalysis.BoundryMap);

                // Find all positble moves for the player
                FloodFillStrategy floodFill = new FloodFillStrategy(boundry, controller.Map.Player);
                Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
                eval.Evaluate(floodFill);

                List<LocationNode> shortestPath = floodFill.GetShortestPath(firstMove);
                if (shortestPath == null) throw new InvalidOperationException("Map intial player position must be on the path.");

                foreach (LocationNode locationNode in shortestPath)
                {
                    result.Add(locationNode.Location);
                }
                startPos = nodePath[0].Data.PlayerPositionBeforeMove;
                startIndex = 1;
            }
            else
            {
                result = new Path(nodePath[0].Data.ChainSolutionLink.PlayerPosition);
                startPos = result.StartLocation;
            }
                

            
            
            for (int cc = startIndex; cc < nodePath.Count-1; cc++)
            {
                SolverNode node = nodePath[cc].Data;

                Bitmap boundry = node.CrateMap.BitwiseOR(StaticAnalysis.BoundryMap);

                // Find all positble moves for the player
                FloodFillStrategy floodFill = new FloodFillStrategy(boundry, startPos);
                Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
                eval.Evaluate(floodFill);

                VectorInt destPos = node.PlayerPosition;
                List<LocationNode> shortestPath = floodFill.GetShortestPath(destPos);
                if (shortestPath == null) throw new InvalidOperationException("New player position must be on the path. ");

                foreach (LocationNode locationNode in shortestPath)
                {
                    result.Add(locationNode.Location);
                }

                startPos = node.PlayerPositionBeforeMove;
                                
            }

            result.Add(VectorInt.Reverse(nodePath[nodePath.Count-2].Data.MoveDirection));

            return result;
        }

        /// <summary>
        /// Prefix nodes with R for reverse
        /// </summary>
        /// <returns></returns>
        protected override string GetNextNodeID()
        {
            return "R"+base.GetNextNodeID();
        }

        /// <summary>
        /// Check if a crate can be pulled
        /// </summary>
        /// <param name="solverNode"></param>
        /// <param name="playerPosition"></param>
        /// <param name="pullDirection"></param>
        /// <returns></returns>
        private int CheckPull(SolverNode solverNode, VectorInt playerPosition, Direction pullDirection)
        {
            VectorInt oldCratePosition = playerPosition.Offset(VectorInt.Reverse(pullDirection));
            VectorInt newCratePosition = playerPosition;
            VectorInt newPlayerPosition = playerPosition.Offset(pullDirection);

            // Must be a crate to pull
            if (!solverNode.CrateMap[oldCratePosition]) return 0;

            // Player must move into a space
            if (StaticAnalysis.BoundryMap[newPlayerPosition]) return 0;
            if (solverNode.CrateMap[newPlayerPosition]) return 0;

            // Ok, we have a valid pull
            SolverNode newChild = new SolverNode();
            AddNodeForEval(solverNode.TreeNode, newChild); // so we can get parent weighting

            newChild.CrateMap = new Bitmap(solverNode.CrateMap);
            newChild.CrateMap[oldCratePosition] = false;
            newChild.CrateMap[newCratePosition] = true;       // Old player pos is the new crate position
            newChild.PlayerPosition = newPlayerPosition;
            newChild.MoveMap = null;
            newChild.IsStateEvaluated = false;
            newChild.IsChildrenEvaluated = false;
            newChild.MoveDirection = pullDirection;
            newChild.Weighting = CalcWeighting(newChild);

            
            return 1;
        }

        /// <summary>
        /// Weighting function, this is the basis for selecting the next node to evaluate
        /// </summary>
        /// <param name="solverNode"></param>
        /// <returns></returns>
        private float CalcWeighting(SolverNode solverNode)
        {
            if (false)
            {
                // DEPTH only
                if (solverNode.TreeNode != null)
                {
                    return 100 - solverNode.TreeNode.Depth;
                }
                return 0;
            }
            else
            {

                // Start with crate map weighting
                float weighting = new Matrix(solverNode.CrateMap, 1f).Multiply(StaticAnalysis.StaticForwardCrateWeighting).Total();

                return weighting;
            }
            
            return 0;
        }


        /// <summary>
        /// Check to see if this node already exists in the search tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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

        private SolverController controller;
        private SolverNodeCollection cachedNodes;   

       
    }
}
