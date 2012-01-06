using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop.Util;
using FFmpegSharp.Interop.SWScale;
using FFmpegSharp.Interop;
using System.IO;
using FFmpegSharp.Interop.Codec;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FFmpegSharp
{
    public unsafe class VideoScalingStream : IVideoStream
    {
        #region Private Instance Members

        IVideoStream m_source;
        SwsContext* m_scalingContext;
        int m_width;
        int m_height;
        PixelFormat m_pixelFormat;
        AVPicture m_outPict;
        bool m_outPictAllocated = false;

        #endregion

        public VideoScalingStream(IVideoStream source, int width, int height, PixelFormat pixelFormat)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            m_source = source;
            m_width = width;
            m_height = height;
            m_pixelFormat = pixelFormat;

            m_scalingContext = FFmpeg.sws_getContext(source.Width, source.Height, source.PixelFormat,
                                                     width, height, pixelFormat, SwsFlags.Bicubic, null, null, null);
            if (m_scalingContext == null)
                throw new DecoderException("Error getting scaling context");

            if (FFmpeg.avpicture_alloc(out m_outPict, this.PixelFormat, this.Width, this.Height) !=0)
                throw new DecoderException("Error allocating AVPicture");

            m_outPictAllocated = true;
        }

        #region IVideoStream Members

        public int Width
        {
            get { return m_width; }
        }

        public int Height
        {
            get { return m_height; }
        }

        public double FrameRate
        {
            get { return m_source.FrameRate; }
        }

        public long FrameCount
        {
            get { return m_source.FrameCount; }
        }

        public int FrameSize
        {
            get { return FFmpeg.avpicture_get_size(PixelFormat, Width, Height); }
        }

        public PixelFormat PixelFormat
        {
            get { return m_pixelFormat; }
        }

        public bool ReadFrame(out byte[] frame)
        {
            frame = null;
            byte[] sourceFrame;

            if (!m_source.ReadFrame(out sourceFrame))
                return false;

            frame = new byte[FrameSize];

            fixed (AVPicture* pOutPict = &m_outPict)
            fixed (byte* pOrig = sourceFrame)
            {
                AVPicture sourcePict;
                int size = FFmpeg.avpicture_fill(out sourcePict, sourceFrame, m_source.PixelFormat, m_source.Width, m_source.Height);
                Debug.Assert(size == sourceFrame.Length);
                
                if (FFmpeg.sws_scale(m_scalingContext, (byte**)&sourcePict.data, sourcePict.linesize, 0, m_source.Height,
                                     (byte**)&pOutPict->data, pOutPict->linesize) < 0)
                    throw new DecoderException("Error scaling output");

                // copy data into our managed buffer
                if (pOutPict->data[0] == IntPtr.Zero)
                {
                    frame = null;
                    return true;
                }

                if (FFmpeg.avpicture_layout((AVPicture*)pOutPict, PixelFormat, Width, Height, frame, frame.Length) < 0)
                    throw new DecoderException("Error copying decoded frame into managed memory");
            }

            return true;
        }

        #endregion

        #region IMediaStream Members

        public TimeSpan Duration
        {
            get { return m_source.Duration; }
        }

        public TimeSpan Timestamp
        {
            get { return m_source.Timestamp; }
        }

        public long Position
        {
            get { return m_source.Position; }
        }

        public long Pts
        {
            get { return m_source.Pts; }
        }

        public long Dts
        {
            get { return m_source.Dts; }
        }

        public long Length
        {
            get { throw new NotImplementedException(); }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public TimeSpan Seek(TimeSpan offset, SeekOrigin origin)
        {
            return m_source.Seek(offset, origin);
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            return m_source.Seek(offset, origin);
        }

        public int UncompressedBytesPerSecond
        {
            get { return (int)Math.Ceiling(FrameRate * FrameSize); }
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (m_scalingContext != null)
            {
                FFmpeg.sws_freeContext(m_scalingContext);
                m_scalingContext = null;
            }

            if (m_outPictAllocated)
                FFmpeg.avpicture_free(ref m_outPict);
        }
    }
}
