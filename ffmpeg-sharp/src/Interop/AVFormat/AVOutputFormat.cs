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

        public int priv_data_size;

        /// <summary>
        /// default audio codec 
        /// </summary>
        public CodecID audio_codec;

        /// <summary>
        /// default video codec
        /// </summary>
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

        public OutputFormatFlags flags;

        private IntPtr set_parameters_ptr;
        /// <summary>
        /// currently only used to set pixel format if not YUV420P
        /// </summary>
        public SetParametersCallback set_parameters
        {
            get { return Utils.GetDelegate<SetParametersCallback>(set_parameters_ptr); }
        }

        private IntPtr interleave_packet_ptr;
        public InterleavePacketCallback interleave_packet
        {
            get { return Utils.GetDelegate<InterleavePacketCallback>(interleave_packet_ptr); }
        }

        /// <summary>
        /// list of supported codec_id-codec_tag pairs, ordered by "better choice first"
        /// the arrays are all CODEC_ID_NONE terminated
        /// </summary>
        AVCodecTag** codec_tag;

        /// <summary>
        /// default subtitle codec
        /// </summary>
        CodecID subtitle_codec;

        public AVOutputFormat* next;
    };
}
