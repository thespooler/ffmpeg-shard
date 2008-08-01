#region LGPL License
//
// Enums.cs
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
namespace FFmpegSharp.Interop.Codec
{
    /// <summary>
    /// Identifies the syntax and semantics of the bitstream.
    /// The principle is roughly:
    /// Two decoders with the same ID can decode the same streams.
    /// Two encoders with the same ID can encode compatible streams.
    /// There may be slight deviations from the principle due to implementation
    /// details.
    /// </summary>
    public enum CodecID
    {
        CODEC_ID_NONE,
        CODEC_ID_MPEG1VIDEO,
        CODEC_ID_MPEG2VIDEO, /* prefered ID for MPEG Video 1 or 2 decoding */
        CODEC_ID_MPEG2VIDEO_XVMC,
        CODEC_ID_H261,
        CODEC_ID_H263,
        CODEC_ID_RV10,
        CODEC_ID_RV20,
        CODEC_ID_MJPEG,
        CODEC_ID_MJPEGB,
        CODEC_ID_LJPEG,
        CODEC_ID_SP5X,
        CODEC_ID_JPEGLS,
        CODEC_ID_MPEG4,
        CODEC_ID_RAWVIDEO,
        CODEC_ID_MSMPEG4V1,
        CODEC_ID_MSMPEG4V2,
        CODEC_ID_MSMPEG4V3,
        CODEC_ID_WMV1,
        CODEC_ID_WMV2,
        CODEC_ID_H263P,
        CODEC_ID_H263I,
        CODEC_ID_FLV1,
        CODEC_ID_SVQ1,
        CODEC_ID_SVQ3,
        CODEC_ID_DVVIDEO,
        CODEC_ID_HUFFYUV,
        CODEC_ID_CYUV,
        CODEC_ID_H264,
        CODEC_ID_INDEO3,
        CODEC_ID_VP3,
        CODEC_ID_THEORA,
        CODEC_ID_ASV1,
        CODEC_ID_ASV2,
        CODEC_ID_FFV1,
        CODEC_ID_4XM,
        CODEC_ID_VCR1,
        CODEC_ID_CLJR,
        CODEC_ID_MDEC,
        CODEC_ID_ROQ,
        CODEC_ID_INTERPLAY_VIDEO,
        CODEC_ID_XAN_WC3,
        CODEC_ID_XAN_WC4,
        CODEC_ID_RPZA,
        CODEC_ID_CINEPAK,
        CODEC_ID_WS_VQA,
        CODEC_ID_MSRLE,
        CODEC_ID_MSVIDEO1,
        CODEC_ID_IDCIN,
        CODEC_ID_8BPS,
        CODEC_ID_SMC,
        CODEC_ID_FLIC,
        CODEC_ID_TRUEMOTION1,
        CODEC_ID_VMDVIDEO,
        CODEC_ID_MSZH,
        CODEC_ID_ZLIB,
        CODEC_ID_QTRLE,
        CODEC_ID_SNOW,
        CODEC_ID_TSCC,
        CODEC_ID_ULTI,
        CODEC_ID_QDRAW,
        CODEC_ID_VIXL,
        CODEC_ID_QPEG,
        CODEC_ID_XVID,
        CODEC_ID_PNG,
        CODEC_ID_PPM,
        CODEC_ID_PBM,
        CODEC_ID_PGM,
        CODEC_ID_PGMYUV,
        CODEC_ID_PAM,
        CODEC_ID_FFVHUFF,
        CODEC_ID_RV30,
        CODEC_ID_RV40,
        CODEC_ID_VC1,
        CODEC_ID_WMV3,
        CODEC_ID_LOCO,
        CODEC_ID_WNV1,
        CODEC_ID_AASC,
        CODEC_ID_INDEO2,
        CODEC_ID_FRAPS,
        CODEC_ID_TRUEMOTION2,
        CODEC_ID_BMP,
        CODEC_ID_CSCD,
        CODEC_ID_MMVIDEO,
        CODEC_ID_ZMBV,
        CODEC_ID_AVS,
        CODEC_ID_SMACKVIDEO,
        CODEC_ID_NUV,
        CODEC_ID_KMVC,
        CODEC_ID_FLASHSV,
        CODEC_ID_CAVS,
        CODEC_ID_JPEG2000,
        CODEC_ID_VMNC,
        CODEC_ID_VP5,
        CODEC_ID_VP6,
        CODEC_ID_VP6F,

