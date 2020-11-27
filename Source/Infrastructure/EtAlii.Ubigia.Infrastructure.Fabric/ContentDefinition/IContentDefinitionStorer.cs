namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentDefinitionStorer
    {
        void Store(in Identifier identifier, ContentDefinition contentDefinition);
    }
}