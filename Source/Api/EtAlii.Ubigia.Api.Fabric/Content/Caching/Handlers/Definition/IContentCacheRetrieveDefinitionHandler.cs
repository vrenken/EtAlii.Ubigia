namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheRetrieveDefinitionHandler
    {
        Task<ContentDefinition> Handle(Identifier identifier);
    }
}