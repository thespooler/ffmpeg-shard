#region LGPL License
//
// AVError.cs
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

namespace FFmpegSharp.Interop.Format
{
    public enum AVError
    {
        OK = 0,
        AVERROR_UNKNOWN = -1,
        AVERROR_IO = -2,
        AVERROR_NUMEXPECTED = -3,
        AVERROR_INVALIDDATA = -4,
        AVERROR_NOMEM = -5,
        AVERROR_NOFMT = -6,
        AVERROR_NOTSUPP = -7,
    }
}
