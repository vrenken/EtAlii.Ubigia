// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILogicalSpaceSet
    {
        IAsyncEnumerable<Space> GetAll(Guid accountId);
        IAsyncEnumerable<Space> GetAll();

        Task<Space> Get(Guid accountId, string spaceName);

        Task<Space> Get(Guid id);

        Task<(Space, bool)> Add(Space item, SpaceTemplate template);

        Task Remove(Guid itemId);

        Task Remove(Space itemToRemove);

        Task<Space> Update(Guid itemId, Space updatedItem);
    }
}
