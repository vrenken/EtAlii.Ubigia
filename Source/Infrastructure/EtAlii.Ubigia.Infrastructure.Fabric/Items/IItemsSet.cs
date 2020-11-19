namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IItemsSet
    {
        T Add<T>(IList<T> items, T item)
            where T : class, IIdentifiable;

        T Add<T>(IList<T> items, Func<IList<T>, T, bool> cannAddFunction, T item)
            where T : class, IIdentifiable;


        IAsyncEnumerable<T> GetAll<T>(IList<T> items)
            where T : class, IIdentifiable;

        T Get<T>(IList<T> items, Guid id)
            where T : class, IIdentifiable;

        Task<ObservableCollection<T>> GetItems<T>(string folder)
            where T : class, IIdentifiable;


        void Remove<T>(IList<T> items, Guid itemId)
            where T : class, IIdentifiable;

        void Remove<T>(IList<T> items, T itemToRemove)
            where T : class, IIdentifiable;


        T Update<T>(IList<T> items, Func<T, T, T> updateFunction, string folder, Guid itemId, T updatedItem)
            where T : class, IIdentifiable;
    }
}