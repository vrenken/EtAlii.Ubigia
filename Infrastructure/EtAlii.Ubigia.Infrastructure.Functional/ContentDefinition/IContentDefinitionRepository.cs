namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public interface IContentDefinitionRepository
    {
        void Store(Identifier identifier, ContentDefinition contentDefinition);
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        IReadOnlyContentDefinition Get(Identifier identifier);
    }
}