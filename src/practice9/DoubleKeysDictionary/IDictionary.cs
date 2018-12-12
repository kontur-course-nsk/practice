using System.Collections.Generic;

namespace DoubleKeysDictionary
{
    public interface IDictionary<TKey1, TKey2, TValue> : 
        ICollection<KeyValuePair<(TKey1, TKey2), TValue>>,
        IEnumerable<KeyValuePair<(TKey1, TKey2), TValue>>
    {
        void Add(TKey1 key1, TKey2 key2, TValue value);

        ICollection<(TKey1, TKey2)> Keys { get; }

        ICollection<TValue> Values { get; }

        bool ContainsKey(TKey1 key);

        bool ContainsKey(TKey2 key);

        bool TryGetValue(TKey1 key, out TValue value);

        bool TryGetValue(TKey2 key, out TValue value);

        TValue this[TKey1 key] { get; set; }

        TValue this[TKey2 key] { get; set; }
    }
}
