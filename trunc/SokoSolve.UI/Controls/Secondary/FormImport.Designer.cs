namespace SokoSolve.UI.Controls.Secondary
{
    partial class FormImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImport));
            this.ucImport = new SokoSolve.UI.Controls.Secondary.ucImport();
            this.SuspendLayout();
            // 
            // ucImport
            // 
            this.ucImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucImport.Location = new System.Drawing.Point(0, 0);
            this.ucImport.Name = "ucImport";
            this.ucImport.Size = new System.Drawing.Size(520, 441);
            this.ucImport.TabIndex = 0;
            
            // 
            // FormImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 441);
            this.Controls.Add(this.ucImport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SokoSolve | Puzzle Library Import Wizard";
            this.ResumeLayout(false);

        }

        #endregion

        public ucImport ucImport;

    }
}