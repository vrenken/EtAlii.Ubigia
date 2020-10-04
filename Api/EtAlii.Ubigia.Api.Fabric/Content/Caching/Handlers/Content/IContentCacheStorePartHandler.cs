namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheStorePartHandler
    {
        Task Handle(Identifier identifier, ContentPart contentPart);
    }
}