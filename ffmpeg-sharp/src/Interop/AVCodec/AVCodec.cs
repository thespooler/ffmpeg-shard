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

namespace FFmpegSharp.Interop.Codec
{
    public delegate int InitCallback(ref AVCodecContext pAVCodecContext);

    public delegate int EncodeCallback(ref AVCodecContext pAVCodecContext, byte[] buf, int buf_size, byte[] data);

    public delegate int CloseCallback(ref AVCodecContext pAVCodecContext);

    public delegate int DecodeCallback(ref AVCodecContext pAVCodecContext, ref byte[] outdata, ref int outdata_size,
                                        byte[] buf, int buf_size);

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

        public CodecType type;

        public CodecID id;

        public int priv_data_size;

        private IntPtr init_ptr;
        public InitCallback init
        {
            get { return (Utils.GetDelegate<InitCallback>(init_ptr)); }
        }

        private IntPtr encode_ptr;
        public EncodeCallback encode
        {
            get { return (Utils.GetDelegate<EncodeCallback>(init_ptr)); }
        }

        private IntPtr close_ptr;
        public CloseCallback close
        {
            get { return (Utils.GetDelegate<CloseCallback>(close_ptr)); }
        }

        private IntPtr decode_ptr;
        public DecodeCallback decode
        {
            get { return (Utils.GetDelegate<DecodeCallback>(decode_ptr)); }
        }

        public CODEC_CAP capabilities;

        public AVCodec* next;

        private IntPtr flush_ptr;
        public FlushCallback flush
        {
            get { return (Utils.GetDelegate<FlushCallback>(flush_ptr)); }
        }

        /// <summary>
        /// Array of supported framerates, or null.  If any, array is terminated by {0,0}
        /// </summary>
        public AVRational* supported_framerates;

        /// <summary>
        /// Array of supported pixel formats, or null.  If unknown, array is terminanted by -1
        /// </summary>
        public PixelFormat* pix_fmts;
    };
}
