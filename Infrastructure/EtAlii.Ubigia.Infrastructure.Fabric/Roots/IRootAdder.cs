namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    public interface IRootAdder
    {
        Root Add(Guid spaceId, Root root);
    }
}