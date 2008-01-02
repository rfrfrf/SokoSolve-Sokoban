namespace SokoSolve.UI.Section.Solver
{
    partial class SolverSection
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
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbStart = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.tsbPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerTop = new System.Windows.Forms.SplitContainer();
            this.treeViewer = new SokoSolve.UI.Section.Solver.TreeViewer();
            this.tabControlTopLeft = new System.Windows.Forms.TabControl();
            this.tabPageStaticImage = new System.Windows.Forms.TabPage();
            this.pictureBoxStaticImage = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainerBottom = new System.Windows.Forms.SplitContainer();
            this.tabControlStaticDetails = new System.Windows.Forms.TabControl();
            this.tabPageStaticMaps = new System.Windows.Forms.TabPage();
            this.bitmapViewerStatic = new SokoSolve.UI.Section.Solver.BitmapViewer();
            this.tabPageStats = new System.Windows.Forms.TabPage();
            this.labelStats = new System.Windows.Forms.Label();
            this.tabControlNode = new System.Windows.Forms.TabControl();
            this.tabPageNodeDetails = new System.Windows.Forms.TabPage();
            this.bitmapViewerNodeMaps = new SokoSolve.UI.Section.Solver.BitmapViewer();
            this.labelNodeDetails = new System.Windows.Forms.Label();
            this.tabPageNodePuzzle = new System.Windows.Forms.TabPage();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxNodePuzzle = new System.Windows.Forms.PictureBox();
            this.toolStripMain.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.splitContainerTop.Panel1.SuspendLayout();
            this.splitContainerTop.Panel2.SuspendLayout();
            this.splitContainerTop.SuspendLayout();
            this.tabControlTopLeft.SuspendLayout();
            this.tabPageStaticImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStaticImage)).BeginInit();
            this.splitContainerBottom.Panel1.SuspendLayout();
            this.splitContainerBottom.Panel2.SuspendLayout();
            this.splitContainerBottom.SuspendLayout();
            this.tabControlStaticDetails.SuspendLayout();
            this.tabPageStaticMaps.SuspendLayout();
            this.tabPageStats.SuspendLayout();
            this.tabControlNode.SuspendLayout();
            this.tabPageNodeDetails.SuspendLayout();
            this.tabPageNodePuzzle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNodePuzzle)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbStart,
            this.tsbStop,
            this.tsbPause,
            this.toolStripSeparator1,
            this.tslStatus,
            this.toolStripSeparator2,
            this.tsbExit});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(575, 25);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "Main Options";
            // 
            // tsbStart
            // 
            this.tsbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStart.Enabled = false;
            this.tsbStart.Image = global::SokoSolve.UI.Properties.Resources.Play;
            this.tsbStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStart.Name = "tsbStart";
            this.tsbStart.Size = new System.Drawing.Size(23, 22);
            this.tsbStart.Text = "toolStripButton1";
            this.tsbStart.Click += new System.EventHandler(this.tsbStart_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Enabled = false;
            this.tsbStop.Image = global::SokoSolve.UI.Properties.Resources.Stop;
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(23, 22);
            this.tsbStop.Text = "toolStripButton2";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // tsbPause
            // 
            this.tsbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPause.Enabled = false;
            this.tsbPause.Image = global::SokoSolve.UI.Properties.Resources.Pause;
            this.tsbPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPause.Name = "tsbPause";
            this.tsbPause.Size = new System.Drawing.Size(23, 22);
            this.tsbPause.Text = "toolStripButton3";
            this.tsbPause.Click += new System.EventHandler(this.tsbPause_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslStatus
            // 
            this.tslStatus.AutoSize = false;
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(300, 22);
            this.tslStatus.Text = "No puzzle selected";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExit
            // 
            this.tsbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExit.Image = global::SokoSolve.UI.Properties.Resources.Exit;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(23, 22);
            this.tsbExit.Text = "toolStripButton4";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 25);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerTop);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerBottom);
            this.splitContainerMain.Size = new System.Drawing.Size(575, 428);
            this.splitContainerMain.SplitterDistance = 238;
            this.splitContainerMain.TabIndex = 1;
            // 
            // splitContainerTop
            // 
            this.splitContainerTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTop.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTop.Name = "splitContainerTop";
            // 
            // splitContainerTop.Panel1
            // 
            this.splitContainerTop.Panel1.AutoScroll = true;
            this.splitContainerTop.Panel1.Controls.Add(this.treeViewer);
            // 
            // splitContainerTop.Panel2
            // 
            this.splitContainerTop.Panel2.Controls.Add(this.tabControlTopLeft);
            this.splitContainerTop.Size = new System.Drawing.Size(575, 238);
            this.splitContainerTop.SplitterDistance = 400;
            this.splitContainerTop.TabIndex = 0;
            // 
            // treeViewer
            // 
            this.treeViewer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.treeViewer.Cursor = System.Windows.Forms.Cursors.Cross;
            this.treeViewer.Location = new System.Drawing.Point(1, 1);
            this.treeViewer.Name = "treeViewer";
            this.treeViewer.Size = new System.Drawing.Size(1800, 480);
            this.treeViewer.TabIndex = 0;
            this.treeViewer.OnClickNode += new System.EventHandler<SokoSolve.Core.Analysis.Solver.SolverNodeEventArgs>(this.treeViewer_OnClickNode);
            // 
            // tabControlTopLeft
            // 
            this.tabControlTopLeft.Controls.Add(this.tabPageStaticImage);
            this.tabControlTopLeft.Controls.Add(this.tabPage2);
            this.tabControlTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTopLeft.Location = new System.Drawing.Point(0, 0);
            this.tabControlTopLeft.Name = "tabControlTopLeft";
            this.tabControlTopLeft.SelectedIndex = 0;
            this.tabControlTopLeft.Size = new System.Drawing.Size(171, 238);
            this.tabControlTopLeft.TabIndex = 0;
            // 
            // tabPageStaticImage
            // 
            this.tabPageStaticImage.Controls.Add(this.pictureBoxStaticImage);
            this.tabPageStaticImage.Location = new System.Drawing.Point(4, 22);
            this.tabPageStaticImage.Name = "tabPageStaticImage";
            this.tabPageStaticImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStaticImage.Size = new System.Drawing.Size(163, 212);
            this.tabPageStaticImage.TabIndex = 0;
            this.tabPageStaticImage.Text = "Puzzle Initial";
            this.tabPageStaticImage.UseVisualStyleBackColor = true;
            // 
            // pictureBoxStaticImage
            // 
            this.pictureBoxStaticImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxStaticImage.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxStaticImage.Name = "pictureBoxStaticImage";
            this.pictureBoxStaticImage.Size = new System.Drawing.Size(157, 206);
            this.pictureBoxStaticImage.TabIndex = 0;
            this.pictureBoxStaticImage.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(163, 212);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainerBottom
            // 
            this.splitContainerBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerBottom.Location = new System.Drawing.Point(0, 0);
            this.splitContainerBottom.Name = "splitContainerBottom";
            // 
            // splitContainerBottom.Panel1
            // 
            this.splitContainerBottom.Panel1.Controls.Add(this.tabControlStaticDetails);
            // 
            // splitContainerBottom.Panel2
            // 
            this.splitContainerBottom.Panel2.Controls.Add(this.tabControlNode);
            this.splitContainerBottom.Size = new System.Drawing.Size(575, 186);
            this.splitContainerBottom.SplitterDistance = 300;
            this.splitContainerBottom.TabIndex = 0;
            // 
            // tabControlStaticDetails
            // 
            this.tabControlStaticDetails.Controls.Add(this.tabPageStaticMaps);
            this.tabControlStaticDetails.Controls.Add(this.tabPageStats);
            this.tabControlStaticDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlStaticDetails.Location = new System.Drawing.Point(0, 0);
            this.tabControlStaticDetails.Name = "tabControlStaticDetails";
            this.tabControlStaticDetails.SelectedIndex = 0;
            this.tabControlStaticDetails.Size = new System.Drawing.Size(300, 186);
            this.tabControlStaticDetails.TabIndex = 1;
            // 
            // tabPageStaticMaps
            // 
            this.tabPageStaticMaps.Controls.Add(this.bitmapViewerStatic);
            this.tabPageStaticMaps.Location = new System.Drawing.Point(4, 22);
            this.tabPageStaticMaps.Name = "tabPageStaticMaps";
            this.tabPageStaticMaps.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStaticMaps.Size = new System.Drawing.Size(292, 160);
            this.tabPageStaticMaps.TabIndex = 0;
            this.tabPageStaticMaps.Text = "Static Maps";
            this.tabPageStaticMaps.UseVisualStyleBackColor = true;
            // 
            // bitmapViewerStatic
            // 
            this.bitmapViewerStatic.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bitmapViewerStatic.Cursor = System.Windows.Forms.Cursors.Cross;
            this.bitmapViewerStatic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitmapViewerStatic.Location = new System.Drawing.Point(3, 3);
            this.bitmapViewerStatic.Name = "bitmapViewerStatic";
            this.bitmapViewerStatic.Size = new System.Drawing.Size(286, 154);
            this.bitmapViewerStatic.TabIndex = 0;
            // 
            // tabPageStats
            // 
            this.tabPageStats.Controls.Add(this.labelStats);
            this.tabPageStats.Location = new System.Drawing.Point(4, 22);
            this.tabPageStats.Name = "tabPageStats";
            this.tabPageStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStats.Size = new System.Drawing.Size(292, 160);
            this.tabPageStats.TabIndex = 1;
            this.tabPageStats.Text = "Statistics";
            this.tabPageStats.UseVisualStyleBackColor = true;
            this.tabPageStats.Click += new System.EventHandler(this.tabPageStats_Click);
            // 
            // labelStats
            // 
            this.labelStats.AutoSize = true;
            this.labelStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStats.Location = new System.Drawing.Point(3, 3);
            this.labelStats.Name = "labelStats";
            this.labelStats.Size = new System.Drawing.Size(0, 13);
            this.labelStats.TabIndex = 0;
            // 
            // tabControlNode
            // 
            this.tabControlNode.Controls.Add(this.tabPageNodeDetails);
            this.tabControlNode.Controls.Add(this.tabPageNodePuzzle);
            this.tabControlNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlNode.Location = new System.Drawing.Point(0, 0);
            this.tabControlNode.Name = "tabControlNode";
            this.tabControlNode.SelectedIndex = 0;
            this.tabControlNode.Size = new System.Drawing.Size(271, 186);
            this.tabControlNode.TabIndex = 0;
            // 
            // tabPageNodeDetails
            // 
            this.tabPageNodeDetails.Controls.Add(this.bitmapViewerNodeMaps);
            this.tabPageNodeDetails.Controls.Add(this.labelNodeDetails);
            this.tabPageNodeDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageNodeDetails.Name = "tabPageNodeDetails";
            this.tabPageNodeDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNodeDetails.Size = new System.Drawing.Size(263, 160);
            this.tabPageNodeDetails.TabIndex = 0;
            this.tabPageNodeDetails.Text = "Current Node Maps";
            this.tabPageNodeDetails.UseVisualStyleBackColor = true;
            // 
            // bitmapViewerNodeMaps
            // 
            this.bitmapViewerNodeMaps.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bitmapViewerNodeMaps.Cursor = System.Windows.Forms.Cursors.Cross;
            this.bitmapViewerNodeMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitmapViewerNodeMaps.Location = new System.Drawing.Point(3, 3);
            this.bitmapViewerNodeMaps.Name = "bitmapViewerNodeMaps";
            this.bitmapViewerNodeMaps.Size = new System.Drawing.Size(137, 154);
            this.bitmapViewerNodeMaps.TabIndex = 0;
            // 
            // labelNodeDetails
            // 
            this.labelNodeDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelNodeDetails.Location = new System.Drawing.Point(140, 3);
            this.labelNodeDetails.Name = "labelNodeDetails";
            this.labelNodeDetails.Size = new System.Drawing.Size(120, 154);
            this.labelNodeDetails.TabIndex = 1;
            // 
            // tabPageNodePuzzle
            // 
            this.tabPageNodePuzzle.Controls.Add(this.pictureBoxNodePuzzle);
            this.tabPageNodePuzzle.Location = new System.Drawing.Point(4, 22);
            this.tabPageNodePuzzle.Name = "tabPageNodePuzzle";
            this.tabPageNodePuzzle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNodePuzzle.Size = new System.Drawing.Size(263, 160);
            this.tabPageNodePuzzle.TabIndex = 1;
            this.tabPageNodePuzzle.Text = "Current Puzzle";
            this.tabPageNodePuzzle.UseVisualStyleBackColor = true;
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureBoxNodePuzzle
            // 
            this.pictureBoxNodePuzzle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxNodePuzzle.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxNodePuzzle.Name = "pictureBoxNodePuzzle";
            this.pictureBoxNodePuzzle.Size = new System.Drawing.Size(257, 154);
            this.pictureBoxNodePuzzle.TabIndex = 0;
            this.pictureBoxNodePuzzle.TabStop = false;
            // 
            // SolverSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStripMain);
            this.Name = "SolverSection";
            this.Size = new System.Drawing.Size(575, 453);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerTop.Panel1.ResumeLayout(false);
            this.splitContainerTop.Panel2.ResumeLayout(false);
            this.splitContainerTop.ResumeLayout(false);
            this.tabControlTopLeft.ResumeLayout(false);
            this.tabPageStaticImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStaticImage)).EndInit();
            this.splitContainerBottom.Panel1.ResumeLayout(false);
            this.splitContainerBottom.Panel2.ResumeLayout(false);
            this.splitContainerBottom.ResumeLayout(false);
            this.tabControlStaticDetails.ResumeLayout(false);
            this.tabPageStaticMaps.ResumeLayout(false);
            this.tabPageStats.ResumeLayout(false);
            this.tabPageStats.PerformLayout();
            this.tabControlNode.ResumeLayout(false);
            this.tabPageNodeDetails.ResumeLayout(false);
            this.tabPageNodePuzzle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNodePuzzle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerTop;
        private System.Windows.Forms.SplitContainer splitContainerBottom;
        private System.Windows.Forms.ToolStripButton tsbStart;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.ToolStripButton tsbPause;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private BitmapViewer bitmapViewerStatic;
        private TreeViewer treeViewer;
        private System.Windows.Forms.TabControl tabControlStaticDetails;
        private System.Windows.Forms.TabPage tabPageStaticMaps;
        private System.Windows.Forms.TabPage tabPageStats;
        private System.Windows.Forms.TabControl tabControlNode;
        private System.Windows.Forms.TabPage tabPageNodeDetails;
        private System.Windows.Forms.TabPage tabPageNodePuzzle;
        private System.Windows.Forms.Label labelNodeDetails;
        private BitmapViewer bitmapViewerNodeMaps;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelStats;
        private System.Windows.Forms.TabControl tabControlTopLeft;
        private System.Windows.Forms.TabPage tabPageStaticImage;
        private System.Windows.Forms.PictureBox pictureBoxStaticImage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBoxNodePuzzle;
    }
}
