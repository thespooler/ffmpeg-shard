#region LGPL License
//
// VideoDecoderStream.cs
//
// Author:
//   Tim Jones (tim@roastedamoeba.com)
//   Justin Cherniak (justin.cherniak@gmail.com)
//
// Copyright (C) 2008 Tim Jones, Justin Cherniak
//
// This library is free software; you can redistribute it and/or modify
// it  under the terms of the GNU Lesser General Public License version
// 2.1 as published by the Free Software Foundation.
//
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307
// USA
//
#endregion

using System;
using System.IO;
using System.Runtime.InteropServices;
using FFmpegSharp.Interop;
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Format;
using FFmpegSharp.Interop.SWScale;
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp
{
    public unsafe class VideoDecoderStream : DecoderStream, IVideoStream
    {
        #region Fields

        private AVFrame* m_avFrame = null;
        private AVPicture m_avPicture;
        private bool m_avPicture_allocated = false;
        private byte[] m_frameBuffer;

        #endregion

        #region Properties

        public int Width
        {
            get { return m_avCodecCtx.width; }
        }

        public int Height
        {
            get { return m_avCodecCtx.height; }
        }

        /// <summary>
        /// Frame rate of video stream in frames/second
        /// </summary>
        public double FrameRate
        {
            get
            {
                if (m_avStream.r_frame_rate.den > 0 && m_avStream.r_frame_rate.num > 0)
                    return m_avStream.r_frame_rate;
                else
                    return 1 / m_avCodecCtx.time_base;
            }
        }

        public long FrameCount
        {
            get { return (long)(FrameRate * m_file.RawDuration); }
        }

        /// <summary>
        /// Size of one frame in bytes
        /// </summary>
        public int FrameSize
        {
            get { return m_buffer.Length; }
        }

        public override int UncompressedBytesPerSecond
        {
            get { return (int)Math.Ceiling(FrameRate * FrameSize); }
        }

        public PixelFormat PixelFormat
        {
            get { return m_avCodecCtx.pix_fmt; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new VideoDecoderStream over a specific filename.
        /// </summary>
        /// <param name="Filename">File to decode</param>
        internal VideoDecoderStream(MediaFile file, ref AVStream stream)
            : base(file, ref stream)
        {
            // allocate video frame
            m_avFrame = FFmpeg.avcodec_alloc_frame();
            if (FFmpeg.avpicture_alloc(out m_avPicture, m_avCodecCtx.pix_fmt, m_avCodecCtx.width, m_avCodecCtx.height) != 0)
                throw new DecoderException("Error allocating AVPicture");
            m_avPicture_allocated = true;

            int buffersize = FFmpeg.avpicture_get_size(m_avCodecCtx.pix_fmt, m_avCodecCtx.width, m_avCodecCtx.height);
            if (buffersize <= 0)
                throw new DecoderException("Invalid size returned by avpicture_get_size");

            m_buffer = new byte[buffersize];
        }

        #endregion

        #region Methods

        protected override bool DecodePacket(ref AVPacket packet)
        {
            // decode video frame
            bool frameFinished = false;
            int byteCount = FFmpeg.avcodec_decode_video(ref m_avCodecCtx, m_avFrame, out frameFinished, (byte*)packet.data, packet.size);
            if (byteCount < 0)
                throw new DecoderException("Couldn't decode frame");

            // copy data into our managed buffer
            if (m_avFrame->data[0] == IntPtr.Zero)
                m_bufferUsedSize = 0;
            else
                m_bufferUsedSize = FFmpeg.avpicture_layout((AVPicture*)m_avFrame, PixelFormat, Width, Height, m_buffer, m_buffer.Length);
            
            if (m_bufferUsedSize < 0)
                throw new DecoderException("Error copying decoded frame into managed memory");

            return frameFinished;
        }

        public bool ReadFrame(out byte[] frame)
        {
            if (m_frameBuffer == null)
                m_frameBuffer = new byte[FrameSize];

            // read whole frame from the stream
            if (Read(m_frameBuffer, 0, FrameSize) <= 0)
            {
                frame = null;
                return false;
            }
            else
            {
                frame = m_frameBuffer;
                return true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (m_avFrame != null)
            {
                FFmpeg.av_free(m_avFrame);
                m_avFrame = null;
            }

            if (m_avPicture_allocated)
            {
                FFmpeg.avpicture_free(ref m_avPicture);
                m_avPicture_allocated = false;
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
