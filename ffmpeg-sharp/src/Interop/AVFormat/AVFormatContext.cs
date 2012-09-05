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
using FFmpegSharp.Interop.Format.Input;
using FFmpegSharp.Interop.Format.Context;
using FFmpegSharp.Interop.Format.Output;

namespace FFmpegSharp.Interop.Format
{
    public delegate int AVIOInterruptCBDelegate(IntPtr opaque);

    public unsafe struct AVIOInterruptCB
    {
        private IntPtr callback_ptr;
        
        public AVIOInterruptCBDelegate callback
        {
            get { return Utils.GetDelegate<AVIOInterruptCBDelegate>(callback_ptr); }
        }

        public IntPtr opaque;
    }

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

        public AVIOContext* pb;

        public int ctx_flags;

        public uint nb_streams;
        public AVStream** streams;

        private fixed sbyte filename_ptr[1024];
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
                    Utils.SetString(ptr, 512, value);
            }
        }

        public long start_time;

        public long duration;

        public int bit_rate;

        public uint packet_size;
        public int max_delay;

        public AVFormatFlags flags;

        public uint probesize;

        public int max_analyse_duration;

        public IntPtr key;
        public int keylen;

        public uint nb_programs;
        public AVProgram** programs;

        public AVCodecID video_codec_id;
        public AVCodecID audio_codec_id;
        public AVCodecID subtitle_codec_id;

        public uint max_index_size;

        public uint max_picture_buffer;

        public uint nb_chapters;
        public AVChapter** chapters;

        public AVDictionary* metadata;

        public long start_time_realtime;

        public int fps_probe_size;

        public int error_recognition;

        public AVIOInterruptCB interrupt_callback;

        public int debug;

        public int ts_id;

        public int audio_preload;

        public int max_chunk_duration;

        public int max_chunk_size;

        public int use_wallclock_as_timestamps;

        public AVPacketList* packet_buffer;
        public AVPacketList* packet_buffer_end;

        public long data_offset;

        public AVPacketList* raw_packet_buffer;
        public AVPacketList* raw_packet_buffer_end;

        public AVPacketList* parse_queue;
        public AVPacketList* parse_queue_end;

        public int raw_packet_buffer_remaining_size;

        public int avio_flags;

        public AVDurationEstimationMethod duration_estimation_method;
    };

    [Flags]
    public enum AVFormatFlags : int
    {
        GeneratePTS     = 0x00001,
        IgnoreIndex     = 0x00002,
        NonBlocking     = 0x00004,
        IgnoreDTS       = 0x00008,
        NoFilling       = 0x00010,
        NoParse         = 0x00020,
        NoBuffer        = 0x00040,
        CustomIO        = 0x00080,
        DiscardCorrupt  = 0x00100,
        MP4A_LATM       = 0x08000,
        SortDTS         = 0x10000,
        PrivOpt         = 0x20000,
        KeepSideData    = 0x40000
    }

    public enum AVDurationEstimationMethod: int
    {
        AVFMT_DURATION_FROM_PTS,    ///< Duration accurately estimated from PTSes
        AVFMT_DURATION_FROM_STREAM, ///< Duration estimated from a stream with a known duration
        AVFMT_DURATION_FROM_BITRATE ///< Duration estimated from bitrate (less accurate)
    };
}
