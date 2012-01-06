#region LGPL License
//
// FifoMemoryStream.cs
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

using System;
using System.IO;

namespace FFmpegSharp.Audio
{
    internal class FifoMemoryStream : Stream
    {
        #region Instance Variables

        private byte[] m_array;
        private int m_writeCursor;
        private int m_readCursor;
        private const int DEFAULT_SIZE = 16384;

        #endregion

        public FifoMemoryStream() : this(DEFAULT_SIZE) { }
        public FifoMemoryStream(int InitialSize)
        {
            m_array = new byte[InitialSize];
            m_readCursor = m_writeCursor = 0;
        }

        #region Supported Properties

        public override bool CanRead { get { return true; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return true; } }

        public override long Length { get { return GetMaxReadSize(); } }

        #endregion

        public override int Read(byte[] buffer, int offset, int count)
        {
            int length = Math.Min(count, GetMaxReadSize());

            do
            {
                int readLength = Math.Min(length, m_array.Length - m_readCursor);
                Buffer.BlockCopy(m_array, m_readCursor, buffer, offset, readLength);

                offset += readLength;
                count -= readLength;
                m_readCursor = (m_readCursor + readLength) % m_array.Length;
            } while (count > 0);

            return length;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (count == 0)
                return;

            // Get the free space in the buffer and resize if necessary
            int maxSize = GetMaxWriteSize();

            while (count >= maxSize)
            {
                DoubleBufferSize();
                maxSize = GetMaxWriteSize();
            }

            do
            {
                int writeLength = m_array.Length - m_writeCursor;
                writeLength = Math.Min(writeLength, count);

                Buffer.BlockCopy(buffer, offset, m_array, m_writeCursor, writeLength);

                m_writeCursor += writeLength;
                m_writeCursor %= m_array.Length;

                offset += writeLength;
                count -= writeLength;
            } while (count > 0);
        }

        private int GetMaxWriteSize()
        {
            int maxSize;

            // If completely filling the buffer, return the max size possible
            if (m_writeCursor == 0 && m_readCursor == 0)
                return m_array.Length;

            // Check if the buffer has looped and return maxSize accordingly
            if (m_writeCursor < m_readCursor)
                maxSize = m_readCursor - m_writeCursor;
            else
                maxSize = m_array.Length - m_writeCursor + m_readCursor;

            return maxSize;
        }

        private int GetMaxReadSize()
        {
            if (m_writeCursor > m_readCursor)
                return m_writeCursor - m_readCursor;
            else
                return m_array.Length - m_writeCursor + m_readCursor;
        }

        private void DoubleBufferSize()
        {
            if (m_readCursor <= m_writeCursor)
                ResizeByteArray(ref m_array, m_array.Length * 2);
            else // Buffer is looped
            {
                int dataSize = m_array.Length - m_readCursor + m_writeCursor;
                byte[] tempArr = new byte[m_array.Length * 2];

                Buffer.BlockCopy(m_array, m_readCursor, tempArr, 0, m_array.Length - m_readCursor);
                Buffer.BlockCopy(m_array, 0, tempArr, m_array.Length - m_readCursor, m_writeCursor);

                m_readCursor = 0;
                m_writeCursor = dataSize;

                m_array = tempArr;
            }
        }

        #region Not Supported

        public override void Flush() { throw new NotSupportedException(); }
        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        public override long Seek(long offset, SeekOrigin origin) { throw new NotSupportedException(); }
        public override void SetLength(long value) { throw new NotSupportedException(); }

        #endregion

        private void ResizeByteArray(ref byte[] array, int newLength)
        {
            byte[] old = array;
            array = new byte[newLength];

            Buffer.BlockCopy(old, 0, array, 0, Math.Min(Buffer.ByteLength(old), Buffer.ByteLength(array)));
        }
    }
}
