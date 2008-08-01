#region LGPL License
//
// AVIndexEntry.cs
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

namespace FFmpegSharp.Interop.Format
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVIndexEntry
    {
        public long pos;
        public long timestamp;

        private int bitmask;

        public AVINDEX_FLAG flags
        {
            get { return (AVINDEX_FLAG)(bitmask & 4); }
            set
            {
                bitmask = (bitmask & 252) | ((byte)value & 3);
            }
        }
        public int size
        {
            get { return (bitmask & 252) >> 2; }
            set
            {
                bitmask = (value << 2) | (bitmask & 4);
            }
        }

        public int min_distance;
    };
}
