#region LGPL License
//
// AVBitStreamFilter.cs
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
    public delegate int FilterCallback(ref AVBitStreamFilterContext pAVBitStreamFilterContext,
                                       ref AVCodecContext pAVCodecContext,
                                       string args,
                                       ref byte[] poutbuf, ref int poutbuf_size,
                                       byte[] buf, int buf_size, int keyframe);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVBitStreamFilter
    {
        private sbyte* name_ptr;
        public string name
        {
            get { return new string(name_ptr); }
        }

        public int priv_data_size;

        private IntPtr filter_ptr;
        public FilterCallback filter
        {
            get { return Utils.GetDelegate<FilterCallback>(filter_ptr); }
        }

        public AVBitStreamFilter* next;
    };
}
