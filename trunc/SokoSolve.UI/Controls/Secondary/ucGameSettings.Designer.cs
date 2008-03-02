namespace SokoSolve.UI.Controls.Secondary
{
    partial class ucGameSettings
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
            this.MusicVolume = new System.Windows.Forms.TrackBar();
            this.SoundVolume = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MusicLocation = new System.Windows.Forms.TextBox();
            this.buttonMusicLoc = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.FullScreen = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MusicVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoundVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // MusicVolume
            // 
            this.MusicVolume.LargeChange = 10;
            this.MusicVolume.Location = new System.Drawing.Point(15, 69);
            this.MusicVolume.Maximum = 100;
            this.MusicVolume.Name = "MusicVolume";
            this.MusicVolume.Size = new System.Drawing.Size(472, 45);
            this.MusicVolume.SmallChange = 2;
            this.MusicVolume.TabIndex = 0;
            this.MusicVolume.TickFrequency = 20;
            // 
            // SoundVolume
            // 
            this.SoundVolume.LargeChange = 10;
            this.SoundVolume.Location = new System.Drawing.Point(15, 141);
            this.SoundVolume.Maximum = 100;
            this.SoundVolume.Name = "SoundVolume";
            this.SoundVolume.Size = new System.Drawing.Size(472, 45);
            this.SoundVolume.SmallChange = 2;
            this.SoundVolume.TabIndex = 1;
            this.SoundVolume.TickFrequency = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Music Volume:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Off";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Max";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Sound Volume:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Off";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(460, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Max";
            // 
            // MusicLocation
            // 
            this.MusicLocation.Location = new System.Drawing.Point(12, 25);
            this.MusicLocation.Name = "MusicLocation";
            this.MusicLocation.Size = new System.Drawing.Size(475, 20);
            this.MusicLocation.TabIndex = 8;
            // 
            // buttonMusicLoc
            // 
            this.buttonMusicLoc.Location = new System.Drawing.Point(490, 24);
            this.buttonMusicLoc.Name = "buttonMusicLoc";
            this.buttonMusicLoc.Size = new System.Drawing.Size(35, 23);
            this.buttonMusicLoc.TabIndex = 9;
            this.buttonMusicLoc.Text = "...";
            this.buttonMusicLoc.UseVisualStyleBackColor = true;
            this.buttonMusicLoc.Click += new System.EventHandler(this.buttonMusicLoc_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Music (MP3) Location:";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Music (MP3) Playback folder location";
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.ApplicationData;
            // 
            // FullScreen
            // 
            this.FullScreen.AutoSize = true;
            this.FullScreen.Location = new System.Drawing.Point(15, 211);
            this.FullScreen.Name = "FullScreen";
            this.FullScreen.Size = new System.Drawing.Size(108, 17);
            this.FullScreen.TabIndex = 11;
            this.FullScreen.Text = "Full Screen mode";
            this.FullScreen.UseVisualStyleBackColor = true;
            // 
            // ucGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FullScreen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonMusicLoc);
            this.Controls.Add(this.MusicLocation);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SoundVolume);
            this.Controls.Add(this.MusicVolume);
            this.Name = "ucGameSettings";
            this.Size = new System.Drawing.Size(541, 355);
            ((System.ComponentModel.ISupportInitialize)(this.MusicVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoundVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TrackBar MusicVolume;
        public System.Windows.Forms.TrackBar SoundVolume;
        public System.Windows.Forms.TextBox MusicLocation;
        private System.Windows.Forms.Button buttonMusicLoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public System.Windows.Forms.CheckBox FullScreen;
    }
}
