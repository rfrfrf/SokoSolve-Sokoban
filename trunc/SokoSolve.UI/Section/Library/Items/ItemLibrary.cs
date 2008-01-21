using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Controls.Web;

namespace SokoSolve.UI.Section.Library.Items
{
	public class ItemLibrary : ExplorerItemBase<Core.Model.Library>
	{
		public ItemLibrary(Core.Model.Library data) : base(data)
		{
            html.OnCommand += new EventHandler<UIBrowserEvent>(html_OnCommand);
		}


		public override void SyncDomain()
		{
            if (DomainData != null)
            {
                if (DomainData.Categories.Count > 0 && DomainData.CategoryTree.Root.Data.GetPuzzles(DomainData).Count == 0)
                {
                    SyncUICollectionWithData<Category>(DomainData.CategoryTree.Root.ChildrenData, delegate(Category item) { return new ItemCategory(item); });
                }
                else
                {
                    // Show the master list's puzzle only (not the list itself)
                    SyncUICollectionWithData<Puzzle>(DomainData.CategoryTree.Root.Data.GetPuzzles(Controller.Current), delegate(Puzzle item) { return new ItemPuzzle(item); });
                }

                base.SyncDomain();
            }
		    
		}

		public override void BindUI()
		{
            base.BindUI();

			if (DomainData != null)
			{
				TreeViewUINode.Text = string.Format("{0}", DomainData.Details.Name);
			}
			else
			{
				TreeViewUINode.Text = "No library is loaded";
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
                return desc;
                
            }
            else
            {
                //if (!Explorer.DetailPayload.Controls.Contains(html))
                //{
                //    Explorer.DetailPayload.Controls.Clear();
                //    Explorer.DetailPayload.Controls.Add(html);
                //}

                Explorer.SetPayload(payload);

                if (DomainData != null)
                {
                    payload.Library = DomainData;
                    
                }
                return html;
            }
		}

        private PayloadLibrary payload = new PayloadLibrary();


        void html_OnCommand(object sender, UIBrowserEvent e)
        {
            if (e.Command.Scheme == "app")
            {
                this.Explorer.Controller.PerformCommandURI(e.Command);
            }
        }

        HtmlView html = new HtmlView();
        ucGenericDescription desc = new ucGenericDescription();
	}
}
