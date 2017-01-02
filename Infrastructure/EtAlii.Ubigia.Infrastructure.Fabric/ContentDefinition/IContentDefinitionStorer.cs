namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IContentDefinitionStorer
    {
        void Store(Identifier identifier, ContentDefinition contentDefinition);
    }
}