#region LGPL License
//
// AVInputFormat.cs
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
    public delegate int ReadProbeCallback([In, Out]AVProbeData pAVProbeData);
    public delegate int ReadHeaderCallback([In, Out]AVFormatContext pAVFormatContext, AVFormatParameters pAVFormatParameters);
    public delegate int ReadPacketCallback([In, Out]AVFormatContext pAVFormatContext, [In, Out]AVPacket pAVPacket);
    public delegate int ReadCloseCallback([In, Out]AVFormatContext pAVFormatContext);
    public delegate int ReadSeekCallback([In, Out]AVFormatContext pAVFormatContext, int stream_index, long timestamp, int flags);
    public delegate long ReadTimestampCallback([In, Out]AVFormatContext pAVFormatContext, int stream_index, ref long pos, long pos_limit);
    public delegate int ReadPlayCallback([In, Out]AVFormatContext pAVFormatContext);
    public delegate int ReadPauseCallback([In, Out]AVFormatContext pAVFormatContext);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVInputFormat
    {
        private sbyte* name_ptr;
        public string name
        {
            get { return new string(name_ptr); }
        }

        private sbyte* long_name_ptr;
        public string long_name
        {
            get { return new string(long_name_ptr); }
        }

        public int priv_data_size;

        private IntPtr read_probe_ptr;
        public ReadProbeCallback read_probe
        {
            get { return Utils.GetDelegate<ReadProbeCallback>(read_probe_ptr); }
        }

        private IntPtr read_header_ptr;
        public ReadHeaderCallback read_header
        {
            get { return Utils.GetDelegate<ReadHeaderCallback>(read_header_ptr); }
        }

        private IntPtr read_packet_ptr;
        public ReadPacketCallback read_packet
        {
            get { return Utils.GetDelegate<ReadPacketCallback>(read_packet_ptr); }
        }

        private IntPtr read_close_ptr;
        public ReadCloseCallback read_close
        {
            get { return Utils.GetDelegate<ReadCloseCallback>(read_close_ptr); }
        }

        private IntPtr read_seek_ptr;
        public ReadSeekCallback read_seek
        {
            get { return Utils.GetDelegate<ReadSeekCallback>(read_seek_ptr); }
        }

        private IntPtr read_timestamp_ptr;
        public ReadTimestampCallback read_timestamp
        {
            get { return Utils.GetDelegate<ReadTimestampCallback>(read_timestamp_ptr); }
        }

        public InputFormatFlags flags;

        private sbyte* extensions_ptr;
        public string extensions
        {
            get { return new string(extensions_ptr); }
        }

        public int value;

        private IntPtr read_play_ptr;
        public ReadPlayCallback read_play
        {
            get { return Utils.GetDelegate<ReadPlayCallback>(read_play_ptr); }
        }

        private IntPtr read_pause_ptr;
        public ReadPauseCallback read_pause
        {
            get { return Utils.GetDelegate<ReadPauseCallback>(read_pause_ptr); }
        }

        public AVInputFormat* next;
    };
}
