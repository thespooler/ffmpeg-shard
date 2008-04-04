using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpegSharp.Interop
{
    /// <summary>
    /// Specifies that a pointer is being marshalled as an int and can 
    /// potentially cause compatibility issues on 64-bit systems.
    /// </summary>
    internal class BrokenPointerAttribute:Attribute
    {
    }
}
