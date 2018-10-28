using System;
using System.IO;
using CSharpTest.Net.Serialization;

namespace CmsSystem
{
    public class IndexValue
    {
        public string Store { get; set; }

        public string Article { get; set; }

        public int Count { get; set; }
    }

    public class IndexValueSerializer : ISerializer<IndexValue>
    {
        public void WriteTo(IndexValue value, Stream stream)
        {
            PrimitiveSerializer.String.WriteTo(value.Article, stream);
            PrimitiveSerializer.String.WriteTo(value.Store, stream);
            PrimitiveSerializer.Int32.WriteTo(value.Count, stream);
        }

        public IndexValue ReadFrom(Stream stream)
        {
            var indexValue = new IndexValue();
            indexValue.Article = PrimitiveSerializer.String.ReadFrom(stream);
            indexValue.Store = PrimitiveSerializer.String.ReadFrom(stream);
            indexValue.Count = PrimitiveSerializer.Int32.ReadFrom(stream);
            return indexValue;
        }
    }
}
