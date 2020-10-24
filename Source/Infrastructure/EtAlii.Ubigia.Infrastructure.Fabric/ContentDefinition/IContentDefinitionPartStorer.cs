namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentDefinitionPartStorer
    {
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
    }
}