using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Core;
using SokoSolve.Core.Reporting;

namespace SokoSolve.UI.Section.Library
{
    /// <summary>
    /// Create a simple XHTML report
    /// </summary>
    public partial class FormReport : Form
    {
        SokoSolve.Core.Model.Library library;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FormReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set the output file name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bBrowse_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = library.Details.Name + ".html";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbFileName.Text = saveFileDialog1.FileName;
            }
        }

        /// <summary>
        /// Current library output to HTML
        /// </summary>
        public SokoSolve.Core.Model.Library Library
        {
            get { return library; }
            set { library = value; }
        }

        /// <summary>
        /// Do the work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            // Anything to do?
            if (string.IsNullOrEmpty(tbFileName.Text)) return;

            // Build the HTML
            LibraryReport rpt = new LibraryReport(library, DrawingHelper.Images, Path.GetDirectoryName(tbFileName.Text));
            rpt.OptionImages = GetImageOptions();
            rpt.OptionCSS = GetCSSOptions();

            // Set inline CSS?
            if (rbCSSInline.Checked)
            {
                rpt.SetCSSInline(FileManager.getContent("$html\\reportstyle.css"));
            }

            // Create and write to disk
            rpt.BuildReport();
            rpt.Save(tbFileName.Text);

            // Copy inline images
            if (rbImageTableCell.Checked && cbCopyStatic.Checked)
            {
                FileHelper.CopyDirectory(FileManager.getContent("$Graphics\\ReportImages"), Path.GetDirectoryName(tbFileName.Text) + "\\images\\");
            }

            // Copy style.css file to destination
            if (rbCSSCopy.Checked)
            {
                File.Copy(FileManager.getContent("$html\\reportstyle.css"), Path.GetDirectoryName(tbFileName.Text) + "\\style.css");
            }

            // Show the results in browser
            if (cbShowInBrowser.Checked)
            {
                Process.Start(tbFileName.Text);
            }
        }

        private LibraryReport.CSS GetCSSOptions()
        {
            if (rbCSSNone.Checked) return LibraryReport.CSS.None;
            if (rbCSSInline.Checked) return LibraryReport.CSS.Inline;
            return LibraryReport.CSS.Copy;
        }

        private LibraryReport.Images GetImageOptions()
        {
            if (rbImageNone.Checked) return LibraryReport.Images.None;
            if (rbImagesFull.Checked) return LibraryReport.Images.Full;
            return LibraryReport.Images.TableCell;
        }
    }
}