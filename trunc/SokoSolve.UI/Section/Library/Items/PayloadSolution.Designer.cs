namespace SokoSolve.UI.Section.Library.Items
{
    partial class PayloadSolution
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
            this.tabPageHTML = new System.Windows.Forms.TabPage();
            this.tabPageSolutionBrowse = new System.Windows.Forms.TabPage();
            this.upSteps = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tbStep = new System.Windows.Forms.TrackBar();
            this.pbSolution = new System.Windows.Forms.PictureBox();
            this.htmlView = new SokoSolve.UI.Controls.Web.HtmlView();
            this.tabControl1.SuspendLayout();
            this.tabPageHTML.SuspendLayout();
            this.tabPageSolutionBrowse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSolution)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageHTML);
            this.tabControl1.Controls.Add(this.tabPageSolutionBrowse);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(461, 424);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageHTML
            // 
            this.tabPageHTML.Controls.Add(this.htmlView);
            this.tabPageHTML.Location = new System.Drawing.Point(4, 22);
            this.tabPageHTML.Name = "tabPageHTML";
            this.tabPageHTML.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHTML.Size = new System.Drawing.Size(453, 398);
            this.tabPageHTML.TabIndex = 0;
            this.tabPageHTML.Text = "Report";
            this.tabPageHTML.UseVisualStyleBackColor = true;
            // 
            // tabPageSolutionBrowse
            // 
            this.tabPageSolutionBrowse.Controls.Add(this.upSteps);
            this.tabPageSolutionBrowse.Controls.Add(this.label1);
            this.tabPageSolutionBrowse.Controls.Add(this.tbStep);
            this.tabPageSolutionBrowse.Controls.Add(this.pbSolution);
            this.tabPageSolutionBrowse.Location = new System.Drawing.Point(4, 22);
            this.tabPageSolutionBrowse.Name = "tabPageSolutionBrowse";
            this.tabPageSolutionBrowse.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSolutionBrowse.Size = new System.Drawing.Size(453, 398);
            this.tabPageSolutionBrowse.TabIndex = 1;
            this.tabPageSolutionBrowse.Text = "Browse Solution";
            this.tabPageSolutionBrowse.UseVisualStyleBackColor = true;
            // 
            // upSteps
            // 
            this.upSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.upSteps.Location = new System.Drawing.Point(363, 372);
            this.upSteps.Maximum = new decimal(new int[] {
            453,
            0,
            0,
            0});
            this.upSteps.Name = "upSteps";
            this.upSteps.Size = new System.Drawing.Size(84, 20);
            this.upSteps.TabIndex = 3;
            this.upSteps.ThousandsSeparator = true;
            this.upSteps.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.upSteps.ValueChanged += new System.EventHandler(this.upSteps_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 372);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Step #30 of 453";
            // 
            // tbStep
            // 
            this.tbStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStep.LargeChange = 20;
            this.tbStep.Location = new System.Drawing.Point(6, 318);
            this.tbStep.Maximum = 300;
            this.tbStep.Name = "tbStep";
            this.tbStep.Size = new System.Drawing.Size(441, 45);
            this.tbStep.TabIndex = 1;
            this.tbStep.TickFrequency = 10;
            this.tbStep.ValueChanged += new System.EventHandler(this.tbStep_ValueChanged);
            // 
            // pbSolution
            // 
            this.pbSolution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSolution.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbSolution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSolution.Location = new System.Drawing.Point(6, 6);
            this.pbSolution.Name = "pbSolution";
            this.pbSolution.Size = new System.Drawing.Size(441, 312);
            this.pbSolution.TabIndex = 0;
            this.pbSolution.TabStop = false;
            // 
            // htmlView
            // 
            this.htmlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlView.Location = new System.Drawing.Point(3, 3);
            this.htmlView.Name = "htmlView";
            this.htmlView.ShowCommandBack = false;
            this.htmlView.ShowCommandDone = false;
            this.htmlView.ShowCommandForward = false;
            this.htmlView.ShowCommandHome = false;
            this.htmlView.ShowCommandPrint = false;
            this.htmlView.ShowCommands = false;
            this.htmlView.ShowCommandSave = false;
            this.htmlView.ShowStatus = false;
            this.htmlView.Size = new System.Drawing.Size(447, 392);
            this.htmlView.TabIndex = 0;
            // 
            // PayloadSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "PayloadSolution";
            this.Size = new System.Drawing.Size(461, 424);
            this.tabControl1.ResumeLayout(false);
            this.tabPageHTML.ResumeLayout(false);
            this.tabPageSolutionBrowse.ResumeLayout(false);
            this.tabPageSolutionBrowse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSolution)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageHTML;
        private System.Windows.Forms.TabPage tabPageSolutionBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbStep;
        private System.Windows.Forms.PictureBox pbSolution;
        private System.Windows.Forms.NumericUpDown upSteps;
        public SokoSolve.UI.Controls.Web.HtmlView htmlView;
    }
}
