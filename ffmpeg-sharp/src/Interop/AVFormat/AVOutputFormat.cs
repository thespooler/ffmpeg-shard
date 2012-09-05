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
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Interop.Format.Output
{
    public delegate int WriteHeader(ref AVFormatContext pAVFormatContext);
    public delegate int WritePacket(ref AVFormatContext pAVFormatContext, ref AVPacket pAVPacket);
    public delegate int WriteTrailer(ref AVFormatContext pAVFormatContext);
    public delegate int InterleavePacketCallback(ref AVFormatContext pAVFormatContext, ref AVPacket pOutAVPacket, ref AVPacket pInAVPacket, int flush);
    public delegate int QueryCodecCallback(AVCodecID id, int std_compliance);
    public delegate void GetOutputTimestampCallback(ref AVFormatContext pAVFormatContext, int stream, ref long dts, ref long wall);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVOutputFormat
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

        private sbyte* mime_type_ptr;
        public string mime_type
        {
            get { return new string(mime_type_ptr); }
        }

        private sbyte* extensions_ptr;
        public string extensions
        {
            get { return new string(extensions_ptr); }
        }

        public AVCodecID audio_codec;
        public AVCodecID video_codec;
        public AVCodecID subtitle_codec;

        public int flags;

        public AVCodecTag** codec_tag;

        public AVClass* priv_class;

        public AVOutputFormat* next;

        public int priv_data_size;

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

        private IntPtr interleave_packet_ptr;
        public InterleavePacketCallback interleave_packet
        {
            get { return Utils.GetDelegate<InterleavePacketCallback>(interleave_packet_ptr); }
        }

        private IntPtr query_codec_ptr;
        public QueryCodecCallback query_codec
        {
            get { return Utils.GetDelegate<QueryCodecCallback>(query_codec_ptr); }
        }

        private IntPtr get_output_timestamp_ptr;
        public GetOutputTimestampCallback get_output_timestamp
        {
            get { return Utils.GetDelegate<GetOutputTimestampCallback>(get_output_timestamp_ptr); }
        }
    };
}
