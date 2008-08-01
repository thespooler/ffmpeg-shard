using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop
{
    internal class ZeroTrueBoolMarshaller : ICustomMarshaler
    {
        private static ZeroTrueBoolMarshaller s_default;

        public static ICustomMarshaler GetInstance(string s)
        {
            if (s_default == null)
                s_default = new ZeroTrueBoolMarshaller();
            return s_default;
        }

        public void CleanUpManagedData(object ManagedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            Marshal.FreeHGlobal(pNativeData);
        }

        public int GetNativeDataSize()
        {
            return (sizeof(int));
        }

        public IntPtr MarshalManagedToNative(object managedObj)
        {
            if (managedObj is bool)
            {
                bool val = (bool)managedObj;
                IntPtr ptr = Marshal.AllocHGlobal(sizeof(int));
                Marshal.WriteInt32(ptr, val ? 0 : -1);
                return ptr;
            }
            else
                throw new MarshalDirectiveException("Invalid object");
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            int val = Marshal.ReadInt32(pNativeData);

            return val == 0;
        }
    }
}
