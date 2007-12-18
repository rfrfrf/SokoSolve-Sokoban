using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.UI.Section.Library;

namespace SokoSolve.UI.Section
{
    /// <summary>
    /// A tree 'windows explorer' like interface. Capable of binding the domain to a master list (tree) and detail (any control) payload.
    /// </summary>
	public class ExplorerPattern : Tree<ExplorerItem>
	{
		private Controller<ExplorerItem> controller;
		private TreeView treeView;
		private Control detailPayload;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="treeView"></param>
        /// <param name="detailPayloadControl"></param>
		public ExplorerPattern(Controller<ExplorerItem> controller, TreeView treeView, Control detailPayloadControl)
		{
			this.controller = controller;
			this.treeView = treeView;
			this.detailPayload = detailPayloadControl;
			this.treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
		}

        /// <summary>
        /// Master list (left hand side) tree control
        /// </summary>
        public TreeView TreeView
        {
            get { return treeView; }
            // set { treeView = value; }
        }

		/// <summary>
		/// Current controller for the selected item (GL - Correct??)
		/// </summary>
		public Controller<ExplorerItem> Controller
		{
			get { return controller; }
			set { controller = value; }
		}

        /// <summary>
        /// Current detail payload
        /// </summary>
		public Control DetailPayload
		{
			get { return detailPayload; }
		}

        /// <summary>
        /// Set the root node data, and then bind the UI to it.
        /// </summary>
        /// <param name="root"></param>
		public void SyncRoot(ExplorerItem root)
		{
            // Clear any existing
            treeView.Nodes.Clear();

            // Set new data and bind
			Top.Data = root;
			Top.Data.SyncWithData();
		}

        /// <summary>
        /// Refresh the UI - 
        /// </summary>
		public void BindUI()
		{
			if (Top.Data.UINode == null)
			{
				if (treeView.Nodes.Count == 0)
				{
					treeView.Nodes.Add("!Top Node!");
				}
				Top.Data.UINode = treeView.Nodes[0];
				Top.Data.UINode.Tag = Top.Data;
			}

			Top.ForEach(delegate(TreeNode<ExplorerItem> item) { item.Data.BindToUI(); }, int.MaxValue);
		}

        /// <summary>
        /// Overloaded. This will find the current ExplorerItem based on the Controllers current selection
        /// </summary>
         public void UpdateSelection()
         {
            ExplorerItem current = null;
             if (Controller.Selection != null && Controller.Selection.Count > 0)
             {
                 current = Controller.Selection[0];
                 UpdateSelection(current);
             }
         }

        /// <summary>
        /// Syncs the UI selection with the model. While the Controller stores the current selection (that may be many),
        /// this method will sync the treeview and payload with the currently selected ExplorerItem
        /// </summary>
        public void UpdateSelection(ExplorerItem current)
        {
            if (current == null) return;

            if (controller.Selection != null && controller.Selection.Count > 0)
            {
                // Cleanup
                foreach (ExplorerItem item in controller.Selection)
                {
                    item.CleanUpDetail();
                }
            }

            List<ExplorerItem> list = new List<ExplorerItem>();
            list.Add(current);
            if (controller.UpdateSelection(list))
            {
                // New selection

                // Update payload
                foreach (ExplorerItem item in controller.Selection)
                {
                    item.ShowDetail();
                }
            }
        }

        /// <summary>
        /// Update the selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ExplorerItem current = treeView.SelectedNode.Tag as ExplorerItem;
            UpdateSelection(current);
        }
	}


	}
