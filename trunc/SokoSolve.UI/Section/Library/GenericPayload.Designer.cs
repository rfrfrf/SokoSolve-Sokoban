namespace SokoSolve.UI.Section.Library
{
    partial class GenericPayload
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listViewChildren = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.uiBrowser = new SokoSolve.UI.Controls.Web.HtmlView();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.uiBrowser);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listViewChildren);
            this.splitContainer.Size = new System.Drawing.Size(473, 430);
            this.splitContainer.SplitterDistance = 200;
            this.splitContainer.TabIndex = 0;
            // 
            // listViewChildren
            // 
            this.listViewChildren.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewChildren.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewChildren.Location = new System.Drawing.Point(0, 0);
            this.listViewChildren.Name = "listViewChildren";
            this.listViewChildren.Size = new System.Drawing.Size(473, 226);
            this.listViewChildren.TabIndex = 0;
            this.listViewChildren.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // uiBrowser
            // 
            this.uiBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiBrowser.Location = new System.Drawing.Point(0, 0);
            this.uiBrowser.Name = "uiBrowser";
            this.uiBrowser.ShowCommands = false;
            this.uiBrowser.ShowStatus = false;
            this.uiBrowser.Size = new System.Drawing.Size(473, 200);
            this.uiBrowser.TabIndex = 0;
            // 
            // GenericPayload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "GenericPayload";
            this.Size = new System.Drawing.Size(473, 430);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        public System.Windows.Forms.ListView listViewChildren;
        public SokoSolve.UI.Controls.Web.HtmlView  uiBrowser;
    }
}
