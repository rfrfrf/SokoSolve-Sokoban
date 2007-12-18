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
        private ucGenericDescription desc = new ucGenericDescription();
	    private HtmlView html = new HtmlView();

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
		public override void SyncWithData()
		{
            SyncUICollectionWithData<Category>(Data.Children, delegate(Category item) { return new ItemCategory(item); });

            // Add puzzles
            LibraryController libCont = Explorer.Controller as LibraryController;
            SyncUICollectionWithData<Puzzle>(Data.GetPuzzles(libCont.Current), delegate(Puzzle item) { return new ItemPuzzle(item); });

            // Sync all shildren: chain downward
			foreach (TreeNode<ExplorerItem> kid in TreeNode.Children)
			{
				kid.Data.SyncWithData();
			}
		}

        /// <summary>
        /// Bind UI Model ( <see cref="ExplorerItem"/>) to the TreeView nodes <see cref="UINode"/>
        /// </summary>
		public override void BindToUI()
		{
			if (UINode == null)
			{
				UINode = TreeNode.Parent.Data.UINode.Nodes.Add("new node for " + this.GetType().ToString());
				UINode.Tag = this;
                UINode.ImageIndex = Explorer.Controller.IconBinder.getIcon(IconSizes.Icon, Data);
                UINode.SelectedImageIndex = UINode.ImageIndex;
			
			}

			UINode.Tag = this;
			if (Data != null && Data.Details != null && Data.Details.Name != null)
			{
				UINode.Text = Data.Details.Name;
			}
			else
			{
				UINode.Text = "No category";
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

                    desc.Data = Data.Details;
                }

               
            }
            else
            {
                //BindListView();

                if (Data != null)
                {
                    if (!Explorer.DetailPayload.Controls.Contains(html))
                    {
                        Explorer.DetailPayload.Controls.Clear();
                        Explorer.DetailPayload.Controls.Add(html);
                    }

                    html.Dock = DockStyle.Fill;

                    LibraryController libController = Explorer.Controller as LibraryController;
                    html.SetHTML(HtmlReporter.Report(Data, libController.Current).GetHTMLPage());
                }
            }

		    return desc;
		}

	}

}
