namespace CSharpUtils
{
    public class HashMap<K, V> : Map<K, V>
    {
        private class Entry
        {
            public K Key;
            public V Value;
            public Entry Next;
            public int Hashcode;
        }

        private const int MIN_CAPACITY = 16;

        private Entry[] buckets;
        private int count;

        public HashMap() : this(MIN_CAPACITY) { }

        public HashMap(int capacity)
        {
            capacity = (capacity < MIN_CAPACITY) ? MIN_CAPACITY : capacity;
            buckets = new Entry[capacity];
        }

        public override void Put(K key, V value)
        {
            int hashcode = key.GetHashCode();
            int targetBucket = (hashcode & int.MaxValue) % buckets.Length;
            Entry ent = null;

            // Search for existing key
            for (ent = buckets[targetBucket]; ent != null; ent = ent.Next)
            {
                if (ent.Hashcode == hashcode && ent.Key.Equals(key))
                {
                    // Key already exists
                    ent.Value = value;
                    return;
                }
            }

            // Rehash if necessary
            if (count + 1 > buckets.Length)
            {
                Expand();
                targetBucket = (hashcode & int.MaxValue) % buckets.Length;
            }

            // Create new entry to house key-value pair
            ent = new Entry()
            {
                Key = key,
                Value = value,
                Hashcode = hashcode
            };

            // And add to table
            ent.Next = buckets[targetBucket];
            buckets[targetBucket] = ent;
            count++;
        }

        public override V Get(K key)
        {
            Entry ent = Find(key);
            if (ent != null)
                return ent.Value;
            return default(V);
        }
        
        public override void Replace(Func<K, V, V> function)
        {
            Entry ent = null;
            for (ent = buckets[0]; ent != null; ent = ent.Next)
            {
                ent.Value = function(ent.Key,ent.Value);
            }
        }

        public override void ForEach(Action<K, V> action)
        {
            Entry ent = null;
            for (ent = buckets[0]; ent != null; ent = ent.Next)
            {
                action(ent.Key,ent.Value);
            }
        }

        public override void Remove(K key)
        {
            int hashcode = key.GetHashCode();
            int targetBucket = (hashcode & int.MaxValue) % buckets.Length;
            Entry ent = buckets[targetBucket];
            Entry last = ent;

            if (ent == null)
                return;

            // Found entry at head of linked list
            if (ent.Hashcode == hashcode && ent.Key.Equals(key))
            {
                buckets[targetBucket] = ent.Next;
                count--;
            }
            else
            {
                while (ent != null)
                {
                    if (ent.Hashcode == hashcode && ent.Key.Equals(key))
                    {
                        last.Next = ent.Next;
                        count--;
                    }
                    last = ent;
                    ent = last.Next;
                }
            }
        }

        public override bool ContainsKey(K key)
        {
            return Find(key) == null;
        }

        private Entry Find(K key)
        {
            int hashcode = key.GetHashCode();
            int targetBucket = (hashcode & int.MaxValue) % buckets.Length;
            // Search for entry
            for (Entry ent = buckets[targetBucket]; ent != null; ent = ent.Next)
            {
                if (ent.Hashcode == hashcode && ent.Key.Equals(key))
                    return ent;
            }
            return null;
        }

        private void Expand()
        {
            Rehash(buckets.Length * 2);
        }

        private void Rehash(int newCapacity)
        {
            // Resize bucket array and redistribute entries
            int oldCapacity = buckets.Length;
            int targetBucket;
            Entry ent, nextEntry;
            Entry[] newBuckets = new Entry[newCapacity];

            for (int i = 0; i < oldCapacity; i++)
            {
                if (buckets[i] != null)
                {
                    ent = buckets[i];
                    while (ent != null)
                    {
                        targetBucket = (ent.Hashcode & int.MaxValue) % newCapacity;
                        nextEntry = ent.Next;
                        ent.Next = newBuckets[targetBucket];
                        newBuckets[targetBucket] = ent;
                        ent = nextEntry;
                    }
                }
            }

            buckets = newBuckets;
        }
    }
}
