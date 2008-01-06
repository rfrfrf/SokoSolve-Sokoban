using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core
{
    public enum DifficultyRating
    {
        Simple,
        Easy,
        Medium,
        Hard,
        Nuts
    }

    public enum Cell
    {
        Void,
        Wall,
        Floor,
        Goal,
        Crate,
        Player
    }

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

    public class CellStatesClass
    {
        static public int Size = 8;
    }


	


   



   
}
