namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheRetrievePartHandler
    {
        Task<IReadOnlyContentPart> Handle(Identifier identifier, ulong contentPartId);
    }
}