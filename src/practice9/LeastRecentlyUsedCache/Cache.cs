using System;

namespace LeastRecentlyUsedCache
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        public int Count => throw new NotImplementedException();

        public TValue this[TKey key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public void RemoveLeastRecentlyUsed()
        {
            throw new NotImplementedException();
        }
    }
}
