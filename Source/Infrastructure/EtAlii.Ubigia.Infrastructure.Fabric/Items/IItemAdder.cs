// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemAdder
{
    Task<T> Add<T>(IList<T> items, T item)
        where T : class, IIdentifiable;

    Task<T> Add<T>(IList<T> items, Func<IList<T>, T, bool> canAddFunction, T item)
        where T : class, IIdentifiable;
}
