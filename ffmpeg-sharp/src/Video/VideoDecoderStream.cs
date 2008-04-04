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

namespace FFmpegSharp.Video
{
    public unsafe class VideoDecoderStream : DecoderStream
    {
        #region Fields

        private PixelFormat m_pixelFormat;
        private AVFrame* m_avFrame;
        private AVPicture m_avPicture;
        private SwsContext* m_swsContext;

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

        public override long Length
        {
            get { return (long)(Math.Ceiling(Duration.TotalSeconds * FrameRate * m_buffer.Length)); }
        }

        /// <summary>
        /// Frame rate of video stream in frames/second
        /// </summary>
        public float FrameRate
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
            get { return (long)(FrameRate * RawDuration); }
        }

        /// <summary>
        /// Size of one frame in bytes
        /// </summary>
        public int FrameSize
        {
            get { return m_buffer.Length; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new VideoDecoderStream over a specific filename.
        /// </summary>
        /// <param name="File">File to decode</param>
        public VideoDecoderStream(FileInfo File) : this(File.FullName) { }

        /// <summary>
        /// Constructs a new VideoDecoderStream over a specific filename.
        /// </summary>
        /// <param name="File">File to decode</param>
        public VideoDecoderStream(FileStream File) : this(File.Name) { }

        /// <summary>
        /// Constructs a new VideoDecoderStream over a specific filename.
        /// </summary>
        /// <param name="Filename">File to decode</param>
        public VideoDecoderStream(string Filename)
            : base(Filename, CodecType.CODEC_TYPE_VIDEO)
        {
            m_pixelFormat = PixelFormat.PIX_FMT_RGB24;

            // allocate video frame
            m_avFrame = FFmpeg.avcodec_alloc_frame();

            // allocate some space for the converted frame
            m_avPicture = new AVPicture();
            if (FFmpeg.avpicture_alloc(ref m_avPicture, (int)m_pixelFormat, m_avCodecCtx.width, m_avCodecCtx.height) != 0)
                throw new DecoderException("Error allocating AVPicture");

            // determine required buffer size and allocate buffer
            int numBytes = FFmpeg.avpicture_get_size(m_pixelFormat, this.Width, this.Height);
            m_buffer = new byte[numBytes];

            // allocate SWS context, which is used for scaling and converting image formats
            m_swsContext = FFmpeg.sws_getContext(m_avCodecCtx.width, m_avCodecCtx.height, (int)m_avCodecCtx.pix_fmt,
                m_avCodecCtx.width, m_avCodecCtx.height, (int)m_pixelFormat, FFmpeg.SWS_BICUBIC);
        }

        #endregion

        #region Methods

        protected override bool DecodeNextPacket(ref AVPacket packet)
        {
            // decode video frame
            bool frameFinished = false;
            int byteCount = FFmpeg.avcodec_decode_video(ref m_avCodecCtx, m_avFrame, out frameFinished, (byte*)packet.data, packet.size);
            if (byteCount < 0)
                throw new DecoderException("Couldn't decode frame");

            // did we get a video frame?
            if (frameFinished)
            {
                // convert the image from its native format to RGB
                fixed (AVPicture* pPict = &m_avPicture)
                {
                    if (FFmpeg.sws_scale(m_swsContext, m_avFrame->data, m_avFrame->linesize, 0, m_avCodecCtx.height, pPict->data, pPict->linesize) != 0)
                        ;// throw new DecoderException("Error during image conversion");
                }

                // copy RGB frame from unmanaged to managed memory
                // We should be able to eliminate this!
                fixed (int* pictureData = m_avPicture.data)
                    Marshal.Copy(new IntPtr(pictureData[0]), m_buffer, 0, m_buffer.Length);

                m_bufferUsedSize = m_buffer.Length;

                return false;
            }
            else
            {
                return true;
            }
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
            base.Dispose(disposing);

            FFmpeg.avpicture_free(ref m_avPicture);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
