using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SokoSolve.Core.Model.Analysis;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    /// <summary>
    /// This class extendes the basic SokobanMap and adds additional 'management' information.
    /// </summary>
    /// <remarks>
    /// Like SokobanMap, no game logic is included here.
    /// </remarks>
    public class Puzzle 
    {
    	private List<PuzzleMap> maps;
		private GenericDescription details;
		private int order; 
		private Category category;
    	private string rating;
        private string puzzleID;
        private Library library; // simple back reference

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="myLibrary"></param>
    	public Puzzle(Library myLibrary)
    	{
    		maps = new List<PuzzleMap>();
    		details = new GenericDescription();
    	    library = myLibrary;
    	}

        /// <summary>
        /// Use Library.IDProvider to set
        /// </summary>
        public string PuzzleID
        {
            get { return puzzleID; }
            set { puzzleID = value; }
        }

       /// <summary>
       /// Simple ownershio back-ref
       /// </summary>
        public Library Library
        {
            get { return library; }
            set { library = value;  }
        }

        /// <summary>
        /// Member category
        /// </summary>
        public Category Category
		{
			get { return category; }
			set { category = value; }
		}

        /// <summary>
        /// Order (independant of category)
        /// </summary>
    	public int Order
    	{
    		get { return order; }
    		set { order = value; }
    	}

        /// <summary>
        /// List of all maps in the puzzle
        /// </summary>
    	public List<PuzzleMap> Maps
    	{
    		get { return maps; }
    		set { maps = value; }
    	}

        /// <summary>
        /// List of alternatives
        /// </summary>
        public List<PuzzleMap> Alternatives
        {
            get
            {
                if (!HasAlternatives) return null;
                List<PuzzleMap> alt = new List<PuzzleMap>(maps);
                alt.RemoveAt(0);
                return alt;
            }
        }

        /// <summary>
        /// Puzzle generic details, name, author etc.
        /// </summary>
    	public GenericDescription Details
    	{
    		get { return details; }
    		set { details = value; }
    	}

        /// <summary>
        /// Rating String
        /// </summary>
    	public string Rating
    	{
    		get { return rating; }
    		set { rating = value; }
    	}

        /// <summary>
        /// Calculate an automated rating for the puzzle
        /// </summary>
        public double AutomatedRating
        {
            get
            {
                if (MasterMap != null)
                {
                    return PuzzleAnalysis.CalcRating(MasterMap.Map);
                }
                return 0;
            }
        }

        /// <summary>
        /// Master/Primary map for the puzzle
        /// </summary>
        public PuzzleMap MasterMap
        {
            get
            {
                if (maps != null && maps.Count > 0) return maps[0];
                return null;
            }
            set
            {
                if (maps.Count ==0 && value != null)
                {
                    maps.Add(value);
                    return;
                }

                if (maps.Count > 0)
                {
                    maps[0] = value;
                }
            }
        }

        /// <summary>
        /// Are there alternative puzzle maps?
        /// </summary>
        public bool HasAlternatives
        {
            get
            {
                return maps.Count > 1;
            }
        }

        /// <summary>
        /// Debug string helper
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", puzzleID, details);
        }
    }
}
