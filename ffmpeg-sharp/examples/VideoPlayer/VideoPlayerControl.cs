using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FFmpegSharp.Video;

namespace FFmpegSharp.Examples
{
    public partial class VideoPlayerControl : Control
    {
        VideoDecoderStream m_stream;
        Timer m_timer;

        public VideoDecoderStream Stream
        {
            get { return m_stream; }
            set
            {
                m_stream = value;
                if (value != null)
                {
                    m_timer = new Timer();
                    m_timer.Interval = (int)((1 / m_stream.FrameRate) * 1000);
                    m_timer.Tick += new EventHandler(timer_Tick);
                    m_timer.Start();
                }
                else
                {
                    m_timer = null;
                }
            }
        }

        public VideoPlayerControl()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (m_stream != null)
                Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (m_stream != null)
            {
                byte[] frame;
                if (m_stream.ReadFrame(out frame))
                {
                    Bitmap image = new Bitmap(m_stream.Width, m_stream.Height);

                    BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                        ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                    Marshal.Copy(frame, 0, data.Scan0, frame.Length);

                    image.UnlockBits(data);

                    pe.Graphics.DrawImage(image, ClientRectangle);
                }
            }
        }
    }
}
