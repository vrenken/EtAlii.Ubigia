// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;

    public interface IItemUpdater
    {
        T Update<T>(IList<T> items, Func<T, T, T> updateFunction, string folder, Guid itemId, T updatedItem)
            where T : class, IIdentifiable;
    }
}