using System;
using System.Windows.Forms;
using FFmpegSharp.Video;

namespace FFmpegSharp.Examples.VideoPlayer
{
    public partial class VideoPlayer : Form
    {
        VideoDecoderStream m_stream;

        public VideoPlayer()
        {
            InitializeComponent();
        }

        private void m_btnPlay_Click(object sender, EventArgs e)
        {
            m_videoSurface.Stream = new VideoDecoderStream(m_txtPath.Text);
        }

        private void m_btnFile_Click(object sender, EventArgs e)
        {
            DialogResult d = m_dlgFile.ShowDialog();

            if (d == DialogResult.OK)
                m_txtPath.Text = m_dlgFile.FileName;
        }
    }
}
