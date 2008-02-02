using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Section;
using SokoSolve.UI.Section.Library;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Controls.Primary
{
    /// <summary>
    /// Library block. The central ui component for the library. It host the explorer pattern, menu and payloads
    /// </summary>
	public partial class Library : UserControl
	{
		private LibraryController controller;
		private LibraryExplorer explorer;

        public Library()
        {
            InitializeComponent();

            if (Site == null)
            {
                // Only when not in designer mode
                controller = new LibraryController(this);
                controller.IconBinder.SetImageList(IconSizes.Small, imageListSmall);
                controller.IconBinder.SetImageList(IconSizes.Icon, imageListIcon);
                controller.IconBinder.SetImageList(IconSizes.Thumbnail, imageListThumbnail);

                explorer = new LibraryExplorer(controller, treeViewLibrary, splitContainer1.Panel2);
            }
        }

        /// <summary>
        /// Initalise the library, this is essentially the set method for <see cref="CurrentLibrary"/>
        /// </summary>
        /// <param name="current"></param>
        public void InitLibrary(SokoSolve.Core.Model.Library current)
	    {
            using (CodeTimer timer = new CodeTimer("Library.InitLibray(...)"))
            {
	            
                explorer.Clear();
                controller.Current = current;
                explorer.SyncDomain(new ItemLibrary(controller.Current));
                explorer.SyncUI();
                explorer.TreeView.ExpandAll();
            }
	    }

        /// <summary>
        /// Current controller library data
        /// </summary>
        public SokoSolve.Core.Model.Library CurrentLibrary
        {
            get
            {
                return controller.Current;
            }
        }

        public override void Refresh()
        {
            
            if (explorer != null)
            {
                explorer.Refresh();
            }
            base.Refresh();
        }

        private void treeViewLibrary_DragDrop(object sender, DragEventArgs e)
        {
            Rectangle treeViewPos = treeViewLibrary.RectangleToScreen(treeViewLibrary.DisplayRectangle);
            TreeNode targetNode = treeViewLibrary.GetNodeAt(e.X - treeViewPos.X, e.Y - treeViewPos.Y);
            if (targetNode != null)
            {
                ItemCategory targetCat = targetNode.Tag as ItemCategory;
                if (targetCat != null)
                {
                    //Move a puzzle
                    Puzzle sourcePuzzle = controller.Current.GetPuzzleByID((string)e.Data.GetData(typeof (string)));
                    if (sourcePuzzle != null)
                    {
                        sourcePuzzle.Category = targetCat.DomainData;
                        explorer.Refresh();
                        return;
                    }

                    Category sourceCat = controller.Current.GetCategoryByID((string)e.Data.GetData(typeof(string)));
                    if (sourceCat != null)
                    {
                        // Remove from cat
                        controller.Current.CategoryTree.Move(sourceCat.TreeNode, targetCat.DomainData.TreeNode);
                        sourceCat.CategoryParentREF =  targetCat.DomainData.CategoryID;
                        explorer.Refresh();
                        return;
                    }

                    return;
                }
            }
        }

        private void treeViewLibrary_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode item = e.Item as TreeNode;
            if (item != null)
            {
                ItemPuzzle itemPuzzle = item.Tag as ItemPuzzle;
                if (itemPuzzle != null)
                {
                    treeViewLibrary.DoDragDrop(itemPuzzle.DomainData.PuzzleID, DragDropEffects.Move);
                    return;
                }

                ItemCategory cat = item.Tag as ItemCategory;
                if (cat != null)
                {
                    treeViewLibrary.DoDragDrop(cat.DomainData.CategoryID, DragDropEffects.Move);
                    return;
                }
            }
        }

     

        private void treeViewLibrary_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                string data = (string)e.Data.GetData(typeof (string));
                if (data != null)
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (treeViewLibrary.SelectedNode != null)
            {

                ItemPuzzle puzzle = treeViewLibrary.SelectedNode.Tag as ItemPuzzle;
                if (puzzle != null)
                {
                    Clipboard.SetText(puzzle.DomainData.MasterMap.Map.ToString(), TextDataFormat.UnicodeText);   
                    return;
                }

                Clipboard.SetText("<html><body><p>SokoSolve not implemented</p></body></html>", TextDataFormat.Html);
                return;

            }
        }
	}
}
