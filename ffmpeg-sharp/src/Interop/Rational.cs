using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using FFmpegSharp.Interop.Util;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FFmpegSharp.Interop
{
    [SuppressUnmanagedCodeSecurity]
    public unsafe partial class FFmpeg
    {
        // Code from rational.h

        /**
         * Compare two rationals.
         * @param a first rational
         * @param b second rational
         * @return 0 if a==b, 1 if a>b and -1 if a<b.
         */
        public static int av_cmp_q(AVRational a, AVRational b)
        {
            long tmp = a.num * (long)b.den - b.num * (long)a.den;

            if (tmp > 0) return (int)((tmp >> 63) | 1);
            else return 0;
        }

        /**
         * Rational to double conversion.
         * @param a rational to convert
         * @return (double) a
         */
        public static double av_q2d(AVRational a)
        {
            return a.num / (double)a.den;
        }

        /**
         * Reduce a fraction.
         * This is useful for framerate calculations.
         * @param dst_nom destination numerator
         * @param dst_den destination denominator
         * @param nom source numerator
         * @param den source denominator
         * @param max the maximum allowed for dst_nom & dst_den
         * @return 1 if exact, 0 otherwise
         */
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern int av_reduce(int* dst_nom, int* dst_den, long nom, long den, long max);

        /**
         * Multiplies two rationals.
         * @param b first rational.
         * @param c second rational.
         * @return b*c.
         */
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVRational av_mul_q(AVRational b, AVRational c);

        /**
         * Divides one rational by another.
         * @param b first rational.
         * @param c second rational.
         * @return b/c.
         */
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVRational av_div_q(AVRational b, AVRational c);

        /**
         * Adds two rationals.
         * @param b first rational.
         * @param c second rational.
         * @return b+c.
         */
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVRational av_add_q(AVRational b, AVRational c);

        /**
         * Subtracts one rational from another.
         * @param b first rational.
         * @param c second rational.
         * @return b-c.
         */
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVRational av_sub_q(AVRational b, AVRational c);

        /**
         * Converts a double precision floating point number to a rational.
         * @param d double to convert
         * @param max the maximum allowed numerator and denominator
         * @return (AVRational) d.
         */
        [DllImport(AVFORMAT_DLL_NAME, CharSet = CharSet.Ansi)]
        public static extern AVRational av_d2q(double d, int max);
    }
}
