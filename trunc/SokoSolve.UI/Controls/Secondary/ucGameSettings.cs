using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SokoSolve.UI.Controls.Secondary
{
    public partial class ucGameSettings : UserControl
    {
        public ucGameSettings()
        {
            InitializeComponent();
        }

        private void buttonMusicLoc_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                MusicLocation.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
