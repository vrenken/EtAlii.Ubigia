namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IRootAdder
    {
        Root Add(Guid spaceId, Root root);
    }
}