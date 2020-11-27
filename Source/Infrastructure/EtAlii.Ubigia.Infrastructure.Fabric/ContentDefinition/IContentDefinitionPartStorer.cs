namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentDefinitionPartStorer
    {
        void Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart);
    }
}