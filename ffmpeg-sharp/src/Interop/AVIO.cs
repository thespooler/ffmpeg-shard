#region LGPL License
//
// AVIO.cs
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
using System.Text;
using FFmpegSharp.Interop.AVIO;

namespace FFmpegSharp.Interop
{
    public delegate int URLInterruptCB();

    public unsafe partial class FFmpeg
    {
        public const string AVUTIL_DLL_NAME = "avutil-49.dll";

        public const int URL_RDONLY = 0;
        public const int URL_WRONLY = 1;
        public const int URL_RDWR = 2;

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        private static extern int url_open(out IntPtr h, string filename, int flags);

        public static int url_open(out URLContext h, string filename, int flags)
        {
            IntPtr ptr;
            int ret = url_open(out ptr, filename, flags);

            h = *(URLContext*)ptr.ToPointer();

            av_free(ptr);

            return ret;
        }

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_read(ref URLContext h, [In, Out]byte[] buf, int size);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_write(ref URLContext h, [In, Out]byte[] buf, int size);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long url_seek(ref URLContext h, long pos, int whence);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_close(ref URLContext h);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_exist(string filename);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long url_filesize(ref URLContext h);

        /// <summary>
        /// Returns the maximum packet size associated to packetized file
        /// handle. If the file is not packetized (stream like http or file on
        /// disk), then 0 is returned.
        /// </summary>
        /// <param name="h">File handle</param>
        /// <returns>Maximum packet size in bytes</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_get_max_packet_size(ref URLContext h);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void url_get_filename(ref URLContext h, [In, Out]StringBuilder buf, int buf_size);

        [Obsolete("Not Implemented")]
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_poll(ref URLPollEntry poll_table, int n, int timeout);

        /// <summary>
        /// Passing this as the "whence" parameter to a seek function causes it to
        /// return the filesize without seeking anywhere. Supporting this is optional.
        /// If it is not supported then the seek function will return <0.
        /// </summary>
        public const int AVSEEK_SIZE = 0x10000;

        /*
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern URLProtocol* first_protocol;

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern URLInterruptCB* url_interrupt_cb;
        */

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int register_protocol(ref URLProtocol protocol);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int init_put_byte(ref ByteIOContext s, [In, Out]byte[] buffer, int buffer_size,
                          int write_flag, IntPtr opaque, ReadPacketCallback read_packet,
                          WritePacketCallback write_packet, SeekCallback seek);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_byte(ref ByteIOContext s, int b);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_buffer(ref ByteIOContext s, [In, Out]byte[] buf, int size);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_le64(ref ByteIOContext s, ulong val);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_be64(ref ByteIOContext s, ulong val);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_le32(ref ByteIOContext s, uint val);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_be32(ref ByteIOContext s, uint val);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_le24(ref ByteIOContext s, uint val);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_be24(ref ByteIOContext s, uint val);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_le16(ref ByteIOContext s, uint val);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_be16(ref ByteIOContext s, uint val);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_tag(ref ByteIOContext s, string tag);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_strz(ref ByteIOContext s, string buf);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long url_fseek(ref ByteIOContext s, long offset, int whence);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void url_fskip(ref ByteIOContext s, long offset);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long url_ftell(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long url_fsize(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_feof(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_ferror(ref ByteIOContext s);

        public const int URL_EOF = -1;

        /// <returns>Returns URL_EOF (-1) if EOF</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_fgetc(ref ByteIOContext s);

        /// <remarks> 
        /// Unlike fgets, the EOL character is not returned and a whole
        /// line is parsed. Returns NULL if first char read was EOF 
        /// </remarks>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string url_fgets(ref ByteIOContext s, [In, Out]byte[] buf, int buf_size);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void put_flush_packet(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int get_buffer(ref ByteIOContext s, [In, Out]byte[] buf, int size);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int get_partial_buffer(ref ByteIOContext s, [In, Out]byte[] buf, int size);

        /// <remarks> 
        /// Returns 0 if EOF, so you cannot use it if EOF handing is necessary
        /// </remarks>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int get_byte(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint get_le24(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint get_le32(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern ulong get_le64(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint get_le16(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string get_strz(ref ByteIOContext s, [In, Out]byte[] buf, int maxlen);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint get_be16(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint get_be24(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint get_be32(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern ulong get_be64(ref ByteIOContext s);

        public static bool url_is_streamed(ref ByteIOContext s)
        {
            return s.is_streamed;
        }

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_fdopen(ref ByteIOContext s, ref URLContext h);

        /// <remarks> Must be called before any I/O</remarks>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_setbufsize(ref ByteIOContext s, int buf_size);

        /// <remarks>
        /// When opened as read/write, the buffers are only used for reading
        /// </remarks>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_fopen(ref ByteIOContext s, string filename, int flags);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_fclose(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern URLContext* url_fileno(ref ByteIOContext s);

        /// <summary>
        /// Return the maximum packet size associated to packetized buffered file
        /// handle. If the file is not packetized (stream like http or file on
        /// disk), then 0 is returned.
        /// </summary>
        ///
        /// <param name="s">Buffered file handle</param>
        /// <returns>Maximum packet size in bytes</returns>
        ///
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_fget_max_packet_size(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_open_buf(ref ByteIOContext s, [In, Out]byte[] buf, int buf_size, int flags);

        /// <summary>
        /// Closes a buffer
        /// </summary>
        /// <returns>Return the written or read size</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_close_buf(ref ByteIOContext s);

        /// <summary>
        /// Open a write only memory stream.
        /// </summary>
        /// <param name="s">New IO context</param>
        /// <returns>Zero if no error.</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_open_dyn_buf(ref ByteIOContext s);

        /// <summary>
        /// Open a write only packetized memory stream with a maximum packet
        /// size of 'max_packet_size'.  The stream is stored in a memory buffer
        /// with a big endian 4 byte header giving the packet size in bytes.
        /// </summary>
        /// <param name="s">New IO context</param>
        /// <param name="max_packet_size">Maximum packet size (must be > 0)</param>
        /// <returns>Zero if no error.</returns>
        ////
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_open_dyn_packet_buf(ref ByteIOContext s, int max_packet_size);

        /// <returns>Number of bytes written to the buffer</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int url_close_dyn_buf(ref ByteIOContext s, out byte* pbuffer);

        public static void url_close_dyn_buf(ref ByteIOContext s, out byte[] buffer)
        {
            byte* ptr;
            int length = url_close_dyn_buf(ref s, out ptr);

            buffer = new byte[length];
            Marshal.Copy((IntPtr)ptr, buffer, 0, length);

            FFmpeg.av_free(ptr);
        }

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern ulong get_checksum(ref ByteIOContext s);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void init_checksum(ref ByteIOContext s, UpdateChecksumCallback update_checksun, ulong checksum);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int udp_set_remote_url(ref URLContext h, string uri);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int udp_get_local_port(ref URLContext h);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int udp_get_file_handle(ref URLContext h);

        /*
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern URLProtocol tcp_protocol;

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern URLProtocol http_protocol;

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern URLProtocol file_protocol;

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern URLProtocol pipe_protocol;

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern URLProtocol udp_protocol;
        */
    }
}
