using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.UI.Controls.Secondary
{
    partial class ucImport
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlImport = new System.Windows.Forms.TabControl();
            this.tabPageSource = new System.Windows.Forms.TabPage();
            this.tabControlSource = new System.Windows.Forms.TabControl();
            this.tabPageClipboard = new System.Windows.Forms.TabPage();
            this.buttonPaste = new System.Windows.Forms.Button();
            this.tbClipboard = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageFileFormat = new System.Windows.Forms.TabPage();
            this.richTextBoxDetails = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFileFormat = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPageFeedback = new System.Windows.Forms.TabPage();
            this.richTextBoxImportResults = new System.Windows.Forms.RichTextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openF = new System.Windows.Forms.OpenFileDialog();
            this.rbNewLibrary = new System.Windows.Forms.RadioButton();
            this.rbAddCurrent = new System.Windows.Forms.RadioButton();
            this.htmlView1 = new SokoSolve.UI.Controls.Web.HtmlView();
            this.ucGenericDescription1 = new SokoSolve.UI.Controls.Secondary.ucGenericDescription();
            this.tabControlImport.SuspendLayout();
            this.tabPageSource.SuspendLayout();
            this.tabControlSource.SuspendLayout();
            this.tabPageClipboard.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPageFileFormat.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPageFeedback.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlImport
            // 
            this.tabControlImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlImport.Controls.Add(this.tabPageSource);
            this.tabControlImport.Controls.Add(this.tabPageFileFormat);
            this.tabControlImport.Controls.Add(this.tabPage2);
            this.tabControlImport.Controls.Add(this.tabPageFeedback);
            this.tabControlImport.Location = new System.Drawing.Point(3, 3);
            this.tabControlImport.Name = "tabControlImport";
            this.tabControlImport.SelectedIndex = 0;
            this.tabControlImport.Size = new System.Drawing.Size(621, 405);
            this.tabControlImport.TabIndex = 0;
            // 
            // tabPageSource
            // 
            this.tabPageSource.Controls.Add(this.tabControlSource);
            this.tabPageSource.Controls.Add(this.buttonSelectFile);
            this.tabPageSource.Controls.Add(this.tbFileName);
            this.tabPageSource.Controls.Add(this.label1);
            this.tabPageSource.Location = new System.Drawing.Point(4, 22);
            this.tabPageSource.Name = "tabPageSource";
            this.tabPageSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSource.Size = new System.Drawing.Size(613, 379);
            this.tabPageSource.TabIndex = 3;
            this.tabPageSource.Text = "Source Location";
            this.tabPageSource.UseVisualStyleBackColor = true;
            // 
            // tabControlSource
            // 
            this.tabControlSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSource.Controls.Add(this.tabPageClipboard);
            this.tabControlSource.Controls.Add(this.tabPage3);
            this.tabControlSource.Location = new System.Drawing.Point(9, 58);
            this.tabControlSource.Name = "tabControlSource";
            this.tabControlSource.SelectedIndex = 0;
            this.tabControlSource.Size = new System.Drawing.Size(598, 302);
            this.tabControlSource.TabIndex = 6;
            // 
            // tabPageClipboard
            // 
            this.tabPageClipboard.Controls.Add(this.buttonPaste);
            this.tabPageClipboard.Controls.Add(this.tbClipboard);
            this.tabPageClipboard.Location = new System.Drawing.Point(4, 22);
            this.tabPageClipboard.Name = "tabPageClipboard";
            this.tabPageClipboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClipboard.Size = new System.Drawing.Size(590, 276);
            this.tabPageClipboard.TabIndex = 0;
            this.tabPageClipboard.Text = "From Clipboard";
            this.tabPageClipboard.UseVisualStyleBackColor = true;
            // 
            // buttonPaste
            // 
            this.buttonPaste.Location = new System.Drawing.Point(7, 7);
            this.buttonPaste.Name = "buttonPaste";
            this.buttonPaste.Size = new System.Drawing.Size(104, 23);
            this.buttonPaste.TabIndex = 1;
            this.buttonPaste.Text = "Paste/Refresh";
            this.buttonPaste.UseVisualStyleBackColor = true;
            this.buttonPaste.Click += new System.EventHandler(this.buttonPaste_Click);
            // 
            // tbClipboard
            // 
            this.tbClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClipboard.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbClipboard.Location = new System.Drawing.Point(6, 37);
            this.tbClipboard.Multiline = true;
            this.tbClipboard.Name = "tbClipboard";
            this.tbClipboard.Size = new System.Drawing.Size(578, 236);
            this.tbClipboard.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.htmlView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(590, 276);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "From Internet";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectFile.Location = new System.Drawing.Point(536, 30);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(69, 23);
            this.buttonSelectFile.TabIndex = 5;
            this.buttonSelectFile.Text = "Browse ...";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click_1);
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(13, 32);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(519, 20);
            this.tbFileName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "From File:";
            // 
            // tabPageFileFormat
            // 
            this.tabPageFileFormat.Controls.Add(this.richTextBoxDetails);
            this.tabPageFileFormat.Controls.Add(this.label3);
            this.tabPageFileFormat.Controls.Add(this.cbFileFormat);
            this.tabPageFileFormat.Location = new System.Drawing.Point(4, 22);
            this.tabPageFileFormat.Name = "tabPageFileFormat";
            this.tabPageFileFormat.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFileFormat.Size = new System.Drawing.Size(613, 379);
            this.tabPageFileFormat.TabIndex = 0;
            this.tabPageFileFormat.Text = "File Format";
            this.tabPageFileFormat.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDetails
            // 
            this.richTextBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxDetails.Location = new System.Drawing.Point(10, 55);
            this.richTextBoxDetails.Name = "richTextBoxDetails";
            this.richTextBoxDetails.Size = new System.Drawing.Size(594, 318);
            this.richTextBoxDetails.TabIndex = 6;
            this.richTextBoxDetails.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Import File Type";
            // 
            // cbFileFormat
            // 
            this.cbFileFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileFormat.FormattingEnabled = true;
            this.cbFileFormat.Location = new System.Drawing.Point(6, 28);
            this.cbFileFormat.Name = "cbFileFormat";
            this.cbFileFormat.Size = new System.Drawing.Size(598, 21);
            this.cbFileFormat.TabIndex = 4;
            this.cbFileFormat.SelectedIndexChanged += new System.EventHandler(this.cbFileFormat_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucGenericDescription1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(613, 379);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Import Author";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPageFeedback
            // 
            this.tabPageFeedback.Controls.Add(this.richTextBoxImportResults);
            this.tabPageFeedback.Location = new System.Drawing.Point(4, 22);
            this.tabPageFeedback.Name = "tabPageFeedback";
            this.tabPageFeedback.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFeedback.Size = new System.Drawing.Size(613, 379);
            this.tabPageFeedback.TabIndex = 2;
            this.tabPageFeedback.Text = "Importer Results";
            this.tabPageFeedback.UseVisualStyleBackColor = true;
            // 
            // richTextBoxImportResults
            // 
            this.richTextBoxImportResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxImportResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxImportResults.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxImportResults.Name = "richTextBoxImportResults";
            this.richTextBoxImportResults.Size = new System.Drawing.Size(607, 373);
            this.richTextBoxImportResults.TabIndex = 0;
            this.richTextBoxImportResults.Text = "";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(464, 414);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(545, 414);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "&Cancel";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // openF
            // 
            this.openF.DefaultExt = "txt";
            this.openF.RestoreDirectory = true;
            this.openF.Title = "Puzzle to import";
            // 
            // rbNewLibrary
            // 
            this.rbNewLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbNewLibrary.AutoSize = true;
            this.rbNewLibrary.Checked = true;
            this.rbNewLibrary.Location = new System.Drawing.Point(7, 419);
            this.rbNewLibrary.Name = "rbNewLibrary";
            this.rbNewLibrary.Size = new System.Drawing.Size(109, 17);
            this.rbNewLibrary.TabIndex = 3;
            this.rbNewLibrary.TabStop = true;
            this.rbNewLibrary.Text = "Create new library";
            this.rbNewLibrary.UseVisualStyleBackColor = true;
            // 
            // rbAddCurrent
            // 
            this.rbAddCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbAddCurrent.AutoSize = true;
            this.rbAddCurrent.Location = new System.Drawing.Point(129, 419);
            this.rbAddCurrent.Name = "rbAddCurrent";
            this.rbAddCurrent.Size = new System.Drawing.Size(122, 17);
            this.rbAddCurrent.TabIndex = 4;
            this.rbAddCurrent.Text = "Add to current library";
            this.rbAddCurrent.UseVisualStyleBackColor = true;
            // 
            // htmlView1
            // 
            this.htmlView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlView1.Location = new System.Drawing.Point(3, 3);
            this.htmlView1.Name = "htmlView1";
            this.htmlView1.ShowCommandBack = false;
            this.htmlView1.ShowCommandDone = false;
            this.htmlView1.ShowCommandForward = false;
            this.htmlView1.ShowCommandHome = false;
            this.htmlView1.ShowCommandPrint = false;
            this.htmlView1.ShowCommands = false;
            this.htmlView1.ShowCommandSave = false;
            this.htmlView1.ShowStatus = false;
            this.htmlView1.Size = new System.Drawing.Size(584, 270);
            this.htmlView1.TabIndex = 0;
            // 
            // ucGenericDescription1
            // 
            this.ucGenericDescription1.Data = null;
            this.ucGenericDescription1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGenericDescription1.Location = new System.Drawing.Point(3, 3);
            this.ucGenericDescription1.Name = "ucGenericDescription1";
            this.ucGenericDescription1.ShowButtons = false;
            this.ucGenericDescription1.Size = new System.Drawing.Size(607, 373);
            this.ucGenericDescription1.TabIndex = 0;
            // 
            // ucImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbAddCurrent);
            this.Controls.Add(this.rbNewLibrary);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlImport);
            this.Name = "ucImport";
            this.Size = new System.Drawing.Size(627, 452);
            this.tabControlImport.ResumeLayout(false);
            this.tabPageSource.ResumeLayout(false);
            this.tabPageSource.PerformLayout();
            this.tabControlSource.ResumeLayout(false);
            this.tabPageClipboard.ResumeLayout(false);
            this.tabPageClipboard.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPageFileFormat.ResumeLayout(false);
            this.tabPageFileFormat.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPageFeedback.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlImport;
        private System.Windows.Forms.TabPage tabPageFileFormat;
        private System.Windows.Forms.TabPage tabPage2;
        private ucGenericDescription ucGenericDescription1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RichTextBox richTextBoxDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFileFormat;
        private System.Windows.Forms.OpenFileDialog openF;
        private System.Windows.Forms.TabPage tabPageFeedback;
        private System.Windows.Forms.RichTextBox richTextBoxImportResults;
        private System.Windows.Forms.TabPage tabPageSource;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlSource;
        private System.Windows.Forms.TabPage tabPageClipboard;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbClipboard;
        private SokoSolve.UI.Controls.Web.HtmlView htmlView1;
        private System.Windows.Forms.Button buttonPaste;
        private System.Windows.Forms.RadioButton rbNewLibrary;
        private System.Windows.Forms.RadioButton rbAddCurrent;
    }
}
