// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public class ItemsSet : IItemsSet
{
    private readonly IItemAdder _itemAdder;
    private readonly IItemGetter _itemGetter;
    private readonly IItemRemover _itemRemover;
    private readonly IItemUpdater _itemUpdater;

    public ItemsSet(
        IItemAdder itemAdder,
        IItemGetter itemGetter,
        IItemRemover itemRemover,
        IItemUpdater itemUpdater)
    {
        _itemAdder = itemAdder;
        _itemRemover = itemRemover;
        _itemUpdater = itemUpdater;
        _itemGetter = itemGetter;
    }

    /// <inheritdoc />
    public Task<T> Add<T>(IList<T> items, T item)
        where T : class, IIdentifiable
    {
        return _itemAdder.Add(items, item);
    }

    /// <inheritdoc />
    public Task<T> Add<T>(IList<T> items, Func<IList<T>, T, bool> canAddFunction, T item)
        where T : class, IIdentifiable
    {
        return _itemAdder.Add(items, canAddFunction, item);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<T> GetAll<T>(IList<T> items)
        where T : class, IIdentifiable
    {
        return _itemGetter.GetAll(items);
    }

    /// <inheritdoc />
    public Task<T> Get<T>(IList<T> items, Guid id)
        where T : class, IIdentifiable
    {
        return _itemGetter.Get(items, id);
    }

    /// <inheritdoc />
    public Task<ObservableCollection<T>> GetItems<T>(string folder)
        where T : class, IIdentifiable
    {
        return _itemGetter.GetItems<T>(folder);
    }

    /// <inheritdoc />
    public Task Remove<T>(IList<T> items, Guid itemId)
        where T : class, IIdentifiable
    {
        return _itemRemover.Remove(items, itemId);
    }

    /// <inheritdoc />
    public Task Remove<T>(IList<T> items, T itemToRemove)
        where T : class, IIdentifiable
    {
        return _itemRemover.Remove(items, itemToRemove);
    }

    /// <inheritdoc />
    public Task<T> Update<T>(IList<T> items, Func<T, T, T> updateFunction, string folder, Guid itemId, T updatedItem)
        where T : class, IIdentifiable
    {
        return _itemUpdater.Update(items, updateFunction, folder, itemId, updatedItem);
    }
}
