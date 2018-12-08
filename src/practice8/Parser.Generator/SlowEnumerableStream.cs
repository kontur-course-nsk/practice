using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Parser.Generator
{
    using System.Threading;

    public static class SlowEnumerableStream
    {
        public static SlowEnumerableStream<T> Create<T>(IEnumerable<T> source, Func<T, byte[]> serializer)
        {
            return new SlowEnumerableStream<T>(source, serializer);
        }
    }

    public class SlowEnumerableStream<T> : Stream
    {
        private readonly IEnumerator<T> _source;
        private readonly Func<T, IEnumerable<byte>> _serializer;
        private readonly Queue<byte> _buf = new Queue<byte>();

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => -1;

        public SlowEnumerableStream(IEnumerable<T> source, Func<T, IEnumerable<byte>> serializer)
        {
            _source = source.GetEnumerator();
            _serializer = serializer;
        }

        private bool SerializeNext()
        {
            if (!_source.MoveNext())
            {
                return false;
            }

            foreach (var b in _serializer(_source.Current))
            {
                _buf.Enqueue(b);
            }

            return true;
        }

        private byte? NextByte()
        {
            if (_buf.Any() || SerializeNext())
            {
                return _buf.Dequeue();
            }

            return null;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            Thread.Sleep(1000);
            var read = 0;
            while (read < count)
            {
                var mayb = NextByte();
                if (mayb == null)
                {
                    break;
                }

                buffer[offset + read] = (byte)mayb;
                read++;
            }

            return read;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }
    }
}
