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
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace FFmpegSharp
{
    public unsafe abstract class DecoderStream : Stream, IMediaStream
    {
        #region Fields

        protected MediaFile m_file;
        protected AVCodecContext m_avCodecCtx;
        protected AVStream m_avStream;
        protected uint m_streamIdx;
        protected bool m_disposed;
        protected byte[] m_buffer = null;
        protected int m_bufferUsedSize = 0;
        protected long m_position;
        private bool m_codecOpen = false;
        private TimeSpan m_timestamp = TimeSpan.Zero;

        private Queue<AVPacket> m_packetQueue = new Queue<AVPacket>();

        private long m_rawPts;
        private long m_rawDts;

        #endregion

        #region Properties

        internal Queue<AVPacket> PacketQueue
        {
            get { return m_packetQueue; }
        }

        public override long Length
        {
            get { return (long)(Math.Ceiling(Duration.TotalSeconds * UncompressedBytesPerSecond)); }
        }

        public TimeSpan Duration
        {
            get { return m_file.Duration; }
        }

        public TimeSpan Timestamp
        {
            get { return m_timestamp; }
        }

        public abstract int UncompressedBytesPerSecond { get; }

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

        public long Pts
        {
            get { return m_rawPts; }
        }

        public long Dts
        {
            get { return m_rawDts; }
        }

        #endregion

        #region Constructors / destructor

        public DecoderStream(MediaFile file, AVStream* stream)
        {
            // Initialize instance variables
            m_disposed = false;
            m_position = m_bufferUsedSize = 0;
            m_file = file;
            m_avStream = *stream;

            m_avCodecCtx = *m_avStream.codec;

            // Open the decoding codec
            AVCodec* avCodec = FFmpeg.avcodec_find_decoder(m_avCodecCtx.codec_id);
            if (avCodec == null)
                throw new DecoderException("No decoder found");

            if (FFmpeg.avcodec_open(ref m_avCodecCtx, avCodec) < 0)
                throw new DecoderException("Error opening codec");

            m_codecOpen = true;
        }

        public DecoderStream(MediaFile file, ref AVStream stream)
        {
            // Initialize instance variables
            m_disposed = false;
            m_position = m_bufferUsedSize = 0;
            m_file = file;
            m_avStream = stream;

            m_avCodecCtx = *m_avStream.codec;

            // Open the decoding codec
            AVCodec* avCodec = FFmpeg.avcodec_find_decoder(m_avCodecCtx.codec_id);
            if (avCodec == null)
                throw new DecoderException("No decoder found");

            if (FFmpeg.avcodec_open(ref m_avCodecCtx, avCodec) < 0)
                throw new DecoderException("Error opening codec");

            m_codecOpen = true;
        }

        ~DecoderStream()
        {
            Dispose(false);
        }

        #endregion

        #region Methods

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalRead = 0;

            while (count > 0)
            {
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

                if (m_bufferUsedSize > 0)
                {
                    int len = Math.Min(count, m_bufferUsedSize);

                    Utils.CopyArray(m_buffer, 0, buffer, offset, len);

                    m_bufferUsedSize -= len;
                    offset += len;
                    count -= len;
                    totalRead += len;

                    if (m_bufferUsedSize > 0)
                        Utils.CopyArray(m_buffer, len, m_buffer, 0, m_bufferUsedSize);
                }

                Debug.Assert(m_bufferUsedSize >= 0);
            }

            m_position += totalRead;
            m_timestamp += TimeSpan.FromSeconds(totalRead / UncompressedBytesPerSecond);

            if (totalRead == 0)
                return -1;

            return totalRead;
        }

        private void ReadNextPacket()
        {
            if (m_position >= Length)
                throw new System.IO.EndOfStreamException();

            if (m_disposed)
                return;

            AVPacket packet;
            bool retry = false;
            do
            {
                while (PacketQueue.Count == 0)
                    m_file.EnqueueNextPacket();

                packet = PacketQueue.Dequeue();

                try
                {
                    retry = !DecodePacket(ref packet);
                }
                finally
                {
                    FFmpeg.av_free_packet(ref packet);
                }
            } while (retry);

            m_rawDts = packet.dts;
            m_rawPts = packet.pts;
        }

        protected abstract bool DecodePacket(ref AVPacket packet);

        public override long Seek(long offset, SeekOrigin origin)
        {
            TimeSpan position = Seek(TimeSpan.FromSeconds(offset / UncompressedBytesPerSecond), origin);

            return (long)(position.TotalSeconds * UncompressedBytesPerSecond);
        }

        public TimeSpan Seek(TimeSpan offset, SeekOrigin origin)
        {
            TimeSpan newPosition = TimeSpan.Zero;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    newPosition = offset;
                    break;
                case SeekOrigin.Current:
                    newPosition = offset + Timestamp;
                    break;
                case SeekOrigin.End:
                    newPosition = Duration - Timestamp;
                    break;
            }

            if (newPosition < TimeSpan.Zero)
                newPosition = TimeSpan.Zero;
            else if (newPosition > Duration)
                newPosition = Duration;

            bool backward = newPosition > Timestamp;

            AVSEEK_FLAG flags = AVSEEK_FLAG.Any | (backward ? AVSEEK_FLAG.Backward: 0);
            long position = (long)(newPosition.TotalSeconds * FFmpeg.AV_TIME_BASE);

            if (FFmpeg.av_seek_frame(ref m_file.FormatContext, -1, position, flags) < 0)
            {
                m_position = Length;
                m_timestamp = Duration;
                return Duration;
            }

            FFmpeg.avcodec_flush_buffers(ref m_avCodecCtx);

            m_timestamp = offset;
            m_position = (long)(m_timestamp.TotalSeconds * UncompressedBytesPerSecond);

            return m_timestamp;
        }

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
                m_disposed = true;

                // BROKEN: Throwing exception on video codec close with MPEG4
//                if (m_codecOpen)
//                    FFmpeg.avcodec_close(ref m_avCodecCtx);
            }
        }

        #endregion
    }
}
