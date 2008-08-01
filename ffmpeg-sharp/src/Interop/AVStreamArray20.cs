using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop.Format;

namespace FFmpegSharp.Interop
{
    public unsafe struct AVStreamArray20
    {
        AVStream* stream_ptrs1, stream_ptrs2, stream_ptrs3, stream_ptrs4,
            stream_ptrs5, stream_ptrs6, stream_ptrs7, stream_ptrs8, stream_ptrs9,
            stream_ptrs10, stream_ptrs11, stream_ptrs12, stream_ptrs13, stream_ptrs14,
            stream_ptrs15, stream_ptrs16, stream_ptrs17, stream_ptrs18, stream_ptrs19, stream_ptrs20;

        public AVStream* this[int idx]
        {
            get
            {
                switch (idx)
                {
                    case 0: return stream_ptrs1;
                    case 1: return stream_ptrs2;
                    case 2: return stream_ptrs3;
                    case 3: return stream_ptrs4;
                    case 4: return stream_ptrs5;
                    case 5: return stream_ptrs6;
                    case 6: return stream_ptrs7;
                    case 7: return stream_ptrs8;
                    case 8: return stream_ptrs9;
                    case 9: return stream_ptrs10;
                    case 10: return stream_ptrs11;
                    case 11: return stream_ptrs12;
                    case 12: return stream_ptrs13;
                    case 13: return stream_ptrs14;
                    case 14: return stream_ptrs15;
                    case 15: return stream_ptrs16;
                    case 16: return stream_ptrs17;
                    case 17: return stream_ptrs18;
                    case 18: return stream_ptrs19;
                    case 19: return stream_ptrs20;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (idx)
                {
                    case 0: stream_ptrs1 = value; break;
                    case 1: stream_ptrs2 = value; break;
                    case 2: stream_ptrs3 = value; break;
                    case 3: stream_ptrs4 = value; break;
                    case 4: stream_ptrs5 = value; break;
                    case 5: stream_ptrs6 = value; break;
                    case 6: stream_ptrs7 = value; break;
                    case 7: stream_ptrs8 = value; break;
                    case 8: stream_ptrs9 = value; break;
                    case 9: stream_ptrs10 = value; break;
                    case 10: stream_ptrs11 = value; break;
                    case 11: stream_ptrs12 = value; break;
                    case 12: stream_ptrs13 = value; break;
                    case 13: stream_ptrs14 = value; break;
                    case 14: stream_ptrs15 = value; break;
                    case 15: stream_ptrs16 = value; break;
                    case 16: stream_ptrs17 = value; break;
                    case 17: stream_ptrs18 = value; break;
                    case 18: stream_ptrs19 = value; break;
                    case 19: stream_ptrs20 = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int Length
        {
            get { return 20; }
        }
    }
}
