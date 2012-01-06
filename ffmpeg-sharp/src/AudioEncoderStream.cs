#region LGPL License
//
// AudioEncoderStream.cs
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
using System.IO;
using System.Text;
using System.Xml.Serialization;
using FFmpegSharp.Interop;
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Format;
using FFmpegSharp.Util;

namespace FFmpegSharp
{
    public unsafe class AudioEncoderStream : Stream
    {
        #region Private Instance Variables

        private AVFormatContext m_avFormatCtx;
        private AVCodecContext m_avCodecCtx;
        private AVStream m_avStream;
        private bool m_disposed;
        private bool m_fileOpen;
        private string m_filename;
        private FifoMemoryStream m_buffer;
        private int m_totalWritten;

        #endregion

        #region Properties

        public int FrameSize
        {
            get { return m_avCodecCtx.frame_size * m_avCodecCtx.channels * 2; } //2 == Sample Size (16-bit)
        }

        public string Filename { get { return m_filename; } }

        public override bool CanRead { get { return false; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return true; } }

        public override long Length { get { return m_totalWritten; } }

        public override long Position
        {
            get { return Length; }
            set { throw new NotSupportedException(); }
        }

        #endregion

        public AudioEncoderStream(string Filename, AudioCodec Codec, int Bitrate, int SampleRate, int Channels, bool VBR)
            : this(Filename, new EncoderInformation(Codec, Bitrate, SampleRate, Channels, VBR)) { }

        public AudioEncoderStream(string Filename, EncoderInformation EncoderInfo)
        {
            // Initialize instance variables
            m_filename = Filename;
            m_disposed = m_fileOpen = false;
            m_buffer = new FifoMemoryStream();

            // Open FFmpeg
            FFmpeg.av_register_all();

            // Initialize the output format context
            m_avFormatCtx = FFmpeg.av_alloc_format_context();

            // Get output format
            m_avFormatCtx.oformat = FFmpeg.guess_format(EncoderInfo.Codec.ShortName, null, null);
            if (m_avFormatCtx.oformat != null)
                throw new EncoderException("Could not find output format.");

            FFmpeg.av_set_parameters(ref m_avFormatCtx, null);

            // Initialize the new output stream
            AVStream* stream = FFmpeg.av_new_stream(ref m_avFormatCtx, 1);
            if (stream == null)
                throw new EncoderException("Could not alloc output audio stream");

            m_avStream = *stream;

            // Initialize output codec context
            m_avCodecCtx = *m_avStream.codec;

            m_avCodecCtx.codec_id = EncoderInfo.Codec.CodecID;
            m_avCodecCtx.codec_type = CodecType.CODEC_TYPE_AUDIO;
            m_avCodecCtx.sample_rate = EncoderInfo.SampleRate;
            m_avCodecCtx.channels = EncoderInfo.Channels;
            m_avCodecCtx.bits_per_sample = EncoderInfo.SampleSize;
            m_avCodecCtx.bit_rate = EncoderInfo.Bitrate;

            if (EncoderInfo.VBR)
            {
                m_avCodecCtx.flags |= FFmpeg.CODEC_FLAG_QSCALE;
                m_avCodecCtx.global_quality = EncoderInfo.FFmpegQualityScale;
            }

            // Open codec
            AVCodec* outCodec = FFmpeg.avcodec_find_encoder(m_avCodecCtx.codec_id);
            if (outCodec == null)
                throw new EncoderException("Could not find encoder");

            if (FFmpeg.avcodec_open(ref m_avCodecCtx, outCodec) < 0)
                throw new EncoderException("Could not open codec.");

            // Open and prep file
            if (FFmpeg.url_fopen(ref m_avFormatCtx.pb, m_filename, FFmpeg.URL_WRONLY) < 0)
                throw new EncoderException("Could not open output file.");

            m_fileOpen = true;

            FFmpeg.av_write_header(ref m_avFormatCtx);
        }

        public override void Flush()
        {
            while (m_buffer.Length > 0)
                EncodeAndWritePacket();
        }

