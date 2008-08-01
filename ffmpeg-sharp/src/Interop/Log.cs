using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop
{
    /* Stuff from log.h should be here but ... some functions pose a 
     * problem so are not implemented for now.*/
    public partial class FFmpeg
    {
        //void av_log(void*, int level, const char *fmt, ...);
        //void av_vlog(void*, int level, const char *fmt, va_list);

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_log_get_level();

        [DllImport(FFmpeg.AVUTIL_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern void av_log_set_level(int level);
        //void av_log_set_callback(void (*)(void*, int, const char*, va_list));
        //void av_log_default_callback(void* ptr, int level, const char* fmt, va_list vl);

#if !LIBAVUTIL_V50 // #if LIBAVUTIL_VERSION < 50
        public const int AV_LOG_QUIET = -1;
        public const int AV_LOG_FATAL = 0;
        public const int AV_LOG_ERROR = 0;
        public const int AV_LOG_WARNING = 1;
        public const int AV_LOG_INFO = 1;
        public const int AV_LOG_VERBOSE = 1;
        public const int AV_LOG_DEBUG = 2;
#else
        public const int AV_LOG_QUIET = -8;

        /**
         * something went really wrong and we will crash now
         */
        public const int AV_LOG_PANIC = 0;

        /**
         * something went wrong and recovery is not possible
         * like no header in a format which depends on it or a combination
         * of parameters which are not allowed
         */
        public const int AV_LOG_FATAL = 8;

        /**
         * something went wrong and cannot losslessly be recovered
         * but not all future data is affected
         */
        public const int AV_LOG_ERROR = 16;

        /**
         * something somehow does not look correct / something which may or may not
         * lead to some problems like use of -vstrict -2
         */
        public const int AV_LOG_WARNING = 24;

        public const int AV_LOG_INFO = 32;
        public const int AV_LOG_VERBOSE = 40;

        /**
         * stuff which is only useful for libav* developers
         */
        public const int AV_LOG_DEBUG = 48;
#endif
    }
}
