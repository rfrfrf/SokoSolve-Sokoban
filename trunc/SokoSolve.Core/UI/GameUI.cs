using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using SokoSolve.Common.Math;
using SokoSolve.Core.Game;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI.Nodes;
using SokoSolve.Core.UI.Nodes.Complex;
using SokoSolve.Core.UI.Nodes.Effects;
using SokoSolve.Core.UI.Nodes.Specific;
using SokoSolve.Core.UI.Nodes.UINodes;

namespace SokoSolve.Core.UI
{
    /// <summary>
    /// GameUI is responsible for the animation cycle that drives the game.
    /// </summary>
    /// <remarks>
    /// The key node is <see cref="NodeDynamicPlayer"/> which acts as the primary input driver.
    /// The game logic does not reside here. Therefore a key mechanism is how 
    /// changes to the Game object's Cells are communicated to the nodes.
    /// </remarks>
    public class GameUI : SokoSolve.Core.Game.Game
    {
        /// <summary>
        /// Strong Construction. <see cref="Init"/>, then <see cref="InitFX"/>, then <see cref="StartRender"/>
        /// </summary>
        /// <param name="Map">Puzzle to play</param>
        public GameUI(Puzzle aPuzzle, SokobanMap Map, ISoundSubSystem sfx) : base(aPuzzle, Map)
        {
            GameCoords = new GameCoords(this);
            StepCurrent = 0;

            ResourceManager = ResourceFactory.Singleton.GetInstance("Default.GameTiles");
            GameCoords.GlobalTileSize = new SizeInt(32, 32);
           
            nodes = new List<NodeBase>();
            nodesToRemove = new List<NodeBase>();
            nodesToAdd = new List<NodeBase>();

            // I would like to move this somewhere else
            sound = sfx;
            sfxWelcome = sound.GetHandle("Welcome.wav");
            sfxUndo = sound.GetHandle("sound40.wav");
            sfxRestart = sound.GetHandle("sound31.wav");

            // Add blank bookmarks
            Add((Bookmark)null);
            Add((Bookmark)null);
            Add((Bookmark)null);
            Add((Bookmark)null);
            Add((Bookmark)null);
            
        }

        public void Start()
        {
            initType = InitType.NewGame;
            Init();
            Active = true;
        }

        /// <summary>
        /// Setup the display or drawing system
        /// </summary>
        /// <param name="aGraphics"></param>
        public void StartRender(Graphics aGraphics)
        {
            Graphics = aGraphics;
        }

