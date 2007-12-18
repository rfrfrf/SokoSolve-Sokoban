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
        private IDProvider idProvider;


    	public Library()
    	{
    	    idProvider = new IDProvider(0);

    		puzzles = new List<Puzzle>();
    		categories = new Tree<Category>();
    		details = new GenericDescription();
    		details.Name = "unnamed library";
    		details.Description = "a blank library created by SokoSolve";

    		categories.Top.Data  = new Category();
    		categories.Top.Data.Details.Name = "Master List";
    		categories.Top.Data.CategoryID = "C"+idProvider.GetNextID().ToString();
    	}

    	public List<Puzzle> Puzzles
    	{
    		get { return puzzles; }
    		set { puzzles = value; }
    	}

    	public Tree<Category> Categories
    	{
    		get { return categories; }
    		set { categories = value; }
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


        public IDProvider IdProvider
        {
            get { return idProvider; }
        }
    }

	



}
