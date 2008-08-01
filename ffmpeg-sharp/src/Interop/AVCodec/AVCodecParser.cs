#region LGPL License
//
// AVCodecParser.cs
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
    public delegate int ParaerInitCallback(ref AVCodecContext pAVCodecParserContext);

    public delegate int ParserParseCallback(ref AVCodecParserContext pAVCodecParserContext,
                                            ref AVCodecContext pAVCodecContext,
                                            ref byte[] poutbuf,
                                            ref int poutbuf_size,
                                            byte[] buf, int buf_size);

    public delegate void ParserCloseCallback(ref AVCodecParserContext pAVcodecParserContext);

    public delegate int SplitCallback(ref AVCodecContext pAVCodecContext, [In, Out]byte[] buf, int buf_size);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVCodecParser
    {
        /// <summary>
        /// CodecIDs
        /// </summary>
        public fixed int codec_ids[5]; /* several codec IDs are permitted */

        public int priv_data_size;

        private IntPtr parser_init_ptr;
        public ParaerInitCallback parser_init
        {
            get { return (Utils.GetDelegate<ParaerInitCallback>(parser_init_ptr)); }
        }

        private IntPtr parser_parse_ptr;
        public ParserParseCallback parser_parse
        {
            get { return (Utils.GetDelegate<ParserParseCallback>(parser_parse_ptr)); }
        }

        private IntPtr parser_close_ptr;
        public ParserCloseCallback parser_close
        {
            get { return (Utils.GetDelegate<ParserCloseCallback>(parser_close_ptr)); }
        }

        private IntPtr split_ptr;
        public SplitCallback split
        {
            get { return (Utils.GetDelegate<SplitCallback>(split_ptr)); }
        }

        public AVCodecParser* next;
    };
}
