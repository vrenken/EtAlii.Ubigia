namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;

    public interface IContentSet
    {
        IReadOnlyContent Get(Identifier identifier);
        IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId);
        void Store(Identifier identifier, ContentPart contentPart);
        void Store(Identifier identifier, Content content);
    }
}