using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SokoSolve.UI.Controls.Secondary
{
    public partial class FormDisplayText : Form
    {
        public FormDisplayText()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Helper. Creates and displays the text
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        public static void ShowDialog(string title, string text)
        {
            FormDisplayText windows = new FormDisplayText();
            windows.Text = title;
            windows.TextPayload = text;
            windows.ShowDialog();
        }

        /// <summary>
        /// Text to display in the payload textbox
        /// </summary>
        public string TextPayload
        {
            get
            {
                return richTextBox1.Text;
            }
            set
            {
                richTextBox1.Text = value;
            }
        }
    }
}