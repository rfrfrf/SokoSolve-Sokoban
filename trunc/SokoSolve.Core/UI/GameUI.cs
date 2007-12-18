using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI.Nodes;
using SokoSolve.Core.UI.Nodes.Complex;
using SokoSolve.Core.UI.Nodes.Effects;
using SokoSolve.Core.UI.Nodes.UINodes;

namespace SokoSolve.Core.UI
{
    /// <summary>
    /// GameUI is responsible for the animation cycle that drives the game.
    /// </summary>
    /// <remarks>
    /// The key node is <see cref="NodeDynamicPlayer"/> which acts as the primary input driver.
    /// Note however that the game logic does not reside here. Therefore a key mechanism is how 
    /// changes to the Game object's Cells are communicated to the nodes.
    /// </remarks>
    public class GameUI : SokoSolve.Core.Game.Game
    {
        /// <summary>
        /// Strong Construction. <see cref="Init"/>, then <see cref="InitFX"/>, then <see cref="InitDisplay"/>
        /// </summary>
        /// <param name="aPuz">Puzzle to play</param>
        public GameUI(SokobanMap aPuz) : base(aPuz)
        {
            GameCoords = new GameCoords(this);
            StepCurrent = 0;

            ResourceManager = ResourceFactory.Singleton.GetInstance("Default.GameTiles");
            GameCoords.GlobalTileSize = new SizeInt(32, 32);
           
            nodes = new List<NodeBase>();
            nodesToRemove = new List<NodeBase>();
            nodesToAdd = new List<NodeBase>();
        }

        /// <summary>
        /// Setup the display or drawing system
        /// </summary>
        /// <param name="aGraphics"></param>
        public void InitDisplay(Graphics aGraphics)
        {
            Graphics = aGraphics;
        }

        /// <summary>
        /// Cleanup and then prepare and layout the GameNodes from the Puzzle
        /// </summary>
        /// <remarks>
        /// Create a set of nodes for each cell state of the puzzle.
        /// </remarks>
        public virtual void Init()
        {
            // Cleanups
            nodes.Clear();
            nodesToAdd.Clear();
            nodesToRemove.Clear();

            StepCurrent = 0;

            for(int px=0; px<Current.Size.X; px++)
                for (int py = 0; py < Current.Size.Y; py++)
                {
                    VectorInt pos = new VectorInt(px, py);
                    CellStates cs = Current[px, py];
                    switch (cs)
                    {
                        case(CellStates.Void):
                            NodeStaticCell s = new NodeStaticCell(this, Cell.Void, pos, 0);
                            nodes.Add(s);
                            break;
                        case (CellStates.Wall):
                            s = new NodeStaticCell(this, Cell.Wall, pos, 0);
                            nodes.Add(s);
                            break;
                        case (CellStates.FloorGoal):
                            s = new NodeStaticCell(this, Cell.Floor, pos, 0);
                            nodes.Add(s);

                            NodeStaticCell g = new NodeStaticCell(this, Cell.Goal, pos, 2);
                            nodes.Add(g);
                            break;
                        case (CellStates.Floor):
                            s = new NodeStaticCell(this, Cell.Floor, pos, 0);
                            nodes.Add(s);
                            break;
                        case (CellStates.FloorCrate):
                            s = new NodeStaticCell(this, Cell.Floor, pos, 0);
                            nodes.Add(s);

                            NodeDynamicCrate c = new NodeDynamicCrate(this, pos, 3);
                            nodes.Add(c);
                            break;

                        case (CellStates.FloorGoalCrate):
                            s = new NodeStaticCell(this, Cell.Floor, pos, 0);
                            nodes.Add(s);

                            g = new NodeStaticCell(this, Cell.Goal, pos, 2);
                            nodes.Add(g);

                            c = new NodeDynamicCrate(this, pos, 3);
                            nodes.Add(c);
                            break;

                        case (CellStates.FloorGoalPlayer):
                            s = new NodeStaticCell(this, Cell.Floor, pos, 0);
                            nodes.Add(s);

                            g = new NodeStaticCell(this, Cell.Goal, pos, 2);
                            nodes.Add(g);

                            player = new NodeDynamicPlayer(this, pos, 5);
                            nodes.Add(player);
                            break;

                        case (CellStates.FloorPlayer):
                            s = new NodeStaticCell(this, Cell.Floor, pos, 0);
                            nodes.Add(s);

                            player = new NodeDynamicPlayer(this, pos, 5);
                            nodes.Add(player);
                            break;
                    }
                }

            // Initialise additional non-game element
            InitFX();
        }

        /// <summary>
        /// Inititialise the special effects
        /// </summary>
        private void InitFX()
        {
            cursor = new NodeCursor(this, int.MaxValue);
            Add(cursor);

            NodeEffectText start = new NodeEffectText(this, 10, "Welcome to SokoSolve, START!", Player.CurrentAbsolute);
            start.Brush = new SolidBrush(Color.FromArgb(80, Color.White));
            start.BrushShaddow = new SolidBrush(Color.FromArgb(80, Color.Black));
            start.Path = new Paths.Spiral(Player.CurrentCentre);
            Add(start);

            NodeControllerStatus status = new NodeControllerStatus(this, 500);
            Add(status);

            NodeControllerCommands commands = new NodeControllerCommands(this, 500);
            Add(commands);
        }

