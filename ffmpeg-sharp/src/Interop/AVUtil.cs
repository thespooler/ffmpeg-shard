using System;
using System.Collections.Generic;
using System.Text;
using System.Security;

namespace FFmpegSharp.Interop
{
    [SuppressUnmanagedCodeSecurity]
    public unsafe partial class FFmpeg
    {
        public const string AVUTIL_DLL_NAME = "avutil-51.dll";
    }
}
