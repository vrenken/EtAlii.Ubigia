﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRootRepository
    {
        IAsyncEnumerable<Root> GetAll(Guid spaceId);

        Task<Root> Get(Guid spaceId, Guid rootId);
        Task<Root> Get(Guid spaceId, string name);

        Task<Root> Add(Guid spaceId, Root root);

        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);

        Task<Root> Update(Guid spaceId, Guid rootId, Root root);
    }
}