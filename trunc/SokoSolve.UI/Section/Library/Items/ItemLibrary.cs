using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
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

        void html_OnCommand(object sender, UIBrowserEvent e)
        {
            if (e.Command.Scheme == "app")
            {
                this.Explorer.Controller.PerformCommandURI(e.Command);
            }
        }


		public override void SyncWithData()
		{
			if (Data != null)
			{
				if (Data.Categories != null)
				{
					TreeNode.Add(new ItemCategory(Data.Categories.Top.Data));
					foreach (TreeNode<ExplorerItem> kid in TreeNode.Children)
					{
						kid.Data.SyncWithData();
					}
				}
			}
		}


		public override void BindToUI()
		{
			if (Data != null)
			{
				UINode.Text = string.Format("{0}", Data.Details.Name);
			}
			else
			{
				UINode.Text = "No library is loaded";
			}
            UINode.ImageIndex = Explorer.Controller.IconBinder.getIcon(IconSizes.Icon, Data);
            UINode.SelectedImageIndex = UINode.ImageIndex;
			
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
                return desc;
                
            }
            else
            {
                if (!Explorer.DetailPayload.Controls.Contains(html))
                {
                    Explorer.DetailPayload.Controls.Clear();
                    Explorer.DetailPayload.Controls.Add(html);
                }

                if (Data != null)
                {
                    html.Dock = DockStyle.Fill;
                    html.SetHTML( HtmlReporter.Report(Data).GetHTMLPage() );
                }
                return html;
            }
		}

        HtmlView html = new HtmlView();
        ucGenericDescription desc = new ucGenericDescription();
	}
}
