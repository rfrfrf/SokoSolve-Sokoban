using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Game
{
    /// <summary>
    /// Encapsultes the basic game logic
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="aMap"></param>
        public Game(Puzzle aPuzzle, SokobanMap aMap)
        {
            if (aMap == null) throw new ArgumentNullException("aPuz");
            StringCollection sc = null;
            if (!aMap.isValid(out sc)) throw new Exception(sc[0]);

            Puzzle = aPuzzle;
            StartPuzzle = aMap;
            Current = new SokobanMap(StartPuzzle);
            Moves = new Stack<Move>();
            
            Stats = new Stats();
            Stats.Start = DateTime.MinValue;
            Stats.End = DateTime.MinValue;
        }


        /// <summary>
        /// Result of a move
        /// </summary>
        public enum MoveResult
        {
            Invalid,
            ValidMove,
            ValidPush,
            ValidPushGoal,
            ValidPushWin
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

        public Puzzle Puzzle;
        public SokobanMap StartPuzzle;
        public SokobanMap Current;
        public Stack<Move> Moves;
        public DateTime Start;
        public DateTime End;
        public Stats Stats;
        public EventHandler OnGameWin;
    }
}
