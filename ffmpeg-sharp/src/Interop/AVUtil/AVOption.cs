#region LGPL License
//
// AVOption.cs
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

namespace FFmpegSharp.Interop.Util
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVOption
    {
        sbyte* name_ptr;
        public string name
        {
            get { return new string(name_ptr); }
        }

        sbyte* help_ptr;
        public string help
        {
            get { return new string(help_ptr); }
        }

        public int offset;
        public AVOptionType type;
        public double default_val;
        public double min;
        public double max;
        public AV_OPT_FLAG flags;
        public IntPtr unit;
    };
}
