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
using FFmpegSharp.Interop.Util;
using FFmpegSharp.Interop.Codec;

namespace FFmpegSharp.Interop
{
    [SuppressUnmanagedCodeSecurity]
    public static unsafe partial class FFmpeg
    {
        public const string SWSCALE_DLL_NAME = "swscale-0.dll";

        #region Functions

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_freeContext(SwsContext* swsContext);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern SwsContext* sws_getContext(int srcW, int srcH, PixelFormat srcFormat, int dstW, int dstH, PixelFormat dstFormat,
                                                        SwsFlags flags, SwsFilter* srcFilter, SwsFilter* dstFilter, double* param);

        /// <returns>0 on success, -1 on failure</returns>
        [DllImport(SWSCALE_DLL_NAME)]
        public static extern int sws_scale(SwsContext* context, byte** src, int* srcStride, int srcSliceY,
                                           int srcSliceH, byte** dst, int* dstStride);
        [DllImport(SWSCALE_DLL_NAME)]
        [Obsolete]
        public static extern int sws_scale_ordered(SwsContext* context, byte** src, int* srcStride, int srcSliceY,
                                                   int srcSliceH, byte** dst, int dstStride);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern int sws_setColorspaceDetails(SwsContext* c, int* inv_table, int srcRange, int* table, int dstRange, 
                                                          int brightness, int contrast, int saturation);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern int sws_getColorspaceDetails(SwsContext* c, int** inv_table, int* srcRange, int** table, int* dstRange, 
                                                          int* brightness, int* contrast, int* saturation);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern SwsVector* sws_getGaussianVec(double variance, double quality);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern SwsVector* sws_getConstVec(double c, int length);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern SwsVector* sws_getIdentityVec();

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_scaleVec(SwsVector* a, double scalar);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_normalizeVec(SwsVector* a, double height);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_convVec(SwsVector* a, SwsVector* b);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_addVec(SwsVector* a, SwsVector* b);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_subVec(SwsVector* a, SwsVector* b);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_shiftVec(SwsVector* a, int shift);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern SwsVector* sws_cloneVec(SwsVector* a);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_printVec(SwsVector* a);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_freeVec(SwsVector* a);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern SwsFilter* sws_getDefaultFilter(float lumaGBlur, float chromaGBlur,
                                                             float lumaSarpen, float chromaSharpen,
                                                             float chromaHShift, float chromaVShift,
                                                             int verbose);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern void sws_freeFilter(SwsFilter* filter);

        [DllImport(SWSCALE_DLL_NAME)]
        public static extern SwsContext* sws_getCachedContext(SwsContext* context, int srcW, int srcH, PixelFormat srcFormat,
                                                              int dstW, int dstH, int dstFormat, SwsFlags flags, SwsFilter* srcFilter, 
                                                              SwsFilter* dstFilter, double* param);

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

        public const uint SWS_CPU_CAPS_MMX = 0x80000000;
        public const int SWS_CPU_CAPS_MMX2 = 0x20000000;
        public const int SWS_CPU_CAPS_3DNOW = 0x40000000;
        public const int SWS_CPU_CAPS_ALTIVEC = 0x10000000;
        public const int SWS_CPU_CAPS_BFIN = 0x01000000;

        public const double SWS_MAX_REDUCE_CUTOFF = 0.002;

        public const int MAX_FILTER_SIZE = 256;

        public const int SWS_CS_ITU709 = 1;
        public const int SWS_CS_FCC = 4;
        public const int SWS_CS_ITU601 = 5;
        public const int SWS_CS_ITU624 = 5;
        public const int SWS_CS_SMPTE170M = 5;
        public const int SWS_CS_SMPTE240M = 7;
        public const int SWS_CS_DEFAULT = 5;

        #endregion
    }
}
