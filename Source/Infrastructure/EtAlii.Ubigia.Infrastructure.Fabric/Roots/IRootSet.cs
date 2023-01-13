// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRootSet
{
    Task<Root> Add(Guid spaceId, Root root);

    IAsyncEnumerable<Root> GetAll(Guid spaceId);
    Task<Root> Get(Guid spaceId, Guid rootId);
    Task<Root> Get(Guid spaceId, string name);


    Task Remove(Guid spaceId, Guid rootId);
    Task Remove(Guid spaceId, Root root);

    Task<Root> Update(Guid spaceId, Guid rootId, Root updatedRoot);
}
