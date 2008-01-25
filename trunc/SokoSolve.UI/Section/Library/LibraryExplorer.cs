using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Section.Library
{
	public class LibraryExplorer : ExplorerPattern
	{
		private LibraryController controller;

		public LibraryExplorer(LibraryController controller, TreeView treeView, Control payload) : base(controller, treeView, payload)
		{
            // Set the controller
			this.controller = controller;

            // Attach this explorer, with the contoller for back referencing
		    this.controller.Explorer = this;

			controller.OnCurrentChanged += new EventHandler(controller_OnCurrentChanged);

		    treeView.ImageList = controller.IconBinder.GetImageList(IconSizes.Small);

			SyncDomain(new ItemLibrary(controller.Current));
			SyncUI();

		    controller.UpdateSelection(controller.Selection);
		}

		void controller_OnCurrentChanged(object sender, EventArgs e)
		{
			SyncDomain(new ItemLibrary(controller.Current));
			SyncUI();
		}


        /// <summary>
        /// Clear all nodes and selection
        /// </summary>
	    public void Clear()
	    {
            base.TreeView.Nodes.Clear();
            base.TreeView.Nodes.Add("Root");
            controller.UpdateSelection(new List<ExplorerItem>());
	    }
	}

	
}
