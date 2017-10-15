namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;

    public interface IRootRemover
    {
        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);
    }
}