        /* various pcm "codecs" */
        CODEC_ID_PCM_S16LE = 0x10000,
        CODEC_ID_PCM_S16BE,
        CODEC_ID_PCM_U16LE,
        CODEC_ID_PCM_U16BE,
        CODEC_ID_PCM_S8,
        CODEC_ID_PCM_U8,
        CODEC_ID_PCM_MULAW,
        CODEC_ID_PCM_ALAW,
        CODEC_ID_PCM_S32LE,
        CODEC_ID_PCM_S32BE,
        CODEC_ID_PCM_U32LE,
        CODEC_ID_PCM_U32BE,
        CODEC_ID_PCM_S24LE,
        CODEC_ID_PCM_S24BE,
        CODEC_ID_PCM_U24LE,
        CODEC_ID_PCM_U24BE,
        CODEC_ID_PCM_S24DAUD,

        /* various adpcm codecs */
        CODEC_ID_ADPCM_IMA_QT = 0x11000,
        CODEC_ID_ADPCM_IMA_WAV,
        CODEC_ID_ADPCM_IMA_DK3,
        CODEC_ID_ADPCM_IMA_DK4,
        CODEC_ID_ADPCM_IMA_WS,
        CODEC_ID_ADPCM_IMA_SMJPEG,
        CODEC_ID_ADPCM_MS,
        CODEC_ID_ADPCM_4XM,
        CODEC_ID_ADPCM_XA,
        CODEC_ID_ADPCM_ADX,
        CODEC_ID_ADPCM_EA,
        CODEC_ID_ADPCM_G726,
        CODEC_ID_ADPCM_CT,
        CODEC_ID_ADPCM_SWF,
        CODEC_ID_ADPCM_YAMAHA,
        CODEC_ID_ADPCM_SBPRO_4,
        CODEC_ID_ADPCM_SBPRO_3,
        CODEC_ID_ADPCM_SBPRO_2,

        /* AMR */
        CODEC_ID_AMR_NB = 0x12000,
        CODEC_ID_AMR_WB,

        /* RealAudio codecs*/
        CODEC_ID_RA_144 = 0x13000,
        CODEC_ID_RA_288,

        /* various DPCM codecs */
        CODEC_ID_ROQ_DPCM = 0x14000,
        CODEC_ID_INTERPLAY_DPCM,
        CODEC_ID_XAN_DPCM,
        CODEC_ID_SOL_DPCM,

        /* audio codecs */
        CODEC_ID_MP2 = 0x15000,
        CODEC_ID_MP3, /* prefered ID for MPEG Audio layer 1, 2 or3 decoding */
        CODEC_ID_AAC,
#if LIBAVCODEC_51
        CODEC_ID_MPEG4AAC = CODEC_ID_AAC,
#endif
        CODEC_ID_AC3,
        CODEC_ID_DTS,
        CODEC_ID_VORBIS,
        CODEC_ID_DVAUDIO,
        CODEC_ID_WMAV1,
        CODEC_ID_WMAV2,
        CODEC_ID_MACE3,
        CODEC_ID_MACE6,
        CODEC_ID_VMDAUDIO,
        CODEC_ID_SONIC,
        CODEC_ID_SONIC_LS,
        CODEC_ID_FLAC,
        CODEC_ID_MP3ADU,
        CODEC_ID_MP3ON4,
        CODEC_ID_SHORTEN,
        CODEC_ID_ALAC,
        CODEC_ID_WESTWOOD_SND1,
        CODEC_ID_GSM,
        CODEC_ID_QDM2,
        CODEC_ID_COOK,
        CODEC_ID_TRUESPEECH,
        CODEC_ID_TTA,
        CODEC_ID_SMACKAUDIO,
        CODEC_ID_QCELP,
        CODEC_ID_WAVPACK,
        CODEC_ID_DSICINAUDIO,
        CODEC_ID_IMC,
        CODEC_ID_MUSEPACK7,
        CODEC_ID_MLP,
        CODEC_ID_GSM_MS, /* as found in WAV */
        CODEC_ID_ATRAC3,
        CODEC_ID_VOXWARE,
        CODEC_ID_APE,
        CODEC_ID_NELLYMOSER,
        CODEC_ID_MUSEPACK8,
        CODEC_ID_SPEEX,
        CODEC_ID_WMAVOICE,
        CODEC_ID_WMAPRO,
        CODEC_ID_WMALOSSLESS,

