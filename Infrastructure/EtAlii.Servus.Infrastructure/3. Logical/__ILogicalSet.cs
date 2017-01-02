//namespace EtAlii.Servus.Infrastructure
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Collections.ObjectModel;
//    using EtAlii.Servus.Api;

//    public interface ILogicalSet<T>
//            where T : class, IIdentifiable
//    {
//        T Add(T item);

//        IEnumerable<T> GetAll();

//        T Get(Guid id);

//        ObservableCollection<T> GetItems();

//        void Remove(Guid itemId);

//        void Remove(T itemToRemove);

//        T Update(Guid itemId, T updatedItem);
//    }
//}