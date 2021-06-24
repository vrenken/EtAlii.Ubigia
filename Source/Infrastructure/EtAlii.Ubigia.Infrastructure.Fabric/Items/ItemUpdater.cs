// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Persistence;

    internal class ItemUpdater : IItemUpdater
    {
        private readonly IStorage _storage;

        public ItemUpdater(IStorage storage)
        {
            _storage = storage;
        }

        public T Update<T>(IList<T> items, Func<T, T, T> updateFunction, string folder, Guid itemId, T updatedItem)
            where T : class, IIdentifiable
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentException("No item ID specified");
            }
            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem), "No item specified");
            }

            var itemToUpdate = items.SingleOrDefault(item => item.Id == itemId);
            var index = items.IndexOf(itemToUpdate);

            updatedItem = updateFunction(itemToUpdate, updatedItem);

            var containerId = _storage.ContainerProvider.ForItems(folder);
            _storage.Items.Store(updatedItem, updatedItem.Id, containerId);

            items[index] = updatedItem;
            return updatedItem;
        }
    }
}