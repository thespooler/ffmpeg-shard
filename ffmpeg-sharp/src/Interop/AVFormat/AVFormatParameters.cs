#region LGPL License
//
// AVFormatParameters.cs
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
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Interop.Format
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVFormatParameters
    {
        public AVRational time_base;
        public int sample_rate;
        public int channels;
        public int width;
        public int height;
        public PixelFormat pix_fmt;

        /// <summary>
        /// Used to select dv channel
        /// </summary>
        public int channel;

#if LIBAVFORMAT_51
        public IntPtr device_ptr;
        /// <summary>
        /// Video, audio or DV device
        /// </summary>
        public string device
        {
            get { return new string(device_ptr); }
        }
#endif

        private sbyte*  standard_ptr;
        /// <summary>
        /// TV standard: NTSC, PAL, SECAM
        /// </summary>
        public string standard
        {
            get { return new string(standard_ptr); }
        }

        private int bitFieldMask;

        /// <summary>
        /// force raw MPEG2 transport stream output, if possible
        /// </summary>
        public bool mpeg2ts_raw
        {
            get { return (bitFieldMask & 1) > 0; }
            set { bitFieldMask &= (value ? 1 : ~1); }
        }

        /// <summary>
        /// compute exact PCR for each transport stream packet 
        /// (only meaningful if mpeg2ts_raw is TRUE)
        /// </summary>
        public bool mpeg2ts_compute_pcr
        {
            get { return (bitFieldMask & 2) > 0; }
            set { bitFieldMask &= (value ? 2 : ~2); }
        }

        /// <summary>
        /// do not begin to play the stream 
        /// immediately (RTSP only)
        /// </summary>
        public bool initial_pause
        {
            get { return (bitFieldMask & 4) > 0; }
            set { bitFieldMask &= (value ? 4 : ~4); }
        }
        public bool prealloced_context
        {
            get { return (bitFieldMask & 8) > 0; }
            set { bitFieldMask &= (value ? 8 : ~8); }
        }

#if !LIBAVFORMAT_53
        public CodecID video_codec_id;
        public CodecID audio_codec_id;
#endif
    };
}
