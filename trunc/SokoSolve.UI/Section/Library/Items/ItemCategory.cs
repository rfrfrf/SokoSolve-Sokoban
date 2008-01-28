using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Model;
using SokoSolve.Common.Structures;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Controls.Web;

namespace SokoSolve.UI.Section.Library.Items
{
	class ItemCategory : ExplorerItemBase<Category>
	{
        private static GenericPayload payload = new GenericPayload();
        private static ucGenericDescription desc = new ucGenericDescription();
	    private static HtmlView html = new HtmlView();

		public ItemCategory(Category data) : base(data)
		{
            html.OnCommand += new EventHandler<UIBrowserEvent>(html_OnCommand);
		}

        void html_OnCommand(object sender, UIBrowserEvent e)
        {
            if (e.Command.Scheme == "app")
            {
                this.Explorer.Controller.PerformCommandURI(e.Command);
            }
        }

      
        /// <summary>
        /// Syncronise the model data <see cref="DataUnTyped"/> children with <see cref="ExplorerItem"/> (the UI Model)
        /// </summary>
		public override void SyncDomain()
		{
            List<Category> cats = DomainData.Children == null ? null : new List<Category>(DomainData.Children);
            if (cats != null) cats.Sort(delegate(Category lhs, Category rhs) { return lhs.NestedOrder.CompareTo(rhs.NestedOrder); });
            SyncUICollectionWithData<Category>(cats, delegate(Category item) { return new ItemCategory(item); });

            // Add puzzles
            LibraryController libCont = Explorer.Controller as LibraryController;
            SyncUICollectionWithData<Puzzle>(DomainData.GetPuzzles(), delegate(Puzzle item) { return new ItemPuzzle(item); });

            // Add sub-categories
            base.SyncDomain();
		}

        /// <summary>
        /// Bind UI Model ( <see cref="ExplorerItem"/>) to the TreeView nodes <see cref="UINode"/>
        /// </summary>
		public override void BindUI()
		{
            base.BindUI();

			if (DomainData != null && DomainData.Details != null && DomainData.Details.Name != null)
			{
				TreeViewUINode.Text = DomainData.Details.Name;
			}
			else
			{
				TreeViewUINode.Text = "No category";
			}
		}

	   

		public override Control ShowDetail()
		{
            if (IsEditable)
            {
                if (!Explorer.DetailPayload.Controls.Contains(desc))
                {
                    Explorer.DetailPayload.Controls.Clear();
                    Explorer.DetailPayload.Controls.Add(desc);
                    desc.Dock = DockStyle.Fill;

                    desc.Data = DomainData.Details;
                }

               
            }
            else
            {
                //BindListView();

                if (DomainData != null)
                {
                    if (!Explorer.DetailPayload.Controls.Contains(html))
                    {
                        Explorer.DetailPayload.Controls.Clear();
                        Explorer.DetailPayload.Controls.Add(html);
                    }

                    html.Dock = DockStyle.Fill;

                    LibraryController libController = Explorer.Controller as LibraryController;
                    html.SetHTML(HtmlReporter.Report(DomainData, libController.Current).GetHTMLPage());
                }
            }

		    return desc;
		}

	}

}
