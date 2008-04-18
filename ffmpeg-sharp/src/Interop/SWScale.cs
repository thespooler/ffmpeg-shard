#region LGPL License
//
// SWScale.cs
//
// Author:
//   Tim Jones (tim@roastedamoeba.com
//
// Copyright (C) 2008 Tim Jones
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
using System.Security;
using FFmpegSharp.Interop.SWScale;

namespace FFmpegSharp.Interop
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class FFmpeg
    {
        public const string SWSCALE_DLL_NAME = "swscale-0.dll";

        #region Functions

        [DllImport(SWSCALE_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void sws_freeContext(ref SwsContext SwsContext);

        [DllImport(SWSCALE_DLL_NAME, EntryPoint = "sws_getContext", CharSet = CharSet.Ansi)]
        private unsafe static extern IntPtr sws_getContext_internal(int source_width, int source_height,
            int source_pix_fmt, int dest_width, int dest_height, int dest_pix_fmt, int flags,
            ref SwsFilter srcFilter, ref SwsFilter destFilter, double* Param);

        public unsafe static SwsContext* sws_getContext(int source_width, int source_height,
            int source_pix_fmt, int dest_width, int dest_height, int dest_pix_fmt, int flags,
            ref SwsFilter srcFilter, ref SwsFilter destFilter, double* Param)
        {
            return (SwsContext*)sws_getContext_internal(source_width, source_height,
                source_pix_fmt, dest_width, dest_height, dest_pix_fmt, flags,
                ref srcFilter, ref destFilter, Param);
        }

        [DllImport(SWSCALE_DLL_NAME, EntryPoint = "sws_getContext", CharSet = CharSet.Ansi)]
        private unsafe static extern IntPtr sws_getContext_internal(int source_width, int source_height,
            int source_pix_fmt, int dest_width, int dest_height, int dest_pix_fmt, int flags,
            SwsFilter* srcFilter, SwsFilter* destFilter, double* Param);

        public unsafe static SwsContext* sws_getContext(int source_width, int source_height,
            int source_pix_fmt, int dest_width, int dest_height, int dest_pix_fmt, int flags)
        {
            return (SwsContext*)sws_getContext_internal(source_width, source_height,
                source_pix_fmt, dest_width, dest_height, dest_pix_fmt, flags,
                null, null, null);
        }

        [DllImport(SWSCALE_DLL_NAME, CharSet = CharSet.Ansi)]
        public unsafe static extern int sws_scale(SwsContext* SwsContext,
            int* src,
            int* srcStride,
            int srcSliceY, int srcSliceH,
            int* dst,
            int* dstStride);

        [DllImport(SWSCALE_DLL_NAME, CharSet = CharSet.Ansi)]
        public unsafe static extern int sws_scale(SwsContext* SwsContext,
            byte*[] src,
            int[] srcStride,
            int srcSliceY, int srcSliceH,
            byte*[] dst,
            int[] dstStride);

        [DllImport(SWSCALE_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int sws_rgb2rgb_init(int flags);

        #endregion

        #region Constants

        public const int SWS_FAST_BILINEAR = 1;
        public const int SWS_BILINEAR = 2;
        public const int SWS_BICUBIC = 4;
        public const int SWS_X = 8;
        public const int SWS_POINT = 0x10;
        public const int SWS_AREA = 0x20;
        public const int SWS_BICUBLIN = 0x40;
        public const int SWS_GAUSS = 0x80;
        public const int SWS_SINC = 0x100;
        public const int SWS_LANCZOS = 0x200;
        public const int SWS_SPLINE = 0x400;

        public const int SWS_SRC_V_CHR_DROP_MASK = 0x30000;
        public const int SWS_SRC_V_CHR_DROP_SHIFT = 16;

        public const int SWS_PARAM_DEFAULT = 123456;

        public const int SWS_PRINT_INFO = 0x1000;

        //the following 3 flags are not completely implemented
        //internal chrominace subsampling info

        public const int SWS_FULL_CHR_H_INT = 0x2000;
        //
        /// <summary>
        /// Input subsampling info
        /// </summary>
        public const int SWS_FULL_CHR_H_INP = 0x4000;

        public const int SWS_DIRECT_BGR = 0x8000;

        public const int SWS_ACCURATE_RND = 0x40000;

        // public const int SWS_CPU_CAPS_MMX     = 0x80000000;
        // public const int SWS_CPU_CAPS_MMX2    = 0x20000000;
        // public const int SWS_CPU_CAPS_3DNOW   = 0x40000000;
        //public const int SWS_CPU_CAPS_ALTIVEC = 0x10000000;
        //public const int SWS_CPU_CAPS_BFIN    = 0x01000000;

        public const double SWS_MAX_REDUCE_CUTOFF = 0.002;

        public const int MAX_FILTER_SIZE = 256;

        #endregion
    }
}
