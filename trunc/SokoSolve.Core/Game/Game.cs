using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI.Nodes;

namespace SokoSolve.Core.Game
{
    /// <summary>
    /// Encapsulate the basic game logic
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="aMap"></param>
        public Game(Puzzle aPuzzle, SokobanMap aMap)
        {
            if (aMap == null) throw new ArgumentNullException("aPuzzle");
            StringCollection sc = null;
            if (!aMap.IsValid(out sc)) throw new Exception(sc[0]);

            puzzle = aPuzzle;
            startPuzzle = aMap;
            current = new SokobanMap(StartPuzzle);
            moves = new Stack<Move>();
            
            stats = new Stats();
            stats.Start = DateTime.MinValue;
            stats.End = DateTime.MinValue;

            bookmarks = new List<Bookmark>();
        }


        /// <summary>
        /// Result of a move
        /// </summary>
        public enum MoveResult
        {
             None,

            /// <summary>
            /// A invalid push
            /// </summary>
            Invalid,
            
            /// <summary>
            /// A valid simple player move
            /// </summary>
            ValidMove,

            /// <summary>
            /// Player move resulting in a crate push
            /// </summary>
            ValidPush,

            /// <summary>
            /// Player movement resulting in a crate push onto a goal
            /// </summary>
            ValidPushGoal,

            /// <summary>
            /// A winning crate push
            /// </summary>
            ValidPushWin,

            /// <summary>
            /// Allow for easier control (not really game logic)
            /// </summary>
            Undo
           
        }

        /// <summary>
        /// Perform a player move. This is the function that encapsulated the game moves.
        /// </summary>
        /// <param name="moveDir">Direction of move/push</param>
        /// <returns>Result of the move</returns>
        /// <remarks>
        ///     Basic puzzle rules:
        ///     <list type="">
        ///         <item>Player can push, never pull, a crate into a space</item>
        ///         <item>There are no diagonal moves, or pushes</item>
        ///         <item>When all crates are on goal positions the puzzle is complete</item>
        ///         <item>Two crates cannot be pushed in a line</item>
        ///     </list>
        /// </remarks>
        public virtual MoveResult Move(Direction moveDir)
        {
            // First Move?
            if (Stats.Start == DateTime.MinValue)
            {
                Stats.Start = DateTime.Now;
            }

            Stats.Moves++;
            Stats.MovesTotal++;

            VectorInt curr = Current.Player;
			VectorInt currstep = Current.Player.Offset(moveDir);
			VectorInt currstepstep = currstep.Offset(moveDir);
            CellStates p_curr = Current[curr];
            CellStates p_currstep  = Current[currstep];

            CellStates p_currstepstep = CellStates.Void;
            if (Current.Rectangle.Contains(currstepstep))
                p_currstepstep = Current[currstepstep];

            // Is the move valid
            if (p_currstep == CellStates.Wall || p_currstep == CellStates.Void) return MoveResult.Invalid;

            // Next map
            SokobanMap next = new SokobanMap(Current);

            // Step?
            if (p_currstep == CellStates.Floor || p_currstep == CellStates.FloorGoal)
            {
                // Step, no push

                // New player pos
                if (p_currstep == CellStates.Floor) next[currstep] = CellStates.FloorPlayer;
                    else next[currstep] = CellStates.FloorGoalPlayer;

                // Old pos
                if (p_curr == CellStates.FloorGoalPlayer) next[curr] = CellStates.FloorGoal;
                if (p_curr == CellStates.FloorPlayer) next[curr] = CellStates.Floor;

                Moves.Push(new Move(Current, moveDir, false));
                Current = next;
                return MoveResult.ValidMove;
            }

            // Push?
            if (p_currstep == CellStates.FloorCrate || p_currstep == CellStates.FloorGoalCrate)
            {
                Stats.Pushes++;
                Stats.PushesTotal++;
                // Push a crate

                // Valid push?
                if (p_currstepstep == CellStates.Floor || p_currstepstep == CellStates.FloorGoal)
                {
                    // Valid.

                    // Old pos
                    if (p_curr == CellStates.FloorGoalPlayer) next[curr] = CellStates.FloorGoal;
                    if (p_curr == CellStates.FloorPlayer) next[curr] = CellStates.Floor;

                    // Player Pos
                    if (p_currstep == CellStates.FloorCrate) next[currstep] = CellStates.FloorPlayer;
                    if (p_currstep == CellStates.FloorGoalCrate) next[currstep] = CellStates.FloorGoalPlayer;

                    // Crate Pos
                    if (p_currstepstep == CellStates.Floor) next[currstepstep] = CellStates.FloorCrate;
                    if (p_currstepstep == CellStates.FloorGoal) next[currstepstep] = CellStates.FloorGoalCrate;

                    Moves.Push(new Move(Current, moveDir, true));
                    Current = next;

                    // Has the map been solved
                    if (Current.isPuzzleComplete())
                    {
                        Stats.End = DateTime.Now;
                        return MoveResult.ValidPushWin;
                    }

                    // Normal push, or crate onto goal
                    if (next[currstepstep] == CellStates.FloorGoalCrate)
                    {
                        return MoveResult.ValidPushGoal;
                    }

                    return MoveResult.ValidPush;
                }
                else
                {
                    // Cannot push
                    return MoveResult.Invalid;
                }
            }
            return MoveResult.Invalid;
        }

        

