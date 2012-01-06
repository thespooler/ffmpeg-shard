using System;
using System.IO;

namespace FFmpegSharp
{
    public interface IMediaStream
    {
        TimeSpan Duration { get; }
        TimeSpan Timestamp { get; }
        long Position { get; }
        long Pts { get; }
        long Dts { get; }
        long Length { get; }
        int Read(byte[] buffer, int offset, int count);
        TimeSpan Seek(TimeSpan offset, SeekOrigin origin);
        long Seek(long offset, SeekOrigin origin);
        int UncompressedBytesPerSecond { get; }
    }
}
