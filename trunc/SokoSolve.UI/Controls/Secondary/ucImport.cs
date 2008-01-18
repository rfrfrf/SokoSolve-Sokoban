using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Core.IO;
using SokoSolve.Core.Model;

namespace SokoSolve.UI.Controls.Secondary
{
    public partial class ucImport : UserControl
    {
        private List<Importer> importers;
        private Library library;

        public ucImport()
        {
            InitializeComponent();

            openF.InitialDirectory = ProfileController.Current.LibraryCurrentImportDir;

            importers = new List<Importer>();
            importers.Add(new ImportTXT());
            importers.Add(new ImportSLC());

            ucGenericDescription1.SetFromProfile();

            cbFileFormat.Items.AddRange(importers.ToArray());
            cbFileFormat.SelectedIndex = 0;
        }

        /// <summary>
        /// Library of imported
        /// </summary>
        [Browsable(false)]
        public Library Library
        {
            get { return library; }
            set { library = value; }
        }

        /// <summary>
        /// Current Importer to use 
        /// </summary>
        [Browsable(false)]
        public Importer Importer
        {
            get { return (cbFileFormat.SelectedItem as Importer); }
        }


        private void cbFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxDetails.Text = Importer.Description;
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            richTextBoxImportResults.Text = "";

            if (string.IsNullOrEmpty(tbFileName.Text))
            {
                // Force file select
                buttonSelectFile_Click(sender, e);
                return;
            }

            // Store the new directory in profile
            ProfileController.Current.LibraryCurrentImportDir = Path.GetDirectoryName(openF.FileName);

            Importer.Details = ucGenericDescription1.Data;

            library = Importer.Import(tbFileName.Text);
            if (library != null && Importer.LastError == null)
            {
                FindForm().DialogResult = DialogResult.OK;
                FindForm().Close();
            }
            else
            {
                richTextBoxImportResults.Text =
                    string.Format("{0}\n\n\nTechnical Error Message (For advanced users or bug reports):\n{1}",
                                  Importer.LastError.Message, StringHelper.Report(Importer.LastError));

                tabControl1.SelectTab(tabPageFeedback);
            }
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            if (openF.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = openF.FileName;
            }
        }
    }
}