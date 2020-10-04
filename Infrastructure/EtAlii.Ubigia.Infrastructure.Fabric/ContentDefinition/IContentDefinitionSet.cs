namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    public interface IContentDefinitionSet
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
        IReadOnlyContentDefinitionPart Get(Identifier identifier, UInt64 contentDefinitionPartId);
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        void Store(Identifier identifier, ContentDefinition contentDefinition);
    }
}