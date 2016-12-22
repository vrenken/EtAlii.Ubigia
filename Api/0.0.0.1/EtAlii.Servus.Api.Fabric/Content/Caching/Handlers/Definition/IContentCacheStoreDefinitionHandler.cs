namespace EtAlii.Servus.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheStoreDefinitionHandler
    {
        Task Handle(Identifier identifier, ContentDefinition definition);
    }
}