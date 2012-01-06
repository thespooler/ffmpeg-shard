#region LGPL License
//
// AVCodec.cs
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
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Interop
{
    [SuppressUnmanagedCodeSecurity]
    public unsafe partial class FFmpeg
    {
        public const string AVCODEC_DLL_NAME = "avcodec-51.dll";

        #region "Functions"

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern ReSampleContext* audio_resample_init(int output_channels, int input_channels,
                                                    int output_rate, int input_rate);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int audio_resample(ref ReSampleContext pResampleContext, [In, Out]short[] output, [In, Out]short[] intput, int nb_samples);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void audio_resample_close(ref ReSampleContext pResampleContext);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVResampleContext* av_resample_init(int out_rate, int in_rate, int filter_length, int log2_phase_count, int linear, double cutoff);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_resample(ref AVResampleContext pAVResampleContext, [In, Out]short[] dst, [In, Out]short[] src,
                                              ref int consumed, int src_size, int dst_size, int udpate_ctx);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_resample_compensate(ref AVResampleContext pAVResampleContext, int sample_delta, int compensation_distance);

        /// <summary>
        /// Don't call this method, use AVResampleContext.Dispose();
        /// </summary>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_resample_close(ref AVResampleContext pAVResampleContext);

#if LIBAVCODEC_51
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern ImgReSampleContext* img_resample_init(int output_width, int output_height,
                                      int input_width, int input_height);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern ImgReSampleContext* img_resample_full_init(int owidth, int oheight,
                                      int iwidth, int iheight,
                                      int topBand, int bottomBand,
                                      int leftBand, int rightBand,
                                      int padtop, int padbottom,
                                      int padleft, int padright);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void img_resample(ref ImgReSampleContext pImgReSampleContext, ref AVPicture output, ref AVPicture input);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void img_resample_close(ref ImgReSampleContext pImgReSampleContext);

#endif

        /// <summary>
        /// Allocate memory for a picture.  Call avpicture_free to free it.
        /// </summary>
        /// <param name="picture">the picture to be filled in</param>
        /// <param name="pix_fmt">the format of the picture</param>
        /// <param name="width">the width of the picture</param>
        /// <param name="height">the height of the picture</param>
        /// <returns>0 on success, &lt0 if invalid</returns>        
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avpicture_alloc(out AVPicture picture, PixelFormat pix_fmt, int width, int height);

        /// <summary>
        /// Free a picture previously allocated by avpicture_alloc()
        /// </summary>
        /// <param name="pAVPicture"></param>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avpicture_free(ref AVPicture pAVPicture);

        /// <summary>
        /// Fill in the AVPicture fields.
        /// The fields of the given AVPicture are filled in by using the 'ptr' address
        /// which points to the image data buffer. Depending on the specified picture
        /// format, one or multiple image data pointers and line sizes will be set.
        /// If a planar format is specified, several pointers will be set pointing to
        /// the different picture planes and the line sizes of the different planes
        /// will be stored in the lines_sizes array.
        /// </summary>
        /// <param name="pAVPicture">AVPicture whose field are to be filled in</param>
        /// <param name="ptr">Buffer which will contain or contains the actual image data</param>
        /// <param name="pix_fmt">The format in which the picture data is stored</param>
        /// <param name="width">The width of the image in pixels </param>
        /// <param name="height">The height of the image in pixels</param>
        /// <returns>Size of the image data in bytes</returns> 
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avpicture_fill(out AVPicture pAVPicture, byte[] ptr, PixelFormat pix_fmt, int width, int height);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avpicture_layout(AVPicture* src, PixelFormat pix_fmt, int width, int height,
                                                  [In,Out]byte[] dest, int dest_size);

        /// <summary>
        /// Calculate the size in bytes that a picture of the given width and height
        /// would occupy in stored in the given picture format
        /// </summary>
        /// <param name="pix_fmt">the given picture format</param>
        /// <param name="width">the width of the image</param>
        /// <param name="height">the height of the image</param>
        /// <returns>Image data size in bytes</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avpicture_get_size(PixelFormat pix_fmt, int width, int height);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_get_chroma_sub_sample(PixelFormat pix_fmt, out int h_shift, out int v_shift);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string avcodec_get_pix_fmt_name(PixelFormat pix_fmt);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_set_dimensions(ref AVCodecContext pAVCodecContext, int width, int height);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern PixelFormat avcodec_get_pix_fmt(string name);

        /// <returns>Returns a fourcc value</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint avcodec_pix_fmt_to_codec_tag(PixelFormat p);

        /// <summary>
        /// Computes what kind of losses will occur when converting from one specific
        /// pixel format to another.
        /// When converting from one pixel format to another, information loss may occur.
        /// For example, when converting from RGB24 to GRAY, the color information will
        /// be lost. Similarly, other losses occur when converting from some formats to
        /// other formats. These losses can involve loss of chroma, but also loss of
        /// resolution, loss of color depth, loss due to the color space conversion, loss
        /// of the alpha bits or loss due to color quantization.
        /// avcodec_get_fix_fmt_loss() informs you about the various types of losses
        /// which will occur when converting from one pixel format to another.
        /// </summary>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern FF_LOSS avcodec_get_pix_fmt_loss(int dst_pix_fmt, int src_pix_fmt, int has_alpha);

        /// <summary>
        /// Finds the best pixel format to convert to given a certain source pixel
        /// format.  When converting from one pixel format to another, information loss
        /// may occur.  For example, when converting from RGB24 to GRAY, the color
        /// information will be lost. Similarly, other losses occur when converting from
        /// some formats to other formats. avcodec_find_best_pix_fmt() searches which of
        /// the given pixel formats should be used to suffer the least amount of loss.
        /// The pixel formats from which it chooses one, are determined by the
        /// pix_fmt_mask parameter.
        ///
        /// <code>
        /// src_pix_fmt = PIX_FMT_YUV420P;
        /// pix_fmt_mask = (1 &lt&lt PIX_FMT_YUV422P) || (1 &lt&lt PIX_FMT_RGB24);
        /// dst_pix_fmt = avcodec_find_best_pix_fmt(pix_fmt_mask, src_pix_fmt, alpha, &loss);
        /// </code>
        /// </summary>
        /// <param name="pix_fmt_mask">bitmask determining which pixel format to choose from</param>
        /// <param name="src_pix_fmt">source pixel format</param>
        /// <param name="has_alpha">Whether the source pixel format alpha channel is used.</param>
        /// <param name="loss_ptr">Combination of flags informing you what kind of losses will occur.</param>
        /// <returns>The best pixel format to convert to or -1 if none was found.</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_find_best_pix_fmt(int pix_fmt_mask, int src_pix_fmt, int has_alpha, out int loss_ptr);

        /// <summary>
        /// Print in buf the string corresponding to the pixel format with
        /// number pix_fmt, or an header if pix_fmt is negative.
        /// </summary>
        /// <param name="buf">the buffer where to write the string</param>
        /// <param name="buf_size">the size of buf</param>
        /// <param name="pix_fmt">
        /// the number of the pixel format to print the corresponding info string, or
        /// a negative value to print the corresponding header.
        /// </param>
        /// <remarks>
        /// Meaningful values for obtaining a pixel format info vary from 0 to PIX_FMT_NB -1.
        /// </remarks>
        [DllImport(AVCODEC_DLL_NAME)]
        public static extern void avcodec_pix_fmt_string(char* buf, int buf_size, PixelFormat pix_fmt);

        /// <summary>
        /// Tell if an image really has transparent alpha values.
        /// </summary>
        /// <returns>ored mask of FF_ALPHA_xxx constants</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern FF_ALPHA img_get_alpha_info(ref AVPicture pAVPicture, PixelFormat pix_fmt, int width, int height);

#if LIBAVCODEC_51
        /// <summary>
        /// Convert among pixel formats
        /// </summary>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int img_convert(ref AVPicture dst, PixelFormat dst_pix_fmt,
                                             ref AVPicture src, PixelFormat src_pix_fmt,
                                             int width, int height);
#endif

        /// <summary>
        /// Deinterlace a picture
        /// </summary>
        /// <returns>If not supported, -1</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avpicture_deinterlace(ref AVPicture dst, ref AVPicture src,
                            int pix_fmt, int width, int height);

        /// <summary>
        /// returns LIBAVCODEC_VERSION_INT constant
        /// </summary>
        /// <returns></returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint avcodec_version();

        /// <summary>
        /// returns LIBAVCODEC_BUILD constant
        /// </summary>
        /// <returns></returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern uint avcodec_build();

        /// <summary>
        /// Initializes libavcodec
        /// 
        /// Warning: This function must be called before any other libavcodec function.
        /// </summary>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_init();

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void register_avcodec(ref AVCodec pAVCodec);

        /// <summary>
        /// Finds an encoder with a matching Codec ID
        /// </summary>
        /// <param name="id">CodecID of the requested encoder</param>
        /// <returns>An encoder if one was found, NULL otherwise</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVCodec* avcodec_find_encoder(CodecID id);

        /// <summary>
        /// Finds an encoder with the specified name.
        /// </summary>
        /// <param name="name">Name of the requested encoder</param>
        /// <returns>An encoder if one was found, NULL otherwise</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVCodec* avcodec_find_encoder_by_name(string name);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVCodec* avcodec_find_decoder(CodecID id);

        /// <summary>
        /// Finds a decoder with the specified name.
        /// </summary>
        /// <param name="name">Name of the requested decoder</param>
        /// <returns>A decoder if one was found, NULL otherwise</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVCodec* avcodec_find_decoder_by_name(string mame);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_string(string name, int buf_size,
                    ref AVCodecContext pAVCodeContext, int encode);

        /// <summary>
        /// Sets the fiels of the given AVCodecContext to default values.
        /// </summary>
        /// <param name="pAVCodecContext">
        /// The AVCodecContext of which the fields should be set to default values.
        /// </param>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_get_context_defaults(ref AVCodecContext pAVCodecContext);

        /// <summary>
        /// Allocates an AVCodecContext and sets its fields to default values.
        /// </summary>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVCodecContext* avcodec_alloc_context();

        /// <summary>
        /// Sets the fields of the given AVFrame to default values.
        /// </summary>
        /// <param name="pAVFrame"></param>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_get_frame_defaults(ref AVFrame pAVFrame);

        /// <summary>
        /// Allocates a AVFrame and sets its fields to default values.
        /// </summary>
        [DllImport(AVCODEC_DLL_NAME)]
        public static extern AVFrame* avcodec_alloc_frame();

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_default_get_buffer(ref AVCodecContext pAVCodecContext, ref AVFrame pAVFrame);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_default_release_buffer(ref AVCodecContext pAVCodecContext, ref AVFrame pAVFrame);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_default_reget_buffer(ref AVCodecContext pAVCodecContext, ref AVFrame pAVFrame);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_align_dimensions(ref AVCodecContext pAVCodecContext, ref int width, ref int height);

        /// <summary>
        /// Checks if the given dimensions of a picture is valid, meaning that all
        /// bytes of the picture can be address with a signed int
        /// </summary>
        /// <param name="av_log_ctx">A pointer to an arbitrary struct of which the first field is a 
        /// pointer to an AVClass struct</param>
        /// <param name="width">Width of the picture</param>
        /// <param name="height">Height of the picture</param>
        /// <returns>Zero if valid, a negative value if invalid</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_check_dimensions(object av_log_ctx, uint width, uint height);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern PixelFormat avcodec_default_get_format(ref AVCodecContext pAVCodecContext, ref PixelFormat fmt);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_thread_init(ref AVCodecContext pAVCodecContext, int thread_count);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_thread_free(ref AVCodecContext pAVCodecContext);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_thread_execute(ref AVCodecContext pAVCodecContext,
                                [MarshalAs(UnmanagedType.FunctionPtr)]FuncCallback func,
                                void** arg, ref int ret, int count);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_default_execute(ref AVCodecContext pAVCodecContext,
                               [MarshalAs(UnmanagedType.FunctionPtr)]FuncCallback func,
                               void** arg, ref int ret, int count);

        /// <summary>
        /// Initializes the AVCodecContext to use the given AVCodec. Prior to using this
        /// function the context has to be allocated.
        /// </summary>
        /// <remarks>
        /// The functions avcodec_find_decoder_by_name(), avcodec_find_encoder_by_name(),
        /// avcodec_find_decoder() and avcodec_find_encoder() provide an easy way for
        /// retrieving a codec.
        /// </remarks>
        /// <warning>This function is not thread safe!</warning>
        /// <example><code>
        /// avcodec_register_all();
        /// codec = avcodec_find_decoder(CODEC_ID_H264);
        /// if (!codec)
        ///     exit(1);
        ///
        /// context = avcodec_alloc_context();
        ///
        /// if (avcodec_open(context, codec) &lt 0)
        ///     exit(1);
        /// </code></example>
        /// <param name="pAVCodecContext">The context which will be set up to use the given codec.</param>
        /// <param name="pAVCodec">The codec to use within the context.</param>
        /// <returns>Zero on success, a negative value on error</returns>
        /// <seealso cref="avcodec_alloc_context"/>
        /// <seealso cref="avcodec_find_decoder"/>
        /// <seealso cref="avcodec_find_encoder"/>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_open(ref AVCodecContext pAVCodecContext, AVCodec* pAVCodec);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        [Obsolete("Depreciated, use avcodec_decode_audio2() instead")]
        public static extern int avcodec_decode_audio(ref AVCodecContext pAVCodecContext, [In, Out]short[] samples,
                                                        ref int frame_size_ptr, byte[] buf, int buf_size);

        /// <summary>
        /// Decodes an audio frame from buf into samples.
        /// The avcodec_decode_audio2() function decodes an audio frame from the input
        /// buffer buf of size buf_size. To decode it, it makes use of the
        /// audio codec which was coupled with avctx using avcodec_open(). The
        /// resulting decoded frame is stored in output buffer samples.  If no frame
        /// could be decompressed, frame_size_ptr is zero. Otherwise, it is the
        /// decompressed frame size in bytes.
        /// </summary>
        /// 
        /// <warning>
        /// You must set frame_size_ptr to the allocated size of the
        /// output buffer before calling avcodec_decode_audio2().
        /// </warning>
        ///
        /// <warning>
        /// The input buffer must be FF_INPUT_BUFFER_PADDING_SIZE larger than
        /// the actual read bytes because some optimized bitstream readers read 32 or 64
        /// bits at once and could read over the end.
        /// </warning>
        ///
        /// <warning>
        /// The end of the input buffer buf should be set to 0 to ensure that
        /// no overreading happens for damaged MPEG streams.
        /// </warning>
        ///
        /// <remarks>
        /// You might have to align the input buffer buf and output buffer \p
        /// samples. The alignment requirements depend on the CPU: On some CPUs it isn't
        /// necessary at all, on others it won't work at all if not aligned and on others
        /// it will work but it will have an impact on performance. In practice, the
        /// bitstream should have 4 byte alignment at minimum and all sample data should
        /// be 16 byte aligned unless the CPU doesn't need it (AltiVec and SSE do). If
        /// the linesize is not a multiple of 16 then there's no sense in aligning the
        /// start of the buffer to 16.
        /// </remarks>
        /// <param name="pAVCodecContext">The codec context</param>
        /// <param name="samples">The output buffer.</param>
        /// <param name="frame_size_ptr">The used output buffer size in bytes (must be 
        /// initialized to the size of the output buffer in bytes)</param>
        /// <param name="buf">The input buffer</param>
        /// <param name="buf_size">The input buffer size in bytes</param>
        /// <returns>On error a negative value is returned,otherwise the number of bytes 
        /// used or zero if no frame could be decompressed</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_decode_audio2(ref AVCodecContext avctx, [In, Out]short[] samples,
                                                        ref int frame_size_ptr, byte[] buf, int buf_size);

        /// <summary>
        /// Decodes a video frame from buf into picture.
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public unsafe static extern int avcodec_decode_audio2(ref AVCodecContext avctx, short* samples,
                                                        ref int frame_size_ptr, byte* buf, int buf_size);

        /// <summary>Decodes a video frame from buf into picture.
        /// The avcodec_decode_video() function decodes a video frame from the input
        /// buffer buf of size buf_size. To decode it, it makes use of the
        /// video codec which was coupled with avctx using avcodec_open(). The
        /// resulting decoded frame is stored in picture.
        /// </summary>
        /// 
        /// <warning>The input buffer must be FF_INPUT_BUFFER_PADDING_SIZE larger than
        /// the actual read bytes because some optimized bitstream readers read 32 or 64
        /// bits at once and could read over the end.
        /// </warning>
        /// 
        /// <warning>
        /// The end of the input buffer buf must be set to 0 to ensure that
        /// no overreading happens for damaged MPEG streams.
        /// </warning>
        /// 
        /// <remarks> 
        /// You might have to align the input buffer buf and output buffer \p
        /// samples. The alignment requirements depend on the CPU: on some CPUs it isn't
        /// necessary at all, on others it won't work at all if not aligned and on others
        /// it will work but it will have an impact on performance. In practice, the
        /// bitstream should have 4 byte alignment at minimum and all sample data should
        /// be 16 byte aligned unless the CPU doesn't need it (AltiVec and SSE do). If
        /// the linesize is not a multiple of 16 then there's no sense in aligning the
        /// start of the buffer to 16.
        /// </remarks>
        /// 
        /// <param name="pAVCodecContext">The codec context</param>
        /// <param name="pAVFrame">The AVFrame in which the decoded video frame will be stored</param>
        /// <param name="got_picture_ptr">True if a frame could be decompressed</param>
        /// <param name="buf">The input buffer</param>
        /// <param name="buf_size">The size of the input buffer in bytes</param>
        /// <returns>
        /// On error a negative value is returned, otherwise the number of bytes
        /// used or zero if no frame could be decompressed.
        /// </returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_decode_video(ref AVCodecContext pAVCodecContext, ref AVFrame pAVFrame,
                                                      [MarshalAs(UnmanagedType.Bool)]out bool got_picture_ptr,
                                                      byte* buf, int buf_size);

        /// <summary>Decodes a video frame from buf into picture.
        /// The avcodec_decode_video() function decodes a video frame from the input
        /// buffer buf of size buf_size. To decode it, it makes use of the
        /// video codec which was coupled with avctx using avcodec_open(). The
        /// resulting decoded frame is stored in picture.</summary>
        /// 
        /// <warning>The input buffer must be FF_INPUT_BUFFER_PADDING_SIZE larger than
        /// the actual read bytes because some optimized bitstream readers read 32 or 64
        /// bits at once and could read over the end.</warning>
        /// 
        /// <warning>The end of the input buffer buf must be set to 0 to ensure that
        /// no overreading happens for damaged MPEG streams.</warning>
        /// 
        /// <remarks> You might have to align the input buffer buf and output buffer \p
        /// samples. The alignment requirements depend on the CPU: on some CPUs it isn't
        /// necessary at all, on others it won't work at all if not aligned and on others
        /// it will work but it will have an impact on performance. In practice, the
        /// bitstream should have 4 byte alignment at minimum and all sample data should
        /// be 16 byte aligned unless the CPU doesn't need it (AltiVec and SSE do). If
        /// the linesize is not a multiple of 16 then there's no sense in aligning the
        /// start of the buffer to 16.</remarks>
        /// 
        /// <param name="pAVCodecContext">The codec context</param>
        /// <param name="pAVFrame">The AVFrame in which the decoded video frame will be stored</param>
        /// <param name="got_picture_ptr">True if a frame could be decompressed</param>
        /// <param name="buf">The input buffer</param>
        /// <param name="buf_size">The size of the input buffer in bytes</param>
        /// <returns></returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_decode_video(ref AVCodecContext pAVCodecContext, AVFrame* pAVFrame,
                                                      [MarshalAs(UnmanagedType.Bool)]out bool got_picture_ptr,
                                                      byte* buf, int buf_size);

        /// <summary>
        /// Decode a subtitle message. If no subtitle could be decompressed,
        /// got_sub_ptr is zero. Otherwise, the subtitle is stored in *sub
        /// </summary>
        /// <param name="pAVCodecContext">The codec context</param>
        /// <param name="pAVSubtitle">The decoded subtitle it (got_sub_ptr) is true or null</param>
        /// <param name="got_sub_ptr">True if a subtitle could be decompressed</param>
        /// <param name="buf">The input buffer</param>
        /// <param name="buf_size">The input buffer's size in bytes</param>
        /// <returns>
        /// Return -1 if error, otherwise return the number of bytes used.
        /// </returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_decode_subtitle(ref AVCodecContext pAVCodecContext, ref AVSubtitle pAVSubtitle,
                                           [MarshalAs(UnmanagedType.Bool)]out bool got_sub_ptr, byte[] buf, int buf_size);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_parse_frame(ref AVCodecContext pAVCodecContext,
                                            ref byte[] pdata,
                                            ref int data_size_ptr, byte[] buf, int buf_size);

        /// <summary>
        /// Encodes an audio frame from samples into buf.
        /// The avcodec_encode_audio() function encodes an audio frame from the input
        /// buffer samples. To encode it, it makes use of the audio codec which was
        /// coupled with avctx using avcodec_open(). The resulting encoded frame is
        /// stored in output buffer buf.
        /// </summary>
        /// 
        /// <remarks>The output buffer should be at least FF_MIN_BUFFER_SIZE bytes large</remarks>
        /// <param name="pAVCodecContext">The codec context</param>
        /// <param name="buf">The output buffer</param>
        /// <param name="buf_size">The output buffer size</param>
        /// <param name="samples">The input buffer containing the samples</param>
        /// <returns>On error a negative value is returned, on success zero or the number 
        /// of bytes used from the input buffer.</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_encode_audio(ref AVCodecContext pAVCodecContext, [In, Out]byte[] buf, int buf_size,
                                            short[] samples);

        /// <summary>
        /// Encodes an audio frame from samples into buf.
        /// The avcodec_encode_audio() function encodes an audio frame from the input
        /// buffer samples. To encode it, it makes use of the audio codec which was
        /// coupled with avctx using avcodec_open(). The resulting encoded frame is
        /// stored in output buffer buf.
        /// </summary>
        /// 
        /// <remarks>The output buffer should be at least FF_MIN_BUFFER_SIZE bytes large</remarks>
        /// <param name="pAVCodecContext">The codec context</param>
        /// <param name="buf">The output buffer</param>
        /// <param name="buf_size">The output buffer size</param>
        /// <param name="samples">The input buffer containing the samples</param>
        /// <returns>On error a negative value is returned, on success zero or the number 
        /// of bytes used from the input buffer.</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_encode_audio(ref AVCodecContext pAVCodecContext, byte* buf, int buf_size, short* samples);

        /// <summary>
        /// Encodes a video frame from pict into buf.
        /// The avcodec_encode_video() function encodes a video frame from the input
        /// pict. To encode it, it makes use of the video codec which was coupled with
        /// avctx using avcodec_open(). The resulting encoded bytes representing the
        /// frame are stored in the output buffer buf. The input picture should be
        /// stored using a specific format, namely avctx.pix_fmt.
        /// </summary>
        /// <param name="pAVCodecContext">The codec context</param>
        /// <param name="buf">The output buffer for the bitstream of the encoded frame</param>
        /// <param name="buf_size">The size of the output buffer in bytes</param>
        /// <param name="pAVFrame">The input picture to encode</param>
        /// <returns>On error a negative value is returned, on success zero or the number
        /// of bytes used from the input buffer.</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_encode_video(ref AVCodecContext pAVCodecContext, [In, Out]byte[] buf, int buf_size,
                                            ref AVFrame pAVFrame);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_encode_subtitle(ref AVCodecContext pAVCodecContext, [In, Out]byte[] buf, int buf_size,
                                            ref AVSubtitle pAVSubtitle);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int avcodec_close(ref AVCodecContext pAVCodecContext);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_register_all();

        /// <summary>
        /// Flush buffers, should be called when seeking or when switching to a different stream 
        /// </summary>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_flush_buffers(ref AVCodecContext pAVCodecContext);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void avcodec_default_free_buffers(ref AVCodecContext pAVCodecContext);

        /// <summary>
        /// Returns a single letter to describe the given picture type
        /// </summary>
        /// <param name="pict_type">The picture type</param>
        /// <returns>A single character representing the picture type</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern char av_get_pict_type_char(PictureType pict_type);

        /// <summary>
        /// Returns the codec bits per sample
        /// </summary>
        /// <param name="codec_id">The codec</param>
        /// <returns>Number of bits per sample or zero if unknown for the given codec</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_get_bits_per_sample(CodecID codec_id);

        /// <summary>Returns sample format bits per sample.</summary>
        /// <param ref="sample_fmt">the sample format</param>
        /// <returns>Number of bits per sample or zero if unknown for the given sample format.</returns>
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_get_bits_per_sample_format(SampleFormat sample_fmt);

        [DllImport(AVCODEC_DLL_NAME)]
        public static extern AVCodecParser* av_parser_next(AVCodecParser* c);

        [DllImport(AVCODEC_DLL_NAME)]
        public static extern void av_register_codec_parser(ref AVCodecParser pAVcodecParser);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVCodecParserContext* av_parser_init(CodecID codec_id);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_parser_parse(ref AVCodecParserContext pAVCodecParserContext,
                                                ref AVCodecContext pAVCodecContext,
                            ref byte[] poutbuf, ref int poutbuf_size,
                            byte[] buf, int buf_size, long pts, long dts);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_parser_change(ref AVCodecParserContext pAVCodecParserContext,
                                                ref AVCodecContext pAVCodecContext,
                            ref byte[] poutbuf, ref int poutbuf_size,
                            byte[] buf, int buf_size, int keyframe);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_parser_close(ref AVCodecParserContext pAVCodecParserContext);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_register_bitstream_filter(ref AVBitStreamFilter pAVBitStreamFilter);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVBitStreamFilter av_bitstream_filter_init(
                                        string name);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_bitstream_filter_filter(ref AVBitStreamFilterContext pAVBitStreamFilterContext,
                                        ref AVCodec pAVCodecContext,
                                        string args,
                                        ref byte[] poutbuf,
                                        ref int poutbuf_size, byte[] buf, int buf_size, int keyframe);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_bitstream_filter_close(ref AVBitStreamFilterContext pAVBitStreamFilterContext);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVBitStreamFilter* av_bitstream_filter_next(AVBitStreamFilter* f);

