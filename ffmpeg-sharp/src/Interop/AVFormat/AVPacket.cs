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
        public int duration;

        private IntPtr destruct_ptr;
        public DestructCallback destruct
        {
            get { return Utils.GetDelegate<DestructCallback>(destruct_ptr); }
        }

        public IntPtr priv;
        public long pos;
    };
}
