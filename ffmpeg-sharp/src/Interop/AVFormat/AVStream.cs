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

        public long start_time;

        // decoding: duration of the stream, in AV_TIME_BASE fractional seconds. 
        public long duration;

        public long nb_frames; // number of frames in this stream if known or 0

        public AVDisposition disposition;

        public AVDiscard discard;

        public AVRational sample_aspect_ratio;

        public AVDictionary* metadata;

        public AVRational avg_frame_rate;

        public AVPacket attached_pic;

        IntPtr stream_info_priv;

        public int pts_wrap_bits;

        public long reference_dts;

        public long first_dts;

        public long cur_dts;

        public long last_IP_pts;

        public int last_IP_duration;

        public int probe_packets;

        public int codec_info_nb_frames;

        public int stream_identifier;

        public long interleaver_chunk_size;
        public long interleaver_chunk_duration;

        public AVStreamParseType need_parsing;

        public AVCodecParserContext* parser;

        public AVPacketList* last_in_packet_buffer;

        AVProbeData probe_data;
        
        const int MAX_REORDER_DELAY = 16;
        public fixed long pts_buffer[MAX_REORDER_DELAY+1];

        public AVIndexEntry* index_entries;

        public int nb_index_entries;
        public uint index_entries_allocated_size;

        public int request_probe;
        public int skip_to_keyframe;
        public int skip_samples;
    };

    public enum AVStreamParseType
    { }

    public enum AVDisposition
    { }

    public enum AVDiscard
    {
        AVDISCARD_NONE = -16, ///< discard nothing
        AVDISCARD_DEFAULT = 0, ///< discard useless packets like 0 size packets in avi
        AVDISCARD_NONREF = 8, ///< discard all non reference
        AVDISCARD_BIDIR = 16, ///< discard all bidirectional frames
        AVDISCARD_NONKEY = 32, ///< discard all frames except keyframes
        AVDISCARD_ALL = 48, ///< discard all
    }
}
