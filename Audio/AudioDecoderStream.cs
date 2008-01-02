#region LGPL License
//
// AudioDecoderStream.cs
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

namespace FFmpegSharp.Audio
{
    public unsafe class AudioDecoderStream : Stream
    {
        #region Private Instance Members

        private AVFormatContext m_avFormatCtx;
        private AVCodecContext m_avCodecCtx;
        private uint m_audioStreamIdx;
        private bool m_disposed;
        private byte[] m_buffer;
        private int m_bufferUsedSize;
        private long m_position;
        private const int BUFFER_SIZE = FFmpeg.AVCODEC_MAX_AUDIO_FRAME_SIZE;

        #endregion

        /// <summary>
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="File">File to decode</param>
        public AudioDecoderStream(FileInfo File) : this(File.FullName) { }

        /// <summary>
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="File">File to decode</param>
        public AudioDecoderStream(FileStream File) : this(File.Name) { }

        /// <summary>
        /// Constructs a new AudioDecoderStream over a specific filename.
        /// </summary>
        /// <param name="Filename">File to decode</param>
        public AudioDecoderStream(string Filename)
        {
            // Initialize instance variables
            m_disposed = false;
            m_buffer = new byte[FFmpeg.AVCODEC_MAX_AUDIO_FRAME_SIZE];
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
                m_avCodecCtx = *m_avFormatCtx.streams[m_audioStreamIdx]->codec;

                if (m_avCodecCtx.codec_type == CodecType.CODEC_TYPE_AUDIO)
                    break;
            }

            if (m_avCodecCtx.codec_type != CodecType.CODEC_TYPE_AUDIO)
                throw new DecoderException("No audio stream found");

            // Open the audio decoding codec
            AVCodec* avCodec = FFmpeg.avcodec_find_decoder(m_avCodecCtx.codec_id);
            if (avCodec == null)
                throw new DecoderException("No decoder found");

            if (FFmpeg.avcodec_open(ref m_avCodecCtx, ref *avCodec) < 0)
                throw new DecoderException("Error opening codec");
        }

        ~AudioDecoderStream()
        {
            Dispose(false);
        }

        /// <summary>
        /// Duration of the stream 
        /// </summary>
        public TimeSpan Duration
        {
            get { return new TimeSpan((long)((m_avFormatCtx.duration / (double)FFmpeg.AV_TIME_BASE) * 1E7)); }
        }

        /// <summary>
        /// Number of channels in the audio stream.
        /// </summary>
        public int Channels
        {
            get { return m_avCodecCtx.channels; }
        }

        /// <summary>
        /// Sample rate of the stream in bits per second
        /// </summary>
        public int SampleRate
        {
            get { return m_avCodecCtx.sample_rate; }
        }

        /// <summary>
        /// Returns the sample size in bits.
        /// </summary>
        public int SampleSize
        {
            get
            {
                switch (m_avCodecCtx.sample_fmt)
                {
                    case SampleFormat.SAMPLE_FMT_U8:
                        return 8;
                    case SampleFormat.SAMPLE_FMT_S16:
                        return 16;
                    case SampleFormat.SAMPLE_FMT_S24:
                        return 24;
                    case SampleFormat.SAMPLE_FMT_S32:
                        return 32;
                    default:
                        throw new Exception("Unknown sample size.");
                }
            }
        }

        /// <summary>
        /// Average bytes per second of the stream
        /// </summary>
        public int AverageBytesPerSecond
        {
            get { return (Channels * SampleRate * SampleSize) / 8; }
        }

        private void DecodeNextPacket()
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

                    int totalOutput = 0;

                    // Copy the data pointer to we can muck with it
                    int packetSize = packet.size;
                    byte* packetData = (byte*)packet.data;

                    // Allocate a new PcmPacket and get a pointer to its array so we can muck with it
                    fixed (byte* pcmPacketPtr = m_buffer)
                    {
                        // May be necessary to loop multiple times if more than one frame is in the compressed packet
                        do
                        {
                            if (m_disposed)
                            {
                                m_bufferUsedSize = 0;
                                return;
                            }

                            int outputBufferUsedSize = (m_buffer.Length) - totalOutput; //Must be initialized before sending in as per docs

                            short* pcmWritePtr = (short*)((byte*)pcmPacketPtr + totalOutput);

                            int usedInputBytes = FFmpeg.avcodec_decode_audio2(ref m_avCodecCtx, pcmWritePtr, ref outputBufferUsedSize, packetData, packetSize);

                            if (usedInputBytes < 0) //Error in packet, ignore packet
                                break;

                            if (outputBufferUsedSize > 0)
                                totalOutput += outputBufferUsedSize;

                            packetData += usedInputBytes;
                            packetSize -= usedInputBytes;
                        }
                        while (packetSize > 0);

                        retry = false;
                        m_bufferUsedSize = totalOutput;
                    }
                }
                finally
                {
                    FFmpeg.av_free_packet(ref packet);
                }

            } while (retry);
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

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Length
        {
            get { return (long)(Math.Ceiling(Duration.TotalSeconds * AverageBytesPerSecond)); }
        }

        public override long Position
        {
            get { return m_position; }
            set { this.Seek(value, SeekOrigin.Begin); }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalWritten = 0;

            while (count > 0)
            {
                if (m_bufferUsedSize > 0)
                {
                    int len = Math.Min(count, m_bufferUsedSize);

                    Array.Copy(m_buffer, 0, buffer, offset, len);

                    m_bufferUsedSize -= len;
                    offset += len;
                    count -= len;
                    totalWritten += len;

                    if (m_bufferUsedSize > 0)
                        Array.Copy(m_buffer, len, m_buffer, 0, m_bufferUsedSize);
                }

                Debug.Assert(m_bufferUsedSize >= 0);

                if (m_bufferUsedSize == 0)
                {
                    try
                    {
                        DecodeNextPacket();
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

        public override long Seek(long offset, SeekOrigin origin)
        {
            long newPosition = -1;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    newPosition = offset;
                    break;
                case SeekOrigin.Current:
                    newPosition = offset + m_position;
                    break;
                case SeekOrigin.End:
                    newPosition = Length - m_position;
                    break;
            }

            if (newPosition < 0)
                newPosition = 0;
            else if (newPosition > Length)
                newPosition = Length;

            bool Backward = newPosition > m_position;

            int flags = FFmpeg.AVSEEK_FLAG_ANY | (Backward ? FFmpeg.AVSEEK_FLAG_BACKWARD : 0);
            long newTimestamp = (long)((newPosition / (m_avCodecCtx.channels * sizeof(short) * m_avCodecCtx.sample_rate)) * FFmpeg.AV_TIME_BASE);

            m_bufferUsedSize = 0;

            if (FFmpeg.av_seek_frame(ref m_avFormatCtx, -1, newTimestamp, flags) < 0)
            {
                m_position = Length;
                return m_position;
            }

            FFmpeg.avcodec_flush_buffers(ref m_avCodecCtx);

            m_position = offset;

            return m_position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }

    public class DecoderException : ApplicationException
    {
        public DecoderException() { }
        public DecoderException(string Message) : base(Message) { }
    }
}
