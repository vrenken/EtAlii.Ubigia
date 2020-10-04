﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public interface ISpaceRepository 
    {
        IEnumerable<Space> GetAll(Guid accountId);
        Space Get(Guid accountId, string spaceName);

		IEnumerable<Space> GetAll();
        Space Get(Guid itemId);

        Task<Space> Add(Space item, SpaceTemplate template);

        void Remove(Guid itemId);
        void Remove(Space item);

        Space Update(Guid itemId, Space item);
    }
}