namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;

    public interface IContentPartGetter
    {
        IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId);
    }
}