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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sasquatch",
            "Dire Gem",
            "Pending"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Not implemented");
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
            this.splitContainerTop = new System.Windows.Forms.SplitContainer();
            this.tabControlTreeVis = new System.Windows.Forms.TabControl();
            this.tabPageForward = new System.Windows.Forms.TabPage();
            this.treeViewer = new SokoSolve.UI.Section.Solver.TreeViewer();
            this.tabPageReverse = new System.Windows.Forms.TabPage();
            this.visualisationContainerReverseTree = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.tabPageLogger = new System.Windows.Forms.TabPage();
            this.richTextBoxSolverReport = new System.Windows.Forms.RichTextBox();
            this.tabPageSolverBrowser = new System.Windows.Forms.TabPage();
            this.inlineBrowserSolver = new SokoSolve.UI.Controls.Primary.InlineBrowser();
            this.tabControlTopLeft = new System.Windows.Forms.TabControl();
            this.tabPageLocalNodes = new System.Windows.Forms.TabPage();
            this.visualisationContainerLocalNodes = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.tabPageStaticImage = new System.Windows.Forms.TabPage();
            this.pictureBoxStaticImage = new System.Windows.Forms.PictureBox();
            this.splitContainerBottom = new System.Windows.Forms.SplitContainer();
            this.tabControlStaticDetails = new System.Windows.Forms.TabControl();
            this.tabPageStaticMaps = new System.Windows.Forms.TabPage();
            this.bitmapViewerStatic = new SokoSolve.UI.Section.Solver.BitmapViewer();
            this.tabPageStats = new System.Windows.Forms.TabPage();
            this.webBrowserGlobalStats = new System.Windows.Forms.WebBrowser();
            this.tabPageEvalList = new System.Windows.Forms.TabPage();
            this.visualisationContainerEvalList = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.tabPageExitConditions = new System.Windows.Forms.TabPage();
            this.exitConditions = new SokoSolve.UI.Section.Solver.ExitConditions();
            this.tabControlNode = new System.Windows.Forms.TabControl();
            this.tabPageNodeDetails = new System.Windows.Forms.TabPage();
            this.splitContainerCurrentNode = new System.Windows.Forms.SplitContainer();
            this.bitmapViewerNodeMaps = new SokoSolve.UI.Section.Solver.BitmapViewer();
            this.webBrowserNodeCurrent = new System.Windows.Forms.WebBrowser();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabPageBatchMode = new System.Windows.Forms.TabPage();
            this.listViewBatch = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.toolStripMain.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.splitContainerTop.Panel1.SuspendLayout();
            this.splitContainerTop.Panel2.SuspendLayout();
            this.splitContainerTop.SuspendLayout();
            this.tabControlTreeVis.SuspendLayout();
            this.tabPageForward.SuspendLayout();
            this.tabPageReverse.SuspendLayout();
            this.tabPageLogger.SuspendLayout();
            this.tabPageSolverBrowser.SuspendLayout();
            this.tabControlTopLeft.SuspendLayout();
            this.tabPageLocalNodes.SuspendLayout();
            this.tabPageStaticImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStaticImage)).BeginInit();
            this.splitContainerBottom.Panel1.SuspendLayout();
            this.splitContainerBottom.Panel2.SuspendLayout();
            this.splitContainerBottom.SuspendLayout();
            this.tabControlStaticDetails.SuspendLayout();
            this.tabPageStaticMaps.SuspendLayout();
            this.tabPageStats.SuspendLayout();
            this.tabPageEvalList.SuspendLayout();
            this.tabPageExitConditions.SuspendLayout();
            this.tabControlNode.SuspendLayout();
            this.tabPageNodeDetails.SuspendLayout();
            this.splitContainerCurrentNode.Panel1.SuspendLayout();
            this.splitContainerCurrentNode.Panel2.SuspendLayout();
            this.splitContainerCurrentNode.SuspendLayout();
            this.tabPageBatchMode.SuspendLayout();
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
            // splitContainerTop
            // 
            this.splitContainerTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTop.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTop.Name = "splitContainerTop";
            // 
            // splitContainerTop.Panel1
            // 
            this.splitContainerTop.Panel1.AutoScroll = true;
            this.splitContainerTop.Panel1.Controls.Add(this.tabControlTreeVis);
            // 
            // splitContainerTop.Panel2
            // 
            this.splitContainerTop.Panel2.Controls.Add(this.tabControlTopLeft);
            this.splitContainerTop.Size = new System.Drawing.Size(575, 238);
            this.splitContainerTop.SplitterDistance = 400;
            this.splitContainerTop.TabIndex = 0;
            // 
            // tabControlTreeVis
            // 
            this.tabControlTreeVis.Controls.Add(this.tabPageForward);
            this.tabControlTreeVis.Controls.Add(this.tabPageReverse);
            this.tabControlTreeVis.Controls.Add(this.tabPageLogger);
            this.tabControlTreeVis.Controls.Add(this.tabPageSolverBrowser);
            this.tabControlTreeVis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTreeVis.Location = new System.Drawing.Point(0, 0);
            this.tabControlTreeVis.Name = "tabControlTreeVis";
            this.tabControlTreeVis.SelectedIndex = 0;
            this.tabControlTreeVis.Size = new System.Drawing.Size(400, 238);
            this.tabControlTreeVis.TabIndex = 1;
            // 
            // tabPageForward
            // 
            this.tabPageForward.Controls.Add(this.treeViewer);
            this.tabPageForward.Location = new System.Drawing.Point(4, 22);
            this.tabPageForward.Name = "tabPageForward";
            this.tabPageForward.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageForward.Size = new System.Drawing.Size(392, 212);
            this.tabPageForward.TabIndex = 0;
            this.tabPageForward.Text = "Forward Tree";
            this.tabPageForward.UseVisualStyleBackColor = true;
            // 
            // treeViewer
            // 
            this.treeViewer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.treeViewer.Cursor = System.Windows.Forms.Cursors.Cross;
            this.treeViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewer.Location = new System.Drawing.Point(3, 3);
            this.treeViewer.Name = "treeViewer";
            this.treeViewer.OnVisualisationClick = null;
            this.treeViewer.Size = new System.Drawing.Size(386, 206);
            this.treeViewer.TabIndex = 0;
            // 
            // tabPageReverse
            // 
            this.tabPageReverse.Controls.Add(this.visualisationContainerReverseTree);
            this.tabPageReverse.Location = new System.Drawing.Point(4, 22);
            this.tabPageReverse.Name = "tabPageReverse";
            this.tabPageReverse.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReverse.Size = new System.Drawing.Size(392, 212);
            this.tabPageReverse.TabIndex = 1;
            this.tabPageReverse.Text = "Reverse Tree";
            this.tabPageReverse.UseVisualStyleBackColor = true;
            // 
            // visualisationContainerReverseTree
            // 
            this.visualisationContainerReverseTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualisationContainerReverseTree.Location = new System.Drawing.Point(3, 3);
            this.visualisationContainerReverseTree.Name = "visualisationContainerReverseTree";
            this.visualisationContainerReverseTree.RenderOnClick = false;
            this.visualisationContainerReverseTree.Size = new System.Drawing.Size(386, 206);
            this.visualisationContainerReverseTree.Status = "Status";
            this.visualisationContainerReverseTree.TabIndex = 0;
            this.visualisationContainerReverseTree.Visualisation = null;
            // 
            // tabPageLogger
            // 
            this.tabPageLogger.Controls.Add(this.richTextBoxSolverReport);
            this.tabPageLogger.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogger.Name = "tabPageLogger";
            this.tabPageLogger.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogger.Size = new System.Drawing.Size(392, 212);
            this.tabPageLogger.TabIndex = 2;
            this.tabPageLogger.Text = "Solver Report";
            this.tabPageLogger.UseVisualStyleBackColor = true;
            // 
            // richTextBoxSolverReport
            // 
            this.richTextBoxSolverReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxSolverReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxSolverReport.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxSolverReport.Name = "richTextBoxSolverReport";
            this.richTextBoxSolverReport.ReadOnly = true;
            this.richTextBoxSolverReport.Size = new System.Drawing.Size(386, 206);
            this.richTextBoxSolverReport.TabIndex = 0;
            this.richTextBoxSolverReport.Text = "";
            this.richTextBoxSolverReport.WordWrap = false;
            // 
            // tabPageSolverBrowser
            // 
            this.tabPageSolverBrowser.Controls.Add(this.inlineBrowserSolver);
            this.tabPageSolverBrowser.Location = new System.Drawing.Point(4, 22);
            this.tabPageSolverBrowser.Name = "tabPageSolverBrowser";
            this.tabPageSolverBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSolverBrowser.Size = new System.Drawing.Size(392, 212);
            this.tabPageSolverBrowser.TabIndex = 3;
            this.tabPageSolverBrowser.Text = "Solver Help";
            this.tabPageSolverBrowser.UseVisualStyleBackColor = true;
            // 
            // inlineBrowserSolver
            // 
            this.inlineBrowserSolver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inlineBrowserSolver.Location = new System.Drawing.Point(3, 3);
            this.inlineBrowserSolver.Name = "inlineBrowserSolver";
            this.inlineBrowserSolver.Size = new System.Drawing.Size(386, 206);
            this.inlineBrowserSolver.TabIndex = 0;
            // 
            // tabControlTopLeft
            // 
            this.tabControlTopLeft.Controls.Add(this.tabPageLocalNodes);
            this.tabControlTopLeft.Controls.Add(this.tabPageStaticImage);
            this.tabControlTopLeft.Controls.Add(this.tabPageBatchMode);
            this.tabControlTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTopLeft.Location = new System.Drawing.Point(0, 0);
            this.tabControlTopLeft.Name = "tabControlTopLeft";
            this.tabControlTopLeft.SelectedIndex = 0;
            this.tabControlTopLeft.Size = new System.Drawing.Size(171, 238);
            this.tabControlTopLeft.TabIndex = 0;
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
            this.tabControlStaticDetails.Controls.Add(this.tabPageExitConditions);
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
            // tabPageExitConditions
            // 
            this.tabPageExitConditions.Controls.Add(this.exitConditions);
            this.tabPageExitConditions.Location = new System.Drawing.Point(4, 22);
            this.tabPageExitConditions.Name = "tabPageExitConditions";
            this.tabPageExitConditions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExitConditions.Size = new System.Drawing.Size(292, 160);
            this.tabPageExitConditions.TabIndex = 3;
            this.tabPageExitConditions.Text = "Exit Conditions";
            this.tabPageExitConditions.UseVisualStyleBackColor = true;
            // 
            // exitConditions
            // 
            this.exitConditions.AutoScroll = true;
            this.exitConditions.AutoSize = true;
            this.exitConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exitConditions.Location = new System.Drawing.Point(3, 3);
            this.exitConditions.Name = "exitConditions";
            this.exitConditions.Size = new System.Drawing.Size(286, 154);
            this.exitConditions.TabIndex = 0;
            // 
            // tabControlNode
            // 
            this.tabControlNode.Controls.Add(this.tabPageNodeDetails);
            this.tabControlNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlNode.Location = new System.Drawing.Point(0, 0);
            this.tabControlNode.Name = "tabControlNode";
            this.tabControlNode.SelectedIndex = 0;
            this.tabControlNode.Size = new System.Drawing.Size(271, 186);
            this.tabControlNode.TabIndex = 0;
            // 
            // tabPageNodeDetails
            // 
            this.tabPageNodeDetails.Controls.Add(this.splitContainerCurrentNode);
            this.tabPageNodeDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageNodeDetails.Name = "tabPageNodeDetails";
            this.tabPageNodeDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNodeDetails.Size = new System.Drawing.Size(263, 160);
            this.tabPageNodeDetails.TabIndex = 0;
            this.tabPageNodeDetails.Text = "Current Node Maps";
            this.tabPageNodeDetails.UseVisualStyleBackColor = true;
            // 
            // splitContainerCurrentNode
            // 
            this.splitContainerCurrentNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCurrentNode.Location = new System.Drawing.Point(3, 3);
            this.splitContainerCurrentNode.Name = "splitContainerCurrentNode";
            // 
            // splitContainerCurrentNode.Panel1
            // 
            this.splitContainerCurrentNode.Panel1.Controls.Add(this.bitmapViewerNodeMaps);
            // 
            // splitContainerCurrentNode.Panel2
            // 
            this.splitContainerCurrentNode.Panel2.Controls.Add(this.webBrowserNodeCurrent);
            this.splitContainerCurrentNode.Size = new System.Drawing.Size(257, 154);
            this.splitContainerCurrentNode.SplitterDistance = 125;
            this.splitContainerCurrentNode.TabIndex = 1;
            // 
            // bitmapViewerNodeMaps
            // 
            this.bitmapViewerNodeMaps.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bitmapViewerNodeMaps.Cursor = System.Windows.Forms.Cursors.Cross;
            this.bitmapViewerNodeMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitmapViewerNodeMaps.Location = new System.Drawing.Point(0, 0);
            this.bitmapViewerNodeMaps.Name = "bitmapViewerNodeMaps";
            this.bitmapViewerNodeMaps.Size = new System.Drawing.Size(125, 154);
            this.bitmapViewerNodeMaps.TabIndex = 0;
            // 
            // webBrowserNodeCurrent
            // 
            this.webBrowserNodeCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserNodeCurrent.Location = new System.Drawing.Point(0, 0);
            this.webBrowserNodeCurrent.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserNodeCurrent.Name = "webBrowserNodeCurrent";
            this.webBrowserNodeCurrent.Size = new System.Drawing.Size(128, 154);
            this.webBrowserNodeCurrent.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // tabPageBatchMode
            // 
            this.tabPageBatchMode.Controls.Add(this.listViewBatch);
            this.tabPageBatchMode.Location = new System.Drawing.Point(4, 22);
            this.tabPageBatchMode.Name = "tabPageBatchMode";
            this.tabPageBatchMode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBatchMode.Size = new System.Drawing.Size(163, 212);
            this.tabPageBatchMode.TabIndex = 3;
            this.tabPageBatchMode.Text = "Batch Mode";
            this.tabPageBatchMode.UseVisualStyleBackColor = true;
            // 
            // listViewBatch
            // 
            this.listViewBatch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewBatch.FullRowSelect = true;
            this.listViewBatch.GridLines = true;
            this.listViewBatch.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewBatch.Location = new System.Drawing.Point(3, 3);
            this.listViewBatch.Name = "listViewBatch";
            this.listViewBatch.Size = new System.Drawing.Size(157, 206);
            this.listViewBatch.TabIndex = 0;
            this.listViewBatch.UseCompatibleStateImageBehavior = false;
            this.listViewBatch.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Library";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Puzzle";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Status";
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
            this.tabControlTreeVis.ResumeLayout(false);
            this.tabPageForward.ResumeLayout(false);
            this.tabPageReverse.ResumeLayout(false);
            this.tabPageLogger.ResumeLayout(false);
            this.tabPageSolverBrowser.ResumeLayout(false);
            this.tabControlTopLeft.ResumeLayout(false);
            this.tabPageLocalNodes.ResumeLayout(false);
            this.tabPageStaticImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStaticImage)).EndInit();
            this.splitContainerBottom.Panel1.ResumeLayout(false);
            this.splitContainerBottom.Panel2.ResumeLayout(false);
            this.splitContainerBottom.ResumeLayout(false);
            this.tabControlStaticDetails.ResumeLayout(false);
            this.tabPageStaticMaps.ResumeLayout(false);
            this.tabPageStats.ResumeLayout(false);
            this.tabPageEvalList.ResumeLayout(false);
            this.tabPageExitConditions.ResumeLayout(false);
            this.tabPageExitConditions.PerformLayout();
            this.tabControlNode.ResumeLayout(false);
            this.tabPageNodeDetails.ResumeLayout(false);
            this.splitContainerCurrentNode.Panel1.ResumeLayout(false);
            this.splitContainerCurrentNode.Panel2.ResumeLayout(false);
            this.splitContainerCurrentNode.ResumeLayout(false);
            this.tabPageBatchMode.ResumeLayout(false);
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
        private BitmapViewer bitmapViewerNodeMaps;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabControl tabControlTopLeft;
        private System.Windows.Forms.TabPage tabPageStaticImage;
        private System.Windows.Forms.PictureBox pictureBoxStaticImage;
        private System.Windows.Forms.ToolStripComboBox tsDropDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbRefreshVis;
        private System.Windows.Forms.TabPage tabPageStats;
        private System.Windows.Forms.WebBrowser webBrowserGlobalStats;
        private System.Windows.Forms.TabPage tabPageEvalList;
        private VisualisationContainer visualisationContainerEvalList;
        private System.Windows.Forms.TabPage tabPageLocalNodes;
        private VisualisationContainer visualisationContainerLocalNodes;
        private System.Windows.Forms.TabPage tabPageExitConditions;
        private ExitConditions exitConditions;
        private System.Windows.Forms.SplitContainer splitContainerCurrentNode;
        private System.Windows.Forms.WebBrowser webBrowserNodeCurrent;
        private System.Windows.Forms.TabControl tabControlTreeVis;
        private System.Windows.Forms.TabPage tabPageForward;
        private System.Windows.Forms.TabPage tabPageReverse;
        private VisualisationContainer visualisationContainerReverseTree;
        private System.Windows.Forms.TabPage tabPageLogger;
        private System.Windows.Forms.RichTextBox richTextBoxSolverReport;
        private System.Windows.Forms.TabPage tabPageSolverBrowser;
        private SokoSolve.UI.Controls.Primary.InlineBrowser inlineBrowserSolver;
        private System.Windows.Forms.TabPage tabPageBatchMode;
        private System.Windows.Forms.ListView listViewBatch;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}
