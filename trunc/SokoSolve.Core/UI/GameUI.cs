using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using SokoSolve.Common;
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
        /// Strong Construction. Start a 'Normal' game
        /// <see cref="Init"/>, then <see cref="InitFX"/>, then <see cref="StartRender"/>
        /// </summary>
        /// <param name="aMap">Puzzle to play</param>
        public GameUI(Puzzle aPuzzle, PuzzleMap aMap, ISoundSubSystem sfx) : base(aPuzzle, aMap.Map)
        {
            solution = null;
            puzzleMap = aMap;

            initType = InitTypes.NewGame;

            GameCoords = new GameCoords(this);
            StepCurrent = 0;

            ResourceFactory = ResourceController.Singleton.GetInstance("Default.GameTiles");
            GameCoords.GlobalTileSize = new SizeInt(48, 48);
           
            nodes = new List<NodeBase>();
            nodesToRemove = new List<NodeBase>();
            nodesToAdd = new List<NodeBase>();

            // I would like to move this somewhere else
            sound = sfx;
           
            // Add blank bookmarks
            Add((Bookmark)null);
            Add((Bookmark)null);
            Add((Bookmark)null);
            Add((Bookmark)null);
            Add((Bookmark)null);
        }

        /// <summary>
        /// Strong Construction. Replay a solution
        /// </summary>
        /// <param name="aPuzzle"></param>
        /// <param name="aMap"></param>
        /// <param name="aSolution"></param>
        public GameUI(Puzzle aPuzzle, PuzzleMap aMap, Solution aSolution) : base(aPuzzle, aMap.Map)
        {
            solution = aSolution;
            puzzleMap = aMap;

            initType = InitTypes.SolutionReplay;

            GameCoords = new GameCoords(this);
            StepCurrent = 0;

            ResourceFactory = ResourceController.Singleton.GetInstance("Default.GameTiles");
            GameCoords.GlobalTileSize = new SizeInt(48, 48);

            nodes = new List<NodeBase>();
            nodesToRemove = new List<NodeBase>();
            nodesToAdd = new List<NodeBase>();
        }

        /// <summary>
        /// Provide access to coordinate-calculation information
        /// </summary>
        public GameCoords GameCoords
        {
            get { return gameCoords; }
            set { gameCoords = value; }
        }

        /// <summary>
        /// The GDI graphics handeller
        /// </summary>
        public Graphics Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }

        /// <summary>
        /// Provide access to resoruces
        /// </summary>
        public ResourceFactory ResourceFactory
        {
            get { return resourceFactory; }
            set { resourceFactory = value; }
        }

        /// <summary>
        /// The Size of each tile (this should be in GameCoords)
        /// </summary>
        public Size SizeTile
        {
            get { return sizeTile; }
            set { sizeTile = value; }
        }

        /// <summary>
        /// The current game step
        /// </summary>
        public int StepCurrent
        {
            get { return stepCurrent; }
            set { stepCurrent = value; }
        }

        /// <summary>
        /// Is the game active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                if (active == false)
                {
                    if (sound != null)
                    {
                        sound.Stop();    
                    }
                }
            }
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void Start()
        {
            initType = InitTypes.NewGame;
            Init();
            Active = true;
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartSolution()
        {
            initType = InitTypes.SolutionReplay;
            Init();
            Active = true;

            foreach (Direction dir in solution.ToPath().Moves)
            {
                Player.doMove(dir);
            }
        }

        /// <summary>
        /// The puzzle currently being played
        /// </summary>
        public PuzzleMap PuzzleMap
        {
            get { return puzzleMap; }
        }

        /// <summary>
        /// Setup the display or drawing system
        /// </summary>
        /// <param name="aGraphics"></param>
        public void StartRender(Graphics aGraphics)
        {
            Graphics = aGraphics;
        }

        /// <summary>
        /// Shut down  the display or drawing system
        /// </summary>
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

        /// <summary>
        /// Currently init game type
        /// </summary>
        public InitTypes InitType
        {
            get { return initType; }
        }

        /// <summary>
        /// Ways in which the game can be initialised
        /// </summary>
        public enum InitTypes
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

#if DEBUG
            NodeDebug debug = new NodeDebug(this, int.MaxValue);
            Add(debug);
#endif

            if (initType == InitTypes.SolutionReplay)
            {
                // Exit
                return;
            }

            cursor = new NodeCursor(this, int.MaxValue);
            Add(cursor);

            if (initType == InitTypes.NewGame)
            {
                NodeEffectText start = new NodeEffectText(this, 10, "Welcome to SokoSolve, START!", Player.CurrentAbsolute);
                start.Brush = new SolidBrush(Color.FromArgb(80, Color.White));
                start.BrushShaddow = new SolidBrush(Color.FromArgb(80, Color.Black));
                start.Path = new Paths.Spiral(Player.CurrentCentre);
                Add(start);

                // Stop all music and sounds
                sound.Stop();
                sound.PlaySound(ResourceFactory[ResourceID.GameSoundWelcome].DataAsSound);

                string[] music = Directory.GetFiles(FileManager.getContent("$Music/"), "*.mp3");
                sound.PlayMusic(sound.GetHandle(Path.GetFileName(RandomHelper.Select<string>(music))));

            }
            else if (initType == InitTypes.Restart)
            {
                NodeEffectText start = new NodeEffectText(this, 10, "Starting again, eh?", Player.CurrentAbsolute);
                start.Brush = new SolidBrush(Color.FromArgb(180, Color.Gold));
                start.BrushShaddow = new SolidBrush(Color.FromArgb(180, Color.Black));
                start.Path = new Paths.Spiral(Player.CurrentCentre);
                Add(start);

                sound.PlaySound(ResourceFactory[ResourceID.GameSoundRestart].DataAsSound);
            }
            else if (initType == InitTypes.Undo)
            {
                NodeEffectText start = new NodeEffectText(this, 10, new string[] { "Hmmm", "Grrr", "Pity", "???"}, GameCoords.WindowRegion.Center);
                start.Brush = new SolidBrush(Color.Cyan);
                Add(start);

                sound.PlaySound(ResourceFactory[ResourceID.GameSoundUndo].DataAsSound);
            }
            

            NodeControllerStatus status = new NodeControllerStatus(this, 500);
            Add(status);

            NodeControllerCommands commands = new NodeControllerCommands(this, 501);
            Add(commands);

            NodeTitle title = new NodeTitle(this, 1000);
            Add(title);

            NodeControllerBookmarks waypoint = new NodeControllerBookmarks(this, 502);
            Add(waypoint);

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
            initType = InitTypes.Undo;
            base.Undo();
            Init();
            
        }

        /// <summary>
        /// Reset/Restart the puzzle to its initial condition
        /// </summary>
        public override void Reset()
        {
            initType = InitTypes.Restart;
            base.Reset();
            Init();
        }

        /// <summary>
        /// Reset/Restart the puzzle to its initial condition
        /// </summary>
        public override void Reset(Bookmark bookMark)
        {
            initType = InitTypes.Restart;
            base.Reset(bookMark);
            Init();
        }

        /// <summary>
        /// Quite, exit, give up the puzzle
        /// </summary>
        public void Exit()
        {
            Active = false;
            sound.Stop();
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
                // So that the render cycle does not get completely killed.
                string txt = StringHelper.Report(ex);
                Graphics.DrawString(txt, new Font("Arial", 10f), new SolidBrush(Color.Red), 2, 2);   
            }
        }

        /// <summary>
        /// Capture mouse button events
        /// </summary>
        public enum MouseButtons
        {
            None,
            Left,
            Right
        }

        /// <summary>
        /// Capture mouse clicks
        /// </summary>
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
            if (cursor == null) return false;
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

        /// <summary>
        /// General game events
        /// </summary>
        public EventHandler<NotificationEvent> OnGameEvents;

        /// <summary>
        /// Allow 'friend' classes to send a common notification event
        /// </summary>
        /// <param name="e"></param>
        protected internal void FireNotificationEvent(NotificationEvent e)
        {
            if (OnGameEvents != null)
            {
                OnGameEvents(this, e);
            }
        }
        
        private GameCoords gameCoords;
        private Graphics graphics;
        private ResourceFactory resourceFactory;
        private Size sizeTile;
        private int stepCurrent;
        private bool active;
        private List<NodeBase> nodes;
        private List<NodeBase> nodesToAdd;
        private List<NodeBase> nodesToRemove;
        private NodeCursor cursor;
        private NodeDynamicPlayer player;
        private ISoundSubSystem sound;
        private Solution solution;
        private PuzzleMap puzzleMap;
        private InitTypes initType;
    }
}
