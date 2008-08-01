#region LGPL License
//
// AVFrame.cs
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

namespace FFmpegSharp.Interop.Codec
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVFrame
    {
        /// <summary>
        /// Pointer to the picture planes.
        /// This might be different from the first allocated byte
        /// </summary>
        public IntPtrArray4 data;

        public fixed int linesize[4];

        /// <summary>
        /// Pointer to the first allocated byte of the picture.  Can be used in get_buffer/release_buffer.
        /// This isn't used by libavcodec unless the default get/release_buffer() is used.
        /// </summary>
        public IntPtrArray4 @base;

        /// <summary>
        /// Keyframe.  Set by libavcodec
        /// </summary>
        public int key_frame;
            
        /// <summary>
        /// Picture type of the frame
        /// -Encoding: Set by libavcodec for coded_picture (set by the user for input).
        /// -Decoding: Set by libavcodec
        /// </summary>
        public PictureType pict_type;

        public long pts;

        /// <summary>
        /// Picture number in bitstream order
        /// </summary>
        public int codec_picture_number;

        /// <summary>
        /// Picture number in display order.
        /// </summary>
        public int display_picture_number;

        /// <summary>
        /// Quality (between 1 (good) and FF_LAMBDA_MAX (bad))
        /// -Encoding: Set by libavcodec for coded_picture (set by the user for input).
        /// -Decoding: Set by libavcodec
        /// </summary>
        public int quality;

        /// <summary>
        /// buffer age (1->was last buffer and dint change, 2->..., ...).
        /// Set to INT_MAX if the buffer has not been used yet.
        /// - encoding: unused
        /// - decoding: MUST be set by get_buffer().
        /// </summary>
        public int age;

        /// <summary>
        /// Is this picture used as a reference.
        /// -encoding: unused
        /// -decoding: Set by libavcodec (before get_buffer() call)
        /// </summary>
        public int reference;

        /// <summary>
        /// QP table
        /// -encoding: unused
        /// -decoding: Set by libavcodec
        /// </summary>
        public byte* qscale_table;

        /// <summary>
        /// QP store stride
        /// -encoding: unused
        /// -decoding: Set by libavcodec
        /// </summary>
        public byte* mbskip_table;

        ///<summary>
        /// motion vector table
        /// <example><code>
        /// int mv_sample_log2= 4 - motion_subsample_log2;
        /// int mb_width= (width+15)>>4;
        /// int mv_stride= (mb_width << mv_sample_log2) + 1;
        /// motion_val[direction][x + y///mv_stride][0->mv_x, 1->mv_y];
        /// </code></example>
        /// - encoding: Set by user.
        /// - decoding: Set by libavcodec.
        /// </summary>
        /// <remarks>int16_t (*motion_val[2])[2];</remarks>
        public fixed short motionVal[4];

        ///< summary>
        /// macroblock type table
        /// mb_type_base + mb_width + 2
        /// - encoding: Set by user.
        /// - decoding: Set by libavcodec.
        /// </summary>
        public uint* mb_type;

        /// <summary>
        /// Log2 of the size of the block which a single vector in motion_val represents
        /// (4->16x16, 3->8x8, 2->4x4, 1->2x2)
        ///  - encoding: unused
        ///  - decoding: Set by libavcodec
        /// </summary>
        public byte motion_subsample_log;

        /// <summary>
        /// For some private data of the user.
        ///  - encoding: unused
        ///  - decoding: Set by user.
        /// </summary>
        public IntPtr opaque;

        /// <summary>
        /// Error
        ///  - enconding: Set by libavcodec if flags&CODEC_FLAG_PSNR
        ///  - decoding: unused
        /// </summary>
        public fixed ulong error[4];

        /// <summary>
        /// type of the buffer (to keep track of who has to deallocate data)
        /// - encoding: Set by the one who allocates it
        /// - decoding: Set by the one who allocates it.
        /// Note: User allocated (direct rendering) & internal buffers cannot coexist currently.
        /// </summary>
        public int type;

        /// <summary>
        /// When decoding, this signals how much the picture must be delayed.
        /// extra_delay = repeat_pict / (2*fps)
        /// - encoding: unused
        /// - decoding: Set by libavcodec.
        /// </summary>
        public int repeat_pict;

        public int qscale_type;

        /// <summary>
        /// The content of the picture is interlaced.
        /// - encoding: Set by user.
        /// - decoding: Set by libavcodec. (default 0)
        /// </summary>
        public int interlaced_frame;

        /// <summary>
        /// If the content is interlaced, is top field displayed first.
        /// - encoding: Set by user.
        /// - decoding: Set by libavcodec.
        /// </summary>
        public int top_field_first;

        /// <summary>
        /// Pan scan.
        ///  - encoding: Set by user.
        ///  - decoding: Set by libavcodec
        /// </summary>
        public AVPanScan* pan_scan;

        /// <summary>
        /// Tell user application that palette has changed from previous frame.
        /// - encoding: ??? (no palette-enabled encoder yet)
        /// - decoding: Set by libavcodec. (default 0).
        /// </summary>
        public int palette_has_changed;

        /// <summary>
        /// codec suggestion on buffer type if != 0
        /// - encoding: unused
        /// - decoding: Set by libavcodec. (before get_buffer() call)).
        /// </summary>
        public int buffer_hints;

        /// <summary>
        /// DCT coefficients
        /// - encoding: unused
        /// - decoding: Set by libavcodec
        /// </summary>
        public short* dct_coeff;

        /// <summary>
        ///  motion referece frame index
        /// - encoding: Set by user.
        /// - decoding: Set by libavcodec.
        /// </summary>
        public fixed byte ref_index[2];

        public static implicit operator AVPicture(AVFrame frame)
        {
            return *((AVPicture*)&frame);
        }

        public static explicit operator AVFrame(AVPicture picture)
        {
            return *((AVFrame*)&picture);
        }
    };
}
