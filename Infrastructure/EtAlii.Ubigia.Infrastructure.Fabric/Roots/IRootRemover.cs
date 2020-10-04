namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    public interface IRootRemover
    {
        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);
    }
}