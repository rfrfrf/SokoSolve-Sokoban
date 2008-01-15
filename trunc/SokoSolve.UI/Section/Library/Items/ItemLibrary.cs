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
                List<Category> wrapper = new List<Category>();

                if (DomainData.Categories.Root.Count > 0 && DomainData.Categories.Root.Data.GetPuzzles(DomainData).Count == 0)
                {
                    SyncUICollectionWithData<Category>(DomainData.Categories.Root.ChildrenData, delegate(Category item) { return new ItemCategory(item); });
                }
                else
                {
                    // Show the master list
                    wrapper.Add(DomainData.Categories.Root.Data);
                    SyncUICollectionWithData<Category>(wrapper, delegate(Category item) { return new ItemCategory(item); });
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
                    payload.ucPuzzleList.Library = DomainData;
                    payload.htmlView1.SetHTML( HtmlReporter.Report(DomainData).GetHTMLPage() );
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
