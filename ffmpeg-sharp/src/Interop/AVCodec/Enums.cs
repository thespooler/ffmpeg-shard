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

namespace FFmpegSharp.Interop.Codec
{
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

        CODEC_ID_MP2 = 0x15000,
        CODEC_ID_MP3, /* prefered ID for MPEG Audio layer 1, 2 or3 decoding */
        CODEC_ID_AAC,
        CODEC_ID_MPEG4AAC,
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

        /* subtitle codecs */
        CODEC_ID_DVD_SUBTITLE = 0x17000,
        CODEC_ID_DVB_SUBTITLE,

        CODEC_ID_MPEG2TS = 0x20000, /* _FAKE_ codec to indicate a raw MPEG2 transport
                         stream (only used by libavformat) */
    };

    public enum CodecType
    {
        CODEC_TYPE_UNKNOWN = -1,
        CODEC_TYPE_VIDEO,
        CODEC_TYPE_AUDIO,
        CODEC_TYPE_DATA,
        CODEC_TYPE_SUBTITLE,
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
}