using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop.Util;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop
{
    public unsafe partial class FFmpeg
    {
        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOption* av_find_opt(void* obj, string name, string unit, int mask, AV_OPT_FLAG flags);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOption* av_set_string(void* obj, string name, string val);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOption* av_set_double(void* obj, string name, double n);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOption* av_set_q(void* obj, string name, AVRational n);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOption* av_set_int(void* obj, string name, long n);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern double av_get_double(void* obj, string name, AVOption** o_out);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVRational av_get_q(void* obj, string name, AVOption** o_out);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long av_get_int(void* obj, string name, AVOption** o_out);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern string av_get_string(void* obj, string name, AVOption** o_out, string buf, int buf_len);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVOption* av_next_option(void* obj, AVOption* last);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_opt_show(void* obj, void* av_log_obj);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_opt_set_defaults(void* s);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_opt_set_defaults2(void* s, int mask, int flags);

        #region Consts

        public const int AV_OPT_FLAG_ENCODING_PARAM = 1;   ///< a generic parameter which can be set by the user for muxing or encoding
        public const int AV_OPT_FLAG_DECODING_PARAM = 2;   ///< a generic parameter which can be set by the user for demuxing or decoding
        public const int AV_OPT_FLAG_METADATA = 4;   ///< some data extracted or inserted into the file like title, comment, ...
        public const int AV_OPT_FLAG_AUDIO_PARAM = 8;
        public const int AV_OPT_FLAG_VIDEO_PARAM = 16;
        public const int AV_OPT_FLAG_SUBTITLE_PARAM = 32;

        #endregion
    }
}
