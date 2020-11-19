﻿namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRootSet
    {
        Task<Root> Add(Guid spaceId, Root root);

        IAsyncEnumerable<Root> GetAll(Guid spaceId);
        Task<Root> Get(Guid spaceId, Guid rootId);
        Task<Root> Get(Guid spaceId, string name);


        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);

        Task<Root> Update(Guid spaceId, Guid rootId, Root updatedRoot);
    }
}