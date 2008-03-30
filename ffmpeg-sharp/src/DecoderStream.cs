#region LGPL License
//
// DecoderStream.cs
//
// Author:
//   Justin Cherniak (justin.cherniak@gmail.com
//
// Copyright (C) 2008 Justin Cherniak
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
using System.Diagnostics;
using System.IO;
using FFmpegSharp.Interop;
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Format;

namespace FFmpegSharp
{
    public unsafe abstract class DecoderStream : Stream
    {
        #region Fields

        protected AVFormatContext m_avFormatCtx;
        protected AVCodecContext m_avCodecCtx;
        protected AVStream m_avStream;
        protected uint m_audioStreamIdx;
        protected bool m_disposed;
        protected byte[] m_buffer;
        protected int m_bufferUsedSize;
        protected long m_position;

        #endregion

        #region Properties

        /// <summary>
        /// Duration of the stream
        /// </summary>
        public TimeSpan Duration
        {
            get { return new TimeSpan((long) (RawDuration * 1e7)); }
        }

        public float RawDuration
        {
            get
            {
                float duration = 0;
                duration = (float) (m_avFormatCtx.duration / (double) FFmpeg.AV_TIME_BASE);
                if (duration < 0)
                    duration = 0;
                return duration;
            }
        }

        public int Width
        {
            get { return m_avCodecCtx.width; }
        }

        public int Height
        {
            get { return m_avCodecCtx.height; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Position
        {
            get { return m_position; }
            set { this.Seek(value, SeekOrigin.Begin); }
        }

        #endregion

        #region Constructors / destructor

        /// <summary>
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="File">File to decode</param>
        public DecoderStream(FileInfo File, CodecType codecType) : this(File.FullName, codecType) { }

        /// <summary>
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="File">File to decode</param>
        public DecoderStream(FileStream File, CodecType codecType) : this(File.Name, codecType) { }

        /// <summary>
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="Filename">File to decode</param>
        public DecoderStream(string Filename, CodecType codecType)
        {
            // Initialize instance variables
            m_disposed = false;
            m_position = m_bufferUsedSize = 0;

            // Open FFmpeg
            FFmpeg.av_register_all();

            // Open the file with FFmpeg
            if (FFmpeg.av_open_input_file(out m_avFormatCtx, Filename) != 0)
                throw new DecoderException("Couldn't open file");

            if (FFmpeg.av_find_stream_info(ref m_avFormatCtx) < 0)
                throw new DecoderException("Couldn't find stream info");

            if (m_avFormatCtx.nb_streams < 1)
                throw new DecoderException("No streams found");

            // Find the first audio stream in the file (eventually might support selecting streams)
            m_avCodecCtx = *m_avFormatCtx.streams[0]->codec;

            for (m_audioStreamIdx = 0; m_audioStreamIdx < m_avFormatCtx.nb_streams; m_audioStreamIdx++)
            {
                m_avStream = *m_avFormatCtx.streams[m_audioStreamIdx];
                m_avCodecCtx = *m_avFormatCtx.streams[m_audioStreamIdx]->codec;

                if (m_avCodecCtx.codec_type == codecType)
                    break;
            }

            if (m_avCodecCtx.codec_type != codecType)
                throw new DecoderException("No stream of specified type found");

            // Open the decoding codec
            AVCodec* avCodec = FFmpeg.avcodec_find_decoder(m_avCodecCtx.codec_id);
            if (avCodec == null)
                throw new DecoderException("No decoder found");

            if (FFmpeg.avcodec_open(ref m_avCodecCtx, ref *avCodec) < 0)
                throw new DecoderException("Error opening codec");
        }

        ~DecoderStream()
        {
            Dispose(false);
        }

        #endregion

        #region Methods

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalWritten = 0;

            while (count > 0)
            {
                if (m_bufferUsedSize > 0)
                {
                    int len = Math.Min(count, m_bufferUsedSize);

                    Buffer.BlockCopy(m_buffer, 0, buffer, offset, len);

                    m_bufferUsedSize -= len;
                    offset += len;
                    count -= len;
                    totalWritten += len;

                    if (m_bufferUsedSize > 0)
                        Buffer.BlockCopy(m_buffer, len, m_buffer, 0, m_bufferUsedSize);
                }

                Debug.Assert(m_bufferUsedSize >= 0);

                if (m_bufferUsedSize == 0)
                {
                    try
                    {
                        ReadNextPacket();
                    }
                    catch (EndOfStreamException)
                    {
                        break;
                    }
                }
            }

            m_position += totalWritten;

            if (totalWritten == 0)
                return -1;

            return totalWritten;
        }

        private void ReadNextPacket()
        {
            if (m_position >= Length)
                throw new System.IO.EndOfStreamException();

            if (m_disposed)
                return;

            AVPacket packet = new AVPacket();
            FFmpeg.av_init_packet(ref packet);

            bool retry = false;

            do
            {
                // Read a frame of compressed audio data
                int r;
                if ((r = FFmpeg.av_read_frame(ref m_avFormatCtx, ref packet)) < 0)
                    throw new System.IO.EndOfStreamException();

                try
                {
                    // Make sure the packet is an audio packet and has data
                    if (packet.stream_index != m_audioStreamIdx ||
                            packet.data == IntPtr.Zero)
                    {
                        retry = true;
                        continue;
                    }

                    retry = DecodeNextPacket(ref packet);
                }
                finally
                {
                    FFmpeg.av_free_packet(ref packet);
                }

            } while (retry);
        }

        protected abstract bool DecodeNextPacket(ref AVPacket packet);

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                    m_buffer = null;

                m_disposed = true;

                if (m_avCodecCtx.codec != null)
                    FFmpeg.avcodec_close(ref m_avCodecCtx);

                FFmpeg.av_close_input_file(ref m_avFormatCtx);
            }
        }

        #endregion
    }
}
