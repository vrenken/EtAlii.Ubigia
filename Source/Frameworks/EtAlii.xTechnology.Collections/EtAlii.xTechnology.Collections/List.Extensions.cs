// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    public static class ListExtensions
    {
        /// <summary>
        /// Use this method to add a whole list of items to the list at once.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemsToAdd"></param>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> itemsToAdd)
        {
            foreach (var item in itemsToAdd)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Use this method to add a whole list of items to the list at once.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="itemsToAdd"></param>
        public static void AddRange(this IList list, IEnumerable itemsToAdd)
        {
            foreach (var item in itemsToAdd)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Use this method to remove a whole list of items from the list at once.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemsToRemove"></param>
        public static bool RemoveRange<T>(this IList<T> list, IEnumerable<T> itemsToRemove)
        {
            var result = true;
            foreach (var item in itemsToRemove)
            {
                result &= list.Remove(item);
            }
            return result;
        }

        /// <summary>
        /// Use this method to remove a whole list of items from the list at once.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="itemsToRemove"></param>
        public static void RemoveRange(this IList list, IEnumerable itemsToRemove)
        {
            foreach (var item in itemsToRemove)
            {
                list.Remove(item);
            }
        }
    }
}
