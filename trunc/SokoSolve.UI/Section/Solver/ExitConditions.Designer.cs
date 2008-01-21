namespace SokoSolve.UI.Section.Solver
{
    partial class ExitConditions
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
            this.cbStopOnSolution = new System.Windows.Forms.CheckBox();
            this.upMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.upMaxItter = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.upMaxTime = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.upMaxNodes = new System.Windows.Forms.NumericUpDown();
            this.cbProfiles = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.upMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upMaxItter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upMaxTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upMaxNodes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbStopOnSolution
            // 
            this.cbStopOnSolution.AutoSize = true;
            this.cbStopOnSolution.Checked = true;
            this.cbStopOnSolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStopOnSolution.Location = new System.Drawing.Point(6, 19);
            this.cbStopOnSolution.Name = "cbStopOnSolution";
            this.cbStopOnSolution.Size = new System.Drawing.Size(106, 17);
            this.cbStopOnSolution.TabIndex = 0;
            this.cbStopOnSolution.Text = "Stop On Solution";
            this.cbStopOnSolution.UseVisualStyleBackColor = true;
            // 
            // upMaxDepth
            // 
            this.upMaxDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.upMaxDepth.Location = new System.Drawing.Point(7, 55);
            this.upMaxDepth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.upMaxDepth.Name = "upMaxDepth";
            this.upMaxDepth.Size = new System.Drawing.Size(220, 20);
            this.upMaxDepth.TabIndex = 1;
            this.upMaxDepth.ThousandsSeparator = true;
            this.upMaxDepth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max Depth:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Max Itterations:";
            // 
            // upMaxItter
            // 
            this.upMaxItter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.upMaxItter.Increment = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.upMaxItter.Location = new System.Drawing.Point(6, 103);
            this.upMaxItter.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.upMaxItter.Name = "upMaxItter";
            this.upMaxItter.Size = new System.Drawing.Size(220, 20);
            this.upMaxItter.TabIndex = 3;
            this.upMaxItter.ThousandsSeparator = true;
            this.upMaxItter.Value = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Max Time (mins):";
            // 
            // upMaxTime
            // 
            this.upMaxTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.upMaxTime.DecimalPlaces = 1;
            this.upMaxTime.Location = new System.Drawing.Point(6, 148);
            this.upMaxTime.Name = "upMaxTime";
            this.upMaxTime.Size = new System.Drawing.Size(220, 20);
            this.upMaxTime.TabIndex = 5;
            this.upMaxTime.ThousandsSeparator = true;
            this.upMaxTime.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(266, 34);
            this.label4.TabIndex = 7;
            this.label4.Text = "Solver Constraints: When should the solver give up? (This cannot be changed once " +
                "the sovler is started)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Max Nodes (Puzzle Positions):";
            // 
            // upMaxNodes
            // 
            this.upMaxNodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.upMaxNodes.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upMaxNodes.Location = new System.Drawing.Point(7, 195);
            this.upMaxNodes.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.upMaxNodes.Name = "upMaxNodes";
            this.upMaxNodes.Size = new System.Drawing.Size(220, 20);
            this.upMaxNodes.TabIndex = 8;
            this.upMaxNodes.ThousandsSeparator = true;
            this.upMaxNodes.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            // 
            // cbProfiles
            // 
            this.cbProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProfiles.FormattingEnabled = true;
            this.cbProfiles.Items.AddRange(new object[] {
            "Default",
            "5min balanced",
            "10min time only burn"});
            this.cbProfiles.Location = new System.Drawing.Point(4, 65);
            this.cbProfiles.Name = "cbProfiles";
            this.cbProfiles.Size = new System.Drawing.Size(249, 21);
            this.cbProfiles.TabIndex = 10;
            this.cbProfiles.SelectedIndexChanged += new System.EventHandler(this.cbProfiles_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Profile Presets:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbStopOnSolution);
            this.groupBox1.Controls.Add(this.upMaxDepth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.upMaxItter);
            this.groupBox1.Controls.Add(this.upMaxNodes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.upMaxTime);
            this.groupBox1.Location = new System.Drawing.Point(7, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 235);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exit Conditions";
            // 
            // ExitConditions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbProfiles);
            this.Controls.Add(this.label4);
            this.Name = "ExitConditions";
            this.Size = new System.Drawing.Size(266, 355);
            ((System.ComponentModel.ISupportInitialize)(this.upMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upMaxItter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upMaxTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upMaxNodes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.CheckBox cbStopOnSolution;
        internal System.Windows.Forms.NumericUpDown upMaxDepth;
        internal System.Windows.Forms.NumericUpDown upMaxItter;
        internal System.Windows.Forms.NumericUpDown upMaxTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.NumericUpDown upMaxNodes;
        private System.Windows.Forms.ComboBox cbProfiles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
