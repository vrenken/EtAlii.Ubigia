namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;

    public interface IRootUpdater
    {
        Root Update(Guid spaceId, Guid rootId, Root updatedRoot);
    }
}