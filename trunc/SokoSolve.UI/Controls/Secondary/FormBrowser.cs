using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SokoSolve.UI.Controls.Secondary
{
    public partial class FormBrowser : Form
    {
        public FormBrowser()
        {
            InitializeComponent();
        }

        public void SetHTML(string html)
        {
            htmlView1.SetHTML(html);
        }

        private void htmlView1_OnCommand(object sender, SokoSolve.UI.Controls.Web.UIBrowserEvent e)
        {
            if (e.Command == new Uri("app://Controller/Done"))
            {
                Close();
            }
        }
    }
}