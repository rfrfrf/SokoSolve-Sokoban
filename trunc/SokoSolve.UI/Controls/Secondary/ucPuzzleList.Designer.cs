namespace SokoSolve.UI.Controls.Secondary
{
    partial class ucPuzzleList
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Category: Easy (23 items)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sasquatch",
            "1",
            "34.20",
            "10w12h, 4 crates, 2 solutions"}, -1);
            this.listViewPuzzles = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewPuzzles
            // 
            this.listViewPuzzles.AutoArrange = false;
            this.listViewPuzzles.CheckBoxes = true;
            this.listViewPuzzles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewPuzzles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPuzzles.FullRowSelect = true;
            listViewGroup1.Header = "Category: Easy (23 items)";
            listViewGroup1.Name = "listViewGroup1";
            this.listViewPuzzles.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            this.listViewPuzzles.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewPuzzles.Location = new System.Drawing.Point(0, 0);
            this.listViewPuzzles.Name = "listViewPuzzles";
            this.listViewPuzzles.Size = new System.Drawing.Size(235, 295);
            this.listViewPuzzles.TabIndex = 0;
            this.listViewPuzzles.UseCompatibleStateImageBehavior = false;
            this.listViewPuzzles.View = System.Windows.Forms.View.Details;
            this.listViewPuzzles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewPuzzles_ColumnClick);
            this.listViewPuzzles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewPuzzles_ItemSelectionChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Puzzle";
            this.columnHeader3.Width = 122;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Order";
            this.columnHeader4.Width = 38;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Rating";
            this.columnHeader5.Width = 44;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Description";
            this.columnHeader6.Width = 193;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.ToolTipTitle = "Puzzle";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listViewPuzzles);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AutoScroll = true;
            this.splitContainer.Panel2.Controls.Add(this.pictureBoxMap);
            this.splitContainer.Size = new System.Drawing.Size(411, 295);
            this.splitContainer.SplitterDistance = 235;
            this.splitContainer.TabIndex = 2;
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxMap.TabIndex = 0;
            this.pictureBoxMap.TabStop = false;
            // 
            // ucPuzzleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "ucPuzzleList";
            this.Size = new System.Drawing.Size(411, 295);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewPuzzles;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox pictureBoxMap;
    }
}
