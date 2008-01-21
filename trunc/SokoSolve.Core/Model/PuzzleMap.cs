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
        private string rating;
        bool? isMasterMap;

        private GenericDescription details;
        private List<Solution> solutions;

        public PuzzleMap(Puzzle owner)
        {
            puzzle = owner;
            map = new SokobanMap();
            solutions = new List<Solution>();
            isMasterMap = null;
        }

        public PuzzleMap(PuzzleMap clone)
        {
            puzzle = clone.puzzle;
            map = new SokobanMap(clone.map);
            solutions = new List<Solution>(clone.solutions);
            details = new GenericDescription(clone.details);
            rating = clone.rating;
            mapID = clone.mapID;
            isMasterMap = clone.IsMasterMap;
        }

        public string MapID
        {
            get { return mapID; }
            set { mapID = value; }
        }

        public string Rating
        {
            get { return rating; }
            set { rating = value; }
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

        /// <summary>
        /// Is this the master map?
        /// </summary>
        public bool IsMasterMap
        {
            get
            {
                if (isMasterMap != null) return isMasterMap.Value;
                return object.ReferenceEquals(puzzle.MasterMap, this);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", mapID, details);
        }
    }
}
