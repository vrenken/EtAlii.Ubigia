namespace EtAlii.Servus.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(Guid itemId);

        T Add(T item);

        void Remove(Guid itemId);
        void Remove(T item);

        T Update(Guid itemId, T item);
    }
}