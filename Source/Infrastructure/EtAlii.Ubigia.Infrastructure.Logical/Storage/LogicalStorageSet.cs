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
        private readonly ILogicalContextOptions _options;

        private const string Folder = "Storages";

        private ObservableCollection<Storage> Items { get; set; }

        /// <inheritdoc />
	    public Func<Storage, Task> Initialized { get; set; }

        /// <inheritdoc />
        public Func<Storage, Task> Added { get; set; }

        public LogicalStorageSet(
            ILocalStorageGetter localStorageGetter,
            ILogicalContextOptions options,
            IFabricContext fabric)
        {
            _fabric = fabric;
            _localStorageGetter = localStorageGetter;
            _options = options;
        }

        /// <inheritdoc />
        public Storage GetLocal()
        {
            return _localStorageGetter.GetLocal(Items);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Storage> GetAll()
        {
            return _fabric.Items.GetAll(Items);
        }

        /// <inheritdoc />
        public Storage Get(string name)
        {
            return Items.SingleOrDefault(storage => storage.Name == name);
        }

        /// <inheritdoc />
        public Storage Get(Guid id)
        {
            return _fabric.Items.Get(Items, id);
        }

        /// <inheritdoc />
        public async Task<Storage> Add(Storage item)
        {
            item = _fabric.Items.Add(Items, CannAddFunction, item);

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
                var storage = _localStorageGetter.GetLocal(items);
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

        private bool CannAddFunction(IList<Storage> items, Storage item)
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
        public void Remove(Guid itemId)
        {
            _fabric.Items.Remove(Items, itemId);
        }

        /// <inheritdoc />
        public void Remove(Storage itemToRemove)
        {
            _fabric.Items.Remove(Items, itemToRemove);
        }

        /// <inheritdoc />
        public Storage Update(Guid itemId, Storage updatedItem)
        {
            return _fabric.Items.Update(Items, UpdateFunction, Folder, itemId, updatedItem);
        }
    }
}
