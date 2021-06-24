// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISpaceContext : IStorageClientContext
    {
        Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate spaceTemplate);
        Task Remove(Guid spaceId);
        Task<Space> Change(Guid spaceId, string spaceName);
        Task<Space> Get(Guid accountId, string spaceName);
        Task<Space> Get(Guid spaceId);
        IAsyncEnumerable<Space> GetAll(Guid accountId);
    }
}
