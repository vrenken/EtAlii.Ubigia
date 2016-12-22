namespace EtAlii.Servus.Api.Fabric
{
    using System;
    using System.Threading.Tasks;

    internal interface IContentCacheRetrievePartHandler
    {
        Task<IReadOnlyContentPart> Handle(Identifier identifier, UInt64 contentPartId);
    }
}