        /// <summary>
        /// Use the base logic, then sync the changed nodes
        /// </summary>
        /// <param name="moveDir"></param>
        /// <returns></returns>
        /// <remarks>Rather use Player.doMove() to allow animations and stacking</remarks>
        public override MoveResult Move(Direction moveDir)
        {
            // Do the logic
            MoveResult r = base.Move(moveDir);

            // Sync the puzzle to the related nodes
            if (r != MoveResult.Invalid)
            {
                // Change the player position
                Player.PuzzleLocation = Current.Player;

                if (r == MoveResult.ValidPush || r == MoveResult.ValidPushGoal || r == MoveResult.ValidPushWin)
                {
                    // Change the pushed crate location
                    NodeBase pushedcrate = getNodeCell(typeof(NodeDynamicCrate), Current.Player); // Note: The crate is now in the wrong place
                    if (pushedcrate != null)
                    {
                        (pushedcrate as NodeDynamicCrate).PuzzleLocation = Current.Player.Offset(moveDir); 
                    }
                }
            }
            return r;
        }

        /// <summary>
        /// Undo a single move
        /// </summary>
        public override void Undo()
        {
            base.Undo();
            Init();
            
        }

        /// <summary>
        /// Reset/Restart the puzzle to its initial condition
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            Init();
            
        }

        /// <summary>
        /// Quite, exit, give up the puzzle
        /// </summary>
        public void Exit()
        {
            if (OnExit != null) OnExit(this, new EventArgs());
        }

        /// <summary>
        /// This handled the close/exit implmentation called by Exit()
        /// </summary>
        public event EventHandler OnExit;

        /// <summary>
        /// The node representing the player
        /// </summary>
        public NodeDynamicPlayer Player
        {
            get
            {
                if (player == null)
                {
                    player = (NodeDynamicPlayer)getNodeCell(typeof(NodeDynamicPlayer), Current.Player);
                }
                return player;
            }
        }

        /// <summary>
        /// Get a node depending on its position and class type
        /// </summary>
        /// <param name="aType"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public NodeBase getNodeCell(Type aType, VectorInt pos)
        {
            foreach (NodeBase n in nodes)
            {
                NodeCell nc = n as NodeCell;
                if (nc == null) continue;
                if (n.GetType() != aType) continue;
                if (nc.PuzzleLocation != pos) continue;
                return nc;
            }
            return null;
        }

        /// <summary>
        /// Do a game step
        /// </summary>
        public virtual void PerformStep()
        {
            StepCurrent++;
            foreach (NodeBase n in nodes)
            {
                n.doStep();
            }

            foreach (NodeBase n in nodesToAdd)
            {
                if (!nodes.Contains(n)) nodes.Add(n);
            }
            nodesToAdd.Clear();

            foreach (NodeBase n in nodesToRemove)
            {
                if (nodes.Contains(n)) nodes.Remove(n);
            }
            nodesToRemove.Clear();
        }

        /// <summary>
        /// Render the entire puzzle
        /// </summary>
        public virtual void Render()
        {
            // Clean Up
            //Graphics.FillRectangle(new SolidBrush(Color.Gray), GameCoords.PuzzleRegion.ToDrawingRect());

            // Sort
            nodes.Sort(new Comparison<NodeBase>(NodeCompareDepth));
            // Render each node
            foreach (NodeBase n in nodes)
            {
                n.Render();
            }
        }

        /// <summary>
        /// Update the cursor position
        /// </summary>
        /// <param name="X">Mouse X</param>
        /// <param name="Y">Mouse Y</param>
        /// <param name="Clicks">Number of clicks</param>
        /// <param name="Button">Button enum 0=None, 1=Left, 2=Right</param>
        /// <returns>true is cursor is drawn by game</returns>
        /// <remarks>
        /// See <see cref="NodeCursor"/> for implementation. This is a proxy method.
        /// </remarks>
        public bool SetCursor(int X, int Y, int Clicks, int Button)
        {
            return cursor.SetCursor(X, Y, Clicks, Button);
        }


        /// <summary>
        /// Add a new game node 
        /// </summary>
        /// <param name="aNode"></param>
        public void Add(NodeBase aNode)
        {
            if (!nodesToAdd.Contains(aNode)) nodesToAdd.Add(aNode);
        }

        /// <summary>
        /// Remove an existing game node
        /// </summary>
        /// <param name="aNode"></param>
        public void Remove(NodeBase aNode)
        {
            if (!nodesToRemove.Contains(aNode)) nodesToRemove.Add(aNode);
        }

        /// <summary>
        /// Find a node 
        /// </summary>
        /// <param name="TargetType">Find by type</param>
        public NodeBase Find(Type TargetType)
        {
            NodeBase result = nodes.Find(delegate(NodeBase item) { return item.GetType() == TargetType; });
            if (result != null) return result;

            result = nodesToAdd.Find(delegate(NodeBase item) { return item.GetType() == TargetType; });
            return result;
        }

        /// <summary>
        /// Depth compare 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        static int NodeCompareDepth(NodeBase l, NodeBase r)
        {
            return l.Depth - r.Depth;
        }

        
        public GameCoords GameCoords;
        public Graphics Graphics;
        public ResourceManager ResourceManager;
        public Size SizeTile;
        public int StepCurrent;

        List<NodeBase> nodes;
        List<NodeBase> nodesToAdd;
        List<NodeBase> nodesToRemove;

        private NodeCursor cursor;
        NodeDynamicPlayer player;
    }
}
