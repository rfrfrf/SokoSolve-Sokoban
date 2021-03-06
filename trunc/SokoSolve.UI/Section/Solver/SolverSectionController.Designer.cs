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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolverSectionController));
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Solved", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Failed", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Errors", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Pending", System.Windows.Forms.HorizontalAlignment.Left);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbDone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbStart = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
            this.tsbUnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSaveXML = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveHTML = new System.Windows.Forms.ToolStripButton();
            this.tsbVisualisation = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsgProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSolverQueue = new System.Windows.Forms.TabPage();
            this.ucPuzzleList1 = new SokoSolve.UI.Controls.Secondary.ucPuzzleList();
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.chPuzzle = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chSummary = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRetainHistory = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbAlwaysAdd = new System.Windows.Forms.RadioButton();
            this.rbAddBetter = new System.Windows.Forms.RadioButton();
            this.rbDontAdd = new System.Windows.Forms.RadioButton();
            this.cbThreadPriority = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exitConditions1 = new SokoSolve.UI.Section.Solver.ExitConditions();
            this.timerUpdater = new System.Windows.Forms.Timer(this.components);
            this.solverSettings1 = new SokoSolve.UI.Section.Solver.SolverSettings();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSolverQueue.SuspendLayout();
            this.tabPageResults.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.tsbSelectAll,
            this.tsbUnSelectAll,
            this.toolStripSeparator4,
            this.tsbSaveXML,
            this.tsbSaveHTML,
            this.tsbVisualisation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(578, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbDone
            // 
            this.tsbDone.Image = global::SokoSolve.UI.Properties.Resources.Exit;
            this.tsbDone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDone.Name = "tsbDone";
            this.tsbDone.Size = new System.Drawing.Size(52, 22);
            this.tsbDone.Text = "Done";
            this.tsbDone.ToolTipText = "Return to the library";
            this.tsbDone.Click += new System.EventHandler(this.tsbDone_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbStart
            // 
            this.tsbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStart.Image = global::SokoSolve.UI.Properties.Resources.Play;
            this.tsbStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStart.Name = "tsbStart";
            this.tsbStart.Size = new System.Drawing.Size(23, 22);
            this.tsbStart.Text = "Start Batch";
            this.tsbStart.ToolTipText = "Start all items in the match";
            this.tsbStart.Click += new System.EventHandler(this.tsbStart_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Image = global::SokoSolve.UI.Properties.Resources.Stop;
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(23, 22);
            this.tsbStop.Text = "Stop";
            this.tsbStop.ToolTipText = "Cancel all items in the batch, including the current solver";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSelectAll
            // 
            this.tsbSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectAll.Image")));
            this.tsbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectAll.Name = "tsbSelectAll";
            this.tsbSelectAll.Size = new System.Drawing.Size(54, 22);
            this.tsbSelectAll.Text = "Select All";
            this.tsbSelectAll.Click += new System.EventHandler(this.tsbSelectAll_Click);
            // 
            // tsbUnSelectAll
            // 
            this.tsbUnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbUnSelectAll.Image")));
            this.tsbUnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUnSelectAll.Name = "tsbUnSelectAll";
            this.tsbUnSelectAll.Size = new System.Drawing.Size(67, 22);
            this.tsbUnSelectAll.Text = "UnSelect All";
            this.tsbUnSelectAll.Click += new System.EventHandler(this.tsbUnSelectAll_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSaveXML
            // 
            this.tsbSaveXML.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveXML.Enabled = false;
            this.tsbSaveXML.Image = global::SokoSolve.UI.Properties.Resources.Save;
            this.tsbSaveXML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveXML.Name = "tsbSaveXML";
            this.tsbSaveXML.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveXML.Text = "Save XML";
            this.tsbSaveXML.ToolTipText = "Save result report as XML";
            // 
            // tsbSaveHTML
            // 
            this.tsbSaveHTML.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveHTML.Enabled = false;
            this.tsbSaveHTML.Image = global::SokoSolve.UI.Properties.Resources.Report;
            this.tsbSaveHTML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveHTML.Name = "tsbSaveHTML";
            this.tsbSaveHTML.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveHTML.Text = "Save HTML";
            this.tsbSaveHTML.ToolTipText = "Save result report as XHTML";
            this.tsbSaveHTML.Click += new System.EventHandler(this.tsbSaveHTML_Click);
            // 
            // tsbVisualisation
            // 
            this.tsbVisualisation.Enabled = false;
            this.tsbVisualisation.Image = global::SokoSolve.UI.Properties.Resources.Graph;
            this.tsbVisualisation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVisualisation.Name = "tsbVisualisation";
            this.tsbVisualisation.Size = new System.Drawing.Size(119, 22);
            this.tsbVisualisation.Text = "Show Visualisations";
            this.tsbVisualisation.ToolTipText = "Show Visualisations popup form";
            this.tsbVisualisation.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            // ucPuzzleList1
            // 
            this.ucPuzzleList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPuzzleList1.Library = null;
            this.ucPuzzleList1.Location = new System.Drawing.Point(3, 3);
            this.ucPuzzleList1.Name = "ucPuzzleList1";
            this.ucPuzzleList1.ShowGroups = true;
            this.ucPuzzleList1.ShowPreview = true;
            this.ucPuzzleList1.Size = new System.Drawing.Size(564, 367);
            this.ucPuzzleList1.TabIndex = 0;
            this.ucPuzzleList1.UseCheckBoxes = true;
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
            this.listViewResults.Location = new System.Drawing.Point(3, 3);
            this.listViewResults.MultiSelect = false;
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(564, 367);
            this.listViewResults.SmallImageList = this.imageList1;
            this.listViewResults.TabIndex = 0;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            this.listViewResults.DoubleClick += new System.EventHandler(this.listViewResults_DoubleClick);
            this.listViewResults.SelectedIndexChanged += new System.EventHandler(this.listViewResults_SelectedIndexChanged);
            this.listViewResults.Leave += new System.EventHandler(this.listViewResults_Leave);
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
            this.tabPageSettings.Controls.Add(this.solverSettings1);
            this.tabPageSettings.Controls.Add(this.groupBox1);
            this.tabPageSettings.Controls.Add(this.groupBox2);
            this.tabPageSettings.Controls.Add(this.cbThreadPriority);
            this.tabPageSettings.Controls.Add(this.label1);
            this.tabPageSettings.Controls.Add(this.exitConditions1);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(570, 373);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbRetainHistory);
            this.groupBox1.Location = new System.Drawing.Point(269, 204);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 86);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Retain History";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 44);
            this.label2.TabIndex = 1;
            this.label2.Text = "* WARNING * This will keep each solver attempt in memory, and will consume large " +
                "amounts of memory for large work lists.";
            // 
            // cbRetainHistory
            // 
            this.cbRetainHistory.Location = new System.Drawing.Point(9, 15);
            this.cbRetainHistory.Name = "cbRetainHistory";
            this.cbRetainHistory.Size = new System.Drawing.Size(280, 22);
            this.cbRetainHistory.TabIndex = 0;
            this.cbRetainHistory.Text = "Allow visualisations for each item in the work list.";
            this.cbRetainHistory.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbAlwaysAdd);
            this.groupBox2.Controls.Add(this.rbAddBetter);
            this.groupBox2.Controls.Add(this.rbDontAdd);
            this.groupBox2.Location = new System.Drawing.Point(269, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add solutions to library";
            // 
            // rbAlwaysAdd
            // 
            this.rbAlwaysAdd.AutoSize = true;
            this.rbAlwaysAdd.Location = new System.Drawing.Point(7, 68);
            this.rbAlwaysAdd.Name = "rbAlwaysAdd";
            this.rbAlwaysAdd.Size = new System.Drawing.Size(79, 17);
            this.rbAlwaysAdd.TabIndex = 2;
            this.rbAlwaysAdd.Text = "Always add";
            this.rbAlwaysAdd.UseVisualStyleBackColor = true;
            // 
            // rbAddBetter
            // 
            this.rbAddBetter.AutoSize = true;
            this.rbAddBetter.Checked = true;
            this.rbAddBetter.Location = new System.Drawing.Point(7, 44);
            this.rbAddBetter.Name = "rbAddBetter";
            this.rbAddBetter.Size = new System.Drawing.Size(82, 17);
            this.rbAddBetter.TabIndex = 1;
            this.rbAddBetter.TabStop = true;
            this.rbAddBetter.Text = "Add if better";
            this.rbAddBetter.UseVisualStyleBackColor = true;
            // 
            // rbDontAdd
            // 
            this.rbDontAdd.AutoSize = true;
            this.rbDontAdd.Location = new System.Drawing.Point(7, 20);
            this.rbDontAdd.Name = "rbDontAdd";
            this.rbDontAdd.Size = new System.Drawing.Size(71, 17);
            this.rbDontAdd.TabIndex = 0;
            this.rbDontAdd.Text = "Don\'t add";
            this.rbDontAdd.UseVisualStyleBackColor = true;
            // 
            // cbThreadPriority
            // 
            this.cbThreadPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbThreadPriority.FormattingEnabled = true;
            this.cbThreadPriority.Location = new System.Drawing.Point(279, 71);
            this.cbThreadPriority.Name = "cbThreadPriority";
            this.cbThreadPriority.Size = new System.Drawing.Size(194, 21);
            this.cbThreadPriority.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thread Priority:";
            // 
            // exitConditions1
            // 
            this.exitConditions1.Location = new System.Drawing.Point(6, 6);
            this.exitConditions1.Name = "exitConditions1";
            this.exitConditions1.Size = new System.Drawing.Size(267, 346);
            this.exitConditions1.TabIndex = 0;
            // 
            // timerUpdater
            // 
            this.timerUpdater.Interval = 10000;
            this.timerUpdater.Tick += new System.EventHandler(this.timerUpdater_Tick);
            // 
            // solverSettings1
            // 
            this.solverSettings1.Location = new System.Drawing.Point(269, 297);
            this.solverSettings1.Name = "solverSettings1";
            this.solverSettings1.Size = new System.Drawing.Size(295, 149);
            this.solverSettings1.TabIndex = 6;
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
            this.tabPageSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.ComboBox cbThreadPriority;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbSelectAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbAlwaysAdd;
        private System.Windows.Forms.RadioButton rbAddBetter;
        private System.Windows.Forms.RadioButton rbDontAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbRetainHistory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton tsbUnSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private SolverSettings solverSettings1;
    }
}
