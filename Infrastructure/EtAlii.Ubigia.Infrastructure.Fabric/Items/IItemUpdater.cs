namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public interface IItemUpdater
    {
        T Update<T>(IList<T> items, Func<T, T, T> updateFunction, string folder, Guid itemId, T updatedItem)
            where T : class, IIdentifiable;
    }
}