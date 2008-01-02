#region LGPL License
//
// AVPalletteControl.cs
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

using System.Runtime.InteropServices;

namespace FFmpegSharp.Interop.Codec
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AVPaletteControl
    {
        /* demuxer sets this to 1 to indicate the palette has changed;
         * decoder resets to 0 */
        public int palette_changed;

        /* 4-byte ARGB palette entries, stored in native byte order; note that
         * the individual palette components should be on a 8-bit scale; if
         * the palette data comes from a IBM VGA native format, the component
         * data is probably 6 bits in size and needs to be scaled */
        public fixed uint palette[FFmpeg.AVPALETTE_COUNT];
    };
}
