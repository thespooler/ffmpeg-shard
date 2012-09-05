using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop.AVUtil
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVDictionary
    {
        int count;
        AVDictionaryEntry* entries;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVDictionaryEntry
    {
        char* key;
        char* value;
    }
}
