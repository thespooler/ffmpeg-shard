using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop
{
    /* IEEE 80 bits extended float */
    public unsafe struct AVExtFloat
    {
        public fixed byte exponent[2];
        public fixed byte mantissa[8];
    }

    public partial class FFmpeg
    {
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern double av_int2dbl(long v);
        
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern float av_int2flt(int v);
        
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern double av_ext2dbl(AVExtFloat ext);
        
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern long av_dbl2int(double d);
        
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_flt2int(float d);

        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVExtFloat av_dbl2ext(double d);
    }
}
