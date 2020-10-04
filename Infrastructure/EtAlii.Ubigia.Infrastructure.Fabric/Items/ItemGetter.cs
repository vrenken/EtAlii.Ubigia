namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using EtAlii.Ubigia.Persistence;

    internal class ItemGetter : IItemGetter
    {
        private readonly IStorage _storage;

        public ItemGetter(IStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<T> GetAll<T>(IList<T> items)
            where T : class, IIdentifiable
        {
            return items;
        }

        public T Get<T>(IList<T> items, Guid id)
            where T : class, IIdentifiable
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("No item ID specified");
            }

            return items.SingleOrDefault(item => item.Id == id);
        }

        public ObservableCollection<T> GetItems<T>(string folder)
            where T : class, IIdentifiable
        {
            var items = new ObservableCollection<T>();

            var containerId = _storage.ContainerProvider.ForItems(folder);
            var itemIds = _storage.Items.Get(containerId);
            foreach (var itemId in itemIds)
            {
                var item = _storage.Items.Retrieve<T>(itemId, containerId);
                items.Add(item);
            }

            items.CollectionChanged += (o, e) => OnItemsChanged<T>(e, folder);

            return items;
        }

        private void OnItemsChanged<T>(NotifyCollectionChangedEventArgs e, string folder) // object sender, 
            where T : class, IIdentifiable
        {
            var containerId = _storage.ContainerProvider.ForItems(folder);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (T item in e.NewItems)
                    {
                        _storage.Items.Store(item, item.Id, containerId);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                    {
                        _storage.Items.Remove(item.Id, containerId);
                    }
                    break;
            }
        }

    }
}