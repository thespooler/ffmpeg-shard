#region LGPL License
//
// AVFormat.cs
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
using System.Security;
using System.Text;
using FFmpegSharp.Interop.AVIO;
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Format;

namespace FFmpegSharp.Interop
{
    [SuppressUnmanagedCodeSecurity]
    public unsafe partial class FFmpeg
    {
#if LIBAVFORMAT_51
        public const string AVFORMAT_DLL_NAME = "avformat-51.dll";
#else
        public const string AVFORMAT_DLL_NAME = "avformat-52.dll";
#endif

        #region Functions

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_destruct_packet_nofree(ref AVPacket pAVPacket);

        /// <summary>
        /// Default packet destructor
        /// </summary>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_destruct_packet(ref AVPacket pAVPacket);

        /// <summary>
        /// Initialize optional fields of a packet.
        /// </summary>
        /// <param name="pAVPacket">packet</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_init_packet(ref AVPacket pAVPacket);

        /// <summary>
        /// Allocate the payload of a pack and initialize its fields to default values
        /// </summary>
        /// <param name="pAVPacket">packet</param>
        /// <param name="size">wanted payload size</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_new_packet(ref AVPacket pAVPacket, int size);

        /// <summary>
        /// Allocate and read the payload of a packet and initialize its fields to defalut values
        /// </summary>
        /// <param name="pByteIOContext"></param>
        /// <param name="pAVPacket">packet</param>
        /// <param name="size">wanted payload size</param>
        /// <returns>>0 (read size) if OK.  AVError otherwise</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_get_packet(ref ByteIOContext pByteIOContext, ref AVPacket pAVPacket, int size);

        /// <summary>
        /// <warning>This is a hack - the packet memory allocation stuff is broken.  The
        /// packet is allocated if it was not really allocated</warning>
        /// </summary>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_dup_packet(ref AVPacket pAVPacket);

        /// <summary>
        /// Free a packet
        /// </summary>
        /// <param name="pAVPacket">packet to free</param>
        /// <remarks>
        /// Inline function defined in avformat.h, thus its code must be duplicated here since inline functions 
        /// cannot be DllImported
        /// </remarks>
        public static void av_free_packet(ref AVPacket packet)
        {
            if (packet.destruct != null)
                packet.destruct(ref packet);
        }

        #region AVImage //Doesn't seems to exist in latest avformat.h
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVImageFormat"></param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_register_image_format(IntPtr pAVImageFormat);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVProbeData"></param>
        /// <returns></returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern IntPtr av_probe_image_format(IntPtr pAVProbeData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern IntPtr guess_image_format([MarshalAs(UnmanagedType.LPTStr)]byte* filename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern CodecID av_guess_image2_codec([MarshalAs(UnmanagedType.LPTStr)]
                                                                    byte* filename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pByteIOContext"></param>
        /// <param name="filename"></param>
        /// <param name="pAVImageFormat"></param>
        /// <param name="alloc_cb"></param>
        /// <param name="opaque"></param>
        /// <returns></returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_read_image(IntPtr pByteIOContext,
                                            [MarshalAs(UnmanagedType.LPTStr)]byte* filename,
                                            IntPtr pAVImageFormat,
                                            [MarshalAs(UnmanagedType.FunctionPtr)]
                                            AllocCBCallback alloc_cb,
                                            IntPtr opaque);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pByteIOContext"></param>
        /// <param name="pAVImageFormat"></param>
        /// <param name="pAVImageInfo"></param>
        /// <returns></returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_write_image(IntPtr pByteIOContext, IntPtr pAVImageFormat, IntPtr pAVImageInfo);

        */
        #endregion

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_register_input_format(ref AVInputFormat pAVInputFormat);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_register_output_format(ref AVOutputFormat pAVOutputFormat);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOutputFormat* guess_stream_format(string short_name, string filename,
                                                                 string mime_type);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOutputFormat* guess_format(string short_name, string filename,
                                                          string mime_type);

