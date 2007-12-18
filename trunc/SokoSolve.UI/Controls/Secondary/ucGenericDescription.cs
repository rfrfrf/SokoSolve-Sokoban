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
			InitializeComponent();
		}

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
				
			}
		}

	    public Button ButtonOK
	    {
            get { return buttonOk; }
	    }

        public Button ButtonCancel
        {
            get { return buttonCancel; }
        }
	}
}
