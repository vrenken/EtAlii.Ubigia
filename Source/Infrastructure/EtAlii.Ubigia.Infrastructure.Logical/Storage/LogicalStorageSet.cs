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
        private readonly ILogicalContextConfiguration _configuration;

        private const string Folder = "Storages";

        private ObservableCollection<Storage> Items { get; set; }

	    public Func<Storage, Task> Initialized { get; set; }
	    public Func<Storage, Task> Added { get; set; }

        public LogicalStorageSet(
            ILocalStorageGetter localStorageGetter,
            ILogicalContextConfiguration configuration,
            IFabricContext fabric)
        {
            _fabric = fabric;
            _localStorageGetter = localStorageGetter;
            _configuration = configuration;
        }

        public Storage GetLocal()
        {
            return _localStorageGetter.GetLocal(Items);
        }

        public IAsyncEnumerable<Storage> GetAll()
        {
            return _fabric.Items.GetAll(Items);
        }

        public Storage Get(string name)
        {
            return Items.SingleOrDefault(storage => storage.Name == name);
        }

        public Storage Get(Guid id)
        {
            return _fabric.Items.Get(Items, id);
        }

        public async Task<Storage> Add(Storage item)
        {
            item = _fabric.Items.Add(Items, CannAddFunction, item);

            if (item != null && Added != null)
            {
                await Added.Invoke(item).ConfigureAwait(false);
            }
            return item;
        }

        public async Task Start()
        {
            var items = await _fabric.Items.GetItems<Storage>(Folder).ConfigureAwait(false);

            // TODO: This test to see if the local storage has already been added is not very stable.
            // Please find another way to determine that the local storage needs initialization.
            var isAlreadyRegistered = items.Any(s => s.Name == _configuration.Name);
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

        public void Remove(Guid itemId)
        {
            _fabric.Items.Remove(Items, itemId);
        }

        public void Remove(Storage itemToRemove)
        {
            _fabric.Items.Remove(Items, itemToRemove);
        }

        public Storage Update(Guid itemId, Storage updatedItem)
        {
            return _fabric.Items.Update(Items, UpdateFunction, Folder, itemId, updatedItem);
        }
    }
}
