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


	class ItemPuzzleMap : ExplorerItemBase<PuzzleMap>
	{
		public ItemPuzzleMap(PuzzleMap data)
			: base(data)
		{
		}

		public override void SyncWithData()
		{
			foreach (Solution solution in Data.Solutions)
			{
				TreeNode.Add(new ItemSolution(solution));
			}

			foreach (TreeNode<ExplorerItem> kid in TreeNode.Children)
			{
				kid.Data.SyncWithData();
			}
		}

		public override void BindToUI()
		{
			if (UINode == null)
			{
				UINode = TreeNode.Parent.Data.UINode.Nodes.Add("new node for " + this.GetType().ToString());
				UINode.Tag = this;
                UINode.ImageIndex = Explorer.Controller.IconBinder.getIcon(IconSizes.Small, Data);
                UINode.SelectedImageIndex = UINode.ImageIndex;
			
			}

			UINode.Tag = this;
			if (Data != null)
			{
				UINode.Text = string.Format("map {0}", Data.Details);
			}
			else
			{
				UINode.Text = "No puzzle";
			}
		}

	    private static HtmlView html = new HtmlView();

		public override Control ShowDetail()
		{
			if (Data != null)
			{
                if (!Explorer.DetailPayload.Controls.Contains(html))
				{
                    Explorer.DetailPayload.Controls.Add(html);
				}

                html.Dock = DockStyle.Fill;
				StringBuilder sb = new StringBuilder();

			    sb.Append("Not implemented");

			    html.SetHTML(sb.ToString());
			}

            return Explorer.DetailPayload;
		}

        
	}

}