#if !LIBAVCODEC_51
        ///<summary>Copy image 'src' to 'dst'.</summary>
        [DllImport(AVCODEC_DLL_NAME)]
        public static extern void av_picture_copy(ref AVPicture dst, ref AVPicture src,
                                                  int pix_fmt, int width, int height);

        ///<summary>Crop image top and left side.</summary>
        [DllImport(AVCODEC_DLL_NAME)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ZeroTrueBoolMarshaller))]
        public static extern bool av_picture_crop(ref AVPicture dst, ref AVPicture src,
                                                 int pix_fmt, int top_band, int left_band);

        ///<summary>Pad image.</summary>
        [DllImport(AVCODEC_DLL_NAME)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ZeroTrueBoolMarshaller))]
        public static extern bool av_picture_pad(ref AVPicture dst, ref AVPicture src, int height,
                                                int width, int pix_fmt, int padtop, int padbottom,
                                                int padleft, int padright, int[] color);
#else
        [DllImport(AVCODEC_DLL_NAME)]
        public static extern void img_copy(ref AVPicture dst, ref AVPicture src,
                            PixelFormat pix_fmt, int width, int height);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int img_crop(ref AVPicture dst, ref AVPicture stc,
                            int pix_fmt, int top_band, int left_band);

        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int img_pad(ref AVPicture dst, ref AVPicture src,
                            int height, int width, int pix_fmt, int padtop, int padbottom,
                            int padleft, int padright, ref int color);
