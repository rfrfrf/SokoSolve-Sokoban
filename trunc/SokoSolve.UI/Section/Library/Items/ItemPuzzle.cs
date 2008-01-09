using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Controls.Web;
using SokoSolve.Common.Math;

namespace SokoSolve.UI.Section.Library.Items
{
	class ItemPuzzle : ExplorerItemBase<Puzzle>
	{
		public ItemPuzzle(Puzzle data) : base(data)
		{
		}

		public override void SyncDomain()
		{
            // Alternatives
            SyncUICollectionWithData<PuzzleMap>(DomainData.Alternatives, delegate(PuzzleMap item) { return new ItemPuzzleMap(item); });
            
            // Solutons
            SyncUICollectionWithData<Solution>(DomainData.MasterMap.Solutions, delegate(Solution item) { return new ItemSolution(item); });
           
		    base.SyncDomain();
		}

		public override void BindUI()
		{
            base.BindUI();

			if (DomainData != null)
			{
				TreeViewUINode.Text = DomainData.Details.Name;
			}
			else
			{
				TreeViewUINode.Text = "No puzzle";
			}
		}

        public override Control ShowDetail()
        {
            if (IsEditable)
            {
                if (showProps)
                {
                    if (!Explorer.DetailPayload.Controls.Contains(properties))
                    {
                        Explorer.DetailPayload.SuspendLayout();

                        Explorer.DetailPayload.Controls.Clear();
                        Explorer.DetailPayload.Controls.Add(properties);
                        properties.Dock = DockStyle.Fill;

                        properties.Data = DomainData.Details;

                        Explorer.DetailPayload.ResumeLayout();

                        properties.Data = DomainData.Details;
                    }

                    return properties;
                }
                else
                {
                    if (!Explorer.DetailPayload.Controls.Contains(puzzleEditor))
                    {
                        Explorer.DetailPayload.SuspendLayout();

                        Explorer.DetailPayload.Controls.Clear();
                        Explorer.DetailPayload.Controls.Add(puzzleEditor);
                        puzzleEditor.Dock = DockStyle.Fill;

                        Explorer.DetailPayload.ResumeLayout();

                        puzzleEditor.Map = DomainData.MasterMap.Map;
                    }

                    return puzzleEditor;
                }
            }
            else
            {
                if (!Explorer.DetailPayload.Controls.Contains(browser))
                {
                    Explorer.DetailPayload.SuspendLayout();

                    Explorer.DetailPayload.Controls.Clear();
                    Explorer.DetailPayload.Controls.Add(browser);
                    browser.Dock = DockStyle.Fill;

                    Explorer.DetailPayload.ResumeLayout();
                }


                HtmlBuilder html = BuildDetails();
                browser.SetHTML(html.GetHTMLPage());

                return browser;
            }
        }

	    HtmlBuilder BuildDetails()
        {
            return HtmlReporter.Report(DomainData, DrawingHelper.Images);
        }

        /// <summary>
        /// Show the details instead of the map editor
        /// </summary>
	    public bool ShowProps
	    {
	        get { return showProps; }
	        set { showProps = value; }
	    }

	    private static HtmlView browser = new HtmlView();
        private Editor puzzleEditor = new Editor();
        private static ucGenericDescription properties = new ucGenericDescription();
	    private bool showProps = false;
	}
}
