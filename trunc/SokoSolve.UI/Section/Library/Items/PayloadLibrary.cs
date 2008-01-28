using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Model;
using SokoSolve.UI.Controls.Web;

namespace SokoSolve.UI.Section.Library.Items
{
    public partial class PayloadLibrary : UserControl
    {
        public PayloadLibrary()
        {
            InitializeComponent();
        }

        public SokoSolve.Core.Model.Library Library
        {
            get { return ucPuzzleList.Library;  }
            set 
            { 
                ucPuzzleList.Library = value;
                htmlView1.SetHTML(HtmlReporter.Report(value).GetHTMLPage());
                BindCategories();
            }
        }

        private void BindCategories()
        {
            listViewCat.BeginUpdate();
            listViewCat.Items.Clear();

            List<Category> categories = Library.Categories;
            
            foreach (Category category in categories )
            {
                ListViewItem item = new ListViewItem();
                item.Tag = category;
                item.Text = category.NestedOrder.ToString();
                item.SubItems.Add(category.Details.Name);
                item.SubItems.Add(category.GetPuzzles().Count.ToString());
                listViewCat.Items.Add(item);
            }
            listViewCat.EndUpdate();
        }

        private void tsbPreview_Click(object sender, EventArgs e)
        {
            ucPuzzleList.ShowPreview = tsbPreview.Checked;
        }

        private void tsbOrderUp_Click(object sender, EventArgs e)
        {
            List<Puzzle> sel =  ucPuzzleList.Selected;
            if (sel.Count > 0)
            {
                int idx = ucPuzzleList.Library.Puzzles.IndexOf(sel[0]);
                int idxNext = idx - 1;
                if (idxNext >= 0)
                {
                    Puzzle curr = ucPuzzleList.Library.Puzzles[idx];
                    Puzzle next = ucPuzzleList.Library.Puzzles[idxNext];
                    int order = curr.Order;
                    curr.Order = next.Order;
                    next.Order = order;
                    ucPuzzleList.Bind();
                }
            }
        }

        private void tsbOrderDown_Click(object sender, EventArgs e)
        {
            List<Puzzle> sel = ucPuzzleList.Selected;
            if (sel.Count > 0)
            {
                int idx = ucPuzzleList.Library.Puzzles.IndexOf(sel[0]);
                int idxNext = idx + 1;
                if (idxNext < ucPuzzleList.Library.Puzzles.Count)
                {
                    Puzzle curr = ucPuzzleList.Library.Puzzles[idx];
                    Puzzle next = ucPuzzleList.Library.Puzzles[idxNext];
                    int order = curr.Order;
                    curr.Order = next.Order;
                    next.Order = order;
                    ucPuzzleList.Bind();
                }
            }
        }

        private void tsbShowGroups_Click(object sender, EventArgs e)
        {
            ucPuzzleList.ShowGroups = tsbShowGroups.Checked;
        }

        private void tsbCatUp_Click(object sender, EventArgs e)
        {
            if (listViewCat.SelectedItems.Count == 0) return;

            // Are the two nodes siblings
            int itemIdx = listViewCat.SelectedIndices[0];
            int nextIdx = itemIdx - 1;
            if (nextIdx < 0) return;

            Category item = listViewCat.Items[itemIdx].Tag as Category;
            Category next = listViewCat.Items[nextIdx].Tag as Category;
            if (item == null || next == null) return;

            if (!item.TreeNode.IsSibling(next.TreeNode)) return;

            int tmp = item.Order;
            item.Order = next.Order;
            next.Order = tmp;

            BindCategories();
        }

        private void tsbCatDown_Click(object sender, EventArgs e)
        {
            if (listViewCat.SelectedItems.Count == 0) return;

            // Are the two nodes siblings
            int itemIdx = listViewCat.SelectedIndices[0];
            int nextIdx = itemIdx + 1;
            if (nextIdx >= listViewCat.Items.Count) return;

            Category item = listViewCat.Items[itemIdx].Tag as Category;
            Category next = listViewCat.Items[nextIdx].Tag as Category;
            if (item == null || next == null) return;

            if (!item.TreeNode.IsSibling(next.TreeNode)) return;

            int tmp = item.Order;
            item.Order = next.Order;
            next.Order = tmp;

            BindCategories();
        }
    }
}