#endif

#if !LIBAVCODEC_51
        [DllImport(AVCODEC_DLL_NAME)]
        public static extern uint av_xiphlacing(string s, uint v);

        /**
         * Parses \p str and put in \p width_ptr and \p height_ptr the detected values.
         *
         * @return 0 in case of a successful parsing, a negative value otherwise
         * @param[in] str the string to parse: it has to be a string in the format
         * <width>x<height> or a valid video frame size abbreviation.
         * @param[in,out] width_ptr pointer to the variable which will contain the detected
         * frame width value
         * @param[in,out] height_ptr pointer to the variable which will contain the detected
         * frame height value
         */
        [DllImport(AVCODEC_DLL_NAME)]
        public static extern int av_parse_video_frame_size(ref int width_ptr, ref int height_ptr,
                                       string str);

        /**
         * Parses \p str and put in \p frame_rate the detected values.
         *
         * @return 0 in case of a successful parsing, a negative value otherwise
         * @param[in] str the string to parse: it has to be a string in the format
         * <frame_rate_nom>/<frame_rate_den>, a float number or a valid video rate abbreviation
         * @param[in,out] frame_rate pointer to the AVRational which will contain the detected
         * frame rate
         */
        [DllImport(AVCODEC_DLL_NAME)]
        public static extern int av_parse_video_frame_rate(ref AVRational frame_rate,
                                      string str);
