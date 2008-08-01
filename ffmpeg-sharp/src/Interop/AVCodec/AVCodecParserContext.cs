#region LGPL License
//
// AVCodecParserContext.cs
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

namespace FFmpegSharp.Interop.Codec
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVCodecParserContext
    {
        public IntPtr priv_data;

        public AVCodecParser* parser;

        public long frame_offset; // offset of the current frame 

        public long cur_offset; // current offset incremented by each av_parser_parse()

        public long last_frame_offset; // offset of the last frame

        /* video info */
        public int pict_type; /* XXX: put it back in AVCodecContext */

        public int repeat_pict; /* XXX: put it back in AVCodecContext */

        public long pts;     /* pts of the current frame */

        public long dts;     /* dts of the current frame */

        /* private data */
        public long last_pts;

        public long last_dts;

        public int fetch_timestamp;

        public int cur_frame_start_index;

        public fixed long cur_frame_offset[FFmpeg.AV_PARSER_PTS_NB];

        public fixed long cur_frame_pts[FFmpeg.AV_PARSER_PTS_NB];

        public fixed long cur_frame_dts[FFmpeg.AV_PARSER_PTS_NB];

        public PARSER_FLAG flags;

        public long offset;      // byte offset from starting packet start
        public long last_offset;
    };
}
