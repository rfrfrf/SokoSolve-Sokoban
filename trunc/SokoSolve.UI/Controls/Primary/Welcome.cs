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
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            InitializeComponent();

            htmlView.Navigate(FileManager.getContent("$html/Welcome.html"));
        }

        private void htmlView_OnCommand(object sender, SokoSolve.UI.Controls.Web.UIBrowserEvent e)
        {
            if (e.Command == new Uri("app://Library"))
            {
                FormMain main = FindForm() as FormMain;
                if (main != null)
                {
                    main.Mode = FormMain.Modes.Library;
                }    
            }
        }
    }
}
