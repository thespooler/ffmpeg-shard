using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpegSharp.Interop.SWScale
{
    [Flags]
    public enum SwsFlags
    {
        FastBilinear = FFmpeg.SWS_FAST_BILINEAR,
        Bilinear = FFmpeg.SWS_BILINEAR,
        Bicubic = FFmpeg.SWS_BICUBIC,
        X = FFmpeg.SWS_X,
        Point = FFmpeg.SWS_POINT,
        Area = FFmpeg.SWS_AREA,
        Bicublin = FFmpeg.SWS_BICUBLIN,
        Gauss = FFmpeg.SWS_GAUSS,
        Sinc = FFmpeg.SWS_SINC,
        Lanczos = FFmpeg.SWS_LANCZOS,
        Spline = FFmpeg.SWS_SPLINE,
        SrcVChrDropMask = FFmpeg.SWS_SRC_V_CHR_DROP_MASK,
        SrcVChrDropShift = FFmpeg.SWS_SRC_V_CHR_DROP_SHIFT,
        ParamDefault = FFmpeg.SWS_PARAM_DEFAULT,
        PrintInfo = FFmpeg.SWS_PRINT_INFO,
        FullChrHInt = FFmpeg.SWS_FULL_CHR_H_INT, //internal chrominace subsampling info
        FullChrHInp = FFmpeg.SWS_FULL_CHR_H_INP, //input subsampling info
        DirectBGR = FFmpeg.SWS_DIRECT_BGR,
        AccurateRnd = FFmpeg.SWS_ACCURATE_RND
    }
}
