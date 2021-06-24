// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;

    public interface ILogicalSpaceSet
    {
        IAsyncEnumerable<Space> GetAll(Guid accountId);
        IAsyncEnumerable<Space> GetAll();

        Space Get(Guid accountId, string spaceName);

        Space Get(Guid id);

        Space Add(Space item, SpaceTemplate template, out bool isAdded);
        
        void Remove(Guid itemId);

        void Remove(Space itemToRemove);

        Space Update(Guid itemId, Space updatedItem);
    }
}