#region LGPL License
//
// AVOutputFormat.cs
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

namespace FFmpegSharp.Interop.Format
{
    public delegate int WriteHeader(ref AVFormatContext pAVFormatContext);
    public delegate int WritePacket(ref AVFormatContext pAVFormatContext, ref AVPacket pAVPacket);
    public delegate int WriteTrailer(ref AVFormatContext pAVFormatContext);
    public delegate int SetParametersCallback(ref AVFormatContext pAVFormatContext, ref AVFormatParameters avFormatParameters);
    public delegate int InterleavePacketCallback(ref AVFormatContext pAVFormatContext, ref AVPacket pOutAVPacket, ref AVPacket pInAVPacket, int flush);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVOutputFormat
    {
        IntPtr name_ptr;
        public string name { get { return Utils.GetString(name_ptr); } }

        IntPtr long_name_ptr;
        public string long_name { get { return Utils.GetString(long_name_ptr); } }

        IntPtr mime_type_ptr;
        public string mime_type { get { return Utils.GetString(mime_type_ptr); } }

        IntPtr extensions_ptr;
        public string extensions { get { return Utils.GetString(extensions_ptr); } }

        public int priv_data_size;

        public CodecID audio_codec;

        public CodecID video_codec;

        private IntPtr write_header_ptr;
        public WriteHeader write_header
        {
            get { return Utils.GetDelegate<WriteHeader>(write_header_ptr); }
        }

        private IntPtr write_packet_ptr;
        public WritePacket write_packet
        {
            get { return Utils.GetDelegate<WritePacket>(write_packet_ptr); }
        }

        private IntPtr write_trailer_ptr;
        public WriteTrailer write_trailer
        {
            get { return Utils.GetDelegate<WriteTrailer>(write_trailer_ptr); }
        }

        public int flags;

        private IntPtr set_parameters_ptr;
        public SetParametersCallback set_parameters
        {
            get { return Utils.GetDelegate<SetParametersCallback>(set_parameters_ptr); }
        }

        private IntPtr interleave_packet_ptr;
        public InterleavePacketCallback interleave_packet
        {
            get { return Utils.GetDelegate<InterleavePacketCallback>(interleave_packet_ptr); }
        }

        public AVOutputFormat* next;
    };
}
