namespace EtAlii.Ubigia.Infrastructure.Logical
{
    public interface ILogicalContentDefinitionSet
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
        IReadOnlyContentDefinitionPart Get(Identifier identifier, ulong contentDefinitionPartId);
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        void Store(Identifier identifier, ContentDefinition contentDefinition);
    }
}