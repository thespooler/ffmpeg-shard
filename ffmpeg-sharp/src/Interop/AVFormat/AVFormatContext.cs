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

        public IntPtr priv_data;

        public ByteIOContext pb;

        public int nb_streams;

        AVStream* stream_ptrs1, stream_ptrs2, stream_ptrs3, stream_ptrs4,
                    stream_ptrs5, stream_ptrs6, stream_ptrs7, stream_ptrs8, stream_ptrs9,
                    stream_ptrs10, stream_ptrs11, stream_ptrs12, stream_ptrs13, stream_ptrs14,
                    stream_ptrs15, stream_ptrs16, stream_ptrs17, stream_ptrs18, stream_ptrs19, stream_ptrs20;
        public AVStream*[] streams
        {
            get
            {
                return new AVStream*[] { stream_ptrs1, stream_ptrs2, stream_ptrs3, stream_ptrs4,
            stream_ptrs5,stream_ptrs6,stream_ptrs7,stream_ptrs8,stream_ptrs9,
            stream_ptrs10,stream_ptrs11,stream_ptrs12,stream_ptrs13,stream_ptrs14,
            stream_ptrs15,stream_ptrs16,stream_ptrs17,stream_ptrs18,stream_ptrs19,stream_ptrs20};
            }
        }

        private fixed byte filename_ptr[1024];
        /// <summary>
        /// Input/Output filename
        /// </summary>
        public string filename
        {
            get
            {
                fixed (byte* ptr = filename_ptr)
                    return Utils.GetString(ptr);
            }
        }

        /* stream info */
        public long timestamp;

        private fixed byte title_ptr[512];
        public string title
        {
            get
            {
                fixed (byte* ptr = title_ptr)
                    return Utils.GetString(ptr);
            }
        }

        private fixed byte author_ptr[512];
        public string author
        {
            get
            {
                fixed (byte* ptr = author_ptr)
                    return Utils.GetString(ptr);
            }
        }

        private fixed byte copyright_ptr[512];
        public string copyright
        {
            get
            {
                fixed (byte* ptr = copyright_ptr)
                    return Utils.GetString(ptr);
            }
        }

        private fixed byte comment_ptr[512];
        public string comment
        {
            get
            {
                fixed (byte* ptr = comment_ptr)
                    return Utils.GetString(ptr);
            }
        }

        private fixed byte album_ptr[512];
        public string album
        {
            get
            {
                fixed (byte* ptr = album_ptr)
                    return Utils.GetString(ptr);
            }
        }

        /// <summary>
        /// ID3 year, 0 if none
        /// </summary>
        public int year;

        /// <summary>
        /// Track number, 0 if none
        /// </summary>
        public int tract;

        private fixed byte genre_ptr[32];
        /// <summary>
        /// ID3 genre
        /// </summary>
        public string genre
        {
            get
            {
                fixed (byte* ptr = genre_ptr)
                    return Utils.GetString(ptr);
            }
        }

        public int ctx_flags; // format specific flags, see AVFMTCTX_xx

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

        /* decoding: total stream bitrate in bit/s, 0 if not
           available. Never set it directly if the file_size and the
           duration are known as ffmpeg can compute it automatically. */
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
        public int loop_output;

        public int flags;

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
    };
}
