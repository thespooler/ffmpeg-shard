#region LGPL License
//
// AVPacket.cs
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

namespace FFmpegSharp.Interop.Format
{
    public delegate void DestructCallback(ref AVPacket pAVPacket);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVPacket
    {
        public long pts; // presentation time stamp in time_base units
        public long dts; // decompression time stamp in time_base units
        public IntPtr data;
        public int size;
        public int stream_index;
        public PacketFlags flags;

        public AVPacketSideData* side_data;
        public int side_data_elems;

        public int duration;

        private IntPtr destruct_ptr;
        public DestructCallback destruct
        {
            get { return Utils.GetDelegate<DestructCallback>(destruct_ptr); }
        }

        public IntPtr priv;
        public long pos;
        public long convergence_duration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVPacketSideData
    {
        public IntPtr data;
        public int size;
        public AVPacketSideDataType type;
    }

    public enum AVPacketSideDataType : int
    {
        AV_PKT_DATA_PALETTE,
        AV_PKT_DATA_NEW_EXTRADATA,

        /**
         * An AV_PKT_DATA_PARAM_CHANGE side data packet is laid out as follows:
         * @code
         * u32le param_flags
         * if (param_flags & AV_SIDE_DATA_PARAM_CHANGE_CHANNEL_COUNT)
         *     s32le channel_count
         * if (param_flags & AV_SIDE_DATA_PARAM_CHANGE_CHANNEL_LAYOUT)
         *     u64le channel_layout
         * if (param_flags & AV_SIDE_DATA_PARAM_CHANGE_SAMPLE_RATE)
         *     s32le sample_rate
         * if (param_flags & AV_SIDE_DATA_PARAM_CHANGE_DIMENSIONS)
         *     s32le width
         *     s32le height
         * @endcode
         */
        AV_PKT_DATA_PARAM_CHANGE,

        /**
         * An AV_PKT_DATA_H263_MB_INFO side data packet contains a number of
         * structures with info about macroblocks relevant to splitting the
         * packet into smaller packets on macroblock edges (e.g. as for RFC 2190).
         * That is, it does not necessarily contain info about all macroblocks,
         * as long as the distance between macroblocks in the info is smaller
         * than the target payload size.
         * Each MB info structure is 12 bytes, and is laid out as follows:
         * @code
         * u32le bit offset from the start of the packet
         * u8    current quantizer at the start of the macroblock
         * u8    GOB number
         * u16le macroblock address within the GOB
         * u8    horizontal MV predictor
         * u8    vertical MV predictor
         * u8    horizontal MV predictor for block number 3
         * u8    vertical MV predictor for block number 3
         * @endcode
         */
        AV_PKT_DATA_H263_MB_INFO,

        /**
         * Recommmends skipping the specified number of samples
         * @code
         * u32le number of samples to skip from start of this packet
         * u32le number of samples to skip from end of this packet
         * u8    reason for start skip
         * u8    reason for end   skip (0=padding silence, 1=convergence)
         * @endcode
         */
        AV_PKT_DATA_SKIP_SAMPLES = 70,
    }
}
