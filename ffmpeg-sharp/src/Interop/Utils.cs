#region LGPL License
//
// Utils.cs
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
using System.Text;

namespace FFmpegSharp.Interop
{
    public static class Utils
    {
        public unsafe static string GetString(IntPtr buf) { return GetString((byte*)buf); }
        public unsafe static string GetString(byte* buf)
        {
            if ((IntPtr)buf == IntPtr.Zero)
                return null;

            StringBuilder s = new StringBuilder();

            for (int i = 0; ; i++)
            {
                try
                {
                    if (buf[i] == '\0')
                        break;

                    s.Append((char)buf[i]);
                }
                catch (AccessViolationException e)
                {
                    throw new ArgumentException("Data in buffer not null terminated or bad pointer", e);
                }
            }

            return s.ToString();
        }

        public static T GetDelegate<T>(IntPtr ptr) where T : class
        {
            if (!(typeof(T).IsSubclassOf(typeof(Delegate))))
                throw new ArgumentException("You must call this class using a delegate type.");

            if (ptr == IntPtr.Zero)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)) as T;
        }
    }
}
