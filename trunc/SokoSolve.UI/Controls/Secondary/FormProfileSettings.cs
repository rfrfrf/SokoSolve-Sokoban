using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SokoSolve.UI.Controls.Secondary
{
    public partial class FormProfileSettings : Form
    {
        public FormProfileSettings()
        {
            InitializeComponent();

            Bind();
        }

        public void Bind()
        {
            usProfileSettings1.tbName.Text = ProfileController.Current.UserName;
            usProfileSettings1.tbEmail.Text = ProfileController.Current.UserEmail;
            usProfileSettings1.tbHomePage.Text = ProfileController.Current.UserHomepage;
            usProfileSettings1.cbLicense.Text = ProfileController.Current.UserLicense;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProfileController.Current.UserName = usProfileSettings1.tbName.Text;
            ProfileController.Current.UserEmail = usProfileSettings1.tbEmail.Text;
            ProfileController.Current.UserHomepage = usProfileSettings1.tbHomePage.Text;
            ProfileController.Current.UserLicense = usProfileSettings1.cbLicense.Text;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }


    }
}