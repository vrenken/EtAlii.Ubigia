// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISpaceRepository 
    {
        Space Get(Guid accountId, string spaceName);
        Space Get(Guid itemId);

        IAsyncEnumerable<Space> GetAll(Guid accountId);
        IAsyncEnumerable<Space> GetAll();

        Task<Space> Add(Space item, SpaceTemplate template);

        void Remove(Guid itemId);
        void Remove(Space item);

        Space Update(Guid itemId, Space item);
    }
}