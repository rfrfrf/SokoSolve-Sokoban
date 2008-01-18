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
        /// Is a valid solution created from a join in the forward and reverse trees
        /// </summary>
        SolutionChain,

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
    public class SolverNode : IEvaluationNode, ITreeNodeBackReference<SolverNode>, IEquatable<SolverNode>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SolverNode()
        {
            status = SolverNodeStates.None;
            playerPosition = VectorInt.Null;
            cachedHashCode = int.MinValue;
        }

        /// <summary>
        /// Deep copy constructor -- Allows state to be preseved current state (but not treenode)
        /// </summary>
        public SolverNode(SolverNode rhs)
        {
           // Group: Base Eval items
            nodeID = rhs.nodeID;
            isStateEvaluated = rhs.isStateEvaluated;
            isChildrenEvaluated = rhs.isChildrenEvaluated;
        

            // Basic state
            if (rhs.crateMap != null) crateMap = new  Bitmap(rhs.crateMap);
            if (rhs.moveMap != null) moveMap = new Bitmap(rhs.moveMap);
            playerPosition = rhs.playerPosition;
            moveDirection= rhs.moveDirection;

            // Derrived meta-state
            status = rhs.status;
            if (rhs.deadMap!= null) deadMap = new SolverBitmap(rhs.deadMap);
            if (rhs.chainSolutionLink != null) chainSolutionLink = new SolverNode(rhs.chainSolutionLink);
            weighting = rhs.weighting;
            backRef = rhs.backRef;

            Init();
        }

        private void Init()
        {
            if (crateMap != null && moveMap != null)
            {
                cachedHashCode = crateMap.GetHashCode() ^ moveMap.GetHashCode();    
            }
            else
            {
                cachedHashCode = int.MinValue;
            }
        }

        public bool Equals(SolverNode rhs)
        {
            if (rhs == null) return false;
            if (cachedHashCode != rhs.GetHashCode()) return false;

            if (crateMap == null || rhs.crateMap == null) return false;
            if (moveMap == null || rhs.moveMap == null) return false;

            return crateMap.Equals(rhs.crateMap) && moveMap.Equals(rhs.moveMap);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SolverNode);
        }

        /// <summary>
        /// Be very carefull with this. We cache GetHashCode, so Init must be call on *ALL* method that may affect Equals(...)
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (cachedHashCode == int.MinValue) throw new InvalidOperationException("CrateMap and MoveMap required");
            return cachedHashCode;
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
            set { crateMap = value; Init(); }
        }

        /// <summary>
        /// Movemap as generated from the crate and player position
        /// </summary>
        public Bitmap MoveMap
        {
            get { return moveMap; }
            set { moveMap = value; Init(); }
        }

        /// <summary>
        /// Current player position (position after push, which created CrateMap, but before MoveMap)
        /// </summary>
        public VectorInt PlayerPosition
        {
            get { return playerPosition; }
            set
            {
                if (value.IsNull) throw new ArgumentNullException("value", "PlayerPosition cannot be null");
                playerPosition = value;
            }
        }

        /// <summary>
        /// Direction of the push/pull resulting in the crate map
        /// </summary>
        public Direction MoveDirection
        {
            get { return moveDirection; }
            set { moveDirection = value; }
        }

        /// <summary>
        /// <see cref="PlayerPosition"/> Position before the push
        /// </summary>
        public VectorInt PlayerPositionBeforeMove
        {
            get
            {
                return playerPosition.Offset(VectorInt.Reverse(moveDirection));
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

        /// <summary>
        /// Provide a 'back-ref' to the tree node information
        /// </summary>
        public TreeNode<SolverNode> TreeNode
        {
            get { return backRef; }
            set { backRef = value; }
        }

        /// <summary>
        /// Dynamic dead node map
        /// </summary>
        public SolverBitmap DeadMap
        {
            get { return deadMap; }
            set { deadMap = value; }
        }

        /// <summary>
        /// The matching node in the forward/reverse chain
        /// </summary>
        public SolverNode ChainSolutionLink
        {
            get { return chainSolutionLink; }
            set { chainSolutionLink = value; }
        }

        /// <summary>
        /// This this a forward node
        /// </summary>
        public bool IsForward
        {
            get { return nodeID[0] == 'F';  }
        }

        /// <summary>
        /// This this a reverse node
        /// </summary>
        public bool IsReverse
        {
            get { return !IsForward;  }
        }

        /// <summary>
        /// Debug information
        /// </summary>
        /// <returns></returns>
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
            txt.Add("Player", string.Format("{0} {1} -> {2}", PlayerPositionBeforeMove, MoveDirection, playerPosition));
            txt.Add("Depth", backRef.Depth);
            txt.Add("Total Depth", backRef.TotalDepth);
            if (backRef.HasChildren)
            {
                txt.AddListSummary("Children", new List<SolverNode>(backRef.ChildrenData), delegate(SolverNode item) { return item.nodeID;  }, ", ");
            }
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
        private Direction moveDirection;

        // Derrived meta-state
        private SolverNodeStates status;
        private SolverBitmap deadMap;
        private SolverNode chainSolutionLink;
        private float weighting;
        private TreeNode<SolverNode> backRef;
        
        // Optimisation
        int cachedHashCode;

      
    }
}
