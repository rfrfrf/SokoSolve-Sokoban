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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayloadLibrary));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageReport = new System.Windows.Forms.TabPage();
            this.htmlView1 = new SokoSolve.UI.Controls.Web.HtmlView();
            this.tabPageQuickView = new System.Windows.Forms.TabPage();
            this.ucPuzzleList = new SokoSolve.UI.Controls.Secondary.ucPuzzleList();
            this.toolStripLibrary = new System.Windows.Forms.ToolStrip();
            this.tsbOrderUp = new System.Windows.Forms.ToolStripButton();
            this.tsbOrderDown = new System.Windows.Forms.ToolStripButton();
            this.tsbPreview = new System.Windows.Forms.ToolStripButton();
            this.tsbShowGroups = new System.Windows.Forms.ToolStripButton();
            this.tabPageCategories = new System.Windows.Forms.TabPage();
            this.listViewCat = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.toolStripCategories = new System.Windows.Forms.ToolStrip();
            this.tsbCatUp = new System.Windows.Forms.ToolStripButton();
            this.tsbCatDown = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPageReport.SuspendLayout();
            this.tabPageQuickView.SuspendLayout();
            this.toolStripLibrary.SuspendLayout();
            this.tabPageCategories.SuspendLayout();
            this.toolStripCategories.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageReport);
            this.tabControl1.Controls.Add(this.tabPageQuickView);
            this.tabControl1.Controls.Add(this.tabPageCategories);
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
            // tabPageQuickView
            // 
            this.tabPageQuickView.Controls.Add(this.ucPuzzleList);
            this.tabPageQuickView.Controls.Add(this.toolStripLibrary);
            this.tabPageQuickView.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuickView.Name = "tabPageQuickView";
            this.tabPageQuickView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQuickView.Size = new System.Drawing.Size(466, 358);
            this.tabPageQuickView.TabIndex = 1;
            this.tabPageQuickView.Text = "Quick View";
            this.tabPageQuickView.UseVisualStyleBackColor = true;
            // 
            // ucPuzzleList
            // 
            this.ucPuzzleList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPuzzleList.Library = null;
            this.ucPuzzleList.Location = new System.Drawing.Point(6, 31);
            this.ucPuzzleList.Name = "ucPuzzleList";
            this.ucPuzzleList.ShowGroups = true;
            this.ucPuzzleList.ShowPreview = true;
            this.ucPuzzleList.Size = new System.Drawing.Size(454, 321);
            this.ucPuzzleList.TabIndex = 2;
            this.ucPuzzleList.UseCheckBoxes = false;
            // 
            // toolStripLibrary
            // 
            this.toolStripLibrary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOrderUp,
            this.tsbOrderDown,
            this.tsbPreview,
            this.tsbShowGroups});
            this.toolStripLibrary.Location = new System.Drawing.Point(3, 3);
            this.toolStripLibrary.Name = "toolStripLibrary";
            this.toolStripLibrary.Size = new System.Drawing.Size(460, 25);
            this.toolStripLibrary.TabIndex = 3;
            this.toolStripLibrary.Text = "toolStrip1";
            // 
            // tsbOrderUp
            // 
            this.tsbOrderUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOrderUp.Image = global::SokoSolve.UI.Properties.Resources.Up;
            this.tsbOrderUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOrderUp.Name = "tsbOrderUp";
            this.tsbOrderUp.Size = new System.Drawing.Size(23, 22);
            this.tsbOrderUp.Text = "Move Up";
            this.tsbOrderUp.Click += new System.EventHandler(this.tsbOrderUp_Click);
            // 
            // tsbOrderDown
            // 
            this.tsbOrderDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOrderDown.Image = global::SokoSolve.UI.Properties.Resources.Down;
            this.tsbOrderDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOrderDown.Name = "tsbOrderDown";
            this.tsbOrderDown.Size = new System.Drawing.Size(23, 22);
            this.tsbOrderDown.Text = "Move Down";
            this.tsbOrderDown.Click += new System.EventHandler(this.tsbOrderDown_Click);
            // 
            // tsbPreview
            // 
            this.tsbPreview.Checked = true;
            this.tsbPreview.CheckOnClick = true;
            this.tsbPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPreview.Image = ((System.Drawing.Image)(resources.GetObject("tsbPreview.Image")));
            this.tsbPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPreview.Name = "tsbPreview";
            this.tsbPreview.Size = new System.Drawing.Size(78, 22);
            this.tsbPreview.Text = "Show Preview";
            this.tsbPreview.Click += new System.EventHandler(this.tsbPreview_Click);
            // 
            // tsbShowGroups
            // 
            this.tsbShowGroups.Checked = true;
            this.tsbShowGroups.CheckOnClick = true;
            this.tsbShowGroups.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbShowGroups.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbShowGroups.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowGroups.Image")));
            this.tsbShowGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowGroups.Name = "tsbShowGroups";
            this.tsbShowGroups.Size = new System.Drawing.Size(74, 22);
            this.tsbShowGroups.Text = "Show Groups";
            this.tsbShowGroups.Click += new System.EventHandler(this.tsbShowGroups_Click);
            // 
            // tabPageCategories
            // 
            this.tabPageCategories.Controls.Add(this.listViewCat);
            this.tabPageCategories.Controls.Add(this.toolStripCategories);
            this.tabPageCategories.Location = new System.Drawing.Point(4, 22);
            this.tabPageCategories.Name = "tabPageCategories";
            this.tabPageCategories.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCategories.Size = new System.Drawing.Size(466, 358);
            this.tabPageCategories.TabIndex = 2;
            this.tabPageCategories.Text = "Category List";
            this.tabPageCategories.UseVisualStyleBackColor = true;
            // 
            // listViewCat
            // 
            this.listViewCat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCat.FullRowSelect = true;
            this.listViewCat.Location = new System.Drawing.Point(3, 28);
            this.listViewCat.MultiSelect = false;
            this.listViewCat.Name = "listViewCat";
            this.listViewCat.Size = new System.Drawing.Size(460, 327);
            this.listViewCat.TabIndex = 1;
            this.listViewCat.UseCompatibleStateImageBehavior = false;
            this.listViewCat.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Order";
            this.columnHeader1.Width = 88;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 273;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Puzzles";
            // 
            // toolStripCategories
            // 
            this.toolStripCategories.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCatUp,
            this.tsbCatDown});
            this.toolStripCategories.Location = new System.Drawing.Point(3, 3);
            this.toolStripCategories.Name = "toolStripCategories";
            this.toolStripCategories.Size = new System.Drawing.Size(460, 25);
            this.toolStripCategories.TabIndex = 0;
            this.toolStripCategories.Text = "toolStrip2";
            // 
            // tsbCatUp
            // 
            this.tsbCatUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCatUp.Image = global::SokoSolve.UI.Properties.Resources.Up;
            this.tsbCatUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCatUp.Name = "tsbCatUp";
            this.tsbCatUp.Size = new System.Drawing.Size(23, 22);
            this.tsbCatUp.Text = "Move Up";
            this.tsbCatUp.Click += new System.EventHandler(this.tsbCatUp_Click);
            // 
            // tsbCatDown
            // 
            this.tsbCatDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCatDown.Image = global::SokoSolve.UI.Properties.Resources.Down;
            this.tsbCatDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCatDown.Name = "tsbCatDown";
            this.tsbCatDown.Size = new System.Drawing.Size(23, 22);
            this.tsbCatDown.Text = "Move Down";
            this.tsbCatDown.Click += new System.EventHandler(this.tsbCatDown_Click);
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
            this.toolStripLibrary.ResumeLayout(false);
            this.toolStripLibrary.PerformLayout();
            this.tabPageCategories.ResumeLayout(false);
            this.tabPageCategories.PerformLayout();
            this.toolStripCategories.ResumeLayout(false);
            this.toolStripCategories.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageReport;
        public SokoSolve.UI.Controls.Web.HtmlView htmlView1;
        private System.Windows.Forms.TabPage tabPageQuickView;
        public SokoSolve.UI.Controls.Secondary.ucPuzzleList ucPuzzleList;
        private System.Windows.Forms.ToolStrip toolStripLibrary;
        private System.Windows.Forms.ToolStripButton tsbOrderUp;
        private System.Windows.Forms.ToolStripButton tsbOrderDown;
        private System.Windows.Forms.ToolStripButton tsbPreview;
        private System.Windows.Forms.ToolStripButton tsbShowGroups;
        private System.Windows.Forms.TabPage tabPageCategories;
        private System.Windows.Forms.ToolStrip toolStripCategories;
        private System.Windows.Forms.ListView listViewCat;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripButton tsbCatUp;
        private System.Windows.Forms.ToolStripButton tsbCatDown;

    }
}
