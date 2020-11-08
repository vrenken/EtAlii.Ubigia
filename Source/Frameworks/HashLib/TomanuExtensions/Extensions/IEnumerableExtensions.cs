using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TomanuExtensions
{
    [DebuggerStepThrough]
    public static class IEnumerableExtensions
    {
        public static void ForEachWithIndex<T>(this IEnumerable<T> a_enumerable,
            Action<T, int> a_handler)
        {
            var index = 0;
            foreach (var item in a_enumerable)
                a_handler(item, index++);
        }

        public static void ForEach<T>(this IEnumerable<T> a_enumerable, Action<T> a_handler)
        {
            foreach (var item in a_enumerable)
                a_handler(item);
        }

        public static int IndexOf<T>(this IEnumerable<T> a_enum, T a_el)
        {
            var i = 0;

            foreach (var el in a_enum)
            {
                if (a_el.Equals(el))
                    return i;
                i++;
            }

            return -1;
        }

        public static bool Unique<T>(this IEnumerable<T> a_enum)
        {
            return a_enum.Distinct().Count() == a_enum.Count();
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> a_enumerable, T a_element)
        {
            foreach (var ele in a_enumerable)
            {
                if (!ele.Equals(a_element))
                    yield return ele;
            }
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> a_enumerable, T a_element,
            IEqualityComparer<T> a_comparer)
        {
            foreach (var ele in a_enumerable)
            {
                if (!a_comparer.Equals(ele, a_element))
                    yield return ele;
            }
        }

        public static IEnumerable<T> ExceptExact<T>(this IEnumerable<T> a_enumerable,
            IEnumerable<T> a_values)
        {
            if (a_values.FirstOrDefault() == null)
                return a_enumerable;

            var list = new List<T>(a_enumerable);

            foreach (var ele in a_values)
            {
                var index = list.IndexOf(ele);
                if (index != -1)
                    list.RemoveAt(index);
            }

            return list;
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> a_enumerable,
                                               Predicate<T> a_predicate)
        {
            foreach (var ele in a_enumerable)
            {
                if (!a_predicate(ele))
                    yield return ele;
            }
        }

        public static bool ContainsAny<T>(this IEnumerable<T> a_enumerable, IEnumerable<T> a_values)
        {
            return a_enumerable.Intersect(a_values).Any();
        }

        public static bool ContainsAny<T>(this IEnumerable<T> a_enumerable,
            IEnumerable<T> a_values, IEqualityComparer<T> a_comparer)
        {
            return a_enumerable.Intersect(a_values, a_comparer).Any();
        }

        public static bool Contains<T>(this IEnumerable<T> a_enumerable, IEnumerable<T> a_values)
        {
            if (a_values.FirstOrDefault() == null)
                return false;

            foreach (var ele in a_values)
            {
                if (!a_enumerable.Contains(ele))
                    return false;
            }

            return true;
        }

        public static bool Contains<T>(this IEnumerable<T> a_enumerable, IEnumerable<T> a_values,
            IEqualityComparer<T> a_comparer)
        {
            if (a_values.FirstOrDefault() == null)
                return false;

            foreach (var ele in a_values)
            {
                if (!a_enumerable.Contains(ele, a_comparer))
                    return false;
            }

            return true;
        }

        public static bool Exact<T>(this IEnumerable<T> a_enumerable, IEnumerable<T> a_values)
        {
            var list = new List<T>(a_values);

            var init_count = list.Count;
            var count = 0;

            foreach (var ele in a_enumerable)
            {
                count++;

                if (count > init_count)
                    return false;

                var index = list.IndexOf(ele);
                if (index == -1)
                    return false;
                else
                    list.RemoveAt(index);
            }

            return count == init_count;
        }

        public static bool Exact<T>(this IEnumerable<T> a_enumerable, IEnumerable<T> a_values,
            IEqualityComparer<T> a_comparer)
        {
            var list = new List<T>(a_values);

            var init_count = list.Count;
            var count = 0;

            foreach (var ele in a_enumerable)
            {
                count++;

                if (count > init_count)
                    return false;

                var index = list.IndexOf(ele, a_comparer);
                if (index == -1)
                    return false;
                else
                    list.RemoveAt(index);
            }

            return count == init_count;
        }

        public static IEnumerable<T> Substract<T>(this IEnumerable<T> a_enumerable,
            IEnumerable<T> a_values)
        {
            var list = new List<T>(a_values);

            foreach (var ele in a_enumerable)
            {
                var index = list.IndexOf(ele);
                if (index != -1)
                    list.RemoveAt(index);
                else
                    yield return ele;
            }
        }

        public static IEnumerable<T> Substract<T>(this IEnumerable<T> a_enumerable,
            IEnumerable<T> a_values, IEqualityComparer<T> a_comparer)
        {
            var list = new List<T>(a_values);

            foreach (var ele in a_enumerable)
            {
                var index = list.IndexOf(ele, a_comparer);
                if (index != -1)
                    list.RemoveAt(index);
                else
                    yield return ele;
            }
        }

        public static bool ContainsExact<T>(this IEnumerable<T> a_enumerable,
            IEnumerable<T> a_values)
        {
            if (a_values.FirstOrDefault() == null)
                return false;

            var list = new List<T>(a_enumerable);

            foreach (var ele in a_values)
            {
                var index = list.IndexOf(ele);
                if (index == -1)
                    return false;
                else
                    list.RemoveAt(index);
            }

            return true;
        }

        public static bool ContainsExact<T>(this IEnumerable<T> a_enumerable,
            IEnumerable<T> a_values, IEqualityComparer<T> a_comparer)
        {
            var list = new List<T>(a_enumerable);

            if (a_values.FirstOrDefault() == null)
                return false;

            foreach (var ele in a_values)
            {
                var index = list.IndexOf(ele, a_comparer);
                if (index == -1)
                    return false;
                else
                    list.RemoveAt(index);
            }

            return true;
        }

        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> a_enum, int a_times = 1)
        {
            if (a_times == 0)
                yield break;

            for (var i = 0; i < a_times; i++)
            {
                foreach (var el in a_enum)
                    yield return el;
            }
        }

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> a_enum, int a_skip = 1)
        {
            return a_enum.Reverse().Skip(a_skip).Reverse();
        }

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> a_enum)
        {
            return a_enum.SelectMany(obj => obj);
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> a_enumerable, int a_from,
            int a_count)
        {
            return a_enumerable.Skip(a_from).Take(a_count);
        }

        public static IEnumerable<T> TakeAllOrOne<T>(this IEnumerable<T> a_enum, bool a_all,
            Func<T, bool> a_take)
        {
            var first = false;

            foreach (var el in a_enum)
            {
                if (first && !a_all)
                    yield break;

                if (!a_take(el))
                    continue;

                first = true;
                yield return el;
            }
        }
    }
}