        /* subtitle codecs */
        CODEC_ID_DVD_SUBTITLE = 0x17000,
        CODEC_ID_DVB_SUBTITLE,
        CODEC_ID_TEXT,  ///< raw UTF-8 text
        CODEC_ID_XSUB,
        CODEC_ID_SSA,
        CODEC_ID_MOV_TEXT,

        /* other specific kind of codecs (generally used for attachments) */
        CODEC_ID_TTF = 0x18000,

        CODEC_ID_MPEG2TS = 0x20000, /**< _FAKE_ codec to indicate a raw MPEG-2 TS
                                      * stream (only used by libavformat) */
#if LIBAVCODEC_51
        CODEC_ID_MP3LAME = CODEC_ID_MP3,
#endif
    };

    public enum CodecType
    {
        CODEC_TYPE_UNKNOWN = -1,
        CODEC_TYPE_VIDEO,
        CODEC_TYPE_AUDIO,
        CODEC_TYPE_DATA,
        CODEC_TYPE_SUBTITLE,
        CODEC_TYPE_ATTACHMENT,
        CODEC_TYPE_NB
    };

    /* currently unused, may be used if 24/32 bits samples ever supported */
    /* all in native endian */
    public enum SampleFormat
    {
        SAMPLE_FMT_NONE = -1,
        SAMPLE_FMT_U8,              ///< unsigned 8 bits
        SAMPLE_FMT_S16,             ///< signed 16 bits
        SAMPLE_FMT_S24,             ///< signed 24 bits
        SAMPLE_FMT_S32,             ///< signed 32 bits
        SAMPLE_FMT_FLT,             ///< float
    };

    public enum Motion_Est_ID
    {
        ME_ZERO = 1,
        ME_FULL,
        ME_LOG,
        ME_PHODS,
        ME_EPZS,
        ME_X1,
        ME_HEX,
        ME_UMH,
        ME_ITER,
    };

    public enum AVDiscard
    {
        //we leave some space between them for extensions (drop some keyframes for intra only or drop just some bidir frames)
        AVDISCARD_NONE = -16, ///< discard nothing
        AVDISCARD_DEFAULT = 0, ///< discard useless packets like 0 size packets in avi
        AVDISCARD_NONREF = 8, ///< discard all non reference
        AVDISCARD_BIDIR = 16, ///< discard all bidirectional frames
        AVDISCARD_NONKEY = 32, ///< discard all frames except keyframes
        AVDISCARD_ALL = 48, ///< discard all
    };

    public enum FF_LOSS : int
    {
        /// <summary>
        /// loss due to resolution change
        /// </summary>
        Resolution = FFmpeg.FF_LOSS_RESOLUTION,
        /// <summary>
        /// loss due to color depth change
        /// </summary>
        Depth = FFmpeg.FF_LOSS_DEPTH,
        /// <summary>
        /// loss due to color space conversion 
        /// </summary>
        ColorSpace = FFmpeg.FF_LOSS_COLORSPACE,
        /// <summary>
        /// loss of alpha bits 
        /// </summary>
        Alpha = FFmpeg.FF_LOSS_ALPHA,
        /// <summary>
        /// loss due to color quantization 
        /// </summary>
        ColorQuant = FFmpeg.FF_LOSS_COLORQUANT,
        /// <summary>
        /// loss of chroma (e.g. RGB to gray conversion) 
        /// </summary>
        Chroma = FFmpeg.FF_LOSS_CHROMA
    }

    [Flags]
    public enum FF_ALPHA
    {
        SemiTransparent = FFmpeg.FF_ALPHA_SEMI_TRANSP,
        Transparent = FFmpeg.FF_ALPHA_TRANSP
    }

