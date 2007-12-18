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

		public override void SyncWithData()
		{
		    int cc = 0;
			foreach (PuzzleMap map in Data.Maps)
			{
                // Skip the first
                if (cc++ == 0) continue;
			    
				TreeNode.Add(new ItemPuzzleMap(map));
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
				UINode.Text = Data.Details.Name;
			}
			else
			{
				UINode.Text = "No puzzle";
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

                        properties.Data = Data.Details;

                        Explorer.DetailPayload.ResumeLayout();

                        properties.Data = Data.Details;
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

                        puzzleEditor.Map = Data.MasterMap.Map;
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

        static ItemPuzzle()
        {
            drawing = new StaticImage(ResourceFactory.Singleton.GetInstance("Default.Tiles"), new VectorInt(16, 16));
            drawing.Tiles[0] = null;  // Make void transparent
        }

	    private static StaticImage drawing;

        HtmlBuilder BuildDetails()
        {
            return HtmlReporter.Report(Data, drawing);
            
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
