// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalStorageSet : ILogicalStorageSet
    {
        private readonly IFabricContext _fabric;
        private readonly ILocalStorageGetter _localStorageGetter;
        private readonly LogicalContextOptions _options;

        private const string Folder = "Storages";

        private ObservableCollection<Storage> Items { get; set; }

        /// <inheritdoc />
	    public Func<Storage, Task> Initialized { get; set; }

        /// <inheritdoc />
        public Func<Storage, Task> Added { get; set; }

        public LogicalStorageSet(
            ILocalStorageGetter localStorageGetter,
            LogicalContextOptions options,
            IFabricContext fabric)
        {
            _fabric = fabric;
            _localStorageGetter = localStorageGetter;
            _options = options;
        }

        /// <inheritdoc />
        public Task<Storage> GetLocal()
        {
            return _localStorageGetter.GetLocal(Items);
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
            item = await _fabric.Items.Add(Items, CanAddFunction, item).ConfigureAwait(false);

            if (item != null && Added != null)
            {
                await Added.Invoke(item).ConfigureAwait(false);
            }
            return item;
        }

        /// <inheritdoc />
        public async Task Start()
        {
            var items = await _fabric.Items
                .GetItems<Storage>(Folder)
                .ConfigureAwait(false);

            // Improve this method and use a better way to decide if the local Storage needs to be added.
            // This current test to see if the local storage has already been added is not very stable/scalable.
            // Please find another way to determine that the local storage needs initialization.
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/94
            var isAlreadyRegistered = items.Any(s => s.Name == _options.Name);
            if (!isAlreadyRegistered)
            {
                var storage = await _localStorageGetter.GetLocal(items).ConfigureAwait(false);
                items.Add(storage);

                if (Initialized != null)
                {
                    await Initialized(storage).ConfigureAwait(false);
                }
            }

            Items = items;
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
}
