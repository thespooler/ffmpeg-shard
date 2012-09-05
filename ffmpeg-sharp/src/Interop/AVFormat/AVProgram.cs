using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Interop.Format
{
    public unsafe struct AVProgram
    {
        public int id;

        public int flags;

        /// <summary>
        /// Selects which program to discard and which to feed to the caller
        /// </summary>
        public AVDiscard discard;

        public uint* stream_index;

        public uint nb_stream_indexes;

        public AVDictionary* metadata;

        public int program_num;
        public int pmt_pid;
        public int pcr_pid;
    }
}
