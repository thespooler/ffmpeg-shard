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
        public unsafe static void SetString(sbyte* buf, int buf_length, string str)
        {
            byte[] str_bytes = Encoding.ASCII.GetBytes(str);

            if (str_bytes.Length >= buf_length)
                throw new ArgumentException("String is too long to fit into pointer.");

            Marshal.Copy(str_bytes, 0, (IntPtr)buf, str_bytes.Length);
            buf[str_bytes.Length] = 0;
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

        public unsafe static void CopyArray(byte[] source, int sourceStartIdx, byte[] dest, int destStartIdx, int length)
        {
            fixed (byte* pSource = source)
                CopyArray(pSource, sourceStartIdx, dest, destStartIdx, length);
        }

        public unsafe static void CopyArray(IntPtr source, int sourceStartIdx, byte[] dest, int destStartIdx, int length)
        {
            CopyArray((byte*)source, sourceStartIdx, dest, destStartIdx, length);
        }

        public unsafe static void CopyArray(byte* source, int sourceStartIdx, byte[] dest, int destStartIdx, int length)
        {
            fixed (byte* pDest = dest)
                CopyArray(source, sourceStartIdx, pDest, destStartIdx, length);
        }

        public unsafe static void CopyArray(byte* source, int sourceStartIdx, byte* dest, int destStartIdx, int length)
        {
            while (length >= 16)
            {
                ((int*)dest)[0] = ((int*)source)[0];
                ((int*)dest)[1] = ((int*)source)[1];
                ((int*)dest)[2] = ((int*)source)[2];
                ((int*)dest)[3] = ((int*)source)[3];
                dest += 16;
                source += 16;
                length -= 16;
            }
            while (length >= 4)
            {
                ((int*)dest)[0] = ((int*)source)[0];
                dest += 4;
                source += 4;
                length -= 4;
            }
            while (length > 0)
            {
                ((byte*)dest)[0] = ((byte*)source)[0];
                dest += 1;
                source += 1;
                --length;
            }
        }
    }
}
