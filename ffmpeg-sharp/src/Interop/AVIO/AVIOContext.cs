using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop.Util;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop.AVIO.Context
{
    public delegate int PacketIOCallback(IntPtr opaque, [In, Out] byte[] buf, int buf_size);

    public delegate long SeekCallback(IntPtr opaque, long offset, int whence);

    public delegate uint UpdateChecksumCallback(uint checksum, [In, Out] byte[] buf, uint size);

    public delegate int ReadPauseCallback(IntPtr opaque, int pause);

    public delegate long ReadSeekCallback(IntPtr opaque, int stream_index, long timestamp, int flags);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVIOContext
    {
        public AVClass* pAVClass;

        public byte* buffer;
        public int buffer_size;

        public byte* buf_ptr;
        public byte* buf_end;

        public IntPtr opaque;

        private IntPtr read_packet_ptr;
        public PacketIOCallback read_packet
        {
            get { return Utils.GetDelegate<PacketIOCallback>(read_packet_ptr); }
        }

        private IntPtr write_packet_ptr;
        public PacketIOCallback write_packet
        {
            get { return Utils.GetDelegate<PacketIOCallback>(write_packet_ptr); }
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

        public uint max_packet_size;

        public uint checksum;

        public byte* checksum_ptr;

        private IntPtr update_checksum_ptr;
        public UpdateChecksumCallback update_checksum
        {
            get { return Utils.GetDelegate<UpdateChecksumCallback>(update_checksum_ptr); }
        }

        public int error; // contains the error code or 0 if no error happened

        private IntPtr read_pause_ptr;
        public ReadPauseCallback read_pause 
        {
            get { return Utils.GetDelegate<ReadPauseCallback>(read_pause_ptr); }
        }

        private IntPtr read_seek_ptr;
        public ReadSeekCallback read_seek
        {
            get { return Utils.GetDelegate<ReadSeekCallback>(read_seek_ptr); }
        }

        public int seekable; // A combination of AVIO_SEEKABLE_ flags or 0 when the stream is not seekable.

        long maxsize;

        public int direct;

        public long bytes_read;

        public int seek_count;
    };
}
