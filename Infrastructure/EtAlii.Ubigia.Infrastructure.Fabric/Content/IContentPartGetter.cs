namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IContentPartGetter
    {
        IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId);
    }
}