        private void EncodeAndWritePacket()
        {
            byte[] frameBuffer = new byte[FrameSize];
            m_buffer.Read(frameBuffer, 0, frameBuffer.Length);

            fixed (byte* pcmSamples = frameBuffer)
            {
                if (m_disposed)
                    throw new ObjectDisposedException(this.ToString());

                AVPacket outPacket = new AVPacket();
                FFmpeg.av_init_packet(ref outPacket);

                byte[] buffer = new byte[FFmpeg.FF_MIN_BUFFER_SIZE];
                fixed (byte* encodedData = buffer)
                {
                    try
                    {
                        outPacket.size = FFmpeg.avcodec_encode_audio(ref m_avCodecCtx, encodedData, FFmpeg.FF_MIN_BUFFER_SIZE, (short*)pcmSamples);
                        outPacket.pts = m_avCodecCtx.coded_frame->pts;
                        outPacket.flags |= FFmpeg.PKT_FLAG_KEY;
                        outPacket.stream_index = m_avStream.index;
                        outPacket.data = (IntPtr)encodedData;

                        if (outPacket.size > 0)
                        {
                            if (FFmpeg.av_write_frame(ref m_avFormatCtx, ref outPacket) != 0)
                                throw new IOException("Error while writing encoded audio frame to file");
                        }
                    }
                    finally
                    {
                        FFmpeg.av_free_packet(ref outPacket);
                    }
                }
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (m_disposed)
                throw new ObjectDisposedException(this.ToString());

            m_buffer.Write(buffer, offset, count);

            while (m_buffer.Length >= FrameSize)
                EncodeAndWritePacket();

            m_totalWritten += count;
        }

        protected override void Dispose(bool Disposing)
        {
            if (!m_disposed)
            {
                if (Disposing)
                {
                    m_filename = null;
                }

                if (m_avCodecCtx.codec != null)
                    FFmpeg.avcodec_close(ref m_avCodecCtx);

                for (int i = 0; i < m_avFormatCtx.nb_streams; i++)
                {
                    IntPtr ptr = (IntPtr)m_avFormatCtx.streams[i]->codec;
                    FFmpeg.av_freep(ref ptr);

                    ptr = (IntPtr)m_avFormatCtx.streams[i];
                    FFmpeg.av_freep(ref ptr);
                }

                if (m_fileOpen)
                    FFmpeg.url_fclose(ref m_avFormatCtx.pb);
            }

            m_disposed = true;
        }

        #region Unsupported Stream Methods

        public override int Read(byte[] buffer, int offset, int count) { throw new NotSupportedException(); }
        public override long Seek(long offset, SeekOrigin origin) { throw new NotSupportedException(); }
        public override void SetLength(long value) { throw new NotSupportedException(); }

        #endregion
    }

    public class AudioCodec
    {
        private string m_shortname;
        private CodecID m_id;

        public string ShortName { get { return m_shortname; } }
        public CodecID CodecID { get { return m_id; } }

        public AudioCodec(string ShortName, CodecID ID)
        {
            m_shortname = ShortName;
            m_id = ID;
        }

        public static readonly AudioCodec AAC = new AudioCodec("mp4", CodecID.CODEC_ID_AAC);
        public static readonly AudioCodec AC3 = new AudioCodec("ac3", CodecID.CODEC_ID_AC3);
        public static readonly AudioCodec FLAC = new AudioCodec("flac", CodecID.CODEC_ID_FLAC);
        public static readonly AudioCodec MP2 = new AudioCodec("mp2", CodecID.CODEC_ID_MP2);
        public static readonly AudioCodec MP3 = new AudioCodec("mp3", CodecID.CODEC_ID_MP3);
        public static readonly AudioCodec PCM = new AudioCodec("wav", CodecID.CODEC_ID_PCM_S16BE);
        public static readonly AudioCodec Vorbis = new AudioCodec("ogg", CodecID.CODEC_ID_VORBIS);
        public static readonly AudioCodec WMA = new AudioCodec("asf", CodecID.CODEC_ID_WMAV2);
    }

    public class EncoderInformation
    {
        public readonly AudioCodec Codec;
        /// <summary>
        /// The bitrate of the audio if doing constant bitrate encoding (VBR == false)
        /// </summary>
        public readonly int Bitrate;

        public readonly int SampleRate;

        public readonly int Channels;

        /// <summary>
        /// Quality Scale if using VBR (valid values, 1-100)
        /// </summary>
        public readonly float QualityScale;

        /// <summary> Sample Size (in bits) </summary>
        public readonly int SampleSize;

        public readonly bool VBR;

        public EncoderInformation(AudioCodec Codec, int Bitrate, int SampleRate, int Channels, bool VBR)
        {
            this.Codec = Codec;
            this.Bitrate = Bitrate;
            this.SampleRate = SampleRate;
            this.Channels = Channels;
            this.VBR = VBR;
            this.QualityScale = 0;
            this.SampleSize = sizeof(short);
        }

        public static EncoderInformation Deserialize(String xmlString)
        {
            StringReader reader = new StringReader(xmlString);

            XmlSerializer s = new XmlSerializer(typeof(EncoderInformation));
            EncoderInformation info;
            info = (EncoderInformation)s.Deserialize(reader);

            return info;
        }

        public string SerializeToXML()
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer s = new XmlSerializer(typeof(EncoderInformation));
            TextWriter w = new StringWriter(sb);
            s.Serialize(w, this);

            return sb.ToString();
        }

        public int FFmpegQualityScale
        {
            get
            {
                float ffqscale = 0;

                if (this.Codec == AudioCodec.AAC)
                {
                    ffqscale = QualityScale * 5;
                    ffqscale = ffqscale < 10 ? 10 : ffqscale;
                }

                if (this.Codec == AudioCodec.MP3)
                {
                    ffqscale = (float)Math.Round(QualityScale / 11);
                    ffqscale = ffqscale > 9 ? 9 : ffqscale;
                }
                if (this.Codec == AudioCodec.Vorbis)
                {
                    ffqscale = QualityScale / 100;
                }

                return (int)Math.Round(ffqscale) * FFmpeg.FF_QP2LAMBDA;
            }
        }
    }

    public class EncoderException : ApplicationException
    {
        public EncoderException() { }
        public EncoderException(string Message) : base(Message) { }
    }
}
