#region LGPL License
//
// AVCodecContext.cs
//
// Author:
//   Justin Cherniak (justin.cherniak@gmail.com)
//   Martin Cyr (mcyr@innvue.com)
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
using FFmpegSharp.Interop.Util;
using FFmpegSharp.Interop.Format;

namespace FFmpegSharp.Interop.Codec
{
    /// <param name="offset">int[4]</param>
    public delegate void DrawhorizBandCallback(ref AVCodecContext pAVCodecContext, ref AVFrame pAVFrame,
                                        ref int offset,
                                        int y, int type, int height);

    public delegate void RtpCallback(ref AVCodecContext pAVCodecContext, ref byte[] pdata, int size, int mb_nb);

    public delegate int GetBufferCallback(ref AVCodecContext pAVCodecContext, ref AVFrame pAVFrame);

    public delegate void ReleaseBufferCallback(ref AVCodecContext pAVCodecContext, ref AVFrame pAVFrame);

    public delegate PixelFormat GetFormatCallback(ref AVCodecContext pAVCodecContext, PixelFormat pPixelFormat);

    public delegate int RegetBufferCallback(ref AVCodecContext pAVCodecContext, ref AVFrame pAVFrame);

    public delegate int FuncCallback(ref AVCodecContext pAVCodecContext, object parg);

    public delegate int ExecuteCallback(ref AVCodecContext pAVCodecContext,
                                        [MarshalAs(UnmanagedType.FunctionPtr)]FuncCallback func,
                                        ref IntPtr arg2, ref int ret, int count, int size);

