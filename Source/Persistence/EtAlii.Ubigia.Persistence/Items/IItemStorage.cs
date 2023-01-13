// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System;
using System.Threading.Tasks;

public interface IItemStorage
{
    void Store<T>(T item, Guid id, ContainerIdentifier container)
        where T : class;
    Task<T> Retrieve<T>(Guid id, ContainerIdentifier container)
        where T : class;
    void Remove(Guid id, ContainerIdentifier container);
    Guid[] Get(ContainerIdentifier container);
    bool Has(Guid id, ContainerIdentifier container);
}
