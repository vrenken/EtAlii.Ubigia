namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheRetrievePartHandler
    {
        Task<ContentPart> Handle(Identifier identifier, ulong contentPartId);
    }
}