    public delegate int Execute2Callback(ref AVCodecContext pAVCodecContext,
                                        [MarshalAs(UnmanagedType.FunctionPtr)]FuncCallback func,
                                        ref IntPtr arg2, ref int ret, int count);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVCodecContext
    {
        /**
         * Info on struct for av_log
         * - set by avcodec_alloc_context
         */
        public AVClass* av_class;

        public int log_level_offset;

        public AVMediaType codec_type;
        public AVCodec* codec;

        private fixed sbyte codec_name_ptr[32];
        public string codec_name
        {
            get
            {
                fixed (sbyte* ptr = codec_name_ptr)
                    return new string(ptr);
            }
            set
            {
                fixed (sbyte* ptr = codec_name_ptr)
                    Utils.SetString(ptr, 32, value);
            }
        }

        public AVCodecID codec_id;

        public uint codec_tag;

        public uint stream_codec_tag;

        public int sub_id;

        public void* priv_data;

        public AVCodecInternal* internal_context;

        public void* opaque;

        /**
         * the average bitrate.
         * - encoding: set by user. unused for constant quantizer encoding
         * - decoding: set by lavc. 0 or some bitrate if this info is available in the stream
         */
        public int bit_rate;

        /**
         * number of bits the bitstream is allowed to diverge from the reference.
         *           the reference can be CBR (for CBR pass1) or VBR (for pass2)
         * - encoding: set by user. unused for constant quantizer encoding
         * - decoding: unused
         */
        public int bit_rate_tolerance;

        /**
         * global quality for codecs which cannot change it per frame.
         * this should be proportional to MPEG1/2/4 qscale.
         * - encoding: set by user.
         * - decoding: unused
         */
        public GlobalQuality global_quality;

        /**
         * - encoding: set by user.
         * - decoding: unused
         */
        public int compression_level;

        /**
         * CODEC_FLAG_*.
         * - encoding: set by user.
         * - decoding: set by user.
         */
        public CODEC_FLAG flags;

        /**
         * CODEC_FLAG2_*.
         * - encoding: set by user.
         * - decoding: set by user.
         */
        public CODEC_FLAG2 flags2;

        /**
         * some codecs need / can use extra-data like huffman tables.
         * mjpeg: huffman tables
         * rv10: additional flags
         * mpeg4: global headers (they can be in the bitstream or here)
         * the allocated memory should be FF_INPUT_BUFFER_PADDING_SIZE bytes larger
         * then extradata_size to avoid prolems if its read with the bitstream reader
         * the bytewise contents of extradata must not depend on the architecture or cpu endianness
         * - encoding: set/allocated/freed by lavc.
         * - decoding: set/allocated/freed by user.
         */
        public byte* extradata; // void* extradata;

        public int extradata_size;

        /**
         * this is the fundamental unit of time (in seconds) in terms
         * of which frame timestamps are represented. for fixed-fps content,
         * timebase should be 1/framerate and timestamp increments should be
         * identically 1.
         * - encoding: MUST be set by user
         * - decoding: set by lavc.
         */

        public AVRational time_base;

        /**
         * For some codecs, the time base is closer to the field rate than the frame rate.
         * Most notably, H.264 and MPEG-2 specify time_base as half of frame duration
         * if no telecine is used ...
         *
         * Set to time_base ticks per frame. Default 1, e.g., H.264/MPEG-2 set it to 2.
         */
        public int ticks_per_frame;

        /**
         * Encoding: Number of frames delay there will be from the encoder input to
         *           the decoder output. (we assume the decoder matches the spec)
         * Decoding: Number of frames delay in addition to what a standard decoder
         *           as specified in the spec would produce.
         *
         * Video:
         *   Number of frames the decoded output will be delayed relative to the
         *   encoded input.
         *
         * Audio:
         *   Number of "priming" samples added to the beginning of the stream
         *   during encoding. The decoded output will be delayed by this many
         *   samples relative to the input to the encoder. Note that this field is
         *   purely informational and does not directly affect the pts output by
         *   the encoder, which should always be based on the actual presentation
         *   time, including any delay.
         *
         * - encoding: Set by libavcodec.
         * - decoding: Set by libavcodec.
         */
        public int delay;


        /* video only */
        /**
         * picture width / height.
         * - encoding: MUST be set by user.
         * - decoding: Set by libavcodec.
         * Note: For compatibility it is possible to set this instead of
         * coded_width/height before decoding.
         */
        public int width, height;

        /**
         * Bitstream width / height, may be different from width/height if lowres enabled.
         * - encoding: unused
         * - decoding: Set by user before init if known. Codec should override / dynamically change if needed.
         */
        public int coded_width, coded_height;

        /**
         * the number of pictures in a group of pictures, or 0 for intra_only
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int gop_size;


        /**
         * Pixel format, see PIX_FMT_xxx.
         * May be set by the demuxer if known from headers.
         * May be overridden by the decoder if it knows better.
         * - encoding: Set by user.
         * - decoding: Set by user if known, overridden by libavcodec if known
         */
        public PixelFormat pix_fmt;

        /**
         * motion estimation algorithm used for video coding.
         * 1 (zero), 2 (full), 3 (log), 4 (phods), 5 (epzs), 6 (x1), 7 (hex),
         * 8 (umh), 9 (iter) [7, 8 are x264 specific, 9 is snow specific]
         * - encoding: MUST be set by user.
         * - decoding: unused
         */
        public int me_method;

        /**
         * if non NULL, 'draw_horiz_band' is called by the libavcodec
         * decoder to draw an horizontal band. It improve cache usage. Not
         * all codecs can do that. You must check the codec capabilities
         * before
         * - encoding: unused
         * - decoding: set by user.
         * @param height the height of the slice
         * @param y the y position of the slice
         * @param type 1->top field, 2->bottom field, 3->frame
         * @param offset offset into the AVFrame.data from which the slice should be read
         */
        public IntPtr draw_horiz_band_ptr;
        public DrawhorizBandCallback draw_horiz_band
        {
            get { return (Utils.GetDelegate<DrawhorizBandCallback>(draw_horiz_band_ptr)); }
        }

        /**
         * callback to negotiate the pixelFormat.
         * @param fmt is the list of formats which are supported by the codec,
         * its terminated by -1 as 0 is a valid format, the formats are ordered by quality
         * the first is allways the native one
         * @return the choosen format
         * - encoding: unused
         * - decoding: set by user, if not set then the native format will always be choosen
         */
        private IntPtr get_format_ptr;
        public GetFormatCallback get_format
        {
            get { return (Utils.GetDelegate<GetFormatCallback>(get_format_ptr)); }
        }

        /**
         * maximum number of b frames between non b frames.
         * note: the output will be delayed by max_b_frames+1 relative to the input
         * - encoding: set by user.
         * - decoding: unused
         */
        public int max_b_frames;

        /**
         * qscale factor between ip and b frames.
         * - encoding: set by user.
         * - decoding: unused
         */
        public float b_quant_factor;

        public FF_RC_STRATEGY rc_strategy;

        public int b_frame_strategy;

        [Obsolete]
        public int luma_elim_threshold;

        [Obsolete]
        public int chroma_elim_threshold;

        /**
         * qscale offset between IP and B-frames
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float b_quant_offset;

        /**
         * Size of the frame reordering buffer in the decoder.
         * For MPEG-2 it is 1 IPB or 0 low delay IP.
         * - encoding: Set by libavcodec.
         * - decoding: Set by libavcodec.
         */
        public int has_b_frames;

        /**
         * 0-> h263 quant 1-> mpeg quant
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int mpeg_quant;

        /**
         * qscale factor between P and I-frames
         * If > 0 then the last p frame quantizer will be used (q= lastp_q*factor+offset).
         //* If < 0 then normal ratecontrol will be done (q= -normal_q*factor+offset).
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float i_quant_factor;

        /**
         * qscale offset between P and I-frames
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float i_quant_offset;

        /**
         * luminance masking (0-> disabled)
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float lumi_masking;

        /**
         * temporary complexity masking (0-> disabled)
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float temporal_cplx_masking;

        /**
         * spatial complexity masking (0-> disabled)
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float spatial_cplx_masking;

        /**
         * p block masking (0-> disabled)
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float p_masking;

        /**
         * darkness masking (0-> disabled)
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float dark_masking;

        /**
         * slice count
         * - encoding: Set by libavcodec.
         * - decoding: Set by user (or 0).
         */
        public int slice_count;

        /**
         * prediction method (needed for huffyuv)
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int prediction_method;

        /**
         * slice offsets in the frame in bytes
         * - encoding: Set/allocated by libavcodec.
         * - decoding: Set/allocated by user (or NULL).
         */
        public int* slice_offset;

        /**
         * sample aspect ratio (0 if unknown)
         * That is the width of a pixel divided by the height of the pixel.
         * Numerator and denominator must be relatively prime and smaller than 256 for some video standards.
         * - encoding: Set by user.
         * - decoding: Set by libavcodec.
         */
        public AVRational sample_aspect_ratio;

        /**
         * motion estimation comparison function
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int me_cmp;

        /**
         * subpixel motion estimation comparison function
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int me_sub_cmp;

        /**
         * macroblock comparison function (not supported yet)
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int mb_cmp;

        /**
         * interlaced DCT comparison function
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int ildct_cmp;

        /**
         * ME diamond size & shape
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int dia_size;

        /**
         * amount of previous MV predictors (2a+1 x 2a+1 square)
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int last_predictor_count;

        /**
         * prepass for motion estimation
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int pre_me;

        /**
         * motion estimation prepass comparison function
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int me_pre_cmp;

        /**
         * ME prepass diamond size & shape
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int pre_dia_size;

        /**
         * subpel ME quality
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int me_subpel_quality;

        /**
         * DTG active format information (additional aspect ratio
         * information only used in DVB MPEG-2 transport streams)
         * 0 if not set.
         *
         * - encoding: unused
         * - decoding: Set by decoder.
         */
        public int dtg_active_format;

        /**
         * maximum motion estimation search range in subpel units
         * If 0 then no limit.
         *
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int me_range;

        /**
         * intra quantizer bias
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int intra_quant_bias;

        /**
         * inter quantizer bias
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int inter_quant_bias;

        [Obsolete]
        public int color_table_id;

        /**
         * slice flags
         * - encoding: unused
         * - decoding: Set by user.
         */
        public int slice_flags;


        /**
         * XVideo Motion Acceleration
         * - encoding: forbidden
         * - decoding: set by decoder
         */
        public int xvmc_acceleration;

        /**
         * macroblock decision mode
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int mb_decision;

        /**
         * custom intra quantization matrix
         * - encoding: Set by user, can be NULL.
         * - decoding: Set by libavcodec.
         */
        public ushort* intra_matrix;

        /**
         * custom inter quantization matrix
         * - encoding: Set by user, can be NULL.
         * - decoding: Set by libavcodec.
         */
        public ushort* inter_matrix;

        /**
         * scene change detection threshold
         * 0 is default, larger means fewer detected scene changes.
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int scenechange_threshold;

        /**
         * noise reduction strength
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int noise_reduction;

        [Obsolete]
        public int inter_threshold;

        [Obsolete]
        public int quantizer_noise_shaping;

        /**
        * Motion estimation threshold below which no motion estimation is
        * performed, but instead the user specified motion vectors are used.
        *
        * - encoding: Set by user.
        * - decoding: unused
        */
        public int me_threshold;

        /**
         * Macroblock threshold below which the user specified macroblock types will be used.
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int mb_threshold;

        /**
         * precision of the intra DC coefficient - 8
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int intra_dc_precision;

        /**
         * Number of macroblock rows at the top which are skipped.
         * - encoding: unused
         * - decoding: Set by user.
         */
        public int skip_top;

        /**
         * Number of macroblock rows at the bottom which are skipped.
         * - encoding: unused
         * - decoding: Set by user.
         */
        public int skip_bottom;

        /**
         * Border processing masking, raises the quantizer for mbs on the borders
         * of the picture.
         * - encoding: Set by user.
         * - decoding: unused
         */
        public float border_masking;

        /**
         * minimum MB lagrange multipler
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int mb_lmin;

        /**
         * maximum MB lagrange multipler
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int mb_lmax;

        /**
         *
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int me_penalty_compensation;

        /**
         *
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int bidir_refine;

        /**
         *
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int brd_scale;

        /**
         * minimum GOP size
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int keyint_min;

        /**
         * number of reference frames
         * - encoding: Set by user.
         * - decoding: Set by lavc.
         */
        public int refs;

        /**
         * chroma qp offset from luma
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int chromaoffset;

        /**
         * Multiplied by qscale for each frame and added to scene_change_score.
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int scenechange_factor;

        /**
         *
         * Note: Value depends upon the compare function used for fullpel ME.
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int mv0_threshold;

        /**
         * Adjust sensitivity of b_frame_strategy 1.
         * - encoding: Set by user.
         * - decoding: unused
         */
        public int b_sensitivity;

        /**
         * Chromaticity coordinates of the source primaries.
         * - encoding: Set by user
         * - decoding: Set by libavcodec
         */
        public AVColorPrimaries color_primaries;

        /**
         * Color Transfer Characteristic.
         * - encoding: Set by user
         * - decoding: Set by libavcodec
         */
        public AVColorTransferCharacteristic color_trc;

        /**
         * YUV colorspace type.
         * - encoding: Set by user
         * - decoding: Set by libavcodec
         */
        public AVColorSpace colorspace;

        /**
         * MPEG vs JPEG YUV range.
         * - encoding: Set by user
         * - decoding: Set by libavcodec
         */
        public AVColorRange color_range;

        /**
         * This defines the location of chroma samples.
         * - encoding: Set by user
         * - decoding: Set by libavcodec
         */
        public AVChromaLocation chroma_sample_location;

        /**
         * Number of slices.
         * Indicates number of picture subdivisions. Used for parallelized
         * decoding.
         * - encoding: Set by user
         * - decoding: unused
         */
        public int slices;

        /** Field order
         * - encoding: set by libavcodec
         * - decoding: Set by libavcodec
         */
        public AVFieldOrder field_order;


        /* audio only */
        public int sample_rate; ///< samples per second
        public int channels;    ///< number of audio channels

        /**
            * audio sample format
            * - encoding: Set by user.
            * - decoding: Set by libavcodec.
            */
        public AVSampleFormat sample_fmt;  ///< sample format

        /* The following data should not be initialized. */
        /**
         * Samples per packet, initialized when calling 'init'.
         */
        public int frame_size;

        /**
         * Frame counter, set by libavcodec.
         *
         * - decoding: total number of frames returned from the decoder so far.
         * - encoding: total number of frames passed to the encoder so far.
         *
         *   @note the counter is not incremented if encoding/decoding resulted in
         *   an error.
         */
        public int frame_number;

        /**
         * number of bytes per packet if constant and known or 0
         * Used by some WAV based audio codecs.
         */
        int block_align;

        /**
         * Audio cutoff bandwidth (0 means "automatic")
         * - encoding: Set by user.
         * - decoding: unused
         */
        int cutoff;

        /**
         * Decoder should decode to this many channels if it can (0 for default)
         * - encoding: unused
         * - decoding: Set by user.
         * @deprecated Deprecated in favor of request_channel_layout.
         */
        int request_channels;

        /**
         * Audio channel layout.
         * - encoding: set by user.
         * - decoding: set by user, may be overwritten by libavcodec.
         */
        ulong channel_layout;

        /**
         * Request decoder to use this channel layout if it can (0 for default)
         * - encoding: unused
         * - decoding: Set by user.
         */
        ulong request_channel_layout;

        /**
        * Type of service that the audio stream conveys.
        * - encoding: Set by user.
        * - decoding: Set by libavcodec.
        */
        AVAudioServiceType audio_service_type;

        /**
         * desired sample format
         * - encoding: Not used.
         * - decoding: Set by user.
         * Decoder will decode to this format if it can.
         */
        AVSampleFormat request_sample_fmt;


        /**
         * Called at the beginning of each frame to get a buffer for it.
         *
         * The function will set AVFrame.data[], AVFrame.linesize[].
         * AVFrame.extended_data[] must also be set, but it should be the same as
         * AVFrame.data[] except for planar audio with more channels than can fit
         * in AVFrame.data[]. In that case, AVFrame.data[] shall still contain as
         * many data pointers as it can hold.
         *
         * if CODEC_CAP_DR1 is not set then get_buffer() must call
         * avcodec_default_get_buffer() instead of providing buffers allocated by
         * some other means.
         *
         * AVFrame.data[] should be 32- or 16-byte-aligned unless the CPU doesn't
         * need it. avcodec_default_get_buffer() aligns the output buffer properly,
         * but if get_buffer() is overridden then alignment considerations should
         * be taken into account.
         *
         * @see avcodec_default_get_buffer()
         *
         * Video:
         *
         * If pic.reference is set then the frame will be read later by libavcodec.
         * avcodec_align_dimensions2() should be used to find the required width and
         * height, as they normally need to be rounded up to the next multiple of 16.
         *
         * If frame multithreading is used and thread_safe_callbacks is set,
         * it may be called from a different thread, but not from more than one at
         * once. Does not need to be reentrant.
         *
         * @see release_buffer(), reget_buffer()
         * @see avcodec_align_dimensions2()
         *
         * Audio:
         *
         * Decoders request a buffer of a particular size by setting
         * AVFrame.nb_samples prior to calling get_buffer(). The decoder may,
         * however, utilize only part of the buffer by setting AVFrame.nb_samples
         * to a smaller value in the output frame.
         *
         * Decoders cannot use the buffer after returning from
         * avcodec_decode_audio4(), so they will not call release_buffer(), as it
         * is assumed to be released immediately upon return.
         *
         * As a convenience, av_samples_get_buffer_size() and
         * av_samples_fill_arrays() in libavutil may be used by custom get_buffer()
         * functions to find the required data size and to fill data pointers and
         * linesize. In AVFrame.linesize, only linesize[0] may be set for audio
         * since all planes must be the same size.
         *
         * @see av_samples_get_buffer_size(), av_samples_fill_arrays()
         *
         * - encoding: unused
         * - decoding: Set by libavcodec, user can override.
         */

        public IntPtr get_buffer_ptr;
        public GetBufferCallback get_buffer
        {
            get { return (Utils.GetDelegate<GetBufferCallback>(get_buffer_ptr)); }
        }


        /**
         * Called to release buffers which were allocated with get_buffer.
         * A released buffer can be reused in get_buffer().
         * pic.data[*] must be set to NULL.
         * May be called from a different thread if frame multithreading is used,
         * but not by more than one thread at once, so does not need to be reentrant.
         * - encoding: unused
         * - decoding: Set by libavcodec, user can override.
         */
        public IntPtr release_buffer_ptr;
        public ReleaseBufferCallback release_buffer
        {
            get { return (Utils.GetDelegate<ReleaseBufferCallback>(release_buffer_ptr)); }
        }


        /**
         * Called at the beginning of a frame to get cr buffer for it.
         * Buffer type (size, hints) must be the same. libavcodec won't check it.
         * libavcodec will pass previous buffer in pic, function should return
         * same buffer or new buffer with old frame "painted" into it.
         * If pic.data[0] == NULL must behave like get_buffer().
         * if CODEC_CAP_DR1 is not set then reget_buffer() must call
         * avcodec_default_reget_buffer() instead of providing buffers allocated by
         * some other means.
         * - encoding: unused
         * - decoding: Set by libavcodec, user can override.
         */
        public IntPtr reget_buffer_ptr;
        public GetBufferCallback reget_buffer
        {
            get { return (Utils.GetDelegate<GetBufferCallback>(reget_buffer_ptr)); }
        }

        /* - encoding parameters */
        float qcompress;  ///< amount of qscale change between easy & hard scenes (0.0-1.0)
        float qblur;      ///< amount of qscale smoothing over time (0.0-1.0)

        /**
         * minimum quantizer
         * - encoding: Set by user.
         * - decoding: unused
         */
        int qmin;

        /**
         * maximum quantizer
         * - encoding: Set by user.
         * - decoding: unused
         */
        int qmax;

        /**
         * maximum quantizer difference between frames
         * - encoding: Set by user.
         * - decoding: unused
         */
        int max_qdiff;

        /**
         * ratecontrol qmin qmax limiting method
         * 0-> clipping, 1-> use a nice continuous function to limit qscale wthin qmin/qmax.
         * - encoding: Set by user.
         * - decoding: unused
         */
        float rc_qsquish;

        float rc_qmod_amp;
        int rc_qmod_freq;

        /**
         * decoder bitstream buffer size
         * - encoding: Set by user.
         * - decoding: unused
         */
        int rc_buffer_size;

        /**
         * ratecontrol override, see RcOverride
         * - encoding: Allocated/set/freed by user.
         * - decoding: unused
         */
        int rc_override_count;
        RcOverride* rc_override;

        /**
         * rate control equation
         * - encoding: Set by user
         * - decoding: unused
         */
        char* rc_eq;

        /**
         * maximum bitrate
         * - encoding: Set by user.
         * - decoding: unused
         */
        int rc_max_rate;

        /**
         * minimum bitrate
         * - encoding: Set by user.
         * - decoding: unused
         */
        int rc_min_rate;

        float rc_buffer_aggressivity;

        /**
         * initial complexity for pass1 ratecontrol
         * - encoding: Set by user.
         * - decoding: unused
         */
        float rc_initial_cplx;

        /**
         * Ratecontrol attempt to use, at maximum, <value> of what can be used without an underflow.
         * - encoding: Set by user.
         * - decoding: unused.
         */
        float rc_max_available_vbv_use;

        /**
         * Ratecontrol attempt to use, at least, <value> times the amount needed to prevent a vbv overflow.
         * - encoding: Set by user.
         * - decoding: unused.
         */
        float rc_min_vbv_overflow_use;

        /**
         * Number of bits which should be loaded into the rc buffer before decoding starts.
         * - encoding: Set by user.
         * - decoding: unused
         */
        int rc_initial_buffer_occupancy;

        /**
         * coder type
         * - encoding: Set by user.
         * - decoding: unused
         */
        int coder_type;

        /**
         * context model
         * - encoding: Set by user.
         * - decoding: unused
         */
        int context_model;

        /**
         * minimum Lagrange multipler
         * - encoding: Set by user.
         * - decoding: unused
         */
        int lmin;

        /**
         * maximum Lagrange multipler
         * - encoding: Set by user.
         * - decoding: unused
         */
        int lmax;

        /**
         * frame skip threshold
         * - encoding: Set by user.
         * - decoding: unused
         */
        int frame_skip_threshold;

        /**
         * frame skip factor
         * - encoding: Set by user.
         * - decoding: unused
         */
        int frame_skip_factor;

        /**
         * frame skip exponent
         * - encoding: Set by user.
         * - decoding: unused
         */
        int frame_skip_exp;

        /**
         * frame skip comparison function
         * - encoding: Set by user.
         * - decoding: unused
         */
        int frame_skip_cmp;

        /**
         * trellis RD quantization
         * - encoding: Set by user.
         * - decoding: unused
         */
        int trellis;

        /**
         * - encoding: Set by user.
         * - decoding: unused
         */
        int min_prediction_order;

        /**
         * - encoding: Set by user.
         * - decoding: unused
         */
        int max_prediction_order;

        /**
         * GOP timecode frame start number
         * - encoding: Set by user, in non drop frame format
         * - decoding: Set by libavcodec (timecode in the 25 bits format, -1 if unset)
         */
        long timecode_frame_start;

        /* The RTP callback: This function is called    */
        /* every time the encoder has a packet to send. */
        /* It depends on the encoder if the data starts */
        /* with a Start Code (it should). H.263 does.   */
        /* mb_nb contains the number of macroblocks     */
        /* encoded in the RTP payload.                  */
        IntPtr rtp_callback_ptr;
        public RtpCallback rtp_callback
        {
            get { return (Utils.GetDelegate<RtpCallback>(rtp_callback_ptr)); }
        }

        int rtp_payload_size;   /* The size of the RTP payload: the coder will  */
        /* do its best to deliver a chunk with size     */
        /* below rtp_payload_size, the chunk will start */
        /* with a start code on some codecs like H.263. */
        /* This doesn't take account of any particular  */
        /* headers inside the transmitted RTP payload.  */

        /* statistics, used for 2-pass encoding */
        int mv_bits;
        int header_bits;
        int i_tex_bits;
        int p_tex_bits;
        int i_count;
        int p_count;
        int skip_count;
        int misc_bits;

        /**
         * number of bits used for the previously encoded frame
         * - encoding: Set by libavcodec.
         * - decoding: unused
         */
        int frame_bits;

        /**
         * pass1 encoding statistics output buffer
         * - encoding: Set by libavcodec.
         * - decoding: unused
         */
        char* stats_out;

        /**
         * pass2 encoding statistics input buffer
         * Concatenated stuff from stats_out of pass1 should be placed here.
         * - encoding: Allocated/set/freed by user.
         * - decoding: unused
         */
        char* stats_in;

        /**
         * Work around bugs in encoders which sometimes cannot be detected automatically.
         * - encoding: Set by user
         * - decoding: Set by user
         */
        int workaround_bugs;

        /**
         * strictly follow the standard (MPEG4, ...).
         * - encoding: Set by user.
         * - decoding: Set by user.
         * Setting this to STRICT or higher means the encoder and decoder will
         * generally do stupid things, whereas setting it to unofficial or lower
         * will mean the encoder might produce output that is not supported by all
         * spec-compliant decoders. Decoders don't differentiate between normal,
         * unofficial and experimental (that is, they always try to decode things
         * when they can) unless they are explicitly asked to behave stupidly
         * (=strictly conform to the specs)
         */
        int strict_std_compliance;

        /**
         * error concealment flags
         * - encoding: unused
         * - decoding: Set by user.
         */
        int error_concealment;

        /**
         * debug
         * - encoding: Set by user.
         * - decoding: Set by user.
         */
        int debug;

        /**
         * debug
         * - encoding: Set by user.
         * - decoding: Set by user.
         */
        int debug_mv;

        /**
         * Error recognition; may misdetect some more or less valid parts as errors.
         * - encoding: unused
         * - decoding: Set by user.
         */
        int err_recognition;

        /**
         * opaque 64bit number (generally a PTS) that will be reordered and
         * output in AVFrame.reordered_opaque
         * @deprecated in favor of pkt_pts
         * - encoding: unused
         * - decoding: Set by user.
         */
        long reordered_opaque;

        /**
         * Hardware accelerator in use
         * - encoding: unused.
         * - decoding: Set by libavcodec
         */
        AVHWAccel* hwaccel;

        /**
         * Hardware accelerator context.
         * For some hardware accelerators, a global context needs to be
         * provided by the user. In that case, this holds display-dependent
         * data FFmpeg cannot instantiate itself. Please refer to the
         * FFmpeg HW accelerator documentation to know how to fill this
         * is. e.g. for VA API, this is a struct vaapi_context.
         * - encoding: unused
         * - decoding: Set by user
         */
        void* hwaccel_context;

        /**
         * error
         * - encoding: Set by libavcodec if flags&CODEC_FLAG_PSNR.
         * - decoding: unused
         */
        fixed ulong error[8];

        /**
         * DCT algorithm, see FF_DCT_* below
         * - encoding: Set by user.
         * - decoding: unused
         */
        int dct_algo;

        /**
         * IDCT algorithm, see FF_IDCT_* below.
         * - encoding: Set by user.
         * - decoding: Set by user.
         */
        int idct_algo;

        /**
         * bits per sample/pixel from the demuxer (needed for huffyuv).
         * - encoding: Set by libavcodec.
         * - decoding: Set by user.
         */
        int bits_per_coded_sample;

        /**
         * Bits per sample/pixel of internal libavcodec pixel/sample format.
         * - encoding: set by user.
         * - decoding: set by libavcodec.
         */
        int bits_per_raw_sample;

        /**
         * low resolution decoding, 1-> 1/2 size, 2->1/4 size
         * - encoding: unused
         * - decoding: Set by user.
         */
        int lowres;

        /**
         * the picture in the bitstream
         * - encoding: Set by libavcodec.
         * - decoding: Set by libavcodec.
         */
        AVFrame* coded_frame;

        /**
         * thread count
         * is used to decide how many independent tasks should be passed to execute()
         * - encoding: Set by user.
         * - decoding: Set by user.
         */
        int thread_count;

        /**
         * Which multithreading methods to use.
         * Use of FF_THREAD_FRAME will increase decoding delay by one frame per thread,
         * so clients which cannot provide future frames should not use it.
         *
         * - encoding: Set by user, otherwise the default is used.
         * - decoding: Set by user, otherwise the default is used.
         */
        int thread_type;

        /**
         * Which multithreading methods are in use by the codec.
         * - encoding: Set by libavcodec.
         * - decoding: Set by libavcodec.
         */
        int active_thread_type;

        /**
         * Set by the client if its custom get_buffer() callback can be called
         * synchronously from another thread, which allows faster multithreaded decoding.
         * draw_horiz_band() will be called from other threads regardless of this setting.
         * Ignored if the default get_buffer() is used.
         * - encoding: Set by user.
         * - decoding: Set by user.
         */
        int thread_safe_callbacks;

        /**
         * The codec may call this to execute several independent things.
         * It will return only after finishing all tasks.
         * The user may replace this with some multithreaded implementation,
         * the default implementation will execute the parts serially.
         * @param count the number of things to execute
         * - encoding: Set by libavcodec, user can override.
         * - decoding: Set by libavcodec, user can override.
         */
        IntPtr execute_ptr;
        public ExecuteCallback execute
        {
            get { return (Utils.GetDelegate<ExecuteCallback>(execute_ptr)); }
        }

        /**
         * The codec may call this to execute several independent things.
         * It will return only after finishing all tasks.
         * The user may replace this with some multithreaded implementation,
         * the default implementation will execute the parts serially.
         * Also see avcodec_thread_init and e.g. the --enable-pthread configure option.
         * @param c context passed also to func
         * @param count the number of things to execute
         * @param arg2 argument passed unchanged to func
         * @param ret return values of executed functions, must have space for "count" values. May be NULL.
         * @param func function that will be called count times, with jobnr from 0 to count-1.
         *             threadnr will be in the range 0 to c->thread_count-1 < MAX_THREADS and so that no
         *             two instances of func executing at the same time will have the same threadnr.
         * @return always 0 currently, but code should handle a future improvement where when any call to func
         *         returns < 0 no further calls to func may be done and < 0 is returned.
         * - encoding: Set by libavcodec, user can override.
         * - decoding: Set by libavcodec, user can override.
         */
        IntPtr execute2_ptr;
        public Execute2Callback execute2
        {
            get { return (Utils.GetDelegate<Execute2Callback>(execute2_ptr)); }
        }

        /**
         * thread opaque
         * Can be used by execute() to store some per AVCodecContext stuff.
         * - encoding: set by execute()
         * - decoding: set by execute()
         */
        void* thread_opaque;

        /**
         * noise vs. sse weight for the nsse comparsion function
         * - encoding: Set by user.
         * - decoding: unused
         */
        int nsse_weight;

        /**
         * profile
         * - encoding: Set by user.
         * - decoding: Set by libavcodec.
         */
        int profile;

        /**
         * level
         * - encoding: Set by user.
         * - decoding: Set by libavcodec.
         */
        int level;

        /**
         *
         * - encoding: unused
         * - decoding: Set by user.
         */
        AVDiscard skip_loop_filter;

        /**
         *
         * - encoding: unused
         * - decoding: Set by user.
         */
        AVDiscard skip_idct;

        /**
         *
         * - encoding: unused
         * - decoding: Set by user.
         */
        AVDiscard skip_frame;

        /**
         * Header containing style information for text subtitles.
         * For SUBTITLE_ASS subtitle type, it should contain the whole ASS
         * [Script Info] and [V4+ Styles] section, plus the [Events] line and
         * the Format line following. It shouldn't include any Dialogue line.
         * - encoding: Set/allocated/freed by user (before avcodec_open2())
         * - decoding: Set/allocated/freed by libavcodec (by avcodec_open2())
         */
        byte* subtitle_header;
        int subtitle_header_size;

        /**
         * Simulates errors in the bitstream to test error concealment.
         * - encoding: Set by user.
         * - decoding: unused
         */
        int error_rate;

        /**
         * Current packet as passed into the decoder, to avoid having
         * to pass the packet into every function. Currently only valid
         * inside lavc and get/release_buffer callbacks.
         * - decoding: set by avcodec_decode_*, read by get_buffer() for setting pkt_pts
         * - encoding: unused
         */
        AVPacket* pkt;

        /**
         * VBV delay coded in the last frame (in periods of a 27 MHz clock).
         * Used for compliant TS muxing.
         * - encoding: Set by libavcodec.
         * - decoding: unused.
         */
        ulong vbv_delay;

        /**
         * Timebase in which pkt_dts/pts and AVPacket.dts/pts are.
         * Code outside libavcodec should access this field using:
         * avcodec_set_pkt_timebase(avctx)
         * - encoding unused.
         * - decodimg set by user
         */
        AVRational pkt_timebase;

        /**
         * AVCodecDescriptor
         * Code outside libavcodec should access this field using:
         * avcodec_get_codec_descriptior(avctx)
         * - encoding: unused.
         * - decoding: set by libavcodec.
         */
        AVCodecDescriptor* codec_descriptor;

        /**
         * Current statistics for PTS correction.
         * - decoding: maintained and used by libavcodec, not intended to be used by user apps
         * - encoding: unused
         */
        long pts_correction_num_faulty_pts; /// Number of incorrect PTS values so far
        long pts_correction_num_faulty_dts; /// Number of incorrect DTS values so far
        long pts_correction_last_pts;       /// PTS of the last frame
        long pts_correction_last_dts;       /// DTS of the last frame
    }

