using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;
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

		public override void SyncDomain()
		{
            // No children
		}

		public override void BindUI()
		{
            base.BindUI();

            if (DomainData != null && DomainData.Details != null)
            {
                TreeViewUINode.Text = DomainData.Details.Name;
            }
            else
            {
                TreeViewUINode.Text = "No solution";
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
                    builder.Add(HtmlReporter.Report(DomainData.Details));

                    string[] moveSplit = StringHelper.SplitOnLength(DomainData.Steps, 60);
                    builder.AddLabel("Moves", StringHelper.Join(moveSplit, "<br/>"));
                    builder.AddLabel("Move Length", DomainData.Steps.Length.ToString());

                    html.SetHTML(builder.GetHTMLPage());
                }
                return html;
            }
        }



        static HtmlView html = new HtmlView();
        private static ucGenericDescription properties = new ucGenericDescription();
	}

}
