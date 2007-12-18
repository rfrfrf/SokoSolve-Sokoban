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
	class ItemSolution : ExplorerItemBase<Solution>
	{
		public ItemSolution(Solution data) : base(data)
		{
		}

		public override void SyncWithData()
		{

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
                UINode.Text = Data.Details.Name;
            }
            else
            {
                UINode.Text = "No solution";
            }
		}

        public override Control ShowDetail()
        {
            if (IsEditable)
            {
                if (!Explorer.DetailPayload.Controls.Contains(properties))
                {
                    Explorer.DetailPayload.Controls.Clear();
                    Explorer.DetailPayload.Controls.Add(properties);
                    properties.Dock = DockStyle.Fill;

                    properties.Data = Data.Details;
                }

                return properties;
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
                    html.SetHTML(HtmlReporter.Report(Data.Details).GetHTMLPage());
                }
                return html;
            }
        }

        static HtmlView html = new HtmlView();
        private static ucGenericDescription properties = new ucGenericDescription();
	}

}