        /// <summary>
        /// Test a solution to see if it is valid
        /// </summary>
        /// <param name="aSolution">The solution to replay</param>
        /// <param name="FirstError">A human-readable error string</param>
        public bool Test(Solution aSolution, out string FirstError)
        {
            FirstError = null;
            int cc = 0;
            Direction[] moves = aSolution.ToPath().Moves;
            foreach (Direction move in moves)
            {
                MoveResult res = Move(move);
                if (res == MoveResult.Invalid)
                {
                    FirstError = string.Format("Invalid move at step {0} of {1}", cc, moves.Length);
                    return false;
                }
                if (res == MoveResult.ValidPushWin) return true;

                cc++;
            }

            FirstError = string.Format("Moves do not result in a solution. Check {0} moves.", cc);

            return false;
        }


        /// <summary>
        /// Undo one move
        /// </summary>
        public virtual void Undo()
        {
            if (Moves.Count == 0) return;

            Stats.Undos++;
            Stats.Moves--;

            Move aPop = Moves.Pop();
            if (aPop.isPush)
            {
                Stats.Pushes--;
            }

            Current = aPop.Before;
        }

        /// <summary>
        /// Reset to the start position
        /// </summary>
        public virtual void Reset()
        {
            Stats.Restarts++;
            Stats.Moves = 0;
            Stats.Pushes = 0;
            Current = new SokobanMap(StartPuzzle);
            Moves = new Stack<Move>();
        }

        /// <summary>
        /// Reset to the start position
        /// </summary>
        public virtual void Reset(Bookmark bookMark)
        {
            Stats.Restarts++;
            Stats.Moves = 0;
            Stats.Pushes = 0;
            Current = bookMark.Current;
            Moves = new Stack<Move>();
           
            // Do not allow undo beyond this point
        }

        /// <summary>
        /// The puzzle being played
        /// </summary>
        public Puzzle Puzzle
        {
            get { return puzzle; }
            set { puzzle = value; }
        }

        /// <summary>
        /// The map being played
        /// </summary>
        public SokobanMap StartPuzzle
        {
            get { return startPuzzle; }
            set { startPuzzle = value; }
        }

        /// <summary>
        /// The current map layout
        /// </summary>
        public SokobanMap Current
        {
            get { return current; }
            set { current = value; }
        }

        /// <summary>
        /// Stack of moves resulting in <see cref="Current"/>
        /// </summary>
        public Stack<Move> Moves
        {
            get { return moves; }
            set { moves = value; }
        }

        /// <summary>
        /// Game statistics
        /// </summary>
        public Stats Stats
        {
            get { return stats; }
            set { stats = value; }
        }

        /// <summary>
        /// Add a new bookmark to the game
        /// </summary>
        /// <param name="newBookmark">Snapshot/Waypoint/Bookmark</param>
        public void Add(Bookmark newBookmark)
        {
            bookmarks.Add(newBookmark);
        }

        /// <summary>
        /// Crate a bookmark from the <see cref="Current"/> game map.
        /// </summary>
        /// <returns></returns>
        public Bookmark MakeBookmark()
        {
            Bookmark bk = new Bookmark();
            bk.Current = new SokobanMap(current);
            bk.PlayerMoves = new Path(StartPuzzle.Player);
            foreach(Move move in moves.ToArray())
            {
                bk.PlayerMoves.Add(move.MoveDirection);
            }
            return bk;
        }

        /// <summary>
        /// List of bookmarks of the game 
        /// </summary>
        public List<Bookmark> Bookmarks
        {
            get { return bookmarks; }
        }

        /// <summary>
        /// The game win solution
        /// </summary>
        public EventHandler<NotificationEvent> OnGameWin;


        private Puzzle puzzle;
        private SokobanMap startPuzzle;
        private SokobanMap current;
        private Stack<Move> moves;
        private Stats stats;
        private List<Bookmark> bookmarks;
    }
}
