namespace SokoSolve.UI.Controls.Primary
{
    partial class InlineBrowser
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
            this.htmlView = new SokoSolve.UI.Controls.Web.HtmlView();
            this.SuspendLayout();
            // 
            // htmlView
            // 
            this.htmlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlView.Location = new System.Drawing.Point(0, 0);
            this.htmlView.Name = "htmlView";
            this.htmlView.ShowCommands = true;
            this.htmlView.ShowStatus = true;
            this.htmlView.Size = new System.Drawing.Size(320, 335);
            this.htmlView.TabIndex = 0;
            this.htmlView.OnCommand += new System.EventHandler<SokoSolve.UI.Controls.Web.UIBrowserEvent>(this.htmlView_OnCommand);
            // 
            // InlineBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.htmlView);
            this.Name = "InlineBrowser";
            this.Size = new System.Drawing.Size(320, 335);
            this.ResumeLayout(false);

        }

        #endregion

        private SokoSolve.UI.Controls.Web.HtmlView htmlView;

    }
}
