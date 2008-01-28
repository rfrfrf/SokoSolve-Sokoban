using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    /// <summary>
    /// Simple Folder structure for categories, which are an organising system
    /// </summary>
    public class Category : ITreeNodeBackReference<Category>
    {
        private GenericDescription details;
        private string categoryID;
        private string categoryParentREF;
        private TreeNode<Category> treeNode;
        private int order;
        private Library library;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="myLib"></param>
        public Category(Library myLib)
        {
            library = myLib;
            details = new GenericDescription();
        }


        public Library Library
        {
            get { return library; }
        }

        /// <summary>
        /// Description
        /// </summary>
        public GenericDescription Details
        {
            get { return details; }
            set { details = value; }
        }


        /// <summary>
        /// Order for display
        /// </summary>
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary>
        /// Global order allowing for tree structure
        /// </summary>
        public string NestedOrder
        {
            get 
            {
                if (treeNode.IsRoot) return order.ToString("00");
                else return treeNode.Parent.Data.NestedOrder + "." + order.ToString("00");
             }
        }

        /// <summary>
        /// ID string
        /// </summary>
        public string CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }


        /// <summary>
        /// Parent string (cached from treenode, allows the categories to be assembled from XML)
        /// </summary>
        public string CategoryParentREF
        {
            get { return categoryParentREF; }
            set { categoryParentREF = value; }
        }

        /// <summary>
        /// Get all puzzles assigned to this category
        /// </summary>
        /// <returns></returns>
        public List<Puzzle> GetPuzzles()
        {
            return library.Puzzles.FindAll(delegate(Puzzle item) { return item.Category == this; });
        }

        /// <summary>
        /// Children/Sub categories
        /// </summary>
        public Category[] Children
        {
            get
            {
                return treeNode.ChildrenData;
            }
        }

        #region ITreeNodeBackReference<Category> Members

        /// <summary>
        /// Allow the data payload to have a referrence with its structure
        /// </summary>
        public TreeNode<Category> TreeNode
        {
            get { return treeNode; }
            set { treeNode = value; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1}", categoryID, details);
        }

        /// <summary>
        /// Delete *this* category, all sub-categories and all puzzles
        /// </summary>
        public void Delete()
        {
            if (treeNode.IsRoot) return;

            // Remove puzzles
            if (treeNode.HasChildren)
            {
                treeNode.ForEach(delegate(TreeNode<Category> item)
                {
                    foreach (Puzzle catPuz in item.Data.GetPuzzles())
                    {
                        library.Puzzles.Remove(catPuz);
                    }
                }, int.MaxValue);
            }

            foreach (Puzzle catPuz in GetPuzzles())
            {
                library.Puzzles.Remove(catPuz);
            }
            
            // Remove category
            treeNode.Parent.RemoveChild(this);

        }
    }
}
