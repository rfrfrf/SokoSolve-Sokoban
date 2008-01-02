namespace SokoSolve.UI.Section.Solver
{
    partial class BitmapViewer
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
            this.visualisationContainer = new SokoSolve.UI.Section.Solver.VisualisationContainer();
            this.SuspendLayout();
            // 
            // visualisationContainer
            // 
            this.visualisationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualisationContainer.Location = new System.Drawing.Point(0, 0);
            this.visualisationContainer.Name = "visualisationContainer";
            this.visualisationContainer.Size = new System.Drawing.Size(308, 150);
            this.visualisationContainer.Status = "Status";
            this.visualisationContainer.TabIndex = 0;
            this.visualisationContainer.Visualisation = null;
            // 
            // BitmapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.visualisationContainer);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Name = "BitmapViewer";
            this.Size = new System.Drawing.Size(308, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualisationContainer visualisationContainer;

    }
}