#endif

        #endregion

        #region "Consts"

        /// <summary>
        /// 1 second of 48khz 32bit audio in bytes
        /// </summary>
        public const int AVCODEC_MAX_AUDIO_FRAME_SIZE = 192000;

        /// <summary>
        /// Required number of additionally allocated bytes at the end of the input bitstream for decoding.
        /// </summary> 
        /// <remarks>
        /// This is mainly needed because some optimized bitstream readers 
        /// read 32 or 64 bit at once and could read over the end
        /// </remarks> 
        /// <remarks>
        /// Note, if the first 23 bits of the additional bytes are not 0 then damaged
        /// MPEG bitstreams could cause overread and segfault
        /// </remarks>
        public const int FF_INPUT_BUFFER_PADDING_SIZE = 8;

        /// <summary>Minimum encoding buffer size</summary>
        /// <remarks>Used to avoid some checks during header writing</remarks>
        public const int FF_MIN_BUFFER_SIZE = 16384;

        public const int FF_MAX_B_FRAMES = 16;

        /* encoding support
           these flags can be passed in AVCodecContext.flags before initing
           Note: not everything is supported yet.
        */
        public const int CODEC_FLAG_QSCALE = 0x0002;  // use fixed qscale
        public const int CODEC_FLAG_4MV = 0x0004;  // 4 MV per MB allowed / Advanced prediction for H263
        public const int CODEC_FLAG_QPEL = 0x0010;  // use qpel MC
        public const int CODEC_FLAG_GMC = 0x0020;  // use GMC
        public const int CODEC_FLAG_MV0 = 0x0040;  // always try a MB with MV=<0,0>
        public const int CODEC_FLAG_PART = 0x0080;  // use data partitioning
        /* parent program gurantees that the input for b-frame containing streams is not written to
           for at least s->max_b_frames+1 frames, if this is not set than the input will be copied */
        public const int CODEC_FLAG_INPUT_PRESERVED = 0x0100;
        public const int CODEC_FLAG_PASS1 = 0x0200;   // use internal 2pass ratecontrol in first  pass mode
        public const int CODEC_FLAG_PASS2 = 0x0400;   // use internal 2pass ratecontrol in second pass mode
        public const int CODEC_FLAG_EXTERN_HUFF = 0x1000; // use external huffman table (for mjpeg)
        public const int CODEC_FLAG_GRAY = 0x2000;   // only decode/encode grayscale
        public const int CODEC_FLAG_EMU_EDGE = 0x4000;// don't draw edges
        public const int CODEC_FLAG_PSNR = 0x8000; // error[?] variables will be set during encoding
        public const int CODEC_FLAG_TRUNCATED = 0x00010000; /** input bitstream might be truncated at a random location instead
                                            of only at frame boundaries */
        public const int CODEC_FLAG_NORMALIZE_AQP = 0x00020000; // normalize adaptive quantization
        public const int CODEC_FLAG_INTERLACED_DCT = 0x00040000; // use interlaced dct
        public const int CODEC_FLAG_LOW_DELAY = 0x00080000; // force low delay
        public const int CODEC_FLAG_ALT_SCAN = 0x00100000; // use alternate scan
        public const int CODEC_FLAG_TRELLIS_QUANT = 0x00200000; // use trellis quantization
        public const int CODEC_FLAG_GLOBAL_HEADER = 0x00400000; // place global headers in extradata instead of every keyframe
        public const int CODEC_FLAG_BITEXACT = 0x00800000; // use only bitexact stuff (except (i)dct)
        /* Fx : Flag for h263+ extra options */
