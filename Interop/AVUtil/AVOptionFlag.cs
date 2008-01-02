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
    public enum AvOptionFlag
    {
        AV_OPT_FLAG_ENCODING_PARAM = 1,   ///< a generic parameter which can be set by the user for muxing or encoding
        AV_OPT_FLAG_DECODING_PARAM = 2,   ///< a generic parameter which can be set by the user for demuxing or decoding
        AV_OPT_FLAG_METADATA = 4,   ///< some data extracted or inserted into the file like title, comment, ...
        AV_OPT_FLAG_AUDIO_PARAM = 8,
        AV_OPT_FLAG_VIDEO_PARAM = 16,
        AV_OPT_FLAG_SUBTITLE_PARAM = 32,
    }
}
