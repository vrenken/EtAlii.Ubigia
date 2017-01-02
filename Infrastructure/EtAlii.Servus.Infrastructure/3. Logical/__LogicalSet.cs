namespace EtAlii.Servus.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Servus.Api;

    //public abstract class LogicalSet<T> : ILogicalSet<T>
    //        where T : class, IIdentifiable
    //{
        //private readonly IFabricContext _fabric;

        //protected ObservableCollection<T> Items { get { lock (_lockObject) { return _items ?? (_items = InitializeItems()); } } }
        //private ObservableCollection<T> _items; // We don't us a Lazy construction here because the first get of this property is actually cascaded through the logical layer. A Lazy instance results in a deadlock.

        //private readonly string _folder;
        //private readonly object _lockObject = new object();

        //public LogicalSet(IFabricContext fabric)
        //{
        //    _fabric = fabric;
        //    _folder = GetFolder();
        //}

        //protected abstract string GetFolder();
        //protected abstract bool CannAddFunction(IList<T> items, T item);
        //protected abstract T UpdateFunction(T itemToUpdate, T updatedItem);

        //protected virtual ObservableCollection<T> InitializeItems()
        //{
        //    var items = _fabric.Items.GetItems<T>(_folder);
        //    return items;
        //}

        //public virtual T Add(T item)
        //{
        //    return _fabric.Items.Add(Items, CannAddFunction, item);
        //    //return _fabric.Items.Add(Items, item);
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return _fabric.Items.GetAll(Items);
        //}

        //public T Get(Guid id)
        //{
        //    return _fabric.Items.Get(Items, id);
        //}

        //public ObservableCollection<T> GetItems()
        //{
        //    return _fabric.Items.GetItems<T>(_folder);
        //}

        //public void Remove(Guid itemId)
        //{
        //    _fabric.Items.Remove<T>(Items, itemId);
        //}

        //public void Remove(T itemToRemove)
        //{
        //    _fabric.Items.Remove<T>(Items, itemToRemove);
        //}

        //public T Update(Guid itemId, T updatedItem)
        //{
        //    return _fabric.Items.Update<T>(Items, UpdateFunction, _folder, itemId, updatedItem);
        //}
    //}
}