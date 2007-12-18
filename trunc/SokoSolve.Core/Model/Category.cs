using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.Model
{
    public class Category : ITreeNodeBackReference<Category>
    {
        private GenericDescription details;
        private string categoryID;
        private string categoryParentREF;
        private TreeNode<Category> treeNode;


        public Category()
        {
            details = new GenericDescription();
        }

        public GenericDescription Details
        {
            get { return details; }
            set { details = value; }
        }


        public string CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }


        public string CategoryParentREF
        {
            get { return categoryParentREF; }
            set { categoryParentREF = value; }
        }

        public List<Puzzle> GetPuzzles(Library library)
        {
            return library.Puzzles.FindAll(delegate(Puzzle item) { return item.Category == this; });
        }

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
    }
}
