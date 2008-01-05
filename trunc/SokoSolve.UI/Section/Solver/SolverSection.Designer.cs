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
            this.tsDropDown = new System.Windows.Forms.ToolStripComboBox();
            this.tsbRefreshVis = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslStatus = new System.Windows.Forms.ToolStripLabel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerBottom = new System.Windows.Forms.SplitContainer();
            this.tabControlStaticDetails = new System.Windows.Forms.TabControl();
            this.tabPageStaticMaps = new System.Windows.Forms.TabPage();
            this.tabPageStats = new System.Windows.Forms.TabPage();
            this.webBrowserGlobalStats = new System.Windows.Forms.WebBrowser();
            this.tabPageEvalList = new System.Windows.Forms.TabPage();
            this.tabControlNode = new System.Windows.Forms.TabControl();
            this.tabPageNodeDetails = new System.Windows.Forms.TabPage();
            this.tabPageNodePuzzle = new System.Windows.Forms.TabPage();
            this.pictureBoxNodePuzzle = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.splitContainerTop = new System.Windows.Forms.SplitContainer();
            this.treeViewer = new SokoSolve.UI.Section.Solver.TreeViewer();
            this.tabControlTopLeft = new System.Windows.Forms.TabControl();
            this.tabPageStaticImage = new System.Windows.Forms.TabPage();
            this.pictureBoxStaticImage = new System.Windows.Forms.PictureBox();
            this.tabPageCurrentHTML = new System.Windows.Forms.TabPage();
            this.webBrowserNodeCurrent = new System.Windows.Forms.WebBrowser();
            this.tabPageLocalNodes = new System.Windows.Forms.TabPage();
            this.visualisationContainerLocalNodes = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.bitmapViewerStatic = new SokoSolve.UI.Section.Solver.BitmapViewer();
            this.visualisationContainerEvalList = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.bitmapViewerNodeMaps = new SokoSolve.UI.Section.Solver.BitmapViewer();
            this.toolStripMain.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.splitContainerBottom.Panel1.SuspendLayout();
            this.splitContainerBottom.Panel2.SuspendLayout();
            this.splitContainerBottom.SuspendLayout();
            this.tabControlStaticDetails.SuspendLayout();
            this.tabPageStaticMaps.SuspendLayout();
            this.tabPageStats.SuspendLayout();
            this.tabPageEvalList.SuspendLayout();
            this.tabControlNode.SuspendLayout();
            this.tabPageNodeDetails.SuspendLayout();
            this.tabPageNodePuzzle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNodePuzzle)).BeginInit();
            this.splitContainerTop.Panel1.SuspendLayout();
            this.splitContainerTop.Panel2.SuspendLayout();
            this.splitContainerTop.SuspendLayout();
            this.tabControlTopLeft.SuspendLayout();
            this.tabPageStaticImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStaticImage)).BeginInit();
            this.tabPageCurrentHTML.SuspendLayout();
            this.tabPageLocalNodes.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbStart,
            this.tsbStop,
            this.tsbPause,
            this.toolStripSeparator1,
            this.tsDropDown,
            this.tsbRefreshVis,
            this.tsbExit,
            this.toolStripSeparator3,
            this.tslStatus});
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
            this.tsbStart.Text = "Start the solver";
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
            this.tsbStop.Text = "Stop the Solver";
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
            this.tsbPause.Text = "Pause the solver";
            this.tsbPause.Click += new System.EventHandler(this.tsbPause_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsDropDown
            // 
            this.tsDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsDropDown.Items.AddRange(new object[] {
            "Visualisation Off",
            "3 sec",
            "5 sec",
            "10 sec",
            "30 sec",
            "60 sec"});
            this.tsDropDown.Name = "tsDropDown";
            this.tsDropDown.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbRefreshVis
            // 
            this.tsbRefreshVis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefreshVis.Image = global::SokoSolve.UI.Properties.Resources.Refresh;
            this.tsbRefreshVis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshVis.Name = "tsbRefreshVis";
            this.tsbRefreshVis.Size = new System.Drawing.Size(23, 22);
            this.tsbRefreshVis.Text = "Refresh the visualisation";
            this.tsbRefreshVis.Click += new System.EventHandler(this.tsbRefreshVis_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExit.Image = global::SokoSolve.UI.Properties.Resources.Exit;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(23, 22);
            this.tsbExit.Text = "Exit the solver";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(96, 22);
            this.tslStatus.Text = "No puzzle selected";
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
            this.tabControlStaticDetails.Controls.Add(this.tabPageEvalList);
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
            // tabPageStats
            // 
            this.tabPageStats.Controls.Add(this.webBrowserGlobalStats);
            this.tabPageStats.Location = new System.Drawing.Point(4, 22);
            this.tabPageStats.Name = "tabPageStats";
            this.tabPageStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStats.Size = new System.Drawing.Size(292, 160);
            this.tabPageStats.TabIndex = 1;
            this.tabPageStats.Text = "Statistics";
            this.tabPageStats.UseVisualStyleBackColor = true;
            this.tabPageStats.Click += new System.EventHandler(this.tabPageStats_Click);
            // 
            // webBrowserGlobalStats
            // 
            this.webBrowserGlobalStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserGlobalStats.Location = new System.Drawing.Point(3, 3);
            this.webBrowserGlobalStats.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserGlobalStats.Name = "webBrowserGlobalStats";
            this.webBrowserGlobalStats.Size = new System.Drawing.Size(286, 154);
            this.webBrowserGlobalStats.TabIndex = 0;
            // 
            // tabPageEvalList
            // 
            this.tabPageEvalList.Controls.Add(this.visualisationContainerEvalList);
            this.tabPageEvalList.Location = new System.Drawing.Point(4, 22);
            this.tabPageEvalList.Name = "tabPageEvalList";
            this.tabPageEvalList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEvalList.Size = new System.Drawing.Size(292, 160);
            this.tabPageEvalList.TabIndex = 2;
            this.tabPageEvalList.Text = "Eval List";
            this.tabPageEvalList.UseVisualStyleBackColor = true;
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
            this.tabPageNodeDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageNodeDetails.Name = "tabPageNodeDetails";
            this.tabPageNodeDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNodeDetails.Size = new System.Drawing.Size(263, 160);
            this.tabPageNodeDetails.TabIndex = 0;
            this.tabPageNodeDetails.Text = "Current Node Maps";
            this.tabPageNodeDetails.UseVisualStyleBackColor = true;
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
            // pictureBoxNodePuzzle
            // 
            this.pictureBoxNodePuzzle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxNodePuzzle.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxNodePuzzle.Name = "pictureBoxNodePuzzle";
            this.pictureBoxNodePuzzle.Size = new System.Drawing.Size(257, 154);
            this.pictureBoxNodePuzzle.TabIndex = 0;
            this.pictureBoxNodePuzzle.TabStop = false;
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
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
            this.treeViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewer.Location = new System.Drawing.Point(0, 0);
            this.treeViewer.Name = "treeViewer";
            this.treeViewer.OnVisualisationClick = null;
            this.treeViewer.Size = new System.Drawing.Size(400, 238);
            this.treeViewer.TabIndex = 0;
            // 
            // tabControlTopLeft
            // 
            this.tabControlTopLeft.Controls.Add(this.tabPageLocalNodes);
            this.tabControlTopLeft.Controls.Add(this.tabPageCurrentHTML);
            this.tabControlTopLeft.Controls.Add(this.tabPageStaticImage);
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
            // tabPageCurrentHTML
            // 
            this.tabPageCurrentHTML.Controls.Add(this.webBrowserNodeCurrent);
            this.tabPageCurrentHTML.Location = new System.Drawing.Point(4, 22);
            this.tabPageCurrentHTML.Name = "tabPageCurrentHTML";
            this.tabPageCurrentHTML.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCurrentHTML.Size = new System.Drawing.Size(163, 212);
            this.tabPageCurrentHTML.TabIndex = 1;
            this.tabPageCurrentHTML.Text = "Current Node";
            this.tabPageCurrentHTML.UseVisualStyleBackColor = true;
            // 
            // webBrowserNodeCurrent
            // 
            this.webBrowserNodeCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserNodeCurrent.Location = new System.Drawing.Point(3, 3);
            this.webBrowserNodeCurrent.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserNodeCurrent.Name = "webBrowserNodeCurrent";
            this.webBrowserNodeCurrent.Size = new System.Drawing.Size(157, 206);
            this.webBrowserNodeCurrent.TabIndex = 0;
            // 
            // tabPageLocalNodes
            // 
            this.tabPageLocalNodes.Controls.Add(this.visualisationContainerLocalNodes);
            this.tabPageLocalNodes.Location = new System.Drawing.Point(4, 22);
            this.tabPageLocalNodes.Name = "tabPageLocalNodes";
            this.tabPageLocalNodes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLocalNodes.Size = new System.Drawing.Size(163, 212);
            this.tabPageLocalNodes.TabIndex = 2;
            this.tabPageLocalNodes.Text = "Local Nodes";
            this.tabPageLocalNodes.UseVisualStyleBackColor = true;
            // 
            // visualisationContainerLocalNodes
            // 
            this.visualisationContainerLocalNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualisationContainerLocalNodes.Location = new System.Drawing.Point(3, 3);
            this.visualisationContainerLocalNodes.Name = "visualisationContainerLocalNodes";
            this.visualisationContainerLocalNodes.RenderOnClick = true;
            this.visualisationContainerLocalNodes.Size = new System.Drawing.Size(157, 206);
            this.visualisationContainerLocalNodes.Status = "Status";
            this.visualisationContainerLocalNodes.TabIndex = 0;
            this.visualisationContainerLocalNodes.Visualisation = null;
            // 
            // bitmapViewerStatic
            // 
            this.bitmapViewerStatic.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bitmapViewerStatic.Cursor = System.Windows.Forms.Cursors.Cross;
            this.bitmapViewerStatic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitmapViewerStatic.Location = new System.Drawing.Point(3, 3);
            this.bitmapViewerStatic.MapSize = null;
            this.bitmapViewerStatic.Name = "bitmapViewerStatic";
            this.bitmapViewerStatic.Size = new System.Drawing.Size(286, 154);
            this.bitmapViewerStatic.TabIndex = 0;
            // 
            // visualisationContainerEvalList
            // 
            this.visualisationContainerEvalList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualisationContainerEvalList.Location = new System.Drawing.Point(3, 3);
            this.visualisationContainerEvalList.Name = "visualisationContainerEvalList";
            this.visualisationContainerEvalList.RenderOnClick = true;
            this.visualisationContainerEvalList.Size = new System.Drawing.Size(286, 154);
            this.visualisationContainerEvalList.Status = "Not Implemented";
            this.visualisationContainerEvalList.TabIndex = 0;
            this.visualisationContainerEvalList.Visualisation = null;
            // 
            // bitmapViewerNodeMaps
            // 
            this.bitmapViewerNodeMaps.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bitmapViewerNodeMaps.Cursor = System.Windows.Forms.Cursors.Cross;
            this.bitmapViewerNodeMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitmapViewerNodeMaps.Location = new System.Drawing.Point(3, 3);
            this.bitmapViewerNodeMaps.MapSize = null;
            this.bitmapViewerNodeMaps.Name = "bitmapViewerNodeMaps";
            this.bitmapViewerNodeMaps.Size = new System.Drawing.Size(257, 154);
            this.bitmapViewerNodeMaps.TabIndex = 0;
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
            this.splitContainerBottom.Panel1.ResumeLayout(false);
            this.splitContainerBottom.Panel2.ResumeLayout(false);
            this.splitContainerBottom.ResumeLayout(false);
            this.tabControlStaticDetails.ResumeLayout(false);
            this.tabPageStaticMaps.ResumeLayout(false);
            this.tabPageStats.ResumeLayout(false);
            this.tabPageEvalList.ResumeLayout(false);
            this.tabControlNode.ResumeLayout(false);
            this.tabPageNodeDetails.ResumeLayout(false);
            this.tabPageNodePuzzle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNodePuzzle)).EndInit();
            this.splitContainerTop.Panel1.ResumeLayout(false);
            this.splitContainerTop.Panel2.ResumeLayout(false);
            this.splitContainerTop.ResumeLayout(false);
            this.tabControlTopLeft.ResumeLayout(false);
            this.tabPageStaticImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStaticImage)).EndInit();
            this.tabPageCurrentHTML.ResumeLayout(false);
            this.tabPageLocalNodes.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripButton tsbExit;
        private BitmapViewer bitmapViewerStatic;
        private TreeViewer treeViewer;
        private System.Windows.Forms.TabControl tabControlStaticDetails;
        private System.Windows.Forms.TabPage tabPageStaticMaps;
        private System.Windows.Forms.TabControl tabControlNode;
        private System.Windows.Forms.TabPage tabPageNodeDetails;
        private System.Windows.Forms.TabPage tabPageNodePuzzle;
        private BitmapViewer bitmapViewerNodeMaps;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabControl tabControlTopLeft;
        private System.Windows.Forms.TabPage tabPageStaticImage;
        private System.Windows.Forms.PictureBox pictureBoxStaticImage;
        private System.Windows.Forms.TabPage tabPageCurrentHTML;
        private System.Windows.Forms.PictureBox pictureBoxNodePuzzle;
        private System.Windows.Forms.ToolStripComboBox tsDropDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbRefreshVis;
        private System.Windows.Forms.TabPage tabPageStats;
        private System.Windows.Forms.WebBrowser webBrowserGlobalStats;
        private System.Windows.Forms.WebBrowser webBrowserNodeCurrent;
        private System.Windows.Forms.TabPage tabPageEvalList;
        private VisualisationContainer visualisationContainerEvalList;
        private System.Windows.Forms.TabPage tabPageLocalNodes;
        private VisualisationContainer visualisationContainerLocalNodes;
    }
}