    public unsafe struct AVCodecDescriptor
    {
        public AVCodecID id;
        public AVMediaType type;
        /**
         * Name of the codec described by this descriptor. It is non-empty and
         * unique for each codec descriptor. It should contain alphanumeric
         * characters and '_' only.
         */
        public char* name;
        /**
         * A more descriptive name for this codec. May be NULL.
         */
        public char* long_name;
        /**
         * Codec properties, a combination of AV_CODEC_PROP_* flags.
         */
        public int props;
    }

    public delegate int FrameCallback(ref AVCodecContext context, ref byte buffer, uint buf_size);
    public delegate int EndFrameCallback(ref AVCodecContext context);

    public unsafe struct AVHWAccel
    {
        /**
         * Name of the hardware accelerated codec.
         * The name is globally unique among encoders and among decoders (but an
         * encoder and a decoder can share the same name).
         */
        public char* name;

        /**
         * Type of codec implemented by the hardware accelerator.
         *
         * See AVMEDIA_TYPE_xxx
         */
        public AVMediaType type;

        /**
         * Codec implemented by the hardware accelerator.
         *
         * See AV_CODEC_ID_xxx
         */
        public AVCodecID id;

        /**
         * Supported pixel format.
         *
         * Only hardware accelerated formats are supported here.
         */
        public PixelFormat pix_fmt;

