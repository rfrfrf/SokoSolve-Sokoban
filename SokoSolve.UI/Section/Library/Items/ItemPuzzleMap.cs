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

		public override void SyncDomain()
		{
			foreach (Solution solution in DomainData.Solutions)
			{
				TreeNode.Add(new ItemSolution(solution));
			}

            base.SyncDomain();
		}

		public override void BindUI()
		{
            base.BindUI();

			if (DomainData != null && DomainData.Details != null)
			{
				TreeViewUINode.Text = string.Format("{0}", DomainData.Details.Name);
			}
			else
			{
				TreeViewUINode.Text = "No puzzle map";
			}
		}

	    private static HtmlView html = new HtmlView();
        private static ucGenericDescription properties = new ucGenericDescription();

		public override Control ShowDetail()
		{
            if (IsEditable)
            {
                if (!Explorer.DetailPayload.Controls.Contains(properties))
                {
                    Explorer.DetailPayload.Controls.Clear();
                    Explorer.DetailPayload.Controls.Add(properties);
                    properties.Dock = DockStyle.Fill;

                    properties.Data = DomainData.Details;
                }

                return properties;
            }
            else
            {
                if (!Explorer.DetailPayload.Controls.Contains(html))
                {
                    Explorer.DetailPayload.Controls.Clear();
                    Explorer.DetailPayload.Controls.Add(html);
                    html.Dock = DockStyle.Fill;
                }

                if (DomainData != null)
                {
                    HtmlBuilder builder = new HtmlBuilder();
                    builder.Add(HtmlReporter.Report(DomainData, DrawingHelper.Images));

                    

                    html.SetHTML(builder.GetHTMLPage());
                }
                return html;
            }
		}


        
	}

}
