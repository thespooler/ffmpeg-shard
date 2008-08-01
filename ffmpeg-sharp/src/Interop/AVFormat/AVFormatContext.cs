#region LGPL License
//
// AVFormatContext.cs
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
using System.Runtime.InteropServices;
using FFmpegSharp.Interop.AVIO;
using FFmpegSharp.Interop.Util;
using FFmpegSharp.Interop.Codec;

namespace FFmpegSharp.Interop.Format
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVFormatContext
    {
        /// <summary>
        /// Set by av_alloc_format_context
        /// </summary>
        public AVClass* pAVClass;

        public AVInputFormat* iformat; // can only be iformat or oformat, not both at the same time 
        public AVOutputFormat* oformat;

        public void* priv_data;

#if LIBAVFORMAT_51
        public ByteIOContext pb;
#else
        public ByteIOContext* pb;
#endif

        public uint nb_streams;

        public AVStreamArray20 streams;

        private fixed sbyte filename_ptr[1024];
        /// <summary>
        /// Input/Output filename
        /// </summary>
        public string filename
        {
            get
            {
                fixed (sbyte* ptr = filename_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = filename_ptr)
                    Utils.SetString(ptr, 1024, value);
            }
        }

        /* stream info */
        public long timestamp;

        private fixed sbyte title_ptr[512];
        public string title
        {
            get
            {
                fixed (sbyte* ptr = title_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = title_ptr)
                    Utils.SetString(ptr, 512, value);
            }
        }

        private fixed sbyte author_ptr[512];
        public string author
        {
            get
            {
                fixed (sbyte* ptr = author_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = author_ptr)
                    Utils.SetString(ptr, 512, value);
            }
        }

        private fixed sbyte copyright_ptr[512];
        public string copyright
        {
            get
            {
                fixed (sbyte* ptr = copyright_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = copyright_ptr)
                    Utils.SetString(ptr, 512, value);
            }
        }

        private fixed sbyte comment_ptr[512];
        public string comment
        {
            get
            {
                fixed (sbyte* ptr = comment_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = comment_ptr)
                    Utils.SetString(ptr, 512, value);
            }
        }

        private fixed sbyte album_ptr[512];
        public string album
        {
            get
            {
                fixed (sbyte* ptr = album_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = album_ptr)
                    Utils.SetString(ptr, 512, value);
            }
        }

        /// <summary>
        /// ID3 year, 0 if none
        /// </summary>
        public int year;

        /// <summary>
        /// Track number, 0 if none
        /// </summary>
        public int track;

        private fixed sbyte genre_ptr[32];
        /// <summary>
        /// ID3 genre
        /// </summary>
        public string genre
        {
            get
            {
                fixed (sbyte* ptr = genre_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = genre_ptr)
                    Utils.SetString(ptr, 32, value);
            }
        }

        public FormatSpecificFlags ctx_flags; // format specific flags, see AVFMTCTX_xx

        /// <summary>
        /// This buffer is only needed when packets were already buffered but
        /// not decoded, for example to get the codec parameters in mpeg
        /// streams
        /// </summary>
        public AVPacketList* packet_buffer;

        /// <summary>
        /// decoding: position of the first frame of the component, in
        /// AV_TIME_BASE fractional seconds. NEVER set this value directly:
        /// it is deduced from the AVStream values.
        /// </summary>
        public long start_time;


        /// <summary>
        /// decoding: duration of the stream, in AV_TIME_BASE fractional
        /// seconds. NEVER set this value directly: it is deduced from the
        /// AVStream values. 
        /// </summary>
        public long duration;

        /// <summary>
        /// decoding: Total file size. 0 if unknown.
        /// </summary>
        public long file_size;

        /// <summary>
        /// decoding: total stream bitrate in bit/s, 0 if not
        /// available. Never set it directly if the file_size and the
        /// duration are known as ffmpeg can compute it automatically.
        /// </summary>
        public int bit_rate;

        /* av_read_frame() support */

        public AVStream* cur_st;

        public byte* cur_ptr; // byte
        public int cur_len;

        public AVPacket cur_pkt;

        /* av_seek_frame() support */

        /// <summary>
        /// Offset of the first packet
        /// </summary>
        public long data_offset;

        public int index_built;

        public int mux_rate;

        public int packet_size;

        public int preload;

        public int max_delay;

        /// <summary>
        /// Number of times to loop output in formats that support it
        /// </summary>
        public OutputLoop loop_output;

        public AVFMT_FLAG flags;

        public int loop_input;

        /// <summary>
        /// Decoding: size of data to probe
        /// Encoding: unused
        /// </summary>
        public uint probesize;

        /// <summary>
        /// Maximum duration in AV_TIME_BASE units over which the input should be 
        /// analyzed in av_find_stream_info()
        /// </summary>
        public int max_analyze_duration;

        public byte* key;
        public int keylen;

        public uint nb_programs;
        AVProgram** programs;

        /**
         * Forced video codec_id.
         * demuxing: set by user
         */
        public CodecID video_codec_id;
        /**
         * Forced audio codec_id.
         * demuxing: set by user
         */
        public CodecID audio_codec_id;
        /**
         * Forced subtitle codec_id.
         * demuxing: set by user
         */
        public CodecID subtitle_codec_id;

        /**
         * Maximum amount of memory in bytes to use per stream for the index.
         * If the needed index exceeds this size entries will be discarded as
         * needed to maintain a smaller size. This can lead to slower or less
         * accurate seeking (depends on demuxer).
         * Demuxers for which a full in memory index is mandatory will ignore
         * this.
         * muxing  : unused
         * demuxing: set by user
         */
        public uint max_index_size;

        /**
         * Maximum amount of memory in bytes to use for buffering frames
         * obtained from real-time capture devices.
         */
        public uint max_picture_buffer;

        public uint nb_chapters;
        public AVChapter** chapters;
    };
}
