namespace HashTable
{
    public class KeyValuePair<TKey, TValue>
    {
        public KeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key
        {
            get;
            // The key should not be changed because it would affect the indexing in the hash table.
            private set;
        }

        public TValue Value
        {
            get;
            set;
        }
    }
}
