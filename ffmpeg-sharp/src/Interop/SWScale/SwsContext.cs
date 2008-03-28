#region LGPL License
//
// SwsContext.cs
//
// Author:
//   Tim Jones (tim@roastedamoeba.com)
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
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Interop.SWScale
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct SwsContext
	{
		/// <summary>
		/// info on struct for av_log
		/// </summary>
		public AVClass* av_class;

		/// <summary>
		/// Note that src, dst, srcStride, dstStride will be copied in the sws_scale() wrapper so they can be freely modified here.
		/// </summary>
		public IntPtr draw_horiz_band_ptr;

		public int srcW, srcH, dstH;
		public int chrSrcW, chrSrcH, chrDstW, chrDstH;
		public int lumXInc, chrXInc;
		public int lumYInc, chrYInc;
		public int dstFormat;

		/// <summary>
		/// format 4:2:0 type is always YV12
		/// </summary>
		public int srcFormat;

		public int origDstFormat, origSrcFormat;
		public int chrSrcHSubSample, chrSrcVSubSample;
		public int chrIntHSubSample, chrIntVSubSample;
		public int chrDstHSubSample, chrDstVSubSample;
		public int vChrDrop;
		public int sliceDir;
		public fixed double param[2];

		public IntPtr lumPixBuf;
		public IntPtr chrPixBuf;
		public IntPtr hLumFilter;
		public IntPtr hLumFilterPos;
		public IntPtr hChrFilter;
		public IntPtr hChrFilterPos;
		public IntPtr vLumFilter;
		public IntPtr vLumFilterPos;
		public IntPtr vChrFilter;
		public IntPtr vChrFilterPos;

		public fixed byte formatConvBuffer[4000];

		public int hLumFilterSize;
		public int hChrFilterSize;
		public int vLumFilterSize;
		public int vChrFilterSize;
		public int vLumBufSize;
		public int vChrBufSize;

		public byte* funnyYCode;
		public byte* funnyUVCode;
		public int* lumMmx2FilterPos;
		public int* chrMmx2FilterPos;
		public short* lumMmx2Filter;
		public short* chrMmx2Filter;

		public int canMMX2BeUsed;

		public int lastInLumBuf;
		public int lastInChrBuf;
		public int lumBufIndex;
		public int chrBufIndex;
		public int dstY;
		public int flags;
		public void* yuvTable;
		public fixed int table_rV[256];
		public fixed int table_gU[256];
		public fixed int table_gV[256];
		public fixed int table_bU[256];

		//Colorspace stuff
		public int contrast, brightness, saturation;    // for sws_getColorspaceDetails
		public fixed int srcColorspaceTable[4];
		public fixed int dstColorspaceTable[4];
		public int srcRange, dstRange;
	}
}
