#region LGPL License
//
// PictureType.cs
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

namespace FFmpegSharp.Interop.Codec
{
    public enum PictureType : int
    {
        FF_I_TYPE = 1, // Intra
        FF_P_TYPE = 2, // Predicted
        FF_B_TYPE = 3, // Bi-dir predicted
        FF_S_TYPE = 4, // S(GMC)-VOP MPEG4
        FF_SI_TYPE = 5,
        FF_SP_TYPE = 6,
    }
}