        public void EndRender()
        {
            Graphics = null;
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

        public enum InitType
        {
            NewGame,
            Restart,
            Undo,
            SolutionReplay
        }

        /// <summary>
        /// Inititialise the special effects
        /// </summary>
        private void InitFX()
        {
            cursor = new NodeCursor(this, int.MaxValue);
            Add(cursor);

            if (initType == InitType.NewGame)
            {
                NodeEffectText start = new NodeEffectText(this, 10, "Welcome to SokoSolve, START!", Player.CurrentAbsolute);
                start.Brush = new SolidBrush(Color.FromArgb(80, Color.White));
                start.BrushShaddow = new SolidBrush(Color.FromArgb(80, Color.Black));
                start.Path = new Paths.Spiral(Player.CurrentCentre);
                Add(start);

                sound.PlaySound(sfxWelcome);

                sound.PlayMusic(sound.GetHandle("Camokaze-Low.mp3"));

            }
            else if (initType == InitType.Restart)
            {
                NodeEffectText start = new NodeEffectText(this, 10, "Starting again, eh?", Player.CurrentAbsolute);
                start.Brush = new SolidBrush(Color.FromArgb(180, Color.Gold));
                start.BrushShaddow = new SolidBrush(Color.FromArgb(180, Color.Black));
                start.Path = new Paths.Spiral(Player.CurrentCentre);
                Add(start);

                sound.PlaySound(sfxRestart);
            }
            else if (initType == InitType.Undo)
            {
                NodeEffectText start = new NodeEffectText(this, 10, new string[] { "Hmmm", "Grrr", "Pity", "???"}, GameCoords.WindowRegion.Center);
                start.Brush = new SolidBrush(Color.Cyan);
                Add(start);

                sound.PlaySound(sfxUndo);
            }
            

            NodeControllerStatus status = new NodeControllerStatus(this, 500);
            Add(status);

            NodeControllerCommands commands = new NodeControllerCommands(this, 500);
            Add(commands);

            NodeTitle title = new NodeTitle(this, 1000);
            Add(title);

            NodeControllerBookmarks waypoint = new NodeControllerBookmarks(this, 500);
            Add(waypoint);

#if DEBUG
            NodeDebug debug = new NodeDebug(this, int.MaxValue);
            Add(debug);
#endif
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
            initType = InitType.Undo;
            base.Undo();
            Init();
            
        }

        /// <summary>
        /// Reset/Restart the puzzle to its initial condition
        /// </summary>
        public override void Reset()
        {
            initType = InitType.Restart;
            base.Reset();
            Init();
        }

        /// <summary>
        /// Reset/Restart the puzzle to its initial condition
        /// </summary>
        public override void Reset(Bookmark bookMark)
        {
            initType = InitType.Restart;
            base.Reset(bookMark);
            Init();
        }

        /// <summary>
        /// Quite, exit, give up the puzzle
        /// </summary>
        public void Exit()
        {
            Active = false;
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
        /// Allow direct accuss to the cursor node and logic
        /// </summary>
        public NodeCursor Cursor
        {
            get { return cursor;  }
        }

        /// <summary>
        /// Access the sound (sfx, music)  playing sub-system
        /// </summary>
        public ISoundSubSystem Sound
        {
            get { return sound; }
            set { sound = value; }
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
            if (!Active) return;

            StepCurrent++;
            foreach (NodeBase n in nodes)
            {
                if (!n.IsRemoved) n.doStep();
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
            if (!Active) return;

            try
            {
                // Sort
                nodes.Sort(new Comparison<NodeBase>(NodeCompareDepth));
                // Render each node
                foreach (NodeBase n in nodes)
                {
                    if (!n.IsRemoved)  n.Render();
                }
            }
            catch(Exception ex)
            {
                // Log me?
                Debug.WriteLine(ex.Message);

                // So that the render cycle does not get completely killed.
                Graphics.DrawString(ex.Message, new Font("Arial", 10f), new SolidBrush(Color.Red), 2, 2);
                
            }
        }

        public enum MouseButtons
        {
            None,
            Left,
            Right
        }

        public enum MouseClicks
        {
            None,
            Down,
            Up
        }

        /// <summary>
        /// Update the cursor position
        /// </summary>
        /// <param name="X">Mouse X</param>
        /// <param name="Y">Mouse Y</param>
        /// <param name="ClickCount">Number of clicks</param>
        /// <param name="Button">Button enum 0=None, 1=Left, 2=Right</param>
        /// <returns>true is cursor is drawn by game</returns>
        /// <remarks>
        /// See <see cref="NodeCursor"/> for implementation. This is a proxy method.
        /// </remarks>
        public bool SetCursor(int X, int Y, int ClickCount, MouseButtons Button, MouseClicks ClickType)
        {
            return cursor.SetCursor(X, Y, ClickCount, Button, ClickType);
        }

        /// <summary>
        /// Does this node already exist
        /// </summary>
        /// <param name="aNode"></param>
        /// <returns></returns>
        public bool HasNode(NodeBase aNode)
        {
            return (nodesToAdd.Contains(aNode) || nodes.Contains(aNode));
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
        public bool Active;

        List<NodeBase> nodes;
        List<NodeBase> nodesToAdd;
        List<NodeBase> nodesToRemove;

        private NodeCursor cursor;
        private NodeDynamicPlayer player;
        private ISoundSubSystem sound;

        private ISoundHandle sfxUndo;
        private ISoundHandle sfxRestart;
        private ISoundHandle sfxWelcome;

        private InitType initType;

        
    }
}
