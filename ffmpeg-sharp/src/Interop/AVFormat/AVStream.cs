#region LGPL License
//
// AVStream.cs
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
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Interop.Format
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVStream
    {
        public int index; // stream index in AVFormatContext

        public int id; // format specific stream id

        public AVCodecContext* codec; // AVCodecContext

        /**
         * real base frame rate of the stream.
         * for example if the timebase is 1/90000 and all frames have either
         * approximately 3600 or 1800 timer ticks then r_frame_rate will be 50/1
         */

        public AVRational r_frame_rate;

        public IntPtr priv_data;

        public long codec_info_duration; // internal data used in av_find_stream_info()

        public int codec_info_nb_frames;

#pragma warning disable 618
        public AVFrac pts; // encoding: PTS generation when outputing stream 
#pragma warning restore 618

        /**
         * this is the fundamental unit of time (in seconds) in terms
         * of which frame timestamps are represented. for fixed-fps content,
         * timebase should be 1/framerate and timestamp increments should be
         * identically 1.
         */

        public AVRational time_base;

        public int pts_wrap_bits; // number of bits in pts (used for wrapping control) 

        public int stream_copy; // if TRUE, just copy stream 


        public AVDiscard discard; // selects which packets can be discarded at will and dont need to be demuxed

        public float quality;

        public long start_time;

        // decoding: duration of the stream, in AV_TIME_BASE fractional seconds. 
        public long duration;

        private fixed sbyte language_ptr[4]; // ISO 639 3-letter language code (empty string if undefined)
        public string language
        {
            get
            {
                fixed (sbyte* ptr = language_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = language_ptr)
                    Utils.SetString(ptr, 4, value);
            }
        }

        public int need_parsing;

        public AVCodecParserContext* AVCodecParserContext;

        public long cur_dts;

        public int last_IP_duration;

        public long last_IP_pts;

        public AVIndexEntry* index_entries; // only used if the format does not support seeking natively

        public int nb_index_entries;

        public uint index_entries_allocated_size;

        public long nb_frames; // number of frames in this stream if known or 0

        public fixed long pts_buffer[FFmpeg.MAX_REORDER_DELAY + 1]; // pts_buffer[MAX_REORDER_DELAY+1]
    };
}
