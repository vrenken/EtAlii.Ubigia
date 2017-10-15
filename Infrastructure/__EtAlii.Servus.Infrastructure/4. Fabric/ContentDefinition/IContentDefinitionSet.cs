namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;

    public interface IContentDefinitionSet
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
        IReadOnlyContentDefinitionPart Get(Identifier identifier, UInt64 contentDefinitionPartId);
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        void Store(Identifier identifier, ContentDefinition contentDefinition);
    }
}