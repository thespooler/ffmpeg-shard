#region LGPL License
//
// AVFrac.cs
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
using System;

namespace FFmpegSharp.Interop.Format
{
    /// <summary>
    /// Fractional numbers for exact pts handling
    /// </summary>
    /// <remarks>
    /// the exact value of the fractional number is: 'val + num / den'.
    /// * num is assumed to be such as 0 &lt= num &lt den
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [Obsolete("Use AVRational instead")]
    public unsafe struct AVFrac
    {
        long val;
        long num;
        long den;
    };
}
