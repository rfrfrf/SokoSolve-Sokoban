using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core;
using SokoSolve.Core.Model;
using SokoSolve.UI.Section;

namespace SokoSolve.UI.Controls.Secondary
{
    /// <summary>
    /// Provide a basic type-strong list of puzzles (sortable)
    /// </summary>
    public partial class ucPuzzleList : UserControl
    {
        public ucPuzzleList()
        {
            InitializeComponent();

            currentSort = SortOrder;
        }

        /// <summary>
        /// Use check boxes
        /// </summary>
        [Browsable(true)]
        public bool UseCheckBoxes
        {
            get { return listViewPuzzles.CheckBoxes;}
            set { listViewPuzzles.CheckBoxes = value; }
        }

        /// <summary>
        /// Bind to the list
        /// </summary>
        public void Bind()
        {
            listViewPuzzles.Items.Clear();

            if (library == null)
            {
                
            }
            else
            {
                listViewPuzzles.BeginUpdate();

                // Add categories
                Dictionary<string, ListViewGroup> categories = new Dictionary<string, ListViewGroup>();
                List<Category> cats = library.Categories;
                listViewPuzzles.Groups.Clear();
                foreach (Category category in cats)
                {
                    ListViewGroup grp = new ListViewGroup();
                    grp.Header = string.Format("Category: {0} ({1} puzzles)", category.Details.Name, category.GetPuzzles().Count, category.NestedOrder);
                    grp.Tag = category;
                    categories.Add(category.CategoryID, grp);
                    listViewPuzzles.Groups.Add(grp);
                }

                List<Puzzle> puzzles = library.Puzzles;  
                puzzles.Sort(currentSort);

                foreach (Puzzle puzzle in puzzles)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = puzzle;
                    item.Text = puzzle.Details.Name;
                    if (puzzle.Category != null)
                        item.Group = categories[puzzle.Category.CategoryID];
                    item.SubItems.Add(puzzle.Order.ToString());
                    item.SubItems.Add(puzzle.AutomatedRating.ToString("0.0"));
                    item.SubItems.Add(BuildDescription(puzzle));
                    listViewPuzzles.Items.Add(item);
                }

                listViewPuzzles.EndUpdate();
            }

           
        }

        private string BuildDescription(Puzzle puzzle)
        {
            return string.Format("{0}, {1} crates, {2} solutions. {3}", 
                puzzle.MasterMap.Map.Size, 
                puzzle.MasterMap.Map.Count(Cell.Crate), 
                puzzle.MasterMap.Solutions.Count,
                puzzle.Rating);
        }

        /// <summary>
        /// Current library
        /// </summary>
        [Browsable(false)]
        public Library Library
        {
            get { return library; }
            set 
            { 
                library = value;
                Bind(); 
            }
        }

        /// <summary>
        /// Show the preview pane
        /// </summary>
        [Browsable(true)]
        public bool ShowPreview
        {
            get
            {
                return !this.splitContainer.Panel2Collapsed;
            }
            set
            {
                this.splitContainer.Panel2Collapsed = !value;
            }
            
        }

        /// <summary>
        /// Show the groupings
        /// </summary>
        [Browsable(true)]
        public bool ShowGroups
        {
            get
            {
                return listViewPuzzles.ShowGroups;
            }
            set
            {
                listViewPuzzles.ShowGroups = value;
            }

        }

        public void Select(Puzzle puzzle)
        {
            foreach (ListViewItem item in listViewPuzzles.Items)
            {
                Puzzle puz = item.Tag as Puzzle;
                if (puz != null && puz == puzzle)
                {
                    if (UseCheckBoxes)
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Selected = true;    
                    }
                    
                }
            }
        }

        Comparison<Puzzle> currentSort;

        int SortOrder(Puzzle lhs, Puzzle rhs)
        {
            return lhs.Order.CompareTo(rhs.Order);
        }

        int SortRating(Puzzle lhs, Puzzle rhs)
        {

            return lhs.AutomatedRating.CompareTo(rhs.AutomatedRating);    
 
        }

        int SortName(Puzzle lhs, Puzzle rhs)
        {
            return lhs.Details.Name.CompareTo(rhs.Details.Name);
        }

        /// <summary>
        /// Currently selected puzzles
        /// </summary>
        public List<Puzzle> Selected
        {
            get
            {
                List<Puzzle> result = new List<Puzzle>();
             
                if (UseCheckBoxes)
                {
                    foreach (ListViewItem item in listViewPuzzles.CheckedItems)
                    {
                        Puzzle puz = item.Tag as Puzzle;
                        if (puz != null)
                        {
                            result.Add(puz);
                        }
                    }
                }
                else
                {
                    foreach (ListViewItem item in listViewPuzzles.SelectedItems)
                    {
                        Puzzle puz = item.Tag as Puzzle;
                        if (puz != null)
                        {
                            result.Add(puz);
                        }
                    }
                }
                
                return result;
            }
        }

        /// <summary>
        /// Currently selected puzzles
        /// </summary>
        public List<PuzzleMap> SelectedPuzzleMaps
        {
            get
            {
                List<PuzzleMap> result = new List<PuzzleMap>();


                if (UseCheckBoxes)
                {
                    foreach (ListViewItem item in listViewPuzzles.CheckedItems)
                    {
                        Puzzle puz = item.Tag as Puzzle;
                        if (puz != null)
                        {
                            result.Add(puz.MasterMap);
                        }
                        PuzzleMap puzMap = item.Tag as PuzzleMap;
                        if (puzMap != null)
                        {
                            result.Add(puzMap);
                        }
                    }
                }
                else
                {
                    foreach (ListViewItem item in listViewPuzzles.SelectedItems)
                    {
                        Puzzle puz = item.Tag as Puzzle;
                        if (puz != null)
                        {
                            result.Add(puz.MasterMap);
                        }
                        PuzzleMap puzMap = item.Tag as PuzzleMap;
                        if (puzMap != null)
                        {
                            result.Add(puzMap);
                        }
                    }
                }

                return result;
            }
        }

        private SokoSolve.Core.Model.Library library;

        private void listViewPuzzles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listViewPuzzles.SelectedItems.Count > 0)
            {
                object obj = listViewPuzzles.SelectedItems[0].Tag;
                Puzzle puz = obj as Puzzle;
                if (puz != null)
                {
                    pictureBoxMap.Image = DrawingHelper.DrawPuzzle(puz.MasterMap);    
                }
                PuzzleMap puzMap = obj as PuzzleMap;
                if (puzMap != null)
                {
                    pictureBoxMap.Image = DrawingHelper.DrawPuzzle(puzMap);    
                }
            }      
        }

        private void listViewPuzzles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch(e.Column)
            {
                case (0): currentSort = SortName;
                    break;

                case (1): currentSort = SortOrder;
                    break;

                case (2): currentSort = SortRating;
                    break;


                default: currentSort = SortOrder; break;
            }

            Bind();
        }


        /// <summary>
        /// Select everything
        /// </summary>
        public void SelectAll()
        {
                foreach (ListViewItem item in listViewPuzzles.Items)
                {
                    if (UseCheckBoxes) item.Checked = true;
                    else item.Selected = true;
                } 
        }
    }
}
