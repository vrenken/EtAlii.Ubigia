// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemUpdater
{
    Task<T> Update<T>(IList<T> items, Func<T, T, T> updateFunction, string folder, Guid itemId, T updatedItem)
        where T : class, IIdentifiable;
}
