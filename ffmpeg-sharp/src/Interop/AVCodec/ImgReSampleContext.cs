#region LGPL License
//
// ImgReSampleContext.cs
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
#if LIBAVCODEC_51
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImgReSampleContext
    {
        public int iwidth, iheight, owidth, oheight;
        public int topBand, bottomBand, leftBand, rightBand;
        public int padtop, padbottom, padleft, padright;
        public int pad_owidth, pad_oheight;
        public int h_incr, v_incr;
        public IntPtr h_filters; /* horizontal filters */
        public IntPtr v_filters; /* vertical filters */
        public byte* line_buf;
    }
#endif
}