    [Flags]
    public enum CODEC_CAP
    {
        DrawHorizBand = FFmpeg.CODEC_CAP_DRAW_HORIZ_BAND,
        DR1 = FFmpeg.CODEC_CAP_DR1,
        ParseOnly = FFmpeg.CODEC_CAP_PARSE_ONLY,
        Truncated = FFmpeg.CODEC_CAP_TRUNCATED,
        HwAccel = FFmpeg.CODEC_CAP_HWACCEL,
        Delay = FFmpeg.CODEC_CAP_DELAY,
        SmallLastFrame = FFmpeg.CODEC_CAP_SMALL_LAST_FRAME
    }

    public enum FF_PROFILE
    {
        AacLow = FFmpeg.FF_PROFILE_AAC_LOW,
        AacLtp = FFmpeg.FF_PROFILE_AAC_LTP,
        AacMain = FFmpeg.FF_PROFILE_AAC_MAIN,
        AacSsr = FFmpeg.FF_PROFILE_AAC_SSR,
        Unknown = FFmpeg.FF_PROFILE_UNKNOWN,
    }

    public enum X264_PART
    {
        B8X8 = FFmpeg.X264_PART_B8X8,
        I4X4 = FFmpeg.X264_PART_I4X4,
        I8X8 = FFmpeg.X264_PART_I8X8,
        P4X4 = FFmpeg.X264_PART_P4X4,
        P8X8 = FFmpeg.X264_PART_P8X8,
    }

    public enum FF_AA
    {
        Auto = FFmpeg.FF_AA_AUTO,
        FastInt = FFmpeg.FF_AA_FASTINT,
        Float = FFmpeg.FF_AA_FLOAT,
        Int = FFmpeg.FF_AA_INT
    }

    public enum FF_BUG
    {
        AcVLC = FFmpeg.FF_BUG_AC_VLC,
        Amv = FFmpeg.FF_BUG_AMV,
        AutoDetect = FFmpeg.FF_BUG_AUTODETECT,
        DcClip = FFmpeg.FF_BUG_DC_CLIP,
        BlockSize = FFmpeg.FF_BUG_DIRECT_BLOCKSIZE,
        Edge = FFmpeg.FF_BUG_EDGE,
        HpelChroma = FFmpeg.FF_BUG_HPEL_CHROMA,
        MS = FFmpeg.FF_BUG_MS,
        NoPadding = FFmpeg.FF_BUG_NO_PADDING,
        OldMsMPEG4 = FFmpeg.FF_BUG_OLD_MSMPEG4,
        QpelChroma = FFmpeg.FF_BUG_QPEL_CHROMA,
        QpelChroma2 = FFmpeg.FF_BUG_QPEL_CHROMA2,
        StdQpel = FFmpeg.FF_BUG_STD_QPEL,
        Ump4 = FFmpeg.FF_BUG_UMP4,
        XvidInterlace = FFmpeg.FF_BUG_XVID_ILACE,
    }

    public enum FF_COMPLIANCE
    {
        Experimental = FFmpeg.FF_COMPLIANCE_EXPERIMENTAL,
        Inofficial = FFmpeg.FF_COMPLIANCE_INOFFICIAL,
        Normal = FFmpeg.FF_COMPLIANCE_NORMAL,
        Strict = FFmpeg.FF_COMPLIANCE_STRICT,
        VeryStrict = FFmpeg.FF_COMPLIANCE_VERY_STRICT,
    }

    public enum FF_ER
    {
        Aggressive = FFmpeg.FF_ER_AGGRESSIVE,
        Careful = FFmpeg.FF_ER_CAREFUL,
        Compliant = FFmpeg.FF_ER_COMPLIANT,
        VeryAggressive = FFmpeg.FF_ER_VERY_AGGRESSIVE,
    }

    public enum MpegQuant
    {
        H263 = 0,
        Mpeg = 1
    }

