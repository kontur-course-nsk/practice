using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleKeysDictionary
{
    public class DoubleKeysDictionary<TKey1, TKey2, TValue> : IDictionary<TKey1, TKey2, TValue>
    {
        public int Count { get; }

        public bool IsReadOnly { get; }

        public ICollection<(TKey1, TKey2)> Keys { get; }

        public ICollection<TValue> Values { get; }

        public IEnumerator<KeyValuePair<(TKey1, TKey2), TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<(TKey1, TKey2), TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<(TKey1, TKey2), TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<(TKey1, TKey2), TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<(TKey1, TKey2), TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            throw new NotImplementedException();
        }
    
        public bool ContainsKey1(TKey1 key)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey2(TKey2 key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValueByKey1(TKey1 key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValueByKey2(TKey2 key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public TValue GetValueByKey1(TKey1 key)
        {
            throw new NotImplementedException();
        }

        public TValue GetValueByKey2(TKey2 key)
        {
            throw new NotImplementedException();
        }

        public TValue this[(TKey1, TKey2) key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
