namespace EtAlii.Servus.Infrastructure.Functional
{
    using EtAlii.Servus.Api;

    public interface IContentDefinitionRepository
    {
        void Store(Identifier identifier, ContentDefinition contentDefinition);
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        IReadOnlyContentDefinition Get(Identifier identifier);
    }
}