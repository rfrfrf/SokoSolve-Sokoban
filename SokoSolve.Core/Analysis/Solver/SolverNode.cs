using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Primary Node states
    /// </summary>
    public enum SolverNodeStates
    {
        /// <summary>
        /// No status
        /// </summary>
        None,

        /// <summary>
        /// Has this state already been found and evaluated
        /// </summary>
        Duplicate,

        /// <summary>
        /// Is a valid solution
        /// </summary>
        Solution,

        /// <summary>
        /// While not a solution this is on the path to a solution
        /// </summary>
        SolutionPath,

        /// <summary>
        /// This node is dead
        /// </summary>
        Dead,

        /// <summary>
        /// This node and all children nodes are dead
        /// </summary>
        DeadChildren,

         /// <summary>
        /// This node is dead as there are no further children nodes to analyse
        /// </summary>
        DeadExhausted
    }


    /// <summary>
    /// The domain knowledge for a puzzle node
    /// </summary>
    public class SolverNode : IEvaluationNode, ITreeNodeBackReference<SolverNode>
    {
        public SolverNode()
        {
            status = SolverNodeStates.None;
        }

        #region IEvaluationNode Members

        /// <summary>
        /// Human-readable ID.
        /// </summary>
        public string NodeID
        {
            get { return nodeID; }
            set { nodeID = value; }
        }

        /// <summary>
        /// Has the initial evaluation been done
        /// </summary>
        public bool IsStateEvaluated
        {
            get { return isStateEvaluated; }
            set { isStateEvaluated = value; }
        }

        /// <summary>
        /// Have the children been generated
        /// </summary>
        public bool IsChildrenEvaluated
        {
            get { return isChildrenEvaluated; }
            set { isChildrenEvaluated = value; }
        }

        #endregion

        /// <summary>
        /// Current crate position
        /// </summary>
        public Bitmap CrateMap
        {
            get { return crateMap; }
            set { crateMap = value; }
        }

        /// <summary>
        /// Movemap as generated from the crate and player position
        /// </summary>
        public Bitmap MoveMap
        {
            get { return moveMap; }
            set { moveMap = value; }
        }

        /// <summary>
        /// Current player position (position after push, which created CrateMap, but before MoveMap)
        /// </summary>
        public VectorInt PlayerPosition
        {
            get { return playerPosition; }
            set { playerPosition = value; }
        }

        /// <summary>
        /// Direction of the push resulting in the crate map
        /// </summary>
        public Direction PushDirection
        {
            get { return pushDirection; }
            set { pushDirection = value; }
        }

        /// <summary>
        /// <see cref="PlayerPosition"/> Position before the push
        /// </summary>
        public VectorInt PlayerPositionBeforePush
        {
            get
            {
                return playerPosition.Offset(VectorInt.Reverse(pushDirection));
            }
        }

        /// <summary>
        /// The primary dead/solution status
        /// </summary>
        public SolverNodeStates Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Weighting assesment
        /// </summary>
        public float Weighting
        {
            get { return weighting; }
            set { weighting = value; }
        }

        public TreeNode<SolverNode> TreeNode
        {
            get { return backRef; }
            set { backRef = value; }
        }

        public SolverBitmap DeadMap
        {
            get { return deadMap; }
            set { deadMap = value; }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1} {2} {3} {4} p{5}",
                                 nodeID,
                                 isStateEvaluated ? "Eval" : "NoEval",
                                 isChildrenEvaluated ? "ChildEval" : "NotChildEval",
                                 status,
                                 weighting, playerPosition);
        }

        /// <summary>
        /// Get simple display labels
        /// </summary>
        /// <returns></returns>
        public SolverLabelList GetDisplayData()
        {
            SolverLabelList txt = new SolverLabelList();
            txt.Add("NodeID", nodeID);
            txt.Add("Evaluated", isStateEvaluated);
            txt.Add("Evaluated Children", isChildrenEvaluated);
            txt.Add("Status", status.ToString());
            txt.Add("Weighting", weighting);
            txt.Add("Player", playerPosition.ToString());
            txt.Add("Depth", backRef.Depth);
            txt.Add("Total Depth", backRef.TotalDepth);
            txt.AddListSummary("Children", new List<SolverNode>(backRef.ChildrenData), delegate(SolverNode item) { return item.nodeID;  }, ", ");
            txt.Add("Total Children", backRef.TotalChildCount);
            return txt;
        }

        // Group: Base Eval items
        private string nodeID;
        private bool isStateEvaluated;
        private bool isChildrenEvaluated;

        // Basic state
        private Bitmap crateMap;
        private Bitmap moveMap;
        private VectorInt playerPosition;
        private Direction pushDirection;

        // Derrived meta-state
        private SolverNodeStates status;
        private SolverBitmap deadMap;
        private float weighting;
        private TreeNode<SolverNode> backRef;
    }
}
