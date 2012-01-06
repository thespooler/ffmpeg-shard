using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop;
using FFmpegSharp.Interop.Format;
using System.IO;
using FFmpegSharp.Interop.Codec;
using System.Collections.ObjectModel;

namespace FFmpegSharp
{
    public unsafe class MediaFile : IDisposable
    {
        internal AVFormatContext FormatContext;
        private bool m_disposed = false;
        private SortedList<int, DecoderStream> m_streams;
        private string m_filename;

        #region Properties

        public unsafe ReadOnlyCollection<DecoderStream> Streams
        {
            get { return new ReadOnlyCollection<DecoderStream>(m_streams.Values); }
        }

        public string Filename
        {
            get { return m_filename; }
        }

        public long Length
        {
            get { return FormatContext.file_size; }
        }

        public string FileFormat
        {
            get { unsafe { return FormatContext.iformat->name; } }
        }

        /// <summary>
        /// Duration of the stream
        /// </summary>
        public TimeSpan Duration
        {
            get { return new TimeSpan((long)(RawDuration * 1e7)); }
        }

        public double RawDuration
        {
            get
            {
                double duration = (double)(FormatContext.duration / (double)FFmpeg.AV_TIME_BASE);
                if (duration < 0)
                    duration = 0;
                return duration;
            }
        }

        #endregion

        static MediaFile()
        {
            // Register all codecs and protocols
            FFmpeg.av_register_all();
#if DEBUG
            FFmpeg.av_log_set_level(1000);
#endif
        }

        public MediaFile(FileInfo File) : this(File.FullName) { }

        public MediaFile(FileStream File, CodecType codecType) : this(File.Name) { }

        public unsafe MediaFile(string Filename)
        {
            if (String.IsNullOrEmpty(Filename))
                throw new ArgumentNullException("Filename");

            m_filename = Filename;

            // Open the file with FFmpeg
            if (FFmpeg.av_open_input_file(out FormatContext, Filename) != AVError.OK)
                throw new DecoderException("Couldn't open file");

            if (FFmpeg.av_find_stream_info(ref FormatContext) < AVError.OK)
                throw new DecoderException("Couldn't find stream info");

            if (FormatContext.nb_streams < 1)
                throw new DecoderException("No streams found");

            m_streams = new SortedList<int, DecoderStream>();
            for (int i = 0; i < FormatContext.nb_streams; i++)
            {
                AVStream stream = *FormatContext.streams[i];

                switch (stream.codec->codec_type)
                {
                    case CodecType.CODEC_TYPE_VIDEO:
                        m_streams.Add(i, new VideoDecoderStream(this, ref stream));
                        break;
                    case CodecType.CODEC_TYPE_AUDIO:
                        m_streams.Add(i, new AudioDecoderStream(this, ref stream));
                        break;
                    case CodecType.CODEC_TYPE_UNKNOWN:
                    case CodecType.CODEC_TYPE_DATA:
                    case CodecType.CODEC_TYPE_SUBTITLE:
                    default:
                        m_streams.Add(i, null);
                        break;
                }
            }
        }

        internal void EnqueueNextPacket()
        {
            AVPacket packet = new AVPacket();
            FFmpeg.av_init_packet(ref packet);

            if (FFmpeg.av_read_frame(ref FormatContext, ref packet) < 0)
                throw new System.IO.EndOfStreamException();

            DecoderStream dest = null;
            if (m_streams.TryGetValue(packet.stream_index, out dest))
                dest.PacketQueue.Enqueue(packet);
            else
                FFmpeg.av_free_packet(ref packet);
        }

        ~MediaFile()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                m_disposed = true;

                if (disposing)
                {
                    m_streams = null;
                }

                FFmpeg.av_close_input_file(ref FormatContext);
            }
        }
    }
}
