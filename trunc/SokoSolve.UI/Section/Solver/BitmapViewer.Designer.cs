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
            this.labelMouse = new System.Windows.Forms.Label();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // labelMouse
            // 
            this.labelMouse.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelMouse.Location = new System.Drawing.Point(0, 134);
            this.labelMouse.Name = "labelMouse";
            this.labelMouse.Size = new System.Drawing.Size(308, 16);
            this.labelMouse.TabIndex = 0;
            this.labelMouse.Text = "Mouse over for details";
            // 
            // checkedListBox
            // 
            this.checkedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox.CausesValidation = false;
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Items.AddRange(new object[] {
            "Test",
            "Two"});
            this.checkedListBox.Location = new System.Drawing.Point(223, 3);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(82, 139);
            this.checkedListBox.TabIndex = 1;
            this.checkedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_ItemCheck);
            // 
            // BitmapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.labelMouse);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Name = "BitmapViewer";
            this.Size = new System.Drawing.Size(308, 150);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BitmapViewer_MouseMove);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BitmapViewer_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelMouse;
        private System.Windows.Forms.CheckedListBox checkedListBox;
    }
}
