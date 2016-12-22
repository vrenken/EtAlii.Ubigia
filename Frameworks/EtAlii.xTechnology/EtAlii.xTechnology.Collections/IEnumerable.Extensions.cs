namespace EtAlii.xTechnology.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtensions
    {
        public static bool Multiple<T>(this IEnumerable<T> items)
        {
            var result = false;
            if (items.Any())
            {
                result = items.Skip(1).Any();
            }
            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }

        public static string ToCommaSeperatedList<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.Select(item => item.ToString());
            return String.Join(", ", list);
        }

        /// <summary>
        /// Flattens the hierarchy and returns each element within the tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="selector">The selector that selects the child nodes.</param>
        /// <remarks>
        /// Got it of StackOverflow but forgot to copy the link. Can't find it anymore...
        /// </remarks>
        public static IEnumerable<T> Flatten<T>(this IEnumerable collection, Func<T, IEnumerable> selector)
        {
            Stack<IEnumerable<T>> stack = new Stack<IEnumerable<T>>();
            stack.Push(collection.OfType<T>());

            while (stack.Count > 0)
            {
                IEnumerable<T> items = stack.Pop();
                foreach (var item in items)
                {
                    yield return item;

                    IEnumerable<T> children = selector(item).OfType<T>();
                    stack.Push(children);
                }
            }
        }
    }
}