        /// <summary>
        /// Guesses the codec id based upon muxer and filename
        /// </summary>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern CodecID av_guess_codec(ref AVOutputFormat pAVOutoutFormat, string short_name,
                                                    string filename, string mime_type, CodecType type);

        /// <summary>
        /// Send a nice hexadecimal dump of a buffer to the specified file stream.
        /// </summary>
        /// <param name="pFile">The file stream pointer where the dump should be sent to</param>
        /// <param name="buf">buffer</param>
        /// <param name="size">buffer size</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_hex_dump(IntPtr pFile, byte* buf, int size);

        /// <summary>
        /// Send a nice headecimal dump of a buffer to the log
        /// </summary>
        /// <param name="avcl">A pointer to an arbitrary struct of which the first field is a 
        /// pointer to an AVClass struct</param>
        /// <param name="level">The impotance level of the message, lower values signifying
        /// higher importance</param>
        /// <param name="buf">buffer</param>
        /// <param name="size">buffer size</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_hex_dump_log(void* avcl, int level, byte* buf, int size);

        /// <summary>
        /// Sends a nice dump of a packet to the specified file stream.
        /// </summary>
        /// <param name="pFile">The file stream pointer where the dump should be sent to</param>
        /// <param name="pAVPacket">packet to dump</param>
        /// <param name="dump_payload">true if the payload must be displayed too</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_pkt_dump(IntPtr pFile, ref AVPacket pAVPacket, [MarshalAs(UnmanagedType.Bool)] bool dump_payload);

        /// <summary>
        /// Registers all codecs with the library.
        /// </summary>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_register_all();


        /* media file input */
        /// <summary>
        /// Finds ref AVInputFormat based on input format's short name
        /// </summary>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVInputFormat* av_find_input_format(string short_name);

        /// <summary>
        /// Guess file format.
        /// </summary>
        /// <param name="pAVProbeData"></param>
        /// <param name="is_opened">whether the file is already opened, determines whether 
        /// demuxers with or without AVFMT_NOFILE are probed</param>
        /// <returns>ref AVInputFormat pointer</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVInputFormat* av_probe_input_format(ref AVProbeData pAVProbeData, [MarshalAs(UnmanagedType.Bool)] bool is_opened);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        private static extern AVError av_open_input_stream(out AVFormatContext* ic_ptr, ref ByteIOContext pb,
                                                           string filename, ref AVInputFormat fmt, ref AVFormatParameters ap);

        /// <summary>
        /// Allocates all the structures needed to read an input stream.
        /// This does not open the needed codecs for decoding the stream[s].
        /// </summary>
        private static AVError av_open_input_stream(out AVFormatContext ctx, ref ByteIOContext pb,
                                                    string filename, ref AVInputFormat fmt, ref AVFormatParameters ap)
        {
            AVFormatContext* ptr;
            AVError err = av_open_input_stream(out ptr, ref pb, filename, ref fmt, ref ap);

            ctx = *ptr;

            FFmpeg.av_free(ptr);

            return err;
        }


        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        private static extern AVError av_open_input_file(out IntPtr pFormatContext,
                                                         string filename,
                                                         AVInputFormat* fmt,
                                                         int buf_size,
                                                         AVFormatParameters* ap);

        /// <summary>
        /// Opens a media file as input.  The codecs are not opened.  Only the file
        /// header (if present) is read.
        /// </summary>
        /// <param name="pFormatContext">the opened media file handle is put here</param>
        /// <param name="filename">filename to open</param>
        /// <returns>AVError</returns>
        public static AVError av_open_input_file(out AVFormatContext pFormatContext, string filename)
        {
            IntPtr ptr;
            AVError err = av_open_input_file(out ptr, filename, null, 0, null);

            if (ptr == IntPtr.Zero)
            {
                pFormatContext = new AVFormatContext();
                return err;
            }

            pFormatContext = *(AVFormatContext*)ptr.ToPointer();

            FFmpeg.av_freep(ref ptr);

            return err;
        }


        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        private static extern AVError av_open_input_file(out IntPtr pFormatContext,
                                                         string filename,
                                                         ref AVInputFormat fmt,
                                                         int buf_size,
                                                         ref AVFormatParameters ap);

        /// <summary>
        /// Opens a media file as input.  The codecs are not opened.  Only the file
        /// header (if present) is read.
        /// </summary>
        /// <param name="pFormatContext">the opened media file handle is put here</param>
        /// <param name="filename">filename to open</param>
        /// <param name="pAVInputFormat">if non null, force the file format to use</param>
        /// <param name="buf_size">optional buffer size (zero as default is OK)</param>
        /// <param name="pAVFormatParameters">Additional parameters needed when open the file (Null if default)</param>
        /// <returns>AVError</returns>
        public static AVError av_open_input_file(out AVFormatContext pFormatContext, string filename,
                                                 ref AVInputFormat fmt, int buf_size, ref AVFormatParameters ap)
        {
            IntPtr ptr;
            AVError err = av_open_input_file(out ptr, filename, ref fmt, 0, ref ap);

            if (ptr == IntPtr.Zero)
            {
                pFormatContext = new AVFormatContext();
                return err;
            }

            pFormatContext = *(AVFormatContext*)ptr.ToPointer();

            FFmpeg.av_freep(ref ptr);

            return err;
        }

        /// <summary>
        /// Allocates an empty AVFormatContext
        /// </summary>
        /// <remarks>This must be freed using <see cref="FFmpeg.av_free()">FFmpeg.av_free()</see> </remarks>
        [DllImport("avformat-51.dll", CharSet = CharSet.Ansi, EntryPoint = "av_alloc_format_context")]
        private static extern AVFormatContext* av_alloc_format_context_internal();

        /// <summary>
        /// Allocates an empty AVFormatContext with its default values set
        /// </summary>
        public static AVFormatContext av_alloc_format_context()
        {
            AVFormatContext* ptr = av_alloc_format_context_internal();
            AVFormatContext ctx = *ptr;

            IntPtr p = new IntPtr(ptr);
            FFmpeg.av_freep(ref p);
            return ctx;
        }

        /// <summary>
        /// Read packets of a media file to get stream information. This
        /// is useful for file formats with no headers such as MPEG. This
        /// function also computes the real frame rate in case of mpeg2 repeat
        /// frame mode.
        /// The logical file position is not changed by this function;
        /// examined packets may be buffered for later processing.
        /// </summary>
        /// <param name="pAVFormatContext">The media file handle</param>
        /// <returns>>=0 if OK.  AVError otherwise</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_find_stream_info(ref AVFormatContext pAVFormatContext);

        /// <summary>
        ///Read a transport packet from a media file.
        /// </summary>
        /// <param name="pAVFormatContext">Media file handle</param>
        /// <param name="pAVPacket">packet to be filled</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        [Obsolete("This function is obsolete and should never be used. Use av_read_frame() instead.")]
        public static extern AVError av_read_packet(ref AVFormatContext pAVFormatContext, ref AVPacket pAVPacket);

        /// <summary>
        /// Return the next frame of a stream.
        ///
        /// The returned packet is valid
        /// until the next av_read_frame() or until av_close_input_file() and
        /// must be freed with av_free_packet. For video, the packet contains
        /// exactly one frame. For audio, it contains an integer number of
        /// frames if each frame has a known fixed size (e.g. PCM or ADPCM
        /// data). If the audio frames have a variable size (e.g. MPEG audio),
        /// then it contains one frame.
        ///
        /// pkt->pts, pkt->dts and pkt->duration are always set to correct
        /// values in AVStream.timebase units (and guessed if the format cannot
        /// provided them). pkt->pts can be AV_NOPTS_VALUE if the video format
        /// has B frames, so it is better to rely on pkt->dts if you do not
        /// decompress the payload.
        /// </summary>
        /// <returns>0 if OK, < 0 if error or end of file.</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_read_frame(ref AVFormatContext pAVFormatContext, ref AVPacket pAVPacket);

        /// <summary>
        /// Seek to the key frame at timestamp in stream_index
        /// </summary>
        /// <param name="stream_index">If stream_index is (-1), a default
        /// stream is selected, and timestamp is automatically converted 
        /// from AV_TIME_BASE units to the stream specific time_base.</param>
        /// <param name="timestamp">timestamp in AVStream.time_base units
        /// or if there is no stream specified then in AV_TIME_BASE units</param>
        /// <param name="flags">Flags which select direction and seeking mode.</param>
        /// <returns>>=0 on success</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_seek_frame(ref AVFormatContext pAVFormatContext, int stream_index, long timestamp, AVSEEK_FLAG flags);

        /// <summary>
        /// Start playing a network based stream (e.g. RTSP stream) at the
        /// current position
        /// </summary>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_read_play(ref AVFormatContext pAVFormatContext);

        /// <summary>
        /// Pause a network based stream (e.g. RTSP stream).
        /// 
        /// Use av_read_play() to resume it.
        /// </summary>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_read_pause(ref AVFormatContext pAVFormatContext);

        /// <summary>
        /// Close a media file (but not its codecs).
        /// </summary>
        /// <param name="pAVFormatContext">Media file handle</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_close_input_file(ref AVFormatContext pAVFormatContext);

        /// <summary>
        /// Add a new stream to a media file.
        /// 
        /// Can only be called in the read_header() function.  If the flag
        /// AVFMTCTX_NOHEADER is in the format context, then new streams
        /// can be added in read_packet too.
        /// </summary>
        /// <param name="pAVFormatContext">Media file handle</param>
        /// <param name="id">File format dependent stream id</param>
        /// <returns>AVStream pointer</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVStream* av_new_stream(ref AVFormatContext pAVFormatContext, int id);

        /// <summary>
        /// Set the pts for a given stream.
        /// </summary>
        /// <param name="pAVStream">Stream</param>
        /// <param name="pts_wrap_bits">Number of bits effectively used by the pts
        /// (used for wrap control, 33 is the value for MPEG)</param>
        /// <param name="pts_num">numerator to convert to seconds (MPEG: 1)</param>
        /// <param name="pts_den">denominator to convert to seconds (MPEG: 90000)</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_set_pts_info(ref AVStream pAVStream, int pts_wrap_bits, int pts_num, int pts_den);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_find_default_stream_index(ref AVFormatContext pAVFormatContext);

        /// <summary>
        /// Gets the index for a specific timestamp.
        /// </summary>
        /// <param name="pAVStream"></param>
        /// <param name="timestamp"></param>
        /// <param name="flags">if AVSEEK_FLAG_BACKWARD then the returned index will correspond to
        /// the timestamp which is &lt= the requested one, if backward is 0 then it will be &gt=
        /// if AVSEEK_FLAG_ANY seek to any frame, only keyframes otherwise</param>
        /// <returns>&lt 0 if no such timestamp could be found</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_index_search_timestamp(ref AVStream pAVStream, long timestamp, AVSEEK_FLAG flags);

        /// <summary>
        /// Add a index entry into a sorted list updateing if it is already there.
        /// </summary>
        /// <param name="timestamp">timestamp in the timebase of the given stream</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_add_index_entry(ref AVStream pAVStream, long pos, long timestamp, int size, int distance, AVSEEK_FLAG flags);

        /// <summary>
        /// Does a binary search using av_index_search_timestamp() and AVCodec.read_timestamp().
        /// This is not supposed to be called directly by a user application, but by demuxers.
        /// </summary>
        /// <param name="stream_index">stream number</param>
        /// <param name="target_ts">target timestamp in the time base of the given stream</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_seek_frame_binary(ref AVFormatContext pAVFormatContext, int stream_index, long target_ts, AVSEEK_FLAG flags);

        /// <summary>
        /// Updates cur_dts of all streams based on given timestamp and AVStream.
        /// </summary>
        /// <param name="pAVFormatContext"></param>
        /// <param name="pAVStream">reference stream giving time_base of param timestamp
        /// unchanged, others set cur_dts in their native timebase
        /// only needed for timestamp wrapping or if (dts not set and pts!=dts).</param>
        /// <param name="timestamp">new dts expressed in time_base of param ref_st</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_update_cur_dts(ref AVFormatContext pAVFormatContext, ref AVStream pAVStream, long timestamp);

        /* media file output */
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_set_parameters(ref AVFormatContext pAVFormatContext, AVFormatParameters* pAVFormatParameters);

        /// <summary>
        /// Allocate the stream private data and write the stream header to an
        /// output media file.
        /// </summary>
        /// <param name="pAVFormatContext">Media file handle</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError av_write_header(ref AVFormatContext pAVFormatContext);

        /// <summary>
        /// Write a packet to an output media file.
        ///
        /// The packet shall contain one audio or video frame.
        /// The packet must be correctly interleaved according to the container specification,
        /// if not then av_interleaved_write_frame must be used
        /// </summary>
        /// <param name="pAVFormatContext">media file handle</param>
        /// <param name="pAVPacket">the packet, which contains the stream_index, buf/buf_size, dts/pts, ...</param>
        /// <returns>1 if end of stream wanted, otherwise AVError</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_write_frame(ref AVFormatContext pAVFormatContext, ref AVPacket pAVPacket);

        /// <summary>
        /// Writes a packet to an output media file ensuring correct interleaving.
        ///
        /// The packet must contain one audio or video frame.
        /// If the packets are already correctly interleaved the application should
        /// call av_write_frame() instead as it is slightly faster. It is also important
        /// to keep in mind that completely non-interleaved input will need huge amounts
        /// of memory to interleave with this, so it is preferable to interleave at the
        /// demuxer level.
        /// </summary>
        /// <param name="pAVFormatContext">media file handle</param>
        /// <param name="pAVPacket">the packet, which contains the stream_index, buf/buf_size, dts/pts, ...</param>
        /// <returns>1 if end of stream wanted, otherwise AVError</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_interleaved_write_frame(ref AVFormatContext pAVFormatContext, ref AVPacket pAVPacket);

        /// <summary>
        /// Interleave a packet per DTS in an output media file.
        ///
        /// Packets with pkt->destruct == av_destruct_packet will be freed inside this function,
        /// so they cannot be used after it, note calling av_free_packet() on them is still safe.
        /// </summary>
        /// <param name="pAVFormatContext">media file handle</param>
        /// <param name="p_out_AVPacket">the interleaved packet will be output here</param>
        /// <param name="pAVPacket">the input packet</param>
        /// <param name="flush">true if no further packets are available as input and all
        /// remaining packets should be output</param>
        /// <returns>1 if end of stream wanted, otherwise AVError</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_interleave_packet_per_dts(ref AVFormatContext pAVFormatContext, ref AVPacket p_out_AVPacket,
                                                              ref AVPacket pAVPacket, [MarshalAs(UnmanagedType.Bool)]bool flush);

        /// <summary>
        /// Write the stream trailer to an output media file and
        /// free the file private data.
        /// </summary>
        /// <param name="pAVFormatContext">media file handle</param>
        /// <returns>0 if OK. AVERROR_xxx if error.</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_write_trailer(ref AVFormatContext pAVFormatContext);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void dump_format(ref AVFormatContext pAVFormatContext, int index, string url, int is_output);

        /// <summary>
        /// parses width and height out of string str.
        /// </summary>
        [Obsolete("Use av_parse_video_frame_size instead.")]
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int parse_image_size(ref int width_ptr, ref int height_ptr, string arg);

        /// <summary>
        /// Converts frame rate from string to a fraction.
        /// </summary>
        [Obsolete("Use av_parse_video_frame_rate instead.")]
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int parse_frame_rate(ref int pFrame_rate, ref int pFrame_rate_base, string arg);

        /// <summary>
        /// Converts date string to number of seconds since Jan 1st, 1970.
        /// </summary>
        /// <code>
        /// Syntax:
        /// - If not a duration:
        ///  [{YYYY-MM-DD|YYYYMMDD}]{T| }{HH[:MM[:SS[.m...]]][Z]|HH[MM[SS[.m...]]][Z]}
        /// Time is localtime unless Z is suffixed to the end. In this case GMT
        /// Return the date in micro seconds since 1970
        ///
        /// - If a duration:
        ///  HH[:MM[:SS[.m...]]]
        ///  S+[.m...]
        /// </code>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long parse_date(string datestr, int duration);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long av_gettime();

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long ffm_read_write_index(int fd);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void ffm_write_write_index(int fd, long pos);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void ffm_set_write_index(ref AVFormatContext pAVFormatContext, long pos, long file_size);

        /// <summary>
        /// Attempts to find a specific tag in a URL.
        /// 
        /// syntax: '?tag1=val1&tag2=val2...'. Little URL decoding is done.
        /// </summary>
        /// <returns>1 if found</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int find_info_tag(string arg, int arg_size, string tag1, string info);

        /// <summary>
        /// Returns in 'buf' the path with '%d' replaced by number.
        /// 
        /// Also handles the '%0nd' format where 'n' is the total number
        /// of digits and '%%'.
        /// </summary>
        /// <param name="buf">destination buffer</param>
        /// <param name="buf_size">destination buffer size</param>
        /// <param name="path">numbered sequence string</param>
        /// <param name="number">frame number</param>
        /// <returns>0 if OK, -1 if format error.</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_get_frame_filename([In, Out] StringBuilder buf, int buf_size,
                                                        string path, int number);

        /// <summary>
        /// Check whether filename actually is a numbered sequence generator.
        /// </summary>
        /// <param name="filename">possible numbered sequence string</param>
        /// <returns>true if valid numbered sequence string, false otherwise</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool av_filename_number_test(string filename);

        /// <summary>
        /// Generate an SDP for an RTP session.
        /// </summary>
        /// <param name="ac">
        /// Array of AVFormatContexts describing the RTP streams. If the
        /// array is composed by only one context, such context can contain
        /// multiple AVStreams (one AVStream per RTP stream). Otherwise,
        /// all the contexts in the array (an AVCodecContext per RTP stream)
        /// must contain only one AVStream.
        /// </param> 
        /// <param name="n_files">Number of AVCodecContexts contained in ac</param> 
        /// <param name="buff">Buffer where the SDP will be stored</param> 
        /// <param name="size">Size of the buffer</param> 
        /// <returns>0 if OK. AVERROR_xxx if error.</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError avf_sdp_create(AVFormatContext** ac, int n_files, [In,Out]StringBuilder buff, int size);

        #endregion

        #region Constants

        public const int AV_TIME_BASE = 1000000;

        public const int AVFMT_INFINITEOUTPUTLOOP = 0;

        public const uint AVFMT_FLAG_GENPTS = 0x0001;

        public const uint AVFMT_FLAG_IGNIDX = 0x0002;

        public const int AVFMT_NOOUTPUTLOOP = -1;

        // no file should be opened
        public const uint AVFMT_NOFILE = 0x0001;

        // needs '%d' in filename
        public const uint AVFMT_NEEDNUMBER = 0x0002;

        // show format stream IDs numbers
        public const uint AVFMT_SHOW_IDS = 0x0008;

        // format wants AVPicture structure for raw picture data 
        public const uint AVFMT_RAWPICTURE = 0x0020;

        // format wants global header
        public const uint AVFMT_GLOBALHEADER = 0x0040;

        // format doesnt need / has any timestamps
        public const uint AVFMT_NOTIMESTAMPS = 0x0080;

        // AVImageFormat.flags field constants
        public const uint AVIMAGE_INTERLEAVED = 0x0001;

        public const int AVPROBE_SCORE_MAX = 100;

        public const int PKT_FLAG_KEY = 0x0001;

        public const int AVINDEX_KEYFRAME = 0x001;

        public const int MAX_REORDER_DELAY = 4;

        public const uint AVFMTCTX_NOHEADER = 0x001;

        public const int MAX_STREAMS = 20;

        public const int AVSEEK_FLAG_BACKWARD = 1;
        public const int AVSEEK_FLAG_BYTE = 2;
        public const int AVSEEK_FLAG_ANY = 4;

        public const int FFM_PACKET_SIZE = 4096;

        #endregion
    }
}
