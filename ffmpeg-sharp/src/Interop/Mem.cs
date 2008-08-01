#region LGPL License
//
// Mem.cs
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

namespace FFmpegSharp.Interop
{
    public unsafe partial class FFmpeg
    {
        /// <summary>
        /// Frees memory which has been allocated with av_malloc(z)() or av_realloc().
        /// </summary>
        /// <remarks>
        /// ptr = IntPtr.Zero is explicetly allowed
        /// It is recommended that you use av_freep() instead.
        /// </remarks>
        [DllImport(AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_free(IntPtr ptr);

        /// <summary>
        /// Frees memory which has been allocated with av_malloc(z)() or av_realloc().
        /// </summary>
        /// <remarks>
        /// ptr = IntPtr.Zero is explicetly allowed
        /// It is recommended that you use av_freep() instead.
        /// </remarks>
        [DllImport(AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_free(void* ptr);

        /// <summary>
        /// Frees memory which has been allocated with av_malloc(z)() or av_realloc() and 
        /// sets the pointer to IntPtr.Zero.
        /// </summary>
        /// <param name="ptr">Pointer to the pointer which should be freed (void**)</param>
        [DllImport(AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_freep(ref IntPtr ptr);

        /// <summary>
        /// Frees memory which has been allocated with av_malloc(z)() or av_realloc() and 
        /// sets the pointer to IntPtr.Zero.
        /// </summary>
        /// <param name="ptr">Pointer to the pointer which should be freed (void**)</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_freep(void** ptr);
    }
}
