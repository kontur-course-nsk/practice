using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using AnalysisTool.Models;

namespace AnalysisTool
{
    /// <summary>
    /// Этот класс позволяет работать с файлом с событиями как с массивом и обеспечивает доступ по индексу за O(1)
    /// </summary>
    public class EventLog : IDisposable
    {
        private const int RecordLength = 40;
        private readonly MemoryMappedFile mmf;

        public EventLog(string eventLogPath)
        {
            this.Count = new FileInfo(eventLogPath).Length / RecordLength;
            this.mmf = MemoryMappedFile.CreateFromFile(eventLogPath, FileMode.Open, "events");
        }

        public EventRecord this[long i] => GetEventRecord(i);

        public long Count { get; }

        private EventRecord GetEventRecord(long index)
        {
            if (index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }

            var bytes = new byte[RecordLength];
            using (var accessor = mmf.CreateViewAccessor())
            {
                accessor.ReadArray<byte>(index * RecordLength, bytes, 0, bytes.Length);
                return ParseLine(bytes);
            }
        }

        private static EventRecord ParseLine(byte[] bytes, int offset = 0)
        {
            var str = Encoding.UTF8.GetString(bytes, offset, RecordLength).Split(';');
            return new EventRecord()
            {
                Date = DateTime.Parse(str[0]),
                Article = str[1].TrimStart('0'),
                Store = str[2].TrimEnd('\0'),
                Count = int.Parse(str[3])
            };
        }

        public void Dispose()
        {
            mmf?.Dispose();
        }
    }
}