    public enum FF_DCT
    {
        Altivec = FFmpeg.FF_DCT_ALTIVEC,
        Auto = FFmpeg.FF_DCT_AUTO,
        Faan = FFmpeg.FF_DCT_FAAN,
        FastInt = FFmpeg.FF_DCT_FASTINT,
        Int = FFmpeg.FF_DCT_INT,
        Mlib = FFmpeg.FF_DCT_MLIB,
        MMX = FFmpeg.FF_DCT_MMX
    }

    public enum FF_IDCT
    {
        Altivec = FFmpeg.FF_IDCT_ALTIVEC,
        ARM = FFmpeg.FF_IDCT_ARM,
        Auto = FFmpeg.FF_IDCT_AUTO,
        Cavs = FFmpeg.FF_IDCT_CAVS,
        Faan = FFmpeg.FF_IDCT_FAAN,
        H264 = FFmpeg.FF_IDCT_H264,
        Int = FFmpeg.FF_IDCT_INT,
        Ipp = FFmpeg.FF_IDCT_IPP,
        LibMpeg2MMX = FFmpeg.FF_IDCT_LIBMPEG2MMX,
        Mlib = FFmpeg.FF_IDCT_MLIB,
        PS2 = FFmpeg.FF_IDCT_PS2,
        SH4 = FFmpeg.FF_IDCT_SH4,
        Simple = FFmpeg.FF_IDCT_SIMPLE,
        SimpleARM = FFmpeg.FF_IDCT_SIMPLEARM,
        SimpleARMV5TE = FFmpeg.FF_IDCT_SIMPLEARMV5TE,
        SimpleARMV6 = FFmpeg.FF_IDCT_SIMPLEARMV6,
        SimpleMMX = FFmpeg.FF_IDCT_SIMPLEMMX,
        SimpleVis = FFmpeg.FF_IDCT_SIMPLEVIS,
        VP3 = FFmpeg.FF_IDCT_VP3,
        Wmv2 = FFmpeg.FF_IDCT_WMV2,
        XvidMMX = FFmpeg.FF_IDCT_XVIDMMX
    }

    [Flags]
    public enum FF_EC
    {
        DeBlock = FFmpeg.FF_EC_DEBLOCK,
        GuessMVS = FFmpeg.FF_EC_GUESS_MVS,
    }

    [Flags]
    public enum FF_MM : uint
    {
        ThreeDNow = FFmpeg.FF_MM_3DNOW,
        ThreeDNowExt = FFmpeg.FF_MM_3DNOWEXT,
        Force = FFmpeg.FF_MM_FORCE,
        IwMMXt = FFmpeg.FF_MM_IWMMXT,
        MMX = FFmpeg.FF_MM_MMX,
        MMXExt = FFmpeg.FF_MM_MMXEXT,
        SSE = FFmpeg.FF_MM_SSE,
        SSE2 = FFmpeg.FF_MM_SSE2,
        SSE3 = FFmpeg.FF_MM_SSE3,
        SSSE3 = FFmpeg.FF_MM_SSSE3,
    }

    public enum FF_PRED
    {
        Left = FFmpeg.FF_PRED_LEFT,
        Median = FFmpeg.FF_PRED_MEDIAN,
        Plane = FFmpeg.FF_PRED_PLANE,
    }

    public enum FF_DEBUG
    {
        BitStream = FFmpeg.FF_DEBUG_BITSTREAM,
        Bugs = FFmpeg.FF_DEBUG_BUGS,
        DCTCoeff = FFmpeg.FF_DEBUG_DCT_COEFF,
        ER = FFmpeg.FF_DEBUG_ER,
        MBType = FFmpeg.FF_DEBUG_MB_TYPE,
        MMCO = FFmpeg.FF_DEBUG_MMCO,
        MV = FFmpeg.FF_DEBUG_MV,
        PictInfo = FFmpeg.FF_DEBUG_PICT_INFO,
        PTS = FFmpeg.FF_DEBUG_PTS,
        QP = FFmpeg.FF_DEBUG_QP,
        RC = FFmpeg.FF_DEBUG_RC,
        Skip = FFmpeg.FF_DEBUG_SKIP,
        StartCode = FFmpeg.FF_DEBUG_STARTCODE,
    }

