namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IContentDefinitionPartStorer
    {
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
    }
}