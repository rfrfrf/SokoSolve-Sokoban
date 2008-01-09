using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;

namespace SokoSolve.Core.Analysis.Solver.Reverse
{
    /// <summary>
    /// Work backwards from a goal position to the initial crate position
    /// </summary>
    public class ReverseStrategy : EvaluationStrategyBase<SolverNode>
    {
        public ReverseStrategy(SolverController controller) : base(new SolverItterator(controller))
        {
            this.controller = controller;
        }

        /// <summary>
        /// Evaluation tree of search positions
        /// </summary>
        public Tree<SolverNode> EvaluationTree
        {
            get { return base.evaluation; }
        }

        /// <summary>
        /// Initialise the start conditions for the search. Prepare the root search node
        /// </summary>
        /// <returns>Return the root search node for init and eval</returns>
        public override SolverNode InitStartConditions()
        {
            staticAnalysis = new StaticAnalysis(controller);
            staticAnalysis.Analyse();
           
            SolverNode endposition = new SolverNode();

            // Crate map is the goal map
            endposition.CrateMap = staticAnalysis.GoalMap;

            // We do not know that the player position is at the end
            endposition.PlayerPosition = VectorInt.Empty; 

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
            if (staticAnalysis.BoundryMap[newPlayerPosition]) return;
            if (endposition.CrateMap[newPlayerPosition]) return;

            // We have a valid player start position
            Bitmap partialMoveMap = MapAnalysis.GenerateMoveMap(staticAnalysis.BoundryMap, endposition.CrateMap, oldPlayerPosition);
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
            // Check if this is a solution
            if (node.Data.CrateMap.Equals(staticAnalysis.InitialCrateMap))
            {
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

            // Build the players move map
            if (node.Data.MoveMap == null)
            {
                // Generate MoveMap
                node.Data.MoveMap = MapAnalysis.GenerateMoveMap(staticAnalysis.BoundryMap, node.Data.CrateMap, node.Data.PlayerPosition);
            }

            node.Data.IsStateEvaluated = true;

            // Check to chain match (forward and reverse)
            if (controller.CheckChainForward(node.Data))
            {
                return EvalStatus.CompleteSolution;
            }

            // Check for duplicates
            if (CheckDuplicate(node))
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
            if (staticAnalysis.BoundryMap[newPlayerPosition]) return 0;
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
            if (solverNode.TreeNode != null)
            {
                // Depth last search
                return 100 - solverNode.TreeNode.Depth;    
            }

            return 0;
        }


        /// <summary>
        /// Check to see if this node already exists in the search tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool CheckDuplicate(INode<SolverNode> node)
        {
            return CheckDuplicateRecurse(evaluation.Root, node as TreeNode<SolverNode>);
        }
        /// <summary>
        /// Helper Recurse Method. <see cref="CheckDuplicate"/>
        /// Check to see if this node already exists in the search tree
        /// </summary>
        /// <param name="searchFor">Target Search</param>
        /// <returns></returns>
        private bool CheckDuplicateRecurse(TreeNode<SolverNode> current, TreeNode<SolverNode> searchFor)
        {
            if (!object.ReferenceEquals(current, searchFor))
            {
                // Don't match against un-inited nodes
                if (!current.Data.IsStateEvaluated) return false;
                if (current.Data.MoveMap == null) return false;


                if (searchFor.Data.CrateMap.Equals(current.Data.CrateMap))
                {
                    if (searchFor.Data.MoveMap.Equals(current.Data.MoveMap))
                    {
                        // Match found
                        return true;
                    }
                }

                // Recurse down
                if (current.HasChildren)
                {
                    foreach (TreeNode<SolverNode> child in current.Children)
                    {
                        if (CheckDuplicateRecurse(child, searchFor)) return true;
                    }
                }
            }

            return false;
        }

        private SolverController controller;
        private StaticAnalysis staticAnalysis;

    }
}
