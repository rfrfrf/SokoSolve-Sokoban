using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Section;
using SokoSolve.UI.Section.Library;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Controls.Primary
{
	public partial class Library : UserControl
	{
		private LibraryController controller;
		private LibraryExplorer explorer;

		public Library()
		{
			InitializeComponent();

            if (Site == null)
            {
                // Only when not in designer mode
                controller = new LibraryController(this);
                controller.IconBinder.SetImageList(IconSizes.Small, imageListSmall);
                controller.IconBinder.SetImageList(IconSizes.Icon, imageListIcon);
                controller.IconBinder.SetImageList(IconSizes.Thumbnail, imageListThumbnail);

                explorer = new LibraryExplorer(controller, treeViewLibrary, splitContainer1.Panel2);
            }
			

		}

        private void toolStripDropDownButtonDebug_Click(object sender, EventArgs e)
        {
            FormDisplayText report = new FormDisplayText();
            report.Text = "Debug Context Logger";
            report.TextPayload = controller.Logger.CreateReport();
            report.ShowDialog();
        }

	    public void InitLibrary(SokoSolve.Core.Model.Library current)
	    {
	        controller.Current = current;
	        explorer.SyncRoot(new ItemLibrary(controller.Current));
	        explorer.BindUI();
	    }
	}
}
