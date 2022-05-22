// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IItemRemover
    {
        Task Remove<T>(IList<T> items, Guid itemId)
            where T : class, IIdentifiable;

        Task Remove<T>(IList<T> items, T itemToRemove)
            where T : class, IIdentifiable;
    }
}
