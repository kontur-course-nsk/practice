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

        bool ContainsKey1(TKey1 key);

        bool ContainsKey2(TKey2 key);

        bool TryGetValueByKey1(TKey1 key, out TValue value);

        bool TryGetValueByKey2(TKey2 key, out TValue value);

        TValue GetValueByKey1(TKey1 key);

        TValue GetValueByKey2(TKey2 key);

        TValue this[(TKey1, TKey2) key] { get; set; }
    }
}
