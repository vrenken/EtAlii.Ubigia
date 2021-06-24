// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A set of <see cref="T:System.Collections.Generic.IEnumerable`1" /> extensions methods
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Projects each element of a sequence recursively to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> 
        /// and flattens the resulting sequences into one sequence. 
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements 
        /// who are the result of invoking the recursive transform function on each element of the input sequence. 
        /// </returns>
        /// <example>
        /// node.ChildNodes.SelectRecursive(n => n.ChildNodes);
        /// </example>
        public static IEnumerable<IRecursion<T>> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            return SelectRecursive(source, selector, null);
        }
        /// <summary>
        /// Projects each element of a sequence recursively to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> 
        /// and flattens the resulting sequences into one sequence. 
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="predicate">A function to test each element for a condition in each recursion.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of 
        /// invoking the recursive transform function on each element of the input sequence. 
        /// </returns>
        /// <example>
        /// node.ChildNodes.SelectRecursive(n => n.ChildNodes, m => m.Depth &lt; 2);
        /// </example>
        public static IEnumerable<IRecursion<T>> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector, Func<IRecursion<T>, bool> predicate)
        {
            return SelectRecursive(source, selector, predicate, 0);
        }
        private static IEnumerable<IRecursion<T>> SelectRecursive<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> selector, Func<IRecursion<T>, bool> predicate, int depth)
        {
            var q = source
                .Select(item => new Recursion<T>(depth, item))
                .Cast<IRecursion<T>>();
            if (predicate != null)
                q = q.Where(predicate);
            foreach (var item in q)
            {
                yield return item;
                foreach (var item2 in SelectRecursive(selector(item.Item), selector, predicate, depth + 1))
                    yield return item2;
            }
        }
        private class Recursion<T> : IRecursion<T>
        {
            public int Depth { get; }

            public T Item { get; }

            public Recursion(int depth, T item)
            {
                Depth = depth;
                Item = item;
            }
        } 
    }
 
    /// <summary>
    /// Represents an item in a recursive projection.
    /// </summary>
    /// <typeparam name="T">The type of the item</typeparam>
    public interface IRecursion<out T>
    {
        /// <summary>
        /// Gets the recursive depth.
        /// </summary>
        /// <value>The depth.</value>
        int Depth { get; }
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        T Item { get; }
    }
}
