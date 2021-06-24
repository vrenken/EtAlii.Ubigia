// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;

    public interface IItemRemover
    {
        void Remove<T>(IList<T> items, Guid itemId)
            where T : class, IIdentifiable;

        void Remove<T>(IList<T> items, T itemToRemove)
            where T : class, IIdentifiable;
    }
}