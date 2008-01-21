namespace SokoSolve.UI.Controls.Secondary
{
    partial class FormBrowser
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
            this.htmlView1 = new SokoSolve.UI.Controls.Web.HtmlView();
            this.SuspendLayout();
            // 
            // htmlView1
            // 
            this.htmlView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlView1.Location = new System.Drawing.Point(0, 0);
            this.htmlView1.Name = "htmlView1";
            this.htmlView1.ShowCommandBack = false;
            this.htmlView1.ShowCommandDone = true;
            this.htmlView1.ShowCommandForward = false;
            this.htmlView1.ShowCommandHome = false;
            this.htmlView1.ShowCommandPrint = true;
            this.htmlView1.ShowCommands = true;
            this.htmlView1.ShowStatus = true;
            this.htmlView1.Size = new System.Drawing.Size(571, 430);
            this.htmlView1.TabIndex = 0;
            this.htmlView1.OnCommand += new System.EventHandler<SokoSolve.UI.Controls.Web.UIBrowserEvent>(this.htmlView1_OnCommand);
            // 
            // FormBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 430);
            this.Controls.Add(this.htmlView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBrowser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "FormBrowser";
            this.ResumeLayout(false);

        }

        #endregion

        private SokoSolve.UI.Controls.Web.HtmlView htmlView1;

    }
}