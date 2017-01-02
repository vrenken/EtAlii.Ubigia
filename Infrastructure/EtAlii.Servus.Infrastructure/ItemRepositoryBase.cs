namespace EtAlii.Servus.Infrastructure.Model
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.Servus.Storage;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    public abstract class ItemRepositoryBase<T> : SingleFolderRepositoryBase<T>, IRepository<T>
        where T : class, IIdentifiable
    {
        protected ObservableCollection<T> Items { get { return _items.Value; } }
        private readonly Lazy<ObservableCollection<T>> _items;

        private readonly IItemRemover _itemRemover;

        public ItemRepositoryBase(IItemStorage itemStorage, IHostingConfiguration configuration, IItemRemover itemRemover)
            : base(itemStorage, configuration)
        {
            _items = new Lazy<ObservableCollection<T>>(GetItems);
            _itemRemover = itemRemover;
        }

        protected virtual bool CanAdd(T item)
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
                canAdd = !Items.Any(i => i.Name == item.Name || i.Id == item.Id);
            }
            return canAdd;
        }

        protected abstract T Update(T originalItem, T updatedItem);

        #region Get

        public IEnumerable<T> GetAll()
        {
            return Items;
        }

        public T Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("No item ID specified");
            }

            return Items.SingleOrDefault(item => item.Id == id);
        }

        #endregion Get

        #region Add

        public virtual T Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("No item specified");
            }

            var canAdd = CanAdd(item);
            if (canAdd)
            {
                try
                {
                    item.Id = item.Id != Guid.Empty ? item.Id : Guid.NewGuid();
                    Items.Add(item);
                }
                catch
                {
                    Items.Remove(item);
                    throw;
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to add the specified item");
            }
            return canAdd ? item : null;
        }

        #endregion Add

        public void Remove(Guid itemId)
        {
            _itemRemover.Remove(Items, itemId);
        }

        public void Remove(T itemToRemove)
        {
            _itemRemover.Remove(Items, itemToRemove);
        }

        #region Update

        public T Update(Guid itemId, T updatedItem)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentException("No item ID specified");
            }
            if (updatedItem == null)
            {
                throw new ArgumentNullException("No item specified");
            }

            var itemToUpdate = Items.SingleOrDefault(item => item.Id == itemId);
            int index = Items.IndexOf(itemToUpdate);

            updatedItem = Update(itemToUpdate, updatedItem);

            var containerId = ContainerIdentifier.FromPaths(Folder);
            ItemStorage.Store(updatedItem, updatedItem.Id, containerId);

            Items[index] = updatedItem;
            return updatedItem;
        }

        #endregion Update

        private ObservableCollection<T> GetItems()
        {
            var items = new ObservableCollection<T>();

            var containerId = ContainerIdentifier.FromPaths(Folder);
            var itemIds = ItemStorage.Get(containerId);
            foreach (var itemId in itemIds)
            {
                var item = ItemStorage.Retrieve<T>(itemId, containerId);
                items.Add(item);
            }

            items.CollectionChanged += OnItemsChanged;

            return items;
        }

        private void OnItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var containerId = ContainerIdentifier.FromPaths(Folder);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (T item in e.NewItems)
                    {
                        ItemStorage.Store(item, item.Id, containerId);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                    {
                        ItemStorage.Remove(item.Id, containerId);
                    }
                    break;
            }
        }
    }
}