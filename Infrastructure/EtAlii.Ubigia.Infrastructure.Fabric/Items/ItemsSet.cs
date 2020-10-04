namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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

        public T Add<T>(IList<T> items, T item) 
            where T : class, IIdentifiable
        {
            return _itemAdder.Add(items, item);
        }

        public T Add<T>(IList<T> items, Func<IList<T>, T, bool> cannAddFunction, T item) 
            where T : class, IIdentifiable
        {
            return _itemAdder.Add(items, cannAddFunction, item);
        }

        public IEnumerable<T> GetAll<T>(IList<T> items) 
            where T : class, IIdentifiable
        {
            return _itemGetter.GetAll(items);
        }

        public T Get<T>(IList<T> items, Guid id) 
            where T : class, IIdentifiable
        {
            return _itemGetter.Get(items, id);
        }

        public ObservableCollection<T> GetItems<T>(string folder) 
            where T : class, IIdentifiable
        {
            return _itemGetter.GetItems<T>(folder);
        }

        public void Remove<T>(IList<T> items, Guid itemId) 
            where T : class, IIdentifiable
        {
            _itemRemover.Remove(items, itemId);
        }

        public void Remove<T>(IList<T> items, T itemToRemove) 
            where T : class, IIdentifiable
        {
            _itemRemover.Remove(items, itemToRemove);
        }

        public T Update<T>(IList<T> items, Func<T, T, T> updateFunction, string folder, Guid itemId, T updatedItem) 
            where T : class, IIdentifiable
        {
            return _itemUpdater.Update(items, updateFunction, folder, itemId, updatedItem);
        }
    }
}