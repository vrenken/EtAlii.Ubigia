// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
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
            foreach (var item in items)
            {
                action(item);
            }
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
            var stack = new Stack<IEnumerable<T>>();
            stack.Push(collection.OfType<T>());

            while (stack.Count > 0)
            {
                var items = stack.Pop();
                foreach (var item in items)
                {
                    yield return item;

                    var children = selector(item).OfType<T>();
                    stack.Push(children);
                }
            }
        }
    }
}
