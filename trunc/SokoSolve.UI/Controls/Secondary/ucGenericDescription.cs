using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.UI.Controls.Secondary
{
	public partial class ucGenericDescription : UserControl
	{
		public ucGenericDescription()
		{
            showOK = true;

			InitializeComponent();
		}

        [Browsable(false)]
		public GenericDescription Data
		{
			get
			{
			    GenericDescription desc = new GenericDescription();
			    desc.Name = tbName.Text;
			    desc.Description = tbDescription.Text;
			    desc.Comments = tbComments.Text;
			    desc.Author = new GenericDescriptionAuthor();
			    desc.Author.Name = tbAuthor.Text;
			    desc.Author.Email = tbEmail.Text;
			    desc.Author.Homepage = tbWeb.Text;
			    desc.License = cbLicense.Text;
                desc.Date = dateTimePickerCreated.Value;
                desc.DateSpecified = true;
				return desc;
			}
			set
			{
				tbName.Text = "";
				tbDescription.Text = "";
				tbComments.Text = "";
				tbAuthor.Text = "";
				tbEmail.Text = "";
				tbWeb.Text = "";

				tbName.Text = value.Name;
				tbDescription.Text = value.Description;
				tbComments.Text = value.Comments;
				cbLicense.Text = value.License;
				if (value.Author != null)
				{
					tbAuthor.Text = value.Author.Name;
					tbEmail.Text = value.Author.Email;
					tbWeb.Text = value.Author.Homepage;
				}
                if (value.DateSpecified)
                {
                    dateTimePickerCreated.Value = value.Date;
                }
                else
                {
                    dateTimePickerCreated.Value = DateTime.Now;
                }
				
			}
		}

        /// <summary>
        /// Set the detail from the profile
        /// </summary>
        public void SetFromProfile()
        {
            if (Site != null) return;

            tbAuthor.Text = ProfileController.Current.UserName;
            tbEmail.Text = ProfileController.Current.UserEmail;
            tbWeb.Text = ProfileController.Current.UserHomepage;
            cbLicense.Text = ProfileController.Current.UserLicense;
        }

        [Browsable(true)]
        public bool ShowButtons
	    {
	        get { return showOK; }
            set 
            { 
                showOK = value;
                buttonOk.Visible = showOK;
                buttonCancel.Visible = showOK;
            }
	    }

	    private bool showOK;

        [Browsable(false)]
	    public Button ButtonOK
	    {
            get { return buttonOk; }
	    }

        [Browsable(false)]
        public Button ButtonCancel
        {
            get { return buttonCancel; }
        }
	}
}
