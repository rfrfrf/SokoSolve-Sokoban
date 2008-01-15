namespace SokoSolve.UI.Section.Library.Items
{
    partial class PayloadLibrary
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageReport = new System.Windows.Forms.TabPage();
            this.tabPageQuickView = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.htmlView1 = new SokoSolve.UI.Controls.Web.HtmlView();
            this.ucPuzzleList = new SokoSolve.UI.Controls.Secondary.ucPuzzleList();
            this.tabControl1.SuspendLayout();
            this.tabPageReport.SuspendLayout();
            this.tabPageQuickView.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageReport);
            this.tabControl1.Controls.Add(this.tabPageQuickView);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(474, 384);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageReport
            // 
            this.tabPageReport.Controls.Add(this.htmlView1);
            this.tabPageReport.Location = new System.Drawing.Point(4, 22);
            this.tabPageReport.Name = "tabPageReport";
            this.tabPageReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReport.Size = new System.Drawing.Size(466, 358);
            this.tabPageReport.TabIndex = 0;
            this.tabPageReport.Text = "Report";
            this.tabPageReport.UseVisualStyleBackColor = true;
            // 
            // tabPageQuickView
            // 
            this.tabPageQuickView.Controls.Add(this.ucPuzzleList);
            this.tabPageQuickView.Controls.Add(this.toolStrip1);
            this.tabPageQuickView.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuickView.Name = "tabPageQuickView";
            this.tabPageQuickView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQuickView.Size = new System.Drawing.Size(466, 358);
            this.tabPageQuickView.TabIndex = 1;
            this.tabPageQuickView.Text = "Quick View";
            this.tabPageQuickView.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(460, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::SokoSolve.UI.Properties.Resources.Up;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::SokoSolve.UI.Properties.Resources.Down;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
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
            this.htmlView1.ShowStatus = false;
            this.htmlView1.Size = new System.Drawing.Size(460, 352);
            this.htmlView1.TabIndex = 1;
            // 
            // ucPuzzleList
            // 
            this.ucPuzzleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPuzzleList.Library = null;
            this.ucPuzzleList.Location = new System.Drawing.Point(3, 28);
            this.ucPuzzleList.Name = "ucPuzzleList";
            this.ucPuzzleList.Size = new System.Drawing.Size(460, 327);
            this.ucPuzzleList.TabIndex = 2;
            this.ucPuzzleList.UseCheckBoxes = false;
            // 
            // PayloadLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "PayloadLibrary";
            this.Size = new System.Drawing.Size(474, 384);
            this.tabControl1.ResumeLayout(false);
            this.tabPageReport.ResumeLayout(false);
            this.tabPageQuickView.ResumeLayout(false);
            this.tabPageQuickView.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageReport;
        public SokoSolve.UI.Controls.Web.HtmlView htmlView1;
        private System.Windows.Forms.TabPage tabPageQuickView;
        public SokoSolve.UI.Controls.Secondary.ucPuzzleList ucPuzzleList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;

    }
}