    public enum FF_DEBUG_VIS
    {
        MVType = FFmpeg.FF_DEBUG_VIS_MB_TYPE,
        MVBBack = FFmpeg.FF_DEBUG_VIS_MV_B_BACK,
        MVBFor = FFmpeg.FF_DEBUG_VIS_MV_B_FOR,
        MVPFor = FFmpeg.FF_DEBUG_VIS_MV_P_FOR,
        QP = FFmpeg.FF_DEBUG_VIS_QP,
    }

    public enum FF_CMP
    {
        Bit = FFmpeg.FF_CMP_BIT,
        Chroma = FFmpeg.FF_CMP_CHROMA,
        Dct = FFmpeg.FF_CMP_DCT,
        Dct264 = FFmpeg.FF_CMP_DCT264,
        DctMax = FFmpeg.FF_CMP_DCTMAX,
        Nsse = FFmpeg.FF_CMP_NSSE,
        Psnr = FFmpeg.FF_CMP_PSNR,
        Rd = FFmpeg.FF_CMP_RD,
        Sad = FFmpeg.FF_CMP_SAD,
        Satd = FFmpeg.FF_CMP_SATD,
        SSE = FFmpeg.FF_CMP_SSE,
        VSad = FFmpeg.FF_CMP_VSAD,
        VSSE = FFmpeg.FF_CMP_VSSE,
        W53 = FFmpeg.FF_CMP_W53,
        W97 = FFmpeg.FF_CMP_W97,
        Zero = FFmpeg.FF_CMP_ZERO,
    }

    public enum FF_DTG
    {
        AFD_14_9 = FFmpeg.FF_DTG_AFD_14_9,
        AFD_16_9 = FFmpeg.FF_DTG_AFD_16_9,
        AFD_16_9_SP_14_9 = FFmpeg.FF_DTG_AFD_16_9_SP_14_9,
        AFD_4_3 = FFmpeg.FF_DTG_AFD_4_3,
        AFD_4_3_SP_14_9 = FFmpeg.FF_DTG_AFD_4_3_SP_14_9,
        AFD_SAME = FFmpeg.FF_DTG_AFD_SAME,
        AFD_SP_4_3 = FFmpeg.FF_DTG_AFD_SP_4_3,
    }

    public enum GlobalQuality
    {
        LambdaMax = FFmpeg.FF_LAMBDA_MAX,
        LambdaScale = FFmpeg.FF_LAMBDA_SCALE,
        LambdaShift = FFmpeg.FF_LAMBDA_SHIFT,
        QP2Lambda = FFmpeg.FF_QP2LAMBDA,
        QualityScale = FFmpeg.FF_QUALITY_SCALE
    }

    public enum FF_CODER_TYPE
    {
        Ac = FFmpeg.FF_CODER_TYPE_AC,
        Deflate = FFmpeg.FF_CODER_TYPE_DEFLATE,
        Raw = FFmpeg.FF_CODER_TYPE_RAW,
        Rle = FFmpeg.FF_CODER_TYPE_RLE,
        Vlc = FFmpeg.FF_CODER_TYPE_VLC,
    }

    public enum SLICE_FLAG
    {
        AllowField = FFmpeg.SLICE_FLAG_ALLOW_FIELD,
        AllowPlane = FFmpeg.SLICE_FLAG_ALLOW_PLANE,
        CodedOrder = FFmpeg.SLICE_FLAG_CODED_ORDER
    }

    public enum FF_MB_DECISION
    {
        Bits = FFmpeg.FF_MB_DECISION_BITS,
        Rd = FFmpeg.FF_MB_DECISION_RD,
        Simple = FFmpeg.FF_MB_DECISION_SIMPLE,
    }

    public enum PARSER_FLAG
    {
        CompleteFrames = FFmpeg.PARSER_FLAG_COMPLETE_FRAMES
    }

