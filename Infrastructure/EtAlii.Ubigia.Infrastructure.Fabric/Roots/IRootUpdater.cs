namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    public interface IRootUpdater
    {
        Root Update(Guid spaceId, Guid rootId, Root updatedRoot);
    }
}