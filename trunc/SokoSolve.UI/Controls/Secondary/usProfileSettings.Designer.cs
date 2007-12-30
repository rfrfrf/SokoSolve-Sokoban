namespace SokoSolve.UI.Controls.Secondary
{
    partial class usProfileSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLicense = new System.Windows.Forms.ComboBox();
            this.tbHomePage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name (Author field)";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(7, 21);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(332, 20);
            this.tbName.TabIndex = 1;
            this.tbName.Text = "Anonymouse User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Email Address (Author email)";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(7, 61);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(332, 20);
            this.tbEmail.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "License (eg. GPL, Creative Commons, etc)";
            // 
            // cbLicense
            // 
            this.cbLicense.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLicense.FormattingEnabled = true;
            this.cbLicense.Items.AddRange(new object[] {
            "GPL",
            "L-GPL",
            "BSD",
            "Creative Commons",
            "Public Domain",
            "Acknowledgement Required",
            "Commercial*"});
            this.cbLicense.Location = new System.Drawing.Point(7, 140);
            this.cbLicense.Name = "cbLicense";
            this.cbLicense.Size = new System.Drawing.Size(332, 21);
            this.cbLicense.TabIndex = 8;
            // 
            // tbHomePage
            // 
            this.tbHomePage.Location = new System.Drawing.Point(7, 101);
            this.tbHomePage.Name = "tbHomePage";
            this.tbHomePage.Size = new System.Drawing.Size(332, 20);
            this.tbHomePage.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Home page";
            // 
            // usProfileSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbHomePage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbLicense);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Name = "usProfileSettings";
            this.Size = new System.Drawing.Size(348, 204);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ComboBox cbLicense;
        internal System.Windows.Forms.TextBox tbName;
        internal System.Windows.Forms.TextBox tbEmail;
        internal System.Windows.Forms.TextBox tbHomePage;
        private System.Windows.Forms.Label label4;
    }
}
