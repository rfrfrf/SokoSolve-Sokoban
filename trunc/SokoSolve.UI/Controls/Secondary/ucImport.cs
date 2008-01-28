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

            htmlView1.Navigate("http://sokosolve.sourceforge.net/resources.html");
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

            string fileLocation = null;
            if (!string.IsNullOrEmpty(tbFileName.Text))
            {
                // Use local file
                fileLocation = tbFileName.Text;

                // Store the new directory in profile
                ProfileController.Current.LibraryCurrentImportDir = Path.GetDirectoryName(openF.FileName);
            }
            else if (!string.IsNullOrEmpty(tbClipboard.Text))
            {
                // Use clipboard
                fileLocation = Path.GetTempFileName();
                File.WriteAllText(fileLocation, tbClipboard.Text);
            }
            else
            {
                if (htmlView1.WebBrowser.DocumentType == "TXT File" || htmlView1.WebBrowser.DocumentType == "text\\xml")
                {
                    // Use internet
                    fileLocation = Path.GetTempFileName();
                    htmlView1.SaveToFile(fileLocation);    
                } 
                else
                {
                    richTextBoxImportResults.Text = string.Format("Internet import from '{0}' is not supported.", htmlView1.WebBrowser.DocumentType);
                    tabControlImport.SelectTab(tabPageFeedback);
                    return;
                }
                
            }

            Importer.Details = ucGenericDescription1.Data;

            Library newLib = Importer.Import(fileLocation);
            if (newLib != null && Importer.LastError == null)
            {
                if (rbNewLibrary.Checked)
                {
                    library = newLib;
                }
                else if (rbAddCurrent.Checked)
                {
                    // Add to current
                    foreach (Puzzle puzzle in newLib.Puzzles)
                    {
                        puzzle.Category = library.Categories[library.Categories.Count -1];
                        puzzle.Library = library;
                        puzzle.Order = library.Puzzles.Count + 1;
                        puzzle.PuzzleID = library.IdProvider.GetNextIDString("P{0}");
                        library.Puzzles.Add(puzzle);
                    }
                }

                FindForm().DialogResult = DialogResult.OK;
                FindForm().Close();
            }
            else
            {
                richTextBoxImportResults.Text =
                    string.Format("{0}\n\n\nTechnical Error Message (For advanced users or bug reports):\n{1}",
                                  Importer.LastError.Message, StringHelper.Report(Importer.LastError));

                tabControlImport.SelectTab(tabPageFeedback);
            }
        }

     
        private void buttonPaste_Click(object sender, EventArgs e)
        {
            tbClipboard.Text = Clipboard.GetText();
        }

        private void buttonSelectFile_Click_1(object sender, EventArgs e)
        {
            if (openF.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = openF.FileName;
            }
        }
    }
}