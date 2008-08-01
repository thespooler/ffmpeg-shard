#region LGPL License
//
// AVProbeData.cs
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
    /// <summary>
    /// this structure contains the data a format has to probe a file
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVProbeData
    {
        private sbyte* filename_ptr;
        public string filename
        {
            get { return new string(filename_ptr); }
        }

        public byte* buf;
        public int buf_size;
    }
}
