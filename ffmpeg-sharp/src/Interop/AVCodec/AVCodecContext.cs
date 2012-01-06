#region LGPL License
//
// AVCodecContext.cs
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
using FFmpegSharp.Interop.Util;

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
                                        ref IntPtr arg2, ref int ret, int count);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVCodecContext
    {
        /**
         * Info on struct for av_log
         * - set by avcodec_alloc_context
         */
        public AVClass* av_class;

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
         * CODEC_FLAG_*.
         * - encoding: set by user.
         * - decoding: set by user.
         */
        public CODEC_FLAG flags;

        /**
         * some codecs needs additionnal format info. It is stored here
         * - encoding: set by user.
         * - decoding: set by lavc. (FIXME is this ok?)
         */
        public int sub_id;

        /**
         * motion estimation algorithm used for video coding.
         * 1 (zero), 2 (full), 3 (log), 4 (phods), 5 (epzs), 6 (x1), 7 (hex),
         * 8 (umh), 9 (iter) [7, 8 are x264 specific, 9 is snow specific]
         * - encoding: MUST be set by user.
         * - decoding: unused
         */
        public int me_method;

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

        /* video only */
        /**
         * picture width / height.
         * - encoding: MUST be set by user.
         * - decoding: set by lavc.
         * Note, for compatibility its possible to set this instead of
         * coded_width/height before decoding
         */
        public int width;
        public int height;

        /**
         * the number of pictures in a group of pitures, or 0 for intra_only.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int gop_size;

        /**
         * pixel format, see PIX_FMT_xxx.
         * - encoding: set by user.
         * - decoding: set by lavc.
         */
        public PixelFormat pix_fmt;

        /**
         * Frame rate emulation. If not zero lower layer (i.e. format handler)
         * has to read frames at native frame rate.
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int rate_emu;

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

        /* audio only */
        public int sample_rate; // samples per sec
        public int channels;

        /**
         * audio sample format.
         * - encoding: set by user.
         * - decoding: set by lavc.
         */
        public SampleFormat sample_fmt;  // sample format, currenly unused

        /* the following data should not be initialized */
        /**
         * samples per packet. initialized when calling 'init'
         */
        public int frame_size;

        public int frame_number;   // audio or video frame number

        public int real_pict_num;  // returns the real picture number of previous encoded frame

        /**
         * number of frames the decoded output will be delayed relative to
         * the encoded input.
         * - encoding: set by lavc.
         * - decoding: unused
         */
        public int delay;

        /* - encoding parameters */
        public float qcompress;  // amount of qscale change between easy & hard scenes (0.0-1.0)

        public float qblur;      // amount of qscale smoothing over time (0.0-1.0)

        /**
         * minimum quantizer.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int qmin;

        /**
         * maximum quantizer.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int qmax;

        /**
         * maximum quantizer difference etween frames.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int max_qdiff;

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

        /**
         * hurry up amount.
         * deprecated in favor of skip_idct and skip_frame
         * - encoding: unused
         * - decoding: set by user. 1-> skip b frames, 2-> skip idct/dequant too, 5-> skip everything except header
         */
        [Obsolete("Deprecated in favor of skip_idct and skip_frame.")]
        public int hurry_up;

        public AVCodec* codec;

        public void* priv_data;

#if !LIBAVCODEC_52
        /* unused, FIXME remove*/
        public int rtp_mode;
#endif

        /* The size of the RTP payload: the coder will  */
        /* do it's best to deliver a chunk with size    */
        /* below rtp_payload_size, the chunk will start */
        /* with a start code on some codecs like H.263  */
        /* This doesn't take account of any particular  */
        /* headers inside the transmited RTP payload    */
        public int rtp_payload_size;

        /* The RTP callback: This function is called   */
        /* every time the encoder has a packet to send */
        /* Depends on the encoder if the data starts   */
        /* with a Start Code (it should) H.263 does.   */
        /* mb_nb contains the number of macroblocks    */
        /* encoded in the RTP payload                  */
        public IntPtr rtp_callback_ptr;
        public RtpCallback rtp_callback
        {
            get { return (Utils.GetDelegate<RtpCallback>(rtp_callback_ptr)); }
        }

        /* statistics, used for 2-pass encoding */
        public int mv_bits;

        public int header_bits;

        public int i_tex_bits;

        public int p_tex_bits;

        public int i_count;

        public int p_count;

        public int skip_count;

        public int misc_bits;

        /**
         * number of bits used for the previously encoded frame.
         * - encoding: set by lavc
         * - decoding: unused
         */
        public int frame_bits;

        /**
         * private data of the user, can be used to carry app specific stuff.
         * - encoding: set by user
         * - decoding: set by user
         */
        public void* opaque;

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

        public CodecType codec_type; /* see CODEC_TYPE_xxx */

        public CodecID codec_id; /* see CODEC_ID_xxx */

        /**
         * fourcc (LSB first, so "ABCD" -> ('D'&lt&lt24) + ('C'&lt&lt16) + ('B'&lt&lt8) + 'A').
         * this is used to workaround some encoder bugs
         * - encoding: set by user, if not then the default based on codec_id will be used
         * - decoding: set by user, will be converted to upper case by lavc during init
         */
        public uint codec_tag;

        /**
         * workaround bugs in encoders which sometimes cannot be detected automatically.
         * - encoding: set by user
         * - decoding: set by user
         */
        public FF_BUG workaround_bugs;

        /**
         * luma single coeff elimination threshold.
         * - encoding: set by user
         * - decoding: unused
         */
        public int luma_elim_threshold;

        /**
         * chroma single coeff elimination threshold.
         * - encoding: set by user
         * - decoding: unused
         */
        public int chroma_elim_threshold;

        /**
         * strictly follow the std (MPEG4, ...).
         * - encoding: set by user
         * - decoding: unused
         */
        public FF_COMPLIANCE strict_std_compliance;

        /**
         * qscale offset between ip and b frames.
         * if &gt 0 then the last p frame quantizer will be used (q= lastp_q*factor+offset)
         * if &lt 0 then normal ratecontrol will be done (q= -normal_q*factor+offset)
         * - encoding: set by user.
         * - decoding: unused
         */
        public float b_quant_offset;

        /**
         * error resilience higher values will detect more errors but may missdetect
         * some more or less valid parts as errors.
         * - encoding: unused
         * - decoding: set by user
         */
        public FF_ER error_resilience;

        /**
          * called at the beginning of each frame to get a buffer for it.
          * if pic.reference is set then the frame will be read later by lavc
          * avcodec_align_dimensions() should be used to find the required width and
          * height, as they normally need to be rounded up to the next multiple of 16
          * - encoding: unused
          * - decoding: set by lavc, user can override
          */
        public IntPtr get_buffer_ptr;
        public GetBufferCallback get_buffer
        {
            get { return (Utils.GetDelegate<GetBufferCallback>(get_buffer_ptr)); }
        }

        /**
         * called to release buffers which where allocated with get_buffer.
         * a released buffer can be reused in get_buffer()
         * pic.data[*] must be set to NULL
         * - encoding: unused
         * - decoding: set by lavc, user can override
         */
        public IntPtr release_buffer_ptr;
        public ReleaseBufferCallback release_buffer
        {
            get { return (Utils.GetDelegate<ReleaseBufferCallback>(release_buffer_ptr)); }
        }

        /**
         * if 1 the stream has a 1 frame delay during decoding.
         * - encoding: set by lavc
         * - decoding: set by lavc
         */
        public bool has_b_frames;

        /**
         * number of bytes per packet if constant and known or 0
         * used by some WAV based audio codecs
         */
        public int block_align;

        /* - decoding only: if true, only parsing is done
                   (function avcodec_parse_frame()). The frame
                   data is returned. Only MPEG codecs support this now. */
        public bool parse_only;

        /**
         * 0-> h263 quant 1-> mpeg quant.
         * - encoding: set by user.
         * - decoding: unused
         */
        public MpegQuant mpeg_quant;

        /**
         * pass1 encoding statistics output buffer.
         * - encoding: set by lavc
         * - decoding: unused
         */
        private sbyte* stats_out_ptr; // char* stats_out
        public string stats_out
        {
            get { return new string(stats_out_ptr); }
        }

        /**
         * pass2 encoding statistics input buffer.
         * concatenated stuff from stats_out of pass1 should be placed here
         * - encoding: allocated/set/freed by user
         * - decoding: unused
         */
        private sbyte* stats_in_ptr;// char *stats_in
        public string stats_in
        {
            get { return new string(stats_in_ptr); }
        }

        /**
         * ratecontrol qmin qmax limiting method.
         * 0-> clipping, 1-> use a nice continous function to limit qscale wthin qmin/qmax
         * - encoding: set by user.
         * - decoding: unused
         */
        public float rc_qsquish;

        public float rc_qmod_amp;

        public int rc_qmod_freq;

        /**
         * ratecontrol override, see RcOverride.
         * - encoding: allocated/set/freed by user.
         * - decoding: unused
         */
        public RcOverride* rc_override;
        public int rc_override_count;

        /**
         * rate control equation.
         * - encoding: set by user
         * - decoding: unused
         */

        private sbyte* rc_eq_ptr; // char* rc_eq;
        public string rc_eq
        {
            get { return new string(rc_eq_ptr); }
        }

        /**
         * maximum bitrate.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int rc_max_rate;

        /**
         * minimum bitrate.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int rc_min_rate;

        /**
         * decoder bitstream buffer size.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int rc_buffer_size;

        public float rc_buffer_aggressivity;

        /**
         * qscale factor between p and i frames.
         * if &gt 0 then the last p frame quantizer will be used (q= lastp_q*factor+offset)
         * if &lt 0 then normal ratecontrol will be done (q= -normal_q*factor+offset)
         * - encoding: set by user.
         * - decoding: unused
         */
        public float i_quant_factor;

        /**
         * qscale offset between p and i frames.
         * - encoding: set by user.
         * - decoding: unused
         */
        public float i_quant_offset;

        /**
         * initial complexity for pass1 ratecontrol.
         * - encoding: set by user.
         * - decoding: unused
         */
        public float rc_initial_cplx;

        /**
         * dct algorithm, see FF_DCT_* below.
         * - encoding: set by user
         * - decoding: unused
         */
        public FF_DCT dct_algo;

        /**
         * luminance masking (0-> disabled).
         * - encoding: set by user
         * - decoding: unused
         */
        public float lumi_masking;

        /**
         * temporary complexity masking (0-> disabled).
         * - encoding: set by user
         * - decoding: unused
         */
        public float temporal_cplx_masking;

        /**
         * spatial complexity masking (0-> disabled).
         * - encoding: set by user
         * - decoding: unused
         */
        public float spatial_cplx_masking;

        /**
         * p block masking (0-> disabled).
         * - encoding: set by user
         * - decoding: unused
         */
        public float p_masking;

        /**
         * darkness masking (0-> disabled).
         * - encoding: set by user
         * - decoding: unused
         */
        public float dark_masking;

#if !LIBAVCODEC_52
        /* for binary compatibility */
        public int unused;
#endif

        /**
         * idct algorithm, see FF_IDCT_* below.
         * - encoding: set by user
         * - decoding: set by user
         */
        public FF_IDCT idct_algo;

        /**
         * slice count.
         * - encoding: set by lavc
         * - decoding: set by user (or 0)
         */
        public int slice_count;

        /**
         * slice offsets in the frame in bytes.
         * - encoding: set/allocated by lavc
         * - decoding: set/allocated by user (or NULL)
         */
        public int* slice_offset;

        /**
         * error concealment flags.
         * - encoding: unused
         * - decoding: set by user
         */
        public FF_EC error_concealment;

        /**
         * dsp_mask could be add used to disable unwanted CPU features
         * CPU features (i.e. MMX, SSE. ...)
         *
         * with FORCE flag you may instead enable given CPU features
         * (Dangerous: usable in case of misdetection, improper usage however will
         * result into program crash)
         */
        public FF_MM dsp_mask;

        /**
         * bits per sample/pixel from the demuxer (needed for huffyuv).
         * - encoding: set by lavc
         * - decoding: set by user
         */
        public int bits_per_sample;

        /**
         * prediction method (needed for huffyuv).
         * - encoding: set by user
         * - decoding: unused
         */
        public FF_PRED prediction_method;

        /**
         * sample aspect ratio (0 if unknown).
         * numerator and denominator must be relative prime and smaller then 256 for some video standards
         * - encoding: set by user.
         * - decoding: set by lavc.
         */
        public AVRational sample_aspect_ratio;

        /**
         * the picture in the bitstream.
         * - encoding: set by lavc
         * - decoding: set by lavc
         */
        public AVFrame* coded_frame;

        /**
         * debug.
         * - encoding: set by user.
         * - decoding: set by user.
         */
        public FF_DEBUG debug;

        /**
         * debug.
         * - encoding: set by user.
         * - decoding: set by user.
         */
        public FF_DEBUG_VIS debug_mv;

        /**
         * error.
         * - encoding: set by lavc if flags&CODEC_FLAG_PSNR
         * - decoding: unused
         */
        public fixed ulong error[4];

        /**
         * minimum MB quantizer.
         * - encoding: unused
         * - decoding: unused
         */
        public int mb_qmin;

        /**
         * maximum MB quantizer.
         * - encoding: unused
         * - decoding: unused
         */
        public int mb_qmax;

        /**
         * motion estimation compare function.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int me_cmp;


        /**
         * subpixel motion estimation compare function.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int me_sub_cmp;

        /**
         * macroblock compare function (not supported yet).
         * - encoding: set by user.
         * - decoding: unused
         */
        public int mb_cmp;

        /**
         * interlaced dct compare function
         * - encoding: set by user.
         * - decoding: unused
         */
        public FF_CMP ildct_cmp;

        /**
         * ME diamond size & shape.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int dia_size;

        /**
         * amount of previous MV predictors (2a+1 x 2a+1 square).
         * - encoding: set by user.
         * - decoding: unused
         */
        public int last_predictor_count;

        /**
         * pre pass for motion estimation.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int pre_me;

        /**
         * motion estimation pre pass compare function.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int me_pre_cmp;

        /**
         * ME pre pass diamond size & shape.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int pre_dia_size;

        /**
         * subpel ME quality.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int me_subpel_quality;

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
         * DTG active format information (additionnal aspect ratio
         * information only used in DVB MPEG2 transport streams). 0 if
         * not set.
         *
         * - encoding: unused.
         * - decoding: set by decoder
         */
        public FF_DTG dtg_active_format;

        /**
         * Maximum motion estimation search range in subpel units.
         * if 0 then no limit
         *
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int me_range;

        /**
         * intra quantizer bias.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int intra_quant_bias;

        /**
         * inter quantizer bias.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int inter_quant_bias;

        /**
         * color table ID.
         * - encoding: unused.
         * - decoding: which clrtable should be used for 8bit RGB images
         *             table have to be stored somewhere FIXME
         */
        public int color_table_id;

        /**
         * internal_buffer count.
         * Don't touch, used by lavc default_get_buffer()
         */
        public int internal_buffer_count;

        /**
         * internal_buffers.
         * Don't touch, used by lavc default_get_buffer()
         */
        public void* internal_buffer; // void* internal_buffer;

        /**
         * global quality for codecs which cannot change it per frame.
         * this should be proportional to MPEG1/2/4 qscale.
         * - encoding: set by user.
         * - decoding: unused
         */
        public GlobalQuality global_quality;

        /**
         * coder type
         * - encoding: set by user.
         * - decoding: unused
         */
        public FF_CODER_TYPE coder_type;

        /**
         * context model
         * - encoding: set by user.
         * - decoding: unused
         */
        public int context_model;

        /**
          * slice flags
          * - encoding: unused
          * - decoding: set by user.
          */
        public SLICE_FLAG slice_flags;

        /**
         * XVideo Motion Acceleration
         * - encoding: forbidden
         * - decoding: set by decoder
         */
        public int xvmc_acceleration;

        /**
         * macroblock decision mode
         * - encoding: set by user.
         * - decoding: unused
         */
        public FF_MB_DECISION mb_decision;

        /**
         * custom intra quantization matrix
         * - encoding: set by user, can be NULL
         * - decoding: set by lavc
         */
        public ushort* intra_matrix;

        /**
         * custom inter quantization matrix
         * - encoding: set by user, can be NULL
         * - decoding: set by lavc
         */
        public ushort* inter_matrix;

        /**
         * fourcc from the AVI stream header (LSB first, so "ABCD" -> ('D'&lt&lt24) + ('C'&lt&lt16) + ('B'&lt&lt8) + 'A').
         * this is used to workaround some encoder bugs
         * - encoding: unused
         * - decoding: set by user, will be converted to upper case by lavc during init
         */
        public uint stream_codec_tag;

        /**
         * scene change detection threshold.
         * 0 is default, larger means fewer detected scene changes
         * - encoding: set by user.
         * - decoding: unused
         */
        public int scenechange_threshold;

        /**
         * minimum lagrange multipler
         * - encoding: set by user.
         * - decoding: unused
         */
        public int lmin;

        /**
         * maximum lagrange multipler
         * - encoding: set by user.
         * - decoding: unused
         */
        public int lmax;

        /**
         * Palette control structure
         * - encoding: ??? (no palette-enabled encoder yet)
         * - decoding: set by user.
         */
#pragma warning disable 618
        public AVPaletteControl* palctrl;
#pragma warning restore 618

        /**
         * noise reduction strength
         * - encoding: set by user.
         * - decoding: unused
         */
        public int noise_reduction;

        /**
         * called at the beginning of a frame to get cr buffer for it.
         * buffer type (size, hints) must be the same. lavc won't check it.
         * lavc will pass previous buffer in pic, function should return
         * same buffer or new buffer with old frame "painted" into it.
         * if pic.data[0] == NULL must behave like get_buffer().
         * - encoding: unused
         * - decoding: set by lavc, user can override
         */
        private IntPtr reget_buffer_ptr;
        public RegetBufferCallback reget_buffer
        {
            get { return (Utils.GetDelegate<RegetBufferCallback>(reget_buffer_ptr)); }
        }

        /**
         * number of bits which should be loaded into the rc buffer before decoding starts
         * - encoding: set by user.
         * - decoding: unused
         */
        public int rc_initial_buffer_occupancy;

        /**
         *
         * - encoding: set by user.
         * - decoding: unused
         */
        public int inter_threshold;

        /**
         * CODEC_FLAG2_*.
         * - encoding: set by user.
         * - decoding: set by user.
         */
        public CODEC_FLAG2 flags2;

        /**
         * simulates errors in the bitstream to test error concealment.
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int error_rate;

        /**
         * MP3 antialias algorithm, see FF_AA_* below.
         * - encoding: unused
         * - decoding: set by user
         */
        public FF_AA antialias_algo;

        /**
         * Quantizer noise shaping.
         * - encoding: set by user
         * - decoding: unused
         */
        public int quantizer_noise_shaping;

        /**
         * Thread count.
         * is used to decide how many independant tasks should be passed to execute()
         * - encoding: set by user
         * - decoding: set by user
         */
        public int thread_count;

        /**
         * the codec may call this to execute several independant things. it will return only after
         * finishing all tasks, the user may replace this with some multithreaded implementation, the
         * default implementation will execute the parts serially
         * @param count the number of things to execute
         * - encoding: set by lavc, user can override
         * - decoding: set by lavc, user can override
         */
        private IntPtr execute_ptr;
        public ExecuteCallback execute
        {
            get { return (Utils.GetDelegate<ExecuteCallback>(execute_ptr)); }
        }

        /**
         * Thread opaque.
         * can be used by execute() to store some per AVCodecContext stuff.
         * - encoding: set by execute()
         * - decoding: set by execute()
         */
        public void* thread_opaque; // void* thread_opaque;

        /**
         * Motion estimation threshold. under which no motion estimation is
         * performed, but instead the user specified motion vectors are used
         *
         * - encoding: set by user
         * - decoding: unused
         */
        public int me_threshold;

        /**
         * Macroblock threshold. under which the user specified macroblock types will be used
         * - encoding: set by user
         * - decoding: unused
         */
        public int mb_threshold;

        /**
         * precision of the intra dc coefficient - 8.
         * - encoding: set by user
         * - decoding: unused
         */
        public int intra_dc_precision;

        /**
         * noise vs. sse weight for the nsse comparsion function.
         * - encoding: set by user
         * - decoding: unused
         */
        public int nsse_weight;

        /**
         * number of macroblock rows at the top which are skipped.
         * - encoding: unused
         * - decoding: set by user
         */
        public int skip_top;

        /**
         * number of macroblock rows at the bottom which are skipped.
         * - encoding: unused
         * - decoding: set by user
         */
        public int skip_bottom;

        /**
         * profile
         * - encoding: set by user
         * - decoding: set by lavc
         */
        public FF_PROFILE profile;

        /**
         * level
         * - encoding: set by user
         * - decoding: set by lavc
         */
        public int level;

        /**
         * low resolution decoding. 1-> 1/2 size, 2->1/4 size
         * - encoding: unused
         * - decoding: set by user
         */
        public int lowres;

        /**
         * bitsream width / height. may be different from width/height if lowres
         * or other things are used
         * - encoding: unused
         * - decoding: set by user before init if known, codec should override / dynamically change if needed
         */
        public int coded_width, coded_height;

        /**
         * frame skip threshold
         * - encoding: set by user
         * - decoding: unused
         */
        public int frame_skip_threshold;

        /**
         * frame skip factor
         * - encoding: set by user
         * - decoding: unused
         */
        public int frame_skip_factor;


        /**
         * frame skip exponent
         * - encoding: set by user
         * - decoding: unused
         */
        public int frame_skip_exp;

        /**
         * frame skip comparission function
         * - encoding: set by user.
         * - decoding: unused
         */
        public int frame_skip_cmp;

        /**
         * border processing masking. raises the quantizer for mbs on the borders
         * of the picture.
         * - encoding: set by user
         * - decoding: unused
         */
        public float border_masking;

        /**
         * minimum MB lagrange multipler.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int mb_lmin;

        /**
         * maximum MB lagrange multipler.
         * - encoding: set by user.
         * - decoding: unused
         */
        public int mb_lmax;

        /**
         *
         * - encoding: set by user.
         * - decoding: unused
         */
        public int me_penalty_compensation;

        /**
         *
         * - encoding: unused
         * - decoding: set by user.
         */
        public AVDiscard skip_loop_filter;

        /**
        *
        * - encoding: unused
        * - decoding: Set by user.
        */
        public AVDiscard skip_idct;

        /**
         *
         * - encoding: unused
         * - decoding: set by user.
         */
        public AVDiscard skip_frame;

        /**
         *
         * - encoding: set by user.
         * - decoding: unused
         */
        public int bidir_refine;

        /**
         *
         * - encoding: set by user.
         * - decoding: unused
         */
        public int brd_scale;

        /**
         * constant rate factor - quality-based VBR - values ~correspond to qps
         * - encoding: set by user.
         * - decoding: unused
         */
        public float crf;


        /**
         * constant quantization parameter rate control method
         * - encoding: set by user.
         * - decoding: unused
         */
        public int cqp;

        /**
         * minimum gop size
         * - encoding: set by user.
         * - decoding: unused
         */
        public int keyint_min;

        /**
         * number of reference frames
         * - encoding: set by user.
         * - decoding: unused
         */
        public int refs;

        /**
         * chroma qp offset from luma
         * - encoding: set by user.
         * - decoding: unused
         */
        public int chromaoffset;

        /**
         * influences how often b-frames are used
         * - encoding: set by user.
         * - decoding: unused
         */
        public int bframebias;

        /**
         * trellis RD quantization
         * - encoding: set by user.
         * - decoding: unused
         */
        public int trellis;

        /**
         * reduce fluctuations in qp (before curve compression)
         * - encoding: set by user.
         * - decoding: unused
         */
        public float complexityblur;

        /**
         * in-loop deblocking filter alphac0 parameter
         * alpha is in the range -6...6
         * - encoding: set by user.
         * - decoding: unused
         */
        public int deblockalpha;

        /**
         * in-loop deblocking filter beta parameter
         * beta is in the range -6...6
         * - encoding: set by user.
         * - decoding: unused
         */
        public int deblockbeta;

        /**
         * macroblock subpartition sizes to consider - p8x8, p4x4, b8x8, i8x8, i4x4
         * - encoding: set by user.
         * - decoding: unused
         */
        public X264_PART partitions;

        /**
         * direct mv prediction mode - 0 (none), 1 (spatial), 2 (temporal)
         * - encoding: set by user.
         * - decoding: unused
         */
        public int directpred;

        /**
         * audio cutoff bandwidth (0 means "automatic") . Currently used only by FAAC
         * - encoding: set by user.
         * - decoding: unused
         */
        public int cutoff;

        /**
         * multiplied by qscale for each frame and added to scene_change_score
         * - encoding: set by user.
         * - decoding: unused
         */
        public int scenechange_factor;

        /**
         *
         * note: value depends upon the compare functin used for fullpel ME
         * - encoding: set by user.
         * - decoding: unused
         */
        public int mv0_threshold;

        /**
         * adjusts sensitivity of b_frame_strategy 1
         * - encoding: set by user.
         * - decoding: unused
         */
        public int b_sensitivity;

        /**
         * - encoding: set by user.
         * - decoding: unused
         */
        public int compression_level;

        /**
         * sets whether to use LPC mode - used by FLAC encoder
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int use_lpc;

        /**
         * LPC coefficient precision - used by FLAC encoder
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int lpc_coeff_precision;

        /**
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int min_prediction_order;

        /**
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int max_prediction_order;

        /**
         * search method for selecting prediction order
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int prediction_order_method;

        /**
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int min_partition_order;

        /**
         * - encoding: set by user.
         * - decoding: unused.
         */
        public int max_partition_order;

        /**
        * GOP timecode frame start number, in non drop frame format
        * - encoding: Set by user.
        * - decoding: unused
        */
        public long timecode_frame_start;

        /**
        * Decoder should decode to this many channels if it can (0 for default)
        * - encoding: unused
        * - decoding: Set by user.
        */
        public int request_channels;

        /**
         * Percentage of dynamic range compression to be applied by the decoder.
         * The default value is 1.0, corresponding to full compression.
         * - encoding: unused
         * - decoding: Set by user.
         */
        public float drc_scale;
    };
}
