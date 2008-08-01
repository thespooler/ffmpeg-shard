using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IntPtrArray4
    {
        private IntPtr a, b, c, d;

        public IntPtr this[int idx]
        {
            get
            {
                switch (idx)
                {
                    case 0: return a;
                    case 1: return b;
                    case 2: return c;
                    case 3: return d;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (idx)
                {
                    case 0: a = value; break;
                    case 1: b = value; break;
                    case 2: c = value; break;
                    case 3: d = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int Length
        {
            get { return 4; }
        }
    }
}