#if LIBAVCODEC_51
        public const int CODEC_FLAG_H263P_AIC = 0x01000000; // H263 Advanced intra coding / MPEG4 AC prediction (remove this)
#endif
        public const int CODEC_FLAG_AC_PRED = 0x01000000; // H263 Advanced intra coding / MPEG4 AC prediction
        public const int CODEC_FLAG_H263P_UMV = 0x02000000; // Unlimited motion vector
        public const int CODEC_FLAG_CBP_RD = 0x04000000; // use rate distortion optimization for cbp
        public const int CODEC_FLAG_QP_RD = 0x08000000; // use rate distortion optimization for qp selectioon
        public const int CODEC_FLAG_H263P_AIV = 0x00000008; // H263 Alternative inter vlc
        public const int CODEC_FLAG_OBMC = 0x00000001; // OBMC
        public const int CODEC_FLAG_LOOP_FILTER = 0x00000800; // loop filter
        public const int CODEC_FLAG_H263P_SLICE_STRUCT = 0x10000000;
        public const int CODEC_FLAG_INTERLACED_ME = 0x20000000; // interlaced motion estimation
        public const int CODEC_FLAG_SVCD_SCAN_OFFSET = 0x40000000; // will reserve space for SVCD scan offset user data
        public const uint CODEC_FLAG_CLOSED_GOP = ((uint)0x80000000);
        public const int CODEC_FLAG2_FAST = 0x00000001; // allow non spec compliant speedup tricks
        public const int CODEC_FLAG2_STRICT_GOP = 0x00000002; // strictly enforce GOP size
        public const int CODEC_FLAG2_NO_OUTPUT = 0x00000004; // skip bitstream encoding
        public const int CODEC_FLAG2_LOCAL_HEADER = 0x00000008; // place global headers at every keyframe instead of in extradata
        public const int CODEC_FLAG2_BPYRAMID = 0x00000010; // H.264 allow b-frames to be used as references
        public const int CODEC_FLAG2_WPRED = 0x00000020; // H.264 weighted biprediction for b-frames
        public const int CODEC_FLAG2_MIXED_REFS = 0x00000040; // H.264 multiple references per partition
        public const int CODEC_FLAG2_8X8DCT = 0x00000080; // H.264 high profile 8x8 transform
        public const int CODEC_FLAG2_FASTPSKIP = 0x00000100; // H.264 fast pskip
        public const int CODEC_FLAG2_AUD = 0x00000200; // H.264 access unit delimiters
        public const int CODEC_FLAG2_BRDO = 0x00000400; // b-frame rate-distortion optimization
        public const int CODEC_FLAG2_INTRA_VLC = 0x00000800; // use MPEG-2 intra VLC table
        public const int CODEC_FLAG2_MEMC_ONLY = 0x00001000; // only do ME/MC (I frames -> ref, P frame -> ME+MC)
        public const int CODEC_FLAG2_DROP_FRAME_TIMECODE = 0x00002000; ///< timecode is in drop frame format.
        public const int CODEC_FLAG2_SKIP_RD = 0x00004000; ///< RD optimal MB level residual skipping
        public const int CODEC_FLAG2_CHUNKS = 0x00008000; ///< Input bitstream might be truncated at a packet boundaries instead of only at frame boundaries.
        public const int CODEC_FLAG2_NON_LINEAR_QUANT = 0x00010000; ///< Use MPEG-2 nonlinear quantizer.
        public const int CODEC_FLAG2_BIT_RESERVOIR = 0x00020000; ///< Use a bit reservoir when encoding if possible

        /* Unsupported options :
         * Syntax Arithmetic coding (SAC)
         * Reference Picture Selection
         * Independant Segment Decoding */
        /* /Fx */
        /* codec capabilities */
        public const int CODEC_CAP_DRAW_HORIZ_BAND = 0x0001; // decoder can use draw_horiz_band callback

        /**
         * Codec uses get_buffer() for allocating buffers.
         * direct rendering method 1
         */
        public const int CODEC_CAP_DR1 = 0x0002;

        /* if 'parse_only' field is true, then avcodec_parse_frame() can be
            used */
        public const int CODEC_CAP_PARSE_ONLY = 0x0004;
        public const int CODEC_CAP_TRUNCATED = 0x0008;

        /* codec can export data for HW decoding (XvMC) */
        public const int CODEC_CAP_HWACCEL = 0x0010;

        /**
         * codec has a non zero delay and needs to be feeded with NULL at the end to get the delayed data.
         * if this is not set, the codec is guranteed to never be feeded with NULL data
         */
        public const int CODEC_CAP_DELAY = 0x0020;

        /**
         * Codec can be fed a final frame with a smaller size.
         * This can be used to prevent truncation of the last audio samples.
        */
        public const int CODEC_CAP_SMALL_LAST_FRAME = 0x0040;

        //the following defines may change, don't expect compatibility if you use them
        public const int MB_TYPE_INTRA4x4 = 0x001;
        public const int MB_TYPE_INTRA16x16 = 0x0002; //FIXME h264 specific
        public const int MB_TYPE_INTRA_PCM = 0x0004; //FIXME h264 specific
        public const int MB_TYPE_16x16 = 0x0008;
        public const int MB_TYPE_16x8 = 0x0010;
        public const int MB_TYPE_8x16 = 0x0020;
        public const int MB_TYPE_8x8 = 0x0040;
        public const int MB_TYPE_INTERLACED = 0x0080;
        public const int MB_TYPE_DIRECT2 = 0x0100; //FIXME
        public const int MB_TYPE_ACPRED = 0x0200;
        public const int MB_TYPE_GMC = 0x0400;
        public const int MB_TYPE_SKIP = 0x0800;
        public const int MB_TYPE_P0L0 = 0x1000;
        public const int MB_TYPE_P1L0 = 0x2000;
        public const int MB_TYPE_P0L1 = 0x4000;
        public const int MB_TYPE_P1L1 = 0x8000;
        public const int MB_TYPE_L0 = (MB_TYPE_P0L0 | MB_TYPE_P1L0);
        public const int MB_TYPE_L1 = (MB_TYPE_P0L1 | MB_TYPE_P1L1);
        public const int MB_TYPE_L0L1 = (MB_TYPE_L0 | MB_TYPE_L1);
        public const int MB_TYPE_QUANT = 0x00010000;
        public const int MB_TYPE_CBP = 0x00020000;
        //Note bits 24-31 are reserved for codec specific use (h264 ref0, mpeg1 0mv, ...)

        public const int FF_QSCALE_TYPE_MPEG1 = 0;
        public const int FF_QSCALE_TYPE_MPEG2 = 1;
        public const int FF_QSCALE_TYPE_H264 = 2;

        public const int FF_BUFFER_TYPE_INTERNAL = 1;
        public const int FF_BUFFER_TYPE_USER = 2; // Direct rendering buffers (image is (de)allocated by user)
        public const int FF_BUFFER_TYPE_SHARED = 4; // buffer from somewhere else, don't dealloc image (data/base), all other tables are not shared
        public const int FF_BUFFER_TYPE_COPY = 8; // just a (modified) copy of some other buffer, don't dealloc anything

        public const int FF_I_TYPE = 1; ///< Intra
        public const int FF_P_TYPE = 2; ///< Predicted
        public const int FF_B_TYPE = 3; ///< Bi-dir predicted
        public const int FF_S_TYPE = 4; ///< S(GMC)-VOP MPEG4
        public const int FF_SI_TYPE = 5; ///< Switching Intra
        public const int FF_SP_TYPE = 6; ///< Switching Predicted
        public const int FF_BI_TYPE = 7;

        public const int FF_BUFFER_HINTS_VALID = 0x01;// Buffer hints value is meaningful (if 0 ignore).
        public const int FF_BUFFER_HINTS_READABLE = 0x02;// Codec will read from buffer.
        public const int FF_BUFFER_HINTS_PRESERVE = 0x04;// User must not alter buffer content.
        public const int FF_BUFFER_HINTS_REUSABLE = 0x08;// Codec will reuse the buffer (update).

        public const int DEFAULT_FRAME_RATE_BASE = 1001000;
        public const int FF_ASPECT_EXTENDED = 15;
        public const int FF_RC_STRATEGY_XVID = 1;

        public const int FF_BUG_AUTODETECT = 1;  // autodetection
        public const int FF_BUG_OLD_MSMPEG4 = 2;
        public const int FF_BUG_XVID_ILACE = 4;
        public const int FF_BUG_UMP4 = 8;
        public const int FF_BUG_NO_PADDING = 16;
        public const int FF_BUG_AMV = 32;
        public const int FF_BUG_AC_VLC = 0;  // will be removed, libavcodec can now handle these non compliant files by default
        public const int FF_BUG_QPEL_CHROMA = 64;
        public const int FF_BUG_STD_QPEL = 128;
        public const int FF_BUG_QPEL_CHROMA2 = 256;
        public const int FF_BUG_DIRECT_BLOCKSIZE = 512;
        public const int FF_BUG_EDGE = 1024;
        public const int FF_BUG_HPEL_CHROMA = 2048;
        public const int FF_BUG_DC_CLIP = 4096;
        public const int FF_BUG_MS = 8192; // workaround various bugs in microsofts broken decoders

        public const int FF_COMPLIANCE_VERY_STRICT = 2; // strictly conform to a older more strict version of the spec or reference software
        public const int FF_COMPLIANCE_STRICT = 1; // strictly conform to all the things in the spec no matter what consequences
        public const int FF_COMPLIANCE_NORMAL = 0;
        public const int FF_COMPLIANCE_INOFFICIAL = -1; // allow inofficial extensions
        public const int FF_COMPLIANCE_EXPERIMENTAL = -2; // allow non standarized experimental things

        public const int FF_ER_CAREFUL = 1;
        public const int FF_ER_COMPLIANT = 2;
        public const int FF_ER_AGGRESSIVE = 3;
        public const int FF_ER_VERY_AGGRESSIVE = 4;

        public const int FF_DCT_AUTO = 0;
        public const int FF_DCT_FASTINT = 1;
        public const int FF_DCT_INT = 2;
        public const int FF_DCT_MMX = 3;
        public const int FF_DCT_MLIB = 4;
        public const int FF_DCT_ALTIVEC = 5;
        public const int FF_DCT_FAAN = 6;

        public const int FF_IDCT_AUTO = 0;
        public const int FF_IDCT_INT = 1;
        public const int FF_IDCT_SIMPLE = 2;
        public const int FF_IDCT_SIMPLEMMX = 3;
        public const int FF_IDCT_LIBMPEG2MMX = 4;
        public const int FF_IDCT_PS2 = 5;
        public const int FF_IDCT_MLIB = 6;
        public const int FF_IDCT_ARM = 7;
        public const int FF_IDCT_ALTIVEC = 8;
        public const int FF_IDCT_SH4 = 9;
        public const int FF_IDCT_SIMPLEARM = 10;
        public const int FF_IDCT_H264 = 11;
        public const int FF_IDCT_VP3 = 12;
        public const int FF_IDCT_IPP = 13;
        public const int FF_IDCT_XVIDMMX = 14;
        public const int FF_IDCT_CAVS = 15;
        public const int FF_IDCT_SIMPLEARMV5TE = 16;
        public const int FF_IDCT_SIMPLEARMV6 = 17;
        public const int FF_IDCT_SIMPLEVIS = 18;
        public const int FF_IDCT_WMV2 = 19;
        public const int FF_IDCT_FAAN = 20;

        public const int FF_EC_GUESS_MVS = 1;
        public const int FF_EC_DEBLOCK = 2;

        public const uint FF_MM_FORCE = 0x80000000; /* force usage of selected flags (OR) */
        //    /* lower 16 bits - CPU features */

        public const int FF_MM_MMX = 0x0001; /* standard MMX */
        public const int FF_MM_3DNOW = 0x0004; /* AMD 3DNOW */
        public const int FF_MM_MMXEXT = 0x0002;/* SSE integer functions or AMD MMX ext */
        public const int FF_MM_SSE = 0x0008; /* SSE functions */
        public const int FF_MM_SSE2 = 0x0010;/* PIV SSE2 functions */
        public const int FF_MM_3DNOWEXT = 0x0020;/* AMD 3DNowExt */
        public const int FF_MM_SSE3 = 0x0040; ///< Prescott SSE3 functions
        public const int FF_MM_SSSE3 = 0x0080; ///< Conroe SSSE3 functions

        public const int FF_MM_IWMMXT = 0x0100; /* XScale IWMMXT */

        public const int FF_PRED_LEFT = 0;
        public const int FF_PRED_PLANE = 1;
        public const int FF_PRED_MEDIAN = 2;

        public const int FF_DEBUG_PICT_INFO = 1;
        public const int FF_DEBUG_RC = 2;
        public const int FF_DEBUG_BITSTREAM = 4;
        public const int FF_DEBUG_MB_TYPE = 8;
        public const int FF_DEBUG_QP = 16;
        public const int FF_DEBUG_MV = 32;
        public const int FF_DEBUG_DCT_COEFF = 0x00000040;
        public const int FF_DEBUG_SKIP = 0x00000080;
        public const int FF_DEBUG_STARTCODE = 0x00000100;
        public const int FF_DEBUG_PTS = 0x00000200;
        public const int FF_DEBUG_ER = 0x00000400;
        public const int FF_DEBUG_MMCO = 0x00000800;
        public const int FF_DEBUG_BUGS = 0x00001000;
        public const int FF_DEBUG_VIS_QP = 0x00002000;
        public const int FF_DEBUG_VIS_MB_TYPE = 0x00004000;

        public const int FF_DEBUG_VIS_MV_P_FOR = 0x00000001; //visualize forward predicted MVs of P frames
        public const int FF_DEBUG_VIS_MV_B_FOR = 0x00000002; //visualize forward predicted MVs of B frames
        public const int FF_DEBUG_VIS_MV_B_BACK = 0x00000004; //visualize backward predicted MVs of B frames

        public const int FF_CMP_SAD = 0;
        public const int FF_CMP_SSE = 1;
        public const int FF_CMP_SATD = 2;
        public const int FF_CMP_DCT = 3;
        public const int FF_CMP_PSNR = 4;
        public const int FF_CMP_BIT = 5;
        public const int FF_CMP_RD = 6;
        public const int FF_CMP_ZERO = 7;
        public const int FF_CMP_VSAD = 8;
        public const int FF_CMP_VSSE = 9;
        public const int FF_CMP_NSSE = 10;
        public const int FF_CMP_W53 = 11;
        public const int FF_CMP_W97 = 12;
        public const int FF_CMP_DCTMAX = 13;
        public const int FF_CMP_DCT264 = 14;
        public const int FF_CMP_CHROMA = 256;

        public const int FF_DTG_AFD_SAME = 8;
        public const int FF_DTG_AFD_4_3 = 9;
        public const int FF_DTG_AFD_16_9 = 10;
        public const int FF_DTG_AFD_14_9 = 11;
        public const int FF_DTG_AFD_4_3_SP_14_9 = 13;
        public const int FF_DTG_AFD_16_9_SP_14_9 = 14;
        public const int FF_DTG_AFD_SP_4_3 = 15;

        public const int FF_DEFAULT_QUANT_BIAS = 999999;

        public const int FF_LAMBDA_SHIFT = 7;
        public const int FF_LAMBDA_SCALE = (1 << FF_LAMBDA_SHIFT);
        public const int FF_QP2LAMBDA = 118; // factor to convert from H.263 QP to lambda
        public const int FF_LAMBDA_MAX = (256 * 128 - 1);

        public const int FF_QUALITY_SCALE = FF_LAMBDA_SCALE;//FIXME maybe remove

        public const int FF_CODER_TYPE_VLC = 0;
        public const int FF_CODER_TYPE_AC = 1;
        public const int FF_CODER_TYPE_RAW = 2;
        public const int FF_CODER_TYPE_RLE = 3;
        public const int FF_CODER_TYPE_DEFLATE = 4;

        public const int SLICE_FLAG_CODED_ORDER = 0x0001; // draw_horiz_band() is called in coded order instead of display
        public const int SLICE_FLAG_ALLOW_FIELD = 0x0002; // allow draw_horiz_band() with field slices (MPEG2 field pics)
        public const int SLICE_FLAG_ALLOW_PLANE = 0x0004; // allow draw_horiz_band() with 1 component at a time (SVQ1)


        public const int FF_MB_DECISION_SIMPLE = 0; // uses mb_cmp
        public const int FF_MB_DECISION_BITS = 1; // chooses the one which needs the fewest bits
        public const int FF_MB_DECISION_RD = 2; // rate distoration

        public const int FF_AA_AUTO = 0;
        public const int FF_AA_FASTINT = 1; //not implemented yet
        public const int FF_AA_INT = 2;
        public const int FF_AA_FLOAT = 3;

        public const int FF_PROFILE_UNKNOWN = -99;
        public const int FF_PROFILE_AAC_MAIN = 0;
        public const int FF_PROFILE_AAC_LOW = 1;
        public const int FF_PROFILE_AAC_SSR = 2;
        public const int FF_PROFILE_AAC_LTP = 3;

        public const int FF_LEVEL_UNKNOWN = -99;

        public const int X264_PART_I4X4 = 0x001; /* Analyse i4x4 */
        public const int X264_PART_I8X8 = 0x002; /* Analyse i8x8 (requires 8x8 transform) */
        public const int X264_PART_P8X8 = 0x010; /* Analyse p16x8, p8x16 and p8x8 */
        public const int X264_PART_P4X4 = 0x020; /* Analyse p8x4, p4x8, p4x4 */
        public const int X264_PART_B8X8 = 0x100; /* Analyse b16x8, b8x16 and b8x8 */

        public const int FF_COMPRESSION_DEFAULT = -1;

        public const int AVPALETTE_SIZE = 1024;
        public const int AVPALETTE_COUNT = 256;

        public const int FF_LOSS_RESOLUTION = 0x0001; /* loss due to resolution change */
        public const int FF_LOSS_DEPTH = 0x0002; /* loss due to color depth change */
        public const int FF_LOSS_COLORSPACE = 0x0004; /* loss due to color space conversion */
        public const int FF_LOSS_ALPHA = 0x0008; /* loss of alpha bits */
        public const int FF_LOSS_COLORQUANT = 0x0010; /* loss due to color quantization */
        public const int FF_LOSS_CHROMA = 0x0020; /* loss of chroma (e.g. rgb to gray conversion) */

        public const int FF_ALPHA_TRANSP = 0x0001; // image has some totally transparent pixels 
        public const int FF_ALPHA_SEMI_TRANSP = 0x0002; // image has some transparent pixels 

        public const int AV_PARSER_PTS_NB = 4;

        public const int PARSER_FLAG_COMPLETE_FRAMES = 0x0001;
        public const int AVERROR_UNKNOWN = -22;  /**< unknown error */
        public const int AVERROR_IO = -5;  /**< I/O error */
        public const int AVERROR_NUMEXPECTED = -33;   /**< Number syntax expected in filename. */
        public const int AVERROR_INVALIDDATA = -22; /**< invalid data found */
        public const int AVERROR_NOMEM = -12; /**< not enough memory */
        public const int AVERROR_NOFMT = -42; /**< unknown format */
        public const int AVERROR_NOTSUPP = -40; /**< Operation not supported. */
        public const int AVERROR_NOENT = -2; /**< No such file or directory. */

        #endregion
    }
}
