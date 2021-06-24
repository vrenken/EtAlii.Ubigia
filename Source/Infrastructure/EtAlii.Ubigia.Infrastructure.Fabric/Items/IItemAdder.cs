// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;

    public interface IItemAdder
    {
        T Add<T>(IList<T> items, T item)
            where T : class, IIdentifiable;

        T Add<T>(IList<T> items, Func<IList<T>, T, bool> cannAddFunction, T item)
            where T : class, IIdentifiable;
    }
}