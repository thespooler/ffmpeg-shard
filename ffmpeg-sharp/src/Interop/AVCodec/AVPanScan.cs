#region LGPL License
//
// AVPanScan.cs
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

using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop.Codec
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVPanScan
    {
        /**
         * id.
         * - encoding: set by user.
         * - decoding: set by lavc
         */
        public int id;
        /**
         * width and height in 1/16 pel
         * - encoding: set by user.
         * - decoding: set by lavc
         */
        public int width;
        public int height;
        /**
         * position of the top left corner in 1/16 pel for up to 3 fields/frames.
         * - encoding: set by user.
         * - decoding: set by lavc
         */
        // [3][2] = 3 x 2 = 6
        [Broken("Possibly wrong size")]
        public fixed short position[6];
    };

}
