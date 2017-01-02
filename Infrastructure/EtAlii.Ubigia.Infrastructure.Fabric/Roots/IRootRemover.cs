namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IRootRemover
    {
        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);
    }
}