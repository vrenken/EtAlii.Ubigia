﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal class ItemAdder : IItemAdder
{
    private bool CanAdd<T>(IList<T> items, T item)
        where T : class, IIdentifiable
    {
        var canAdd = !string.IsNullOrWhiteSpace(item.Name);
        if (canAdd)
        {
            canAdd = item.Id == Guid.Empty;
        }
        if (canAdd)
        {
            canAdd = !items.Any(i => i.Name == item.Name || i.Id == item.Id);
        }
        return canAdd;
    }

    /// <inheritdoc />
    public Task<T> Add<T>(IList<T> items, T item)
        where T : class, IIdentifiable
    {
        return Add(items, CanAdd, item);
    }

    /// <inheritdoc />
    public Task<T> Add<T>(IList<T> items, Func<IList<T>, T, bool> canAddFunction, T item)
        where T : class, IIdentifiable
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "No item specified");
        }

        var canAdd = canAddFunction(items, item);
        if (canAdd)
        {
            try
            {
                item.Id = item.Id != Guid.Empty ? item.Id : Guid.NewGuid();
                items.Add(item);
            }
            catch
            {
                items.Remove(item);
                throw;
            }
        }
        else
        {
            throw new InvalidOperationException("Unable to add the specified item");
        }
        return Task.FromResult(item);
    }
}
