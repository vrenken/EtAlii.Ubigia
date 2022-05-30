// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Threading.Tasks;

    public interface IRootRemover
    {
        Task Remove(Guid spaceId, Guid rootId);
        Task Remove(Guid spaceId, Root root);
    }
}