        /**
         * Hardware accelerated codec capabilities.
         * see FF_HWACCEL_CODEC_CAP_*
         */
        int capabilities;

        public AVHWAccel* next;

        /**
         * Called at the beginning of each frame or field picture.
         *
         * Meaningful frame information (codec specific) is guaranteed to
         * be parsed at this point. This function is mandatory.
         *
         * Note that buf can be NULL along with buf_size set to 0.
         * Otherwise, this means the whole frame is available at this point.
         *
         * @param avctx the codec context
         * @param buf the frame data buffer base
         * @param buf_size the size of the frame in bytes
         * @return zero if successful, a negative value otherwise
         */
        IntPtr start_frame_ptr;
        public FrameCallback start_frame
        {
            get { return (Utils.GetDelegate<FrameCallback>(start_frame_ptr)); }
        }

        /**
         * Callback for each slice.
         *
         * Meaningful slice information (codec specific) is guaranteed to
         * be parsed at this point. This function is mandatory.
         *
         * @param avctx the codec context
         * @param buf the slice data buffer base
         * @param buf_size the size of the slice in bytes
         * @return zero if successful, a negative value otherwise
         */
        IntPtr decode_slice_ptr;
        public FrameCallback decode_slice
        {
            get { return (Utils.GetDelegate<FrameCallback>(decode_slice_ptr)); }
        }

