namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public interface ILogicalContentSet
    {
        IReadOnlyContent Get(Identifier identifier);
        IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId);
        void Store(Identifier identifier, ContentPart contentPart);
        void Store(Identifier identifier, Content content);
    }
}