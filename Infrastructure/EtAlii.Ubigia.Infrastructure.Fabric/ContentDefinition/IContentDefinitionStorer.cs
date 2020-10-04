namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentDefinitionStorer
    {
        void Store(Identifier identifier, ContentDefinition contentDefinition);
    }
}