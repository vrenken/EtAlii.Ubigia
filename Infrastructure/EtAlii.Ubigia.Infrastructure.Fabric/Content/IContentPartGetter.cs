namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    public interface IContentPartGetter
    {
        IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId);
    }
}