using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    public class PuzzleMap
    {
        private string mapID;
        private Puzzle puzzle;
        private SokobanMap map;

        private GenericDescription details;
        private List<Solution> solutions;

        public PuzzleMap(Puzzle owner)
        {
            puzzle = owner;
            map = new SokobanMap();
            solutions = new List<Solution>();
        }

        public string MapID
        {
            get { return mapID; }
            set { mapID = value; }
        }

        public Puzzle Puzzle
        {
            get { return puzzle; }
        }

        public SokobanMap Map
        {
            get { return map; }
            set { map = value; }
        }

        public GenericDescription Details
        {
            get { return details; }
            set { details = value; }
        }

        public List<Solution> Solutions
        {
            get { return solutions; }
            set { solutions = value; }
        }

        public bool HasSolution
        {
            get { return solutions != null && solutions.Count > 0;  }
        }
    }
}
