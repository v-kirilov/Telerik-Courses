namespace HashTable
{
    using SinglyLinkedList;

    public class HashTable<TKey, TValue>
    {
        private readonly LinkedList<KeyValuePair<TKey, TValue>>[] array;

        public HashTable(int capacity)
        {
            this.array = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];

            for (int i = 0; i < capacity; i++)
            {
                this.array[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }
        }

        public void Add(TKey key, TValue value)
        {
            // Get the index for the given key
            int index = System.Math.Abs(key.GetHashCode() % this.array.Length);

            // Use the index to get the LinkedList that will store the key-value pair
            var list = this.array[index];

            // Check the list for a duplicate key
            foreach (var pair in list)
            {
                if (pair.Key.Equals(key))
                {
                    throw new System.ArgumentException($"Duplicate key detected: {key}");
                }
            }

            // Add the new key-value pair in the LinkedList
            var newPair = new KeyValuePair<TKey, TValue>(key, value);
            list.AddFirst(newPair);
        }

        public void Update(TKey key, TValue value)
        {
            // Get the index for the given key
            int index = System.Math.Abs(key.GetHashCode() % this.array.Length);

            // Use the index to get the LinkedList that will store the key-value pair
            var list = this.array[index];

            // Iterate the key/value pairs in the list and check for matching keys
            bool isUpdated = false;
            foreach (var pair in list)
            {
                if (pair.Key.Equals(key))
                {
                    // Update the value
                    pair.Value = value;
                    isUpdated = true;
                    break;
                }
            }

            // Throw exception if the key is not present in the hashtable
            if (!isUpdated)
            {
                throw new System.ArgumentException($"Key doesn't exist: {key}");
            }
        }

        public bool Remove(TKey key)
        {
            // Get the index for the given key
            int index = System.Math.Abs(key.GetHashCode() % this.array.Length);

            // Use the index to get the LinkedList that will store the key-value pair
            var list = this.array[index];

            // Try to remove by manually iterating the LinkedList and comparing the keys only.
            // This is necessary because the Remove method of the LinkedList uses Equals in an undesired way.
            // The LinkedList.Remove calls LinkedListNode.Value.Equals which is a combination of Key and Value
            // whereas we need to use only the Key.
            bool isRemoved = false;
            var current = list.Head;
            while (current != null)
            {
                var pair = current.Value;
                if (pair.Key.Equals(key))
                {
                    list.Remove(pair);
                    isRemoved = true;
                    break;
                }

                current = current.Next;
            }

            return isRemoved;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            // Get the index for the given key
            int index = System.Math.Abs(key.GetHashCode() % this.array.Length);

            // Use the index to get the LinkedList that will store the key-value pair
            var list = this.array[index];

            value = default;

            bool found = false;

            foreach (var pair in list)
            {
                if (pair.Key.Equals(key))
                {
                    value = pair.Value;
                    found = true;
                    break;
                }
            }

            return found;
        }

        public int Capacity
        {
            get
            {
                return this.array.Length;
            }
        }

        public System.Collections.Generic.IEnumerable<TKey> Keys
        {
            get
            {
                foreach (var list in this.array)
                {
                    foreach (var pair in list)
                    {
                        yield return pair.Key;
                    }
                }
            }
        }

        public System.Collections.Generic.IEnumerable<TValue> Values
        {
            get
            {
                foreach (var list in this.array)
                {
                    foreach (var pair in list)
                    {
                        yield return pair.Value;
                    }
                }
            }
        }

        public System.Collections.Generic.IEnumerable<KeyValuePair<TKey, TValue>> Items
        {
            get
            {
                foreach (var list in this.array)
                {
                    foreach (var pair in list)
                    {
                        yield return pair;
                    }
                }
            }
        }
    }
}
