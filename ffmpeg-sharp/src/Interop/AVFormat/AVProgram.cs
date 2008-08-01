using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop.Codec;

namespace FFmpegSharp.Interop.Format
{
    public unsafe struct AVProgram
    {
        public int id;

        private sbyte* provider_name_ptr;
        /// <summary>
        /// Network name for DVB streams
        /// </summary>
        public string provider_name
        {
            get { return new string(provider_name_ptr); }
        }

        private sbyte* name_ptr;
        /// <summary>
        /// Service name for DVB streams
        /// </summary>
        public string name
        {
            get { return new string(name_ptr); }
        }

        public int flags;

        /// <summary>
        /// Selects which program to discard and which to feed to the caller
        /// </summary>
        public AVDiscard discard;

        public uint* stream_index;

        public uint nb_stream_indexes;
    }
}
