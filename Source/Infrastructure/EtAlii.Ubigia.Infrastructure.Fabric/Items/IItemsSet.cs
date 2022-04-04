// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IItemsSet
    {
        /// <summary>
        /// Add a new item to the item set.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Add<T>(IList<T> items, T item)
            where T : class, IIdentifiable;

        /// <summary>
        /// Add a new item to the item set, but first check if the item can be added.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="canAddFunction"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Add<T>(IList<T> items, Func<IList<T>, T, bool> canAddFunction, T item)
            where T : class, IIdentifiable;


        /// <summary>
        /// Return all items registered in this item set.
        /// </summary>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IAsyncEnumerable<T> GetAll<T>(IList<T> items)
            where T : class, IIdentifiable;

        T Get<T>(IList<T> items, Guid id)
            where T : class, IIdentifiable;

        Task<ObservableCollection<T>> GetItems<T>(string folder)
            where T : class, IIdentifiable;


        /// <summary>
        /// Remove the item with the specified id from the item set.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="itemId"></param>
        /// <typeparam name="T"></typeparam>
        void Remove<T>(IList<T> items, Guid itemId)
            where T : class, IIdentifiable;

        /// <summary>
        /// Remove the specified items from the item set.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="itemToRemove"></param>
        /// <typeparam name="T"></typeparam>
        void Remove<T>(IList<T> items, T itemToRemove)
            where T : class, IIdentifiable;


        /// <summary>
        /// Update the item with the specified itemId to the state of updatedItem.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="updateFunction"></param>
        /// <param name="folder"></param>
        /// <param name="itemId"></param>
        /// <param name="updatedItem"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Update<T>(IList<T> items, Func<T, T, T> updateFunction, string folder, Guid itemId, T updatedItem)
            where T : class, IIdentifiable;
    }
}
