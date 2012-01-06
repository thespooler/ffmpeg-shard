#region LGPL License
//
// PixelFormat.cs
//
// Author:
//   Justin Cherniak (justin.cherniak@gmail.com
//
// Copyright (C) 2008 Justin Cherniak
//
// This library is free software; you can redistribute it and/or modify
// it  under the terms of the GNU Lesser General Public License version
// 2.1 as published by the Free Software Foundation.
//
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307
// USA
//
#endregion

namespace FFmpegSharp.Interop.Util
{
    /// <summary>
    /// Pixel format. 
    /// </summary>
    /// <remarks>
    /// PIX_FMT_RGB32 is handled in an endian-specific manner. A RGBA
    /// color is put together as:
    ///  (A &lt;&lt; 24) | (R &lt;&lt; 16) | (G &lt;&lt; 8) | B
    /// This is stored as BGRA on little endian CPU architectures and ARGB on
    /// big endian CPUs.
    ///
    /// When the pixel format is palettized RGB (PIX_FMT_PAL8), the palettized
    /// image data is stored in AVFrame.data[0]. The palette is transported in
    /// AVFrame.data[1] and, is 1024 bytes long (256 4-byte entries) and is
    /// formatted the same as in PIX_FMT_RGB32 described above (i.e., it is
    /// also endian-specific). Note also that the individual RGB palette
    /// components stored in AVFrame.data[1] should be in the range 0..255.
    /// This is important as many custom PAL8 video codecs that were designed
    /// to run on the IBM VGA graphics adapter use 6-bit palette components.
    /// </remarks>    
    public enum PixelFormat
    {
        PIX_FMT_NONE = -1,
        PIX_FMT_YUV420P,   // Planar YUV 4:2:0, 12bpp, (1 Cr & Cb sample per 2x2 Y samples)
        PIX_FMT_YUYV422,   // Packed YUV 4:2:2, 16bpp, Y0 Cb Y1 Cr
        PIX_FMT_RGB24,     // Packed RGB 8:8:8, 24bpp, RGBRGB...
        PIX_FMT_BGR24,     // Packed RGB 8:8:8, 24bpp, BGRBGR...
        PIX_FMT_YUV422P,   // Planar YUV 4:2:2, 16bpp, (1 Cr & Cb sample per 2x1 Y samples)
        PIX_FMT_YUV444P,   // Planar YUV 4:4:4, 24bpp, (1 Cr & Cb sample per 1x1 Y samples)
        PIX_FMT_RGB32,     // Packed RGB 8:8:8, 32bpp, (msb)8A 8R 8G 8B(lsb), in cpu endianness
        PIX_FMT_YUV410P,   // Planar YUV 4:1:0,  9bpp, (1 Cr & Cb sample per 4x4 Y samples)
        PIX_FMT_YUV411P,   // Planar YUV 4:1:1, 12bpp, (1 Cr & Cb sample per 4x1 Y samples)
        PIX_FMT_RGB565,    // Packed RGB 5:6:5, 16bpp, (msb)   5R 6G 5B(lsb), in cpu endianness
        PIX_FMT_RGB555,    // Packed RGB 5:5:5, 16bpp, (msb)1A 5R 5G 5B(lsb), in cpu endianness most significant bit to 0
        PIX_FMT_GRAY8,     //        Y        ,  8bpp
        PIX_FMT_MONOWHITE, //        Y        ,  1bpp, 0 is white, 1 is black
        PIX_FMT_MONOBLACK, //        Y        ,  1bpp, 0 is black, 1 is white
        PIX_FMT_PAL8,      // 8 bit with PIX_FMT_RGB32 palette
        PIX_FMT_YUVJ420P,  // Planar YUV 4:2:0, 12bpp, full scale (jpeg)
        PIX_FMT_YUVJ422P,  // Planar YUV 4:2:2, 16bpp, full scale (jpeg)
        PIX_FMT_YUVJ444P,  // Planar YUV 4:4:4, 24bpp, full scale (jpeg)
        PIX_FMT_XVMC_MPEG2_MC,// XVideo Motion Acceleration via common packet passing(xvmc_render.h)
        PIX_FMT_XVMC_MPEG2_IDCT,
        PIX_FMT_UYVY422,   // Packed YUV 4:2:2, 16bpp, Cb Y0 Cr Y1
        PIX_FMT_UYYVYY411, // Packed YUV 4:1:1, 12bpp, Cb Y0 Y1 Cr Y2 Y3
        PIX_FMT_BGR32,     // Packed RGB 8:8:8, 32bpp, (msb)8A 8B 8G 8R(lsb), in cpu endianness
        PIX_FMT_BGR565,    // Packed RGB 5:6:5, 16bpp, (msb)   5B 6G 5R(lsb), in cpu endianness
        PIX_FMT_BGR555,    // Packed RGB 5:5:5, 16bpp, (msb)1A 5B 5G 5R(lsb), in cpu endianness most significant bit to 1
        PIX_FMT_BGR8,      // Packed RGB 3:3:2,  8bpp, (msb)2B 3G 3R(lsb)
        PIX_FMT_BGR4,      // Packed RGB 1:2:1,  4bpp, (msb)1B 2G 1R(lsb)
        PIX_FMT_BGR4_BYTE, // Packed RGB 1:2:1,  8bpp, (msb)1B 2G 1R(lsb)
        PIX_FMT_RGB8,      // Packed RGB 3:3:2,  8bpp, (msb)2R 3G 3B(lsb)
        PIX_FMT_RGB4,      // Packed RGB 1:2:1,  4bpp, (msb)2R 3G 3B(lsb)
        PIX_FMT_RGB4_BYTE, // Packed RGB 1:2:1,  8bpp, (msb)2R 3G 3B(lsb)
        PIX_FMT_NV12,      // Planar YUV 4:2:0, 12bpp, 1 plane for Y and 1 for UV
        PIX_FMT_NV21,      // as above, but U and V bytes are swapped

        PIX_FMT_RGB32_1,   // Packed RGB 8:8:8, 32bpp, (msb)8R 8G 8B 8A(lsb), in cpu endianness
        PIX_FMT_BGR32_1,   // Packed RGB 8:8:8, 32bpp, (msb)8B 8G 8R 8A(lsb), in cpu endianness

        PIX_FMT_GRAY16BE,  //        Y        , 16bpp, big-endian
        PIX_FMT_GRAY16LE,  //        Y        , 16bpp, little-endian
        PIX_FMT_YUV440P,   // Planar YUV 4:4:0 (1 Cr & Cb sample per 1x2 Y samples)
        PIX_FMT_YUVJ440P,  // Planar YUV 4:4:0 full scale (jpeg)
        PIX_FMT_YUVA420P,  // Planar YUV 4:2:0, 20bpp, (1 Cr & Cb sample per 2x2 Y & A samples)
        PIX_FMT_NB,        // number of pixel formats, DO NOT USE THIS if you want to link with shared libav* because the number of formats might differ between versions

    };
}
