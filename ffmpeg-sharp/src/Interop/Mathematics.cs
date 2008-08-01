using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop.Util;
using FFmpegSharp.Interop.AVUtil;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop
{
    // Imports from mathematics.h
    public partial class FFmpeg
    {
        /**
         * rescale a 64bit integer with rounding to nearest.
         * a simple a*b/c isn't possible as it can overflow
         */
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long av_rescale(long a, long b, long c);

        /**
         * rescale a 64bit integer with specified rounding.
         * a simple a*b/c isn't possible as it can overflow
         */
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long av_rescale_rnd(long a, long b, long c, AVRounding rnd);

        /**
         * rescale a 64bit integer by 2 rational numbers.
         */
        [DllImport(AVCODEC_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long av_rescale_q(long a, AVRational bq, AVRational cq);
    }
}
