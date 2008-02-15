namespace SokoSolve.UI.Section.Solver
{
    partial class SolverSettings
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
            this.cbUseReverseSolver = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbUseReverseSolver
            // 
            this.cbUseReverseSolver.AutoSize = true;
            this.cbUseReverseSolver.Checked = true;
            this.cbUseReverseSolver.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseReverseSolver.Location = new System.Drawing.Point(9, 0);
            this.cbUseReverseSolver.Name = "cbUseReverseSolver";
            this.cbUseReverseSolver.Size = new System.Drawing.Size(132, 17);
            this.cbUseReverseSolver.TabIndex = 0;
            this.cbUseReverseSolver.Text = "Use the reverse solver";
            this.cbUseReverseSolver.UseVisualStyleBackColor = true;
            // 
            // SolverSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbUseReverseSolver);
            this.Name = "SolverSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox cbUseReverseSolver;

    }
}