    [Flags]
    public enum CODEC_FLAG :uint
    {
        FourMv = FFmpeg.CODEC_FLAG_4MV,
        AcPred = FFmpeg.CODEC_FLAG_AC_PRED,
        AltScan = FFmpeg.CODEC_FLAG_ALT_SCAN,
        BitExact = FFmpeg.CODEC_FLAG_BITEXACT,
        CbpRd = FFmpeg.CODEC_FLAG_CBP_RD,
        ClosedGop = FFmpeg.CODEC_FLAG_CLOSED_GOP,
        EmuEdge = FFmpeg.CODEC_FLAG_EMU_EDGE,
        ExternHuff = FFmpeg.CODEC_FLAG_EXTERN_HUFF,
        GlobalHeader = FFmpeg.CODEC_FLAG_GLOBAL_HEADER,
        GMC = FFmpeg.CODEC_FLAG_GMC,
        Gray = FFmpeg.CODEC_FLAG_GRAY,
        H263P_Aiv = FFmpeg.CODEC_FLAG_H263P_AIV,
        H263P_SlictStruct = FFmpeg.CODEC_FLAG_H263P_SLICE_STRUCT,
        H263P_Umv = FFmpeg.CODEC_FLAG_H263P_UMV,
        InputPreserved = FFmpeg.CODEC_FLAG_INPUT_PRESERVED,
        InterlacedDct = FFmpeg.CODEC_FLAG_INTERLACED_DCT,
        InterlacedMe = FFmpeg.CODEC_FLAG_INTERLACED_ME,
        LoopFilter = FFmpeg.CODEC_FLAG_LOOP_FILTER,
        LowDelay = FFmpeg.CODEC_FLAG_LOW_DELAY,
        MV0 = FFmpeg.CODEC_FLAG_MV0,
        NormalizeAqp = FFmpeg.CODEC_FLAG_NORMALIZE_AQP,
        Obmc = FFmpeg.CODEC_FLAG_OBMC,
        Part = FFmpeg.CODEC_FLAG_PART,
        Pass1 = FFmpeg.CODEC_FLAG_PASS1,
        Pass2 = FFmpeg.CODEC_FLAG_PASS2,
        Psnr = FFmpeg.CODEC_FLAG_PSNR,
        QpRd = FFmpeg.CODEC_FLAG_QP_RD,
        Qpel = FFmpeg.CODEC_FLAG_QPEL,
        QScale = FFmpeg.CODEC_FLAG_QSCALE,
        SvcdScanOffset = FFmpeg.CODEC_FLAG_SVCD_SCAN_OFFSET,
        TrellisQuant = FFmpeg.CODEC_FLAG_TRELLIS_QUANT,
        Truncated = FFmpeg.CODEC_FLAG_TRUNCATED
    }

    public enum FF_RC_STRATEGY
    {
        XVID = FFmpeg.FF_RC_STRATEGY_XVID
    }

    public enum CODEC_FLAG2
    {
        _8x8DCT = FFmpeg.CODEC_FLAG2_8X8DCT,
        Aud = FFmpeg.CODEC_FLAG2_AUD,
        BitReservoir = FFmpeg.CODEC_FLAG2_BIT_RESERVOIR,
        Bpyramid = FFmpeg.CODEC_FLAG2_BPYRAMID,
        Brdo = FFmpeg.CODEC_FLAG2_BRDO,
        Chunks = FFmpeg.CODEC_FLAG2_CHUNKS,
        DropFrameTimecode = FFmpeg.CODEC_FLAG2_DROP_FRAME_TIMECODE,
        Fast = FFmpeg.CODEC_FLAG2_FAST,
        FastPSkip = FFmpeg.CODEC_FLAG2_FASTPSKIP,
        IntraVlc = FFmpeg.CODEC_FLAG2_INTRA_VLC,
        LocalHeader = FFmpeg.CODEC_FLAG2_LOCAL_HEADER,
        MemcOnly = FFmpeg.CODEC_FLAG2_MEMC_ONLY,
        MixedRefs = FFmpeg.CODEC_FLAG2_MIXED_REFS,
        NoOutput = FFmpeg.CODEC_FLAG2_NO_OUTPUT,
        NonLinearQuant = FFmpeg.CODEC_FLAG2_NON_LINEAR_QUANT,
        SkipRD = FFmpeg.CODEC_FLAG2_SKIP_RD,
        StrictGOP = FFmpeg.CODEC_FLAG2_STRICT_GOP,
        WPred = FFmpeg.CODEC_FLAG2_WPRED
    }
}