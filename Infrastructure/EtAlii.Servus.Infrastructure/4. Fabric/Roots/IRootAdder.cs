namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;

    public interface IRootAdder
    {
        Root Add(Guid spaceId, Root root);
    }
}