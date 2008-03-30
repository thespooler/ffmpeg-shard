#region LGPL License
//
// VideoDecoderStream.cs
//
// Author:
//   Tim Jones (tim@roastedamoeba.com)
//
// Copyright (C) 2008 Tim Jones
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
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Audio
{
    public unsafe class VideoDecoderStream : DecoderStream
    {
        #region Fields

        private PixelFormat m_pixelFormat;
        private IntPtr m_avFramePtr;
        private AVFrame m_avFrame;
        private AVPicture m_avPicture;
        //private SwsContext m_swsContext;

        private byte[] m_frameBuffer;

        #endregion

        #region Properties

        public override long Length
        {
            get { return (long) (Math.Ceiling(Duration.TotalSeconds * FrameRate * m_buffer.Length)); }
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
            get { return (long) (FrameRate * RawDuration); }
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
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="File">File to decode</param>
        public VideoDecoderStream(FileInfo File) : this(File.FullName) { }

        /// <summary>
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="File">File to decode</param>
        public VideoDecoderStream(FileStream File) : this(File.Name) { }

        /// <summary>
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="Filename">File to decode</param>
        public VideoDecoderStream(string Filename)
            : base(Filename, CodecType.CODEC_TYPE_VIDEO)
        {
            m_pixelFormat = PixelFormat.PIX_FMT_RGB24;

            // allocate video frame
            m_avFramePtr = new IntPtr(FFmpeg.avcodec_alloc_frame());

            // allocate some space for the converted frame
            m_avPicture = new AVPicture();
            FFmpeg.avpicture_alloc(ref m_avPicture, (int) m_pixelFormat, m_avCodecCtx.width, m_avCodecCtx.height);

            // determine required buffer size and allocate buffer
            int numBytes = FFmpeg.avpicture_get_size(m_pixelFormat, this.Width, this.Height);
            m_buffer = new byte[numBytes];

            // allocate SWS context, which is used for scaling and converting image formats
            /*m_swsContext = *FFmpeg.sws_getContext(m_avCodecCtx.width, m_avCodecCtx.height, (int) m_avCodecCtx.pix_fmt,
                m_avCodecCtx.width, m_avCodecCtx.height, (int) m_pixelFormat, FFmpeg.SWS_BICUBIC);*/
        }

        #endregion

        #region Methods

        protected override bool DecodeNextPacket(ref AVPacket packet)
        {
            // decode video frame
            bool frameFinished = false;
            int byteCount = FFmpeg.avcodec_decode_video(ref m_avCodecCtx, m_avFramePtr, out frameFinished, packet.data, packet.size);
            if (byteCount < 0)
                throw new DecoderException("Couldn't decode frame");

            // did we get a video frame?
            if (frameFinished)
            {
                // convert the image from its native format to RGB
                m_avFrame = (AVFrame) Marshal.PtrToStructure(m_avFramePtr, typeof(AVFrame));
                FFmpeg.img_convert(ref m_avPicture, m_pixelFormat, ref m_avFrame, m_avCodecCtx.pix_fmt, this.Width, this.Height);

                // can't get sws_scale to work. the converted image is black, with a blue bar down the left hand side
                /*fixed (int* srcData = m_avFrame.data, srcLinesize = m_avFrame.linesize, dstData = m_avPicture.data, dstLinesize = m_avPicture.linesize)
                {
                    FFmpeg.sws_scale(ref m_swsContext, srcData, srcLinesize, 0, m_avCodecCtx.height, dstData, dstLinesize);
                }*/

                // copy RGB frame from unmanaged to managed memory
                fixed (int* pictureData = m_avPicture.data)
                {
                    Marshal.Copy(new IntPtr(pictureData[0]), m_buffer, 0, m_buffer.Length);
                }

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

            try
            {
                FFmpeg.avpicture_free(ref m_avPicture);
            }
            catch { }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}