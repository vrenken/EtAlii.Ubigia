﻿namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;

    public interface IRootSet
    {
        Root Add(Guid spaceId, Root root);

        IEnumerable<Root> GetAll(Guid spaceId);
        Root Get(Guid spaceId, Guid rootId);
        Root Get(Guid spaceId, string name);


        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);

        Root Update(Guid spaceId, Guid rootId, Root updatedRoot);
    }
}