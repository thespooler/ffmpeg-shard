#region LGPL License
//
// AVOptionType.cs
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

namespace FFmpegSharp.Interop.Util
{
    public enum AVOptionType
    {
        FF_OPT_TYPE_FLAGS,
        FF_OPT_TYPE_INT,
        FF_OPT_TYPE_long,
        FF_OPT_TYPE_DOUBLE,
        FF_OPT_TYPE_FLOAT,
        FF_OPT_TYPE_STRING,
        FF_OPT_TYPE_RATIONAL,
        FF_OPT_TYPE_CONST = 128,
    };
}
