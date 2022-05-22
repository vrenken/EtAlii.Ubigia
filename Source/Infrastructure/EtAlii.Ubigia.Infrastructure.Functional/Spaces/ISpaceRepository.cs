// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISpaceRepository
    {
        Task<Space> Get(Guid accountId, string spaceName);
        Task<Space> Get(Guid itemId);

        IAsyncEnumerable<Space> GetAll(Guid accountId);
        IAsyncEnumerable<Space> GetAll();

        Task<Space> Add(Space item, SpaceTemplate template);

        Task Remove(Guid itemId);
        Task Remove(Space item);

        Task<Space> Update(Guid itemId, Space item);
    }
}
