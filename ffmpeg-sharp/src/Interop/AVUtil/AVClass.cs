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
using FFmpegSharp.Interop.Codec;

namespace FFmpegSharp.Interop.Util
{
    public delegate string ItemNameCallback(IntPtr ctx);
    public delegate IntPtr NextChildCallback(IntPtr obj, IntPtr prev);
    public delegate IntPtr NextChildClassCallback(ref AVClass prev);
    public delegate AVClassCategory ClassCategoryCallback(IntPtr ctx);

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

        public int version;
        public int log_level_offset;
        public int parent_log_context_offset;

        /**
         * Return next AVOptions-enabled child or NULL
         */
        private IntPtr child_next_ptr;
        public NextChildCallback child_next
        {
            get { return Utils.GetDelegate<NextChildCallback>(child_next_ptr); }
        }

        /**
         * Return an AVClass corresponding to next potential
         * AVOptions-enabled child.
         *
         * The difference between child_next and this is that
         * child_next iterates over _already existing_ objects, while
         * child_class_next iterates over _all possible_ children.
         */
        private IntPtr child_class_next_ptr;
        public NextChildClassCallback child_class_next
        {
            get { return Utils.GetDelegate<NextChildClassCallback>(child_class_next_ptr); }
        }

        /**
         * Category used for visualization (like color)
         * This is only set if the category is equal for all objects using this class.
         * available since version (51 << 16 | 56 << 8 | 100)
         */
        AVClassCategory category;

        /**
         * Callback to return the category.
         * available since version (51 << 16 | 59 << 8 | 100)
         */
        private IntPtr get_category_ptr;
        public ClassCategoryCallback get_category
        {
            get { return Utils.GetDelegate<ClassCategoryCallback>(get_category_ptr); }
        }
    }

    public enum AVClassCategory
    {
        AV_CLASS_CATEGORY_NA = 0,
        AV_CLASS_CATEGORY_INPUT,
        AV_CLASS_CATEGORY_OUTPUT,
        AV_CLASS_CATEGORY_MUXER,
        AV_CLASS_CATEGORY_DEMUXER,
        AV_CLASS_CATEGORY_ENCODER,
        AV_CLASS_CATEGORY_DECODER,
        AV_CLASS_CATEGORY_FILTER,
        AV_CLASS_CATEGORY_BITSTREAM_FILTER,
        AV_CLASS_CATEGORY_SWSCALER,
        AV_CLASS_CATEGORY_SWRESAMPLER,
        AV_CLASS_CATEGORY_NB ///< not part of ABI/API
    }
}
