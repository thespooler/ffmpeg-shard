namespace FFmpegSharp.Examples.VideoPlayer
{
    partial class VideoPlayer
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
            this.m_txtPath = new System.Windows.Forms.TextBox();
            this.m_btnPlay = new System.Windows.Forms.Button();
            this.m_btnFile = new System.Windows.Forms.Button();
            this.m_dlgFile = new System.Windows.Forms.OpenFileDialog();
            this.m_videoSurface = new FFmpegSharp.Examples.VideoPlayerControl();
            this.SuspendLayout();
            // 
            // m_txtPath
            // 
            this.m_txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtPath.Location = new System.Drawing.Point(12, 12);
            this.m_txtPath.Name = "m_txtPath";
            this.m_txtPath.Size = new System.Drawing.Size(269, 20);
            this.m_txtPath.TabIndex = 0;
            // 
            // m_btnPlay
            // 
            this.m_btnPlay.Location = new System.Drawing.Point(12, 38);
            this.m_btnPlay.Name = "m_btnPlay";
            this.m_btnPlay.Size = new System.Drawing.Size(75, 23);
            this.m_btnPlay.TabIndex = 1;
            this.m_btnPlay.Text = "Play";
            this.m_btnPlay.UseVisualStyleBackColor = true;
            this.m_btnPlay.Click += new System.EventHandler(this.m_btnPlay_Click);
            // 
            // m_btnFile
            // 
            this.m_btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnFile.Location = new System.Drawing.Point(287, 12);
            this.m_btnFile.Name = "m_btnFile";
            this.m_btnFile.Size = new System.Drawing.Size(41, 23);
            this.m_btnFile.TabIndex = 3;
            this.m_btnFile.Text = "...";
            this.m_btnFile.UseVisualStyleBackColor = true;
            this.m_btnFile.Click += new System.EventHandler(this.m_btnFile_Click);
            // 
            // m_videoSurface
            // 
            this.m_videoSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_videoSurface.Location = new System.Drawing.Point(12, 67);
            this.m_videoSurface.Name = "m_videoSurface";
            this.m_videoSurface.Size = new System.Drawing.Size(309, 229);
            this.m_videoSurface.Stream = null;
            this.m_videoSurface.TabIndex = 4;
            this.m_videoSurface.Text = "videoPlayerControl1";
            // 
            // VideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 308);
            this.Controls.Add(this.m_videoSurface);
            this.Controls.Add(this.m_btnFile);
            this.Controls.Add(this.m_btnPlay);
            this.Controls.Add(this.m_txtPath);
            this.Name = "VideoPlayer";
            this.Text = "VideoPlayer Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtPath;
        private System.Windows.Forms.Button m_btnPlay;
        private System.Windows.Forms.Button m_btnFile;
        private System.Windows.Forms.OpenFileDialog m_dlgFile;
        private VideoPlayerControl m_videoSurface;
    }
}

