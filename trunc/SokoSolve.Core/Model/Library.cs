using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    
    public class Library 
    {
    	private List<Puzzle> puzzles;
    	private Tree<Category> categories;
		private GenericDescription details;
    	private string rating;
        private string libraryID;
        private IDProvider idProvider;
        private string fileName;


    	public Library(Guid LibraryGUID)
    	{
    	    idProvider = new IDProvider(0);

    		puzzles = new List<Puzzle>();
    		categories = new Tree<Category>();
    		details = new GenericDescription();
    		details.Name = "unnamed library";
    		details.Description = "a blank library created by SokoSolve";

    	    libraryID = LibraryGUID.ToString();

    		categories.Root.Data  = new Category(this);
    		categories.Root.Data.Details.Name = "Master List";
    		categories.Root.Data.CategoryID = "C"+idProvider.GetNextID().ToString();
    	}

        /// <summary>
        /// This is normally a GUID for universal identity
        /// </summary>
        public string LibraryID
        {
            get { return libraryID; }
            set { libraryID = value; }
        }

        public List<Puzzle> Puzzles
    	{
    		get { return puzzles; }
    		set
    		{
    		    puzzles = value;
                puzzles.Sort(delegate(Puzzle lhs, Puzzle rhs) { return lhs.Order.CompareTo(rhs.Order); });
    		}
    	}

    	public Tree<Category> CategoryTree
    	{
    		get { return categories; }
    		set { categories = value; }
    	}

        public List<Category> Categories
        {
            get
            {
                 List<Category> cats = categories.Nodes;
                 cats.Sort(delegate(Category lhs, Category rhs) { return lhs.NestedOrder.CompareTo(rhs.NestedOrder); });
                return cats;
            }
        }

    	public GenericDescription Details
    	{
    		get { return details; }
    		set { details = value; }
    	}

    	public string Rating
    	{
    		get { return rating; }
    		set { rating = value; }
    	}

        /// <summary>
        /// Return the current file from which this library was loaded. Ie. This is not stored in XML
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Provide new ID for sub-nodes in the library
        /// </summary>
        public IDProvider IdProvider
        {
            get { return idProvider; }
            set { idProvider = value; }
        }

        /// <summary>
        /// Get a puzzle by its ID
        /// </summary>
        /// <param name="PuzzleID"></param>
        /// <returns></returns>
        public Puzzle GetPuzzleByID(string PuzzleID)
        {
            return puzzles.Find(delegate(Puzzle item) { return item.PuzzleID == PuzzleID; });
        }

        /// <summary>
        /// Get a Category by its ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public Category GetCategoryByID(string CategoryID)
        {
            TreeNode<Category> findC = categories.Root.Find(delegate(TreeNode<Category> item) { return item.Data.CategoryID == CategoryID; }, int.MaxValue);
            if (findC == null) return null;
            return findC.Data;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", libraryID, details);
        }

        /// <summary>
        /// Get the next puzzle
        /// </summary>
        /// <param name="puzzle"></param>
        /// <returns>null mean nothing next</returns>
        public Puzzle Next(Puzzle puzzle)
        {
            SortByOrder();
            int idx = puzzles.IndexOf(puzzle);
            idx ++;
            if (idx < puzzles.Count - 1) return puzzles[idx];
            return null;
        }

        /// <summary>
        /// Make sure all puzzles have a unique order
        /// </summary>
        public void EnsureOrder()
        {
            SortByOrder();
            int cc = 1;
            foreach (Puzzle puzzle in puzzles)
            {
                puzzle.Order = cc++;
            }

            List<Category> cats = categories.Nodes;
            cats.Sort(delegate(Category lhs, Category rhs) { return lhs.NestedOrder.CompareTo(rhs.NestedOrder); });
            cc =1;
            foreach (Category cat in cats)
            {
                cat.Order = cc++;
            }
        }

        /// <summary>
        /// Sort all puzzles by order
        /// </summary>
        public void SortByOrder()
        {
            puzzles.Sort(delegate(Puzzle lhs, Puzzle rhs) { return lhs.Order.CompareTo((rhs.Order)); });
            
        }

        public GenericDescription Consolidate(Puzzle puzzle)
        {
            GenericDescription res = new GenericDescription(puzzle.Details);
            if (string.IsNullOrEmpty(res.Name)) res.Name = details.Name;
            if (string.IsNullOrEmpty(res.Description)) res.Description = details.Description;
            if (string.IsNullOrEmpty(res.Comments)) res.Comments = details.Comments;
            if (res.Author == null)
            {
                res.Author = details.Author;
            }
            else
            {
                if (string.IsNullOrEmpty(res.Author.Name)) res.Author.Name = details.Author.Name;
            }
            if (!res.DateSpecified && details.DateSpecified)
            {
                res.DateSpecified = true;
                res.Date = details.Date;
            }

            return res;
        }

        public GenericDescription GetDetails()
        {
            return details;
        }
    }

	



}
