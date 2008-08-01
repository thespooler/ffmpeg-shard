#region LGPL License
//
// URLProtocol.cs
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

namespace FFmpegSharp.Interop.AVIO
{
    public delegate int OpenCallback(ref URLContext h, string filename, int flags);
    public unsafe delegate int ReadCallback(ref URLContext h, [In, Out]byte[] buf, int size);
    public unsafe delegate int WriteCallBack(ref URLContext h, [In, Out]byte[] buf, int size);
    public delegate long SeekCallBack(ref URLContext h, long pos, int whence);
    public delegate int CloseCallback(ref URLContext h);

    public unsafe struct URLProtocol
    {
#pragma warning disable 649
        private sbyte* name_ptr;
#pragma warning restore 649
        public string name
        {
            get { return new string(name_ptr); }
        }

        private IntPtr open_ptr;
        public OpenCallback Open
        {
            get { return Utils.GetDelegate<OpenCallback>(open_ptr); }
            set { open_ptr = Marshal.GetFunctionPointerForDelegate(value); }
        }

        private IntPtr read_ptr;
        public ReadCallback Read
        {
            get { return Utils.GetDelegate<ReadCallback>(read_ptr); }
            set { read_ptr = Marshal.GetFunctionPointerForDelegate(value); }
        }

        private IntPtr write_ptr;
        public WriteCallBack Write
        {
            get { return Utils.GetDelegate<WriteCallBack>(write_ptr); }
            set { write_ptr = Marshal.GetFunctionPointerForDelegate(value); }
        }

        private IntPtr seek_ptr;
        public SeekCallBack Seek
        {
            get { return Utils.GetDelegate<SeekCallBack>(seek_ptr); }
            set { seek_ptr = Marshal.GetFunctionPointerForDelegate(value); }
        }

        private IntPtr close_ptr;
        public CloseCallback Close
        {
            get { return Utils.GetDelegate<CloseCallback>(close_ptr); }
            set { close_ptr = Marshal.GetFunctionPointerForDelegate(value); }
        }

        public URLProtocol* next;
    }
}
