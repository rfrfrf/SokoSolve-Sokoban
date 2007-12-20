using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core;

namespace SokoSolve.UI.Controls.Primary
{
    public partial class InlineBrowser : UserControl
    {
        public InlineBrowser()
        {
            InitializeComponent();
        }

        public void Navigate(string Uri)
        {
            if (firstURL != null)
            {
                firstURL = Uri;
            }

            htmlView.Navigate(Uri); 
        }

        /// <summary>
        /// Navigate to included content (via the FileManger.getContent())
        /// </summary>
        /// <param name="RelativeContent">Eg "$html/about.html"</param>
        public void NavigateIncludedContent(string RelativeContent)
        {
            htmlView.Navigate(FileManager.getContent(RelativeContent));
        }

        private void htmlView_OnCommand(object sender, SokoSolve.UI.Controls.Web.UIBrowserEvent e)
        {
            FormMain main = FindForm() as FormMain;
            if (main != null) return;

            if (e.Command.ToString() == "app://Controller/Done")
            {
                main.Mode = FormMain.Modes.Library;
                e.Completed = true;
                return;
            }

            if (e.Command.ToString() == "app://Controller/Home")
            {
                Navigate(firstURL);
                e.Completed = true;
                return;
            }
        }

        private string firstURL;
    }
}
