using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpegSharp.Interop.Codec
{
    public unsafe struct AVProfile
    {
        int profile;
        char* name; ///< short name for the profile
    }
}
