#region LGPL License
//
// AVRational.cs
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

using System.Runtime.InteropServices;
using System;

namespace FFmpegSharp.Interop.Util
{
    /// <summary>
    /// Rational number num/den.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVRational : IComparable<AVRational>, IEquatable<AVRational>
    {
        /// <summary>
        /// numerator
        /// </summary>
        public int num;

        /// <summary>
        /// denominator
        /// </summary>
        public int den;

        public bool Equals(AVRational other)
        {
            return this.num == other.num &&
                this.den == other.den;
        }

        public int CompareTo(AVRational other)
        {
            return FFmpeg.av_cmp_q(this, other);
        }

        public static implicit operator double(AVRational a)
        {
            return FFmpeg.av_q2d(a);
        }

        public static AVRational operator *(AVRational a, AVRational b)
        {
            return FFmpeg.av_mul_q(a, b);
        }

        public static AVRational operator /(AVRational a, AVRational b)
        {
            return FFmpeg.av_div_q(a, b);
        }

        public static AVRational operator +(AVRational a, AVRational b)
        {
            return FFmpeg.av_add_q(a, b);
        }

        public static AVRational operator -(AVRational a, AVRational b)
        {
            return FFmpeg.av_sub_q(a, b);
        }

        public static AVRational FromDouble(double d, int max)
        {
            return FFmpeg.av_d2q(d, max);
        }
    };
}
