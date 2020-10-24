namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheStoreHandler
    {
        Task Handle(Identifier identifier, Content content);
    }
}