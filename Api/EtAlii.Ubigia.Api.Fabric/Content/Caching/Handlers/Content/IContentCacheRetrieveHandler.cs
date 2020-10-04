namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheRetrieveHandler
    {
        Task<IReadOnlyContent> Handle(Identifier identifier);
    }
}