namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Servus.Api;

    public interface IItemGetter
    {
        IEnumerable<T> GetAll<T>(IList<T> items)
            where T : class, IIdentifiable;

        T Get<T>(IList<T> items, Guid id)
            where T : class, IIdentifiable;

        ObservableCollection<T> GetItems<T>(string folder)
            where T : class, IIdentifiable;
    }
}