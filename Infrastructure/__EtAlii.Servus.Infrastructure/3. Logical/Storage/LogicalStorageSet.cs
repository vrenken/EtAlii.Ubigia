namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Fabric;

    public class LogicalStorageSet : ILogicalStorageSet
    {
        private readonly IFabricContext _fabric;
        private readonly ILocalStorageGetter _localStorageGetter;
        private readonly ILogicalContextConfiguration _configuration;
        private readonly object _lockObject = new object();

        private const string _folder = "Storages";

        private ObservableCollection<Storage> Items { get { return _items; } }
        private ObservableCollection<Storage> _items; // We don't us a Lazy construction here because the first get of this property is actually cascaded through the logical layer. A Lazy instance results in a deadlock.

        public event EventHandler<Storage> LocalStorageInitialized { add { _localStorageInitialized += value; } remove { var initialized = _localStorageInitialized; if (initialized != null) initialized -= value; } }
        private EventHandler<Storage> _localStorageInitialized;

        public event EventHandler<Storage> StorageInitialized { add { _storageInitialized += value; } remove { var initialized = _storageInitialized; if (initialized != null) initialized -= value; } }
        private EventHandler<Storage> _storageInitialized;

        public LogicalStorageSet(
            ILocalStorageGetter localStorageGetter, 
            ILogicalContextConfiguration configuration,
            IFabricContext fabric)
        {
            _fabric = fabric;
            _localStorageGetter = localStorageGetter;
            _configuration = configuration;
        }

        public Storage Add(Storage storage)
        {
            storage = _fabric.Items.Add(Items, CannAddFunction, storage);

            if (storage != null)
            {
                _storageInitialized?.Invoke(this, storage);
            }
            return storage;
        }

        public void Start()
        {
            var items = _fabric.Items.GetItems<Storage>(_folder);

            // TODO: This test to see if the local storage has already been added is not very stable. 
            // Please find another way to determine that the local storage needs initialization.
            var isAlreadyRegistered = items.Any(s => s.Name == _configuration.Name);
            if (!isAlreadyRegistered)
            {
                var storage = _localStorageGetter.GetLocal(items);
                items.Add(storage);

                _localStorageInitialized?.Invoke(this, storage);
            }

            _items = items;
        }

        public void Stop()
        {
            // Nothing at this moment.
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
                throw new ArgumentNullException("No item specified");
            }

            var canAdd = !String.IsNullOrWhiteSpace(item.Name);
            if (canAdd)
            {
                canAdd = item.Id == Guid.Empty;
            }
            if (canAdd)
            {
                canAdd = !items.Any(i => (String.CompareOrdinal(i.Name, item.Name) == 0 && String.CompareOrdinal(i.Address, item.Address) == 0) || i.Id == item.Id);
            }
            return canAdd;
        }

        public Storage GetLocal()
        {
            return _localStorageGetter.GetLocal(Items);
        }

        public Storage Get(string name)
        {
            return Items.SingleOrDefault(storage => storage.Name == name);
        }

        public IEnumerable<Storage> GetAll()
        {
            return _fabric.Items.GetAll(Items);
        }

        public Storage Get(Guid id)
        {
            return _fabric.Items.Get(Items, id);
        }

        public ObservableCollection<Storage> GetItems()
        {
            return _fabric.Items.GetItems<Storage>(_folder);
        }

        public void Remove(Guid itemId)
        {
            _fabric.Items.Remove<Storage>(Items, itemId);
        }

        public void Remove(Storage itemToRemove)
        {
            _fabric.Items.Remove<Storage>(Items, itemToRemove);
        }

        public Storage Update(Guid itemId, Storage updatedItem)
        {
            return _fabric.Items.Update<Storage>(Items, UpdateFunction, _folder, itemId, updatedItem);
        }
    }
}