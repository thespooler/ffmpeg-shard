#region LGPL License
//
// AVImageFormat.cs
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

// Not found in latest avformat.h

/*using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop.Format
{
    public delegate int AllocCBCallback(IntPtr pVoid, IntPtr pAVImageInfo);

    public delegate int ImgProbeCallback(IntPtr pAVProbeData);
    public delegate int ImgReadCallback(IntPtr pByteIOContext,
                                        [MarshalAs(UnmanagedType.FunctionPtr)]
                                            AllocCBCallback alloc_cb,
                                        IntPtr pVoid);
    public delegate int ImgWriteCallback(IntPtr pByteIOContext, IntPtr pAVImageInfo);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVImageFormat
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public byte* name;

        [MarshalAs(UnmanagedType.LPTStr)]
        public byte* extensions;

        // tell if a given file has a chance of being parsing by this format
        private IntPtr img_probe_ptr;
        public ImgProbeCallback img_probe
        {
            get { return Utils.GetDelegate<ImgProbeCallback>(img_probe_ptr); }
        }

        // read a whole image. 'alloc_cb' is called when the image size is
        // known so that the caller can allocate the image. If 'allo_cb'
        // returns non zero, then the parsing is aborted. Return '0' if
        // OK. 
        private IntPtr img_read_ptr;
        public ImgReadCallback img_read
        {
            get { return Utils.GetDelegate<ImgReadCallback>(img_read_ptr); }
        }

        // write the image 
        public int supported_pixel_formats; // mask of supported formats for output

        private IntPtr img_write_ptr;
        public ImgWriteCallback img_write
        {
            get { return Utils.GetDelegate<ImgWriteCallback>(img_write_ptr); }
        }

        public int flags;

        public AVImageFormat* next;
    };
}*/
