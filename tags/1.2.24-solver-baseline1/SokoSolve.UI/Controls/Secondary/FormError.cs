using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;

namespace SokoSolve.UI.Controls.Secondary
{
    public partial class FormError : Form
    {
        public FormError()
        {
            InitializeComponent();
        }


        public Exception Exception
        {
            get
            {
                return _Exception;
            }
            set
            {
                _Exception = value;
                textBox1.Text = value.Message;
                textBox2.Text = "";
            	richTextBox1.Text = StringHelper.Report(_Exception);
                
            }
        }

        Exception _Exception;
    }
}