using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core
{
    /// <summary>
    /// Puzzle Difficulty Rating
    /// </summary>
    public enum DifficultyRating
    {
        /// <summary>
        /// Simple, solved within minutes
        /// </summary>
        Easy,

        /// <summary>
        /// Requires a little thought for an experiance user
        /// </summary>
        Medium,

        /// <summary>
        /// Taxing, even for an experiances user
        /// </summary>
        Hard,

        /// <summary>
        /// Expert user only
        /// </summary>
        Insane
    }

    /// <summary>
    /// Puzzle Cells
    /// </summary>
    public enum Cell
    {
        Void,
        Wall,
        Floor,
        Goal,
        Crate,
        Player
    }

    /// <summary>
    /// Cell Stats (a position may have many states)
    /// </summary>
    public enum CellStates
    {
        Void,
        Wall,
        Floor,
        FloorCrate,
        FloorGoal,
        FloorGoalCrate,
        FloorPlayer,
        FloorGoalPlayer
    }

    /// <summary>
    /// Helper class
    /// </summary>
    public class CellStatesClass
    {
        static public int Size = 8;
    }


	


   



   
}
