using System.Collections.Generic;
using System.Linq;

namespace Polybool.Net.Logic
{
    internal static class Utils
    {
        public static T[] Shift<T>(this T[] arr)
        {
            return arr.Skip(1).ToArray();
        }

        public static T[] Pop<T>(this T[] arr)
        {
            return arr.Take(arr.Length - 1).ToArray();
        }

        public static List<T> Splice<T>(this List<T> source, int index, int count)
        {
            source.RemoveRange(index, count);
            return source;
        }

        public static T[] Push<T>(this T[] source, T elem)
        {
            var lst = source.ToList();
            lst.Add(elem);
            return lst.ToArray();
        }

        public static T[] Unshift<T>(this T[] source, T elem)
        {
            var lst = new[] { elem }.ToList();
            lst.AddRange(source);
            return lst.ToArray();
        }
    }
}