using System.Reflection;

namespace CompanyDB
{
    public static class MapSerializor
    {
        public static Map<string,object> ToHashMap<T>(this T obj) where T : class
        {
            Map<string,object> map = new HashMap<string, object>();

            FieldInfo[] methods = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in methods)
            {
                map.Put(method.Name, method.GetValue(obj));
            }
            return map;
        }

        public static T FromHashMap<T>(this Map<string, object> map, T obj = null) where T : class, new()
        {
            if (obj == null) obj = new T();

            FieldInfo[] methods = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in methods)
            {
                if (map.ContainsKey(method.Name)) method.SetValue(obj, map.Get(method.Name));
            }

            return obj;
        }

        public static T FromHashMap<T>(this T obj, Map<string, object> map) where T : class, new()
        {
            return FromHashMap(map, obj);
        }
    }
}
