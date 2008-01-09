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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucImport));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTXT = new System.Windows.Forms.TabPage();
            this.richTextBoxDetails = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFileFormat = new System.Windows.Forms.ComboBox();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonOK = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openF = new System.Windows.Forms.OpenFileDialog();
            this.tabPageFeedback = new System.Windows.Forms.TabPage();
            this.richTextBoxImportResults = new System.Windows.Forms.RichTextBox();
            this.ucGenericDescription1 = new SokoSolve.UI.Controls.Secondary.ucGenericDescription();
            this.tabControl1.SuspendLayout();
            this.tabPageTXT.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPageFeedback.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageTXT);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPageFeedback);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(411, 300);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageTXT
            // 
            this.tabPageTXT.Controls.Add(this.richTextBoxDetails);
            this.tabPageTXT.Controls.Add(this.label3);
            this.tabPageTXT.Controls.Add(this.cbFileFormat);
            this.tabPageTXT.Controls.Add(this.buttonSelectFile);
            this.tabPageTXT.Controls.Add(this.tbFileName);
            this.tabPageTXT.Controls.Add(this.label1);
            this.tabPageTXT.Location = new System.Drawing.Point(4, 22);
            this.tabPageTXT.Name = "tabPageTXT";
            this.tabPageTXT.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTXT.Size = new System.Drawing.Size(403, 274);
            this.tabPageTXT.TabIndex = 0;
            this.tabPageTXT.Text = "File Format";
            this.tabPageTXT.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDetails
            // 
            this.richTextBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxDetails.Location = new System.Drawing.Point(10, 95);
            this.richTextBoxDetails.Name = "richTextBoxDetails";
            this.richTextBoxDetails.Size = new System.Drawing.Size(384, 173);
            this.richTextBoxDetails.TabIndex = 6;
            this.richTextBoxDetails.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Import File Type";
            // 
            // cbFileFormat
            // 
            this.cbFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileFormat.FormattingEnabled = true;
            this.cbFileFormat.Location = new System.Drawing.Point(7, 67);
            this.cbFileFormat.Name = "cbFileFormat";
            this.cbFileFormat.Size = new System.Drawing.Size(387, 21);
            this.cbFileFormat.TabIndex = 4;
            this.cbFileFormat.SelectedIndexChanged += new System.EventHandler(this.cbFileFormat_SelectedIndexChanged);
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Location = new System.Drawing.Point(345, 22);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(29, 23);
            this.buttonSelectFile.TabIndex = 2;
            this.buttonSelectFile.Text = "...";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(7, 24);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(332, 20);
            this.tbFileName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filename or URL (http://etc)";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucGenericDescription1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(403, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Import Author";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(254, 309);
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
            this.button3.Location = new System.Drawing.Point(335, 309);
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
            // tabPageFeedback
            // 
            this.tabPageFeedback.Controls.Add(this.richTextBoxImportResults);
            this.tabPageFeedback.Location = new System.Drawing.Point(4, 22);
            this.tabPageFeedback.Name = "tabPageFeedback";
            this.tabPageFeedback.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFeedback.Size = new System.Drawing.Size(403, 274);
            this.tabPageFeedback.TabIndex = 2;
            this.tabPageFeedback.Text = "Importer Results";
            this.tabPageFeedback.UseVisualStyleBackColor = true;
            // 
            // richTextBoxImportResults
            // 
            this.richTextBoxImportResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxImportResults.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxImportResults.Name = "richTextBoxImportResults";
            this.richTextBoxImportResults.Size = new System.Drawing.Size(397, 268);
            this.richTextBoxImportResults.TabIndex = 0;
            this.richTextBoxImportResults.Text = "";
            // 
            // ucGenericDescription1
            // 
            this.ucGenericDescription1.Data = ((SokoSolve.Core.Model.DataModel.GenericDescription)(resources.GetObject("ucGenericDescription1.Data")));
            this.ucGenericDescription1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGenericDescription1.Location = new System.Drawing.Point(3, 3);
            this.ucGenericDescription1.Name = "ucGenericDescription1";
            this.ucGenericDescription1.ShowButtons = false;
            this.ucGenericDescription1.Size = new System.Drawing.Size(397, 268);
            this.ucGenericDescription1.TabIndex = 0;
            // 
            // ucImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Name = "ucImport";
            this.Size = new System.Drawing.Size(417, 347);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTXT.ResumeLayout(false);
            this.tabPageTXT.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPageFeedback.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTXT;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label1;
        private ucGenericDescription ucGenericDescription1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RichTextBox richTextBoxDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFileFormat;
        private System.Windows.Forms.OpenFileDialog openF;
        private System.Windows.Forms.TabPage tabPageFeedback;
        private System.Windows.Forms.RichTextBox richTextBoxImportResults;
    }
}
