// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    public interface IRootRemover
    {
        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);
    }
}