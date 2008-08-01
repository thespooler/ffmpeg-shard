using System;
using System.Collections.Generic;
using System.Text;
using FFmpegSharp.Interop.Util;

namespace FFmpegSharp.Interop.Format
{
    public unsafe struct AVChapter
    {
        /// <summary>
        /// Unique id to identify the chapter
        /// </summary>
        public int id;

        /// <summary>
        /// Timebase in which the start/end timestamps are specified
        /// </summary>
        public AVRational time_base;

        /// <summary>
        /// Chapter start time in time_base units
        /// </summary>
        public long start;
        
        /// <summary>
        /// Chapter end time in time_base units
        /// </summary>
        public long end;

        private sbyte* title_ptr;
        /// <summary>
        /// Chapter title
        /// </summary>
        public string title
        {
            get { return new string(title_ptr); }
        }
    }
}
