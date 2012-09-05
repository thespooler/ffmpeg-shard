#region LGPL License
//
// AVCodec.cs
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FFmpegSharp.Interop.Util;
using FFmpegSharp.Interop.Format;

namespace FFmpegSharp.Interop.Codec
{
    public delegate int InitThreadCopyCallback(ref AVCodecContext pAVCodecContext);
    public delegate int UpdateThreadContextCallback(ref AVCodecContext pAVCodecContextDst, ref AVCodecContext pAVCodecContextSrc);
    public delegate int InitStaticDataCallback(ref AVCodec pAVCodec);
    public delegate int InitCallback(ref AVCodecContext pAVCodecContext);
    public delegate int EncodeCallback(ref AVCodecContext pAVCodecContext, byte[] buf, int buf_size, byte[] data);
    public delegate int Encode2Callback(ref AVCodecContext pAVCodecContext, ref AVPacket packet, ref AVFrame frame, ref int got_packet_ptr);
    public delegate int DecodeCallback(ref AVCodecContext pAVCodecContext, ref byte[] outdata, ref int outdata_size, ref AVPacket avpkt);
    public delegate int CloseCallback(ref AVCodecContext pAVCodecContext);
    public delegate int FlushCallback(ref AVCodecContext pAVCodecContext);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVCodec
    {
        /**
         * Name of the codec implementation.
         * The name is globally unique among encoders and among decoders (but an
         * encoder and a decoder can share the same name).
         * This is the primary way to find a codec from the user perspective.
         */
        private sbyte* name_ptr;
        public string name
        {
            get { return new string(name_ptr); }
        }

        private sbyte* longname_ptr;
        public string longname
        {
            get { return new string(longname_ptr); }
        }

        public AVMediaType type;

        public AVCodecID id;

        public int capabilities;

        AVRational* supported_framerates; ///< array of supported framerates, or NULL if any, array is terminated by {0,0}
        PixelFormat* pix_fmts;       ///< array of supported pixel formats, or NULL if unknown, array is terminated by -1
        int* supported_samplerates;       ///< array of supported audio samplerates, or NULL if unknown, array is terminated by 0
        AVSampleFormat* sample_fmts; ///< array of supported sample formats, or NULL if unknown, array is terminated by -1
        ulong* channel_layouts;         ///< array of support channel layouts, or NULL if unknown. array is terminated by 0
        byte max_lowres;                     ///< maximum value for lowres supported by the decoder
        AVClass* priv_class;              ///< AVClass for the private context
        AVProfile* profiles;              ///< array of recognized profiles, or NULL if unknown, array is terminated by {FF_PROFILE_UNKNOWN}
                                          
        int priv_data_size;
        AVCodec *next;

        private IntPtr init_thread_copy_ptr;
        public InitThreadCopyCallback init_thread_copy
        {
            get { return (Utils.GetDelegate<InitThreadCopyCallback>(init_thread_copy_ptr)); }
        }

        private IntPtr update_thread_context_ptr;
        public UpdateThreadContextCallback update_thread_context
        {
            get { return (Utils.GetDelegate<UpdateThreadContextCallback>(update_thread_context_ptr)); }
        }

        AVCodecDefault* defaults;

        private IntPtr init_static_data_ptr;
        public InitStaticDataCallback init_static_data
        {
            get { return (Utils.GetDelegate<InitStaticDataCallback>(init_static_data_ptr)); }
        }

        private IntPtr init_ptr;
        public InitCallback init
        {
            get { return (Utils.GetDelegate<InitCallback>(init_ptr)); }
        }
        
        private IntPtr encode_ptr;
        public EncodeCallback encode
        {
            get { return (Utils.GetDelegate<EncodeCallback>(encode_ptr)); }
        }

        private IntPtr encode2_ptr;
        public Encode2Callback encode2
        {
            get { return (Utils.GetDelegate<Encode2Callback>(encode2_ptr)); }
        }

        private IntPtr decode_ptr;
        public DecodeCallback decode
        {
            get { return (Utils.GetDelegate<DecodeCallback>(decode_ptr)); }
        }

        private IntPtr close_ptr;
        public CloseCallback close
        {
            get { return (Utils.GetDelegate<CloseCallback>(close_ptr)); }
        }

        private IntPtr flush_ptr;
        public FlushCallback flush
        {
            get { return (Utils.GetDelegate<FlushCallback>(flush_ptr)); }
        }
    }

    public unsafe struct AVCodecDefault
    {
        byte* key;
        byte* value;
    }
}
