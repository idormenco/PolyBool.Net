using System.Collections.Generic;
using System.Linq;

namespace Polybool.Net.Logic
{
    internal static class Utils
    {
        public static void Shift<T>(this T[] arr)
        {
            arr = arr.Skip(1).ToArray();
        }

        public static void Pop<T>(this T[] arr)
        {
            arr = arr.Take(arr.Length - 1).ToArray();
        }

        public static List<T> Splice<T>(this List<T> source, int index, int count)
        {
            source.RemoveRange(index, count);
            return source;
        }

        public static void Push<T>(this T[] source, T elem)
        {
            var lst = source.ToList();
            lst.Add(elem);
            source = lst.ToArray();
        }

        public static void Unshift<T>(this T[] source, T elem)
        {
            var lst = new[] { elem }.ToList();
            lst.AddRange(source);
            source = lst.ToArray();
        }
    }
}