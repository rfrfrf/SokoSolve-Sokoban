namespace SokoSolve.UI.Section.Solver
{
    partial class SolverSectionVisualisation
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
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerTop = new System.Windows.Forms.SplitContainer();
            this.tabControlTreeVis = new System.Windows.Forms.TabControl();
            this.tabPageForward = new System.Windows.Forms.TabPage();
            this.tabPageReverse = new System.Windows.Forms.TabPage();
            this.tabPageLogger = new System.Windows.Forms.TabPage();
            this.richTextBoxSolverReport = new System.Windows.Forms.RichTextBox();
            this.tabPageSolverBrowser = new System.Windows.Forms.TabPage();
            this.tabControlTopLeft = new System.Windows.Forms.TabControl();
            this.tabPageLocalNodes = new System.Windows.Forms.TabPage();
            this.tabPageStaticImage = new System.Windows.Forms.TabPage();
            this.pictureBoxStaticImage = new System.Windows.Forms.PictureBox();
            this.splitContainerBottom = new System.Windows.Forms.SplitContainer();
            this.tabControlStaticDetails = new System.Windows.Forms.TabControl();
            this.tabPageStaticMaps = new System.Windows.Forms.TabPage();
            this.tabPageStats = new System.Windows.Forms.TabPage();
            this.tabPageEvalList = new System.Windows.Forms.TabPage();
            this.tabControlNode = new System.Windows.Forms.TabControl();
            this.tabPageNodeDetails = new System.Windows.Forms.TabPage();
            this.splitContainerCurrentNode = new System.Windows.Forms.SplitContainer();
            this.webBrowserNodeCurrent = new System.Windows.Forms.WebBrowser();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tabPageGraphs = new System.Windows.Forms.TabPage();
            this.treeViewer = new SokoSolve.UI.Section.Solver.TreeViewer();
            this.visualisationContainerReverseTree = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.inlineBrowserSolver = new SokoSolve.UI.Controls.Primary.InlineBrowser();
            this.visualisationContainerLocalNodes = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.bitmapViewerStatic = new SokoSolve.UI.Section.Solver.BitmapViewer();
            this.visualisationContainerEvalList = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.bitmapViewerNodeMaps = new SokoSolve.UI.Section.Solver.BitmapViewer();
            this.htmlViewStats = new SokoSolve.UI.Controls.Web.HtmlView();
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
            this.tabControlNode.SuspendLayout();
            this.tabPageNodeDetails.SuspendLayout();
            this.splitContainerCurrentNode.Panel1.SuspendLayout();
            this.splitContainerCurrentNode.Panel2.SuspendLayout();
            this.splitContainerCurrentNode.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainerMain.Size = new System.Drawing.Size(804, 506);
            this.splitContainerMain.SplitterDistance = 280;
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
            this.splitContainerTop.Size = new System.Drawing.Size(804, 280);
            this.splitContainerTop.SplitterDistance = 559;
            this.splitContainerTop.TabIndex = 0;
            // 
            // tabControlTreeVis
            // 
            this.tabControlTreeVis.Controls.Add(this.tabPageForward);
            this.tabControlTreeVis.Controls.Add(this.tabPageReverse);
            this.tabControlTreeVis.Controls.Add(this.tabPageLogger);
            this.tabControlTreeVis.Controls.Add(this.tabPageSolverBrowser);
            this.tabControlTreeVis.Controls.Add(this.tabPageGraphs);
            this.tabControlTreeVis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTreeVis.Location = new System.Drawing.Point(0, 0);
            this.tabControlTreeVis.Name = "tabControlTreeVis";
            this.tabControlTreeVis.SelectedIndex = 0;
            this.tabControlTreeVis.Size = new System.Drawing.Size(559, 280);
            this.tabControlTreeVis.TabIndex = 1;
            // 
            // tabPageForward
            // 
            this.tabPageForward.Controls.Add(this.treeViewer);
            this.tabPageForward.Location = new System.Drawing.Point(4, 22);
            this.tabPageForward.Name = "tabPageForward";
            this.tabPageForward.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageForward.Size = new System.Drawing.Size(551, 254);
            this.tabPageForward.TabIndex = 0;
            this.tabPageForward.Text = "Forward Tree";
            this.tabPageForward.UseVisualStyleBackColor = true;
            // 
            // tabPageReverse
            // 
            this.tabPageReverse.Controls.Add(this.visualisationContainerReverseTree);
            this.tabPageReverse.Location = new System.Drawing.Point(4, 22);
            this.tabPageReverse.Name = "tabPageReverse";
            this.tabPageReverse.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReverse.Size = new System.Drawing.Size(551, 254);
            this.tabPageReverse.TabIndex = 1;
            this.tabPageReverse.Text = "Reverse Tree";
            this.tabPageReverse.UseVisualStyleBackColor = true;
            // 
            // tabPageLogger
            // 
            this.tabPageLogger.Controls.Add(this.richTextBoxSolverReport);
            this.tabPageLogger.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogger.Name = "tabPageLogger";
            this.tabPageLogger.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogger.Size = new System.Drawing.Size(551, 254);
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
            this.richTextBoxSolverReport.Size = new System.Drawing.Size(545, 248);
            this.richTextBoxSolverReport.TabIndex = 0;
            this.richTextBoxSolverReport.Text = "";
            // 
            // tabPageSolverBrowser
            // 
            this.tabPageSolverBrowser.Controls.Add(this.inlineBrowserSolver);
            this.tabPageSolverBrowser.Location = new System.Drawing.Point(4, 22);
            this.tabPageSolverBrowser.Name = "tabPageSolverBrowser";
            this.tabPageSolverBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSolverBrowser.Size = new System.Drawing.Size(551, 254);
            this.tabPageSolverBrowser.TabIndex = 3;
            this.tabPageSolverBrowser.Text = "Solver Help";
            this.tabPageSolverBrowser.UseVisualStyleBackColor = true;
            // 
            // tabControlTopLeft
            // 
            this.tabControlTopLeft.Controls.Add(this.tabPageLocalNodes);
            this.tabControlTopLeft.Controls.Add(this.tabPageStaticImage);
            this.tabControlTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTopLeft.Location = new System.Drawing.Point(0, 0);
            this.tabControlTopLeft.Name = "tabControlTopLeft";
            this.tabControlTopLeft.SelectedIndex = 0;
            this.tabControlTopLeft.Size = new System.Drawing.Size(241, 280);
            this.tabControlTopLeft.TabIndex = 0;
            // 
            // tabPageLocalNodes
            // 
            this.tabPageLocalNodes.Controls.Add(this.visualisationContainerLocalNodes);
            this.tabPageLocalNodes.Location = new System.Drawing.Point(4, 22);
            this.tabPageLocalNodes.Name = "tabPageLocalNodes";
            this.tabPageLocalNodes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLocalNodes.Size = new System.Drawing.Size(233, 254);
            this.tabPageLocalNodes.TabIndex = 2;
            this.tabPageLocalNodes.Text = "Local Nodes";
            this.tabPageLocalNodes.UseVisualStyleBackColor = true;
            // 
            // tabPageStaticImage
            // 
            this.tabPageStaticImage.Controls.Add(this.pictureBoxStaticImage);
            this.tabPageStaticImage.Location = new System.Drawing.Point(4, 22);
            this.tabPageStaticImage.Name = "tabPageStaticImage";
            this.tabPageStaticImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStaticImage.Size = new System.Drawing.Size(233, 254);
            this.tabPageStaticImage.TabIndex = 0;
            this.tabPageStaticImage.Text = "Puzzle Initial";
            this.tabPageStaticImage.UseVisualStyleBackColor = true;
            // 
            // pictureBoxStaticImage
            // 
            this.pictureBoxStaticImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxStaticImage.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxStaticImage.Name = "pictureBoxStaticImage";
            this.pictureBoxStaticImage.Size = new System.Drawing.Size(227, 248);
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
            this.splitContainerBottom.Size = new System.Drawing.Size(804, 222);
            this.splitContainerBottom.SplitterDistance = 419;
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
            this.tabControlStaticDetails.Size = new System.Drawing.Size(419, 222);
            this.tabControlStaticDetails.TabIndex = 1;
            // 
            // tabPageStaticMaps
            // 
            this.tabPageStaticMaps.Controls.Add(this.bitmapViewerStatic);
            this.tabPageStaticMaps.Location = new System.Drawing.Point(4, 22);
            this.tabPageStaticMaps.Name = "tabPageStaticMaps";
            this.tabPageStaticMaps.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStaticMaps.Size = new System.Drawing.Size(411, 196);
            this.tabPageStaticMaps.TabIndex = 0;
            this.tabPageStaticMaps.Text = "Static Maps";
            this.tabPageStaticMaps.UseVisualStyleBackColor = true;
            // 
            // tabPageStats
            // 
            this.tabPageStats.Controls.Add(this.htmlViewStats);
            this.tabPageStats.Location = new System.Drawing.Point(4, 22);
            this.tabPageStats.Name = "tabPageStats";
            this.tabPageStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStats.Size = new System.Drawing.Size(411, 196);
            this.tabPageStats.TabIndex = 1;
            this.tabPageStats.Text = "Statistics";
            this.tabPageStats.UseVisualStyleBackColor = true;
            this.tabPageStats.Click += new System.EventHandler(this.tabPageStats_Click);
            // 
            // tabPageEvalList
            // 
            this.tabPageEvalList.Controls.Add(this.visualisationContainerEvalList);
            this.tabPageEvalList.Location = new System.Drawing.Point(4, 22);
            this.tabPageEvalList.Name = "tabPageEvalList";
            this.tabPageEvalList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEvalList.Size = new System.Drawing.Size(411, 196);
            this.tabPageEvalList.TabIndex = 2;
            this.tabPageEvalList.Text = "Eval List";
            this.tabPageEvalList.UseVisualStyleBackColor = true;
            // 
            // tabControlNode
            // 
            this.tabControlNode.Controls.Add(this.tabPageNodeDetails);
            this.tabControlNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlNode.Location = new System.Drawing.Point(0, 0);
            this.tabControlNode.Name = "tabControlNode";
            this.tabControlNode.SelectedIndex = 0;
            this.tabControlNode.Size = new System.Drawing.Size(381, 222);
            this.tabControlNode.TabIndex = 0;
            // 
            // tabPageNodeDetails
            // 
            this.tabPageNodeDetails.Controls.Add(this.splitContainerCurrentNode);
            this.tabPageNodeDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageNodeDetails.Name = "tabPageNodeDetails";
            this.tabPageNodeDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNodeDetails.Size = new System.Drawing.Size(373, 196);
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
            this.splitContainerCurrentNode.Size = new System.Drawing.Size(367, 190);
            this.splitContainerCurrentNode.SplitterDistance = 178;
            this.splitContainerCurrentNode.TabIndex = 1;
            // 
            // webBrowserNodeCurrent
            // 
            this.webBrowserNodeCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserNodeCurrent.Location = new System.Drawing.Point(0, 0);
            this.webBrowserNodeCurrent.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserNodeCurrent.Name = "webBrowserNodeCurrent";
            this.webBrowserNodeCurrent.Size = new System.Drawing.Size(185, 190);
            this.webBrowserNodeCurrent.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(804, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::SokoSolve.UI.Properties.Resources.Graph;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(65, 22);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tabPageGraphs
            // 
            this.tabPageGraphs.Location = new System.Drawing.Point(4, 22);
            this.tabPageGraphs.Name = "tabPageGraphs";
            this.tabPageGraphs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGraphs.Size = new System.Drawing.Size(551, 254);
            this.tabPageGraphs.TabIndex = 4;
            this.tabPageGraphs.Text = "Graphs";
            this.tabPageGraphs.UseVisualStyleBackColor = true;
            // 
            // treeViewer
            // 
            this.treeViewer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.treeViewer.Cursor = System.Windows.Forms.Cursors.Cross;
            this.treeViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewer.Location = new System.Drawing.Point(3, 3);
            this.treeViewer.Name = "treeViewer";
            this.treeViewer.OnVisualisationClick = null;
            this.treeViewer.Size = new System.Drawing.Size(545, 248);
            this.treeViewer.TabIndex = 0;
            // 
            // visualisationContainerReverseTree
            // 
            this.visualisationContainerReverseTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualisationContainerReverseTree.Location = new System.Drawing.Point(3, 3);
            this.visualisationContainerReverseTree.Name = "visualisationContainerReverseTree";
            this.visualisationContainerReverseTree.RenderOnClick = false;
            this.visualisationContainerReverseTree.Size = new System.Drawing.Size(545, 248);
            this.visualisationContainerReverseTree.Status = "Status";
            this.visualisationContainerReverseTree.TabIndex = 0;
            this.visualisationContainerReverseTree.Visualisation = null;
            // 
            // inlineBrowserSolver
            // 
            this.inlineBrowserSolver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inlineBrowserSolver.Location = new System.Drawing.Point(3, 3);
            this.inlineBrowserSolver.Name = "inlineBrowserSolver";
            this.inlineBrowserSolver.Size = new System.Drawing.Size(545, 248);
            this.inlineBrowserSolver.TabIndex = 0;
            // 
            // visualisationContainerLocalNodes
            // 
            this.visualisationContainerLocalNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualisationContainerLocalNodes.Location = new System.Drawing.Point(3, 3);
            this.visualisationContainerLocalNodes.Name = "visualisationContainerLocalNodes";
            this.visualisationContainerLocalNodes.RenderOnClick = true;
            this.visualisationContainerLocalNodes.Size = new System.Drawing.Size(227, 248);
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
            this.bitmapViewerStatic.Name = "bitmapViewerStatic";
            this.bitmapViewerStatic.Size = new System.Drawing.Size(405, 190);
            this.bitmapViewerStatic.TabIndex = 0;
            // 
            // visualisationContainerEvalList
            // 
            this.visualisationContainerEvalList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualisationContainerEvalList.Location = new System.Drawing.Point(3, 3);
            this.visualisationContainerEvalList.Name = "visualisationContainerEvalList";
            this.visualisationContainerEvalList.RenderOnClick = true;
            this.visualisationContainerEvalList.Size = new System.Drawing.Size(405, 190);
            this.visualisationContainerEvalList.Status = "Not Implemented";
            this.visualisationContainerEvalList.TabIndex = 0;
            this.visualisationContainerEvalList.Visualisation = null;
            // 
            // bitmapViewerNodeMaps
            // 
            this.bitmapViewerNodeMaps.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bitmapViewerNodeMaps.Cursor = System.Windows.Forms.Cursors.Cross;
            this.bitmapViewerNodeMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitmapViewerNodeMaps.Location = new System.Drawing.Point(0, 0);
            this.bitmapViewerNodeMaps.Name = "bitmapViewerNodeMaps";
            this.bitmapViewerNodeMaps.Size = new System.Drawing.Size(178, 190);
            this.bitmapViewerNodeMaps.TabIndex = 0;
            // 
            // htmlViewStats
            // 
            this.htmlViewStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlViewStats.Location = new System.Drawing.Point(3, 3);
            this.htmlViewStats.Name = "htmlViewStats";
            this.htmlViewStats.ShowCommandBack = false;
            this.htmlViewStats.ShowCommandDone = false;
            this.htmlViewStats.ShowCommandForward = false;
            this.htmlViewStats.ShowCommandHome = false;
            this.htmlViewStats.ShowCommandPrint = false;
            this.htmlViewStats.ShowCommands = false;
            this.htmlViewStats.ShowCommandSave = false;
            this.htmlViewStats.ShowStatus = true;
            this.htmlViewStats.Size = new System.Drawing.Size(405, 190);
            this.htmlViewStats.TabIndex = 0;
            this.htmlViewStats.OnCommand += new System.EventHandler<SokoSolve.UI.Controls.Web.UIBrowserEvent>(this.htmlViewStats_OnCommand);
            // 
            // SolverSectionVisualisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SolverSectionVisualisation";
            this.Size = new System.Drawing.Size(804, 531);
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
            this.tabControlNode.ResumeLayout(false);
            this.tabPageNodeDetails.ResumeLayout(false);
            this.splitContainerCurrentNode.Panel1.ResumeLayout(false);
            this.splitContainerCurrentNode.Panel2.ResumeLayout(false);
            this.splitContainerCurrentNode.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerTop;
        private System.Windows.Forms.SplitContainer splitContainerBottom;
        private System.Windows.Forms.TabControl tabControlNode;
        private System.Windows.Forms.TabPage tabPageNodeDetails;
        private BitmapViewer bitmapViewerNodeMaps;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabControl tabControlTopLeft;
        private System.Windows.Forms.TabPage tabPageStaticImage;
        private System.Windows.Forms.PictureBox pictureBoxStaticImage;
        private System.Windows.Forms.TabPage tabPageLocalNodes;
        private VisualisationContainer visualisationContainerLocalNodes;
        private System.Windows.Forms.SplitContainer splitContainerCurrentNode;
        private System.Windows.Forms.WebBrowser webBrowserNodeCurrent;
        private System.Windows.Forms.TabControl tabControlStaticDetails;
        private System.Windows.Forms.TabPage tabPageStaticMaps;
        private BitmapViewer bitmapViewerStatic;
        private System.Windows.Forms.TabPage tabPageStats;
        private System.Windows.Forms.TabPage tabPageEvalList;
        private VisualisationContainer visualisationContainerEvalList;
        private System.Windows.Forms.TabControl tabControlTreeVis;
        private System.Windows.Forms.TabPage tabPageForward;
        private TreeViewer treeViewer;
        private System.Windows.Forms.TabPage tabPageReverse;
        private VisualisationContainer visualisationContainerReverseTree;
        private System.Windows.Forms.TabPage tabPageLogger;
        private System.Windows.Forms.RichTextBox richTextBoxSolverReport;
        private System.Windows.Forms.TabPage tabPageSolverBrowser;
        private SokoSolve.UI.Controls.Primary.InlineBrowser inlineBrowserSolver;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.TabPage tabPageGraphs;
        private SokoSolve.UI.Controls.Web.HtmlView htmlViewStats;
    }
}
