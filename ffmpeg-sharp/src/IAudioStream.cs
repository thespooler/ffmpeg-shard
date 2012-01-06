using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpegSharp
{
    interface IAudioStream:IMediaStream
    {
        int Channels { get; }
        int SampleRate { get; }
        int SampleSize { get; }
    }
}
