namespace SokoSolve.UI.Section.Solver
{
    partial class SolverSectionController
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
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Solved", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Failed", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Errors", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Pending", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "#2 Dire Gem"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Pending", System.Drawing.SystemColors.ScrollBar, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "3w5h, rating 43.50, no solutions")}, "Sucess.bmp");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolverSectionController));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsgProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSolverQueue = new System.Windows.Forms.TabPage();
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.chPuzzle = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chSummary = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.timerUpdater = new System.Windows.Forms.Timer(this.components);
            this.tsbDone = new System.Windows.Forms.ToolStripButton();
            this.tsbStart = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveXML = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveHTML = new System.Windows.Forms.ToolStripButton();
            this.tsbVisualisation = new System.Windows.Forms.ToolStripButton();
            this.ucPuzzleList1 = new SokoSolve.UI.Controls.Secondary.ucPuzzleList();
            this.exitConditions1 = new SokoSolve.UI.Section.Solver.ExitConditions();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSolverQueue.SuspendLayout();
            this.tabPageResults.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDone,
            this.toolStripSeparator1,
            this.tsbStart,
            this.tsbStop,
            this.toolStripSeparator2,
            this.tsbSaveXML,
            this.tsbSaveHTML,
            this.tsbVisualisation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(578, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTime,
            this.tsgProgress,
            this.tslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(578, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslTime
            // 
            this.tslTime.AutoSize = false;
            this.tslTime.Name = "tslTime";
            this.tslTime.Size = new System.Drawing.Size(100, 17);
            this.tslTime.Text = "Time 0m45s";
            this.tslTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsgProgress
            // 
            this.tsgProgress.Name = "tsgProgress";
            this.tsgProgress.Size = new System.Drawing.Size(100, 16);
            this.tsgProgress.Step = 1;
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(38, 17);
            this.tslStatus.Text = "Status";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSolverQueue);
            this.tabControl1.Controls.Add(this.tabPageResults);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(578, 399);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageSolverQueue
            // 
            this.tabPageSolverQueue.Controls.Add(this.ucPuzzleList1);
            this.tabPageSolverQueue.Location = new System.Drawing.Point(4, 22);
            this.tabPageSolverQueue.Name = "tabPageSolverQueue";
            this.tabPageSolverQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSolverQueue.Size = new System.Drawing.Size(570, 373);
            this.tabPageSolverQueue.TabIndex = 0;
            this.tabPageSolverQueue.Text = "Solver Queue";
            this.tabPageSolverQueue.UseVisualStyleBackColor = true;
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.listViewResults);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResults.Size = new System.Drawing.Size(570, 373);
            this.tabPageResults.TabIndex = 1;
            this.tabPageResults.Text = "Results";
            this.tabPageResults.UseVisualStyleBackColor = true;
            // 
            // listViewResults
            // 
            this.listViewResults.AutoArrange = false;
            this.listViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPuzzle,
            this.chStatus,
            this.chSummary});
            this.listViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewResults.FullRowSelect = true;
            listViewGroup5.Header = "Solved";
            listViewGroup5.Name = "listViewGroupSolved";
            listViewGroup6.Header = "Failed";
            listViewGroup6.Name = "listViewGroupFailed";
            listViewGroup7.Header = "Errors";
            listViewGroup7.Name = "listViewGroupError";
            listViewGroup8.Header = "Pending";
            listViewGroup8.Name = "listViewGroupPending";
            this.listViewResults.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8});
            listViewItem2.Group = listViewGroup8;
            this.listViewResults.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listViewResults.Location = new System.Drawing.Point(3, 3);
            this.listViewResults.MultiSelect = false;
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(564, 367);
            this.listViewResults.SmallImageList = this.imageList1;
            this.listViewResults.TabIndex = 0;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            // 
            // chPuzzle
            // 
            this.chPuzzle.Text = "Puzzle";
            this.chPuzzle.Width = 143;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 155;
            // 
            // chSummary
            // 
            this.chSummary.Text = "Summary";
            this.chSummary.Width = 252;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.exitConditions1);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(570, 373);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // timerUpdater
            // 
            this.timerUpdater.Interval = 10000;
            this.timerUpdater.Tick += new System.EventHandler(this.timerUpdater_Tick);
            // 
            // tsbDone
            // 
            this.tsbDone.Image = global::SokoSolve.UI.Properties.Resources.Exit;
            this.tsbDone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDone.Name = "tsbDone";
            this.tsbDone.Size = new System.Drawing.Size(52, 22);
            this.tsbDone.Text = "Done";
            this.tsbDone.Click += new System.EventHandler(this.tsbDone_Click);
            // 
            // tsbStart
            // 
            this.tsbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStart.Image = global::SokoSolve.UI.Properties.Resources.Play;
            this.tsbStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStart.Name = "tsbStart";
            this.tsbStart.Size = new System.Drawing.Size(23, 22);
            this.tsbStart.Text = "toolStripButton2";
            this.tsbStart.Click += new System.EventHandler(this.tsbStart_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Image = global::SokoSolve.UI.Properties.Resources.Stop;
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(23, 22);
            this.tsbStop.Text = "toolStripButton3";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // tsbSaveXML
            // 
            this.tsbSaveXML.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveXML.Image = global::SokoSolve.UI.Properties.Resources.Save;
            this.tsbSaveXML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveXML.Name = "tsbSaveXML";
            this.tsbSaveXML.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveXML.Text = "toolStripButton4";
            // 
            // tsbSaveHTML
            // 
            this.tsbSaveHTML.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveHTML.Image = global::SokoSolve.UI.Properties.Resources.Report;
            this.tsbSaveHTML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveHTML.Name = "tsbSaveHTML";
            this.tsbSaveHTML.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveHTML.Text = "toolStripButton5";
            // 
            // tsbVisualisation
            // 
            this.tsbVisualisation.Image = global::SokoSolve.UI.Properties.Resources.Graph;
            this.tsbVisualisation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVisualisation.Name = "tsbVisualisation";
            this.tsbVisualisation.Size = new System.Drawing.Size(119, 22);
            this.tsbVisualisation.Text = "Show Visualisations";
            this.tsbVisualisation.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // ucPuzzleList1
            // 
            this.ucPuzzleList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPuzzleList1.Library = null;
            this.ucPuzzleList1.Location = new System.Drawing.Point(3, 3);
            this.ucPuzzleList1.Name = "ucPuzzleList1";
            this.ucPuzzleList1.Size = new System.Drawing.Size(564, 367);
            this.ucPuzzleList1.TabIndex = 0;
            this.ucPuzzleList1.UseCheckBoxes = true;
            // 
            // exitConditions1
            // 
            this.exitConditions1.Location = new System.Drawing.Point(6, 6);
            this.exitConditions1.Name = "exitConditions1";
            this.exitConditions1.Size = new System.Drawing.Size(285, 238);
            this.exitConditions1.TabIndex = 0;
            // 
            // SolverSectionController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SolverSectionController";
            this.Size = new System.Drawing.Size(578, 446);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageSolverQueue.ResumeLayout(false);
            this.tabPageResults.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSolverQueue;
        private System.Windows.Forms.TabPage tabPageResults;
        private System.Windows.Forms.TabPage tabPageSettings;
        private ExitConditions exitConditions1;
        private System.Windows.Forms.ToolStripButton tsbDone;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbStart;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbSaveXML;
        private System.Windows.Forms.ToolStripButton tsbSaveHTML;
        private SokoSolve.UI.Controls.Secondary.ucPuzzleList ucPuzzleList1;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.ColumnHeader chPuzzle;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chSummary;
        private System.Windows.Forms.ToolStripStatusLabel tslTime;
        private System.Windows.Forms.ToolStripProgressBar tsgProgress;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timerUpdater;
        private System.Windows.Forms.ToolStripButton tsbVisualisation;
    }
}
