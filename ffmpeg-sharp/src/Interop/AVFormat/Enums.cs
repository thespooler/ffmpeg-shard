using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpegSharp.Interop.Format
{
    public enum FormatSpecificFlags : uint
    {
        NoHeader = FFmpeg.AVFMTCTX_NOHEADER
    }

    public enum OutputLoop
    {
        NoOutputLoop = FFmpeg.AVFMT_NOOUTPUTLOOP,
        InfiniteOutputLoop = FFmpeg.AVFMT_INFINITEOUTPUTLOOP,
        Loop = 1
    }

    [Flags]
    public enum AVFMT_FLAG : uint
    {
        GenPTS = FFmpeg.AVFMT_FLAG_GENPTS,
        IgnIdx = FFmpeg.AVFMT_FLAG_IGNIDX
    }

    [Flags]
    public enum AVSEEK_FLAG : int
    {
        Any = FFmpeg.AVSEEK_FLAG_ANY,
        Backward = FFmpeg.AVSEEK_FLAG_BACKWARD,
        Byte = FFmpeg.AVSEEK_FLAG_BYTE,
    }

    [Flags]
    public enum AVINDEX_FLAG
    {
        KeyFrame = FFmpeg.AVINDEX_KEYFRAME
    }

    [Flags]
    public enum InputFormatFlags : uint
    {
        NoFile = FFmpeg.AVFMT_NOFILE,
        NeedNumber = FFmpeg.AVFMT_NEEDNUMBER
    }

    [Flags]
    public enum OutputFormatFlags : uint
    {
        NoFile = FFmpeg.AVFMT_NOFILE,
        NeedNumber = FFmpeg.AVFMT_NEEDNUMBER,
        GlobalHeader = FFmpeg.AVFMT_GLOBALHEADER
    }

    [Flags]
    public enum PacketFlags : uint
    {
        Key = FFmpeg.PKT_FLAG_KEY
    }
}
