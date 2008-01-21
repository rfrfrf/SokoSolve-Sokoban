namespace SokoSolve.UI.Section.Solver
{
    partial class FormSolverVisualisation
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSolverVisualisation));
            this.solverSectionVisualisation1 = new SokoSolve.UI.Section.Solver.SolverSectionVisualisation();
            this.SuspendLayout();
            // 
            // solverSectionVisualisation1
            // 
            this.solverSectionVisualisation1.Controller = null;
            this.solverSectionVisualisation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solverSectionVisualisation1.Location = new System.Drawing.Point(0, 0);
            this.solverSectionVisualisation1.Name = "solverSectionVisualisation1";
            this.solverSectionVisualisation1.Size = new System.Drawing.Size(729, 493);
            this.solverSectionVisualisation1.TabIndex = 0;
            this.solverSectionVisualisation1.Load += new System.EventHandler(this.solverSectionVisualisation1_Load);
            // 
            // FormSolverVisualisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 493);
            this.Controls.Add(this.solverSectionVisualisation1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSolverVisualisation";
            this.Text = "Solver Visualisation";
            this.ResumeLayout(false);

        }

        #endregion

        private SolverSectionVisualisation solverSectionVisualisation1;
    }
}