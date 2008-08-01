#region LGPL License
//
// AVOptionFlag.cs
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

namespace FFmpegSharp.Interop.Util
{
    [Flags]
    public enum AV_OPT_FLAG
    {
        AudioParam = FFmpeg.AV_OPT_FLAG_AUDIO_PARAM,
        DecodingParam = FFmpeg.AV_OPT_FLAG_DECODING_PARAM,
        EncodingParam = FFmpeg.AV_OPT_FLAG_ENCODING_PARAM,
        Metadata = FFmpeg.AV_OPT_FLAG_METADATA,
        SubtitleParam = FFmpeg.AV_OPT_FLAG_SUBTITLE_PARAM,
        VideoParam = FFmpeg.AV_OPT_FLAG_VIDEO_PARAM
    }
}
