// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Collections;

using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    public static void ClearByRemove<T>(this ICollection<T> list)
    {
        var itemsToRemove = list.ToArray();
        foreach (var itemToRemove in itemsToRemove)
        {
            list.Remove(itemToRemove);
        }
    }

    /// <summary>
    /// Use this method when you want items only added once to a list.
    /// </summary>
    /// <typeparam name="T">The type of the list and items.</typeparam>
    /// <param name="list">The list to add the item to.</param>
    /// <param name="itemsToAdd">The items to add to the list.</param>
    public static void AddRangeOnce<T>(this ICollection<T> list, IEnumerable<T> itemsToAdd)
    {
        foreach (var item in itemsToAdd)
        {
            list.AddOnce(item, i => i.Equals(item));
        }
    }

    /// <summary>
    /// Use this method when you want an item only added once to a list.
    /// </summary>
    /// <typeparam name="T">The type of the list and items.</typeparam>
    /// <param name="list">The list to add the item to.</param>
    /// <param name="item">The item to add to the list.</param>
    public static void AddOnce<T>(this ICollection<T> list, T item)
    {
        list.AddOnce(item, i => i.Equals(item));
    }

    /// <summary>
    /// Use this method when you want an item only added once to a list.
    /// </summary>
    /// <typeparam name="T">The type of the list and items.</typeparam>
    /// <param name="list">The list to add the item to.</param>
    /// <param name="item">The item to add to the list.</param>
    /// <param name="predicate">A function to check if the item is already available in the list.</param>
    public static void AddOnce<T>(this ICollection<T> list, T item, Func<T, bool> predicate)
    {
        var alreadyInList = list.Any(predicate);
        if (!alreadyInList)
        {
            list.Add(item);
        }
    }

    /// <summary>
    /// Use this method to add a whole list of items to the collection at once.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="itemsToAdd"></param>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
    {
        foreach (var item in itemsToAdd)
        {
            collection.Add(item);
        }
    }

    /// <summary>
    /// Use this method to remove a whole list of items from the collection at once.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="itemsToRemove"></param>
    public static bool RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToRemove)
    {
        var result = true;
        foreach (var item in itemsToRemove)
        {
            result &= collection.Remove(item);
        }
        return result;
    }

    /// <summary>
    /// Use this method to add unique items to a collection. Uniqueness is determined using
    /// the specified equalityComparer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="itemsToAdd"></param>
    /// <param name="equalityComparer"></param>
    public static void AddRangeUnique<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd, IEqualityComparer<T> equalityComparer)
    {
        var newCollection = collection
            .Union(itemsToAdd)
            .Distinct(equalityComparer);
        collection.Clear();
        collection.AddRange(newCollection);
    }
}
