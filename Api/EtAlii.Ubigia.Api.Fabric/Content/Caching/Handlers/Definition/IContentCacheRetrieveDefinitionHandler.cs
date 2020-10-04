namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheRetrieveDefinitionHandler
    {
        Task<IReadOnlyContentDefinition> Handle(Identifier identifier);
    }
}