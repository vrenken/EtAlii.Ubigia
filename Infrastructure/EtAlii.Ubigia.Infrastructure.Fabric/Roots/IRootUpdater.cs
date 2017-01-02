namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IRootUpdater
    {
        Root Update(Guid spaceId, Guid rootId, Root updatedRoot);
    }
}