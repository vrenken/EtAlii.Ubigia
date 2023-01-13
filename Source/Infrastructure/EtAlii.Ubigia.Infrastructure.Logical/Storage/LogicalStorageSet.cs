// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Fabric;

public class LogicalStorageSet : ILogicalStorageSet
{
    private readonly IFabricContext _fabric;

    private const string Folder = "Storages";

    private ObservableCollection<Storage> Items { get; set; }

    public LogicalStorageSet(IFabricContext fabric)
    {
        _fabric = fabric;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Storage> GetAll()
    {
        return _fabric.Items.GetAll(Items);
    }

    /// <inheritdoc />
    public Task<Storage> Get(string name)
    {
        var storage = Items.SingleOrDefault(storage => storage.Name == name);
        return Task.FromResult(storage);
    }

    /// <inheritdoc />
    public Task<Storage> Get(Guid id)
    {
        return _fabric.Items.Get(Items, id);
    }

    /// <inheritdoc />
    public async Task<Storage> Add(Storage item)
    {
        return await _fabric.Items
            .Add(Items, CanAddFunction, item)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Storage> AddLocalStorage(Storage item)
    {
        // No check for local storage.
        return await _fabric.Items
            .Add(Items,  (_, _) => true, item)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task Start()
    {
        Items = await _fabric.Items
            .GetItems<Storage>(Folder)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task Stop()
    {
        // Nothing at this moment.
        return Task.CompletedTask;
    }

    private Storage UpdateFunction(Storage itemToUpdate, Storage updatedItem)
    {
        itemToUpdate.Address = updatedItem.Address;
        itemToUpdate.Name = updatedItem.Name;
        return itemToUpdate;
    }

    private bool CanAddFunction(IList<Storage> items, Storage item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "No item specified");
        }

        var canAdd = !string.IsNullOrWhiteSpace(item.Name);
        if (canAdd)
        {
            canAdd = item.Id == Guid.Empty;
        }
        if (canAdd)
        {
            canAdd = !items.Any(i => (string.CompareOrdinal(i.Name, item.Name) == 0 && string.CompareOrdinal(i.Address, item.Address) == 0) || i.Id == item.Id);
        }
        return canAdd;
    }

    /// <inheritdoc />
    public Task Remove(Guid itemId)
    {
        return _fabric.Items.Remove(Items, itemId);
    }

    /// <inheritdoc />
    public Task Remove(Storage itemToRemove)
    {
        return _fabric.Items.Remove(Items, itemToRemove);
    }

    /// <inheritdoc />
    public Task<Storage> Update(Guid itemId, Storage updatedItem)
    {
        return _fabric.Items.Update(Items, UpdateFunction, Folder, itemId, updatedItem);
    }
}
