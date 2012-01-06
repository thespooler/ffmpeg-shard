using System;
using System.Windows.Forms;
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Examples.VideoPlayer
{
    public partial class VideoPlayer : Form
    {
        public VideoPlayer()
        {
            InitializeComponent();
        }

        private void m_btnPlay_Click(object sender, EventArgs e)
        {
            MediaFile file = new MediaFile(m_txtPath.Text);

            foreach (DecoderStream stream in file.Streams)
            {
                VideoDecoderStream videoStream = stream as VideoDecoderStream;
                if (videoStream != null)
                    m_videoSurface.Stream = new VideoScalingStream(videoStream, m_videoSurface.ClientRectangle.Width,
                                                                   m_videoSurface.ClientRectangle.Height, PixelFormat.PIX_FMT_RGB32);
            }
        }

        private void m_btnFile_Click(object sender, EventArgs e)
        {
            DialogResult d = m_dlgFile.ShowDialog();

            if (d == DialogResult.OK)
                m_txtPath.Text = m_dlgFile.FileName;
        }
    }
}
