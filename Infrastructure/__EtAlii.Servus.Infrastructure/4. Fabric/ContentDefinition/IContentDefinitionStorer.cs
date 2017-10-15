namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IContentDefinitionStorer
    {
        void Store(Identifier identifier, ContentDefinition contentDefinition);
    }
}