        /**
         * Called at the end of each frame or field picture.
         *
         * The whole picture is parsed at this point and can now be sent
         * to the hardware accelerator. This function is mandatory.
         *
         * @param avctx the codec context
         * @return zero if successful, a negative value otherwise
         */
        IntPtr end_frame_ptr;
        public EndFrameCallback end_frame
        {
            get { return (Utils.GetDelegate<EndFrameCallback>(end_frame_ptr)); }
        }

        /**
         * Size of HW accelerator private data.
         *
         * Private data is allocated with av_mallocz() before
         * AVCodecContext.get_buffer() and deallocated after
         * AVCodecContext.release_buffer().
         */
        public int priv_data_size;
    }

    public unsafe struct AVCodecInternal
    {
        /**
         * internal buffer count
         * used by default get/release/reget_buffer().
         */
        public int buffer_count;

        /**
         * internal buffers
         * used by default get/release/reget_buffer().
         */
        public InternalBuffer* buffer;

        /**
         * Whether the parent AVCodecContext is a copy of the context which had
         * init() called on it.
         * This is used by multithreading - shared tables and picture pointers
         * should be freed from the original context only.
         */
        public int is_copy;

        /**
         * An audio frame with less than required samples has been submitted and
         * padded with silence. Reject all subsequent frames.
         */
        public int last_audio_frame;

        /**
         * temporary buffer used for encoders to store their bitstream
         */
        public byte* byte_buffer;
        public uint byte_buffer_size;

        public void* frame_thread_encoder;

        /**
         * Number of audio samples to skip at the start of the next decoded frame
         */
        public int skip_samples;
    }

    public unsafe struct InternalBuffer
    {
        public fixed int @base[8];
        public fixed int data[8];
        public fixed int linesize[8];
        public int width;
        public int height;
        public PixelFormat pix_fmt;
        public byte** extended_data;
        public int audio_data_size;
        public int nb_channels;
    }
}
