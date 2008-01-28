namespace SokoSolve.UI.Section.Library
{
    partial class FormReport
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReport));
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.bBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCSSCopy = new System.Windows.Forms.RadioButton();
            this.rbCSSInline = new System.Windows.Forms.RadioButton();
            this.rbCSSNone = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbImageTableCell = new System.Windows.Forms.RadioButton();
            this.rbImagesFull = new System.Windows.Forms.RadioButton();
            this.rbImageNone = new System.Windows.Forms.RadioButton();
            this.cbCopyStatic = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cbShowInBrowser = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(13, 30);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(369, 20);
            this.tbFileName.TabIndex = 0;
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(388, 27);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(75, 23);
            this.bBrowse.TabIndex = 1;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output Filename:";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(307, 253);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(388, 253);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCSSCopy);
            this.groupBox1.Controls.Add(this.rbCSSInline);
            this.groupBox1.Controls.Add(this.rbCSSNone);
            this.groupBox1.Location = new System.Drawing.Point(16, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stylesheet options (CSS)";
            // 
            // rbCSSCopy
            // 
            this.rbCSSCopy.AutoSize = true;
            this.rbCSSCopy.Checked = true;
            this.rbCSSCopy.Location = new System.Drawing.Point(7, 68);
            this.rbCSSCopy.Name = "rbCSSCopy";
            this.rbCSSCopy.Size = new System.Drawing.Size(99, 17);
            this.rbCSSCopy.TabIndex = 2;
            this.rbCSSCopy.TabStop = true;
            this.rbCSSCopy.Text = "Create style.css";
            this.rbCSSCopy.UseVisualStyleBackColor = true;
            // 
            // rbCSSInline
            // 
            this.rbCSSInline.AutoSize = true;
            this.rbCSSInline.Location = new System.Drawing.Point(7, 44);
            this.rbCSSInline.Name = "rbCSSInline";
            this.rbCSSInline.Size = new System.Drawing.Size(74, 17);
            this.rbCSSInline.TabIndex = 1;
            this.rbCSSInline.Text = "Inline CSS";
            this.rbCSSInline.UseVisualStyleBackColor = true;
            // 
            // rbCSSNone
            // 
            this.rbCSSNone.AutoSize = true;
            this.rbCSSNone.Location = new System.Drawing.Point(7, 20);
            this.rbCSSNone.Name = "rbCSSNone";
            this.rbCSSNone.Size = new System.Drawing.Size(51, 17);
            this.rbCSSNone.TabIndex = 0;
            this.rbCSSNone.Text = "None";
            this.rbCSSNone.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbImageTableCell);
            this.groupBox2.Controls.Add(this.rbImagesFull);
            this.groupBox2.Controls.Add(this.rbImageNone);
            this.groupBox2.Location = new System.Drawing.Point(160, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Puzzle Images";
            // 
            // rbImageTableCell
            // 
            this.rbImageTableCell.AutoSize = true;
            this.rbImageTableCell.Checked = true;
            this.rbImageTableCell.Location = new System.Drawing.Point(7, 68);
            this.rbImageTableCell.Name = "rbImageTableCell";
            this.rbImageTableCell.Size = new System.Drawing.Size(94, 17);
            this.rbImageTableCell.TabIndex = 2;
            this.rbImageTableCell.TabStop = true;
            this.rbImageTableCell.Text = "Use table cells";
            this.rbImageTableCell.UseVisualStyleBackColor = true;
            // 
            // rbImagesFull
            // 
            this.rbImagesFull.AutoSize = true;
            this.rbImagesFull.Location = new System.Drawing.Point(7, 44);
            this.rbImagesFull.Name = "rbImagesFull";
            this.rbImagesFull.Size = new System.Drawing.Size(92, 17);
            this.rbImagesFull.TabIndex = 1;
            this.rbImagesFull.Text = "Create images";
            this.rbImagesFull.UseVisualStyleBackColor = true;
            // 
            // rbImageNone
            // 
            this.rbImageNone.AutoSize = true;
            this.rbImageNone.Location = new System.Drawing.Point(7, 20);
            this.rbImageNone.Name = "rbImageNone";
            this.rbImageNone.Size = new System.Drawing.Size(51, 17);
            this.rbImageNone.TabIndex = 0;
            this.rbImageNone.Text = "None";
            this.rbImageNone.UseVisualStyleBackColor = true;
            // 
            // cbCopyStatic
            // 
            this.cbCopyStatic.AutoSize = true;
            this.cbCopyStatic.Checked = true;
            this.cbCopyStatic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCopyStatic.Location = new System.Drawing.Point(167, 174);
            this.cbCopyStatic.Name = "cbCopyStatic";
            this.cbCopyStatic.Size = new System.Drawing.Size(278, 17);
            this.cbCopyStatic.TabIndex = 7;
            this.cbCopyStatic.Text = "Copy cell image to report directory (only for table cells)";
            this.cbCopyStatic.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "html";
            // 
            // cbShowInBrowser
            // 
            this.cbShowInBrowser.AutoSize = true;
            this.cbShowInBrowser.Checked = true;
            this.cbShowInBrowser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInBrowser.Location = new System.Drawing.Point(23, 253);
            this.cbShowInBrowser.Name = "cbShowInBrowser";
            this.cbShowInBrowser.Size = new System.Drawing.Size(205, 17);
            this.cbShowInBrowser.TabIndex = 8;
            this.cbShowInBrowser.Text = "Open report in browser on completion.";
            this.cbShowInBrowser.UseVisualStyleBackColor = true;
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 288);
            this.Controls.Add(this.cbShowInBrowser);
            this.Controls.Add(this.cbCopyStatic);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.tbFileName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Generare Puzzle Report";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCSSCopy;
        private System.Windows.Forms.RadioButton rbCSSInline;
        private System.Windows.Forms.RadioButton rbCSSNone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbImageTableCell;
        private System.Windows.Forms.RadioButton rbImagesFull;
        private System.Windows.Forms.RadioButton rbImageNone;
        private System.Windows.Forms.CheckBox cbCopyStatic;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox cbShowInBrowser;
    }
}