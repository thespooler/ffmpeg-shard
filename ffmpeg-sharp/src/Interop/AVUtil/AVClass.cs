#region LGPL License
//
// AVClass.cs
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
    public delegate string ItemNameCallback();

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVClass
    {
        sbyte* class_name_ptr;
        public string class_name
        {
            get { return new string(class_name_ptr); }
        }

        private IntPtr item_name_ptr;
        public ItemNameCallback item_name
        {
            get { return Utils.GetDelegate<ItemNameCallback>(item_name_ptr); }
        }

        public AVOption* option;
    };
}
