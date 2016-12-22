namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IRootGetter
    {
        IEnumerable<Root> GetAll(Guid spaceId);
        Root Get(Guid spaceId, Guid rootId);
        Root Get(Guid spaceId, string name);
    }
}