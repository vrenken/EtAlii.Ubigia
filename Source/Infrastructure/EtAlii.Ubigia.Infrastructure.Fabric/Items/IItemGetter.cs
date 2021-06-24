// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IItemGetter
    {
        IAsyncEnumerable<T> GetAll<T>(IList<T> items)
            where T : class, IIdentifiable;

        T Get<T>(IList<T> items, Guid id)
            where T : class, IIdentifiable;

        Task<ObservableCollection<T>> GetItems<T>(string folder)
            where T : class, IIdentifiable;
    }
}