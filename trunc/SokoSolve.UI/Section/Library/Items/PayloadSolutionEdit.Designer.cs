namespace SokoSolve.UI.Section.Library.Items
{
    partial class PayloadSolutionEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbSolutions = new System.Windows.Forms.TextBox();
            this.ucGenericDescription = new SokoSolve.UI.Controls.Secondary.ucGenericDescription();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Solution string (UDLR):";
            // 
            // tbSolutions
            // 
            this.tbSolutions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSolutions.Location = new System.Drawing.Point(3, 24);
            this.tbSolutions.Multiline = true;
            this.tbSolutions.Name = "tbSolutions";
            this.tbSolutions.Size = new System.Drawing.Size(426, 127);
            this.tbSolutions.TabIndex = 1;
            // 
            // ucGenericDescription
            // 
            this.ucGenericDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGenericDescription.Data = null;
            this.ucGenericDescription.Location = new System.Drawing.Point(3, 157);
            this.ucGenericDescription.Name = "ucGenericDescription";
            this.ucGenericDescription.ShowButtons = true;
            this.ucGenericDescription.Size = new System.Drawing.Size(444, 358);
            this.ucGenericDescription.TabIndex = 2;
            // 
            // PayloadSolutionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucGenericDescription);
            this.Controls.Add(this.tbSolutions);
            this.Controls.Add(this.label1);
            this.Name = "PayloadSolutionEdit";
            this.Size = new System.Drawing.Size(447, 525);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public SokoSolve.UI.Controls.Secondary.ucGenericDescription ucGenericDescription;
        public System.Windows.Forms.TextBox tbSolutions;
    }
}
