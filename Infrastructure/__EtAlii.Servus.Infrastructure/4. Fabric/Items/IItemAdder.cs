namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IItemAdder
    {
        T Add<T>(IList<T> items, T item)
            where T : class, IIdentifiable;

        T Add<T>(IList<T> items, Func<IList<T>, T, bool> cannAddFunction, T item)
            where T : class, IIdentifiable;
    }
}