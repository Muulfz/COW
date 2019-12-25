using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Helper
{
    public static class ListHelper
    {
        public static Iterator<T> Iterator<T>(this IList<T> list)
        {
            return new Iterator<T>(list);
        }

        public static List<Tuple<TK, TV>> Entries<TK, TV>(this IDictionary<TK, TV> map)
        {
            List<Tuple<TK, TV>> list = new List<Tuple<TK, TV>>();
            foreach (TK key in map.Keys)
            {
                list.Add(new Tuple<TK, TV>(key, map[key]));
            }
            return list;
        }

        public static List<T> SelectAll<T>(this List<T> list, Predicate<T> select)
        {
            List<T> newList = new List<T>();
            list.SafeForEach(obj =>
            {
                if (select.Invoke(obj))
                {
                    newList.Add(obj);
                }
            });
            return newList;
        }

        public static T SelectFirst<T>(this IList<T> list, Predicate<T> select, T @default = default)
        {
            T value = @default;
            list.SafeForEach(obj =>
            {
                if (select.Invoke(obj))
                {
                    value = obj;
                    return true;
                }

                return false;
            });
            return value;
        }

        public static void SafeForEach<T>(this IList<T> list, Action<T> callback)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                callback.Invoke(list[i]);
            }
        }

        public static void SafeForEach<T>(this IList<T> list, Func<T, bool> callback)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (callback.Invoke(list[i]))
                {
                    break;
                }
            }
        }
    }
}
