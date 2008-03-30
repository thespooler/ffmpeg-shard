#region LGPL License
//
// ByteIOContext.cs
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
    public delegate int ReadPacketCallback(IntPtr opaque, [In, Out] byte[] buf, int buf_size);

    public delegate int WritePacketCallback(IntPtr opaque, [In, Out] byte[] buf, int buf_size);

    public delegate long SeekCallback(IntPtr opaque, long offset, int whence);

    public delegate uint UpdateChecksumCallback(uint checksum, [In, Out] byte[] buf, uint size);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ByteIOContext
    {
        public byte* buffer;
        public int buffer_size;

        public byte* buf_ptr;
        public byte* buf_end;

        public IntPtr opaque;

        private IntPtr read_packet_ptr;
        public ReadPacketCallback read_packet
        {
            get { return Utils.GetDelegate<ReadPacketCallback>(read_packet_ptr); }
        }

        private IntPtr write_packet_ptr;
        public WritePacketCallback write_packet
        {
            get { return Utils.GetDelegate<WritePacketCallback>(write_packet_ptr); }
        }

        private IntPtr seek_ptr;
        public SeekCallback seek
        {
            get { return Utils.GetDelegate<SeekCallback>(seek_ptr); }
        }

        public long pos; // position in the file of the current buffer 

        public int must_flush; // true if the next seek should flush

        public int eof_reached; // true if eof reached

        public int write_flag;  // true if open for writing 

        [MarshalAs(UnmanagedType.Bool)]
        public bool is_streamed;

        public int max_packet_size;

        public uint checksum;

        public byte* checksum_ptr;

        private IntPtr update_checksum_ptr;
        public UpdateChecksumCallback update_checksum
        {
            get { return Utils.GetDelegate<UpdateChecksumCallback>(update_checksum_ptr); }
        }

        public int error; // contains the error code or 0 if no error happened
    };
}
