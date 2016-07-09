using System;
using System.Collections.Generic;

namespace CSharpUtils
{
    public static partial class Extensions
    {
        public static List<T> Clone<T>(this List<T> list)
        {
            List<T> clone = new List<T>();
            clone.AddRange(list.ToArray());
            return clone;
        }

        public static T As<T>(this object obj) where T : class
        {
            return obj as T;
        }

        public static T To<T>(this object obj)
        {
            try
            {
                return (T)obj;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static bool InstanceOf<T>(this object obj) where T : class
        {
            return obj.As<T>() != null;
        }
    }
}
