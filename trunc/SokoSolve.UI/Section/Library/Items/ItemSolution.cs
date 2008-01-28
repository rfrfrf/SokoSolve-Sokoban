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
                if (!Explorer.DetailPayload.Controls.Contains(editControl))
                {
                    Explorer.DetailPayload.Controls.Clear();
                    Explorer.DetailPayload.Controls.Add(editControl);
                    editControl.Dock = DockStyle.Fill;

                    editControl.ucGenericDescription.Data = DomainData.Details;
                }

                return editControl;
            }
            else
            {
                if (!Explorer.DetailPayload.Controls.Contains(readOnlyControl))
                {
                    Explorer.DetailPayload.Controls.Clear();
                    Explorer.DetailPayload.Controls.Add(readOnlyControl);
                    readOnlyControl.Dock = DockStyle.Fill;
                }

                if (DomainData != null)
                {
                    // Html
                    HtmlBuilder builder = new HtmlBuilder();
                    builder.Add(HtmlReporter.Report(DomainData.Details));

                    string[] moveSplit = StringHelper.SplitOnLength(DomainData.Steps, 60);
                    builder.AddLabel("Moves", StringHelper.Join(moveSplit, "<br/>"));
                    builder.AddLabel("Move Length", DomainData.Steps.Length.ToString());

                    readOnlyControl.htmlView.SetHTML(builder.GetHTMLPage());

                    // Solution Browser
                    readOnlyControl.Solution = DomainData;
                }
                return readOnlyControl;
            }
        }



        static PayloadSolution readOnlyControl = new PayloadSolution();
        private static PayloadSolutionEdit editControl = new PayloadSolutionEdit();
	}

}
