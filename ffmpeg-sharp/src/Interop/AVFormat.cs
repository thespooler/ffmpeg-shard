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
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Format;
using FFmpegSharp.Interop.Util;
using FFmpegSharp.Interop.Format.Input;
using FFmpegSharp.Interop.Format.Output;
using FFmpegSharp.Interop.Format.Context;

namespace FFmpegSharp.Interop
{
    [SuppressUnmanagedCodeSecurity]
    public unsafe partial class FFmpeg
    {

        public const string AVFORMAT_DLL_NAME = "avformat-54.dll";

        #region Functions

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError avformat_open_input(out IntPtr ps, string filename, AVInputFormat* fmt, AVDictionary* options);

        /// <summary>
        /// Allocate and read the payload of a packet and initialize its fields to defalut values
        /// </summary>
        /// <param name="pByteIOContext"></param>
        /// <param name="pAVPacket">packet</param>
        /// <param name="size">wanted payload size</param>
        /// <returns>>0 (read size) if OK.  AVError otherwise</returns>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_get_packet(ref AVIOContext s, ref AVPacket pkt, int size);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_register_input_format(ref AVInputFormat pAVInputFormat);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_register_output_format(ref AVOutputFormat pAVOutputFormat);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOutputFormat* guess_format(string short_name, string filename,
                                                          string mime_type);

        /// <summary>
        /// Guesses the codec id based upon muxer and filename
        /// </summary>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVCodecID av_guess_codec(ref AVOutputFormat pAVOutoutFormat, string short_name,
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
        public static extern void av_pkt_dump2(IntPtr pFile, ref AVPacket pAVPacket, [MarshalAs(UnmanagedType.Bool)] bool dump_payload, ref AVStream stream);

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

        /// <summary>
        /// Opens a media file as input.  The codecs are not opened.  Only the file
        /// header (if present) is read.
        /// </summary>
        /// <param name="pFormatContext">the opened media file handle is put here</param>
        /// <param name="filename">filename to open</param>
        /// <returns>AVError</returns>
        public static AVError avformat_open_input_file(out AVFormatContext pFormatContext, string filename)
        {
            IntPtr ptr;
            AVError err = avformat_open_input(out ptr, filename, null, null);

            if (ptr == IntPtr.Zero)
            {
                pFormatContext = new AVFormatContext();
                return err;
            }

            pFormatContext = *(AVFormatContext*)ptr.ToPointer();

            return err;
        }
        
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avformat_close_input(ref IntPtr ppAvFormatCtx);
        
        public unsafe static void avformat_close_input(ref AVFormatContext s)
        {
            fixed (AVFormatContext* avc = &s)
            {
                IntPtr ptr = (IntPtr)avc;
                avformat_close_input(ref ptr);
            }
        }

        /// <summary>
        /// Read packets of a media file to get stream information. This
        /// is useful for file formats with no headers such as MPEG. This
        /// function also computes the real framerate in case of MPEG-2 repeat
        /// frame mode.
        /// The logical file position is not changed by this function;
        /// examined packets may be buffered for later processing.
        /// </summary>
        /// <param name="pAVFormatContext">media file handle</param>
        /// <param name="options">If non-NULL, an ic.nb_streams long array of pointers to
        /// dictionaries, where i-th member contains options for
        /// codec corresponding to i-th stream. 
        /// On return each dictionary will be filled with options that were not found.
        /// </param>
        /// <returns>>=0 if OK, AVERROR_xxx on error</returns>
        /// <remarks>
        /// this function isn't guaranteed to open all the codecs, so
        /// options being non-empty at return is a perfectly normal behavior.
        /// 
        /// @todo Let the user decide somehow what information is needed so that
        /// we do not waste time getting stuff the user does not need.
        /// </remarks>
         
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError avformat_find_stream_info(ref AVFormatContext pAVFormatContext, AVDictionary** options);

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
        [Obsolete]
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
        public static extern void av_set_pts_info(ref AVStream pAVStream, int pts_wrap_bits, uint pts_num, uint pts_den);

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
        /// Updates cur_dts of all streams based on given timestamp and AVStream.
        /// </summary>
        /// <param name="pAVFormatContext"></param>
        /// <param name="pAVStream">reference stream giving time_base of param timestamp
        /// unchanged, others set cur_dts in their native timebase
        /// only needed for timestamp wrapping or if (dts not set and pts!=dts).</param>
        /// <param name="timestamp">new dts expressed in time_base of param ref_st</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_update_cur_dts(ref AVFormatContext pAVFormatContext, ref AVStream pAVStream, long timestamp);

        /// <summary>
        /// Allocate the stream private data and write the stream header to an
        /// output media file.
        /// </summary>
        /// <param name="pAVFormatContext">Media file handle</param>
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVError avformat_write_header(ref AVFormatContext pAVFormatContext, AVDictionary** options);

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
        public static extern void av_dump_format(ref AVFormatContext pAVFormatContext, int index, string url, int is_output);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long av_gettime();

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

        [Flags]
        public enum AVSeekFlag: int
        {
            AVSEEK_FLAG_BACKWARD = 1,
            AVSEEK_FLAG_BYTE = 2,
            AVSEEK_FLAG_ANY = 4,
            AVSEEK_FLAG_FRAME = 8
        }

        #endregion
    }
}
