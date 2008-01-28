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
                BeginDomainSync();

                // Never show the master list
                SyncUICollectionWithData<Category>(DomainData.CategoryTree.Root.ChildrenData, delegate(Category item) { return new ItemCategory(item); });

                // Show the master list's puzzle only (not the list itself)
                SyncUICollectionWithData<Puzzle>(DomainData.CategoryTree.Root.Data.GetPuzzles(), delegate(Puzzle item) { return new ItemPuzzle(item); });

                EndDomainSync();

                base.SyncDomain();
            }
		    
		}

	    private void EndDomainSync()
	    {
            if (TreeNode.HasChildren)
            {
                List<ExplorerItem> exp = new List<ExplorerItem>();
                foreach (ExplorerItem explorerItem in TreeNode.ChildrenData)
                {
                    if (!explorerItem.Synced) exp.Add(explorerItem);
                }
                exp.ForEach(TreeNode.RemoveChild);
            }
	    }

	    private void BeginDomainSync()
	    {
            if (TreeNode.HasChildren)
            {
                foreach (ExplorerItem explorerItem in TreeNode.ChildrenData)
                {
                    explorerItem.Synced = false;
                }
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

        static HtmlView html = new HtmlView();
        static ucGenericDescription desc = new ucGenericDescription();
	}
}
