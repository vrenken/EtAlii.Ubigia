namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public interface ILogicalContentDefinitionSet
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
        IReadOnlyContentDefinitionPart Get(Identifier identifier, UInt64 contentDefinitionPartId);
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        void Store(Identifier identifier, ContentDefinition contentDefinition);
    }
}