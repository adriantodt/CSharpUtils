namespace CSharpUtils
{
    public abstract class Map<K, V>
    {
        public abstract void Put(K key, V value);
        public abstract V Get(K key);
        public abstract void Remove(K key);
        public abstract bool ContainsKey(K key);
    }
}