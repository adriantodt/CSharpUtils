using System;
using System.Collections.Generic;

namespace CSharpUtils
{
    public abstract class Map<K, V>
    {
        public abstract void Put(K key, V value);
        public abstract V Get(K key);
        public abstract void Remove(K key);
        public abstract bool ContainsKey(K key);
        public abstract void Replace(Func<K, V, V> function);
        public abstract void ForEach(Action<K, V> action);
        public abstract int Count { get; }
        public abstract List<K> Keys { get; }
        public abstract List<V> Values { get; }
    }
}
