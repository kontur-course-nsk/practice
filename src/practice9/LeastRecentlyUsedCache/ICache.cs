namespace LeastRecentlyUsedCache
{
    public interface ICache<TKey, TValue>
    {
        int Count { get; }

        TValue this[TKey key] { get; set; }

        void RemoveLeastRecentlyUsed();
    }
}
