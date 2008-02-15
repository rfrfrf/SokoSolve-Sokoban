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
            this.lvLayers = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // visualisationContainer
            // 
            this.visualisationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualisationContainer.Location = new System.Drawing.Point(0, 0);
            this.visualisationContainer.Name = "visualisationContainer";
            this.visualisationContainer.RenderOnClick = true;
            this.visualisationContainer.Size = new System.Drawing.Size(356, 222);
            this.visualisationContainer.Status = "Status";
            this.visualisationContainer.TabIndex = 0;
            this.visualisationContainer.Visualisation = null;
            // 
            // lvLayers
            // 
            this.lvLayers.CheckBoxes = true;
            this.lvLayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLayers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLayers.Location = new System.Drawing.Point(0, 0);
            this.lvLayers.MultiSelect = false;
            this.lvLayers.Name = "lvLayers";
            this.lvLayers.Size = new System.Drawing.Size(169, 222);
            this.lvLayers.TabIndex = 2;
            this.lvLayers.UseCompatibleStateImageBehavior = false;
            this.lvLayers.View = System.Windows.Forms.View.Details;
            this.lvLayers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvLayers_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Layers";
            this.columnHeader1.Width = 108;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.visualisationContainer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvLayers);
            this.splitContainer1.Size = new System.Drawing.Size(529, 222);
            this.splitContainer1.SplitterDistance = 356;
            this.splitContainer1.TabIndex = 3;
            // 
            // BitmapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.splitContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Name = "BitmapViewer";
            this.Size = new System.Drawing.Size(529, 222);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualisationContainer visualisationContainer;
        private System.Windows.Forms.ListView lvLayers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}
