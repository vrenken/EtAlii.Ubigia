namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IContentDefinitionPartStorer
    {
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
    }
}